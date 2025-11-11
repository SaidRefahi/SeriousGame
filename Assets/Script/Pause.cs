// Archivo: Pause.cs (ACTUALIZADO para Nuevo Input System)
using UnityEngine;
using UnityEngine.SceneManagement;
// ¡Ya no se necesita 'using UnityEngine.InputSystem;' para este método!

/// <summary>
/// Gestiona el menú de pausa.
/// Este script recibe mensajes del componente 'PlayerInput'.
/// </summary>
public class Pause : MonoBehaviour
{
    [Tooltip("El panel de UI que se muestra al pausar.")]
    [SerializeField] private GameObject pausePanel;

    private bool isPaused = false;

    // --- ¡EL MÉTODO UPDATE() SE HA ELIMINADO! ---
    // Ya no necesitamos 'escuchar' la tecla Escape aquí.
    // El componente PlayerInput lo hace por nosotros.


    /// <summary>
    /// Esta función es llamada automáticamente por el 'PlayerInput' (Behavior: Send Messages)
    /// cuando la acción "Pause" (que bindeamos a Escape) es disparada.
    /// El nombre debe coincidir: "On" + "Pause" (el nombre de tu Acción).
    /// </summary>
    public void OnPause()
    {
        // Invertimos el estado de pausa
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    /// <summary>
    /// Pausa el juego y muestra el panel.
    /// </summary>
    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // ¡Congela el tiempo del juego!
    }

    /// <summary>
    /// Reanuda el juego y oculta el panel.
    /// </summary>
    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo normal
    }

    /// <summary>
    /// Vuelve a la escena del Menú Principal.
    /// </summary>
    public void LoadMenu()
    {
        // ¡MUY IMPORTANTE! Resetea el tiempo antes de salir de la escena.
        Time.timeScale = 1f;
        
        // Asegúrate de que tu escena de menú se llama "Menu"
        SceneManager.LoadScene("Menu");
    }
}