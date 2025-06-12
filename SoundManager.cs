using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip buttonPressed;
    [SerializeField] AudioClip correctAnswer;
    [SerializeField] AudioClip wrongAnswer;
    [SerializeField] AudioClip clockSound;

    [SerializeField] private Slider volumeSlider;

    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            volumeSlider.value = savedVolume;
            audioSource.volume = savedVolume;
        }
        else
        {
            volumeSlider.value = 0.75f;
            audioSource.volume = 0.75f;
        }

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }


    private void OnVolumeChanged(float volume)
    {
        audioSource.volume = volume;

        // Save the volume setting
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }



    public void ButtonPressed()
    {
        audioSource.PlayOneShot(buttonPressed);
    }

    public void PlayCorrectSound()
    {
        audioSource.PlayOneShot(correctAnswer);
    }

    public void PlayWrongSound()
    {
        audioSource.PlayOneShot(wrongAnswer);
    }
    public void PlayClockSound()
    {
        audioSource.PlayOneShot(clockSound);
    }
    public void StopSound()
    {
        audioSource.Stop();
    }
}
