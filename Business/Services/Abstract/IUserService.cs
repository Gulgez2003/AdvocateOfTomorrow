namespace Business.Services.Abstract
{
    public interface IUserService
    {
        Task Register(RegisterDTO registerDTO);
        Task<string> Authenticate(LoginDTO loginDto);
        Task<string> GetCurrentUserName();
        Task ConfirmAdmin(string userId);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(string userId);
    }
}
