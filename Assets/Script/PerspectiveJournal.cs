// Archivo: PerspectiveJournal.cs
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Gestiona las Fichas de Conocimiento recolectadas por el jugador.
/// Utiliza un patrón Singleton para ser fácilmente accesible.
/// </summary>
public class PerspectiveJournal : MonoBehaviour
{
    // --- Singleton ---
    public static PerspectiveJournal Instance { get; private set; }

    private void Awake()
    {
        // Implementación simple de Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // Evita que el diario se destruya al cambiar de escena
            DontDestroyOnLoad(gameObject); 
        }
    }
    // -------------------

    // Usamos un HashSet para una comprobación de duplicados más eficiente
    // y para almacenar solo los IDs, que es más ligero.
    private HashSet<string> collectedFichaIDs = new HashSet<string>();

    // Opcional: si necesitas acceder a los objetos completos frecuentemente.
    // private List<KnowledgeFicha> collectedFichas = new List<KnowledgeFicha>();


    /// <summary>
    /// Añade una nueva ficha al diario.
    /// Evita añadir duplicados basándose en el FichaID.
    /// </summary>
    /// <param name="ficha">La ficha a añadir.</param>
    public void AddFicha(KnowledgeFicha ficha)
    {
        if (ficha == null)
        {
            Debug.LogWarning("Se intentó añadir una ficha nula.");
            return;
        }

        if (string.IsNullOrEmpty(ficha.FichaID))
        {
             Debug.LogWarning($"La ficha '{ficha.DisplayName}' no tiene un FichaID asignado.");
            return;
        }

        // Intentamos añadir el ID al HashSet.
        // Si 'Add' devuelve 'true', es que el ID no existía y se añadió.
        if (collectedFichaIDs.Add(ficha.FichaID))
        {
            // Opcional: Añadir el objeto completo a la lista si es necesario
            // collectedFichas.Add(ficha);
            
            Debug.Log($"Ficha añadida: {ficha.DisplayName} (ID: {ficha.FichaID})");
            
            // Aquí puedes disparar un evento para que la UI se actualice
            // OnFichaAdded?.Invoke(ficha);
        }
        else
        {
            Debug.Log($"El jugador ya tiene la ficha: {ficha.DisplayName}");
        }
    }

    /// <summary>
    /// Comprueba si el jugador ya posee una ficha específica por su ID.
    /// </summary>
    /// <param name="fichaID">El ID de la ficha a comprobar.</param>
    /// <returns>True si la ficha ya ha sido recolectada.</returns>
    public bool HasFicha(string fichaID)
    {
        if (string.IsNullOrEmpty(fichaID)) return false;
        
        return collectedFichaIDs.Contains(fichaID);
    }

    /// <summary>
    /// Devuelve una copia de todos los IDs de las fichas recolectadas.
    /// </summary>
    public HashSet<string> GetCollectedFichaIDs()
    {
        // Devolvemos una nueva colección para evitar modificaciones externas
        return new HashSet<string>(collectedFichaIDs);
    }

    // --- Aquí iría la lógica para GUARDAR/CARGAR el HashSet ---
    // (por ejemplo, usando PlayerPrefs convirtiendo el HashSet a string, o usando JSON)
}