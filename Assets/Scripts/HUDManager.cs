using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Text")]
    public TMP_Text wave;

    [Header("Bars")]
    public Image globalSpeed;
    public Image minSpeed;
    public Image health;

    [Header("Menu")]
    public GameObject menu;
    public Button nextWaveBtn;
    public Button upgradeHealth;
    public Button upgradeSpeed;
    public Button upgradeFireRate;

    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private PlayerShoot playerShoot;
    private WaveManager waveManager;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerShoot = FindObjectOfType<PlayerShoot>();
        waveManager = FindAnyObjectByType<WaveManager>();
    }

    private void Update()
    {
        Bar();
        Text();
    }

    public void Bar()
    {
        globalSpeed.fillAmount = playerMovement.speed / playerMovement.maxSpeed;
        minSpeed.fillAmount = playerMovement.minSpeed / playerMovement.maxSpeed;
        health.fillAmount = playerHealth.health / playerHealth.maxHealth;
    }

    public void Text()
    {
        wave.text = "Wave: " + waveManager.waveNumber;
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
        //playerShoot.fireRate -= 0.05f;
    }

}
