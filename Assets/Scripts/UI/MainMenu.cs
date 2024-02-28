using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
            SceneManager.LoadScene("Level 1");
    }

    public void CreditMenu()
    {
            SceneManager.LoadScene("Credit");
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }
}
