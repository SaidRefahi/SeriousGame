using UnityEngine;

/// <summary>
/// ScriptableObject que representa una única opción para una decisión.
/// Contiene el texto de la opción y la ficha que otorga.
/// </summary>
[CreateAssetMenu(fileName = "Opcion_", menuName = "Juego de Decisiones/Opción de Decisión")]
public class DecisionOption : ScriptableObject
{
    [Tooltip("El texto que se mostrará en el botón de opción.")]
    [TextArea(2, 4)]
    public string OptionText;

    [Tooltip("La Ficha de Conocimiento que esta opción otorga al jugador.")]
    public KnowledgeFicha FichaToGrant;
}