using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefab;

    [Header("Cannon")]
    public List<Transform> cannonPosition = new List<Transform>();

    [Header("Turrel")]
    public GameObject turrel;
    public float turrelFireRate;
    public float turrelNumFire;
    public float nextTurrelFire;

    [Header("FireRate")]
    public float fireRate;
    public float nextFire;
    public float elapsedTimeSinceLastShot;

    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!GameManager.instance.isGameMenu)
        {

            if (playerMovement.useMouse)
            {
                if (Input.GetMouseButton(0) && Time.time > nextFire && playerMovement.canControl)
                {
                    nextFire = Time.time + fireRate;
                    Shoot();
                    elapsedTimeSinceLastShot = 0f;
                }
            } else
            {
                if(Input.GetKey(KeyCode.Joystick1Button0) && Time.time > nextFire && playerMovement.canControl)
                {
                    nextFire = Time.time + fireRate;
                    Shoot();
                    elapsedTimeSinceLastShot = 0f;
                }
            }

            if (playerMovement.useMouse)
            {
                if (Input.GetMouseButton(1))
                {
                    playerMovement.canControl = false;
                    TurrelRotation();

                    if (Input.GetMouseButton(0))
                    {
                        TurrelShoot();
                    }
                }
                else
                {
                    playerMovement.canControl = true;
                }

            } else {
                
                if(Input.GetKey(KeyCode.Joystick1Button2))
                {
                    playerMovement.canControl = false;
                    TurrelRotation();

                    if (Input.GetKey(KeyCode.Joystick1Button0))
                    {
                        TurrelShoot();
                    }
                }
                else
                {
                    playerMovement.canControl = true;
                }
            }
        }
    }

    private void TurrelRotation()
    {
        if (playerMovement.useMouse)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            turrel.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - turrel.transform.position);
        } 
        else
        {
            Vector3 joystickPos = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            if (joystickPos != Vector3.zero)
            {
                turrel.transform.rotation = Quaternion.LookRotation(Vector3.forward, joystickPos);
            }
        }
    }


    private void TurrelShoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            for (int i = 0; i < turrelNumFire; i++)
            {
                Invoke("FireTurrelBullet", i * turrelFireRate);
            }
        }
    }

    private void FireTurrelBullet()
    {
        Instantiate(bulletPrefab, turrel.transform.position, turrel.transform.rotation);
    }

    private void Shoot()
    {
        foreach (Transform _cannon in cannonPosition)
        {
            Instantiate(bulletPrefab, _cannon.position, _cannon.rotation);
        }
    }
}
