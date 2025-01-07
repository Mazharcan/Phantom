using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuı : MonoBehaviour
{
    private void Start()
    {
     Time.timeScale = 1; 
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        // Editör modunda çalışırken Quit işe yaramaz, bunu görmek için bir log yazdırabiliriz
        Debug.Log("Game quit attempted in editor!");
        // Uygulamayı kapatır
        Application.Quit();
    }
}
