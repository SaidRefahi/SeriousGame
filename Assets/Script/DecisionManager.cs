// Archivo: DecisionManager.cs
using UnityEngine;
using UnityEngine.UI; // ¡Importante para Button!
using System.Collections.Generic;
using TMPro; 

/// <summary>
/// Gestiona la presentación de una decisión en la UI y el flujo a la siguiente.
/// </summary>
public class DecisionManager : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("El campo de texto para mostrar la pregunta o dilema.")]
    [SerializeField] private TextMeshProUGUI decisionPromptText;
    
    [Tooltip("La lista de los 4 botones de opción de la UI.")]
    [SerializeField] private List<OptionButton> optionButtons; 

    [Header("Decision Data")]
    [Tooltip("Asigna aquí la PRIMERA decisión de la cadena.")]
    [SerializeField] private Decision currentDecision;

    void Start()
    {
        if (currentDecision != null)
        {
            ShowDecision(currentDecision);
        }
        else
        {
            Debug.LogError("No hay ninguna 'Decision' inicial asignada en el DecisionManager.");
            gameObject.SetActive(false); // Ocultar si no hay nada que mostrar
        }
    }

    /// <summary>
    /// Configura y muestra una decisión en la pantalla.
    /// </summary>
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
            if (i < currentDecision.Options.Count && currentDecision.Options[i] != null)
            {
                // Configura el botón con los datos
                optionButtons[i].Setup(this, currentDecision.Options[i]);
                optionButtons[i].gameObject.SetActive(true);
                
                // ¡IMPORTANTE: Reactiva el botón!
                optionButtons[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                // Desactiva botones extra
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

        // 1. Desactivar los botones para que no se pueda volver a pulsar
        foreach (var button in optionButtons)
        {
            button.GetComponent<Button>().interactable = false;
        }

        // 2. Comprobar si hay una siguiente decisión en la cadena
        if (currentDecision.NextDecision != null)
        {
            // Cargar la siguiente decisión
            // Opcional: Añadir un retraso con una Corrutina aquí
            ShowDecision(currentDecision.NextDecision);
        }
        else
        {
            // Es el final de la simulación
            Debug.Log("Fin de la cadena de decisiones.");
            // Aquí puedes mostrar un panel de "Fin" o volver al menú
            // Ejemplo: Invoke(nameof(GoToMenu), 2f); 
        }
    }

    // private void GoToMenu()
    // {
    //     UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    // }
}