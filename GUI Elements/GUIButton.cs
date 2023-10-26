using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIButton : GUIElement
{
    public GUIButton()
    {
        
    }
    
    public GUIButton(string label)
    {
        Label = label;
    }
    
    public event Action OnClick;
    public string Label { get; set; }
    public Func<bool> EnableCondition;

    public override void Draw()
    {
        GUI.enabled = EnableCondition?.Invoke() ?? true;
        
        if (GUI.Button(Position, Label))
        {
            OnClick?.Invoke();
        }

        GUI.enabled = true;
    }
}
