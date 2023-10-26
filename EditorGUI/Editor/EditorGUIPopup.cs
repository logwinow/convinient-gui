using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DiractionTeam.DeveloperMode;
using UnityEditor;
using UnityEngine;

public class EditorGUIPopup<T> : GUIElement
{
    public EditorGUIPopup(IEnumerable<T> elements)
    {
        _elements = elements.ToList();
        
        _options.Add(new NoneLabel());
    }
    
    public EditorGUIPopup(Rect position, IEnumerable<T> elements) : this(elements)
    {
        SetPosition(position);
    }
    
    public EditorGUIPopup(GUIContent label, Rect position, IEnumerable<T> elements) : this(label, elements)
    {
        SetPosition(position);
    }
    public EditorGUIPopup(GUIContent label, IEnumerable<T> elements) : this(elements)
    {
        _label = label;   
    }

    private GUIContent _label;
    private List<T> _elements;
    private int _selectedIndex = -1;
    private List<Option> _options = new List<Option>();
    private T _defaultValue = default;

    public int SelectedIndex => _selectedIndex;
    public bool DoesSelectedIndexChanged { get; private set; }
    public GUIContent Label => _label;
    
    private class NoneLabel : Label
    {
        public NoneLabel() : base("(none)")
        {
        
        }

        public override bool Countable => false;
    }

    public EditorGUIPopup<T> SetSelectedIndex(int selectedIndex)
    {
        _selectedIndex = selectedIndex;

        return this;
    }
    
    public EditorGUIPopup<T> SetDefaultValue(T value)
    {
        _defaultValue = value;

        return this;
    }

    public EditorGUIPopup<T> SetSelectedIndex(Func<T, bool> selectedIndexPredicate)
    {
        for (var index = 0; index < _elements.Count; index++)
        {
            var element = _elements[index];

            if (selectedIndexPredicate.Invoke(element))
            {
                _selectedIndex = index;
            }
        }
        
        return this;
    }

    public EditorGUIPopup<T> SetOptionsNames(Func<T, string> nameCreator)
    {
        foreach (var element in _elements)
        {
            _options.Add(new Label(nameCreator.Invoke(element)));
        }

        return this;
    }

    public EditorGUIPopup<T> SetOptionsNames()
    {
        return SetOptionsNames(e => e.ToString());
    }

    public EditorGUIPopup<T> AddOption(Option option)
    {
        _options.Add(option);
    
        return this;
    }

    public T GetSelected()
    {
        if (SelectedIndex == -1)
            return _defaultValue;

        return _elements[SelectedIndex];
    }

    public EditorGUIPopup<T> SetPosition(Rect position)
    {
        Position = new GUIRect().SetRect(position);

        return this;
    }

    protected virtual int DrawPopup(Rect position, int selectedIndex, GUIContent[] names)
    {
        return EditorGUI.Popup(position, _label, selectedIndex, names);
    }

    public override void Draw()
    {
        var countableOptions = (from o in _options
            where o.Countable
            select o).ToList();
        
        var selectedOptionIndex = SelectedIndex == -1
            ? 0 
            : _options.IndexOf(countableOptions[SelectedIndex]);
        
        var previousIndex = SelectedIndex;
        selectedOptionIndex = DrawPopup(Position, selectedOptionIndex, _options.ConvertAll(o => new GUIContent(o.GetOptionText())).ToArray());

        var selectedOption = _options[selectedOptionIndex];
        selectedOption.Select();

        if (selectedOption.Countable)
        {
            _selectedIndex = countableOptions.IndexOf(selectedOption);
        }
        else if (selectedOption.ChangingIndex)
            _selectedIndex = -1;

        if (previousIndex != SelectedIndex)
            DoesSelectedIndexChanged = true;
    }

    public T DrawAndGet()
    {
        Draw();

        return GetSelected();
    }
}
