


using Godot;

partial class GlobalState : Node
{
    public static GlobalState Instance { get; private set; }

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
}