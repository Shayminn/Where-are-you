using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GlowEffect : MonoBehaviour {
    public bool StartFadeIn = true;

    void Start() {
        Text text = GetComponent<Text>();

        if (StartFadeIn) {
            StartCoroutine(FadeIn(text));
        }
        else {
            StartCoroutine(FadeOut(text));
        }
    }

    IEnumerator FadeOut(Text text) {
        Color color = text.color;
        color.a = 1;

        text.color = color;

        // Fade out
        while (text.color.a > 0.25f) {
            color.a -= Time.deltaTime;
            text.color = color;

            yield return null;
        }

        StartCoroutine(FadeIn(text));
    }

    IEnumerator FadeIn(Text text) {

        Color color = text.color;
        color.a = 0.25f;

        text.color = color;

        // Fade In
        while (text.color.a < 1) {
            color.a += Time.deltaTime;
            text.color = color;

            yield return null;
        }

        StartCoroutine(FadeOut(text));
    }
}
