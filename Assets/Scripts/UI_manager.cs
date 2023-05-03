using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _pausepanel;

    


    [SerializeField]
    private Text _scoretext;
    
    [SerializeField]
    private Text _bestScoretext;

    [SerializeField]
    private Image _showLives;


    [SerializeField]
    private Sprite[] _playerLives;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private GameManager _gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        //if (_gamemanager._isCoOpmode == false)
        //{
        //    _bestScoretext.text = "Best: " + PlayerPrefs.GetInt("Bestscore", 0);

        //}
        _scoretext.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _pausepanel.SetActive(false);
    }

    public void playerscore(int score)
    {
        _scoretext.text = "Score: " + score;
    }

    public void bestScore(int Bestscore)
    {  
        _bestScoretext.text = "Best: " + Bestscore;
    }

    public void Updatelives(int currentlive)
    {
        _showLives.sprite = _playerLives[currentlive]; 

        if(currentlive == 0)
        {
            
            gameOverSequence();

        }
    }

    private void gameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _gamemanager.gameOver();
        StartCoroutine(GameoverFlicker());
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        _pausepanel.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    IEnumerator GameoverFlicker()
    {
        while(true)
        {
            _gameOverText.enabled = true;
            yield return new WaitForSeconds(0.5f);
            _gameOverText.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }
    }



 
}
