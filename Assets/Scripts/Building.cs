using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    public Sprite sprite;

    public bool hasChoose;

    public Structure structureSelected;

    private void Start()
    {
        image = GetComponent<Image>();
        GameManager.Instance.ReRollStructure(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //image.sprite = structures[structureSelected].after;
        //hasChoose = true;
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
    public Sprite icon;
}

