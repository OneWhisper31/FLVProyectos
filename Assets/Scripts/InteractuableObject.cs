using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public abstract class InteractuableObject : MonoBehaviour, IPointerUpHandler, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isAffectedByPauseMode=true;

    bool interacteable = true;
    [SerializeField]public bool Interacteable
    {
        get => interacteable;
        set
        {
            image.color = value ? Color.white : Color.gray;
            interacteable = value;
        }
    }

    protected bool disableAnims;

    protected bool scaleAnims=true;

    public Image image;
    protected Vector3 initialScale;



    private void Awake()
    {
        image = GetComponent<Image>();
        initialScale = transform.localScale;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        StopAllCoroutines();

        if (disableAnims||!interacteable)
            return;

        image.color = Color.gray;
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        StopAllCoroutines();

        if (disableAnims || !interacteable)
            return;

        image.color = Color.white;
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();

        if (disableAnims||!interacteable)
            return;

        if(scaleAnims)
            transform.localScale *= 1.1f;

        StartCoroutine(AnimAssetColor());
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();

        if (disableAnims|| !interacteable)
            return;

        if (scaleAnims)
            transform.localScale = initialScale;

        image.color = Color.white;
    }
    IEnumerator AnimAssetColor()
    {
        float numberLerp;
        Color color = image.color;

        while (true)
        {
            numberLerp = Mathf.PingPong(Time.time*2, 1)*0.2f+0.8f;

            color.r = numberLerp;
            color.g = numberLerp;
            color.b = numberLerp;

            image.color = color;

            yield return new WaitForEndOfFrame();
        }
    } 

    //action
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.Instance!=null)
            if (GameManager.Instance.pauseMode)
                if(isAffectedByPauseMode)
                    return;

        if (!interacteable)
            return;

        if (scaleAnims)
            transform.localScale = initialScale;

        StopAllCoroutines();
    }
    
}
