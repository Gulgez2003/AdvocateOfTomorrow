namespace AdvocateOfTomorrow.Controllers.Admin
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("registerAdmin")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            UserRegisterDTOValidator validations = new UserRegisterDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(registerDTO);
            if (validationResult.IsValid)
            {
                await _userService.Register(registerDTO);
                return Ok();
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost("loginAdmin")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            UserLoginDTOValidator validations = new UserLoginDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(loginDTO);
            if (validationResult.IsValid)
            {
                var token = _userService.Authenticate(loginDTO);

                if (token == null)
                {
                    return Unauthorized();
                }
                return Ok(new { token });
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
        }

        [HttpPost("confirmAdmin")]
        public async Task<IActionResult> ConfirmAdmin(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.ConfirmAdmin(id);
            return Ok();
        }
    }
}
