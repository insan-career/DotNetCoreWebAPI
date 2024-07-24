using System.ComponentModel;

namespace DotNetCoreWebAPI.Models;

public class Book
{
    [ReadOnly(true)]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public DateTime PublishedDate { get; set; }
}