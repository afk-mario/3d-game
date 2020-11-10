using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour {

    [SerializeField]
    [Range (0, 100f)]
    private float sensitivity = 100f;

    [SerializeField]
    private Vector2 _inputVector;

    [SerializeField]
    private Transform playerBody;

    private float _xRotation = 0f;

    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate () {

        float mouseX = _inputVector.x * sensitivity * Time.deltaTime;
        float mouseY = _inputVector.y * sensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp (_xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler (_xRotation, 0, 0);
        playerBody.Rotate (Vector3.up * mouseX);
    }

    public void HandleLook (InputAction.CallbackContext context) {
        _inputVector = context.ReadValue<Vector2> ();
    }
}