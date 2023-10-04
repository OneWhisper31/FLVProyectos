using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using Random = UnityEngine.Random;

public class Planet : MonoBehaviour
{
    [SerializeField] Image image;


    [SerializeField] Transform[] posibleSpawns;

    [SerializeField] int deg;//Debugmode

    [SerializeField] Building building;

    [SerializeField] List<Building> buildings;

    [SerializeField] bool gizmos;

    [SerializeField] Range[] avoidRanges;


    private void Start()
    {
        //StartCoroutine(_update());
        foreach (var item in posibleSpawns)
        {
            var obj = Instantiate(building, item.transform.position, Quaternion.identity, this.transform);
            obj.transform.up = item.transform.position - transform.position;
            obj.planet = this;


            buildings.Add(obj);
        }
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward*0.05f);
    }
    IEnumerator _update()
    {
        while (true)
        {
            var random = Random.Range(0, 360);//GetRandomRange();

            var rect = image.rectTransform.rect;
            var pos = image.transform.position;

            Vector2 buildingPos = //transform.TransformDirection(//transforma la direccion de local a global
                new Vector2(
                pos.x + rect.x * Mathf.Cos(Mathf.Deg2Rad * random),
                pos.y + rect.y * Mathf.Sin(Mathf.Deg2Rad * random)
                );//normaliza la direccion y le aplica la magnitud del radio
            
            //var available = posibleSpawns.Where(x => !x.isUsed).ToArray();
            //
            //var spawn = available[Random.Range(0, available.Length)];
            //spawn.isUsed = true;
            //
            ////setea nuevo array con elemento usado
            //SetSpawnPoint(spawn);
            //
            //
            //
            var buildingObj= Instantiate(building, buildingPos, Quaternion.identity, this.transform);
            //
            //buildingObj.planet = this;
            //buildingObj.spawnPoint = spawn;

            yield return new WaitForSeconds(0.5f);
        }
    }
    //public void SetSpawnPoint(SpawnPoint spawn)
    //{
    //    posibleSpawns = posibleSpawns.Where(x => x.transform != spawn.transform).Append(spawn).ToArray();
    //}

    //procedural
    //public float GetRandomRange()
    //{
    //    while(true)
    //    {
    //        float num = Random.Range(0, 360)/*no incluyo 360 pq es igual a 0;*/;
    //        if (!avoidRanges.Where(x => {
    //
    //            var min = x.min + transform.eulerAngles.z;
    //            var max = x.max + transform.eulerAngles.z;
    //
    //            if (min > 360)
    //            {
    //                min -= 360;
    //            }
    //            if (max > 360)
    //            {
    //                max -= 360;
    //            }
    //            return num >= min && num <= max;
    //        }).Any())
    //            return num;
    //    }
    //}

    private void OnDrawGizmos()
    {
        if (!gizmos)
            return;

        var rect = image.rectTransform.rect;
        var pos = image.transform.position;
        
        Gizmos.color = Color.red;
        
        Gizmos.DrawSphere(new Vector2(
                pos.x + rect.x * Mathf.Cos(Mathf.Deg2Rad * deg),
                pos.y + rect.y * Mathf.Sin(Mathf.Deg2Rad * deg)
                ),10);

        Debug.Log(new Vector2(
                pos.x + rect.x * Mathf.Cos(Mathf.Deg2Rad * deg),
                pos.y + rect.y * Mathf.Sin(Mathf.Deg2Rad * deg)
                ));
    }
}
[System.Serializable]
public struct Range
{
    public int min;
    public int max;
}
