using System.Collections;
using UnityEngine;

public class IntroFadeIn : MonoBehaviour {
    public Animator NoFcksGivenText = null;
    public Animator TldrText = null;
    public Animator ContinueText = null;

    public TypeWriterEffect typeWriterEffect = null;

    public float delay = 0.1f;
    public string animationToPlay = "fade_in";

    // Start is called before the first frame update
    void Start() {
        this.StartCoroutine(CheckIsDone());
    }

    IEnumerator CheckIsDone() {
        while (!typeWriterEffect.isDone) {
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(1.35f);

        NoFcksGivenText.Play(animationToPlay);
        yield return new WaitForSeconds(2.4f);

        TldrText.Play(animationToPlay);
        yield return new WaitForSeconds(3f);

        ContinueText.Play(animationToPlay);
    }
}
