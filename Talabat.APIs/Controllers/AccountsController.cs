using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOS;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{

    public class AccountsController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager
            , SignInManager<AppUser> signInManager,
            ITokenServices tokenServices , IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
            _mapper = mapper;
        }
        //Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {

            if(CheckEmailExists(model.Email).Result.Value)
            {
                return BadRequest(new Apiresponse(400, "Email ALready Exist"));
            }


            var User = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
            };
            var Result = await _userManager.CreateAsync(User, model.Password);
            if (!Result.Succeeded) return BadRequest(new Apiresponse(400));

            var ReturnedUser = new UserDTO()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenServices.CreateTokenAsync(User, _userManager)
            };
            return Ok(ReturnedUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> login(loginDto model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User is null) return Unauthorized(new Apiresponse(401));
            var Result = await _signInManager.CheckPasswordSignInAsync(User, model.password, false);
            if (!Result.Succeeded) return Unauthorized(new Apiresponse(401));
            return Ok(new UserDTO()
            {

                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenServices.CreateTokenAsync(User, _userManager)

            });
        }
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async  Task<ActionResult<UserDTO>> GetCurrentUser()
            {

           var Email= User.FindFirstValue(ClaimTypes.Email);
            var user =await _userManager.FindByEmailAsync(Email);

            var Returnedobject = new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenServices.CreateTokenAsync(user , _userManager)
            };
           return Ok(Returnedobject);
            }

        [Authorize]
        [HttpGet("Address")]
        public async  Task<ActionResult<AddressDto>> GetCurrentUserAdress()
        {
            //var Email = User.FindFirstValue(ClaimTypes.Email);
            //var user = await _userManager.FindByEmailAsync(Email);

            var user = await _userManager.FindUserWithAddressAsync(User);
            var mappedadress= _mapper.Map<Address,AddressDto>(user.Address);
            return Ok(mappedadress);

        }
        [Authorize]
        [HttpPut("Address")]
        public async Task <ActionResult<AddressDto>>UpdateAddress(AddressDto UpdatedAddress)
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            var mappedAdress = _mapper.Map<AddressDto, Address>(UpdatedAddress);
            mappedAdress.Id =user.Address.Id;
            user.Address = mappedAdress;
          var Result= await _userManager.UpdateAsync(user);
            if(!Result.Succeeded) return BadRequest(new Apiresponse(400));
            return Ok(UpdatedAddress);
        }

       
        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string Email)
        {
           return await _userManager.FindByEmailAsync(Email) is not null;

        }
    }
}
