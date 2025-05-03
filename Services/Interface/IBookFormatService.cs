using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IBookFormatService
    {
        void AddFormat(InsertBookFormatDto formatDto);
        List<GetAllBookFormatDto> GetAllFormats();
    }
}
