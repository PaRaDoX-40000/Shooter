using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dash : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speedMultiplier = 10;
    private Vector3 _direction;
    private Vector3 _velocityXZ;
    private Vector3 _inputDirection;
    bool _enabledDesh = false;

    public UnityEvent<Vector3> DeshStartEventDirection = new UnityEvent<Vector3>();



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {          
            StartDesh();
        }
        if(_enabledDesh == true)
        {
            float Vertical = (int)Input.GetAxis("Vertical");
            float Horizontal = (int)Input.GetAxis("Horizontal");
            _inputDirection = new Vector3(Horizontal, 0, Vertical);
        }
        

    }

    private void FixedUpdate()
    {
        if(_enabledDesh==true)
        {
            Vector3 vector = transform.TransformVector(new Vector3(_inputDirection.x, 0, _inputDirection.z));
            if (Vector3.Angle(vector, _velocityXZ) > 90)
            {
                _velocityXZ = Vector3.zero;
                _enabledDesh = false;
                _playerMovement.enabled = true;
                return;
            }
            _velocityXZ = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            if (_velocityXZ.magnitude < _playerMovement.Speed)
            {
                _velocityXZ = Vector3.zero;
                _enabledDesh = false;
                _playerMovement.enabled = true;
                return;
            }
            _rigidbody.velocity -= _velocityXZ / 20;            
        }
        
    }

    private void StartDesh()
    {
        _velocityXZ = new Vector3(_rigidbody.velocity.x,0,_rigidbody.velocity.z);
        if(_velocityXZ == Vector3.zero || _enabledDesh == true)
        {            
            return;
        }
               
        _enabledDesh = true;
        _direction = _velocityXZ;
        _rigidbody.velocity= Vector3.zero;
        _rigidbody.velocity = _velocityXZ * _speedMultiplier + new Vector3(0, _rigidbody.velocity.y, 0);
        _playerMovement.enabled= false;
        DeshStartEventDirection?.Invoke(_rigidbody.velocity.normalized);
    }

   
}
