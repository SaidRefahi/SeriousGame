// Archivo: DecisionManager.cs (ACTUALIZADO)
using UnityEngine;
using UnityEngine.UI; // ¡Importante para Button!
using System.Collections.Generic;
using TMPro; 

/// <summary>
/// Gestiona la presentación de una decisión en la UI y el flujo a la siguiente.
/// FLUJO: Muestra dilema -> Espera "Siguiente" -> Muestra opciones.
/// </summary>
public class DecisionManager : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("El campo de texto para mostrar la pregunta o dilema.")]
    [SerializeField] private TextMeshProUGUI decisionPromptText;
    
    [Tooltip("El GameObject que CONTIENE los 4 botones de opción.")]
    [SerializeField] private GameObject panelOpciones; // <--- ¡NUEVO!

    [Tooltip("El botón para pasar del dilema a las opciones.")]
    [SerializeField] private Button botonSiguiente; // <--- ¡NUEVO!

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
            gameObject.SetActive(false); 
        }

        // --- Configurar el listener para el botón "Siguiente" ---
        if (botonSiguiente != null)
        {
            botonSiguiente.onClick.AddListener(ShowOptions);
        }
    }

    /// <summary>
    /// Configura y muestra el DILEMA. Oculta las opciones.
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

        // 2. Configura los botones (aunque estén ocultos)
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

        // 3. --- ¡AQUÍ ESTÁ LA NUEVA LÓGICA! ---
        // Oculta el panel de opciones y muestra el botón "Siguiente"
        panelOpciones.SetActive(false);
        botonSiguiente.gameObject.SetActive(true);
    }

    /// <summary>
    /// Se llama cuando el jugador pulsa "Siguiente".
    /// Muestra las opciones y oculta el botón "Siguiente".
    /// </summary>
    public void ShowOptions()
    {
        panelOpciones.SetActive(true);
        botonSiguiente.gameObject.SetActive(false);
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
            // ShowDecision se encargará de ocultar las opciones y mostrar "Siguiente"
            ShowDecision(currentDecision.NextDecision);
        }
        else
        {
            // Es el final de la simulación
            Debug.Log("Fin de la cadena de decisiones.");
            // Ocultamos el panel de opciones por si acaso
            panelOpciones.SetActive(false);
            // Aquí puedes mostrar un panel de "Fin del Juego" o volver al menú
            // Invoke(nameof(GoToMenu), 2f); 
        }
    }

    // private void GoToMenu()
    // {
    //     UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    // }
}