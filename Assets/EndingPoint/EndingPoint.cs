using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingPoint : MonoBehaviour {
    public bool Tutorial = false;
    public string SceneToChange = "";

    int level;

    void Start() {
        if (!Tutorial) {
            string sceneName = SceneManager.GetActiveScene().name;
            level = int.Parse(sceneName.Substring(sceneName.Length - 1));
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (!Tutorial) {
                TransitionScene.CompletedLevel = level;
            }

            collision.gameObject.SetActive(false);
            SceneChanger.Instance.ChangeScene(SceneToChange);
        }
    }
}
