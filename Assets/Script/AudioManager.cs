// Archivo: AudioManager.cs
using UnityEngine;

/// <summary>
/// Gestiona toda la música de fondo (BGM) y los efectos de sonido (SFX).
/// Es un Singleton que persiste entre escenas.
/// </C#_CODE>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Componentes")]
    [Tooltip("El AudioSource para la música (debe estar en Loop).")]
    [SerializeField] private AudioSource musicSource;

    [Tooltip("El AudioSource para los sonidos (no debe estar en Loop).")]
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips de Audio")]
    [Tooltip("La música de fondo principal del juego.")]
    [SerializeField] private AudioClip backgroundMusic;

    [Tooltip("El sonido que se reproduce al hacer clic en un botón.")]
    [SerializeField] private AudioClip buttonClickSound;

    private void Awake()
    {
        // Implementación de Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destruye duplicados
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ¡Persiste entre escenas!
        }
    }

    private void Start()
    {
        // Inicia la música de fondo
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true; // Asegurarse de que esté en loop
            musicSource.Play();
        }
    }

    /// <summary>
    /// Reproduce el sonido de clic de botón.
    /// Se llama desde los scripts de los botones.
    /// </summary>
    public void PlayClickSound()
    {
        if (sfxSource != null && buttonClickSound != null)
        {
            // PlayOneShot permite reproducir sonidos sin interrumpir otros
            sfxSource.PlayOneShot(buttonClickSound);
        }
    }
}