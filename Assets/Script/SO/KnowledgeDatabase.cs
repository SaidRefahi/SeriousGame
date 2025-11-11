using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Un ScriptableObject que actúa como una base de datos.
/// Contiene una lista de TODAS las fichas de conocimiento posibles en el juego.
/// </summary>
[CreateAssetMenu(fileName = "KnowledgeDatabase", menuName = "Juego de Decisiones/Base de Datos de Fichas")]
public class KnowledgeDatabase : ScriptableObject
{
    [Tooltip("Arrastra aquí TODOS los assets de KnowledgeFicha que existan en tu proyecto.")]
    public List<KnowledgeFicha> allFichas;

    /// <summary>
    /// Busca y devuelve un asset de KnowledgeFicha usando su ID.
    /// </summary>
    /// <param name="id">El FichaID que se quiere encontrar.</param>
    /// <returns>El ScriptableObject de la ficha, o null si no se encuentra.</returns>
    public KnowledgeFicha GetFichaByID(string id)
    {
        // Usamos Linq para encontrar la primera ficha que coincida con el ID.
        return allFichas.FirstOrDefault(ficha => ficha.FichaID == id);
    }
}