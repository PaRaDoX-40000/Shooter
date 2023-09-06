using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAnimator : MonoBehaviour
{
    [SerializeField] private Dash _dash;
    [SerializeField] private Transform _transformFaceSide;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _dash.DeshStartEventDirection.AddListener(DashAnimation);
    }

    private void OnDisable()
    {
        _dash.DeshStartEventDirection.RemoveListener(DashAnimation);
        
    }

    private void DashAnimation(Vector3 direction)
    {
        
        int angle = (int)Vector3.Angle(_transformFaceSide.position - transform.position, direction);

        if (angle < 45)
        {
            _animator.SetInteger("dash forward", 1);
        }
        else if (angle > 135)
        {
            _animator.SetInteger("dash forward", -1);
        }
        else if (angle >= 45 && angle <= 135)
        {
            if (_playerMovement.Direction.x > 0)
            {
                _animator.SetInteger("dash side", 1);
            }
            else
            {
                _animator.SetInteger("dash side", -1);

            }
        }
        StartCoroutine(AnimatirSetZero());
    }

    private IEnumerator AnimatirSetZero()
    {
        yield return new WaitForSeconds(0.5f);
        _animator.SetInteger("dash side", 0);
        _animator.SetInteger("dash forward", 0);
    }

}
