using BookNest.Dtos;
using BookNest.Entities;
using BookNest.Services.Interface;
using First.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace First.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Async method to add a user
        public async Task AddUser(InsertUserDto userDto)
        {
            try
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == userDto.Email.ToLower());
                if (existingUser != null)
                    throw new Exception("Email already exists.");

                string membershipId;
                do
                {
                    membershipId = new Random().Next(1000, 9999).ToString();
                } while (await _context.Users.AnyAsync(u => u.MembershipID == membershipId));

                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    Name = userDto.Name,
                    Email = userDto.Email,
                    PasswordHash = userDto.Password,
                    Role = "User",
                    MembershipID = membershipId,
                    RegistrationDate = DateTime.UtcNow,
                    OrderCount = 0,
                    IsActive = true
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding user: " + ex.Message);
            }
        }

        public async Task AddStaff(InsertUserDto userDto)
        {
            try
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == userDto.Email.ToLower());
                if (existingUser != null)
                    throw new Exception("Email already exists.");

                string membershipId;
                do
                {
                    membershipId = new Random().Next(1000, 9999).ToString();
                } while (await _context.Users.AnyAsync(u => u.MembershipID == membershipId));

                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    Name = userDto.Name,
                    Email = userDto.Email,
                    PasswordHash = userDto.Password,
                    Role = "Staff",
                    MembershipID = membershipId,
                    RegistrationDate = DateTime.UtcNow,
                    OrderCount = 0,
                    IsActive = true
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding user: " + ex.Message);
            }
        }

        public async Task<User> ValidateUserAsync(LoginDto dto)
        {
            Console.WriteLine($"Attempting to find user with email: {dto.Email}");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());

            if (user == null)
            {
                Console.WriteLine($"No user found with email: {dto.Email}");
            }
            else
            {
                Console.WriteLine($"User found: {user.Name}, Role: {user.Role}, UserID: {user.UserId}");
            }

            return user;
        }



        public async Task DeleteUser(Guid id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
                if (user == null)
                    throw new Exception("User Not Found");

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting user: " + ex.Message);
            }
        }

        public async Task<List<GetAllUserDto>> GetAllUsers()
        {
            try
            {
                var users = await _context.Users
                    .Where(u => u.IsActive)
                    .Select(u => new GetAllUserDto
                    {
                        Name = u.Name,
                        Email = u.Email,
                        Role = u.Role,
                        MembershipID = u.MembershipID,
                        RegistrationDate = u.RegistrationDate,
                        OrderCount = u.OrderCount,
                        IsActive = u.IsActive
                    })
                    .ToListAsync();

                if (users == null || users.Count == 0)
                    throw new Exception("No active users found");

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users: " + ex.Message);
            }
        }


        public async Task<List<GetAllUserDto>> GetAllStaff()
        {
            try
            {
                var users = await _context.Users
                    .Where(u => u.IsActive && u.Role == "Staff")
                    .ToListAsync();

                if (users == null || !users.Any())
                    throw new Exception("No active staff users found");

                var result = users.Select(u => new GetAllUserDto
                {
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role,
                    MembershipID = u.MembershipID,
                    RegistrationDate = u.RegistrationDate,
                    OrderCount = u.OrderCount,
                    IsActive = u.IsActive
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving staff users: " + ex.Message);
            }
        }


        // Async method to get a user by Id
        public async Task<GetAllUserDto> GetById(Guid id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

                if (user == null)
                    throw new Exception("User Not Found");

                return new GetAllUserDto
                {
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                    MembershipID = user.MembershipID,
                    RegistrationDate = user.RegistrationDate,
                    OrderCount = user.OrderCount,
                    IsActive = user.IsActive
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving user: " + ex.Message);
            }
        }

        // Async method to update a user
        public async Task UpdateUser(Guid id, UpdateUserDto userDto)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

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
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user: " + ex.Message);
            }
        }
    }
}
