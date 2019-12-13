using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEvilPoints : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI value;


    private void Awake() {
        EvilPoints.OnEvilPointsUpdate += EvilPointsUpdate;
    }

    private void OnDestroy() {
        EvilPoints.OnEvilPointsUpdate -= EvilPointsUpdate;
    }


    void EvilPointsUpdate(int v) {
        value.text = v.ToString();
    }
}
