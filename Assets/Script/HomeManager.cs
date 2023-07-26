using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
   public void OnStartButton()
    {
        SceneManager.LoadScene("Main");
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
