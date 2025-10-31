using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using SmartStockBackend.Application.Services.UserServices.InputModels;
using SmartStockBackend.Application.Services.UserServices.ViewModels;
using SmartStockBackend.Core.Entities;
using SmartStockBackend.Core.Exceptions.UserExceptions;
using SmartStockBackend.Infra;

namespace SmartStockBackend.Application.Services.UserServices
{
    public interface IUserService
    {
        public Task<AccessUserViewDto> Register(RegisterUserInputDto model);
        public Task<AccessUserViewDto> Login(LoginUserInputDto model);
        public Task<List<User>> GetAll();
        public Task<GetUserDataByIdViewDto> GetById(int id);
        public Task Update(int id, UpdateUserInputDto model);
        public Task Delete(int id);
    }
    public class UserService : IUserService
    {
        private readonly SmartStockDbContext _context;
        private readonly TokenService _tokenService;

        public UserService(SmartStockDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task Delete(int id)
        {
            var data = await _context.Users.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new UserNotFoundByIdException(id);
            _context.Users.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAll()
        {
            var data = await _context.Users.ToListAsync();
            return data;
        }

        public async Task<GetUserDataByIdViewDto> GetById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new UserNotFoundByIdException(id);
            return new GetUserDataByIdViewDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                ImageUrl = user.ImageUrl
            };
        }

        public async Task<AccessUserViewDto> Login(LoginUserInputDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email)
                ?? throw new UserNotFoundException(model.Email);
            bool passwordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
            if (!passwordValid)
                throw new UserValidationException();

            var token = _tokenService.GenerateToken(user);
            return new AccessUserViewDto
            {
                Name = user.Name,
                ImageUrl = user.ImageUrl,
                Token = token
            };
        }

        public async Task<AccessUserViewDto> Register(RegisterUserInputDto model)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            User user = new()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                ImageUrl = model.ImageUrl,
                PasswordHash = passwordHash
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var token = _tokenService.GenerateToken(user);
            return new AccessUserViewDto
            {
                Name = user.Name,
                ImageUrl = user.ImageUrl,
                Token = token
            };
        }

        public async Task Update(int id, UpdateUserInputDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new UserNotFoundByIdException(id);
            user.Name = model.Name;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.ImageUrl = model.ImageUrl;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
