namespace Shared.DTOs;

public record LoginRequest(string Email, string Password);
public record RegisterRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string Address,
    bool IsOrganizer
);

public record AuthResponse(
    bool Succeeded,
    string Token,
    string UserId,
    string Email,
    string FirstName,
    string LastName,
    bool IsOrganizer,
    List<string> Errors
); 