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
        
        if (optionText == null)
        {
            optionText = GetComponentInChildren<TextMeshProUGUI>();
        }
        
        button.onClick.AddListener(OnButtonClick);
    }

    /// <summary>
    /// Configura este botón con los datos de una DecisionOption.
    /// </summary>
    public void Setup(DecisionManager manager, DecisionOption option)
    {
        decisionManager = manager;
        currentOption = option;
        
        if (optionText != null)
        {
            optionText.text = currentOption.OptionText;
        }
    }

    /// <summary>
    /// Se llama cuando el jugador hace clic en este botón.
    /// </summary>
    private void OnButtonClick()
    {
        if (currentOption == null) return;
        
        // --- ¡LÍNEA NUEVA! ---
        // Reproduce el sonido de clic ANTES de hacer nada más.
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayClickSound();
        }
        // ---------------------

        // 1. Otorga la ficha de conocimiento al jugador
        if (PerspectiveJournal.Instance != null && currentOption.FichaToGrant != null)
        {
            PerspectiveJournal.Instance.AddFicha(currentOption.FichaToGrant);
        }

        // 2. Notifica al DecisionManager que se ha tomado una decisión
        if (decisionManager != null)
        {
            decisionManager.OnOptionSelected(currentOption);
        }
    }
}