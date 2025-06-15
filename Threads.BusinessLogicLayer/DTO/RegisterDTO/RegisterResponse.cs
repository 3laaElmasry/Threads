namespace Threads.BusinessLogicLayer.DTO.RegisterDTO
{
    public class RegisterResponse
    {
        public string PersonName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
    }

}
