using BookNest.Dtos;
using BookNest.Entities;

namespace BookNest.Services.Interface
{
    public interface IUserService
    {
        Task AddUser(InsertUserDto userDto); 
        Task<List<GetAllUserDto>> GetAllUsers();
        Task<User> ValidateUserAsync(LoginDto dto);
        Task<GetAllUserDto> GetById(Guid id); 
        Task UpdateUser(Guid id, UpdateUserDto userDto); 
        Task DeleteUser(Guid id); 
    }
}
