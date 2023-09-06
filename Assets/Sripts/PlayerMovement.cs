using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    private Rigidbody _rigidbody;
    private int _Vertical = 0;
    private int _Horizontal = 0;
    private Vector3 _direction;
    private Vector3 _additionalSpeed= Vector3.zero;
    private bool _canChangeAdditionalSpeed = true;

    public UnityEvent<Vector3> DirectionChange = new UnityEvent<Vector3>();
   
    public Vector3 Direction => _direction; 
    public float Speed => _speed;

    private Vector3 DirectionChangeDetector
    { get => _direction; 
        set 
        {
            if (_direction != value)
            {
                _direction = value;
                DirectionChange?.Invoke(_direction);
            }
        } }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();              
    }     

    void FixedUpdate()
    {
        _Vertical = (int)Input.GetAxis("Vertical");
        _Horizontal = (int)Input.GetAxis("Horizontal");
        DirectionChangeDetector = new Vector3(_Horizontal, 0, _Vertical);
        MoveToVektor(DirectionChangeDetector);       
    }

    public void MoveToVektor(Vector3 direction)
    {
        
        Vector3 vector =  transform.TransformVector(new Vector3(_direction.x, 0, _direction.z)) * _speed;
        if(Vector3.Angle(vector, _additionalSpeed)>90)
        {
            _additionalSpeed = Vector3.zero;
        }  
        _rigidbody.velocity = vector + _additionalSpeed + new Vector3(0,_rigidbody.velocity.y,0);
       
    }     
}
