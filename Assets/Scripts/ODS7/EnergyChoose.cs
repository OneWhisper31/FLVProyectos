using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EnergyChoose : MonoBehaviour
{
    public EnergySO[] energies;
    public int index;

    public Energy energy;
    public Image selectorImage;
    public Image car;
    public Transform gridPivot;
    public Canvas canvasGrid;

    public Transform EnergyGameplay;

    private void Start()
    {
        selectorImage.sprite = energies[index].initialSprite;
        energy.EnergySO = energies[index];
    }

    public void StartGame()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            child.DOScale(0, 0.3f)
                .OnComplete(() => child.gameObject.SetActive(false));
        }
        EnergyGameplay.DOScale(1, 0.3f);

        gridPivot.localScale = Vector3.zero;
        canvasGrid.sortingOrder = 15;
        gridPivot.DOScale(1, 0.3f);
    }
    void ChangeSelectedEnergy(int _index)
    {
        index = Mathf.Clamp(_index,0, energies.Length-1);
        selectorImage.sprite = energies[index].initialSprite;
        car.sprite = energies[index].car;
        energy.EnergySO = energies[index];
    }
    public void AddSelectedEnergy()=>
        ChangeSelectedEnergy(index + 1>=energies.Length? 0 : index + 1);
    public void SubstractSelectedEnergy()=>
        ChangeSelectedEnergy(index - 1 < 0 ? energies.Length-1 : index - 1);
}
