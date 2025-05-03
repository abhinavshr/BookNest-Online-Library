using BookNest.Dtos;
using BookNest.Entities;
using BookNest.Services.Interface;
using First.Data;

namespace First.Services
{
    public class UserService : IUserService
    {
        public readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddUser(InsertUserDto userDto)
        {
            try
            {
                var user = new User
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    PasswordHash = userDto.Password,
                    Role = userDto.Role,
                    MembershipID = userDto.MembershipID,
                    RegistrationDate = userDto.RegistrationDate,
                    OrderCount = userDto.OrderCount,
                    IsActive = userDto.IsActive
                };

                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding user: " + ex.Message);
            }
        }

        public void DeleteUser(Guid id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == id);
                if (user == null)
                    throw new Exception("User Not Found");

                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting user: " + ex.Message);
            }
        }

        public List<GetAllUserDto> GetAllUsers()
        {
            try
            {
                var users = _context.Users.Where(u => u.IsActive).ToList();

                if (users == null || !users.Any())
                    throw new Exception("No active users found");

                var result = new List<GetAllUserDto>();

                foreach (var u in users)
                {
                    result.Add(new GetAllUserDto
                    {
                        Name = u.Name,
                        Email = u.Email,
                        Role = u.Role,
                        MembershipID = u.MembershipID,
                        RegistrationDate = u.RegistrationDate,
                        OrderCount = u.OrderCount,
                        IsActive = u.IsActive
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users: " + ex.Message);
            }
        }

        public GetAllUserDto GetById(Guid id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == id);

                if (user == null)
                    throw new Exception("User Not Found");

                var result = new GetAllUserDto()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                    MembershipID = user.MembershipID,
                    RegistrationDate = user.RegistrationDate,
                    OrderCount = user.OrderCount,
                    IsActive = user.IsActive
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving user: " + ex.Message);
            }
        }

        public void UpdateUser(Guid id, UpdateUserDto userDto)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == id);

                if (user == null)
                    throw new Exception("User Not Found");

                user.Name = userDto.Name;
                user.Email = userDto.Email;
                user.Role = userDto.Role;
                user.MembershipID = userDto.MembershipID;
                user.RegistrationDate = userDto.RegistrationDate;
                user.OrderCount = userDto.OrderCount;
                user.IsActive = userDto.IsActive;

                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user: " + ex.Message);
            }
        }
    }
}
