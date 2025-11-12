// Archivo: JournalUIManager.cs
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Gestiona el Panel de la UI del Diario.
/// Lee los datos de PerspectiveJournal y los muestra usando prefabs.
/// </summary>
public class JournalUIManager : MonoBehaviour
{
    [Header("Referencias de Proyecto")]
    [Tooltip("Arrastra aquí el asset 'MiDatabaseDeFichas'")]
    [SerializeField] private KnowledgeDatabase knowledgeDatabase;

    [Tooltip("Arrastra aquí el prefab 'FichaEntry_Template'")]
    [SerializeField] private GameObject fichaEntryPrefab;

    [Header("Referencias de Escena")]
    [Tooltip("El objeto 'Content' dentro del Scroll View")]
    [SerializeField] private Transform contentParent;
    
    [Tooltip("El panel del menú principal, para ocultarlo")]
    [SerializeField] private GameObject panelMenuPrincipal; 

    // Se llama automáticamente cuando este GameObject se activa
    void OnEnable()
    {
        PopulateJournal();
    }

    /// <summary>
    /// Muestra el panel del diario y oculta el menú principal.
    /// </summary>
    public void OpenJournal()
    {
        gameObject.SetActive(true);
        if(panelMenuPrincipal != null)
        {
            panelMenuPrincipal.SetActive(false);
        }
    }

    /// <summary>
    /// Cierra el panel del diario y muestra el menú principal.
    /// </summary>
    public void CloseJournal()
    {
        gameObject.SetActive(false);
        if(panelMenuPrincipal != null)
        {
            panelMenuPrincipal.SetActive(true);
        }
    }

    /// <summary>
    /// Borra la lista actual y la vuelve a crear con las fichas recolectadas.
    /// </summary>
    private void PopulateJournal()
    {
        // 1. Limpiar entradas antiguas (para que no se dupliquen)
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // 2. Obtener los IDs de las fichas que tenemos
        HashSet<string> collectedIDs = PerspectiveJournal.Instance.GetCollectedFichaIDs();

        if (collectedIDs.Count == 0)
        {
            Debug.Log("El diario está vacío.");
            // Opcional: Instanciar un prefab de "Diario Vacío"
            return;
        }

        // 3. Crear una entrada por cada ID
        foreach (string id in collectedIDs)
        {
            // 4. Buscar la información completa de la ficha en la base de datos
            KnowledgeFicha ficha = knowledgeDatabase.GetFichaByID(id);

            if (ficha != null)
            {
                // 5. Crear una instancia del prefab
                GameObject entryGO = Instantiate(fichaEntryPrefab, contentParent);
                
                // 6. Configurar la entrada con los datos de la ficha
                entryGO.GetComponent<JournalEntryUI>().Setup(ficha);
            }
            else
            {
                Debug.LogWarning($"No se encontró la ficha con ID: {id} en la base de datos.");
            }
        }
    }

    /// <summary>
    /// Se llama desde el botón "Resetear Progreso".
    /// Llama al PerspectiveJournal para borrar los datos y luego
    /// refresca la UI del diario.
    /// </summary>
    public void HandleResetButton()
    {
        // 1. Pedir al gestor de datos que borre todo
        if (PerspectiveJournal.Instance != null)
        {
            PerspectiveJournal.Instance.ResetJournal();
        }

        // 2. Volver a poblar el diario (que ahora estará vacío)
        // Esto refresca la UI al instante.
        PopulateJournal();
        
        Debug.Log("UI del diario refrescada después del reseteo.");
    }
}