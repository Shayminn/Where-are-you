using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour {
    public float delay = 0.1f;
    public string fullText;

    public bool onStart = true;
    public bool isDone = false;

    private string currentText = "";

    public Text Text;
    public Coroutine TypeWriteEffect;

    void Awake() {
        Text = GetComponent<Text>();
        fullText = Text.text;
        Text.text = "";
    }

    // Start is called before the first frame update
    void Start() {
        if (onStart) {
            TypeWriteEffect = StartCoroutine(ShowText());
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

    public void Skip(bool stopCoroutine = false) {
        if (stopCoroutine) {
            StopCoroutine(TypeWriteEffect);
        }

        Text.text = fullText;

        isDone = true;
    }
}
