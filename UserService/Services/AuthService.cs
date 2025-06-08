using Microsoft.AspNetCore.Identity;
using Shared.DTOs;
using Shared.Models;
using Shared.Services;

namespace UserService.Services;

public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtService _jwtService;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        JwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Address = request.Address,
            IsOrganizer = request.IsOrganizer,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            return new AuthResponse(
                true,
                _jwtService.GenerateToken(user),
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                user.IsOrganizer,
                new List<string>()
            );
        }

        return new AuthResponse(
            false,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            false,
            result.Errors.Select(e => e.Description).ToList()
        );
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return new AuthResponse(
                false,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                false,
                new List<string> { "Invalid email or password." }
            );
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded)
        {
            return new AuthResponse(
                true,
                _jwtService.GenerateToken(user),
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                user.IsOrganizer,
                new List<string>()
            );
        }

        return new AuthResponse(
            false,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            false,
            new List<string> { "Invalid email or password." }
        );
    }
} 