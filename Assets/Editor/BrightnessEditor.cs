using UnityEditor;
using UnityEngine;



[CustomEditor(typeof(Brightness))]
public class BrightnessEditor : Editor
{
    // public override void OnInspectorGUI()
    // {
    //     Brightness brightness = (Brightness)target;
    //
    //     // Draw default inspector properties
    //     DrawDefaultInspector();
    //
    //     // Disable GUI for brightnessValue
    //     GUI.enabled = false;
    //     EditorGUILayout.FloatField("Brightness Value", brightness.brightnessValue);
    //     GUI.enabled = true;
    // }
}
