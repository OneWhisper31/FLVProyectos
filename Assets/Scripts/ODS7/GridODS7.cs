using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class GridODS7 : MonoBehaviour
{
    public CellColors cellColors = new CellColors();

    [SerializeField] int height, width;
    [SerializeField] GameObject cell;
    [SerializeField] Transform pivot;


    public Cell[] cells { get; private set; }
    public Cell LastCell { get => width - 1 + 1 * width < cells.Length ? cells[width - 1 + 1 * width] : null; }
    public bool CellsAnyGivingEnergy { get => cells.Any(x => x.isGivingPower); }

    [SerializeField] int maxBatteries;
    public int batteriesPowered;

    public bool HasPoweredAllBateries { get => batteriesPowered >= maxBatteries; }
    public bool IsEnergyRenovable { get => GameManagerODS7.gm.energyChoose.energy.EnergySO.isRenovable; }
    public TypeOfEnergy TypeOfEnergy { get => GameManagerODS7.gm.energyChoose.energy.EnergySO.typeOfEnergy; }

    [Header("Speed UI")]
    [SerializeField] DefaultButton[] defaultButtons;
    //public Sprite hideTubeSprite;


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

            Cell _cell 
            = Instantiate(cell, pivot.position + new Vector3(i *Screen.width*0.065f, j * Screen.height * 0.115f, 0), Quaternion.identity, pivot)
                .GetComponent<Cell>().SetValue();
            _cell.name += pos;//diferenciacion
            _cell.pos = Tuple.Create(i, j);//le da su posicion
            
            if ((i == 0 && j == 1)|| (i > width - 2 && j == 1))
            {//start cell or end cell
                //_cell.endEnergy = new int[] {2};//termina en derecha
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

        //una vez que esta establecido el array, chequea vecinos
        foreach (var _cell in cells)
            _cell.neighborhood = CheckNeighborhood(_cell.pos.Item1,_cell.pos.Item2);
    }
    public void StartGame()
    {//llamado cuando elije la energia, que da inicio al juego dando energia

        cells[11].GetPowered(0);//arranca la energia por la izq
    }
    public void StopEnergy()//funcion usada para que se deje de dar energia en todas las celdas pq o se gano o se perdio
    {
        foreach (Cell _cell in cells)
        {
            _cell.isGivingPower = false;
            _cell.FinishLevelCell();
            _cell.anim.speed=0;
        }
    }
    public void CheckChange()
    {
        var cellsPressed = cells.Where(x => x.OnPressed);

        if (cellsPressed.Count() >= 2)
        {

            //recalcula los cambiados
            //first.neighborhood = CheckNeighborhood(first.pos.Item1, first.pos.Item2);
            //last.neighborhood = CheckNeighborhood(last.pos.Item1, last.pos.Item2);

            var first = cellsPressed.First();
            var last = cellsPressed.Last();

            //guarda referencia del SO
            var firstSO = first.cellSO;
            var lastSO = last.cellSO;

            first.cellSO = lastSO;
            last.cellSO  = firstSO;

            foreach (var item in cellsPressed)
                item.OnPressed = false;

            //recalcular tablero || esto deberia optimizarse en caso de problemas de rendimiento
            RecalculateNeighborhood();
        }
    }

    public void RecalculateNeighborhood()
    {
        foreach (var item in cells)
            item.neighborhood = CheckNeighborhood(item.pos.Item1, item.pos.Item2);
    }
    public Tuple<Cell,int>[] CheckNeighborhood(int i, int y)
    {
        List<Tuple<Cell, int>> neighborhoodCells = new List<Tuple<Cell,int>> ();

        Cell _cell = cells[i + y * width];
        //neighborchecker se encarga de hacer las comprobaciones
        if (NeighborhoodChecker(_cell,i - 1 + y * width,0,2))
            neighborhoodCells.Add(Tuple.Create(cells[i - 1 + y * width],2));
        if(NeighborhoodChecker(_cell,i + (y + 1) * width,1,3))
            neighborhoodCells.Add(Tuple.Create(cells[i + (y + 1) * width],3));
        if (NeighborhoodChecker(_cell, i + 1 + y * width, 2, 0))
            neighborhoodCells.Add(Tuple.Create(cells[i + 1 + y * width],0));
        if (NeighborhoodChecker(_cell, i + (y - 1) * width, 3, 1))
            neighborhoodCells.Add(Tuple.Create(cells[i + (y - 1) * width],1));

        return neighborhoodCells.ToArray();
    }


    /// <summary>
    /// las comprobaciones para saber si son vecinos o no son las siguientes
    ///  el indice es menor a 0? el indice es mayor a el tama�o del array?
    ///  Tiene una abertura disponible?el vecino tiene abertura disponible?
    ///  es visible? tiene energia?
    ///  esta en distinta fila pero tiene misma columna?(para que no de energia del otro lado del tablero)
    /// <returns></returns>
    /// </summary>
    /// <param name="_cell">celda con energia</param>
    /// <param name="index">indice de la celda con energia</param>
    /// <param name="arrayEnterEnergy">tiene lugares por donde salir la energia?(celda con energia)</param>
    /// <param name="otherArrayEnterEnergy">tiene lugares por donde salir la energia?(celda sin energia)</param>
    /// <returns></returns>
    bool NeighborhoodChecker(Cell _cell, int index, int arrayEnterEnergy, int otherArrayEnterEnergy)
        => index >= 0 && index < height * width &&
        _cell.cellSO.enterEnergy[arrayEnterEnergy] && cells[index].cellSO.enterEnergy[otherArrayEnterEnergy]
        && cells[index].visible && !cells[index].hasEnergy
        &&//son de la misma Y o son de distinta Y pero misma X
        ((_cell.pos.Item2== cells[index].pos.Item2)
        ||(_cell.pos.Item2 != cells[index].pos.Item2&& _cell.pos.Item1 == cells[index].pos.Item1));



    //speed ui funciones
    public void InteractuableHandler(int x,bool choosenState)
    {
        for (int i = 0; i < defaultButtons.Length; i++)
        {
            if (i == x) defaultButtons[i].Interacteable = choosenState;
            else defaultButtons[i].Interacteable = !choosenState;
        }
    }
    public void DisableAllExcept(int x) => InteractuableHandler(x,true);
    public void EnableAllExcept(int x) => InteractuableHandler(x, false);
    public void DisableAll() { foreach (var item in defaultButtons) item.Interacteable = false; } 
}