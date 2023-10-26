using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorGUILayoutPopup<T> : EditorGUIPopup<T>
{
    public EditorGUILayoutPopup(GUIContent label, IEnumerable<T> elements) : base(label, elements)
    {
    }

    protected override int DrawPopup(Rect position, int selectedIndex, GUIContent[] names)
    {
        return EditorGUILayout.Popup(Label, selectedIndex, names);
    }
}
