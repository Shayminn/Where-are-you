using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCounter : MonoBehaviour
{
    [SerializeField] Text DeathCountText = null;
    [SerializeField] Text DeathCountCounter = null; 

    public static DeathCounter Instance;
    public static float DeathCount = 0;

    public float FadeOutDelay = 1f;

    static bool FirstRun = true;

    // Start is called before the first frame update
    void Start()
    {
        if (FirstRun) {
            FirstRun = false;
            DeathCounter.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Increment() {
        DeathCount++;
        DeathCountCounter.text = DeathCount.ToString();

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() {
        Color color = DeathCountText.color;
        color.a = 1;

        DeathCountText.color = color;
        DeathCountCounter.color = color;

        yield return new WaitForSeconds(FadeOutDelay);

        while (DeathCountText.color.a > 0) {
            color.a -= Time.deltaTime;

            DeathCountText.color = color;
            DeathCountCounter.color = color;

            yield return null;
        }
    }
}
