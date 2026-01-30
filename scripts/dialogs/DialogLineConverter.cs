using System;

/// <summary>
/// Converts generic Line objects to specific DialogLineBase types
/// </summary>
public static class DialogLineConverter
{
    public static DialogLineBase ConvertLine(Line line)
    {
        if (line == null)
            throw new ArgumentNullException(nameof(line));

        switch (line.Type?.ToLower())
        {
            case "switch":
                return new SwitchLine
                {
                    Type = line.Type,
                    Condition = line.Condition,
                    NextSection = line.NextSection
                };

            case "nextsection":
                return new NextSectionLine
                {
                    Type = line.Type,
                    NextSection = line.NextSection
                };

            case "showbackground":
                return new ShowBackgroundLine
                {
                    Type = line.Type,
                    Background = line.Background
                };

            case "dialog":
                return new DialogLine
                {
                    Type = line.Type,
                    Speaker = line.Speaker,
                    Text = line.Text
                };

            case "event":
                return new EventLine
                {
                    Type = line.Type,
                    Name = line.Name,
                    Value = line.Value
                };

            case "options":
                return new OptionsLine
                {
                    Type = line.Type,
                    Options = line.Options != null 
                        ? line.Options.ConvertAll(o => new DialogOption
                        {
                            Condition = o.Condition,
                            Text = o.Text,
                            NextSection = o.NextSection
                        })
                        : null
                };

            default:
                throw new ArgumentException($"Unknown dialog line type: {line.Type}");
        }
    }
}
