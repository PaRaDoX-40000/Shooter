using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RifleAnimation))]
public class Rifle : Gun
{
    [SerializeField] private float _damage=10;
    [SerializeField] private float _maxDistance=60;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _maxBullets=20;
    [SerializeField] private float _rateOfFire=0.5f;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _getRemoveTime = 0.4f;
    [SerializeField] private PlayerShootingInput _playerShootingInput;



    private bool _reloading = false;
    private bool _canShoot = true;
    private int _bullets=0;
    private Transform _mainCameraTransform;

    public UnityEvent ShootEvent;
    public UnityEvent ReloadEvent;
    public UnityEvent StartRemoveGunEvent;
    public UnityEvent StartGetGunEvent;

    public float ReloadTime => _reloadTime;
    public float RateOfFire => _rateOfFire;
    public float GetRemoveTime => _getRemoveTime;
  

    void Start()
    {               
        _bullets = _maxBullets;
        _mainCameraTransform = Camera.main.transform;
    }
    private void OnEnable()
    {
        _playerShootingInput.FireInputDown.AddListener(Fire);
        _playerShootingInput.ReloadInputDown.AddListener(Reload);
    }
    private void OnDisable()
    {
        _playerShootingInput.FireInputDown.RemoveListener(Fire);
        _playerShootingInput.ReloadInputDown.RemoveListener(Reload);
    }

    public override void Fire()
    {
        if (_bullets > 0 && _reloading == false && _canShoot==true)
        {
            ShootEvent?.Invoke();
            
            
            Vector3 direction = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 5)) - _mainCameraTransform.position;
            RaycastHit hit;
            if (Physics.Raycast(_mainCameraTransform.position, direction, out hit, _maxDistance, _layerMask))
            {
                IDamageable DamageableObject = hit.collider.gameObject.GetComponent<IDamageable>();//проверку 
                DamageableObject.TakeDamage(_damage);
            }
            _canShoot = false;
            _bullets--;
            
            StartCoroutine(RunThroughTime(_rateOfFire, ShootEnd));// gthtltkfnm
            
        }            
    }

    public void ShootEnd()
    {               
        _canShoot = true;
    } 

    public override void Reload()
    {
        if(_reloading == false && _bullets<_maxBullets)
        {
            ReloadEvent?.Invoke();
            _reloading = true;           
            StartCoroutine(RunThroughTime(_reloadTime, ReloadEnd));         
        }
    }

    public void ReloadEnd()
    {
        _bullets = _maxBullets;
        _reloading = false;        
    }   

    public override void RemoveGun()
    {
        StartRemoveGunEvent?.Invoke();
        _reloading = true;
        _canShoot = false;
        StartCoroutine(RunThroughTime(_getRemoveTime, RemoveGunEnd));
    }

    public void RemoveGunEnd()
    {
        GunRemoved?.Invoke();//Gde
        gameObject.SetActive(false);
    }

    public override void GetGun()
    {
        gameObject.SetActive(true);
        StartGetGunEvent?.Invoke();        
        StartCoroutine(RunThroughTime(_getRemoveTime, GetGunEnd));
        _reloading = true;
        _canShoot = false;
    }

    public void GetGunEnd()
    {        
        _reloading = false;
        _canShoot = true;
    }

    private IEnumerator RunThroughTime(float Time, UnityAction action)
    {
        yield return new WaitForSeconds(Time);
        action?.Invoke();
    }
}
