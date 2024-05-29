using System.Collections;
using System.Collections.Generic;
using Course.PrototypeScripting;
using Course.PrototypeScripting;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ActionInventory))]
[CanEditMultipleObjects]

public class ActionInventoryEditor : Editor
{
    ActionInventory action;

    InventoryData invData;
    string[] invItemNames;

    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        action = target as ActionInventory;
        SerializedObject so = new SerializedObject(target);
        if (invData == null)
            LoadData();

      //  if (GUILayout.Button("Update Data"))
      //      LoadData();
        if (invData == null || invData.invItems == null || invData.invItems.Count == 0)
        {
            EditorGUILayout.LabelField("Keine Inventory Items erstellt.");
            EditorGUILayout.LabelField("Erstelle diese unter SimpleGame > Inventory im Menü");
            return;
        }

        int currentIndex = 0;
        for (int i = 0; i < invItemNames.Length; i++)
        {
            if (invItemNames[i] == action.invItemName)
                currentIndex = i;
        }
        so.FindProperty("invItemName").stringValue = invItemNames[EditorGUILayout.Popup("Inventory Item: ", currentIndex, invItemNames)];
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Action Type: ", GUILayout.Width(120));
        action.type = (ActionInventory.ActionType)EditorGUILayout.EnumPopup(action.type, GUILayout.Width(120));
        so.FindProperty("type").enumValueIndex = (int)action.type;
        so.FindProperty("amount").intValue = EditorGUILayout.IntField(action.amount);
        EditorGUILayout.EndHorizontal();
        so.ApplyModifiedProperties();
    }

    void LoadData()
    {
        invData = Resources.Load<InventoryData>("InventoryData");
        if (invData != null)
            invItemNames = invData.GetNames().ToArray();
    }
}
