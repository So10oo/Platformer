using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public class DSNode : Node, ISetStyleError
    {
        public string ID { get; set; }
        public string DialogueName { get; set; }
        public List<DSChoiceSaveData> Choices { get; set; }
        public string Text { get; set; }
        public DSGroup Group { get; set; }
        public CharacterField Character => popupField?.value;
            
        private Color defaultBackgroundColor;

        public event Action<DSNode, string, string> OnRename;

        PopupField<CharacterField> popupField;

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Disconnect Input Ports", actionEvent => DisconnectInputPorts());
            evt.menu.AppendAction("Disconnect Output Ports", actionEvent => DisconnectOutputPorts());
            base.BuildContextualMenu(evt);
        }

        //List<CharacterField> _characters;
        public virtual void Initialize(string nodeName, Vector2 position, List<CharacterField> characters)
        {
            ID = Guid.NewGuid().ToString();
            DialogueName = nodeName;
            Choices = new List<DSChoiceSaveData>();
            Text = "Dialogue text.";
            SetPosition(new Rect(position, Vector2.zero));

            popupField = new PopupField<CharacterField>(characters, 0);

            defaultBackgroundColor = new Color(29f / 255f, 29f / 255f, 30f / 255f);
            mainContainer.AddToClassList("ds-node__main-container");
            extensionContainer.AddToClassList("ds-node__extension-container");
        }

        public virtual void Initialize(DSNodeSaveData node, List<CharacterField> characters)
        {
            ID = node.ID;
            DialogueName = node.Name;
            Choices = node.Choices.Clone();
            Text = node.Text;
            SetPosition(new Rect(node.Position, Vector2.zero));

            popupField = new PopupField<CharacterField>(characters, 0);

            popupField.value = characters.Find(cf => cf.ID == node.CharacterID);
            defaultBackgroundColor = new Color(29f / 255f, 29f / 255f, 30f / 255f);
            mainContainer.AddToClassList("ds-node__main-container");
            extensionContainer.AddToClassList("ds-node__extension-container");
        }

        public virtual void Draw()
        {
            /* TITLE CONTAINER */
            TextField dialogueNameTextField = DSElementUtility.CreateTextField(DialogueName, null, callback =>
            {
                TextField target = (TextField)callback.target;
                target.value = callback.newValue.RemoveWhitespaces().RemoveSpecialCharacters();
                OnRename?.Invoke(this, DialogueName, target.value);
                DialogueName = target.value;

            });

            dialogueNameTextField.AddClasses(
                "ds-node__text-field",
                "ds-node__text-field__hidden",
                "ds-node__filename-text-field"
            );

            titleContainer.Insert(0, dialogueNameTextField);

             
            if (popupField.value != null)
                popupField.value.OnNameChanged += OnNameChanged;
            popupField.RegisterValueChangedCallback(ChangePopupField);
            titleContainer.Insert(1, popupField);

            /* INPUT CONTAINER */

            Port inputPort = this.CreatePort("Dialogue Connection", Orientation.Horizontal, Direction.Input, Port.Capacity.Multi);

            inputContainer.Add(inputPort);

            /* EXTENSION CONTAINER */

            VisualElement customDataContainer = new VisualElement();
            customDataContainer.AddToClassList("ds-node__custom-data-container");
            Foldout textFoldout = DSElementUtility.CreateFoldout("Dialogue Text");
            TextField textTextField = DSElementUtility.CreateTextArea(Text, null, callback => Text = callback.newValue);
            textTextField.AddClasses(
                "ds-node__text-field",
                "ds-node__quote-text-field"
            );

            textFoldout.Add(textTextField);
            customDataContainer.Add(textFoldout);
            extensionContainer.Add(customDataContainer);
        }

        private void OnNameChanged(CharacterField character)
        {
            popupField.SetValueWithoutNotify(character);
        }

        private void ChangePopupField(ChangeEvent<CharacterField> callback)
        {
            if (callback.previousValue != null) 
                callback.previousValue.OnNameChanged -= OnNameChanged;
            callback.newValue.OnNameChanged += OnNameChanged;
        }


        #region DisconnectPorts
        public event Action<IEnumerable<Edge>> OnDisconnectPorts;
        protected void DisconnectPorts(IEnumerable<Edge> edges)
        {
            OnDisconnectPorts?.Invoke(edges);
        }

        public void DisconnectAllPorts()
        {
            DisconnectInputPorts();
            DisconnectOutputPorts();
        }

        private void DisconnectInputPorts()
        {
            DisconnectPorts(inputContainer);
        }

        private void DisconnectOutputPorts()
        {
            DisconnectPorts(outputContainer);
        }

        private void DisconnectPorts(VisualElement container)
        {
            foreach (Port port in container.Children())
            {
                if (port.connected)
                    DisconnectPorts(port.connections);
            }
        }
        #endregion

        public bool IsStartingNode()
        {
            Port inputPort = (Port)inputContainer.Children().First();

            return !inputPort.connected;
        }

        public void SetErrorStyle(Color color)
        {
            mainContainer.style.backgroundColor = color;
        }

        public void SetDefaultStyle()
        {
            mainContainer.style.backgroundColor = defaultBackgroundColor;
        }

    }
}