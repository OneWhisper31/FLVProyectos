using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseButton : InteractuableObject
{
    [SerializeField] Change value;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (!interacteable)
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
}
public enum Change
{
    Close,
    Positive1,
    Positive2,
    Negative
}
