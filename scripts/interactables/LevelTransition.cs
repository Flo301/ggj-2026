using Godot;
using System;

public partial class LevelTransition : Interactable
{
    [Export]
    public Node3D SpawnPosition { get; set; }
    [Export(PropertyHint.FilePath, ".tscn")]
    public string TargetLevelPath { get; set; }
    [Export]
    public string SceneEnterDialog { get; set; }

    private PackedScene playerScene = GD.Load<PackedScene>("res://scenes/entities/Player.tscn");

    protected override void onInteract()
    {
        GlobalState.Instance.LastScene = GetTree().CurrentScene.SceneFilePath;
        GetTree().ChangeSceneToFile(TargetLevelPath);
        //TODO: Level Transition - fade out/in, loading screen, etc.
    }

    public override void _Ready()
    {
        base._Ready();

        if (string.IsNullOrEmpty(GlobalState.Instance.LastScene))
        {
            GlobalState.Instance.LastScene = TargetLevelPath;
        }

        if (GlobalState.Instance.LastScene == TargetLevelPath)
        {
            var player = playerScene.Instantiate<CharacterController>();
            GetTree().CurrentScene.CallDeferred("add_child", player);
            CallDeferred(MethodName.InitPlayer, player);
        }
    }

    private void InitPlayer(Node3D player)
    {
        player.GlobalPosition = SpawnPosition.GlobalPosition;

        if (!string.IsNullOrEmpty(SceneEnterDialog))
        {
            GlobalState.Instance.openDialog(SceneEnterDialog);
        }
    }
}
