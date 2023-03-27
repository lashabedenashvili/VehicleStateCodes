using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleStateCodes.Data.Domein.Data;
using VehicleStateCodes.Data.Domein.Domein;
using VehicleStateCodes.DataBase.UnitOfWork;
using VehicleStateCodes.Infrastructure.ApiServiceResponse;
using VehicleStateCodes.Infrastructure.Dto.UserDto;

namespace VehicleStateCodes.Application.Services.UserService
{
    public class UserService:IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;            
        }
        public async Task<ApiResponse<string>> LogIn(UserLogInDto request)
        {
            var userDb =  await _unitOfWork.User.Where(x => x.Email == request.Email)
                                               .Include(e => e.UserPasswordHistory
                                               .Where(e => e.IsActive == true))
                                               .FirstOrDefaultAsync();
            var _pasword = userDb?.UserPasswordHistory.FirstOrDefault();

            if (userDb == null || !userDb.IsActive) return new BadApiResponse<string>("User is not active");
            
            var checkpassword = VerifyPasswordHash(request.Password, _pasword.PasswordHash, _pasword.PasswordSalt);
            
            if (checkpassword)
            {
                return new SuccessApiResponse<string>(CreateToken(userDb));
            }
            else return new BadApiResponse<string>("incorrect credentials!");


        }
        public async Task<ApiResponse<string>> Registration(UserRegistrationDto request)
        {           
            var userDb = await _unitOfWork.User.Where(x=>x.Email==request.Email).FirstOrDefaultAsync();          

            if (userDb == null)
            {   
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var userId = new User
                {
                    Email = request.Email,
                    BirthDate = request.BirthDate,
                    Cityzen = request.Cityzen,
                    Gender = request.Gender,
                    Name = request.Name,
                    SurName = request.SurName,
                    PhoneNumber = request.PhoneNumber,
                    IsActive = true
                };
                var userPassHistory = new UserPasswordHistory
                {
                    User = userId,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    CreateTime = DateTime.Now,
                    IsActive = true,
                };
                await _unitOfWork.UserPasswordHistory.AddAsync(userPassHistory);
                await _unitOfWork.SaveChangesAsync();

                return new SuccessApiResponse<string>("Registration is successfuly");
            }
            return new BadApiResponse<string>("Email Is Already Exist");
        }
        public async Task<ApiResponse<UserUpdateDto>> Update(int id, UserUpdateDto request)
        {
            var userDb = await _unitOfWork.User.GetAsync(x => x.Id == id && x.IsActive == true);
            if (userDb != null)
            {
                userDb.Name = request.Name == null ? userDb.Name : request.Name;
                userDb.SurName = request.SurName == null ? userDb.SurName : request.SurName;
                userDb.BirthDate = request.BirthDate.HasValue ? request.BirthDate : userDb.BirthDate;
                userDb.PhoneNumber = request.PhoneNumber == null ? userDb.PhoneNumber : request.PhoneNumber;
                await _unitOfWork.User.Update(userDb);
                await _unitOfWork.User.SaveChangesAsync();
                //return Updated User
                return new SuccessApiResponse<UserUpdateDto>(new UserUpdateDto
                {
                    Name = userDb.Name,
                    SurName = userDb.SurName,
                    BirthDate = userDb.BirthDate,
                    PhoneNumber = userDb.PhoneNumber,
                }
                    );
            }
            return new BadApiResponse<UserUpdateDto>("User Does not exist");
        }
        public async Task<ApiResponse<string>> UpdatePassword(ChangePasswordDto request, int id)
        {
            var userDb = await _unitOfWork.User.Where(x => x.Id == id && x.IsActive == true)
                                               .Include(e => e.UserPasswordHistory).FirstOrDefaultAsync();
            var checkOldPassword = userDb.UserPasswordHistory.Where(x => x.IsActive == true).FirstOrDefault();
            var verifyPassword = VerifyPasswordHash(request.OldPassword, checkOldPassword.PasswordHash, checkOldPassword.PasswordSalt);
            if (verifyPassword)
            {
                if (userDb.UserPasswordHistory.Any(e => VerifyPasswordHash(request.NewPassword, e.PasswordHash, e.PasswordSalt)))
                    return new BadApiResponse<string>("This password matchs the old one");

                var MakeInActive = userDb.UserPasswordHistory.Where(e => e.IsActive).First();
                MakeInActive.IsActive = false;
                await _unitOfWork.UserPasswordHistory.Update(MakeInActive);
                CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

                if (userDb.UserPasswordHistory.Count() >= 3)
                {
                    var updateOldRow = userDb.UserPasswordHistory.OrderBy(x => x.UpdateTime == null).FirstOrDefault() == null ? userDb.UserPasswordHistory.OrderBy(x => x.CreateTime).First() : userDb.UserPasswordHistory.OrderBy(x => x.UpdateTime).First();
                    updateOldRow.PasswordHash = passwordHash;
                    updateOldRow.PasswordSalt = passwordSalt;
                    updateOldRow.IsActive = true;
                    updateOldRow.UpdateTime = DateTime.Now;
                    await _unitOfWork.UserPasswordHistory.Update(updateOldRow);
                }
                else
                {
                    var createNewRow = new UserPasswordHistory
                    {
                        UserId = userDb.Id,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        IsActive = true,
                        CreateTime = DateTime.Now
                    };
                    await _unitOfWork.UserPasswordHistory.AddAsync(createNewRow);
                }
                await _unitOfWork.UserPasswordHistory.SaveChangesAsync();
                return new SuccessApiResponse<string>("", "Updated successfully!");
            }
            return new BadApiResponse<string>("Incorect Password");
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            //check Hash and salt with Null
            if (passwordHash == null && passwordSalt == null)
                return false;
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email.ToString())
            };          

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = cred
            };
            JwtSecurityTokenHandler tokenHendler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHendler.CreateToken(tokenDescriptor);

            return tokenHendler.WriteToken(token);
        }
    }
}
