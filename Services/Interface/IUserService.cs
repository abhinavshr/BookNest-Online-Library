using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IUserService
    {
        void AddUser(InsertUserDto userDto);
        List<GetAllUserDto> GetAllUsers();
        GetAllUserDto GetById(Guid id);
        void UpdateUser(Guid id, UpdateUserDto userDto);
        void DeleteUser(Guid id);
    }
}
