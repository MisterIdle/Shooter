using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    public float speed;
    public float minSpeed;
    public float maxSpeed;

    [Header("Acceleration")]
    public float acceleration;
    public float deceleration;

    [Header("Rotation")]
    public float rotation;

    [Header("Components")]
    public SpriteRenderer spriteRenderer;

    [Header("Upgrade")]
    public bool canControl;

    private void Update()
    {
        if(!GameManager.instance.isGameMenu)
        {
            if (canControl)
            {
                Rotation();
            }

            Movement();
            Acceleration();
        }
    }

    private void Movement()
    {
        Vector3 _movement = spriteRenderer.transform.up;
        transform.position += _movement * speed * Time.deltaTime;
    }

    private void Rotation()
    {
        Vector3 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteRenderer.transform.rotation = Quaternion.Slerp(spriteRenderer.transform.rotation, Quaternion.LookRotation(Vector3.forward, _mousePos - spriteRenderer.transform.position), rotation * Time.deltaTime);
    }

    private void Acceleration()
    {
        // Si le joueur maintient la touche espace, augmenter la vitesse progressivement
        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed += acceleration * Time.deltaTime;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        }
        else
        {
            // Si la touche espace n'est pas enfoncée, décélérer
            speed -= deceleration * Time.deltaTime;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        }

        // Utiliser la touche Q pour augmenter la vitesse minimale progressivement
        if (Input.GetKey(KeyCode.Q))
        {
            minSpeed += acceleration * Time.deltaTime;
            minSpeed = Mathf.Clamp(minSpeed, 1, maxSpeed);
        }

        // Utiliser la touche E pour diminuer la vitesse minimale progressivement
        if (Input.GetKey(KeyCode.E))
        {
            minSpeed -= acceleration * Time.deltaTime;
            minSpeed = Mathf.Clamp(minSpeed, 2, maxSpeed);
        }

        // S'assurer que minSpeed ne soit jamais plus élevé que maxSpeed
        minSpeed = Mathf.Clamp(minSpeed, 1, maxSpeed);
    }
}
