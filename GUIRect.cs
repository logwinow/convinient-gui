using System.Collections;
using System.Collections.Generic;
using DiractionTeam.DeveloperMode;
using UnityEngine;

namespace DiractionTeam.DeveloperMode
{
    public struct GUIRect
    {
        private Rect _rect;

        public Rect Rect => _rect;

        public GUIRect SetPosition(Vector2 position)
        {
            _rect.position = position;

            return this;
        }

        public GUIRect SetPosition(float x, float y) => SetPosition(new Vector2(x, y));

        public GUIRect SetSize(Vector2 size)
        {
            _rect.size = size;

            return this;
        }

        public GUIRect SetSize(float width, float height) => SetSize(new Vector2(width, height));
        public GUIRect SetHeight(float height) => SetSize(_rect.width, height);

        public GUIRect Scale(float scale) => SetSize(_rect.size * scale);

        public GUIRect Scale(Vector2 scale) => SetSize(_rect.size * scale);

        public GUIRect SetRect(Rect rect)
        {
            _rect = rect;

            return this;
        }

        public GUIRect Move(Vector2 value)
        {
            _rect.position += value;

            return this;
        }
        
        public GUIRect MoveDown(float distance) => Move(Vector2.up * distance);
        public GUIRect MoveUp(float distance) => Move(Vector2.down * distance);
        public GUIRect MoveRight(float distance) => Move(Vector2.right * distance);
        public GUIRect MoveLeft(float distance) => Move(Vector2.left * distance);

        public GUIRect Step(Vector2 direction) => Move(direction * _rect.size);
        public GUIRect StepUp(float percent = 1f) => Step(Vector2.down * percent);
        public GUIRect StepDown(float percent = 1f) => Step(Vector2.up * percent);
        public GUIRect StepRight(float percent = 1f) => Step(Vector2.right * percent);
        public GUIRect StepLeft(float percent = 1f) => Step(Vector2.left * percent);
        
        public GUIRect Encapsulate(GUIRect guiRect)
        {
            if (_rect.size == Vector2.zero)
            {
                return SetSize(guiRect.Rect.size);
            }
            
            var bounds = (Bounds) this;
            bounds.Encapsulate((Bounds) guiRect);

            SetRect((GUIRect) bounds);

            return this;
        }

        public GUIRect Clone()
        {
            return Create().SetRect(_rect);
        }

        public static GUIRect Create()
        {
            return new GUIRect();
        }

        public static implicit operator Rect(GUIRect guiRect)
        {
            return guiRect.Rect;
        }

        public static explicit operator Bounds(GUIRect guiRect)
        {
            return new Bounds(guiRect.Rect.center, guiRect.Rect.size);
        }

        public static explicit operator GUIRect(Bounds bounds)
        {
            var guiRect = new GUIRect();
            guiRect.SetPosition(bounds.min);
            guiRect.SetSize(bounds.size);

            return guiRect;
        }

        public override string ToString()
        {
            return _rect.ToString();
        }
    }
}