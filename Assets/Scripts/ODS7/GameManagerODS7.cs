using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerODS7 : MonoBehaviour
{
    public static GameManagerODS7 gm;

    public GridODS7 grid;
    public CanvasHandlerODS7 canvasHandler;
    public EnergyChoose energyChoose;
    public Energy energy { get => energyChoose.energy; }

    public CellSO[] cellSprites;
    public CellSO battery;

    public float CellSpeed { get => cellSpeed; set {

            cellSpeed = value;
            foreach (var cell in grid.cells)
            {
                cell.anim.speed= value;
                //Debug.Log(cell.anim.speed);
            }
        
        } }
    float cellSpeed;

    public void SetCellSpeed(float speed) => CellSpeed = speed;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else
            Destroy(this.gameObject);
    }
}
