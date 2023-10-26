using System.Collections;
using System.Collections.Generic;
using DiractionTeam.DeveloperMode;
using UnityEngine;

public class OrientedLayout : Layout
{
    private static Vector2 _beautifulSize = new Vector2(0.62f, 0.38f);
    
    public virtual float Space { get; set; } = 15;
    protected MoveDirection MoveDirection { get; set; } = MoveDirection.None;
    
    protected override GUIRect GetNextRect(int elementIndex)
    {
        var guiRect = LastGUIRect;

        if (elementIndex != 0 && MoveDirection != MoveDirection.None)
        {
            var direction = MoveDirection switch
            {
                MoveDirection.Left => Vector2.left,
                MoveDirection.Right => Vector2.right,
                MoveDirection.Up => Vector2.down,
                MoveDirection.Down => Vector2.up,
                _ => Vector2.zero
            };

            guiRect = LastGUIRect.Step(direction).Move(direction * Space);
        }

        return guiRect.SetSize(GetStandardSize());
    }

    public override GUIRect GetFullGUIRect()
    {
        var sizeGUIRect = Position;
        
        foreach (var element in Elements)
        {
            sizeGUIRect.Encapsulate(element.GetFullGUIRect());
        }

        return sizeGUIRect;
    }

    protected Vector2 GetStandardSize()
    {
        return _beautifulSize * 100 * Scale;
    }
}
