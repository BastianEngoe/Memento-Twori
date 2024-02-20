using UnityEditor;
using UnityEngine.Audio;


[CustomEditor(typeof(PauseMenuButtons))]
public class PauseMenuButtonsEditor : Editor
{
    // public override void OnInspectorGUI()
    // {
    //     PauseMenuButtons pauseMenuButtons = (PauseMenuButtons)target;
    //
    //     // Draw default inspector properties
    //     DrawDefaultInspector();
    //
    //     // Check if the GameObject's name contains "Music" or "SFX"
    //     if (pauseMenuButtons.gameObject.name.Contains("Music") || pauseMenuButtons.gameObject.name.Contains("SFX"))
    //     {
    //         // Enable GUI for audioMixer
    //         EditorGUILayout.ObjectField("Audio Mixer", pauseMenuButtons.audioMixer, typeof(AudioMixer), false);
    //     }
    // }
}
