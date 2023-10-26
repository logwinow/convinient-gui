using System;
using System.Collections;
using System.Collections.Generic;
using DiractionTeam.DeveloperMode;
using UnityEngine;

public abstract class GUIElement
{
    public abstract void Draw();
    public virtual GUIRect Position { get; set; }

    public virtual GUIRect GetFullGUIRect() => Position;
}
