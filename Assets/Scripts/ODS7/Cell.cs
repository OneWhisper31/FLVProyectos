using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Random = UnityEngine.Random;
using System.Linq;

public class Cell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler
{
    public bool locked, hasEnergy, visible, isGivingPower;
    public bool OnPressed { get => pressed; set { pressed = value; image.color = pressed ? cellColors.pressedColor : cellColors.normalColor; } }
    Grid grid { get => GameManagerODS7.gm.grid; }
    CellColors cellColors { get => GameManagerODS7.gm.grid.cellColors; }

    public CellSO cellSO;
    [HideInInspector] public Tuple<Cell,int>[] neighborhood;//Cell y por donde va a recibir la energia el sig: 0izq,1arr,2der,3aba
    public Tuple<int, int> pos;

    [HideInInspector] public int   startEnergy;//donde empieza

    Image image;
    bool pressed;


    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public Cell SetValue(CellSO _typeOfSprite=null, bool _isVisible =false)
    {
        cellSO = (_typeOfSprite == null ? GameManagerODS7.gm.cellSprites[Random.Range(0, GameManagerODS7.gm.cellSprites.Length)]: null);

        if (_isVisible)
        {
            visible = true;
            locked = true;

            if (cellSO == null)
                cellSO = GameManagerODS7.gm.cellSprites[0];

            image.sprite = cellSO.sprite;
        }

        return this;
    }
    public Cell SetBattery()
    {
        visible = true;
        locked = true;

        cellSO = GameManagerODS7.gm.battery;
        image.sprite = cellSO.sprite;

        //neighborhood= GameManagerODS7.gm.CheckNeighborhood(pos.Item1, pos.Item2);

        return this;
    }
    public void SetNewSO(CellSO so)
    {
        cellSO = so;
        image.sprite = cellSO.sprite;
    }


    public void GetPowered(int _start)
    {
        if (!visible)
            return;
        if (hasEnergy)
            return;

        image.color = cellColors.normalColor;
        locked = true;
        hasEnergy = true;
        isGivingPower = true;
        startEnergy = _start;

        //temporal para testear
        StartCoroutine(navegating());
    }
    IEnumerator navegating()
    {
        yield return new WaitForSeconds(5);
        isGivingPower = false;
        Debug.Log("intento dar energia");

        //si tiene que dar energia y no tiene vecinos que pregunte si hay otro que este dando energia(si no periste)
        if (neighborhood.Length <= 0 && !GameManagerODS7.gm.grid.CellsAnyGivingEnergy)
        {
            Debug.Log("Perdiste");
            StopAllCoroutines();
            yield break;
        }
            
        //da energia a vecinos y comprueba si llegaste al objetivo
        foreach (var item in neighborhood)
        {
            Debug.Log("di energia a " + item.Item1.name);

            if (item.Item1 == GameManagerODS7.gm.grid.LastCell)
            {
                Debug.Log("Ganaste");
                foreach (var _item in GameManagerODS7.gm.grid.cells)
                {
                    _item.FinishLevelCell();
                }
                
                StopAllCoroutines();
                yield break;
            }

            item.Item1.GetPowered(item.Item2);
        }

        //recalcula vecinos para que no vuelva a llenar energias llenas
        GameManagerODS7.gm.grid.RecalculateNeighborhood();

    }
    public void SetVisible()
    {
        visible = true;
        image.sprite = cellSO.sprite;
    }
    public void SetLocked()
    {
        locked = true;
    }
    public void FinishLevelCell()
    {
        SetVisible();
        SetLocked();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (locked)
            return;

        if (visible)
        {
            OnPressed = !OnPressed;
            grid.CheckChange();
        }
        else
        {
            visible = true;

            GameManagerODS7.gm.grid.RecalculateNeighborhood();

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

