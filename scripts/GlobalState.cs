


using System.Collections.Generic;
using Godot;

partial class GlobalState : Node
{
    public static GlobalState Instance { get; private set; }
    public string LastScene { get; internal set; }

    public List<string> doneDialogs = [];

    public override void _Ready()
    {
        base._Ready();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            GD.PushError("GlobalState already instanciated");
        }
    }

    public bool hasDoneDialog(string key)
    {
        return doneDialogs.Contains(key);
    }
    public void setDoneDialog(string key)
    {
        doneDialogs.Add(key);
        GD.Print("Dialog done: " + key);
        GD.Print(doneDialogs);
    }

}