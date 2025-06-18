namespace Threads.BusinessLogicLayer.DTO.RegisterDTO
{
    public class UserResponse
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? ImgUrl { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }

}
