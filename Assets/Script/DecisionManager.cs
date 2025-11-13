// Archivo: DecisionManager.cs (¡ACTUALIZADO OTRA VEZ!)
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; 
using UnityEngine.SceneManagement; // Para volver al menú

/// <summary>
/// Gestiona la presentación de una decisión, sus opciones Y sus consecuencias.
/// Oculta el texto narrativo cuando se muestran las opciones.
/// </summary>
public class DecisionManager : MonoBehaviour
{
    private enum DecisionState
    {
        ShowingDilemma,
        ShowingOptions,
        ShowingConsequence
    }

    [Header("Gestor de Flujo")]
    [SerializeField] private GameFlowManager gameFlowManager;
    
    [Header("UI References")]
    [Tooltip("El objeto 'Scroll View' principal que contiene el texto narrativo.")]
    [SerializeField] private GameObject scrollTextoNarrativo; // <--- ¡NUEVO!

    [SerializeField] private Image decisionBackgroundImage; 
    [SerializeField] private TextMeshProUGUI decisionPromptText; // (Este sigue siendo el 'Content' del scroll view)
    [SerializeField] private GameObject panelOpciones; 
    [SerializeField] private Button botonSiguiente; 
    [SerializeField] private List<OptionButton> optionButtons; 

    private Decision currentDecision;
    private DecisionState currentState;
    private DecisionOption lastChosenOption; 

    void Start()
    {
        if (gameFlowManager == null || gameFlowManager.StartingDecision == null)
        {
            Debug.LogError("¡No se encontró GameFlowManager o no hay StartingDecision asignada!");
            return;
        }

        if (botonSiguiente != null)
        {
            botonSiguiente.onClick.RemoveAllListeners();
            botonSiguiente.onClick.AddListener(OnBotonSiguienteClicked); 
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
        if (scrollTextoNarrativo != null)
        {
            scrollTextoNarrativo.SetActive(true); // <--- ¡MUESTRA EL TEXTO!
        }
        panelOpciones.SetActive(false);
        botonSiguiente.gameObject.SetActive(true);
        currentState = DecisionState.ShowingDilemma;
    }

    /// <summary>
    /// Muestra las opciones. Se llama desde OnBotonSiguienteClicked.
    /// </summary>
    public void ShowOptions()
    {
        if (scrollTextoNarrativo != null)
        {
            scrollTextoNarrativo.SetActive(false); // <--- ¡OCULTA EL TEXTO!
        }
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
        lastChosenOption = chosenOption; 

        // 1. Ocultar panel de opciones
        panelOpciones.SetActive(false);

        // 2. Mostrar Texto e Imagen de la CONSECUENCIA
        decisionPromptText.text = chosenOption.ConsequenceText;
        if (decisionBackgroundImage != null && chosenOption.ConsequenceImage != null)
        {
            decisionBackgroundImage.sprite = chosenOption.ConsequenceImage;
            decisionBackgroundImage.gameObject.SetActive(true);
        }

        // 3. Mostrar el botón "Siguiente", el texto, y cambiar de estado
        if (scrollTextoNarrativo != null)
        {
            scrollTextoNarrativo.SetActive(true); // <--- ¡MUESTRA EL TEXTO DE NUEVO!
        }
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
                ShowDecision(next);
            }
            else
            {
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