using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour {
    [SerializeField]
    Transform firePoint;

    [SerializeField]
    ObjectPool pool;
    public float bulletForce = 20f;

    [SerializeField]
    [Range (0, 0.5f)]
    float rate = 0;
    float _rateAcc = 0;
    bool _canShoot = true;
    bool _isShooting = false;

    public void Update () {
        if (!_canShoot) {
            _rateAcc = Mathf.Max (0, _rateAcc - rate);
        }
        if (_rateAcc == 0) {
            _canShoot = true;
            _rateAcc = 1;
        }

        if (_isShooting) {
            Shoot ();
        }
    }

    public void HandleInteract (InputAction.CallbackContext context) {
        _isShooting = context.ReadValue<float> () == 1;
    }

    void Shoot () {
        if (_canShoot) {
            var bullet = pool.GetObject ();
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = Quaternion.identity;
            // Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
            var rb = bullet.GetComponent<Rigidbody> ();
            rb.AddForce (firePoint.forward * bulletForce, ForceMode.Impulse);
            _canShoot = false;
        }
    }
}