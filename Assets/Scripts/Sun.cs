using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public Raylight raylight;

    [SerializeField] int initialAmmount = 10;

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
                    var obj = Instantiate(raylight, transform.position, Quaternion.identity);
                        //obj.transform.up= new Vector2(Random.Range(0, 1.1f), -1);
                    obj.GetComponent<Raylight>().SetVelocity(new Vector2(Random.Range(0, 1.1f), -1));
                    yield return new WaitForSeconds(0.2f);
                }
                first = true;
            }
            else
            {
                var obj = Instantiate(raylight, transform.position, Quaternion.identity);
                //obj.transform.up = new Vector2(Random.Range(0, 1.1f), -1);
                obj.GetComponent<Raylight>().SetVelocity(new Vector2(Random.Range(0, 1.1f), -1));
            }

            yield return new WaitForSeconds(1.5f);
        }
    }
}
