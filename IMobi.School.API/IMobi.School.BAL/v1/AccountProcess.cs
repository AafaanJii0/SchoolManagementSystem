using AutoMapper;
using IMobi.School.BAL.Base;
using IMobi.School.DAL.Context;
using IMobi.School.DomainModal.v1;
using IMobi.School.DomainModal.v1.AppUser;
using IMobi.School.ServiceModal.v1.AppUser;
using IMobi.School.ServiceModal.v1.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static IMobi.School.BAL.v1.AccountProcess;
using IMobi.School.DomainModal.EnumDM;
using IMobi.School.ServiceModal.EnumSM;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace IMobi.School.BAL.v1
{
    public class AccountProcess : BaseProcess
    {
        private UserManager<ApplicationUserDM> _userManager;
        //private readonly SignInManager<ApplicationUserDM> _signInManager;
        private readonly RoleManager<DomainModal.v1.AppUser.RoleTypeDM> _roleManager;
        private IConfiguration _config;
        public AccountProcess(AppDbContext appDbContext, IMapper mapper,
            UserManager<ApplicationUserDM> userManager, 
            //SignInManager<ApplicationUserDM> signInManager, 
            RoleManager<DomainModal.v1.AppUser.RoleTypeDM> roleManager,
             IConfiguration config) : base(appDbContext, mapper)
        {
            _userManager = userManager;
           //_signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<ApiResponeSM<IdentityResult, ApplicationUserDM>> RegisterUser(LoginUser loginUser)
        {
            ApplicationUserDM applicationUserDM = new ApplicationUserDM();
            applicationUserDM.Id = Guid.NewGuid().ToString();//appuser id
            applicationUserDM.UserName = loginUser.UserName;
          /*  applicationUserDM.FullName = loginUser.FullName;
            applicationUserDM.PhoneNumber = loginUser.PhoneNumber;
            applicationUserDM.Email = loginUser.Email;
            applicationUserDM.FirstName = loginUser.FirstName;
            applicationUserDM.MiddleName = loginUser.MiddleName;
            applicationUserDM.LastName = loginUser.LastName;*/
            var hashPw = _userManager.PasswordHasher.HashPassword(applicationUserDM, loginUser.Password);
            //applicationUserDM.PasswordHash = hashPw;
            DomainModal.v1.AppUser.RoleTypeDM roleTypeDM = new DomainModal.v1.AppUser.RoleTypeDM();
            //roleTypeDM.RoleType = loginUser.RoleType;
            roleTypeDM.Name = loginUser.RoleType.ToString();
            roleTypeDM.NormalizedName = loginUser.RoleType.ToString();

            try
            {
                var result = await _userManager.CreateAsync(applicationUserDM, loginUser.Password);
                if (!result.Succeeded)
                {
                    return new ApiResponeSM<IdentityResult, ApplicationUserDM>()
                    {
                        IdenityResult = result,
                        SuccessData = null
                    };
                }
                else
                {
                    var roleExistsInDb = await _roleManager.RoleExistsAsync(loginUser.RoleType.ToString());
                    if (!roleExistsInDb)
                    {
                        //var identityRoleRes = await _roleManager.CreateAsync(roleTypeDM);
                        //if (identityRoleRes.Succeeded)
                        {
                            _appDbContext.Roles.Add(roleTypeDM);
                            //_appDbContext.ApplicationUsers.Add(applicationUserDM);
                            await _appDbContext.SaveChangesAsync();
                            return new ApiResponeSM<IdentityResult, ApplicationUserDM>()
                            {
                                //IdenityResult = identityRoleRes,
                                //SuccessData = await _appDbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName == loginUser.UserName)
                            };
                        }
                    }

                    ClientUserDM clientUserDM = new ClientUserDM()
                    {
                      /*  FirstName = loginUser.FirstName,
                        LastName = loginUser.LastName,
                        Username = loginUser.UserName,
                        Password = hashPw,
                        RoleTypeDM = loginUser.RoleType*/
                    };
                    _appDbContext.ClientUsers.Add(clientUserDM);
                    await _appDbContext.SaveChangesAsync();

                    return new ApiResponeSM<IdentityResult, ApplicationUserDM>()
                    {
                        IdenityResult = null,
                        //SuccessData = await _appDbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName == loginUser.UserName),
                        Message = "Success"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponeSM<IdentityResult, ApplicationUserDM>()
                {
                    IdenityResult = null,
                    SuccessData = null,
                    IsException = true,
                    Exception = ex,
                    InnerException = ex.InnerException,
                    Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message
                };
            }
        }

        public async Task<ApiResponeSM<IdentityResult, TokenResponseSM>> Login(TokenRequestSM tokenRequestSM)
        {
            if (tokenRequestSM == null) {
                return new ApiResponeSM<IdentityResult, TokenResponseSM>()
                {
                    Message = "Incoming data is null"
                };
            }

            var identityUser = await _userManager.FindByNameAsync(tokenRequestSM.Username);
            if (identityUser == null)
            {
                return new ApiResponeSM<IdentityResult, TokenResponseSM>()
                {
                   // invalid username
                    Message = $"Invalid username or password"
                };
            }

            var result = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, tokenRequestSM.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return new ApiResponeSM<IdentityResult, TokenResponseSM>()
                {
                    //invalid password
                    Message = $"Invalid username or password"
                };
            }

            //var res = await GenerateToken(tokenRequestSM);
                return new ApiResponeSM<IdentityResult, TokenResponseSM>()
                {
                    //SuccessData  = res
                };
            
        }
       /* private async Task<TokenResponseSM> GenerateToken(TokenRequestSM clientUserSM)
        {
            var user = await _userManager.FindByNameAsync(clientUserSM.Username);
            var userExists = await _appDbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName == clientUserSM.Username &&
                    x.PasswordHash == user.PasswordHash);
            var roleType = _userManager.GetRolesAsync(user);
            
            if (userExists == null)
            {
                throw new Exception("User does not exist") { };
            }
            else if(user != null && userExists != null && roleType == clientUserSM.RoleTypeSM.ToString()) { }
            {
                ICollection<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,clientUserSM.Username),
                    new Claim(ClaimTypes.Role,clientUserSM.RoleTypeSM.ToString()),
                    new Claim(ClaimTypes.GivenName,userExists.FirstName + " "+userExists.LastName ),
                    //new Claim(ClaimTypes.Email,userSM.EmailId),
                };
                *//*  if (compId != default)
                  {
                      claims.Add(new Claim(Claim_ClientCode, innerReq.CompanyCode));
                      claims.Add(new Claim(Claim_ClientId, compId.ToString()));
                  }
                var token = await _jwtHandler.ProtectAsync(_apiConfiguration.JwtTokenSigningKey, claims, new DateTimeOffset(DateTime.Now), new DateTimeOffset(expiryDate), "TestProj");
            *//*



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
                return tokenResponse;
            }
        }*/
        public class  ApiResponeSM<TIdentityResult, TSuccessData>
        {
            public TIdentityResult? IdenityResult { get; set; }
            public TSuccessData? SuccessData { get; set; }
            public bool IsException { get; set; } = false;
            public Exception? Exception { get; set; }
            public Exception? InnerException { get; set; }
            public string? Message { get; set; } = "";

        }
    }
}
