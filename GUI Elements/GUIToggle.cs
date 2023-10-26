using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIToggle : GUIElement
{
    public GUIToggle(string label)
    {
        Label = label;
    }

    public GUIToggle()
    {
        
    }
    
    public bool Value { get; set; }
    public string Label { get; set; }

    public event Action<bool> OnValueChanged;
    public event Action OnClick;
    
    public override void Draw()
    {
        var style = GUI.skin.toggle;
        style.normal.textColor = Color.black;
        style.active.textColor = Color.black;
        style.hover.textColor = Color.black;
        style.focused.textColor = Color.black;
        style.onActive.textColor = Color.black;
        style.onFocused.textColor = Color.black;
        style.onHover.textColor = Color.black;
        style.onNormal.textColor = Color.black;

        var previousValue = Value;
        
        Value =  GUI.Toggle(Position, Value, Label, style);

        if (previousValue != Value)
        {
            OnValueChanged?.Invoke(Value);
            OnClick?.Invoke();
        }
    }

    public void VirtualClick()
    {
        OnClick?.Invoke();
        OnValueChanged?.Invoke(Value);
    }
}
