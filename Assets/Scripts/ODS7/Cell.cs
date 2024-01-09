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
    public bool locked, hasEnergy;
    public bool OnPressed { get => pressed; set { pressed = value; image.color = pressed ? cellColors.pressedColor : cellColors.normalColor; } }
    Grid grid { get => GameManagerODS7.gm.grid; }
    CellColors cellColors { get => GameManagerODS7.gm.grid.cellColors; }

    [HideInInspector] public CellSO cellSO;
    [HideInInspector] public Tuple<Cell,int>[] neighborhood;//Cell y por donde va a recibir la energia el sig: 0izq,1arr,2der,3aba
    public Tuple<int, int> pos;

    [HideInInspector] public int   startEnergy;//donde empieza
    int[] endEnergy;//donde tiene que terminar

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
        locked = true;
        hasEnergy = true;
        startEnergy = _start;
        var neighTargets = neighborhood.Where(x => x.Item2 != _start);//filtrar para los que ya tienen energia

        //temporal para testear
        StartCoroutine(navegating(neighTargets.ToArray()));
    }
    IEnumerator navegating(Tuple<Cell,int>[] neigh)
    {
        yield return new WaitForSeconds(10);
        foreach (var item in neigh)
        {
            Debug.Log("di energia a " + item.Item1.name);
            item.Item1.GetPowered(item.Item2);
        }
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

