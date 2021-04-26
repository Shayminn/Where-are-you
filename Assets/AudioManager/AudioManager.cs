using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource BackgroundAudio = null;
    [SerializeField] AudioSource SFXAudio = null;

    [SerializeField] List<AudioClip> BackgroundAudioClips = null;
    List<AudioClip> TempBGAudioClips;
    [SerializeField] List<AudioClip> SFXAudioClips = null;

    public float DelayBetweenEachBGClip = 15f;

    public static AudioManager Instance;
    public static bool FirstRun = true;


    public enum SFX {
        Death,
        Heart
    }

    // Start is called before the first frame update
    void Start()
    {
        if (FirstRun) {
            FirstRun = false;
            ResetList();

            Instance = this;

            DontDestroyOnLoad(gameObject);

            StartCoroutine(PlayBackground());
        }
    }

    public IEnumerator PlayBackground() {

        int random = Random.Range(0, TempBGAudioClips.Count);
        BackgroundAudio.clip = TempBGAudioClips[random];
        BackgroundAudio.Play();

        TempBGAudioClips.RemoveAt(random);
        if (TempBGAudioClips.Count == 0) {
            ResetList();
        }

        while (BackgroundAudio.isPlaying) {
            yield return null;
        }

        yield return new WaitForSeconds(DelayBetweenEachBGClip);

        StartCoroutine(PlayBackground());
    }

    public void ResetList() {
        TempBGAudioClips = new List<AudioClip>(BackgroundAudioClips);
    }

    public void PlaySFX(SFX sfx) {
        SFXAudio.clip = SFXAudioClips.FirstOrDefault(clip => clip.name.Equals(sfx.ToString()));

        SFXAudio.Play();
    }

    public void AdjustVolume(float vol) {
        BackgroundAudio.volume = vol;
        SFXAudio.volume = vol;
    }

    public float GetVolume() {
        return BackgroundAudio.volume;
    }
}
