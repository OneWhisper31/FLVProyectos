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
    public TextMeshProUGUI textObj;

    int _level = 0;//max level2(level3)
    public int level {get=>_level;
        set {
            levelSlider.fillAmount = value / 3f; 
            _level = value; 
        } 
    }

    [SerializeField]Image levelSlider;
    [SerializeField] PointsSystem pointsSystem;

    public UnityEvent OnClick;
    public UnityEvent OnLeftClick;

    private void Start()
    {
        descriptionTextArea = GetComponentInChildren<RawImage>().transform;

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

        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (pointsSystem.points >= 710 * level && level < 3)
            {
                pointsSystem.points -= 710 * level;
                level++;
                OnClick?.Invoke();
            }

            if (pointsSystem.points < 710)
            {
                pointsSystem.EndGame();
            }
        }
        else if (pointsSystem.numberOfGoingBack > 0 && eventData.button == PointerEventData.InputButton.Right)
        {
            if (level > 0)
            {
                pointsSystem.points += 710 * level;
                level--;
                OnLeftClick?.Invoke();
                pointsSystem.numberOfGoingBack--;
            }
        }

    }
    public void Deactivated()
    {

        descriptionTextArea.DOScale(0, 0.5f);
        Interacteable = false;
    }

}
