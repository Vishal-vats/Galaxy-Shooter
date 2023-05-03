using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class enemyscript : MonoBehaviour
{

    
    private PlayerScript _playerscript;

    private Animator _explodeEnemyAnimation;

    private Collider2D _collider;

    
    private GameManager _gamemanager; // To Acknowledge this is CoOp Mode.

    [SerializeField]
    private AudioClip _enemyExplosionSound;

    [SerializeField]
    private AudioSource _audiosource;

    [SerializeField]
    private GameObject _enemyLaser;

    private float _canFire = -1f;
    private float _fireRate = 2f;
 
    private void Start()
    {
        _gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        
        if (_gamemanager._isCoOpmode == true)
        {
            if(GameObject.Find("Player_1")!= null)
            {
                _playerscript = GameObject.Find("Player_1").GetComponent<PlayerScript>();
            }
            if(GameObject.Find("Player_2") != null)
            {
                _playerscript = GameObject.Find("Player_2").GetComponent<PlayerScript>();
            }            

        }
        else
        {
            _playerscript = GameObject.Find("Player").GetComponent<PlayerScript>();

        }

        if(_playerscript == null)
        {
            Debug.LogError("Player is Null");
        }

        _explodeEnemyAnimation = GetComponent<Animator>();

        if(_explodeEnemyAnimation == null)
        {
            Debug.LogError("Enemy animation is NULL");
        }

        _collider = GetComponent<Collider2D>();

        

    }

    void Update()
    {
        calculatemovement();

        //Cooldown system for enemy and Instantiating laser 
        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject laserEnemy = Instantiate(_enemyLaser, transform.position, Quaternion.identity);
            Lasermovement []lasers = laserEnemy.GetComponentsInChildren<Lasermovement>();

            for(int i = 0; i < lasers.Length; i++)
            {
                lasers[i].assignEnemyLaser();
            }

        }


    }

    void calculatemovement()
    {
        if (transform.position.y < -6f)
        {
            int randomposition = Random.Range(-9, 9);
            transform.position = new Vector3(randomposition, 7.5f, 0);
        }
    }

  


    private void OnTriggerEnter2D (Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _playerscript.updateScore(10);
            _explodeEnemyAnimation.SetTrigger("OnenemyDeath"); //it trigger the explosion animation on enemy
            _collider.enabled = false;

            AudioSource.PlayClipAtPoint(_enemyExplosionSound, Camera.main.transform.position, 0.6f);
            

            Destroy(this.gameObject, 1.5f);
        }

        if(other.gameObject.tag == "Player")
        {
          
            PlayerScript playerscript = other.transform.GetComponent<PlayerScript>();
            Debug.Log("enemy hit");

            if(playerscript != null)
            {
                playerscript.Damage();
            }

            _explodeEnemyAnimation.SetTrigger("OnenemyDeath");
            _collider.enabled = false;
            
            AudioSource.PlayClipAtPoint(_enemyExplosionSound, Camera.main.transform.position, 0.6f);

            Destroy(this.gameObject, 1.5f);

        }
    }

}
