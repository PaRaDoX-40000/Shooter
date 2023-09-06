using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

interface IMoving
{    
    void MoveToVektor(Vector3 direction);
    void SubscribeToMovementChangeEvent(UnityAction<float> unityAction);
    void UnsubscribeToMovementChangeEvent(UnityAction<float> unityAction);

}
