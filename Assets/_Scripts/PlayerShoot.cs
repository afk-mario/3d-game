using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour {
    public delegate void ReloadEvent (bool reloading);
    public delegate void AmmoEvent (int x);
    public event AmmoEvent OnAmmoChanged;
    public event ReloadEvent OnReload;

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
    public int maxAmmo = 10;

    private int _ammo;
    int ammo {
        get => _ammo;
        set {
            if (value != _ammo) {
                OnAmmoChanged?.Invoke (value);
                _ammo = value;
            }
        }
    }
    bool reloading = false;

    public void Start () {
        ammo = maxAmmo;
    }

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

    IEnumerator Reload () {
        reloading = true;
        OnReload?.Invoke (true);
        var wait = new WaitForSeconds (.30f);

        while (ammo < maxAmmo) {
            ammo += 1;
            yield return wait;
        }
        reloading = false;
        OnReload?.Invoke (false);
        ammo = maxAmmo;
    }

    void Shoot () {
        if (!_canShoot) return;
        if (reloading) return;
        if (ammo == 0) {
            StartCoroutine (Reload ());
            return;
        }

        ammo -= 1;
        var bullet = pool.GetObject ();
        bullet.transform.position = firePoint.position;
        var rb = bullet.GetComponent<Rigidbody> ();
        rb.AddForce (firePoint.forward * bulletForce, ForceMode.Impulse);
        _canShoot = false;
    }
}