using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    [Header("Years variables")]
    [SerializeField] int yearsInitial=2023;
    [SerializeField] int yearsFinal = 2030;
    [SerializeField] int gametimeSeconds = 300;
    [SerializeField] TextMeshProUGUI years;

    int secondsPerYear;
    int currentYear;
    float currentTime;

    [Header("Temperature variables")]
    [SerializeField] Slider temperature;

    //[SerializeField] float totalTemperature;

    private void Start()
    {
        secondsPerYear = gametimeSeconds / (yearsFinal - yearsInitial);
        currentYear = yearsInitial;
        years.text = currentYear+"";
    }
    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= secondsPerYear)
        {
            if (currentYear < yearsFinal)
                UpdateYears();
            else
                EndGame();
        }
    }
    public void UpdateYears()
    {
        currentYear++;
        currentTime = 0;
        years.text = currentYear + "";

        if (currentYear == yearsFinal)
            EndGame();
    }
    public void EndGame()
    {
        GameManager.Instance.pauseMode = true;
        currentTime = 0;
        Time.timeScale = 0;
    }
    public void UpdateTemperature(float number)=>
        temperature.value = Mathf.Clamp(number + temperature.value, 0, 1);
}
