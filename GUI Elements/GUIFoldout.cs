using System;
using System.Collections;
using System.Collections.Generic;
using DiractionTeam.DeveloperMode;
using UnityEngine;

public class GUIFoldout : VerticalLayout
{
    public GUIFoldout(string label)
    {
        Label = label;
    }
    
    public GUIFoldout() {}
    
    public string Label { get; set; }
    public bool IsFolded { get; set; } = true;
    public override float Space { get; set; } = 15;

    public event Action OnClick;
    
    public override void Draw()
    {
        var triangle = IsFolded ? "△" : "▽";
        
        var style = new GUIStyle("button");
        style.normal.background = null;
        
        if (GUI.Button(Position, triangle + " " + Label, style))
        {
            IsFolded = !IsFolded;
            
            OnClick?.Invoke();
        }

        if (IsFolded)
            return;
        
        base.Draw();
    }

    protected override GUIRect InitializeLastGUIRect()
    {
        return base.InitializeLastGUIRect().StepDown();
    }

    public override GUIRect GetFullGUIRect()
    {
        if (IsFolded)
            return Position;
        
        return base.GetFullGUIRect();
    }
}
