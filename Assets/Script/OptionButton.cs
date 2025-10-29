// Archivo: OptionButton.cs
using UnityEngine;
using UnityEngine.UI; // Para Button
using TMPro; // Para TextMeshProUGUI

/// <summary>
/// Script para un botón de opción de la UI.
/// Almacena su opción y la reporta al ser pulsado.
/// </summary>
[RequireComponent(typeof(Button))]
public class OptionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI optionText;
    
    private DecisionOption currentOption;
    private DecisionManager decisionManager; // Referencia al gestor
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        
        // Nos aseguramos de que el texto esté asignado
        if (optionText == null)
        {
            optionText = GetComponentInChildren<TextMeshProUGUI>();
        }
        
        // Añadimos el listener para el clic
        button.onClick.AddListener(OnButtonClick);
    }

    /// <summary>
    /// Configura este botón con los datos de una DecisionOption.
    /// </summary>
    /// <param name="manager">El DecisionManager que controla este botón.</param>
    /// <param name="option">Los datos de la opción a mostrar.</param>
    public void Setup(DecisionManager manager, DecisionOption option)
    {
        decisionManager = manager;
        currentOption = option;
        
        if (optionText != null)
        {
            optionText.text = currentOption.OptionText;
        }
        else
        {
            Debug.LogWarning("No hay referencia a 'optionText' en el botón.");
        }
    }

    /// <summary>
    /// Se llama cuando el jugador hace clic en este botón.
    /// </summary>
    private void OnButtonClick()
    {
        if (currentOption == null)
        {
            Debug.LogError("OptionButton fue pulsado pero no tiene 'currentOption' asignada.");
            return;
        }

        // 1. Otorga la ficha de conocimiento al jugador
        if (PerspectiveJournal.Instance != null)
        {
            PerspectiveJournal.Instance.AddFicha(currentOption.FichaToGrant);
        }
        else
        {
            Debug.LogError("No se encuentra una instancia de 'PerspectiveJournal' en la escena.");
        }


        // 2. Notifica al DecisionManager que se ha tomado una decisión
        if (decisionManager != null)
        {
            decisionManager.OnOptionSelected(currentOption);
        }
        else
        {
            Debug.LogError("No hay referencia a 'decisionManager' en el botón.");
        }
    }
}