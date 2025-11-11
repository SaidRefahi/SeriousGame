// Archivo: JournalEntryUI.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Controla la visualización de UNA entrada en el diario.
/// Este script va en el prefab 'FichaEntry_Template'.
/// </summary>
public class JournalEntryUI : MonoBehaviour
{
    [SerializeField] private Image fichaIcon;
    [SerializeField] private TextMeshProUGUI fichaName;
    [SerializeField] private TextMeshProUGUI fichaDescription;

    /// <summary>
    /// Rellena los campos de la UI con la información de una Ficha.
    /// </summary>
    public void Setup(KnowledgeFicha ficha)
    {
        if (ficha == null) return;

        // Asigna los datos del ScriptableObject a los componentes de la UI
        fichaIcon.sprite = ficha.Icon;
        fichaName.text = ficha.DisplayName;
        fichaDescription.text = ficha.Description;

        // Opcional: si no hay icono, oculta la imagen
        fichaIcon.gameObject.SetActive(ficha.Icon != null);
    }
}