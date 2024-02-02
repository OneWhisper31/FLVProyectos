using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EspacioEscolar : MonoBehaviour
{
    public string structureName;//usado para destacar cuando no se gastaron puntos en las misma

    public ODS4Buildings[] levelsImage;//tienen que ocuparse en orden

    //nivel 1 significa que no aumento nada y nivel 4 siginifica que la categoria esta al max
    public int CurrentLevel { get => 4 - levelsImage.Length; }

    public void LevelUpCategory()
    {
        var firstLevelImage = levelsImage.First();

        if (firstLevelImage.fadeType == 0)
            StartCoroutine(FadeIn(firstLevelImage.image));
        else
            StartCoroutine(FadeOut(firstLevelImage.image));

        levelsImage = levelsImage.Skip(1).ToArray();
    }

    IEnumerator FadeOut(Image image)
    {
        var color = image.color;
        color.a = 1;
        image.color = color;
        while (color.a >= 0.001f)
        {
            color.a = Mathf.Lerp(image.color.a, 0, 0.05f);
            image.color = color;
            yield return new WaitForSeconds(0.03f);
        }
        color.a = 0;
        image.color = color;
    }
    IEnumerator FadeIn(Image image)
    {
        var color = image.color;
        color.a = 0;
        image.color = color;

        while (color.a <= 0.998f)
        {
            color.a = Mathf.Lerp(image.color.a, 1, 0.05f);
            image.color = color;
            yield return new WaitForEndOfFrame();
        }
        color.a = 1;
        image.color = color;
    }
}
public enum FadeType
{
    FadeIn,
    FadeOut
}
[System.Serializable]
public struct ODS4Buildings
{
    public FadeType fadeType;
    public Image image;
}
