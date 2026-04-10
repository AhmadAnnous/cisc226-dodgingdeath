using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
