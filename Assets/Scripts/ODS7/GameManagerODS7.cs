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

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else
            Destroy(this.gameObject);
    }
}
