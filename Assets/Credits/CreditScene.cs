using UnityEngine;
using UnityEngine.UI;

public class CreditScene : MonoBehaviour {
    [SerializeField] Text DeathCounterText = null;
    [SerializeField] Text CollectibleCounterText = null;

    public void Start() {
        DeathCounterText.text = DeathCounter.DeathCount.ToString();
        CollectibleCounterText.text = CollectibleCounter.CollectibleCollected + " / " + CollectibleCounter.Instance.MaximumCollectibles;
    }

    public void StartOver() {
        SceneChanger.Instance.ChangeScene("Level 1");
    }

    public void Exit() {
        Application.Quit();
    }

}
