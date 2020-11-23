using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour {
    IEnumerator behaviour;

    [Range (0, 5f)]
    float waitTime;
    [SerializeField]
    Transform[] targetPoints;

    [SerializeField]
    Transform target;

    [SerializeField]
    [Range (0, 10)]
    float tolerance;

    [SerializeField]
    [Range (0.2f, 1)]
    float patrolSpeed = 0.2f;

    [SerializeField]
    [Range (0.2f, 1)]
    float followSpeed = 0.2f;

    [SerializeField]
    string followTag;

    Rigidbody rb;

    void Start () {
        rb = gameObject.GetComponent<Rigidbody> ();
    }

    void OnTriggerEnter (Collider collision) {
        if (collision.gameObject.tag == followTag) {
            if (behaviour != null) {
                StopCoroutine (behaviour);
            }
            target = collision.gameObject.transform;
            behaviour = Follow ();
            StartCoroutine (behaviour);
        }
    }

    void OnTriggerExit (Collider collision) {
        if (collision.gameObject.tag == followTag) {
            StopCoroutine (behaviour);
            behaviour = Patrol ();
            StartCoroutine (behaviour);
        }
    }

    Transform AdquireTarget () {
        return targetPoints[Random.Range (0, targetPoints.Length)];
    }

    IEnumerator Patrol () {
        var wait = new WaitForSeconds (Random.Range (0, waitTime));
        var waitFrame = new WaitForFixedUpdate ();
        while (true) {
            target = AdquireTarget ();
            float distance = Vector3.Distance (transform.position, target.position);
            while (distance > tolerance) {
                MoveToTarget (patrolSpeed);
                distance = Vector3.Distance (transform.position, target.position);
                yield return waitFrame;
            }
            yield return wait;
        }
    }

    IEnumerator Follow () {
        var waitFrame = new WaitForFixedUpdate ();
        while (true) {
            float distance = Vector3.Distance (transform.position, target.position);
            if (distance > tolerance) {
                MoveToTarget (followSpeed);
                yield return waitFrame;
            } else {
                yield return waitFrame;
            }
        }
    }

    void MoveToTarget (float speed) {
        Vector3 movePosition = Vector3.Lerp (transform.position, target.position, speed * Time.fixedDeltaTime);
        rb.MovePosition (movePosition);
    }
}