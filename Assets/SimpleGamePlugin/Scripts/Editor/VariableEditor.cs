using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Course.PrototypeScripting;

public class VariableEditor : EditorWindow
{
	static VariableData data;
	static string dataName = "VariableData";

	static List<GenericVariable> variableInfos;

	[MenuItem("Simple Game/Variable Editor")]
	static void Open()
	{

		VariableEditor window = (VariableEditor)EditorWindow.GetWindow(typeof(VariableEditor));
		Init();
	}



	static void Init()
	{
		data = (VariableData)Resources.Load("VariableData");
		if (data == null)
		{
			if (!Directory.Exists(Application.dataPath + "/SimpleGamePlugin/Resources/"))
				Directory.CreateDirectory(Application.dataPath + "/SimpleGamePlugin/Resources/");
			CreateAsset<VariableData>("Assets/SimpleGamePlugin/Resources/" + dataName + ".asset");
		}
		data = (VariableData)Resources.Load("VariableData");
		variableInfos = new List<GenericVariable>();
		variableInfos = data.variableInfos;
	}

	void OnGUI()
	{
		if (data == null || variableInfos == null)
			Init();

		if(variableInfos != null && variableInfos.Count > 0)
		{
			for(int i = 0; i < variableInfos.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				variableInfos[i].name = EditorGUILayout.TextField(variableInfos[i].name);
				variableInfos[i].value =  EditorGUILayout.IntField(variableInfos[i].value);
				if (GUILayout.Button("X"))
				{
					DeleteEntry(i);
					EditorGUILayout.EndHorizontal();
					return;
				}
				EditorGUILayout.EndHorizontal();
			}

		}


		if (GUILayout.Button("+ New Variable"))
			AddVariable();

		if (GUILayout.Button("Save"))
			Save();
	}

	void DeleteEntry(int index)
	{
		variableInfos.RemoveAt(index);
	}

	void AddVariable()
	{
		GenericVariable v = new GenericVariable();
		v.name = "New";
		if (variableInfos == null)
			variableInfos = new List<GenericVariable>();
		variableInfos.Add(v);
	}

	public void Save()
	{
		VariableData newData = new VariableData();

		newData.variableInfos = variableInfos;

		ReplaceAsset<VariableData>("Assets/SimpleGamePlugin/Resources/" + dataName + ".asset", newData);

		data = newData;
	}

	public static void CreateAsset<T>(string path) where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T>();


		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path);
		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();

		Selection.activeObject = asset;
	}

	public static void ReplaceAsset<T>(string path, T data) where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T>();
		asset = data;
		AssetDatabase.DeleteAsset(path);
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path);
		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();
		EditorUtility.FocusProjectWindow();
	}
}
