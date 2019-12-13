using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor
{

    private Transform transform;


    /// <summary>
    /// Трансформ, который перемещать
    /// </summary>
    /// <param name="t"></param>
    public Motor(Transform t) {
        transform = t;
    }



    public void DoMove(Vector3 moveTo) {
        transform.position += moveTo * Time.deltaTime;
    }

    public void DoRotate(float angle) {
        float a = angle * Time.deltaTime;
        transform.Rotate(a, a, a);
    }

}
