using DialogueSystem.Realtime;
using System.Collections.Generic;

namespace DialogueSystem.Editor
{
    using System.Linq;
    using UnityEditor;

    //[CustomEditor(typeof(DSDialogue))]
    //public class DSInspector : Editor
    //{
    //    /* Dialogue Scriptable Objects */
    //    private SerializedProperty dialogueContainerProperty;
    //    private SerializedProperty dialogueGroupProperty;
    //    private SerializedProperty dialogueProperty;

    //    /* Filters */
    //    private SerializedProperty groupedDialoguesProperty;
    //    private SerializedProperty startingDialoguesOnlyProperty;

    //    /* Indexes */
    //    private SerializedProperty selectedDialogueGroupIndexProperty;
    //    private SerializedProperty selectedDialogueIndexProperty;

    //    private void OnEnable()
    //    {
    //        dialogueContainerProperty = serializedObject.FindProperty("dialogueContainer");
    //        dialogueGroupProperty = serializedObject.FindProperty("dialogueGroup");
    //        dialogueProperty = serializedObject.FindProperty("dialogue");

    //        groupedDialoguesProperty = serializedObject.FindProperty("groupedDialogues");
    //        startingDialoguesOnlyProperty = serializedObject.FindProperty("startingDialoguesOnly");

    //        selectedDialogueGroupIndexProperty = serializedObject.FindProperty("selectedDialogueGroupIndex");
    //        selectedDialogueIndexProperty = serializedObject.FindProperty("selectedDialogueIndex");
    //    }

    //    public override void OnInspectorGUI()
    //    {
    //        serializedObject.Update();

    //        DrawDialogueContainerArea();

    //        DSDialogueContainerSO currentDialogueContainer = (DSDialogueContainerSO)dialogueContainerProperty.objectReferenceValue;

    //        if (currentDialogueContainer == null)
    //        {
    //            StopDrawing("Select a Dialogue Container to see the rest of the Inspector.");

    //            return;
    //        }

    //        DrawFiltersArea();

    //        bool currentGroupedDialoguesFilter = groupedDialoguesProperty.boolValue;
    //        bool currentStartingDialoguesOnlyFilter = startingDialoguesOnlyProperty.boolValue;

    //        List<string> dialogueNames;

    //        string dialogueFolderPath = $"Assets/DialogueSystem/Dialogues/{currentDialogueContainer.FileName}";

    //        string dialogueInfoMessage;

    //        if (currentGroupedDialoguesFilter)
    //        {
    //            List<string> dialogueGroupNames = currentDialogueContainer.GetDialogueGroupNames();

    //            if (dialogueGroupNames.Count == 0)
    //            {
    //                StopDrawing("There are no Dialogue Groups in this Dialogue Container.");

    //                return;
    //            }

    //            DrawDialogueGroupArea(currentDialogueContainer, dialogueGroupNames);

    //            DSDialogueGroupSO dialogueGroup = (DSDialogueGroupSO)dialogueGroupProperty.boxedValue;

    //            dialogueNames = currentDialogueContainer.GetGroupedDialogueNames(dialogueGroup, currentStartingDialoguesOnlyFilter);

    //            dialogueFolderPath += $"/Groups/{dialogueGroup.GroupName}/Dialogues";

    //            dialogueInfoMessage = "There are no" + (currentStartingDialoguesOnlyFilter ? " Starting" : "") + " Dialogues in this Dialogue Group.";
    //        }
    //        else
    //        {
    //            dialogueNames = currentDialogueContainer.GetUngroupedDialogueNames(currentStartingDialoguesOnlyFilter);

    //            dialogueFolderPath += "/Global/Dialogues";

    //            dialogueInfoMessage = "There are no" + (currentStartingDialoguesOnlyFilter ? " Starting" : "") + " Ungrouped Dialogues in this Dialogue Container.";
    //        }

    //        if (dialogueNames.Count == 0)
    //        {
    //            StopDrawing(dialogueInfoMessage);

    //            return;
    //        }

    //        DrawDialogueArea(dialogueNames, dialogueFolderPath, currentDialogueContainer);

    //        serializedObject.ApplyModifiedProperties();
    //    }

    //    private void DrawDialogueContainerArea()
    //    {
    //        DSInspectorUtility.DrawHeader("Dialogue Container");

    //        dialogueContainerProperty.DrawPropertyField();

    //        DSInspectorUtility.DrawSpace();
    //    }

    //    private void DrawFiltersArea()
    //    {
    //        DSInspectorUtility.DrawHeader("Filters");

    //        groupedDialoguesProperty.DrawPropertyField();
    //        startingDialoguesOnlyProperty.DrawPropertyField();

    //        DSInspectorUtility.DrawSpace();
    //    }

