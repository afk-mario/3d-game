using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    float limit;
    public Vector3 origin;

    void FixedUpdate () {
        float dist = Vector3.Distance (origin, transform.position);

        if (dist > limit) {
            Destroy (gameObject);
        }
    }

    void OnCollisionEnter (Collision col) {
        Destroy (gameObject);
    }
}