using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class Building : InteractuableObject
{
    public Structure structureSelected;

    bool hasChange;

    public int value=-1;//es negativo o positivo? || puede ser -1 o 1 o 2

    [SerializeField] Animator smokeAnim;

    private void Start()
    {
        GameManager.Instance.ReRollStructure(this);
        transform.DOScale(1, 0.2f);
        scaleAnims = false;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (!Interacteable)
            return;

        if (!hasChange)//si no cambio todavia de estructura
            GameManager.Instance.OptionPanel.EnableOptions(this);
    }
    public void ChangeValue(Change _value)
    {
        hasChange = true;
        disableAnims = true;//interactuableObject

        smokeAnim.SetTrigger("play" + Random.Range(0, 2));

        Sprite newSprite = default;

        switch (_value)
        {
            case Change.Positive1:
                value = 1;
                newSprite= structureSelected.positive1.sprite;
                break;
            case Change.Positive2:
                newSprite = structureSelected.positive2.sprite;
                value = 2;
                break;
            case Change.Negative:
                newSprite = structureSelected.negative.sprite;
                value = -1;
                break;
            default:
                break;
        }


        transform.DOScale(0, 0.2f).OnComplete(()=> {

            image.sprite = newSprite;
            transform.DOScale(1, 0.3f);
        });

        
    }
}
[System.Serializable]
public struct Structure
{
    public Sprite initial;//una cruz en todas
    public SpriteStructure positive1;
    public SpriteStructure positive2;
    public SpriteStructure negative;
}
[System.Serializable]
public struct SpriteStructure
{
    public Sprite sprite;
    //public Sprite icon;
}

