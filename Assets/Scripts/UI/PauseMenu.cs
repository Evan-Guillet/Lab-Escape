using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
        public GameObject canvas;
    // Start is called before the first frame update
    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Play()
    {
            canvas.SetActive(false);
            Time.timeScale = 1f;
    }
}
