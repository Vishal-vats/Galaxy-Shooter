using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    
    public bool _playerOne = false;
    public bool _playerTwo = false;
    public int _lives = 3;
   
    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private GameObject _laserprefab;

    [SerializeField]
    private GameObject _trippleshot;

    [SerializeField]
    private GameObject _spawnThrust;

    [SerializeField]
    private GameObject _shieldvisualiser;

    [SerializeField]
    private GameObject _smallThrust;

    [SerializeField]
    private GameObject[] _hurtDamageEffects;

    private float _canfire = 0f;

    [SerializeField]
    private float _firerate = 0.2f;

    

    [SerializeField]
    private int _scoreText;

    private int _bestScore;

    [SerializeField]
    private Text _BestScoreText;

    


    private UI_manager _uiManager;

    private Spawn_manager _spawnmanager;

    private GameManager _gamemanager;

    [SerializeField]
    private bool _istripplehsotactive = false;

    [SerializeField]
    private bool _isShieldUpActive = false;

    [SerializeField]
    private AudioClip _laserShotClip;

    [SerializeField]
    private AudioClip _playerExplosion;

    [SerializeField]
    private AudioClip _powerUpSound;

    private AudioSource _audiosource;
    


    void Start()
    {

        _smallThrust.SetActive(true);

        _hurtDamageEffects[0].SetActive(false);
        _hurtDamageEffects[1].SetActive(false);


        _spawnmanager = GameObject.Find("Spawn_manager").GetComponent<Spawn_manager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_manager>();

        _gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _audiosource = GetComponent<AudioSource>();

        _shieldvisualiser.SetActive(false);


        if(_gamemanager._isCoOpmode == false)
        {
            _bestScore = PlayerPrefs.GetInt("BestScore", 0);
            _BestScoreText.text = "Best: " + _bestScore.ToString();
        }
        
        if (_playerOne == true && _gamemanager._isCoOpmode == true)
        {
            transform.position = new Vector3(-3.5f, -2.58f, 0);
        }
        else if(_playerTwo == true && _gamemanager._isCoOpmode == true)
        { 
            transform.position = new Vector3(3.5f, -2.58f, 0);

        }


        if (_gamemanager._isCoOpmode == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }

        

        // always check for null condition to avoid errors
        if(_spawnmanager == null)
        {
            Debug.LogError("SpawnManager is Null");
        }

        if(_uiManager == null)
        {
            Debug.LogError("UImanager is null");
        }

        if(_audiosource == null)
        {
            Debug.LogError("AudioSource on a player is NULL");
        }
      

    }

   
    void Update()
    {

        playermovement();


        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire && _playerOne == true)
        {

            firelaser();

        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter) && Time.time >_canfire && _playerTwo == true)
        {
            firelaser();
        }

    }

    // for finding bugs in code and to organise the code we create CUSTOM methods

    private void playermovement()
    {

        
        if(_playerOne == true)
        {
            float movehorizontal = Input.GetAxis("Horizontal");

            float movevertical = Input.GetAxis("Vertical");

            transform.Translate(_speed * Time.deltaTime * new Vector3(movehorizontal, movevertical, 0));
        }
        
        if(_playerTwo == true)
        {
            float moveHorizontal1 = Input.GetAxis("Sidemove");
            float moveVertical1 = Input.GetAxis("Upmove");
            transform.Translate(_speed * Time.deltaTime * new Vector3(moveHorizontal1, moveVertical1, 0));
        }

        // transform.Translate(Vector2.up * movevertical * speed * Time.deltaTime);

        //transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime);

        //transform.position += new Vector3(movehorizontal * speed * Time.deltaTime, movevertical * speed * Time.deltaTime, 0);



        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.9f, 6), 0);


        if (transform.position.x >= 11.33)
        {
            transform.position = new Vector3(-11.33f, transform.position.y, 0);
        }

        else if (transform.position.x <= -11.33)
        {
            transform.position = new Vector3(11.33f, transform.position.y, 0);
        }

    }


    void firelaser()
    { 
        _canfire = Time.time + _firerate;

        if (_istripplehsotactive == true)
        {

            Instantiate(_trippleshot, transform.position + new Vector3(-0.6f, 0, 0), Quaternion.identity);

        }
        else
        {

            Instantiate(_laserprefab, transform.position + new Vector3(0, 1.07f, 0), Quaternion.identity);

        }

        _audiosource.clip = _laserShotClip;
        _audiosource.Play();
        

    }



    public void Damage()
    {

        if(_isShieldUpActive == true)
        {
            Debug.Log("Not Damaged");
            _isShieldUpActive = false;
            _shieldvisualiser.SetActive(false);
            return;
        }


        _lives--;

        _uiManager.Updatelives(_lives);

        switch(_lives)
        {
            case 2:
                _hurtDamageEffects[0].SetActive(true);
                break;

            case 1:
                _hurtDamageEffects[1].SetActive(true);
                break;
        }

        if (_lives <= 0)
        {

            _spawnmanager.onplayerdeath();

            if(_gamemanager._isCoOpmode == false)
            {
                Bestscore();
            }
          
            AudioSource.PlayClipAtPoint(_playerExplosion, Camera.main.transform.position, 0.5f);
            _smallThrust.SetActive(false);
            _hurtDamageEffects[0].SetActive(false);
            _hurtDamageEffects[1].SetActive(false);
            Destroy(this.gameObject, 1.2f);

        }

    }

    private void Bestscore()
    {
        if (_bestScore < _scoreText)
        {
            _bestScore = _scoreText;
            PlayerPrefs.SetInt("BestScore", _bestScore);
            _uiManager.bestScore(_bestScore);
        }
    }

    private void playPowerUpSound()
    {
        _audiosource.clip = _powerUpSound;
        _audiosource.Play();
    }

    public void trippleShotactive()
    {
        _istripplehsotactive = true;
        playPowerUpSound();
        StartCoroutine(TrippleShotCollected());
    }

    IEnumerator TrippleShotCollected()
    {
        yield return new WaitForSeconds(5f);
        _istripplehsotactive = false;
        
    }

    public void speedBoostUpActive()
    {
        _speed = 15f;
        playPowerUpSound();
        StartCoroutine(SpeedUpCollected());
    }
     
    IEnumerator SpeedUpCollected()
    {
        Transform spawnthrustposition = GameObject.Find("Thrust position").GetComponent<Transform>();
        _smallThrust.SetActive(false);
        GameObject Thrustclone = Instantiate(_spawnThrust, spawnthrustposition.position, Quaternion.identity);
        Thrustclone.transform.parent = spawnthrustposition.transform;

        yield return new WaitForSeconds(5f);

        _speed = 5f;
        Destroy(Thrustclone);
        _smallThrust.SetActive(true);

    }

    public void shieldUpActive()
    {
        _isShieldUpActive = true;
        playPowerUpSound();
        _shieldvisualiser.SetActive(true);
        
    }

    public void updateScore(int score)
    {
        _scoreText += score;
       
        _uiManager.playerscore(_scoreText);
    }

    
    

}
