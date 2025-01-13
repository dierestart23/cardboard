using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    public DayNightCycle sun;
    public Renderer cloudsRenderer;

    [Header("Cloud Shader Settings")]
    [Range(0, 0.005f)]
    public float CloudGeneralNoise = 0.005f;
    [Range(0, 0.01f)]
    public float CloudDetailNoise = 0.01f;
    [Range(0, 3f)]
    public float CloudForce = 2f;

    [Header("Clouds Normal")]
    public float NormalCloudGeneralNoise = 0.005f;
    public float NormalCloudDetailNoise = 0.01f;
    public float NormalCloudForce = 2f;

    [Header("Clouds Clear")]
    public float ClearCloudGeneralNoise = 0.002f;
    public float ClearCloudDetailNoise = 0.005f;
    public float ClearCloudForce = 1f;

    [Header("Rainy")]
    public float RainyCloudGeneralNoise = 0.008f;
    public float RainyCloudDetailNoise = 0.015f;
    public float RainyCloudForce = 3f;

    [Header("Foggy")]
    public float FoggyCloudGeneralNoise = 0.007f;
    public float FoggyCloudDetailNoise = 0.012f;
    public float FoggyCloudForce = 2.5f;

    [Header("Weather Toggles")]
    public bool NormalClouds = true;
    public bool ClearClouds;
    public bool RainyClouds;
    public bool FoggyClouds;

    [Header("Transition Settings")]
    public float TransitionSpeed = 0.5f;

    private float targetGeneralNoise;
    private float targetDetailNoise;
    private float targetForce;

    // Start is called before the first frame update
    void Start()
    {
        UpdateTargetSettings();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTargetSettings();
        SmoothTransition();
        cloudsRenderer.material.SetFloat("_ThickNoiseSize",CloudGeneralNoise);
        cloudsRenderer.material.SetFloat("_NoiseSize",CloudDetailNoise);
        cloudsRenderer.material.SetFloat("_CloudsForce",CloudForce);
    }

    void UpdateTargetSettings()
    {
        if (NormalClouds)
        {
            targetGeneralNoise = NormalCloudGeneralNoise;
            targetDetailNoise = NormalCloudDetailNoise;
            targetForce = NormalCloudForce;
        }
        else if (ClearClouds)
        {
            targetGeneralNoise = ClearCloudGeneralNoise;
            targetDetailNoise = ClearCloudDetailNoise;
            targetForce = ClearCloudForce;
        }
        else if (RainyClouds)
        {
            targetGeneralNoise = RainyCloudGeneralNoise;
            targetDetailNoise = RainyCloudDetailNoise;
            targetForce = RainyCloudForce;
        }
        else if (FoggyClouds)
        {
            targetGeneralNoise = FoggyCloudGeneralNoise;
            targetDetailNoise = FoggyCloudDetailNoise;
            targetForce = FoggyCloudForce;
        }
    }

    void SmoothTransition()
    {
        CloudGeneralNoise = Mathf.Lerp(CloudGeneralNoise, targetGeneralNoise, TransitionSpeed * Time.deltaTime * sun.sunRotationSpeed);
        CloudDetailNoise = Mathf.Lerp(CloudDetailNoise, targetDetailNoise, TransitionSpeed * Time.deltaTime * sun.sunRotationSpeed);
        CloudForce = Mathf.Lerp(CloudForce, targetForce, TransitionSpeed * Time.deltaTime * sun.sunRotationSpeed);
    }
}
