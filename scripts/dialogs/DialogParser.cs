using Godot;
using Newtonsoft.Json;

public class DialogParser
{
    const string dialogAssetPath = "res://assets/dialogs/";

    public static Dialog LeadDialog(string key)
    {
        string filePath = dialogAssetPath + key + ".json";
        
        using var file = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
        if (file == null)
        {
            GD.PrintErr($"Failed to load dialog file: {filePath}");
            return null;
        }
        
        return LoadDialogFromJson(file.GetAsText());
    }

    public static Dialog LoadDialogFromJson(string json)
    {
        return JsonConvert.DeserializeObject<Dialog>(json);
    }
}