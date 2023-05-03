using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Trippleshot : MonoBehaviour
{
    // this script is used for every powerUp's

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int _uniquenumberToPowerUp;

   

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);

        if(transform.position.y < -5.7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerScript player = collision.transform.GetComponent<PlayerScript>();

            //checking null condition

            if(player != null)
            {

                switch(_uniquenumberToPowerUp)
                {
                    case 0:
                        player.trippleShotactive();
                        break;

                    case 1:
                        player.speedBoostUpActive();
                        break;

                    case 2:
                        player.shieldUpActive();
                        break;
                }

            }

            Destroy(this.gameObject);
            
        }
    }



}
