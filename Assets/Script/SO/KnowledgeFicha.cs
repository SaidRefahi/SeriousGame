// Archivo: KnowledgeFicha.cs
using UnityEngine;

/// <summary>
/// ScriptableObject que representa una Ficha de Conocimiento coleccionable.
/// Contiene la información única de esta ficha.
/// </summary>
[CreateAssetMenu(fileName = "Ficha_", menuName = "Juego de Decisiones/Ficha de Conocimiento")]
public class KnowledgeFicha : ScriptableObject
{
    [Tooltip("ID único para esta ficha (ej: 'FICHA_001', 'FICHA_LOGICA_A').")]
    public string FichaID; 

    [Tooltip("Nombre de la ficha que se mostrará al jugador.")]
    public string DisplayName;

    [Tooltip("Descripción que aparecerá en el Diario de Perspectivas.")]
    [TextArea(3, 5)]
    public string Description;

    [Tooltip("Icono para mostrar en el diario.")]
    public Sprite Icon;
}