using System.Collections;
using UnityEngine;

public class IntroFadeIn : MonoBehaviour {
    public Animator NoFcksGivenText = null;
    public Animator TldrText = null;
    public Animator ContinueText = null;
    public ContinueScript ContinueTextScript = null;

    public TypeWriterEffect typeWriterEffect = null;

    public GameObject EscapeText;

    public float delay = 0.1f;
    public string animationToPlay = "fade_in";

    Coroutine SlowRead;

    // Start is called before the first frame update
    void Start() {
        SlowRead = this.StartCoroutine(CheckIsDone());
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            StartCoroutine(Skip());

            EscapeText.SetActive(false);
        }    
    }

    public IEnumerator Skip() {
        StopCoroutine(SlowRead);

        typeWriterEffect.Skip(true);

        NoFcksGivenText.Play(animationToPlay, 0, 1);
        TldrText.Play(animationToPlay, 0, 1);

        ContinueText.Play(animationToPlay, 0, 1);

        yield return new WaitForEndOfFrame();
        ContinueTextScript.isDisplayed = true;
    }

    IEnumerator CheckIsDone() {
        while (!typeWriterEffect.isDone) {
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(1.35f);

        NoFcksGivenText.Play(animationToPlay);
        yield return new WaitForSeconds(2f);

        TldrText.Play(animationToPlay);
        yield return new WaitForSeconds(2f);

        ContinueText.Play(animationToPlay);
        ContinueTextScript.isDisplayed = true;
    }
}
