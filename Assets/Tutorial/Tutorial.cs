using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
    [SerializeField] Text[] TutorialTexts = null;

    public void StartTutorial(string tutorial) {
        Text tutorialText = TutorialTexts.FirstOrDefault(tut => tut.name.Equals(tutorial));

        tutorialText.gameObject.SetActive(true);
        StartCoroutine(CheckInput(tutorialText));
        StartCoroutine(GlowEffect(tutorialText));
    }

    public void StartTutorial(string tutorial, KeyCode Keycode) {
        Text tutorialText = TutorialTexts.FirstOrDefault(tut => tut.name.Equals(tutorial));

        tutorialText.gameObject.SetActive(true);
        StartCoroutine(CheckInput(Keycode, tutorialText));
        StartCoroutine(GlowEffect(tutorialText));
    }

    public void StartTutorial(string tutorial, List<KeyCode> Keycode) {
        Text tutorialText = TutorialTexts.FirstOrDefault(tut => tut.name.Equals(tutorial));

        tutorialText.gameObject.SetActive(true);
        StartCoroutine(CheckInput(Keycode, tutorialText));
        StartCoroutine(GlowEffect(tutorialText));
    }

    /// <summary>
    /// For checking place
    /// </summary>
    /// <param name="text">Tutorial text</param>
    /// <returns></returns>
    IEnumerator CheckInput(Text text) {
        int inventory = FindObjectOfType<PlayerDig>().Inventory;
        int expectedInventory = inventory - 1;

        while (inventory != expectedInventory) {
            yield return null;
        }

        Destroy(text);
    }

    /// <summary>
    /// For checking Jump & Dig
    /// </summary>
    /// <param name="keyCode">Keycode (Space & Shift)</param>
    /// <param name="text">Tutorial Text</param>
    /// <returns></returns>
    IEnumerator CheckInput(KeyCode keyCode, Text text) {

        while (!Input.GetKeyDown(keyCode)) {
            yield return null;
        }

        Destroy(text);
    }

    /// <summary>
    /// For checking Movement 
    /// </summary>
    /// <param name="keyCode">List of keycodes (A & D)</param>
    /// <param name="text"></param>
    /// <returns></returns>
    IEnumerator CheckInput(List<KeyCode> keyCodes, Text text) {

        bool success = false;
        while (!success) {

            foreach(KeyCode kc in keyCodes) {
                if (Input.GetKeyDown(kc)) {
                    success = true;
                    break;
                }
            }

            yield return null;
        }

        Destroy(text);
    }

    IEnumerator GlowEffect(Text text) {
        Color color = text.color;
        color.a = 1;

        // Fade out
        while (text.color.a > 0.25f) {
            color.a -= Time.deltaTime;
            text.color = color;

            yield return null;
        }

        color.a = 0.25f;

        // Fade In
        while (text.color.a < 1) {
            color.a += Time.deltaTime;
            text.color = color;

            yield return null;
        }

        StartCoroutine(GlowEffect(text));
    }
}
