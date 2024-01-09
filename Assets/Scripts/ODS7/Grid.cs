using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class Grid : MonoBehaviour
{
    public CellColors cellColors = new CellColors();

    [SerializeField] int height, width;
    [SerializeField] GameObject cell;
    [SerializeField] Transform pivot;

    Cell[] cells;

    [SerializeField] int maxBatteries;


    //posicion = i + j * width;
    private void Start()
    {
        int i = 0;
        int j = 0;

        //Range(min,max,ya tiene una bateria ese rango?)
        List<Tuple<int, int, bool,int>> batteriesRange = new List<Tuple<int, int, bool, int>>();

        //las inicializa poniendoles un rango de donde tendria que estar la bateria
        for (int x = 1; x <= maxBatteries; x++)
            batteriesRange.Add(Tuple.Create(
                (int)(((float)x - 1) / maxBatteries * height * width),//min
                (int)((float)x / maxBatteries * height * width), //max
                false,//ya tiene una bateria ese rango?
                -1));//columna(para comprobar que no esten en la misma)



        cells = new Cell[height * width].Select(x => {
            int pos = i + j * width;

            Cell _cell = Instantiate(cell, pivot.position + new Vector3(i * 57, j * 57, 0), Quaternion.identity, pivot)
                .GetComponent<Cell>().SetValue();
            _cell.name += pos;//diferenciacion

            if ((i == 0 && j == 1) || (i > width - 2 && j == 1))
            {//initial cells
                _cell.SetValue(GameManagerODS7.gm.cellSprites[0], true);
            }
            else
            {
                //rangos en los que todavia no haya una bateria
                for (int y = 0; y < batteriesRange.Count; y++)
                {
                    if (batteriesRange[y].Item3)
                        continue;//si ya es verdadero que siga de largo


                    //si entra en el rango
                    if (pos > batteriesRange[y].Item1 && pos <= batteriesRange[y].Item2)
                    {//(maxRange-pos)/maxRange*100 ej: (16-4)/16*100==75% de chances que no salga
                        float probabilty = (float)(batteriesRange[y].Item2 - pos) / batteriesRange[y].Item2 * 100;
                        if (Random.Range(0, 101) >= probabilty)
                        { 
                            //si coinciden en columna que busque otra
                            if(batteriesRange.Any(x => x.Item4 == i) || i<=1||i>width-3) continue;

                            //setea la bateria y se declara verdadera
                            _cell.SetBattery();
                            batteriesRange[y] = Tuple.Create(batteriesRange[y].Item1, batteriesRange[y].Item2, true,i);
                        }
                    }
                }
            }
            i++;
            if (i > width - 1)
            {
                i = 0;
                j++;
            }

            return _cell;

        }).ToArray();
    }
    public void CheckChange()
    {
        var cellsPressed = cells.Where(x => x.OnPressed);

        if (cellsPressed.Count() >= 2)
        {

            var cell1Position = cellsPressed.First().transform.localPosition;
            var cell2Position = cellsPressed.Last().transform.localPosition;


            cellsPressed.First().transform.localPosition = cell2Position;
            cellsPressed.Last().transform.localPosition = cell1Position;

            foreach (var item in cellsPressed)
                item.OnPressed = false;
        }
    }
    public void CheckNeighborhood(int i, int y)
    {

    }
}
