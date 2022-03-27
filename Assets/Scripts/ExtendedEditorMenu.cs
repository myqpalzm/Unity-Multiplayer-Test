#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

#endif

#if UNITY_EDITOR

public class ExtendedEditorMenu : MonoBehaviour
{
    [MenuItem("Shortcuts/Open Launcher Scene")]
    private static void OpenLauncherScene() {
        EditorSceneManager.OpenScene("Assets/Scenes/Launcher.unity");
    }

    [MenuItem("Shortcuts/Open Room Scene")]
    private static void OpenRoomScene() {
        EditorSceneManager.OpenScene("Assets/Scenes/Room for 1.unity");
    }
}

#endif
