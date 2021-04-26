using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScene : MonoBehaviour
{
    public void StartOver() {
        SceneChanger.Instance.ChangeScene("Level 1");
    }

    public void Exit() {
        Application.Quit();
    }

}
