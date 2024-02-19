using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoHome()
    {
            SceneManager.LoadScene("MainMenu");
    }

    public void Play()
    {
            SceneManager.LoadScene("Level 1");
    }
}
