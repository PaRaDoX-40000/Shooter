using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class RobotSpiderMoving : MonoBehaviour, IMoving
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _speedRotation = 1;
    [SerializeField] private float _animation = 1;
    [SerializeField] Animator _animator;
    private Rigidbody _rigidbody;
    private Vector3 _eulerAngleVelocity;
    private int _Vertical = 0;
    private int _Horizontal = 0;

    

   
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _eulerAngleVelocity = new Vector3(0, 20* _speedRotation, 0);
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        _Vertical = (int)Input.GetAxis("Vertical");
        _Horizontal = (int)Input.GetAxis("Horizontal");
        MoveToVektor(new Vector3(0, 0, _Vertical));
        MoveRotation(_Horizontal);
    }

    private void MoveRotation(float speed)
    {
        Quaternion deltaRotation = Quaternion.Euler(_eulerAngleVelocity * Time.fixedDeltaTime* speed);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);      
    }

    public void MoveToVektor(Vector3 direction)
    {
        _rigidbody.MovePosition(transform.position + transform.TransformVector(direction) * _speed * Time.deltaTime);
        _animator.SetFloat("Speed", _speed * direction.z* _animation);
    }

    public void SubscribeToMovementChangeEvent(UnityAction<float> unityAction)
    {
        throw new System.NotImplementedException();
    }

    public void UnsubscribeToMovementChangeEvent(UnityAction<float> unityAction)
    {
        throw new System.NotImplementedException();
    }
}
