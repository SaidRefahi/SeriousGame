// Archivo: Decision.cs
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ScriptableObject que representa una decisión completa.
/// Contiene la pregunta y la lista de opciones disponibles.
/// </summary>
[CreateAssetMenu(fileName = "Decision_", menuName = "Juego de Decisiones/Decisión")]
public class Decision : ScriptableObject
{
    [Tooltip("El texto de la pregunta o dilema que se presenta al jugador.")]
    [TextArea(3, 6)]
    public string DecisionPrompt;

    [Tooltip("La lista de opciones para esta decisión. Idealmente 4.")]
    public List<DecisionOption> Options;
    
    // Opcional: Puedes añadir un campo para la siguiente decisión a cargar
    // [Tooltip("La próxima decisión que se cargará después de tomar esta.")]
    // public Decision NextDecision;
}