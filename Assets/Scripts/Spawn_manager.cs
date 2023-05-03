using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawn_manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyprefab;
    [SerializeField]
    private float spawntime = 4.0f;
    [SerializeField]
    private GameObject _enemycontainer;
    [SerializeField]
    private GameObject[] _powerUps;
    
    private bool _stopspawing = false;

  

    public void startSpawning()
    {
        StartCoroutine(spawnEnemyroutine());
        StartCoroutine(spawnPowerUpRoutine());

    }

 
   

    // create coroutine.
    // inside it.
    // make infinite loop to create many enemies.
    // instantiate enemy prefab.
    // spawn enemies every 4 seconds.
    // then, start coroutine.

    IEnumerator spawnEnemyroutine()
    {
        yield return new WaitForSeconds(3f);
        while(_stopspawing == false)
        {
            //spawn enemies at ramdom position of x
            Vector3 enemypos = new Vector3(Random.Range(-7.5f, 7.5f), 7, 0);
            GameObject newenemy = Instantiate(_enemyprefab, enemypos, Quaternion.identity);
            newenemy.transform.parent = _enemycontainer.transform;
            yield return new WaitForSeconds(spawntime);
        }
    }
     
    
    IEnumerator spawnPowerUpRoutine()
    {
        // spawn it in every 4 to 8 seconds
        // if player it take the power up
        // then destroy it
        yield return new WaitForSeconds(3f);

        while(_stopspawing == false)
        {

            float poweruptime = Random.Range(10, 15);
            int randompowerUps = Random.Range(0, 3);
            yield return new WaitForSeconds(poweruptime);
            Instantiate(_powerUps[randompowerUps], new Vector3(Random.Range(-7f, 7f), 7f, 0), Quaternion.identity);
        }
    }


    

    public void onplayerdeath()
    {
        _stopspawing = true;
    }

}
