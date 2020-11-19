using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IObjectPoolNotifier {

    [SerializeField]
    float limit;
    public Vector3 origin;

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
        Debug.Log ("created");
    }

    public void OnEnqueuedToPool () {

    }
}