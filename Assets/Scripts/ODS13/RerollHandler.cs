using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollHandler : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartCounting());
    }
    IEnumerator StartCounting()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        var building = collision.GetComponent<Building>();

        if (building != null)
        {

            GameManager.Instance.ReRollStructure(building);
        }
    }
}
