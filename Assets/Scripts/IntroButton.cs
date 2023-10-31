using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntroButton : InteractuableObject
{
    [SerializeField] Introduction intro;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (!interacteable)
            return;

        intro.OnNext();

    }
}
