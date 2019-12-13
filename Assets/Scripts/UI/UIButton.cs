using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public enum ButtonState {
    Idle, Hover, Pressed
}

public abstract class UIButton : MonoBehaviour, IPointerClickHandler {
    [SerializeField] protected Image baseImage;
    protected Button button;
    
    

    private void Awake() {
        button = GetComponent<Button>();
        if (button == null)
            button = GetComponentInChildren<Button>();
    }

    public virtual void SetText(string key) { }
    public virtual void SetButtonState(ButtonState state) { }

    public abstract void OnPointerClick(PointerEventData eventData);
}

