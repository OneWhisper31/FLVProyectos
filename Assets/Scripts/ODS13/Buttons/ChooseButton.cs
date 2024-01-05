using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ChooseButton : InteractuableObject
{
    [SerializeField] Change value;

    //[SerializeField]
    //[TextArea(0, 10)]
    string description;

    Transform descriptionTextArea;
    TextMeshProUGUI textObj;

    private void Start()
    {
        descriptionTextArea = GetComponentInChildren<RawImage>().transform;
        textObj = GetComponentInChildren<TextMeshProUGUI>();

        textObj.text = description;
        descriptionTextArea.localScale = Vector3.zero;

        scaleAnims = false;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (!Interacteable)
            return;

        switch (value)
        {
            case Change.Close:
                GameManager.Instance.OptionPanel.DisableOptions();
                break;
            case Change.Positive1:
            case Change.Positive2:
            case Change.Negative:
                GameManager.Instance.OptionPanel.ChangeValue(value);
                GameManager.Instance.OptionPanel.DisableOptions();
                break;
            default:
                break;
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        descriptionTextArea.DOScale(1, 0.5f);
        base.OnPointerEnter(eventData);

    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        descriptionTextArea.DOScale(0, 0.5f);
        base.OnPointerExit(eventData);

    }

    public void SetChoice(Tuple<Change,SpriteStructure> structureSelected)
    {
        value = structureSelected.Item1;
        image.sprite = structureSelected.Item2.icon;

        description = structureSelected.Item2.hoverText;
        textObj.text = description;
    }
}
public enum Change
{
    Close,
    Positive1,
    Positive2,
    Negative
}
