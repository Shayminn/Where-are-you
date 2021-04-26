using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour {
    public float delay = 0.1f;
    public string fullText;

    public bool onStart = true;
    public bool isDone = false;

    private string currentText = "";

    // Start is called before the first frame update
    void Start() {
        fullText = fullText.Replace("\\n", "\n");

        if (onStart) {
            StartCoroutine(ShowText());
        }
    }

    public IEnumerator ShowText() {
        for (int i = 0; i < fullText.Length; i++) {
            currentText = fullText.Substring(0, i + 1);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }

        isDone = true;
    }
}
