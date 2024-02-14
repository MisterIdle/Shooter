using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Keyboard?")]
    public bool useMouse;

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
        if (!GameManager.instance.isGameMenu)
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
        if (useMouse)
        {
            Vector3 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spriteRenderer.transform.rotation = Quaternion.Slerp(spriteRenderer.transform.rotation, Quaternion.LookRotation(Vector3.forward, _mousePos - spriteRenderer.transform.position), rotation * Time.deltaTime);
        }
        else
        {
            Vector3 _joystickPos = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            if (_joystickPos != Vector3.zero)
            {
                spriteRenderer.transform.rotation = Quaternion.Slerp(spriteRenderer.transform.rotation, Quaternion.LookRotation(Vector3.forward, _joystickPos), rotation * Time.deltaTime);
            }
        }
    }

    private void Acceleration()
    {
        if (useMouse)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                speed += acceleration * Time.deltaTime;
                speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
            }
            else
            {
                speed -= deceleration * Time.deltaTime;
                speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                minSpeed += acceleration * Time.deltaTime;
                minSpeed = Mathf.Clamp(minSpeed, 1, maxSpeed);
            }

            if (Input.GetKey(KeyCode.E))
            {
                minSpeed -= acceleration * Time.deltaTime;
                minSpeed = Mathf.Clamp(minSpeed, 2, maxSpeed);
            }

            minSpeed = Mathf.Clamp(minSpeed, 1, maxSpeed);
        } 
        else
        {
            if (Input.GetKey(KeyCode.Joystick1Button1))
            {
                speed += acceleration * Time.deltaTime;
                speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
            }
            else
            {
                speed -= deceleration * Time.deltaTime;
                speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
            }

            if (Input.GetKey(KeyCode.Joystick1Button4))
            {
                minSpeed += acceleration * Time.deltaTime;
                minSpeed = Mathf.Clamp(minSpeed, 1, maxSpeed);
            }

            if (Input.GetKey(KeyCode.Joystick1Button5))
            {
                minSpeed -= acceleration * Time.deltaTime;
                minSpeed = Mathf.Clamp(minSpeed, 2, maxSpeed);
            }

            minSpeed = Mathf.Clamp(minSpeed, 1, maxSpeed);
        }
    }
}
