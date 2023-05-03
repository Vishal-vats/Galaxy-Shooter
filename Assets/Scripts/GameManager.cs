using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _pausePanel;
    [SerializeField]
    private bool _isGameOver = false;
    public bool _isCoOpmode = false; // set to ture from the unity for Co-Op Mode.
   
  

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //load current scene.
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
       
        if(_isCoOpmode == true && Input.GetKeyDown(KeyCode.M) && _isGameOver == true)
        {
            SceneManager.LoadScene(0);
        }

        
    }

   

    public void gameOver()
    {
        _isGameOver = true;
    }
}
