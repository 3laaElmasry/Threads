namespace Threads.BusinessLogicLayer.DTO.RegisterDTO
{
    public class RegisterResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string Email = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
    }

}
