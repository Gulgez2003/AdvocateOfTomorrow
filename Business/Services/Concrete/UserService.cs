namespace Business.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly string key;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        public UserService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            this.key = configuration.GetSection("JWTKey").ToString();
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        public async Task<string> Authenticate(LoginDTO loginDto)
        {
            User user = await _userRepository.GetAsync(u => u.Email == loginDto.Email);
            var result = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

            if (user != null && result)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(key);

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Email, loginDto.Email),
                    }),

                    Expires = DateTime.UtcNow.AddHours(1),

                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha384Signature
                    )
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }

            throw new Exception("Email or Password is incorrect");
        }


        public async Task Register(RegisterDTO registerDTO)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);

            var user = new User
            {
                Id = ObjectId.GenerateNewId(),
                FullName = registerDTO.FullName,
                Email = registerDTO.Email,
                PasswordHash = passwordHash
            };

            await _userRepository.CreateAsync(user);
        }

        public async Task<string> GetCurrentUserName()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new Exception("User ID not found in token.");
            }

            var user = await _userRepository.GetAsync(u => u.Id == ObjectId.Parse(userId));
            if (user == null)
            {
                throw new NotFoundException(Messages.UserNotFound);
            }

            return user.FullName;
        }
        public async Task ConfirmAdmin(string userId)
        {
            var user = await _userRepository.GetAsync(u => u.Id == ObjectId.Parse(userId));

            if (user == null)
            {
                throw new NotFoundException(Messages.UserNotFound);
            }
            user.IsAdmin = true;

            await _userRepository.UpdateAsync(user);
        }

        public async Task<List<User>> GetAllAsync()
        {
            List<User> users = await _userRepository.GetAllAsync();
            if (users.Count == 0)
            {
                throw new NotFoundException(Messages.UserNotFound);
            }

            var user = users.Select(user => new User
            {
                FullName = user.FullName,
                Email = user.Email
            }).ToList();

            return user;
        }
        public async Task<User> GetByIdAsync(string userId)
        {
            User user = await _userRepository.GetAsync(u => u.Id == ObjectId.Parse(userId));
            if (user == null)
            {
                throw new NotFoundException(Messages.UserNotFound);
            }
            var userDTO = new User
            {
                FullName = user.FullName,
                Email = user.Email
            };
            return userDTO;
        }
    }
}
