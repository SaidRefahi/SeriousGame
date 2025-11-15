// Archivo: ButtonSound.cs
using UnityEngine;

/// <summary>
/// Un script "ayudante" súper simple (KISS).
/// Su única función es ser conectada al OnClick() de cualquier botón
/// de UI que no sea un 'OptionButton' (ej. Pausa, Menú, Diario).
/// </summary>
public class ButtonSound : MonoBehaviour
{
    public void PlayClickSFX()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayClickSound();
        }
    }
}