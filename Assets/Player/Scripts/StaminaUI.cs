using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    public Image staminaBar;  // Reference to the stamina bar UI image
    public float fadeSpeed = 1f;  // Speed at which the UI fades in and out

    private bool isFlickering = false;  // To track the flickering state
    private float flickerTimer = 0f;  // Timer to control the flickering frequency

    void Start()
    {
        if (staminaBar == null)
        {
            Debug.LogError("Stamina Bar Image not assigned.");
        }
    }

    public void UpdateStamina(float currentStamina, float maxStamina, float staminaThreshold)
    {
        staminaBar.fillAmount = currentStamina / maxStamina;

        // Handle flickering transparency during regeneration
        if (currentStamina == 0 && currentStamina < staminaThreshold)
        {
            isFlickering = true;
        }
        else if (currentStamina > staminaThreshold)
        {
            isFlickering = false;
            staminaBar.color = new Color(staminaBar.color.r, staminaBar.color.g, staminaBar.color.b, 1f);
        }

        if (isFlickering)
        {
            FlickerEffect();
        }
    }

    private void FlickerEffect()
    {
        flickerTimer += Time.deltaTime * fadeSpeed;
        float alpha = Mathf.Abs(Mathf.Sin(flickerTimer * Mathf.PI));

        // Flicker between fully visible (alpha = 1) and semi-transparent (alpha = 0.5)
        staminaBar.color = new Color(staminaBar.color.r, staminaBar.color.g, staminaBar.color.b, Mathf.Lerp(0.5f, 1f, alpha));
    }
}
