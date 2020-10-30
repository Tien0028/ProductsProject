using BackendSolution.Entity;
using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApplicationSolution.Infrastructure.Data.Helpers
{
    public interface IAuthenticationHelper
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
        string GenerateToken(User user);
    }
}
