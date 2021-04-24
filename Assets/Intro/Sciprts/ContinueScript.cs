using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueScript : MonoBehaviour {
    public string SceneToChange = "Level 1";
    public bool isDisplayed = false;

    // Update is called once per frame
    void Update() {
        if (isDisplayed && Input.anyKeyDown) {
            SceneChanger.Instance.ChangeScene(SceneToChange);
        }
    }
}
