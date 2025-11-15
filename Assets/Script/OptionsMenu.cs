// Archivo: OptionsMenu.cs (CORREGIDO - Sin funciones duplicadas)
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [Header("Gestor de Audio")]
    [SerializeField] private AudioMixer mainMixer;
    [Header("UI de Opciones")]
    [SerializeField] private GameObject panelMenuPrincipal;
    [SerializeField] private GameObject panelOpciones;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    
    [Tooltip("El interruptor (Toggle) de Pantalla Completa.")]
    [SerializeField] private Toggle fullscreenToggle;

    private const string MusicVolKey = "MusicVolume";
    private const string SfxVolKey = "SFXVolume";

    void Start()
    {
        // Carga y aplica el audio
        float musicVol = PlayerPrefs.GetFloat(MusicVolKey, 0.75f);
        float sfxVol = PlayerPrefs.GetFloat(SfxVolKey, 0.75f);
        if (musicVol <= 0.0001f) musicVol = 0.0001f;
        if (sfxVol <= 0.0001f) sfxVol = 0.0001f;
        if (musicSlider != null) musicSlider.value = musicVol;
        if (sfxSlider != null) sfxSlider.value = sfxVol;
        SetMusicVolume(musicVol);
        SetSFXVolume(sfxVol);

        // Conecta los listeners
        if (musicSlider != null) musicSlider.onValueChanged.AddListener(SetMusicVolume);
        if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        
        // Sincroniza el Toggle
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }
    }

    public void ShowOptionsPanel() { panelMenuPrincipal.SetActive(false); panelOpciones.SetActive(true); }
    public void ShowMainMenu() { panelMenuPrincipal.SetActive(true); panelOpciones.SetActive(false); }

    /// <summary>
    /// Pone o quita el modo pantalla completa (Solo PC).
    /// </summary>
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Modo Pantalla Completa: " + isFullscreen);
    }

    // --- FUNCIONES DE AUDIO (SIN DUPLICAR) ---
    public void SetMusicVolume(float value)
    {
        if (value <= 0.0001f) value = 0.0001f;
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(MusicVolKey, value);
    }

    public void SetSFXVolume(float value)
    {
        if (value <= 0.0001f) value = 0.0001f;
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(SfxVolKey, value);
    }
}