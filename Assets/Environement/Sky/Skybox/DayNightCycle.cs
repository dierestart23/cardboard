using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    [SerializeField] private Light sun;
    public Renderer starsRenderer;
    [SerializeField, Range(0, 24)] private float timeOfDay;
    [SerializeField] public float sunRotationSpeed;

    [Header("Lighting Presets")]
    [SerializeField] private Gradient skyColor;
    [SerializeField] private Gradient equatorColor;
    [SerializeField] private Gradient sunColor;
    [SerializeField] public AnimationCurve starsCurve;


    private void Update()
    {
        timeOfDay += Time.deltaTime * sunRotationSpeed;
        if(timeOfDay > 24)
            timeOfDay = 0;
        UpdateSunRotation();
        UpdateLighting();
    }

    private void OnValidate()
    {
        UpdateSunRotation();
        UpdateLighting();
    }
    
    private void UpdateSunRotation()
    {
        float sunRotation = Mathf.Lerp(-90, 270, timeOfDay / 24);
        sun.transform.rotation = Quaternion.Euler(sunRotation, sun.transform.rotation.y, sun.transform.rotation.z);

    }

    private void UpdateLighting()
    {
        float timeFraction = timeOfDay / 24;
        RenderSettings.ambientEquatorColor = equatorColor.Evaluate(timeFraction);
        RenderSettings.ambientSkyColor = skyColor.Evaluate(timeFraction);
        sun.color = sunColor.Evaluate(timeFraction);
        starsRenderer.sharedMaterial.SetFloat("_StarsAmount",starsCurve.Evaluate(timeOfDay));
    }
}
