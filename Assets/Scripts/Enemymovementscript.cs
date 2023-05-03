using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymovementscript : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
  

    
    void Update()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
    }
}
