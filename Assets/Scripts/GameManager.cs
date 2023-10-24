using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Structure[] structures;
    public int lastStructureIndex;

    public GameObject raylightPrefab;
    public int geiLimitX;
    public int geiLimitY;

    public int temperatureHandler=0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        StartCoroutine(SpawnGEI());
    }
    public IEnumerator SpawnGEI()
    {
        var cameraPos = Camera.main.transform.position;
        cameraPos.z = 0;
        while (true)
        {
            var obj = Instantiate(raylightPrefab, cameraPos, Quaternion.identity, null);
            obj.transform.position
                += new Vector3(Random.Range(-geiLimitX, geiLimitX + 1), Random.Range(-geiLimitY, geiLimitY + 1), 0);
            yield return new WaitForSecondsRealtime(Random.Range(5, 10));
        }
    }

    public void ReRollStructure(Building building)
    {
        building.structureSelected = structures[lastStructureIndex];
        building.image.sprite = structures[lastStructureIndex].initial;

        lastStructureIndex++;
        if (lastStructureIndex >= structures.Length)//start over
            ShuffleStructures(building.structureSelected);
    }
    public void ShuffleStructures(Structure structure)
    {
        lastStructureIndex = 0;
        for (int i = structures.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Structure temp = structures[i];
            structures[i] = structures[j];
            structures[j] = temp;
        }
        if(structures[0].initial== structure.initial)
        {//para que no se repitan dos veces seguidas
            Structure temp = structures[0];
            structures[0] = structures[structures.Length-1];
            structures[structures.Length - 1] = temp;
        }

    }
}
