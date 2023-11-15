using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using LeverEnum;
using UnityEngine.UI;
using TMPro;

public class LeverButton : InteractuableObject
{
    Lever nextState = Lever.Up;
    bool direction;//false down positive up

    [SerializeField] TextMeshProUGUI textCauses;

    [SerializeField] Sprite downSprite, neutralSprite, upSprite;

    [SerializeField] UnityEvent down, neutral, up;

    private void Start()
    {
        scaleAnims = false;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        switch (nextState)
        {
            case Lever.Down:
                down?.Invoke();
                image.sprite = downSprite;
                nextState = Lever.Neutral;
                direction = true;
                break;
            case Lever.Neutral:
                neutral?.Invoke();
                image.sprite = neutralSprite;

                nextState = direction ? Lever.Up : Lever.Down;
                break;
            case Lever.Up:
                up?.Invoke();
                image.sprite = upSprite;
                nextState = Lever.Neutral;
                direction = false;
                break;
            default:
                break;
        }
    }
    public void AddText(string text)
    {
        if (textCauses.text == "")
            textCauses.text = text;
        else
            textCauses.text += "\n \n" + text;
    }
}
namespace LeverEnum
{
    public enum Lever
    {
        Down,
        Neutral,
        Up
    }
}
