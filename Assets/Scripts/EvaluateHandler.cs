using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateHandler : MonoBehaviour
{
    [SerializeField] CanvasHandler canvasHandler;
    [SerializeField] OptionsPanel optionsPanel;

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

        if (building != null)//si la accion es positiva seria -0.05 * 1 por lo que bajaria la temperatura || sino -0.05 * -1
        {
            if (building == optionsPanel.CurrentBuilding)
                optionsPanel.DisableOptions();

            canvasHandler.UpdateTemperature(-0.05f * building.value);
        }
    }
}
