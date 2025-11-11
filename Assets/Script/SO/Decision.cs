// Archivo: Decision.cs
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ScriptableObject que representa una decisión completa.
/// Contiene la pregunta, la lista de opciones y la siguiente decisión.
/// </summary>
[CreateAssetMenu(fileName = "Decision_", menuName = "Juego de Decisiones/Decisión")]
public class Decision : ScriptableObject
{
    [Tooltip("El texto de la pregunta o dilema que se presenta al jugador.")]
    [TextArea(3, 6)]
    public string DecisionPrompt;

    [Tooltip("La lista de opciones para esta decisión. Idealmente 4.")]
    public List<DecisionOption> Options;
    
    [Tooltip("La próxima decisión que se cargará después de tomar esta. Déjalo en 'None' (vacío) si es el final.")]
    public Decision NextDecision;
}