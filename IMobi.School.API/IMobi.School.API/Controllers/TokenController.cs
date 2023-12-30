using IMobi.School.API.Controllers.Root;
using IMobi.School.DAL.Context;
using IMobi.School.DomainModal.EnumDM;
using IMobi.School.DomainModal.v1;
using IMobi.School.ServiceModal.EnumSM;
using IMobi.School.ServiceModal.v1.AppUser;
using IMobi.School.ServiceModal.v1.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IMobi.School.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class TokenController : IMobiRootController
    {
        #region Properties
        private readonly AppDbContext _appDbContext;
        private IConfiguration _config;
        #endregion Properties

        #region Constructor
        public TokenController(AppDbContext appDbContext, IConfiguration config)
        {
            _appDbContext = appDbContext;
            _config = config;
        }
        #endregion Constructor

        #region Register User
        [HttpPost("Register")]
        public async Task<ActionResult<TokenResponseSM>> AddUser(ClientUserDM clientUserDM)
        {
            await _appDbContext.ClientUsers.AddAsync(clientUserDM);
            if (await _appDbContext.SaveChangesAsync() > 0)
                return Ok(_appDbContext.ClientUsers.FirstOrDefault());
            return BadRequest("User not added");
        }
        #endregion Register User

        #region Generate Token
        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenResponseSM>> GenerateToken(TokenRequestSM clientUserSM)
        {
            var userExists = _appDbContext.ClientUsers.FirstOrDefault(x => x.Username == clientUserSM.Username &&
                    x.Password == clientUserSM.Password &&
                    x.RoleTypeDM == (RoleTypeDM)clientUserSM.RoleTypeSM);
            if (userExists == null)
            {
                return BadRequest("User does not exist");
            }
            else
            {
                ICollection<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,clientUserSM.Username),
                    new Claim(ClaimTypes.Role,clientUserSM.RoleTypeSM.ToString()),
                    new Claim(ClaimTypes.GivenName,userExists.FirstName + " "+userExists.LastName ),
                    //new Claim(ClaimTypes.Email,userSM.EmailId),
                };
                /*  if (compId != default)
                  {
                      claims.Add(new Claim(Claim_ClientCode, innerReq.CompanyCode));
                      claims.Add(new Claim(Claim_ClientId, compId.ToString()));
                  }
                var token = await _jwtHandler.ProtectAsync(_apiConfiguration.JwtTokenSigningKey, claims, new DateTimeOffset(DateTime.Now), new DateTimeOffset(expiryDate), "TestProj");
            */



                var expiryDate = DateTime.Now.AddDays(1);
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var Sectoken = new JwtSecurityToken(
                  _config["Jwt:Issuer"],
                  _config["Jwt:Audience"],
                  claims,
                  expires: expiryDate,
                  signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
                var tokenResponse = new TokenResponseSM()
                {
                    AccessToken = token,
                    LoginUserDetails = new ClientUserSM()
                    {
                        FirstName = userExists.FirstName,
                        LastName = userExists.LastName,
                        Username = userExists.Username,
                        Password = userExists.Password,
                        RoleTypeSM = (RoleTypeSM)userExists.RoleTypeDM
                    },
                    ExpiresUtc = expiryDate,
                };
                return Ok(tokenResponse);
            }
        }
        #endregion Generate Token
    }
}
