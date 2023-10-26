using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUILabel : GUIElement
{
    public GUILabel(string label)
    {
        Label = label;
    }
    
    public string Label { get; set; }
    public Color Color { get; set; } = Color.black;
    
    public override void Draw()
    {
        var style = GUI.skin.label;
        style.normal.textColor = Color;
        style.active.textColor = Color;
        style.hover.textColor = Color;
        style.focused.textColor = Color;
        style.onActive.textColor = Color;
        style.onFocused.textColor = Color;
        style.onHover.textColor = Color;
        style.onNormal.textColor = Color;
        
        GUI.Label(Position, Label, style);
    }
}
