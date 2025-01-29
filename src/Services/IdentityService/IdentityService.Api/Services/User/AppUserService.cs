using FluentValidation;
using FluentValidation.Results;
using IdentityService.Api.DTOs;
using IdentityService.Api.Entities.Identity;
using IdentityService.Api.Enums;
using IdentityService.Api.Services.Token;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IdentityService.Api.Services.User
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        IValidator<RegisterDTO> _registerValidator;
        IValidator<LoginDTO> _loginValidator;

        public AppUserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IValidator<RegisterDTO> registerValidator, IValidator<LoginDTO> loginValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        public async Task<RegisterResponseDTO> RegisterAsync(RegisterDTO registerDto)
        {

            ValidationResult validationResult = await _registerValidator.ValidateAsync(registerDto);
            if (!validationResult.IsValid)
            {
                return new RegisterResponseDTO
                {
                    Success = false,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
                return new RegisterResponseDTO
                {
                    Success = false,
                    Errors = new List<string> { "Email already exists" }
                };

            var user = new AppUser
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                UserName = registerDto.Username,
                BirthDate = registerDto.BirthDate,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return new RegisterResponseDTO
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description)
                };

            await _userManager.AddToRoleAsync(user, Role.User.ToString());

            return new RegisterResponseDTO { Success = true };
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginDto)
        {
            ValidationResult validationResult = await _loginValidator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                return new LoginResponseDTO
                {
                    Success = false,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return new LoginResponseDTO { Success = false, Errors = new List<string> { "Incorrect email or password." } };

            var result = await _signInManager.CheckPasswordSignInAsync
                (user,
                loginDto.Password,
                lockoutOnFailure: false);

            if (!result.Succeeded)
                return new LoginResponseDTO { Success = false,
                    Errors = new List<string> { 
                        result.IsLockedOut 
                        ? "Account locked. Please try again later." 
                        : "Incorrect email or password." }
                };

            var roles = await _userManager.GetRolesAsync(user);

            var token = await _tokenService.GenerateTokenAsync(user.Id.ToString(), user.Email, roles);
            return new LoginResponseDTO { Success = true, Token = token };

        }

    }
}
