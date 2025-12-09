namespace CatalogService.Application.Dto_s.AlbumDto_s;

public class GetAlbumDto
{
    public string Title { get; set; } = default!;
    public int Year { get; set; }
    public string MOckData { get; set; } = "TRest mocj data";
}