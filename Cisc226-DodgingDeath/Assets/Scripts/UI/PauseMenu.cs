using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static  bool gamepaused = false;
    public GameObject pausemenu;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamepaused)
            {
                Resume();
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

    public void OpenInventory()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
