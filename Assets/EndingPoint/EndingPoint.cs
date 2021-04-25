using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPoint : MonoBehaviour {
    public string SceneToChange = "";

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.gameObject.SetActive(false);
            SceneChanger.Instance.ChangeScene(SceneToChange);
        }
    }
}
