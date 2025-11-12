// Archivo: Decision.cs
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ScriptableObject que representa una decisión completa.
/// Contiene la pregunta, las opciones, la siguiente decisión y la imagen de fondo.
/// </summary>
[CreateAssetMenu(fileName = "Decision_", menuName = "Juego de Decisiones/Decisión")]
public class Decision : ScriptableObject
{
    [Tooltip("El texto de la pregunta o dilema que se presenta al jugador.")]
    [TextArea(3, 6)]
    public string DecisionPrompt;

    // --- ¡NUEVA LÍNEA! ---
    [Tooltip("El fondo visual para este dilema o contexto narrativo.")]
    public Sprite BackgroundImage;
    // ---------------------

    [Tooltip("La lista de opciones para esta decisión. Idealmente 4.")]
    public List<DecisionOption> Options;
    
    [Tooltip("La próxima decisión que se cargará después de tomar esta. Déjalo en 'None' (vacío) si es el final.")]
    public Decision NextDecision;
}