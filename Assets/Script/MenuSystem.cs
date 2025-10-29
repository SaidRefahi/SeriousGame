using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuSystem : MonoBehaviour
{
   int escenaActual = SceneManager.GetActiveScene().buildIndex;
   public void Play()
   {
      SceneManager.LoadScene(escenaActual + 1);
   }
   public void Quit()
   {
      Application.Quit();
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false; // ya NO tengo que sacarlo al buildear
#endif
      
   }
}

