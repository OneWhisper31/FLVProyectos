using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    [SerializeField] Transform[] posibleSpawns;
    [SerializeField] Building building;

    [SerializeField] [Range(0, 10)] float speed;

    [Header("Debug Mode")]
    [SerializeField] bool debugMode;
    [Range(0,360)] [SerializeField] int degrees;//Debugmode


    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        GameManager.Instance.ShuffleStructures();

        foreach (var item in posibleSpawns)
        {
            var obj = Instantiate(building, item.transform.position, Quaternion.identity, this.transform);
            obj.transform.up = item.transform.position - transform.position;
        }
    }
    private void Update()
    {
        if (GameManager.Instance.pauseMode)
            return;

        transform.Rotate(Vector3.forward * speed*Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (!debugMode)
            return;

        var rect = image.rectTransform.rect;
        var pos = image.transform.position;

        Gizmos.color = Color.red;

        Gizmos.DrawSphere(new Vector2(
                pos.x + rect.x * Mathf.Cos(Mathf.Deg2Rad * degrees),
                pos.y + rect.y * Mathf.Sin(Mathf.Deg2Rad * degrees)
                ), 10);

        Debug.Log(new Vector2(
                pos.x + rect.x * Mathf.Cos(Mathf.Deg2Rad * degrees),
                pos.y + rect.y * Mathf.Sin(Mathf.Deg2Rad * degrees)
                ));
    }
}



















    //IEnumerator _update()
    //{
    //    while (true)
    //    {
    //        var random = Random.Range(0, 360);//GetRandomRange();
    //
    //        var rect = image.rectTransform.rect;
    //        var pos = image.transform.position;
    //
    //        Vector2 buildingPos = //transform.TransformDirection(//transforma la direccion de local a global
    //            new Vector2(
    //            pos.x + rect.x * Mathf.Cos(Mathf.Deg2Rad * random),
    //            pos.y + rect.y * Mathf.Sin(Mathf.Deg2Rad * random)
    //            );//normaliza la direccion y le aplica la magnitud del radio
    //        
    //        //var available = posibleSpawns.Where(x => !x.isUsed).ToArray();
    //        //
    //        //var spawn = available[Random.Range(0, available.Length)];
    //        //spawn.isUsed = true;
    //        //
    //        ////setea nuevo array con elemento usado
    //        //SetSpawnPoint(spawn);
    //        //
    //        //
    //        //
    //        var buildingObj= Instantiate(building, buildingPos, Quaternion.identity, this.transform);
    //        //
    //        //buildingObj.planet = this;
    //        //buildingObj.spawnPoint = spawn;
    //
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //}
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
//[Serializable]
//public struct Range
//{
//    public int min;
//    public int max;
//}
