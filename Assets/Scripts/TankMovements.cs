using UnityEditor.XR;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SimplePlayer : MonoBehaviour
{

    public float speed = 6f;
    public float rotationSpeed = 100f;
    public Transform tourelle;
    public float tourelleRotationSpeed = 100f;

    [SerializeField] private int _life = 50;

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

        if (tourelle != null)
        {
            float lookX = rotateInput.x;
            float lookY = rotateInput.y;

            tourelle.Rotate(Vector3.up * lookX * tourelleRotationSpeed * Time.deltaTime, Space.World);

            float tiltAmount = lookY * tourelleRotationSpeed * Time.deltaTime;
            Vector3 currentRotation = tourelle.localEulerAngles;
            float xRotation = currentRotation.x > 180 ? currentRotation.x - 360 : currentRotation.x;
            float desiredXRotation = Mathf.Clamp(xRotation - tiltAmount, -60f, 15f);
            tourelle.localEulerAngles = new Vector3(desiredXRotation, currentRotation.y, currentRotation.z);
        }
    }

    public int GetLife()
    {
        return _life;
    }

    public void ModifyLife(int delta)
    {
        _life += delta;
    }
}