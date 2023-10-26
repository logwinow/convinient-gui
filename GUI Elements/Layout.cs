using System.Collections;
using System.Collections.Generic;
using DiractionTeam.DeveloperMode;
using UnityEngine;

public abstract class Layout : GUIElement
{
    private List<GUIElement> _elements = new List<GUIElement>();

    protected List<GUIElement> Elements => _elements;

    protected virtual GUIRect LastGUIRect { get; private set; }
    public float Scale { get; set; } = 2;

    public void Add(GUIElement element)
    {
        _elements.Add(element);
    }

    public void Add(params GUIElement[] elements)
    {
        foreach (var element in elements)
        {
            Add(element);
        }
    }

    public override void Draw()
    {
        LastGUIRect = InitializeLastGUIRect();
        
        for (var index = 0; index < _elements.Count; index++)
        {
            var element = _elements[index];
            element.Position = GetNextRect(index);

            DrawElement(element);

            LastGUIRect = element.GetFullGUIRect();
        }
    }

    protected abstract GUIRect GetNextRect(int elementIndex);

    protected virtual GUIRect InitializeLastGUIRect()
    {
        return Position;
    }

    protected virtual void DrawElement(GUIElement element)
    {
        element.Draw();
    }
}
