namespace BookBindingPaginator;

public class PageSequenceGenerator : IPageSequenceGenerator
{
    private readonly Configuration _configuration;

    public PageSequenceGenerator(IOptions<Configuration> options)
    {
        _configuration = options.Value;
    }

    public IEnumerable<int?> GetPages(int pageCount)
    {
        var signatureCount = (int)Math.Ceiling((double)pageCount / _configuration.PagesPerSignature);
        var blankPageCount = signatureCount * _configuration.PagesPerSignature - pageCount;
        var blankPageAtStart = blankPageCount >= 4;

        return Enumerable.Range(0, signatureCount)
            .SelectMany(signature => GetSignaturePages(signature, blankPageAtStart))
            .Select<int, int?>(p => p >= 0 && p < pageCount ? p : null);
    }

    private IEnumerable<int> GetSignaturePages(int signature, bool blankPageAtStart)
    {
        var startPage = signature * _configuration.PagesPerSignature - (blankPageAtStart ? 2 : 0);
        var pages = Enumerable.Range(startPage, _configuration.PagesPerSignature).ToList();
        for (var currentSheet = 0; currentSheet < _configuration.SheetsPerSignature; currentSheet++)
        {
            var firstPage = _configuration.PagesPerSignature - currentSheet * 2 - 1;
            var secondPage = currentSheet * 2;
            var thirdPage = secondPage + 1;
            var fourthPage = firstPage - 1;

            yield return pages[firstPage];
            yield return pages[secondPage];
            yield return pages[thirdPage];
            yield return pages[fourthPage];
        }
    }
}
