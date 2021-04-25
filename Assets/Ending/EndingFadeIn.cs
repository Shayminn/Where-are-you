using System.Collections;
using UnityEngine;

public class EndingFadeIn : MonoBehaviour {
    public Animator OrIsItText = null;
    public Animator NextEpisodeText = null;
    public Animator WhereAreYouText = null;
    public Animator TheEndText = null;
    public Animator ContinueText = null;
    public ContinueScript ContinueTextScript = null;

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

        OrIsItText.Play(animationToPlay);
        yield return new WaitForSeconds(2f);

        NextEpisodeText.Play(animationToPlay);
        WhereAreYouText.Play(animationToPlay);
        yield return new WaitForSeconds(2.4f);

        TheEndText.Play(animationToPlay);
        yield return new WaitForSeconds(3f);

        ContinueText.Play(animationToPlay);
        ContinueTextScript.isDisplayed = true;
    }
}
