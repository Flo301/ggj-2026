using Godot;
using System;

public partial class LevelTransition : Interactable
{
    [Export]
    public Node3D SpawnPosition { get; set; }
    [Export(PropertyHint.FilePath, ".tscn")]
    public string TargetLevelPath { get; set; }

    private PackedScene playerScene = GD.Load<PackedScene>("res://scenes/entities/Player.tscn");

    public override void OnInteract()
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
            player.GlobalPosition = SpawnPosition.GlobalPosition;
        }
    }
}
