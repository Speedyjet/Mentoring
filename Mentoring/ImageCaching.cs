public class ImageCaching
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }

    public ImageCaching()
    {
        Console.WriteLine("here will be caching logic");
    }
}