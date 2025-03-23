using System.Text.Json;

public interface IRepository<T>
{
    IEnumerable<T>? ReadItems();
}

