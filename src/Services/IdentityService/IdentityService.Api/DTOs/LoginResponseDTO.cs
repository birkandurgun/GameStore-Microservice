namespace IdentityService.Api.DTOs
{
    public class LoginResponseDTO
    {
        public bool Success { get; set; }
        public IList<string> Errors { get; set; }
        public string? Token { get; set; }
    }
}
