using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Option
{
    public abstract string GetOptionText();
    public virtual bool Countable => true;
    public virtual bool ChangingIndex => true;
    public virtual void Select() {}
}

public class Separator : Option
{
    public override string GetOptionText()
    {
        return null;
    }

    public override bool Countable => false;
    public override bool ChangingIndex => false;
}

public class Label : Option
{
    public Label(string text)
    {
        Text = text;
    }

    public string Text { get; }

    public override string GetOptionText()
    {
        return Text;
    }
}

public class Command : Label
{
    public Command(string text, Action action) : base(text)
    {
        _action = action;
    }

    private Action _action;

    public override bool Countable => false;
    public override bool ChangingIndex => false;

    public override void Select()
    {
        _action();
    }
}
