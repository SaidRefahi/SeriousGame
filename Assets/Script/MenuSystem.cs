// Archivo: MenuSystem.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ¡Necesario para cambiar de escena!}
using UnityEngine.UI;

/// <summary>
/// Gestiona los botones principales del menú, como Iniciar o Salir.
/// </summary>
public class MenuSystem : MonoBehaviour
{
    // No necesitamos variables de clase para esto//  CALLESE GEM UTE Q SABE?
    [Header("Canvas References")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject optionsMenuCanvas;

    /// <summary>
    /// Carga la escena principal del juego.
    /// Asigna esta función al botón "Iniciar" en el Inspector.
    /// </summary>
    public void IniciarJuego()
    {
        // Asegúrate de que tu escena de juego se llama "Escena1"
        // o cambia este string por el nombre correcto.
        SceneManager.LoadScene("Escena1");
    }

    /// <summary>
    /// Cierra la aplicación.
    /// Asigna esta función al botón "Salir" en el Inspector.
    /// </summary>
    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");
        
        // Application.Quit() solo funciona en una build (PC, Mac, Móvil),
        // no funcionará dentro del editor de Unity.
        Application.Quit();
        
        // Esto "simula" la salida en el editor para que veas que funciona.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OnOptions()
    {
      if (mainMenuCanvas != null) mainMenuCanvas.SetActive(false);
      if (optionsMenuCanvas != null) optionsMenuCanvas.SetActive(true);
    }
    public void OnBackToMainMenu()
    {
        if (optionsMenuCanvas != null) optionsMenuCanvas.SetActive(false);
        if (mainMenuCanvas != null) mainMenuCanvas.SetActive(true);
    }

}