using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleCounter : MonoBehaviour
{
    public static CollectibleCounter Instance;

    public static bool FirstRun = true;

    public static int CollectibleCollected = 0;
    public Image CollectibleImage;
    public Text CollectibleText;
    public float FadeOutDelay = 3f;
    public int MaximumCollectibles;

    // Start is called before the first frame update
    void Start()
    {
        if (FirstRun) {
            FirstRun = false;
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }


    public void IncreaseCounter() {
        CollectibleCollected++;

        CollectibleText.text = CollectibleCollected + " / " + MaximumCollectibles;

        StartCoroutine(FadeOut());
    }

    public void SetCollectibleCounter(int collectibleCollected) {
        CollectibleCounter.CollectibleCollected = collectibleCollected;

        CollectibleText.text = CollectibleCollected + " / " + MaximumCollectibles;

        FadeOut();
    }

    IEnumerator FadeOut() {
        Color color = CollectibleText.color;
        color.a = 1;

        CollectibleImage.color = color;
        CollectibleText.color = color;

        yield return new WaitForSeconds(FadeOutDelay);

        while (color.a > 0) {
            color.a -= Time.deltaTime;

            CollectibleImage.color = color;
            CollectibleText.color = color;

            yield return null;
        }
    }
}
