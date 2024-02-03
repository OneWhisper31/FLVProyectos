using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

public class InfoButton : InteractuableObject
{
    [SerializeField]
    [TextArea(0, 10)]
    string description;

    [HideInInspector]public Transform descriptionTextArea;
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

    public UnityEvent OnClick;

    private void Start()
    {
        descriptionTextArea = GetComponentInChildren<RawImage>().transform;
        textObj = GetComponentInChildren<TextMeshProUGUI>();

        textObj.text = description;
        descriptionTextArea.localScale = Vector3.zero;

        scaleAnims = false;
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if(Interacteable)
            descriptionTextArea.DOScale(1, 0.5f);

        base.OnPointerEnter(eventData);

    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();

        if (Interacteable)
            descriptionTextArea.DOScale(0, 0.5f);

        base.OnPointerExit(eventData);

    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!Interacteable)
            return;

        base.OnPointerClick(eventData);
        StopAllCoroutines();


        if (pointsSystem.points >= 710 && level <= 3)
        {
            pointsSystem.points -= 710;
            level++;
            OnClick?.Invoke();
        }

        if (pointsSystem.points <= 700)
        {
            pointsSystem.EndGame();
        }
    }
    public void Deactivated()
    {

        descriptionTextArea.DOScale(0, 0.5f);
        Interacteable = false;
    }

}
