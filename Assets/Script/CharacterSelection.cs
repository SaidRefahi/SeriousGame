// Archivo: CharacterSelection.cs
using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar la escena

/// <summary>
/// Gestiona la UI de selección de personaje en el Menú.
/// Se encarga de mostrar/ocultar paneles y de asignar la decisión
/// inicial al GameFlowManager.
/// </summary>
public class CharacterSelection : MonoBehaviour
{
    [Header("Gestor de Flujo")]
    [Tooltip("Arrastra aquí el asset 'GameFlowManager' de tu proyecto.")]
    [SerializeField] private GameFlowManager gameFlowManager;

    [Header("Paneles de UI")]
    [Tooltip("El panel del menú principal (con Jugar, Diario, Salir).")]
    [SerializeField] private GameObject panelMenuPrincipal;
    
    [Tooltip("El panel de selección de personaje (con Valentina, Antonella, Mateo).")]
    [SerializeField] private GameObject panelSeleccionPersonaje;

    [Header("Decisiones Iniciales")]
    [Tooltip("La primera decisión de la historia de Valentina (ej: Decision_V1_Filtro).")]
    [SerializeField] private Decision valentinaStartDecision;
    
    [Tooltip("La primera decisión de la historia de Antonella (ej: Decision_A1_Generacional).")]
    [SerializeField] private Decision antonellaStartDecision;
    
    [Tooltip("La primera decisión de la historia de Mateo (ej: Decision_M1_Monotributo).")]
    [SerializeField] private Decision mateoStartDecision;


    /// <summary>
    /// Se llama al inicio. Asegura que solo el panel principal esté visible.
    /// </summary>
    private void Start()
    {
        ShowMainMenu();
    }

    /// <summary>
    /// Muestra el panel de selección de personaje y oculta el menú principal.
    /// Se conecta al botón "Comenzar mi Viaje" o "Jugar".
    /// </summary>
    public void ShowSelectionPanel()
    {
        panelMenuPrincipal.SetActive(false);
        panelSeleccionPersonaje.SetActive(true);
    }

    /// <summary>
    /// Muestra el menú principal y oculta la selección de personaje.
    /// Se conecta al botón "Volver" del panel de selección.
    /// </summary>
    public void ShowMainMenu()
    {
        panelMenuPrincipal.SetActive(true);
        panelSeleccionPersonaje.SetActive(false);
    }

    /// <summary>
    /// Asigna la decisión inicial de Valentina al gestor de flujo
    /// y carga la escena de juego.
    /// </summary>
    public void SelectValentina()
    {
        if (gameFlowManager == null || valentinaStartDecision == null) return;
        
        gameFlowManager.StartingDecision = valentinaStartDecision;
        LoadGameScene();
    }

    /// <summary>
    /// Asigna la decisión inicial de Antonella al gestor de flujo
    /// y carga la escena de juego.
    /// </summary>
    public void SelectAntonella()
    {
        if (gameFlowManager == null || antonellaStartDecision == null) return;
        
        gameFlowManager.StartingDecision = antonellaStartDecision;
        LoadGameScene();
    }
    
    /// <summary>
    /// Asigna la decisión inicial de Mateo al gestor de flujo
    /// y carga la escena de juego.
    /// </summary>
    public void SelectMateo()
    {
        if (gameFlowManager == null || mateoStartDecision == null) return;
        
        gameFlowManager.StartingDecision = mateoStartDecision;
        LoadGameScene();
    }

    /// <summary>
    /// Carga la escena principal del simulador (Escena1).
    /// </summary>
    private void LoadGameScene()
    {
        // Asegúrate de que tu escena de juego se llama "Escena1"
        SceneManager.LoadScene("Escena1");
    }
}