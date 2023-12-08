namespace BookBindingPaginator;

public class Configuration
{
    public string InputFile { get; init; } = null!;
    public string OutputFile { get; init; } = null!;
    public int SheetsPerSignature { get; init; }

    public int PagesPerSignature => SheetsPerSignature * 4;
}
