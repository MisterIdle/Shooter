using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Cannon Settings")]
    [SerializeField] private Cannons[] availableCannons;
    public int selectedCannonIndex = 0;

    private PlayerMovement playerMovement;
    private float nextFire;
    private float elapsedTimeSinceLastShot;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
    }

    private void Shoot()
    {
    }
}
