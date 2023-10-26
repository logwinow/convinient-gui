using System;
using System.Collections;
using System.Collections.Generic;
using DiractionTeam.DeveloperMode;
using UnityEngine;

public class VerticalLayout : OrientedLayout
{
    public VerticalLayout()
    {
        MoveDirection = MoveDirection.Down;
    }
    
    private float scroll;
    private GUIRect _realPosition;
    
    public bool UseScrollBar { get; set; }

    protected override GUIRect InitializeLastGUIRect()
    {
        return Position.MoveDown(Space).MoveRight(Space);
    }

    public override void Draw()
    {
        var originalPosition = InitializeLastGUIRect().Rect.position;
        
        Position = Position.MoveUp(scroll);
        base.Draw();
        Position = Position.MoveDown(scroll);

        if (!UseScrollBar)
            return;
        
        var rect = GetFullGUIRect().Rect;
        rect.position = originalPosition;

        var heightLimit = Screen.height - Space;
        var height = rect.y + rect.height; 

        if (height < heightLimit)
            return;
        
        rect.x -= Space * 1.5f;

        rect.width = 10;

        rect.height = Math.Min(heightLimit - rect.y, height);
        
        var scrollbarMax =  height - heightLimit;
        var size = heightLimit / height * scrollbarMax;
        
        scroll = GUI.VerticalScrollbar(rect, scroll, size, 0, scrollbarMax + size);
    }
}
