using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //int currentScene;

    private void Start()
    {
        //currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void loadGame()
    {

        SceneManager.LoadScene(1); 

    }

    public void singlePlayerGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Co_OpMode()
    {
        SceneManager.LoadScene(2);
    }



}
