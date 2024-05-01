using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DayAndNightManager : MonoBehaviour
{
 private static DayAndNightManager instance;

    [Header("Gradients")] 
    [SerializeField] private Gradient fogGradient;
    [SerializeField] private Gradient ambientGradient;
    [SerializeField] private Gradient directionalLightGradient;
    [SerializeField] private Gradient skyboxTintGradient;

    [Header("Enviromental Assets")] 
    [SerializeField] private Light directionalLight;
    [SerializeField] private Material skyboxMaterial;

    [Header("Variables")] 
    [SerializeField] private float dayDurationInSeconds = 60f;
    [SerializeField] private float rotationSpeed = 1f;

    private float currentTime = 0;

    private float timeScale = 1f;

    private string savedTime, currentTimeText;

    [SerializeField] private TextMeshProUGUI timeText;

    private DayAndNightManager()
    {
        instance = this;
    }
    
    public static DayAndNightManager Instance 
    {
        get {
            if(instance==null) 
            {
                instance = new DayAndNightManager();
            }
 
            return instance;
        }
    }
    
    private void Update()
    {
        UpdateTime();
        UpdateDayNightCycle();
        RotateSkybox();

        if (savedTime == currentTimeText)
        {
            SetTimeScale(30f);
        }
    }

    private void UpdateTime()
    {
        currentTime += (Time.deltaTime / dayDurationInSeconds) / timeScale; // Adjusting time scale
        currentTime = Mathf.Repeat(currentTime, 1f);

        // Display time
        currentTimeText = GetCurrentTime();
        timeText.text = currentTimeText;
    }

    private void UpdateDayNightCycle()
    {
        float sunPosition = Mathf.Repeat(currentTime + 0.25f, 1f);
        directionalLight.transform.rotation = quaternion.Euler(sunPosition = 360f, 0f, 0f);

        RenderSettings.fogColor = fogGradient.Evaluate(currentTime);
        RenderSettings.ambientLight = ambientGradient.Evaluate(currentTime);

        directionalLight.color = directionalLightGradient.Evaluate(currentTime);
        
        skyboxMaterial.SetColor("_Tint", skyboxTintGradient.Evaluate(currentTime));
    }

    private void RotateSkybox()
    {
        float currentRotation = skyboxMaterial.GetFloat("_Rotation");
        float newRotation = currentRotation + (rotationSpeed / timeScale) * Time.deltaTime;
        newRotation = Mathf.Repeat(newRotation, 360f);
        skyboxMaterial.SetFloat("_Rotation", newRotation);
    }

    // Get rid of this when getting build
    private void OnApplicationQuit()
    {
        skyboxMaterial.SetColor("_Tint", new Color(0.5f, 0.5f, 0.5f));
    }

    private string GetCurrentTime()
    {
        // Calculate hours and minutes
        int hours = Mathf.FloorToInt(24 * currentTime);
        int minutes = Mathf.FloorToInt(60 * (24 * currentTime - hours));
        
        // Convert to AM/PM format
        string timeOfDay = "PM";
        if (hours >= 12)
        {
            timeOfDay = "AM";
            if (hours > 12)
            {
                hours -= 12;
            }
        }
        if (hours == 0)
        {
            hours = 12;
        }
        
        return hours.ToString("00") + ":" + minutes.ToString("00") + " " + timeOfDay;
    }
    public void SetTimeScale(float newScale)
    {
        timeScale = newScale;

        savedTime = GetCurrentTime();
    }

    public float GetTimeScale()
    {
        return timeScale;
    }
}
