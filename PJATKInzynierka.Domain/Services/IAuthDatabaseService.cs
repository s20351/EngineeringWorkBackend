using Domain.DTOs.AuthDTOs;
using Domain.DTOs.RegisterDTOs;
using Domain.Models;

namespace Domain.Services
{
    public interface IAuthDatabaseService
    {
        public Task Create(RegisterDTO registerDTO);
        public Farmer Login(LoginDTO loginDTO);
    }
}
