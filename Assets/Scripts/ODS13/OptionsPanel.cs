using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class OptionsPanel : MonoBehaviour
{
    Building buildingSelected;
    public Building CurrentBuilding { get => buildingSelected; }

    public bool IsOpen { get => transform.localScale == Vector3.one;}

    [Header("Option Panel variables")]
    [SerializeField] ChooseButton[] buttons;

    public void EnableOptions(Building _buildingSelected)
    {
        if (IsOpen)
            return;

        buildingSelected = _buildingSelected;
        GameManager.Instance.pauseMode = true;
        transform.DOScale(1, 0.7f);


        StructureSO option = _buildingSelected.structureSelected;
        List<Tuple<Change, SpriteStructure>> optionList = new List<Tuple<Change, SpriteStructure>>();
        //le paso el tipo de opcion que es
        optionList.Add(Tuple.Create(Change.Close, option.initial));
        optionList.Add(Tuple.Create(Change.Positive1, option.positive1));
        optionList.Add(Tuple.Create(Change.Positive2, option.positive2));
        optionList.Add(Tuple.Create(Change.Negative, option.negative));


        int prevCount = optionList.Count;
        for (int i = 0; i < prevCount; i++)
        {//setea el boton a la opcion mezclada
            var randomNum = Random.Range(0, optionList.Count);
            buttons[i].SetChoice(optionList[randomNum]);
            optionList.RemoveAt(randomNum);
        }
    }
    public void DisableOptions()
    {
        GameManager.Instance.pauseMode = false;
        buildingSelected = null;
        transform.DOScale(0, 0.7f);
    }
    public void ChangeValue(Change value)
    {
        buildingSelected.ChangeValue(value);
    }

}
