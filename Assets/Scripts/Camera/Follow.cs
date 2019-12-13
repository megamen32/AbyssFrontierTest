using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private Transform transformSelf;


    private void Awake() {
        transformSelf = transform;
    }


    private void Update() {
        FollowTarget();
    }

    void FollowTarget() {
        if (target) {
            transformSelf.position = target.position + offset;
        }
    }

}
