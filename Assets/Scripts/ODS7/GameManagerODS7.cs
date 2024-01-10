using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerODS7 : MonoBehaviour
{
    public static GameManagerODS7 gm;

    public Grid grid;

    public CellSO[] cellSprites;
    public CellSO battery;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else
            Destroy(this.gameObject);
    }
    //public void RecalculateNeighborhood()
    //=> grid.RecalculateNeighborhood();
}
