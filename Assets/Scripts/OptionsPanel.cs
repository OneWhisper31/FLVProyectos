using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OptionsPanel : MonoBehaviour
{
    Building buildingSelected;

    public bool IsOpen { get => transform.localScale == Vector3.one;}

    public void EnableOptions(Building _buildingSelected)
    {
        buildingSelected = _buildingSelected;

        if(!IsOpen)
            transform.DOScale(1, 0.7f);
    }
    public void DisableOptions()
    {
        buildingSelected = null;
        transform.DOScale(0, 0.7f);
    }
    public void ChangeValue(Change value)
    {
        buildingSelected.ChangeValue(value);
    }

}
