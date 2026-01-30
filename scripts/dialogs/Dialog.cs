// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System.Collections.Generic;
using Newtonsoft.Json;

public class Condition
{
    [JsonProperty("type")]
    public string Type;

    [JsonProperty("value")]
    public string Value;
}

public class Line
{
    public DialogLineBase DialogLine => DialogLineConverter.ConvertLine(this);

    [JsonProperty("type")]
    public string Type;

    [JsonProperty("condition")]
    public Condition Condition;

    [JsonProperty("nextSection")]
    public string NextSection;

    [JsonProperty("background")]
    public string Background;

    [JsonProperty("speaker")]
    public string Speaker;

    [JsonProperty("text")]
    public string Text;

    [JsonProperty("name")]
    public string Name;

    [JsonProperty("value")]
    public string Value;

    [JsonProperty("options")]
    public List<Option> Options;
}

public class Option
{
    [JsonProperty("condition")]
    public Condition Condition;

    [JsonProperty("text")]
    public string Text;

    [JsonProperty("nextSection")]
    public string NextSection;
}

public class Dialog
{
    [JsonProperty("sections")]
    public List<Section> Sections;

    [JsonProperty("startSection")]
    public string StartSection;
}

public class Section
{
    [JsonProperty("id")]
    public string Id;

    [JsonProperty("lines")]
    public List<Line> Lines;
}

