using Domain.DTOs.AuthDTOs;
using Domain.DTOs.RegisterDTOs;
using Domain.Models;
using Domain.Services;
using Infrastructure.Database;

namespace Application.Services.Cycles
{
    public class AuthDatabaseService : IAuthDatabaseService
    {
        private readonly pjatkContext _pjatkContext;

        public AuthDatabaseService(pjatkContext pjatkContext)
        {
            _pjatkContext = pjatkContext;
        }

        public async Task Create(RegisterDTO registerDTO)
        {
            var farmer = new Farmer
            {
                Name = registerDTO.Name,
                Surname = registerDTO.Surname,
                FarmerColor = "Not Implemented",
                Email = registerDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password)
            };

            await _pjatkContext.Farmers.AddAsync(farmer);
            await _pjatkContext.SaveChangesAsync();
        }

        public Farmer Login(LoginDTO loginDTO)
        {
            var farmer = _pjatkContext.Farmers.FirstOrDefault(x => x.Email == loginDTO.Email);

            if (farmer != null && BCrypt.Net.BCrypt.Verify(loginDTO.Password, farmer.Password)){
                return farmer;
            }
            return null;
        }
    }
}
