using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    private Vector2 _inputVector;
    private float speed = 0.1f;

    void FixedUpdate () {
        transform.position = new Vector3 (
            transform.position.x + _inputVector.x * speed,
            transform.position.y + _inputVector.y * speed,
            transform.position.z
        );
    }

    public void HandleInteract (InputAction.CallbackContext context) {
        Debug.Log ("Player interact");
    }

    public void HandleMove (InputAction.CallbackContext context) {
        _inputVector = context.ReadValue<Vector2> ();
    }
}