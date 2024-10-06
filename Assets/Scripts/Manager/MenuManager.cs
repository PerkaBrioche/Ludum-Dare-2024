using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadScene(int Index)
    {
        SceneManager.LoadScene(Index);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
