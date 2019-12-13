using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDifficultControls : MonoBehaviour
{
    [SerializeField] private UIButton_Difficult buttonPrefab;
    [SerializeField] private Transform buttonsRoot;

    private List<UIButton_Difficult> buttons;

    private void Awake() {
        buttons = new List<UIButton_Difficult>();

        for (GameDifficult i = GameDifficult.Easy; i < GameDifficult.Count; ++i) {
            buttons.Add(SpawnButton(ChangeDifficult, i));
        }
        ChangeDifficult(GameDifficult.Easy);
    }

    UIButton_Difficult SpawnButton(Action<GameDifficult> action, GameDifficult difficult){
        UIButton_Difficult b = Instantiate(buttonPrefab, buttonsRoot);
        b.SetOnClickAction(action, difficult);
        b.SetText(difficult.ToString());
        return b;
    }

    void ChangeDifficult(GameDifficult difficult) {
        Settings.CurrentGameDifficult = difficult;
        foreach (UIButton_Difficult b in buttons) {
            if (b.Difficult == difficult)
                b.SetButtonState(ButtonState.Pressed);
            else
                b.SetButtonState(ButtonState.Idle);
        }
    }

}
