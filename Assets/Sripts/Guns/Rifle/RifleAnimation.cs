using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class RifleAnimation : MonoBehaviour
{
    [SerializeField] private float _durationShootingAnimation = 0.7f;
    [SerializeField] private float _durationReloadAnimation = 2.5f;
    [SerializeField] private float _durationMoveAnimation = 0.833f;
    [SerializeField] private float _durationRemoveGunAnimation = 0.41f;
    [SerializeField] private float _durationGetGunAnimation = 0.41f;
    [SerializeField] private GameObject _bone;
    [SerializeField] private Rifle _rifle;
    [SerializeField] private PlayerMovement _playerMovement;
    Vector3 vector = new Vector3(0.25f, -0.35f, 1.05f);
    Vector3 vector2 = new Vector3(0.05f, -1.3f, 1f);
    Vector3 vector3 = new Vector3(0, 85, 0);
    Vector3 vector4 = new Vector3(-26, 46, 20);




    [SerializeField] private Animator _animation;

    private void Awake()
    {
        if (_animation == null)
            _animation = GetComponent<Animator>();

        _rifle.ShootEvent.AddListener(StartShootAnimation);
        _rifle.ReloadEvent.AddListener(StartReloadAnimation);
        _rifle.StartGetGunEvent.AddListener(StartGetGunAnimaion);
        _rifle.StartRemoveGunEvent.AddListener(StartRemoveGunAnimaion);
        _playerMovement.DirectionChange.AddListener(SetAnimationMove);
    }
    void Start()
    {
       
    }

    public void StartReloadAnimation()
    {
        _animation.SetFloat("IsReload", _durationReloadAnimation / _rifle.ReloadTime);
        StartCoroutine(RunThroughTime(_rifle.ReloadTime, StopReloadAnimation));
    }
    public void StopReloadAnimation()
    {
        _animation.SetFloat("IsReload",0);      
    }
    public void StartShootAnimation()
    {
        _animation.SetFloat("IsReload", 0);
        _animation.SetFloat("IsShoot", _durationShootingAnimation / _rifle.RateOfFire);
        StartCoroutine(RunThroughTime(_rifle.RateOfFire, StopShootAnimation));
    }
    public void StopShootAnimation()
    {       
        _animation.SetFloat("IsShoot", 0);        
    }

    public void StartGetGunAnimaion()
    {
        
        _animation.SetFloat("IsGetGun", _durationGetGunAnimation / _rifle.GetRemoveTime);
        StartCoroutine(GetGunAnimaion(_rifle.GetRemoveTime));
    }
    public void StartRemoveGunAnimaion()
    {
        _animation.SetFloat("IsRemoveGun", _durationRemoveGunAnimation / _rifle.GetRemoveTime);
        StartCoroutine(RemoveGunAnimaion(_rifle.GetRemoveTime));
        _animation.SetFloat("IsReload", 0);
        _animation.SetFloat("IsGetGun", 0);
    }


    private IEnumerator GetGunAnimaion(float time)
    {           
        for (int i= 0; i < 24; i++)
        {
            
            yield return new WaitForSeconds(time/ 24f);            
            Vector3 vector6 = Vector3.Lerp(vector2, vector, i / 24f);
           
            _bone.transform.localPosition = vector6;
            _bone.transform.localEulerAngles = Vector3.Lerp(vector4, vector3, i / 24f);          
        }       
    }

    private IEnumerator RemoveGunAnimaion(float time)
    {
        for (int i = 0; i < 24; i++)
        {
            yield return new WaitForSeconds(time / 24f);          
            Vector3 vector6 = Vector3.Lerp(vector, vector2, i / 24f);

            _bone.transform.localPosition = vector6;
            _bone.transform.localEulerAngles = Vector3.Lerp(vector3, vector4, i / 24f);
        }
    }

    public void SetAnimationMove(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            _animation.SetFloat("IsMove", 0);
        }
        else
        {           
            _animation.SetFloat("IsMove", 1);
        }
    }

    private IEnumerator RunThroughTime(float Time, UnityAction action)
    {
        yield return new WaitForSeconds(Time);
        action?.Invoke();
    }







}
