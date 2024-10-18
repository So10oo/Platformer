using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public class CharacterField : VisualElement
    {
        public Texture2D Icon => (Texture2D)propertyValueImage.value;
        public string Name => propertyValueTextField.value;
        public string ID { get; private set; }

        TextField propertyValueTextField;
        TextField textField;
        BlackboardField field;
        ObjectField propertyValueImage;
        public event Action<CharacterField> OnNameChanged;

        public CharacterField(string nameCharacter, Texture2D icon = null, string id = null)
        {
            ID = id ?? Guid.NewGuid().ToString();
            field = new BlackboardField() 
            { 
                text = nameCharacter, 
                typeText = "Character", 
                userData = this,
            };
            textField = field.Q<TextField>("textField");
            textField.RegisterValueChangedCallback(NameChangedTextField);
            this.Add(field);
            var container = new VisualElement();
            propertyValueTextField = new TextField("Name:")
            {
                value = nameCharacter
            };
            propertyValueTextField.RegisterValueChangedCallback(NameChangedPropertyValueTextField);
            propertyValueImage = new ObjectField("Icon:")
            {
                objectType = typeof(Texture),
                value = icon,
            };
            container.Add(propertyValueTextField);
            container.Add(propertyValueImage);
            var blackboardRow = new BlackboardRow(field, container);
            this.Add(blackboardRow);
        }


        ~CharacterField()
        {
            textField.UnregisterValueChangedCallback(NameChangedTextField);
            propertyValueTextField.UnregisterValueChangedCallback(NameChangedPropertyValueTextField);
        }

        private void NameChangedTextField(ChangeEvent<string> callback)
        {
            propertyValueTextField.value = callback.newValue;
            Debug.Log("NameChangedTextField");
            OnNameChanged?.Invoke(this);
        }

        private void NameChangedPropertyValueTextField(ChangeEvent<string> callback)
        {
            field.text = callback.newValue;
            Debug.Log("NameChangedPropertyValueTextField");
            OnNameChanged?.Invoke(this);
        }

        public override string ToString()
        {
            return propertyValueTextField?.value;
        }
    }

}
