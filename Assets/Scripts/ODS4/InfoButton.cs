using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

public class InfoButton : InteractuableObject
{
    [SerializeField]
    [TextAreaAttribute(0,10)]
    string description;

    Transform descriptionTextArea;
    TextMeshProUGUI textObj;

    private void Start()
    {
        descriptionTextArea = GetComponentInChildren<UnityEngine.UI.RawImage>().transform;
        textObj = GetComponentInChildren<TextMeshProUGUI>();

        textObj.text = description;
        descriptionTextArea.localScale = Vector3.zero;

        scaleAnims = false;
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
        base.OnPointerEnter(eventData);

    }


}
