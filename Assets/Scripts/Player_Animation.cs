using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    [SerializeField]
    private int _playernumber;
    
    private Animator _playerAnimation;

    private PlayerScript _playerscript;

    private void Start()
    {
        _playerAnimation = GetComponent<Animator>();
        _playerscript = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_playernumber == 1)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {

                _playerAnimation.SetInteger("Turn_Right", 1);

            }
            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                _playerAnimation.SetInteger("Turn_Right", 0);
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {

                _playerAnimation.SetInteger("Turn_Left", 1);

            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {

                _playerAnimation.SetInteger("Turn_Left", 0);

            }

            if(_playerscript._lives <= 0)
            {
                _playerAnimation.SetTrigger("Explosion");
            }

        }

        if(_playernumber == 2)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {

                _playerAnimation.SetInteger("Turn_Right", 1);

            }
            else if (Input.GetKeyUp(KeyCode.L))
            {
                _playerAnimation.SetInteger("Turn_Right", 0);
            }

            if (Input.GetKeyDown(KeyCode.J))
            {

                _playerAnimation.SetInteger("Turn_Left", 1);

            }
            else if (Input.GetKeyUp(KeyCode.J))
            {

                _playerAnimation.SetInteger("Turn_Left", 0);

            }

            if (_playerscript._lives <= 0)
            {
                _playerAnimation.SetTrigger("Explosion");
            }
        }

       
    }

    


}
