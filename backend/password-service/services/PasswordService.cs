using System;
using System.Threading.Tasks;
using password_service.infrastructures.interfaces.services;

namespace password_service.services;

public class PasswordService : IPasswordService
{
    public async Task<string> GetAllPasswords()
    {
        return "Get all passwords";
    }

    public Task<string> CreateNewPassword(string password)
    {
        throw new NotImplementedException();
    }

    public Task<string> UpdatePassword(string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<string> DeletePassword(string password)
    {
        throw new NotImplementedException();
    }
}