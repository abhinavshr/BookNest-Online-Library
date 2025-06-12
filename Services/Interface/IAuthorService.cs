using BookNest.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookNest.Services.Interface
{
    public interface IAuthorService
    {
        Task AddAuthor(InsertAuthorDto authorDto); 
        Task<List<GetAllAuthorDto>> GetAllAuthors();
        Task<GetAllAuthorDto> GetById(Guid id);
        Task UpdateAuthor(Guid id, UpdateAuthorDto authorDto);
        Task DeleteAuthor(Guid id);
    }
}
