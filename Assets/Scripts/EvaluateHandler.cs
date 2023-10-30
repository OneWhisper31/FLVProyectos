using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateHandler : MonoBehaviour
{
    [SerializeField] CanvasHandler canvasHandler;

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
                canvasHandler.UpdateTemperature(-0.05f);//sube temperatura
            else
                canvasHandler.UpdateTemperature(0.05f);//baja temperatura
        }   
    }
}
