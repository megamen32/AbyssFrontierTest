using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private EnemyParams enemyParams;
    public EnemyParams EnemyParams { get => enemyParams; }


    protected override void Awake() {
        base.Awake();
        if (enemyParams) {
            moveSpeed = Random.Range(enemyParams.SpeedMinMax.x, enemyParams.SpeedMinMax.y);
            rotationAngle = Random.Range(enemyParams.AngleMinMax.x, enemyParams.AngleMinMax.y);
        }
        StartCoroutine(DelayedDestroy());
    }

    protected override void Move() {
        motor.DoMove(transformSelf.forward * moveSpeed);
    }

    protected override void Rotate() {
        base.Rotate();
        motor.DoRotate(rotationAngle);
    }

    IEnumerator DelayedDestroy() {
        yield return new WaitForSeconds(120);
        Destroy(gameObject);
    }
}
