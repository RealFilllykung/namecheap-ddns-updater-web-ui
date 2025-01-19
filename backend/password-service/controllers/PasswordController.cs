using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using password_service.infrastructures.interfaces.services;
using password_service.models;

namespace password_service.controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("/password")]
public class PasswordController
{
    public PasswordController(IPasswordService passwordService)
    {
        _passwordService = passwordService;
    }
    
    private readonly IPasswordService _passwordService;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<string> GetPasswords()
    {
        return await _passwordService.GetAllPasswords();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public string PostPassword([FromBody] PasswordBody password)
    {
        return password.password;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public string PutPassword([FromBody] PasswordBody password)
    {
        return password.password;
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public string DeletePassword([FromBody] PasswordBody password)
    {
        return password.password;
    }
}