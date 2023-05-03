using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 10f;

    [SerializeField]
    private GameObject _explosionAsteroid;

    [SerializeField]
    private AudioClip _explosionAsteroidSound;

    private Spawn_manager _spawnmanager;

    
    //[SerializeField]
    //private float _speed = -6f;

 
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        //transform.position += new Vector3(0, _speed * Time.deltaTime, 0);
        _spawnmanager = GameObject.Find("Spawn_manager").GetComponent<Spawn_manager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            
            Destroy(collision.gameObject);
            GameObject astroidexplosion = Instantiate(_explosionAsteroid, transform.position, Quaternion.identity);
            _spawnmanager.startSpawning(); // this starts the spawning of enemies and powerUp's.

            AudioSource.PlayClipAtPoint(_explosionAsteroidSound, Camera.main.transform.position, 0.6f);

            Destroy(this.gameObject, 0.2f);
            Destroy(astroidexplosion.gameObject, 3f);

        }
    }

}
