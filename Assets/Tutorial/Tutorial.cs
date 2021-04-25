using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    [SerializeField] Text[] TutorialTexts = null;
    public bool Last = false;

    public void StartTutorial(string tutorial) {
        Text tutorialText = TutorialTexts.FirstOrDefault(tut => tut.name.Equals(tutorial));

        tutorialText.gameObject.SetActive(true);
        StartCoroutine(CheckInput(tutorialText));
    }

    public void StartTutorial(string tutorial, KeyCode Keycode) {
        Text tutorialText = TutorialTexts.FirstOrDefault(tut => tut.name.Equals(tutorial));

        tutorialText.gameObject.SetActive(true);
        StartCoroutine(CheckInput(Keycode, tutorialText));
    }

    public void StartTutorial(string tutorial, List<KeyCode> Keycode) {
        Text tutorialText = TutorialTexts.FirstOrDefault(tut => tut.name.Equals(tutorial));

        tutorialText.gameObject.SetActive(true);
        StartCoroutine(CheckInput(Keycode, tutorialText));
    }

    /// <summary>
    /// For checking place
    /// </summary>
    /// <param name="text">Tutorial text</param>
    /// <returns></returns>
    IEnumerator CheckInput(Text text) {
        PlayerDig playerDig = FindObjectOfType<PlayerDig>();

        int expectedInventory = playerDig.Inventory - 1;

        while (playerDig.Inventory != expectedInventory) {
            yield return null;
        }

        text.transform.parent.gameObject.SetActive(false);

        CheckLast();
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

        text.transform.parent.gameObject.SetActive(false);

        CheckLast();
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

        text.transform.parent.gameObject.SetActive(false);

        CheckLast();
    }

    void CheckLast() {
        if (Last) {
            Destroy(gameObject);
        }
    }
}
