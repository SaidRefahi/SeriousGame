// Archivo: GameFlowManager.cs
using UnityEngine;

/// <summary>
/// Este ScriptableObject actúa como un "contenedor" de datos para pasar
/// información entre escenas, como la decisión inicial seleccionada.
/// </summary>
[CreateAssetMenu(fileName = "GameFlowManager", menuName = "Juego de Decisiones/Gestor de Flujo de Juego")]
public class GameFlowManager : ScriptableObject
{
    [Header("Datos de Flujo de Juego")]
    [Tooltip("La primera decisión de la historia que el jugador ha seleccionado.")]
    public Decision StartingDecision;
}