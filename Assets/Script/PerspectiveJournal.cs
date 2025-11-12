// Archivo: PerspectiveJournal.cs
using UnityEngine;
using System.Collections.Generic;
using System.Linq; // ¡Necesario para .ToList()!

/// <summary>
/// Gestiona las Fichas de Conocimiento recolectadas por el jugador.
/// Utiliza un patrón Singleton y guarda/carga los datos en PlayerPrefs.
/// </summary>
public class PerspectiveJournal : MonoBehaviour
{
    // --- Singleton ---
    public static PerspectiveJournal Instance { get; private set; }

    // --- Datos ---
    private HashSet<string> collectedFichaIDs = new HashSet<string>();
    
    // --- Clave de Guardado ---
    private const string SaveKey = "PerspectiveJournalData";

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
            DontDestroyOnLoad(gameObject);
            
            // --- ¡CARGAMOS DATOS AL INICIAR! ---
            LoadJournal();
        }
    }

    /// <summary>
    /// Añade una nueva ficha al diario y guarda el progreso.
    /// </summary>
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

        // Si 'Add' devuelve 'true', es que el ID no existía y se añadió.
        if (collectedFichaIDs.Add(ficha.FichaID))
        {
            Debug.Log($"Ficha añadida: {ficha.DisplayName} (ID: {ficha.FichaID})");
            
            // --- ¡GUARDAMOS CADA VEZ QUE AÑADIMOS UNA NUEVA! ---
            SaveJournal();
            
            // OnFichaAdded?.Invoke(ficha); // Opcional para eventos de UI
        }
        else
        {
            Debug.Log($"El jugador ya tiene la ficha: {ficha.DisplayName}");
        }
    }

    /// <summary>
    /// Comprueba si el jugador ya posee una ficha específica por su ID.
    /// </summary>
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
        return new HashSet<string>(collectedFichaIDs);
    }


    // --- SISTEMA DE GUARDADO / CARGADO ---

    /// <summary>
    /// Pequeña clase contenedora para que JsonUtility pueda guardar una lista.
    /// Debe estar marcada como [System.Serializable]
    /// </summary>
    [System.Serializable]
    private class FichaSaveData
    {
        public List<string> collectedIDs;
    }

    /// <summary>
    /// Carga los IDs guardados desde PlayerPrefs.
    /// </summary>
    private void LoadJournal()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            
            // Convierte el JSON de vuelta a nuestra clase contenedora
            FichaSaveData saveData = JsonUtility.FromJson<FichaSaveData>(json);

            // Convierte la Lista de vuelta a un HashSet
            collectedFichaIDs = new HashSet<string>(saveData.collectedIDs);
            Debug.Log($"Diario cargado. {collectedFichaIDs.Count} fichas recolectadas.");
        }
        else
        {
            Debug.Log("No se encontraron datos de diario. Empezando de cero.");
        }
    }

    /// <summary>
    /// Guarda el HashSet de IDs en PlayerPrefs usando JSON.
    /// </summary>
    private void SaveJournal()
    {
        // Convierte el HashSet a una Lista (JsonUtility no entiende HashSets)
        FichaSaveData saveData = new FichaSaveData
        {
            collectedIDs = collectedFichaIDs.ToList()
        };

        // Convierte la clase contenedora a un string JSON
        string json = JsonUtility.ToJson(saveData);

        // Guarda el string en PlayerPrefs
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save(); // Forzar guardado inmediato
        
        Debug.Log("Progreso del diario guardado.");
    }

    
    /// <summary>
    /// Resetea el progreso del jugador.
    /// Borra los datos guardados en disco (PlayerPrefs) y en memoria (HashSet).
    /// </summary>
    public void ResetJournal()
    {
        // 1. Borrar los datos en memoria
        collectedFichaIDs.Clear();

        // 2. Borrar los datos guardados en disco
        if (PlayerPrefs.HasKey(SaveKey))
        {
            PlayerPrefs.DeleteKey(SaveKey);
            Debug.Log("Datos del diario borrados de PlayerPrefs.");
        }
        
        Debug.Log("¡Progreso del diario reseteado!");
    }
}