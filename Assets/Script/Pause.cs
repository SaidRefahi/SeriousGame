// Archivo: Pause.cs
using UnityEngine;
using UnityEngine.SceneManagement; // Para volver al menú

/// <summary>
/// Gestiona el menú de pausa, el tiempo del juego y la vuelta al menú.
/// </summary>
public class Pause : MonoBehaviour
{
    [Tooltip("El panel de UI que se muestra al pausar.")]
    [SerializeField] private GameObject pausePanel;

    private bool isPaused = false;

    // Se recomienda llamar a la pausa con el nuevo Input System,
    // pero para KISS, usaremos Update() con el input antiguo.
    void Update()
    {
        // Comprueba si se pulsó la tecla "Escape"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
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