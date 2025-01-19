using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using password_service.infrastructures.interfaces.services;
using password_service.models;
using password_service.models.responses;

namespace password_service.controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("/password/[action]")]
public class PasswordController
{
    public PasswordController(IPasswordService passwordService)
    {
        _passwordService = passwordService;
    }
    
    private readonly IPasswordService _passwordService;

    [HttpPost]
    public async Task<EncryptPasswordResponse> EncryptPassword([FromBody] EncryptPasswordRequest request)
    {
        return await _passwordService.EncryptPassword(request);
    }

    [HttpPost]
    public async Task<DecryptPasswordResponse> DecryptPassword([FromBody] DecryptPasswordRequest request)
    {
        return await _passwordService.DecryptPassword(request);
    }
}