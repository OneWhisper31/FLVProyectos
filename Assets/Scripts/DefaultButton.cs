using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DefaultButton : InteractuableObject
{
    [SerializeField] UnityEvent onClick;

    [SerializeField] bool initialInteractable, initialDisableAnims, initialScaleAnims;

    private void Start()
    {
        Interacteable = initialInteractable;
        disableAnims= initialDisableAnims;
        scaleAnims = initialScaleAnims;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (!Interacteable)
            return;

        onClick?.Invoke();
    }
}
