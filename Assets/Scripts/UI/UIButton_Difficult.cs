using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UIButton_Difficult : UIButton
{
    public static UIButton_Difficult ActiveButton;

    [SerializeField] protected TextMeshProUGUI buttonText;
    protected Action<GameDifficult> OnClick;

    public GameDifficult Difficult { get; private set; }

    public void SetOnClickAction(Action<GameDifficult> _onClickAction, GameDifficult difficult) {
        OnClick = _onClickAction;
        Difficult = difficult;
    }

    public override void SetText(string key) {
        base.SetText(key);
        buttonText.text = key;
    }

    public override void SetButtonState(ButtonState state) {
        if (state == ButtonState.Idle)
            baseImage.color = button.colors.normalColor;
        if (state == ButtonState.Pressed) {
            baseImage.color = button.colors.pressedColor;
            ActiveButton = this;
        }
    }

    public override void OnPointerClick(PointerEventData eventData) {
        OnClick(Difficult);
    }
}
