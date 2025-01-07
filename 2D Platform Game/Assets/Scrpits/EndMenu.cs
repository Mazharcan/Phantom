using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1; // Oyunu tekrar baþlat
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1; 
    }
}
