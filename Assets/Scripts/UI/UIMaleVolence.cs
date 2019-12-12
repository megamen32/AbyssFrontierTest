using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMaleVolence : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI value;

    private void Awake() {
        Malevolence.OnMalevolenceUpdate += MaleVolenceUpdate;
    }

    private void OnDestroy() {
        Malevolence.OnMalevolenceUpdate -= MaleVolenceUpdate;
    }

    void MaleVolenceUpdate(float v) {
        value.text = v.ToString("F1");
    }
}
