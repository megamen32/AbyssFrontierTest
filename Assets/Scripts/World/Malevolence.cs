using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malevolence : MonoBehaviour
{
    public static event Action<float> OnMalevolenceUpdate = delegate { };

    [SerializeField] private float maleVolence;
    
    [SerializeField] private int decreaseStep = 4; //уменьшать прирост каждые n секунд
    [SerializeField] private float decreaseTimeValue = 2f; //на протяжении

    enum ModifyMode { Increase, Decrease }
    [SerializeField] private ModifyMode modifyMode = ModifyMode.Increase;


    private void Update() {
        MalevolenceUpdate();
    }

    void MalevolenceUpdate() {
        int difficultModifier = 1;

        if (Settings.CurrentGameDifficult == GameDifficult.Medium)
            difficultModifier = 2;
        if (Settings.CurrentGameDifficult == GameDifficult.Hard)
            difficultModifier = 3;

        int timeInt = (int)Time.time;
        if (modifyMode == ModifyMode.Increase && timeInt != 0 && timeInt % decreaseStep == 0) {
            modifyMode = ModifyMode.Decrease;
            StartCoroutine(DecreaseModeProcess());
        }

        float maleViolenceChange = (modifyMode == ModifyMode.Increase ? Time.deltaTime : -Time.deltaTime) * difficultModifier;
        maleVolence += maleViolenceChange;
        OnMalevolenceUpdate(maleVolence);
    }

    IEnumerator DecreaseModeProcess() {
        yield return new WaitForSeconds(decreaseTimeValue);
        modifyMode = ModifyMode.Increase;
    }

}
