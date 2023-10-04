using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Planet planet;

    //public 

    //public SpawnPoint spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(lifetime());
    }
    IEnumerator lifetime()
    {

        //cuando pase la pantalla
        yield return new WaitForSeconds(5);
        //spawnPoint.isUsed = false;
        //planet.SetSpawnPoint(spawnPoint);
        Destroy(this);
    }
}
