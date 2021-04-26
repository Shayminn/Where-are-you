using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour {

    [SerializeField] Slider VolumeSlider = null;
    [SerializeField] Text VolumeText = null;

    public string SceneToChange = "Introduction";

    void Start() {
        VolumeSlider.value = AudioManager.Instance.GetVolume();
    }

    public void OnSliderChange() {
        float vol = VolumeSlider.value;

        AudioManager.Instance.AdjustVolume(vol);

        VolumeText.text = Mathf.Round(vol * 100).ToString();
    }

    public void Play() {
        SceneChanger.Instance.ChangeScene(SceneToChange);
    }

    public void HandleExitClick() {
        Application.Quit();
    }
}
