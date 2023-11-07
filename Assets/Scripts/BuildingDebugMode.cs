using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class BuildingDebugMode : InteractuableObject
{
    public Structure structureSelected;

    public int value=-1;//es negativo o positivo? || puede ser -1 o 1 o 2

    [SerializeField] Animator smokeAnim;

    private void Start()
    {
        GameManager.Instance.ReRollStructure(this);
        transform.DOScale(1, 0.2f);
        scaleAnims = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            GameManager.Instance.ReRollStructure(this);
        else if (Input.GetKeyDown(KeyCode.Q))
            image.sprite= structureSelected.initial;
        else if(Input.GetKeyDown(KeyCode.W))
            image.sprite = structureSelected.positive1.sprite;
        else if (Input.GetKeyDown(KeyCode.E))
            image.sprite = structureSelected.positive2.sprite;
        else if (Input.GetKeyDown(KeyCode.R))
            image.sprite = structureSelected.negative.sprite;
    }
}

