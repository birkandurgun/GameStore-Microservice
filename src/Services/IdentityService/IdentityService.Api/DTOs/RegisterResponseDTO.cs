﻿namespace IdentityService.Api.DTOs
{
    public class RegisterResponseDTO
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
