using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilPoints : MonoBehaviour {
    [SerializeField] private int evilPointsOnStart = 0;

    public static event Action<int> OnEvilPointsUpdate = delegate { };

    private int evilPoints;

    private float timeToAddPoints = 1f; // каждую секунду

    private float currMaleVolence;

    private void Awake() {
        Malevolence.OnMalevolenceUpdate += MaleVolenceUpdate;
        evilPoints = evilPointsOnStart;
    }

    private void OnDestroy() {
        Malevolence.OnMalevolenceUpdate -= MaleVolenceUpdate;
    }

    void MaleVolenceUpdate(float v) {
        currMaleVolence = v;
    }

    private void Update() {
        AddPoints();
    }

    float nextAddTime = 0f;
    void AddPoints() {
        if (Time.time > nextAddTime) {
            nextAddTime = Time.time + timeToAddPoints;
            evilPoints += (int)currMaleVolence;
            UpdateEvilPoints();
        }
    }

    void UpdateEvilPoints() {
        OnEvilPointsUpdate(evilPoints);
    }

    public int GetEvilPoints() => evilPoints;
    public void SpendPoints(int v) => evilPoints -= v; //TODO может быть и отрицательным числом, проверки то нет
}
