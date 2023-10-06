using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateHandler : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartCounting());
    }
    IEnumerator StartCounting()
    {
        yield return new WaitForSeconds(2);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        var building = collision.GetComponent<Building>();

        if (building!=null)
        {
            if (building.hasChoose)
                GameManager.Instance.chanceHandler--;
            else
                GameManager.Instance.chanceHandler++;

            building.ReRollStructure();

            //Debug.Log(GameManager.Instance.chanceHandler);
        }   
    }
}
