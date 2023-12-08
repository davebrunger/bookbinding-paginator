namespace BookBindingPaginator;

public interface IStreamPaginator
{
    void Paginate(Stream inputStream, Stream outputStream);
}
