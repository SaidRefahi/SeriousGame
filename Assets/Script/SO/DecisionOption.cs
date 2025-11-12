// Archivo: DecisionOption.cs (¡ACTUALIZADO!)
using UnityEngine;

/// <summary>
/// ScriptableObject que representa una RAMA NARRATIVA COMPLETA.
/// Contiene el texto del botón, la consecuencia (texto e imagen) y la ficha que otorga.
/// </summary>
[CreateAssetMenu(fileName = "Opcion_", menuName = "Juego de Decisiones/Opción de Decisión")]
public class DecisionOption : ScriptableObject
{
    [Header("1. Configuración del Botón")]
    [Tooltip("El texto que se mostrará en el botón de opción.")]
    [TextArea(2, 4)]
    public string OptionText;

    // --- ¡CAMPOS NUEVOS! ---
    [Header("2. Consecuencia de esta Opción")]
    [Tooltip("El fondo visual que se muestra DESPUÉS de elegir esta opción.")]
    public Sprite ConsequenceImage; // <--- IMAGEN DE CONSECUENCIA

    [Tooltip("El texto narrativo de la 'Secuencia' y 'Consecuencia' (ver PDF).")]
    [TextArea(4, 8)]
    public string ConsequenceText; // <--- TEXTO DE CONSECUENCIA
    // -----------------------

    [Header("3. Recompensa")]
    [Tooltip("La Ficha de Conocimiento que esta opción otorga al jugador.")]
    public KnowledgeFicha FichaToGrant; 
}