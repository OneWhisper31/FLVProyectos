using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class PointsSystem : MonoBehaviour
{
    public int numberOfGoingBack;


    [SerializeField] int _points;
    public int points { get=> _points; set { pointsText.text = value+""; _points = value; } }

    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] CanvasHandlerODS4 canvasHandler;

    [SerializeField] EspacioEscolar[] structuresLevelUp;
    [SerializeField] InfoButton[] buttons;

    private void Start()
    {
        points = _points;
    }
    public void EndGame()
    => StartCoroutine(_EndGame());
    IEnumerator _EndGame()
    {
        foreach (var button in buttons)
            button.Deactivated();

        yield return new WaitForSeconds(2);

        if (structuresLevelUp.Any(x => x.levelsImage.Length == 3))
        {
            //agarra los nombres de las estructuras que no tuvieron subida de nivel,
            string structures = structuresLevelUp
                .Where(x => x.levelsImage.Length == 3)
                .Aggregate("", (x, y) => x += y.structureName + ", ");

            //le saca la coma al ultimo lo cambia por un punto
            structures = structures.Substring(0, structures.Length - 2) + ".";

            //saca la ultima coma y agrega una Y y le pone el sobrante del texto
            int lastcommaPos = structures.LastIndexOf(",");
            if(lastcommaPos!=-1)
                structures = structures.Substring(0, lastcommaPos) + " Y" + structures.Substring(lastcommaPos + 1);

            canvasHandler.EndGame(structures);
        }
        else
            canvasHandler.EndGame();
    }
}
