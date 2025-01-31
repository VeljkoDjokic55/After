using AutoMapper;
using AFTER.BLL.Services.Interfaces;
using AFTER.DAL.Model;
using AFTER.DAL.UOWs.Interfaces;
using AFTER.Shared.Common;
using AFTER.Shared.Constants;
using AFTER.Shared.DTOs.Pagination;
using AFTER.Shared.DTOs.User.DataIn;
using AFTER.Shared.DTOs.User.DataOut;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFTER.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IMD5Service _md5Service;
        private readonly IEmailService _emailService;


        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IMD5Service md5Service, IEmailService emailService)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _md5Service = md5Service;
            _emailService = emailService;
        }

        public async Task<ResponsePackage<string>> ForgotPassword(string email)
        {
            ResponsePackage<string> retval = await this.GetResetCodeForEmail(email);
            if (retval.Status != ResponseStatus.OK)
                return retval;

            var userResponse = await this.GetByEmail(email);
            EmailData emailData = new EmailData()
            {
                Subject = "Reset password code",
                Content = $"Hello {userResponse.Data.FirstName} {userResponse.Data.LastName},<br> You recently requested to reset the password for your AFTER account. <br> Your code for reset passwotd is <b>{retval.Data}</b> .",
                To = email,
                IsContentHtml = true,
            };
            await _emailService.SendMailAsync(emailData);

            return retval;
        }

        public async Task<ResponsePackage<UserDto>> Get(string email, string password)
        {
            var user = await _uow.GetUserRepository().GetByEmail(email);
            if (user == null || !_md5Service.VerifyMd5Hash(password, user.Password))
                return new ResponsePackage<UserDto>() { Status = ResponseStatus.NotFound, Message = "Invalid email or password." };

            return new ResponsePackage<UserDto>() { Data = _mapper.Map<UserDto>(user) };
        }

        public async Task<ResponsePackage<UserDto>> GetByEmail(string email)
        {
            var user = await _uow.GetUserRepository().GetByEmail(email);
            if (user == null)
                return new ResponsePackage<UserDto>() { Status = ResponseStatus.NotFound, Message = "Invalid email or password." };


            return new ResponsePackage<UserDto>() { Data = _mapper.Map<UserDto>(user) };
        }

        public async Task<ResponsePackage<string>> GetResetCodeForEmail(string email)
        {
            var user = await _uow.GetUserRepository().GetByEmail(email);
            if (user == null)
                return new ResponsePackage<string>() { Status = ResponseStatus.NotFound, Message = "User doesn`t exist." };

            Random generator = new Random();
            string code = generator.Next(0, 1000000).ToString("D6");

            user.ResetCode = code;
            await _uow.CompleteAsync();
            return new ResponsePackage<string>() { Status = ResponseStatus.OK, Message = "Reset code sent to email.", Data = code };
        }

        public async Task<ResponsePackage<string>> ResetPassword(string email, string password, string code)
        {
            var user = await _uow.GetUserRepository().GetByEmail(email);
            if (user == null)
                return new ResponsePackage<string>() { Status = ResponseStatus.NotFound, Message = "User doesn`t exist." };

            if (user.ResetCode == code) 
            {
                user.Password = _md5Service.GetMd5Hash(password);
                await _uow.CompleteAsync();
            } 

            return new ResponsePackage<string>() { Status = ResponseStatus.OK, Message = "Password successfully changed.", Data = string.Empty };

        }

        public async Task<ResponsePackage<string>> ResetPasswordByRole(string email, string password, string code)
        {
            ResponsePackage<string> retval = new ResponsePackage<string>();
            var userResponse = await this.GetByEmail(email);
            if (userResponse.Status != ResponseStatus.OK)
            {
                retval.Status = userResponse.Status;
                retval.Message = userResponse.Message;
            }
            else
            {
                await this.ResetPassword(email, password, code);
            }

            return retval;

        }
        public async Task<ResponsePackage<string>> Save(UserDataIn dataIn)
        {
            ResponsePackage<string> retval = new ResponsePackage<string>(ResponseStatus.OK, "User saved Successfully.");
            var user = _mapper.Map<User>(dataIn);

            if (user.Id == 0)
            {
                var userInDbByEmail = await _uow.GetUserRepository().GetByEmail(user.Email);
                if (userInDbByEmail != null)
                {
                    retval.Status = ResponseStatus.BadRequest;
                    retval.Message = "User with email already exist.";
                }
                else 
                {
                    user.LastUpdateTime = DateTime.Now;
                    user.Password = _md5Service.GetMd5Hash(user.Password);
                    await _uow.GetUserRepository().AddAsync(user);
                }
            }
            else 
            {
                var userInDb = await _uow.GetUserRepository().GetByIdAsync(user.Id);
                if (userInDb != null)
                {
                    userInDb.FirstName = user.FirstName;
                    userInDb.LastName = user.LastName;
                    userInDb.Email = user.Email;
                    userInDb.Password = _md5Service.GetMd5Hash(user.Password);
                    userInDb.Role = user.Role;
                    userInDb.LastUpdateTime = DateTime.Now;

                }
                else 
                {
                    retval.Status = ResponseStatus.NotFound;
                    retval.Message = "User doesn`t exist anymore.";
                }
                
            }

            await _uow.CompleteAsync();

            return retval;
        }


        public async Task<ResponsePackage<PaginationDataOut<UserDto>>> GetAll(UserPageInfo dataIn)
        {
            var data = await _uow.GetUserRepository().GetAll(dataIn);
            var dataDto = _mapper.Map<List<UserDto>>(data.Data);
            return new ResponsePackage<PaginationDataOut<UserDto>>()
            {
                Data = new PaginationDataOut<UserDto>()
                {
                    Count = data.Count,
                    Data = dataDto
                }
            };
        }

        public async Task<ResponsePackage<bool>> SetStatus(SetStatusDataIn dataIn)
        {
            ResponsePackage<bool> retval = new ResponsePackage<bool>(ResponseStatus.OK, "Status changed successfully.");
            var user = await _uow.GetUserRepository().GetByIdAsync(dataIn.Id);
            if (user != null)
            {
                user.Status = dataIn.Status;
                await _uow.CompleteAsync();
            }
            else 
            {
                retval.Status = ResponseStatus.NotFound;
                retval.Message = "User doesn`t exist anymore.";
            }
            return retval;
        }
    }
}
