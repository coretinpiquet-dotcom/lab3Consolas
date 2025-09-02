using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SimplePlayer : MonoBehaviour
{

    public float speed = 6f;
    public float rotationSpeed = 100f;
    public Transform tourelle;
    public float tourelleRotationSpeed = 100f;
    private Vector2 moveInput;
    private Vector2 rotateInput;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void onRotate(InputAction.CallbackContext context)
    {
        rotateInput = context.ReadValue<Vector2>();
    }

    void Update2dMovement()
    {
        if ( moveInput.x == 0 && moveInput.y == 0)
            return;

        Vector3 forwardMovement = transform.forward * moveInput.y * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);

        float turn = moveInput.x * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    // Update is called once per frame
    void Update()
    {
        Update2dMovement();
        float lookX = rotateInput.x;
        if (tourelle != null)
        {
            tourelle.Rotate(Vector3.up * lookX * tourelleRotationSpeed * Time.deltaTime, Space.World);
        }
    }

}