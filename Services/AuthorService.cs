using BookNest.Dtos;
using BookNest.Entities;
using BookNest.Services.Interface;
using First.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookNest.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAuthor(InsertAuthorDto authorDto)
        {
            var newAuthor = new Author
            {
                AuthorId = Guid.NewGuid(),
                Name = authorDto.Name,
                Biography = authorDto.Biography
            };

            await _context.Authors.AddAsync(newAuthor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthor(Guid id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Author not found.");
            }
        }

        public async Task<List<GetAllAuthorDto>> GetAllAuthors()
        {
            return await _context.Authors
                .Select(a => new GetAllAuthorDto
                {
                    AuthorId = a.AuthorId,
                    Name = a.Name,
                    Biography = a.Biography
                })
                .ToListAsync();
        }

        public async Task<GetAllAuthorDto> GetById(Guid id)
        {
            var author = await _context.Authors
                .FirstOrDefaultAsync(a => a.AuthorId == id);

            if (author == null)
            {
                throw new Exception("Author not found.");
            }

            return new GetAllAuthorDto
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
                Biography = author.Biography
            };
        }

        public async Task UpdateAuthor(Guid id, UpdateAuthorDto authorDto)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                author.Name = authorDto.Name;
                author.Biography = authorDto.Biography;

                _context.Authors.Update(author);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Author not found.");
            }
        }
    }
}
