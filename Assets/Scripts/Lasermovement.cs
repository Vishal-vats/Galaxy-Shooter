using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Lasermovement : MonoBehaviour
{
   
    [SerializeField]
    private float _speed = 10f;

    private bool _isEnemyLaser = false;


   
   

    // Update is called once per frame
    void Update()
    {
        if(_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
        
    }


    void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);


        if (transform.position.y > 7.5f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }
    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        if (transform.position.y < -7.5f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }


    public void assignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player" && _isEnemyLaser == true)
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            
            if(player != null)
            {
                player.Damage();
               // Destroy(this.gameObject);
                Destroy(this.transform.parent.gameObject, 0.07f);
            }
        }
        
    }


}
