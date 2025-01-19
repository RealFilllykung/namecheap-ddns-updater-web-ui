using System;
using System.Threading.Tasks;
using password_service.infrastructures.interfaces.services;
using password_service.models;
using password_service.models.responses;

namespace password_service.services;

public class PasswordService : IPasswordService
{
    public async Task<EncryptPasswordResponse> EncryptPassword(EncryptPasswordRequest request)
    {
        EncryptPasswordResponse response = new EncryptPasswordResponse();
        return response;
    }

    public async Task<DecryptPasswordResponse> DecryptPassword(DecryptPasswordRequest request)
    {
        DecryptPasswordResponse response = new DecryptPasswordResponse();
        return response;
    }
}