using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public PlayerShoot playerShoot;
    public PlayerUpgrade playerUpgrade;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
        playerUpgrade = GetComponent<PlayerUpgrade>();
    }

    public void UpgradeHealthButtonClicked()
    {
        playerHealth.maxHealth += 1;
        playerHealth.health = playerHealth.maxHealth;
    }

    public void UpgradeSpeedButtonClicked()
    {
        playerMovement.maxSpeed += 1;
    }

    public void UpgradeFireRateButtonClicked()
    {
        playerShoot.fireRate -= 0.05f;
    }
}
