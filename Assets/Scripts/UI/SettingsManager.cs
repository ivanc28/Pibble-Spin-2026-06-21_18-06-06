using UnityEngine;
using UnityEngine.Audio;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Controls")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider pibbleSlider;
    // Called by button
    public static float masterValue = 1;
    public static float musicValue = 1;
    public static float sfxValue = 1;
    public static float pibbleValue = 1;
    private void Start()
    {
        masterSlider.value = masterValue;
        musicSlider.value = musicValue;
        sfxSlider.value = sfxValue;
        pibbleSlider.value = pibbleValue;
    }
    //public void StartGame()
    //{
    //    SceneManager.LoadScene(sceneToLoadOnStart);
    //}
    //// Called by button
    //public void QuitGame()
    //{
    //    Application.Quit();
    //}

    public void SetMasterVolume(float value)
    {
        AudioListener.volume = value;
        masterValue = value;
    }
    public void SetMusicVolume(float value)
    {
        float db = Mathf.Log10(Mathf.Max(0.0001f, value)) * 20;
        audioMixer.SetFloat("MusicVolume", db);
        musicValue = value;
    }
    public void SetSFXVolume(float value)
    {
        float db = Mathf.Log10(Mathf.Max(0.0001f, value)) * 20;
        audioMixer.SetFloat("SoundVolume", db);
        sfxValue = value;
    }
    public void SetPibbleVolume(float value)
    {
        float db = Mathf.Log10(Mathf.Max(0.0001f, value)) * 20;
        audioMixer.SetFloat("PibbleVolume", db);
        pibbleValue = value;
    }
}