    //    private void DrawDialogueGroupArea(DSDialogueContainerSO dialogueContainer, List<string> dialogueGroupNames)
    //    {
    //        DSInspectorUtility.DrawHeader("Dialogue Group");

    //        int oldSelectedDialogueGroupIndex = selectedDialogueGroupIndexProperty.intValue;

    //        DSDialogueGroupSO oldDialogueGroup = (DSDialogueGroupSO)dialogueGroupProperty.boxedValue;

    //        bool isOldDialogueGroupNull = oldDialogueGroup == null;

    //        string oldDialogueGroupName = isOldDialogueGroupNull ? "" : oldDialogueGroup.GroupName;

    //        UpdateIndexOnNamesListUpdate(dialogueGroupNames, selectedDialogueGroupIndexProperty, oldSelectedDialogueGroupIndex, oldDialogueGroupName, isOldDialogueGroupNull);

    //        selectedDialogueGroupIndexProperty.intValue = DSInspectorUtility.DrawPopup("Dialogue Group", selectedDialogueGroupIndexProperty, dialogueGroupNames.ToArray());

    //        string selectedDialogueGroupName = dialogueGroupNames[selectedDialogueGroupIndexProperty.intValue];

    //        DSDialogueGroupSO selectedDialogueGroup = dialogueContainer.DialogueGroups.Keys.First(x => x.GroupName == selectedDialogueGroupName);//DSIOUtility.LoadAsset<DSDialogueGroupSO>($"Assets/DialogueSystem/Dialogues/{dialogueContainer.FileName}/Groups/{selectedDialogueGroupName}", selectedDialogueGroupName);

    //        dialogueGroupProperty.boxedValue = selectedDialogueGroup;

    //        DSInspectorUtility.DrawDisabledFields(() => dialogueGroupProperty.DrawPropertyField());

    //        DSInspectorUtility.DrawSpace();
    //    }

    //    private void DrawDialogueArea(List<string> dialogueNames, string dialogueFolderPath, DSDialogueContainerSO dialogueContainer)
    //    {
    //        DSInspectorUtility.DrawHeader("Dialogue");

    //        int oldSelectedDialogueIndex = selectedDialogueIndexProperty.intValue;

    //        DSDialogueSO oldDialogue = (DSDialogueSO)dialogueProperty.boxedValue;

    //        bool isOldDialogueNull = oldDialogue == null;

    //        string oldDialogueName = isOldDialogueNull ? "" : oldDialogue.DialogueName;

    //        UpdateIndexOnNamesListUpdate(dialogueNames, selectedDialogueIndexProperty, oldSelectedDialogueIndex, oldDialogueName, isOldDialogueNull);

    //        selectedDialogueIndexProperty.intValue = DSInspectorUtility.DrawPopup("Dialogue", selectedDialogueIndexProperty, dialogueNames.ToArray());

    //        string selectedDialogueName = dialogueNames[selectedDialogueIndexProperty.intValue];

    //        DSDialogueSO selectedDialogue = dialogueContainer.UngroupedDialogues.First(x => x.DialogueName == selectedDialogueName);//DSIOUtility.LoadAsset<DSDialogueSO>(dialogueFolderPath, selectedDialogueName);

    //        dialogueProperty.boxedValue = selectedDialogue;

    //        DSInspectorUtility.DrawDisabledFields(() => dialogueProperty.DrawPropertyField());
    //    }

    //    private void StopDrawing(string reason, MessageType messageType = MessageType.Info)
    //    {
    //        DSInspectorUtility.DrawHelpBox(reason, messageType);

    //        DSInspectorUtility.DrawSpace();

    //        DSInspectorUtility.DrawHelpBox("You need to select a Dialogue for this component to work properly at Runtime!", MessageType.Warning);

    //        serializedObject.ApplyModifiedProperties();
    //    }

    //    private void UpdateIndexOnNamesListUpdate(List<string> optionNames, SerializedProperty indexProperty, int oldSelectedPropertyIndex, string oldPropertyName, bool isOldPropertyNull)
    //    {
    //        if (isOldPropertyNull)
    //        {
    //            indexProperty.intValue = 0;

    //            return;
    //        }

    //        bool oldIndexIsOutOfBoundsOfNamesListCount = oldSelectedPropertyIndex > optionNames.Count - 1;
    //        bool oldNameIsDifferentThanSelectedName = oldIndexIsOutOfBoundsOfNamesListCount || oldPropertyName != optionNames[oldSelectedPropertyIndex];

    //        if (oldNameIsDifferentThanSelectedName)
    //        {
    //            if (optionNames.Contains(oldPropertyName))
    //            {
    //                indexProperty.intValue = optionNames.IndexOf(oldPropertyName);

    //                return;
    //            }

    //            indexProperty.intValue = 0;
    //        }
    //    }
    //}
}