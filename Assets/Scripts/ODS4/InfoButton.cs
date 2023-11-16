using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class InfoButton : InteractuableObject
{
    [SerializeField]
    [TextAreaAttribute(0, 10)]
    string description;

    Transform descriptionTextArea;
    TextMeshProUGUI textObj;

    int _level = 1;//max level2(level3)
    public int level {get=>_level;
        set { 
            float _value = Mathf.Clamp01(value); 
            levelSlider.fillAmount += _value / 3; 
            _level += (int)_value; 
        } 
    }

    [SerializeField]Image levelSlider;
    [SerializeField] PointsSystem pointsSystem;

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
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        StopAllCoroutines();

        if (pointsSystem.points >= 710&&level<=3)
        {
            pointsSystem.points -= 710;
            level++;
        }
    }


}
