using BusinessLayer.Interfaces;
using CommonLayer.BookModel;
using CommonLayer.UserModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;
        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public GetUser getUserById(int UId)
        {
            return _userRepository.getUserById(UId);
        }

        public List<GetUser> getUsers()
        {
          return  _userRepository.getUsers();
        }
        public GetUser Create(User user)
        {
            return _userRepository.Create(user);
        }

        public TokenEmailClass Login(LoginUser loginUser)
        {
            return _userRepository.Login(loginUser);
        }
        public string Update(int id, UpdateUser updateUser)
        {
            return _userRepository.Update(id, updateUser);
        }

        public TokenEmailClass ForgetPassword(string Email)
        {
            return _userRepository.ForgetPassword(Email);
        }

        public bool ResetPasswordMethod(ResetPassword resetPassword)
        {
            return _userRepository.ResetPasswordMethod(resetPassword);
        }



    }
}
