using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour {
    [SerializeField] Animator PausePanelAnimator = null;

    [SerializeField] Text DeathCounterText = null;
    [SerializeField] Text CollectibleCounterText = null;

    readonly KeyCode Open = KeyCode.Escape;

    bool Opened = false;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(Open)) {
            if (!Opened) {
                OpenUI();
            }
        }
    }

    void OpenUI() {
        Opened = true;

        DeathCounterText.text = DeathCounter.DeathCount.ToString();
        CollectibleCounterText.text = CollectibleCounter.CollectibleCollected + " / " + CollectibleCounter.Instance.MaximumCollectibles;

        PausePanelAnimator.Play("SlideDown");

        Time.timeScale = 0;
    }

    void CloseUI() {
        Opened = false;

        PausePanelAnimator.Play("SlideUp");

        Time.timeScale = 1;
    }

    public void Resume() {
        CloseUI();
    }

    public void RestartFromIntroduction() {
        CloseUI();

        SceneChanger.Instance.ChangeScene("Introduction");
    }

    public void RestartFromBeginning() {
        CloseUI();

        SceneChanger.Instance.ChangeScene("Level 1");
    }

    public void Exit() {
        Application.Quit();
    }
}
