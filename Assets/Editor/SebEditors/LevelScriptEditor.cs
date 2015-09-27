using UnityEngine;
using System.Collections;
using UnityEditor;


public class LevelScriptEditor : EditorWindow {
	string myString = "Hello World";
	bool groupEnabled;
	bool myBool = true;
	float myFloat = 1.23f;
	
	// Add menu item named "My Window" to the Window menu
	[MenuItem("Window/My Window")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(LevelScriptEditor));
	}
	
	void OnGUI()
	{
		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField ("Text Field", myString);
		
		groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
		myBool = EditorGUILayout.Toggle ("Toggle", myBool);
		myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
		myFloat += 0.01f;
		EditorGUILayout.EndToggleGroup ();
	}

}

//[CustomEditor(typeof(Selected))]
//
//	public class LevelEditor : Editor {
//	public override void OnInspectorGUI()
//	{
//		Selected myTarget = (Selected)target;
//		
//		myTarget.RatioScaleLevel_c = EditorGUILayout.FloatField("RatioScaleLevel_c", myTarget.RatioScaleLevel_c);
//		EditorGUILayout.LabelField("Ratio", myTarget.Ratio.ToString());
//	}
//}
