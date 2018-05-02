using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardCreator : EditorWindow
{
    ScriptableObject baseObject;
    GameObject gameobj;
    Transform parent;
    ScriptableObject card;

    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Card creator")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        CardCreator window = (CardCreator) EditorWindow.GetWindow(typeof(CardCreator));
        window.Show();
    }

    void OnGUI()
    {

        #region myCode
        
        #endregion

        #region oldCode
        GUILayout.Label("", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
        #endregion
    }
}
