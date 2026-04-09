using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static  bool gamepaused = false;
    public GameObject pausemenu;
    public GameObject statmenu;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamepaused)
            {
                Resume();
                CloseStats();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        gamepaused = false;
    }

    void Pause()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
        gamepaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void OpenStats()
    {
        statmenu.SetActive(true);
    }

    public void CloseStats()
    {
        statmenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
