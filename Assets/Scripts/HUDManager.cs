using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Image globalSpeed;
    public Image minSpeed;
    public Image fireRate;
    public Image health;

    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        globalSpeed.fillAmount = playerMovement.speed / playerMovement.maxSpeed;
        minSpeed.fillAmount = playerMovement.minSpeed / playerMovement.maxSpeed;
        health.fillAmount = playerHealth.health / playerHealth.maxHealth;
    }

}
