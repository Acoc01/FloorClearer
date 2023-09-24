using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerHealthSystem playerHealth;  
    private Image healthBarImage;

    private void Awake()
    {
        healthBarImage = GetComponent<Image>();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthBarImage.fillAmount = playerHealth.remainingHealth / playerHealth.health;
    }
}
