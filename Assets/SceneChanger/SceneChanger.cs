using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    [SerializeField] Animator animator = null;

    [SerializeField] string firstSceneName = "";
    [SerializeField] float changeDelay = 1f;

    public static SceneChanger Instance;
    public static int currentLevel = 1;

    string sceneName;
    static bool firstRun = true;
    static bool changingScenes = false;

    private void Start() {
        if (firstRun) {
            firstRun = false;

            StartCoroutine(LateStart());
        }
    }

    IEnumerator LateStart() {
        yield return new WaitForEndOfFrame();

        // Change square size according to screen size
        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

        SceneManager.LoadScene(firstSceneName);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(string sceneName) {
        if (!changingScenes) {
            this.sceneName = sceneName;
            StartCoroutine(SceneChange());
        }
    }

    IEnumerator SceneChange() {
        changingScenes = true;

        yield return new WaitForSeconds(changeDelay);

        animator.Play("fadeIn");

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone) {
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

            if (asyncOperation.progress >= 0.9f
                && state.length < state.normalizedTime) {
                asyncOperation.allowSceneActivation = true;
                animator.Play("fadeOut");
            }
            yield return null;
        }

        changingScenes = false;
    }
}
