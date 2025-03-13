using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly JwtService _jwtService;

    public AuthController(UserService userService, JwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userService.GetUserByEmailAsync(request.Email);
        if (user == null || user.PasswordHash != _userService.HashPassword(request.Password))
        {
            return Unauthorized(new { message = "Usuário ou senha inválidos" });
        }

        var token = _jwtService.GenerateToken(user.Username, user.Role);
        return Ok(new { Token = token });
    }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}