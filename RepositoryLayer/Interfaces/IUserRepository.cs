using CommonLayer.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        public List<GetUser> getUsers();
        public GetUser getUserById(int UId);
        public GetUser Create(User user);
        public TokenEmailClass Login(LoginUser loginUser);
        public string Update(int id, UpdateUser updateUser);
        public TokenEmailClass ForgetPassword(string Email);
        public bool ResetPasswordMethod(ResetPassword resetPassword);
    }
}
