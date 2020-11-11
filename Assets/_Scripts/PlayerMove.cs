using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour {

    [SerializeField]
    [Range (0.1f, 10)]
    private float acceleration = 6f;
    [SerializeField]
    [Range (0.1f, 1)]
    private float maxSpeed = 40;

    [SerializeField]
    [Range (0, 1)]
    private float friccion = 0.9f;

    [SerializeField]
    [Range (0, .1f)]
    private float tolerance = 0.01f;

    [SerializeField]
    [Range (0, 1f)]
    private float sensitivity = 0.01f;

    [SerializeField]
    private CharacterController controller;

    private Vector2 _inputVector;
    [SerializeField]
    private Vector3 _velocity;

    void FixedUpdate () {
        Vector3 direction = transform.right * _inputVector.x + transform.forward * _inputVector.y;
        _velocity += direction * acceleration * Time.deltaTime;
        _velocity = Vector3.ClampMagnitude (_velocity, maxSpeed);
        _velocity *= friccion;

        if (_velocity.magnitude < tolerance) {
            _velocity = Vector3.zero;
        } else {
            controller.Move (_velocity);
        }
    }

    public void HandleInteract (InputAction.CallbackContext context) {
        Debug.Log ("Player interact");
    }

    public void HandleMove (InputAction.CallbackContext context) {
        _inputVector = context.ReadValue<Vector2> ();
    }
}