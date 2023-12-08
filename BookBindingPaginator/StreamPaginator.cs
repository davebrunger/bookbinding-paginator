namespace BookBindingPaginator;

public class StreamPaginator : IStreamPaginator
{
    private readonly IPageSequenceGenerator _pageSequenceGenerator;

    public StreamPaginator(IPageSequenceGenerator pageSequenceGenerator)
    {
        _pageSequenceGenerator = pageSequenceGenerator;
    }

    public void Paginate(Stream inputStream, Stream outputStream)
    {
        using var inputDocument = PdfReader.Open(inputStream, PdfDocumentOpenMode.Import);

        using var outputDocument = new PdfDocument();

        outputDocument.Version = inputDocument.Version + 1;
        outputDocument.Info.Title = inputDocument.Info.Title;
        outputDocument.Info.Creator = inputDocument.Info.Creator;
        outputDocument.Info.Author = inputDocument.Info.Author;
        outputDocument.Info.Subject = inputDocument.Info.Subject;
        outputDocument.Info.Keywords = inputDocument.Info.Keywords;
        outputDocument.Info.CreationDate = inputDocument.Info.CreationDate;
        outputDocument.Info.ModificationDate = DateTime.Today;

        foreach (var page in _pageSequenceGenerator.GetPages(inputDocument.PageCount))
        {
            if (page.HasValue)
            {
                outputDocument.AddPage(inputDocument.Pages[page.Value]);
            }
            else
            {
                outputDocument.AddPage();
            }
        }

        outputDocument.Save(outputStream);
    }
}
