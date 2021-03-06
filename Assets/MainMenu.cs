using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MenuScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().buildIndex==1){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
            }
        }
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
