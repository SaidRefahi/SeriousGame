// Archivo: DecisionManager.cs (¡MUY ACTUALIZADO!)
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; 
using UnityEngine.SceneManagement; // Para volver al menú

/// <summary>
/// Gestiona la presentación de una decisión, sus opciones Y sus consecuencias.
/// Controla el flujo completo de una pantalla de decisión.
/// </summary>
public class DecisionManager : MonoBehaviour
{
    // Estados para saber qué está haciendo el botón "Siguiente"
    private enum DecisionState
    {
        ShowingDilemma,     // Mostrando el dilema, "Siguiente" mostrará opciones
        ShowingOptions,     // Mostrando opciones, "Siguiente" está oculto
        ShowingConsequence  // Mostrando la consecuencia, "Siguiente" pasará al próximo dilema
    }

    [Header("Gestor de Flujo")]
    [SerializeField] private GameFlowManager gameFlowManager;
    
    [Header("UI References")]
    [SerializeField] private Image decisionBackgroundImage; 
    [SerializeField] private TextMeshProUGUI decisionPromptText; 
    [SerializeField] private GameObject panelOpciones; 
    [SerializeField] private Button botonSiguiente; 
    [SerializeField] private List<OptionButton> optionButtons; 

    private Decision currentDecision;
    private DecisionState currentState;
    private DecisionOption lastChosenOption; // Para recordar qué opción se eligió

    void Start()
    {
        if (gameFlowManager == null || gameFlowManager.StartingDecision == null)
        {
            Debug.LogError("¡No se encontró GameFlowManager o no hay StartingDecision asignada!");
            return;
        }

        // --- ¡CAMBIO IMPORTANTE EN EL BOTÓN SIGUIENTE! ---
        // Borramos cualquier listener viejo y asignamos uno nuevo y permanente.
        if (botonSiguiente != null)
        {
            botonSiguiente.onClick.RemoveAllListeners(); // Limpiamos por si acaso
            botonSiguiente.onClick.AddListener(OnBotonSiguienteClicked); // <--- NUEVA FUNCIÓN
        }
        
        currentDecision = gameFlowManager.StartingDecision;
        ShowDecision(currentDecision);
    }

    /// <summary>
    /// Configura la UI para mostrar el DILEMA (Texto + Fondo del Dilema).
    /// </summary>
    public void ShowDecision(Decision decision)
    {
        if (decision == null)
        {
            Debug.LogError("Se intentó mostrar una decisión nula.");
            return;
        }

        currentDecision = decision;

        // 1. Mostrar Texto e Imagen del DILEMA
        decisionPromptText.text = currentDecision.DecisionPrompt;
        if (decisionBackgroundImage != null && currentDecision.BackgroundImage != null)
        {
            decisionBackgroundImage.sprite = currentDecision.BackgroundImage;
            decisionBackgroundImage.gameObject.SetActive(true);
        }

        // 2. Configurar los botones de opción (se quedan ocultos)
        for (int i = 0; i < optionButtons.Count; i++)
        {
            if (i < currentDecision.Options.Count && currentDecision.Options[i] != null)
            {
                optionButtons[i].Setup(this, currentDecision.Options[i]);
                optionButtons[i].gameObject.SetActive(true); 
                optionButtons[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }

        // 3. Poner la UI en estado "Dilema"
        panelOpciones.SetActive(false);
        botonSiguiente.gameObject.SetActive(true);
        currentState = DecisionState.ShowingDilemma;
    }

    /// <summary>
    /// Muestra las opciones. Se llama desde OnBotonSiguienteClicked.
    /// </summary>
    public void ShowOptions()
    {
        panelOpciones.SetActive(true);
        botonSiguiente.gameObject.SetActive(false);
        currentState = DecisionState.ShowingOptions;
    }

    /// <summary>
    /// Se llama desde OptionButton cuando el jugador elige una opción.
    /// AHORA MUESTRA LA CONSECUENCIA.
    /// </summary>
    public void OnOptionSelected(DecisionOption chosenOption)
    {
        lastChosenOption = chosenOption; // Guardamos la opción elegida

        // 1. Ocultar panel de opciones
        panelOpciones.SetActive(false);

        // 2. Mostrar Texto e Imagen de la CONSECUENCIA
        decisionPromptText.text = chosenOption.ConsequenceText;
        if (decisionBackgroundImage != null && chosenOption.ConsequenceImage != null)
        {
            decisionBackgroundImage.sprite = chosenOption.ConsequenceImage;
            decisionBackgroundImage.gameObject.SetActive(true);
        }

        // 3. Mostrar el botón "Siguiente" y cambiar de estado
        botonSiguiente.gameObject.SetActive(true);
        currentState = DecisionState.ShowingConsequence;
    }

    /// <summary>
    /// Esta ÚNICA función maneja el botón "Siguiente".
    /// Decide qué hacer basándose en el estado actual.
    /// </summary>
    public void OnBotonSiguienteClicked()
    {
        if (currentState == DecisionState.ShowingDilemma)
        {
            // Estábamos viendo el dilema, ahora mostramos las opciones.
            ShowOptions();
        }
        else if (currentState == DecisionState.ShowingConsequence)
        {
            // Estábamos viendo la consecuencia, ahora pasamos al siguiente dilema.
            Decision next = currentDecision.NextDecision;
            
            if (next != null)
            {
                // Cargar el siguiente dilema
                ShowDecision(next);
            }
            else
            {
                // Fin de la historia, volver al menú
                Debug.Log("Fin de la cadena de decisiones. Volviendo al menú.");
                GoToMenu();
            }
        }
    }

    /// <summary>
    /// Vuelve al menú principal.
    /// </summary>
    private void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}