namespace BookBindingPaginator;

public class Paginator
{
    private readonly Configuration _configuration;
    private readonly IStreamPaginator _streamPaginator;

    public Paginator(IOptions<Configuration> options, IStreamPaginator streamPaginator)
    {
        _configuration = options.Value;
        _streamPaginator = streamPaginator;
    }

    public void Paginate()
    {
        using var inputStream = File.OpenRead(_configuration.InputFile);
        var folder = Path.GetDirectoryName(_configuration.InputFile)!;
        var outputPath = Path.Combine(folder, Path.ChangeExtension(_configuration.OutputFile, ".pdf"));
        using var outputStream = File.Create(outputPath);
        _streamPaginator.Paginate(inputStream, outputStream);
    }
}
