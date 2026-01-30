using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// Base class for all dialog line types
/// </summary>
public abstract class DialogLineBase
{
    [JsonProperty("type")]
    public string Type { get; set; }
}

/// <summary>
/// Conditional branching line - switches to a different section based on a condition
/// </summary>
public class SwitchLine : DialogLineBase
{
    [JsonProperty("condition")]
    public Condition Condition { get; set; }

    [JsonProperty("nextSection")]
    public string NextSection { get; set; }
}

/// <summary>
/// Unconditional jump to another section
/// </summary>
public class NextSectionLine : DialogLineBase
{
    [JsonProperty("nextSection")]
    public string NextSection { get; set; }
}

/// <summary>
/// Shows a background image
/// </summary>
public class ShowBackgroundLine : DialogLineBase
{
    [JsonProperty("background")]
    public string Background { get; set; }
}

/// <summary>
/// Displays dialog text with a speaker
/// </summary>
public class DialogLine : DialogLineBase
{
    [JsonProperty("speaker")]
    public string Speaker { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }
}

/// <summary>
/// Triggers a game event
/// </summary>
public class EventLine : DialogLineBase
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
}

/// <summary>
/// Presents multiple options to the player
/// </summary>
public class OptionsLine : DialogLineBase
{
    [JsonProperty("options")]
    public List<DialogOption> Options { get; set; }
}

/// <summary>
/// Represents a single option in an options line
/// </summary>
public class DialogOption
{
    [JsonProperty("condition")]
    public Condition Condition { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("nextSection")]
    public string NextSection { get; set; }
}

/// <summary>
/// Represents a condition for conditional logic
/// </summary>
public class DialogCondition
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
}
