using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CanvasHandler canvasHandler;

    public StructureSO[] structures;
    public int lastStructureIndex;

    public GameObject raylightPrefab;
    [SerializeField] float geiLimitX;
    [SerializeField] float geiLimitY;
    [SerializeField] float geiLimitMinusX;
    [SerializeField] float geiLimitMinusY;
    [SerializeField] float offsetY;
    [SerializeField] float atmosphericRadius;


    [SerializeField] int initalGei = 10;

    public bool pauseMode = true;//if false

    public OptionsPanel OptionPanel { get {
            if (optionPanel==null)
            {
                optionPanel = FindObjectOfType<OptionsPanel>();
                return optionPanel;
            }
            else
                return optionPanel;
    }}
    OptionsPanel optionPanel;

    [Header("DebugMode")]
    [SerializeField] bool debugMode;
    [SerializeField] float debugModeAngle;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }
    private void Start()
    => StartCoroutine(SpawnGEI());

    public void ChangePause(bool newPauseMode) 
        => pauseMode = newPauseMode;

    public IEnumerator SpawnGEI()
    {
        for (int i = 0; i < initalGei; i++)
            NewGEI();

        while (true)
        {
            if (pauseMode)
                yield return new WaitUntil(() => !pauseMode);

            NewGEI();
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }
    public void NewGEI()
    {
        //float semiMajorAxis = geiLimitX;  // Semieje mayor
        //float semiMinorAxis = geiLimitY;  // Semieje menor

        // Generar un ángulo aleatorio en radianes dentro de un semicirculo (0 a 1*pi)
        //float randomAngle = Random.Range(0f, 1f * Mathf.PI);
        //float randomRadius = Random.Range(0f, atmosphericRadius);


        var obj = Instantiate(raylightPrefab,
            //new Vector3(geiLimitX * randomRadius * Mathf.Cos(randomAngle), geiLimitY * randomRadius * Mathf.Sin(randomAngle) - offsetY, 0)
            //Vector3.ClampMagnitude(
            new Vector3(Random.Range(geiLimitMinusX, geiLimitX + 1), Random.Range(geiLimitMinusY, geiLimitY + 1), 0)//,atmosphericRadius)
            , Quaternion.identity, null);

        float currentScale = obj.transform.localScale.x;

        obj.transform.localScale = Vector3.zero;
        obj.transform.DOScale(currentScale + Random.Range(0f, 0.2f), 2);
    }
    public void DelayReRollStructure(Building building, int delaySec)
        => StartCoroutine(delayReRollStructure(building, delaySec));
    IEnumerator delayReRollStructure(Building building, int delaySec)
    {
        StopAllCoroutines();
        if(pauseMode)
            yield return new WaitUntil(() => !pauseMode);

        yield return new WaitForSeconds(delaySec);

        building.ResetButton();
        ReRollStructure(building);
    }
    public void ReRollStructure(Building building)
    {
        building.structureSelected = structures[lastStructureIndex];
        building.image.sprite = structures[lastStructureIndex].initial.sprite;

        lastStructureIndex++;
        if (lastStructureIndex >= structures.Length)//start over
            ShuffleStructures(building.structureSelected);
    }
    public void ReRollStructure(BuildingDebugMode building)
    {
        building.structureSelected = structures[lastStructureIndex];
        building.image.sprite = structures[lastStructureIndex].initial.sprite;

        lastStructureIndex++;
        if (lastStructureIndex >= structures.Length)//start over
            ShuffleStructures(building.structureSelected);
    }
    public void ShuffleStructures(StructureSO structure)
    {
        lastStructureIndex = 0;
        for (int i = structures.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            StructureSO temp = structures[i];
            structures[i] = structures[j];
            structures[j] = temp;
        }
        if (structures[0].initial.sprite == structure.initial.sprite)
        {//para que no se repitan dos veces seguidas
            StructureSO temp = structures[0];
            structures[0] = structures[structures.Length - 1];
            structures[structures.Length - 1] = temp;
        }

    }
    public void ShuffleStructures()
    {
        lastStructureIndex = 0;
        for (int i = structures.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            StructureSO temp = structures[i];
            structures[i] = structures[j];
            structures[j] = temp;
        }

    }
    public void OnDrawGizmos()
    {
        if (!debugMode)
            return;

        float randomAngle = debugModeAngle * Mathf.PI;

        // Calcular la distancia radial dentro de la elipse
        //float randomRadius = Random.Range(0f, 1f);  // Valor entre 0 y 1

        // Calcular las coordenadas en la elipse
        Gizmos.DrawSphere(new Vector3(geiLimitX * atmosphericRadius * Mathf.Cos(randomAngle), geiLimitY* atmosphericRadius * Mathf.Sin(randomAngle) - offsetY, 0),2);
    }

}
