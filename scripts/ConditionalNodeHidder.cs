using Godot;
using System.Linq;

public partial class ConditionalNodeHidder : Node3D
{
    [Export]
    public string[] DoHideWhenFlags = [];

    [Export]
    public bool AllDoesMustBeTrue = false;

    [Export]
    public string[] DontHideWhenFlags = [];

    [Export]
    public bool AllDontsMustBeTrue = false;

    public override void _Ready()
    {
        checkIfShouldHide();
        GlobalState.Instance.DialogDone += checkIfShouldHide;
    }

    private void checkIfShouldHide(string dialogName = "")
    {
        if (ShouldHide())
        {
            Visible = false;
        }
    }

    private bool ShouldHide()
    {
        bool doHide = false;
        if (AllDoesMustBeTrue)
        {
            doHide = DoHideWhenFlags.All(x => GlobalState.Instance.HasDoneDialog(x));
        }
        else
        {
            doHide = DoHideWhenFlags.Any(x => GlobalState.Instance.HasDoneDialog(x));
        }

        if (doHide)
        {
            if (AllDontsMustBeTrue)
            {
                doHide = !DontHideWhenFlags.All(x => GlobalState.Instance.HasDoneDialog(x));
            }
            else
            {
                doHide = !DontHideWhenFlags.Any(x => GlobalState.Instance.HasDoneDialog(x));
            }
        }

        return doHide;
    }

    public override void _ExitTree()
    {
        GlobalState.Instance.DialogDone -= checkIfShouldHide;
    }
}
