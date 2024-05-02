using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private float fps;
    private float updateTimer = 0.2f;
    private bool shouldShouldUI = false;

    [SerializeField] private GameObject textHolder;
    [SerializeField] private TextMeshProUGUI fpsText;

    private void UpdateFPSDisplay()
    {
        updateTimer -= Time.deltaTime;

        if (updateTimer <= 0f)
        {
            fps = 1f / Time.unscaledDeltaTime;
                fpsText.text = "FPS: " + Mathf.Round(fps);
            updateTimer = 0.2f;
        }
    }

    private void Start()
    {
        textHolder.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (shouldShouldUI)
            {
                shouldShouldUI = false;
                textHolder.SetActive(false);
            }
            else
            {
                shouldShouldUI = true;
                textHolder.SetActive(true);
            }
        }
        UpdateFPSDisplay();
        
    }
}
