using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour {
    [SerializeField] Animator PausePanelAnimator = null;

    [SerializeField] Text DeathCounterText = null;
    [SerializeField] Text CollectibleCounterText = null;

    [SerializeField] Slider VolumeSlider = null;
    [SerializeField] Text VolumeText = null;

    readonly KeyCode Open = KeyCode.Escape;

    public bool Opened = false;

    void Start() {
        VolumeSlider.value = AudioManager.Instance.GetVolume();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(Open)) {
            if (PausePanelAnimator.GetCurrentAnimatorStateInfo(0).length < PausePanelAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime) {
                if (!Opened) {
                    OpenUI();
                }
                else {
                    CloseUI();
                }
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

    public void RestartFromBeginning() {
        CloseUI();

        CollectibleCounter.Instance.ResetValues();
        DeathCounter.Instance.ResetValues();

        SceneChanger.Instance.ChangeScene("Level 1");
    }

    public void Exit() {
        Application.Quit();
    }

    public void OnSliderChange() {
        float vol = VolumeSlider.value;

        AudioManager.Instance.AdjustVolume(vol);

        VolumeText.text = Mathf.Round(vol * 100).ToString();
    }
}
