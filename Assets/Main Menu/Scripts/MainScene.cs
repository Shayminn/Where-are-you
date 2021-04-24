using UnityEngine;

public class MainScene : MonoBehaviour {

    public void Play() {
        SceneChanger.Instance.ChangeScene("Level 1");
    }

    public void HandleExitClick() {
        Application.Quit();
    }
}
