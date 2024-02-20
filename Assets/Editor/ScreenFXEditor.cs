using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ScreenEffects))]
public class ScreenFXEditor : UnityEditor.Editor
{
    // public override void OnInspectorGUI()
    // {
    //     ScreenEffects screenEffectsScript = (ScreenEffects)target;
    //
    //     // Draw default inspector properties
    //     DrawDefaultInspector();
    //
    //     // Disable GUI for brightnessValue
    //     GUI.enabled = false;
    //     EditorGUILayout.Toggle("Screen Effects Enabled", screenEffectsScript.screenEffectsEnabled);
    //     GUI.enabled = true;
    // }
}
