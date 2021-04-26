using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class StartingSceneOnPlay {
    private static string oldScene;
    private const string FIRST_SCENE_NAME = "DontDestroyOnLoad";

    static StartingSceneOnPlay() {
        EditorApplication.playModeStateChanged += StateChange;
    }

    static void StateChange(PlayModeStateChange state) {
        if (EditorApplication.isPlaying) {
            EditorApplication.playModeStateChanged -= StateChange;

            //Load First scene only if it's not the first scene
            if (SceneManager.GetActiveScene().name != FIRST_SCENE_NAME) {
                if (!EditorApplication.isPlayingOrWillChangePlaymode) {
                    //We're in playmode, just about to change playmode to Editor
                    EditorSceneManager.OpenScene(oldScene);
                }
                else {
                    //We're in playmode, right after having pressed Play
                    oldScene = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene(FIRST_SCENE_NAME);

                    if (SceneManager.GetSceneByBuildIndex(0).name != FIRST_SCENE_NAME) {
                        Debug.LogWarning("Make sure your starting scene is the first one on the list in the build settings.");
                    }
                }
            }
        }
    }
}