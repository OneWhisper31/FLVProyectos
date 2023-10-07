using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public Raylight raylight;

    [SerializeField] int initialAmmount = 1;
    [SerializeField] float secBetweenSpawn = 0.3f;

    public void Start()
    {
        StartCoroutine(ShootRays());
    }
    public IEnumerator ShootRays()
    {
        bool first = false;
        while (true)
        {
            if (!first)
            {
                for (int i = 0; i < initialAmmount; i++)
                {
                    InstantiateRay();
                    yield return new WaitForSeconds(secBetweenSpawn);
                }
                first = true;
            }
            else
                InstantiateRay();

            yield return new WaitForSeconds(0.2f);
        }
    }
    public void InstantiateRay()
    {
        var obj = Instantiate(raylight, transform.position, Quaternion.identity);
        obj.transform.up = new Vector2(Random.Range(0, 1.1f), -1);
    }
}
