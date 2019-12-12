using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected Vector3 moveDirection;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float rotationAngle;
    

    protected Motor motor;
    protected Transform transformSelf;



    protected virtual void Awake() {
        motor = new Motor(transformSelf = transform);
    }


    protected virtual void Update() {
        Move();
        Rotate();
    }


    protected virtual void Move() {
        motor.DoMove(moveDirection * moveSpeed);
    }

    protected virtual void Rotate() {

    }

}
