using UnityEngine;

public class MainScene : MonoBehaviour {

    public string SceneToChange = "Introduction";

    public void Play() {
        SceneChanger.Instance.ChangeScene(SceneToChange);
    }

    public void HandleExitClick() {
        Application.Quit();
    }
}
