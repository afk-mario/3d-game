using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Bullet : MonoBehaviour, IObjectPoolNotifier {

    [SerializeField]
    float limit;
    public Vector3 origin;

    private Rigidbody _rb;
    private Rigidbody rb {
        get {
            if (_rb == null) _rb = gameObject.GetComponent<Rigidbody> ();
            return _rb;
        }
    }

    void FixedUpdate () {
        float dist = Vector3.Distance (origin, transform.position);

        if (dist > limit) {
            gameObject.ReturnToPool ();
        }
    }

    void OnCollisionEnter (Collision col) {
        gameObject.ReturnToPool ();
    }

    public void OnCreatedOrDequeuedFromPool (bool created) {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
    }

    public void OnEnqueuedToPool () { }
}