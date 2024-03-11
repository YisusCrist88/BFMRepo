using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
   public void SceneLoader (int sceneToLoad)
    {
        
        SceneManager.LoadScene (sceneToLoad);
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
