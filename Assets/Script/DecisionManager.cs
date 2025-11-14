// Archivo: DecisionManager.cs (¡VERSIÓN CORREGIDA!)
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; // <--- ¡Esto ya estaba bien!
using UnityEngine.SceneManagement; 

public class DecisionManager : MonoBehaviour
{
    private enum DecisionState
    {
        ShowingDilemma,
        ShowingOptions,
        ShowingConsequence,
        ShowingFicha 
    }

    [Header("Gestor de Flujo")]
    [SerializeField] private GameFlowManager gameFlowManager;
    
    [Header("UI Dilema/Consecuencia")]
    [SerializeField] private GameObject scrollTextoNarrativo;
    [SerializeField] private Image decisionBackgroundImage; 
    
    // --- ¡AQUÍ ESTÁ LA CORRECCIÓN! ---
    [SerializeField] private TextMeshProUGUI decisionPromptText; // (Era TextMeshProUI)
    // ---------------------------------
    
    [SerializeField] private GameObject panelOpciones; 
    [SerializeField] private Button botonSiguiente; // El botón "Siguiente" principal
    [SerializeField] private List<OptionButton> optionButtons; 

    [Header("UI Ficha Desbloqueada")]
    [Tooltip("El Panel/Canvas 'padre' que contiene tu prefab de ficha (ej: Panel_FichaGanada).")]
    [SerializeField] private GameObject panelFichaGanada;
    [Tooltip("El script 'JournalEntryUI' que está en el prefab que pusiste en la escena.")]
    [SerializeField] private JournalEntryUI entryUIFichaGanada; // <--- ¡LA CLAVE!
    [Tooltip("El botón 'Continuar' que está en el panelFichaGanada.")]
    [SerializeField] private Button botonContinuarFicha;

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

        // Configurar los listeners
        if (botonSiguiente != null)
        {
            botonSiguiente.onClick.RemoveAllListeners();
            botonSiguiente.onClick.AddListener(OnBotonSiguienteClicked); 
        }
        if (botonContinuarFicha != null)
        {
            botonContinuarFicha.onClick.RemoveAllListeners();
            botonContinuarFicha.onClick.AddListener(OnBotonContinuarFichaClicked);
        }
        
        // Ocultar el panel de ficha al empezar
        if(panelFichaGanada != null)
        {
            panelFichaGanada.SetActive(false);
        }
        
        currentDecision = gameFlowManager.StartingDecision;
        ShowDecision(currentDecision);
    }

    /// <summary>
    /// ESTADO 1: Muestra el DILEMA
    /// </summary>
    public void ShowDecision(Decision decision)
    {
        if (decision == null) return;
        currentDecision = decision;

        scrollTextoNarrativo.SetActive(true); 
        decisionPromptText.text = currentDecision.DecisionPrompt;
        if (decisionBackgroundImage != null && currentDecision.BackgroundImage != null)
        {
            decisionBackgroundImage.sprite = currentDecision.BackgroundImage;
            decisionBackgroundImage.gameObject.SetActive(true);
        }

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
        
        panelOpciones.SetActive(false);
        if(panelFichaGanada != null)
        {
            panelFichaGanada.SetActive(false); 
        }
        
        botonSiguiente.gameObject.SetActive(true);
        currentState = DecisionState.ShowingDilemma;
    }

    /// <summary>
    /// ESTADO 2: Muestra las OPCIONES
    /// </summary>
    public void ShowOptions()
    {
        scrollTextoNarrativo.SetActive(false); 
        panelOpciones.SetActive(true);
        botonSiguiente.gameObject.SetActive(false);
        currentState = DecisionState.ShowingOptions;
    }

    /// <summary>
    /// ESTADO 3: Muestra la CONSECUENCIA
    /// </summary>
    public void OnOptionSelected(DecisionOption chosenOption)
    {
        lastChosenOption = chosenOption; 
        panelOpciones.SetActive(false);
        scrollTextoNarrativo.SetActive(true); 
        decisionPromptText.text = chosenOption.ConsequenceText;
        if (decisionBackgroundImage != null && chosenOption.ConsequenceImage != null)
        {
            decisionBackgroundImage.sprite = chosenOption.ConsequenceImage;
            decisionBackgroundImage.gameObject.SetActive(true);
        }
        botonSiguiente.gameObject.SetActive(true);
        currentState = DecisionState.ShowingConsequence;
    }
    
    /// <summary>
    /// ESTADO 4: Muestra la FICHA GANADA
    /// </summary>
    public void ShowFicha()
    {
        KnowledgeFicha ficha = lastChosenOption.FichaToGrant;

        if (ficha == null)
        {
            ShowNextDilemmaOrEnd();
            return;
        }

        // Ocultar la UI del dilema/consecuencia
        scrollTextoNarrativo.SetActive(false);
        botonSiguiente.gameObject.SetActive(false);

        // --- ¡Lógica de Prefab Pre-colocado! ---
        if (entryUIFichaGanada != null)
        {
            // ¡Simplemente le pasamos los datos!
            entryUIFichaGanada.Setup(ficha);
        }
        else
        {
            Debug.LogError("¡No hay referencia a 'entryUIFichaGanada' en el DecisionManager!");
        }
        // -------------------------------------

        // Mostrar el panel "contenedor"
        panelFichaGanada.SetActive(true);
        currentState = DecisionState.ShowingFicha;
    }

    /// <summary>
    /// Maneja el clic en el botón "Siguiente" principal
    /// </summary>
    public void OnBotonSiguienteClicked()
    {
        if (currentState == DecisionState.ShowingDilemma)
        {
            ShowOptions();
        }
        else if (currentState == DecisionState.ShowingConsequence)
        {
            ShowFicha();
        }
    }
    
    /// <summary>
    /// Maneja el clic en el botón "Continuar" DEL PANEL DE FICHA
    /// </summary>
    public void OnBotonContinuarFichaClicked()
    {
        if (currentState != DecisionState.ShowingFicha) return;
        
        // Ocultar el panel
        panelFichaGanada.SetActive(false);
        
        // ¡Ya no hay que destruir nada!
        ShowNextDilemmaOrEnd();
    }

    /// <summary>
    /// Decide si cargar el próximo dilema o volver al menú
    /// </summary>
    private void ShowNextDilemmaOrEnd()
    {
        Decision next = currentDecision.NextDecision;
        
        if (next != null)
        {
            ShowDecision(next); // Carga el siguiente dilema
        }
        else
        {
            Debug.Log("Fin de la cadena de decisiones. Volviendo al menú.");
            GoToMenu();
        }
    }
    
    private void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}