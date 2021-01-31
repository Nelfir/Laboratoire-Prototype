// using UnityEngine;
// using UnityEditor;
// using System.Collections;

// /*
// Class AnaglyphSourceEditor:

// CustomEditor for the AnaglyphSource class.
// */

// [CustomEditor(typeof(AnaglyphSource))] 
// public class EvertSourceEditor : Editor
// {
// 	// There is a variable called 'target' that comes from the Editor, its the script we are extending but to
// 	// make it easy to use we will decalre a new variable called '_target' that will cast this 'target' to our script type
// 	// otherwise you will need to cast it everytime you use it like this: int i = (ourType)target;
// 	AnaglyphSource _target;

// 	void OnEnable()
// 	{
// 		_target = (AnaglyphSource)target;
// 	}

// 	public override void OnInspectorGUI()
// 	{
// 		GUILayout.BeginVertical();

// 		GUILayout.Label ("Anaglyph audio source", EditorStyles.boldLabel);

// 		_target.listenerHead = (Transform)EditorGUILayout.ObjectField ("Listener Head", _target.listenerHead, typeof(Transform), true);
		
// 		_target.bypassReverb = EditorGUILayout.Toggle("Bypass Reverb", _target.bypassReverb);

// 		//	// prevent setup if config file defined
// 		//	if(_target.configFile)
// 		//	{    
// 		//		EditorGUILayout.HelpBox("Empty audio file name", MessageType.Warning);
// 		//	}    

// 		// EditorGUILayout.Space ();

// 		GUILayout.EndVertical();

// 		//If we changed the GUI aply the new values to the script
// 		if(GUI.changed)
// 		{
// 			EditorUtility.SetDirty(_target);            
// 		}
// 	}
// }