namespace BookBindingPaginator;

public interface IPageSequenceGenerator
{
    IEnumerable<int?> GetPages(int pageCount);
}
