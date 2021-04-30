using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour {
    [SerializeField] Animator Animator = null;
    [SerializeField] CanvasScaler CanvasScaler = null;

    [SerializeField] string FirstSceneName = "";
    [SerializeField] float ChangeDelay = 1f;

    public static SceneChanger Instance;
    public static int CurrentLevel = 1;

    string SceneName;
    static bool FirstRun = true;
    static bool ChangingScenes = false;

    private void Start() {
        if (FirstRun) {
            FirstRun = false;

            StartCoroutine(LateStart());
        }
    }

    IEnumerator LateStart() {
        yield return new WaitForEndOfFrame();

        // Change square size according to screen size
        CanvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = CanvasScaler.referenceResolution;

        SceneManager.LoadScene(FirstSceneName);
        Instance = this;
    }

    public void ChangeScene(string sceneName) {
        if (!ChangingScenes) {
            this.SceneName = sceneName;
            StartCoroutine(SceneChange());
        }
    }

    IEnumerator SceneChange() {
        ChangingScenes = true;

        yield return new WaitForSeconds(ChangeDelay);

        Animator.Play("fadeIn");

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone) {
            AnimatorStateInfo state = Animator.GetCurrentAnimatorStateInfo(0);

            if (asyncOperation.progress >= 0.9f
                && state.length < state.normalizedTime) {
                asyncOperation.allowSceneActivation = true;
                Animator.Play("fadeOut");
            }
            yield return null;
        }

        ChangingScenes = false;
    }
}
