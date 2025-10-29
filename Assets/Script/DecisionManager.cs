// Archivo: DecisionManager.cs
using UnityEngine;
using System.Collections.Generic;
using TMPro; // Para TextMeshProUGUI
using UnityEngine.UI; // <--- ¡AÑADE ESTA LÍNEA!

/// <summary>
/// Gestiona la presentación de una decisión en la UI.
/// Carga una 'Decision' (ScriptableObject) y configura la UI.
/// </summary>
public class DecisionManager : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("El campo de texto para mostrar la pregunta o dilema.")]
    [SerializeField] private TextMeshProUGUI decisionPromptText;
    
    [Tooltip("La lista de los 4 botones de opción de la UI.")]
    [SerializeField] private List<OptionButton> optionButtons; 

    [Header("Decision Data")]
    [Tooltip("Asigna aquí el asset de la Decisión que quieres mostrar.")]
    [SerializeField] private Decision currentDecision;

    void Start()
    {
        if (currentDecision != null)
        {
            ShowDecision(currentDecision);
        }
        else
        {
            Debug.LogError("No hay ninguna 'Decision' asignada en el DecisionManager.");
            // Opcional: Ocultar la UI si no hay decisión
            // gameObject.SetActive(false); 
        }
    }

    /// <summary>
    /// Configura y muestra una decisión en la pantalla.
    /// </summary>
    /// <param name="decision">El asset de Decisión a mostrar.</param>
    public void ShowDecision(Decision decision)
    {
        if (decision == null)
        {
            Debug.LogError("Se intentó mostrar una decisión nula.");
            return;
        }

        currentDecision = decision;

        // 1. Muestra el texto de la pregunta
        decisionPromptText.text = currentDecision.DecisionPrompt;

        // 2. Configura los botones
        for (int i = 0; i < optionButtons.Count; i++)
        {
            // Comprueba si tenemos suficientes opciones en el asset
            if (i < currentDecision.Options.Count && currentDecision.Options[i] != null)
            {
                // Asigna la opción al botón
                optionButtons[i].Setup(this, currentDecision.Options[i]);
                optionButtons[i].gameObject.SetActive(true);
                // Asegurarse de que el botón esté interactuable al mostrar una nueva decisión
                optionButtons[i].GetComponent<Button>().interactable = true; 
            }
            else
            {
                // Desactiva botones extra si la decisión tiene menos de 4 opciones
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Se llama desde OptionButton cuando el jugador elige una opción.
    /// </summary>
    public void OnOptionSelected(DecisionOption selectedOption)
    {
        Debug.Log($"Opción seleccionada: {selectedOption.OptionText}");

        // --- Lógica para después de la decisión ---
        
        // 1. Desactivar los botones para que no se pueda volver a pulsar
        foreach (var button in optionButtons)
        {
            // Esta es la línea que causaba el error. Ahora funcionará.
            button.GetComponent<Button>().interactable = false;
        }

        // 2. Esperar unos segundos y/o mostrar feedback
        // StartCoroutine(LoadNextDecision());

        // 3. Cargar la siguiente decisión (si la tienes asignada en el SO 'Decision')
        // if (currentDecision.NextDecision != null)
        // {
        //     // Reactivar botones antes de mostrar la siguiente decisión
        //     foreach (var button in optionButtons)
        //     {
        //         button.GetComponent<Button>().interactable = true;
        //     }
        //     ShowDecision(currentDecision.NextDecision);
        // }
        // else
        // {
        //     Debug.Log("Fin de la cadena de decisiones.");
        //     gameObject.SetActive(false); // Ocultar la UI de decisión
        // }
    }
}