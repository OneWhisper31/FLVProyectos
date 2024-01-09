using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler
{
    public bool locked;
    public bool OnPressed { get => pressed; set { pressed = value; image.color = pressed ? cellColors.pressedColor : cellColors.normalColor; } }
    Grid grid { get => GameManagerODS7.gm.grid; }
    CellColors cellColors { get => GameManagerODS7.gm.grid.cellColors; }

    [HideInInspector] public CellSO cellSO;

    Image image;
    bool pressed;
    bool solved;


    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public Cell SetValue(CellSO _typeOfSprite=null, bool isVisible =false)
    {
        cellSO = (_typeOfSprite == null ? GameManagerODS7.gm.cellSprites[Random.Range(0, GameManagerODS7.gm.cellSprites.Length-1)]: null);

        if (isVisible)
        {
            solved = true;
            locked = true;

            if (cellSO == null)
                cellSO = GameManagerODS7.gm.cellSprites[0];

            image.sprite = cellSO.sprite;
        }

        return this;
    }
    public Cell SetBattery()
    {
        solved = true;
        locked = true;

        cellSO = GameManagerODS7.gm.battery;
        image.sprite = cellSO.sprite;

        return this;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (locked)
            return;

        if (solved)
        {
            OnPressed = !OnPressed;
            grid.CheckChange();
        }
        else
        {
            solved = true;

            if (cellSO ==null)
                cellSO = GameManagerODS7.gm.cellSprites[0];

            image.sprite = cellSO.sprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (locked)
            return;

        if (!OnPressed)
            image.color = cellColors.highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (locked)
            return;

        if (!OnPressed)
            image.color = cellColors.normalColor;
    }
}

[System.Serializable]
public struct CellColors
{
    public Color normalColor;
    public Color highlightColor;
    public Color pressedColor;
}

