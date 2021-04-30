using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingFadeIn : MonoBehaviour {
    public Animator TheEndText = null;
    public Animator ContinueText = null;
    public ContinueScript ContinueTextScript = null;

    public List<TypeWriterEffect> EndingTexts = null;

    public float delay = 0.1f;
    public string animationToPlay = "fade_in";

    public Animator GyuStyfe;
    public SpriteRenderer GyuStyfeImg;
    public int IndexToFadeIn = 5;

    public GameObject EscapeText;

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

        foreach (TypeWriterEffect tw in EndingTexts) {
            tw.Skip();
            
            if (tw.TypeWriteEffect != null) {
                StopCoroutine(tw.TypeWriteEffect);
            }
        }

        Color color = GyuStyfeImg.color;
        color.a = 1;
        GyuStyfeImg.color = color; 

        TheEndText.Play(animationToPlay, 0, 1);

        ContinueText.Play(animationToPlay, 0, 1);

        yield return new WaitForEndOfFrame();
        ContinueTextScript.isDisplayed = true;
    }

    IEnumerator CheckIsDone() {
        int index = 0;

        Color color = GyuStyfeImg.color;

        while (EndingTexts.Count > 0) {
            EndingTexts[0].TypeWriteEffect = StartCoroutine(EndingTexts[0].ShowText());

            while (!EndingTexts[0].isDone) {
                yield return new WaitForSeconds(delay);
            }

            if (++index == IndexToFadeIn) {
                while (color.a < 1) {
                    color.a += Time.deltaTime;
                    GyuStyfeImg.color = color;

                    yield return null;
                }
            }

            EndingTexts.RemoveAt(0);
            yield return new WaitForSeconds(1.5f);
        }

        TheEndText.Play(animationToPlay);
        yield return new WaitForSeconds(1f);

        ContinueText.Play(animationToPlay);
        ContinueTextScript.isDisplayed = true;
    }
}
