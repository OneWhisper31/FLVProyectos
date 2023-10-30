using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Raylight : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyRay());
    }

    public IEnumerator DestroyRay()
    {
        yield return new WaitForSeconds(Random.Range(5,10));

        if (GameManager.Instance.pauseMode)
        {
            yield return new WaitUntil(() => !GameManager.Instance.pauseMode);
            yield return new WaitForSeconds(Random.Range(5, 10));
        }

        transform.DOScale(0,2).OnComplete(()=>Destroy(this.gameObject));
    }
}
