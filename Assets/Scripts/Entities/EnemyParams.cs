using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Data/EnemyParams")]
public class EnemyParams : ScriptableObject
{
    public float Size;
    public int Worth;
    public int Price;
    public Vector2 SpeedMinMax;
    public Vector2 AngleMinMax;

    public float WorthPrice { get => (float)Worth / (float)Price; }
}
