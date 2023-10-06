using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    public Sprite sprite;
    public Structure[] structures { get => GameManager.Instance.structures; }

    public bool hasChoose;

    public int structureSelected;

    private void Start()
    {
        image = GetComponent<Image>();
        ReRollStructure();
    }
    public void ReRollStructure()
    {
        structureSelected = Random.Range(0, structures.Length);
        image.sprite = structures[structureSelected].before;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        image.sprite = structures[structureSelected].after;
        hasChoose = true;
    }
}
[System.Serializable]
public struct Structure
{
    public Sprite before;
    public Sprite after;
}
