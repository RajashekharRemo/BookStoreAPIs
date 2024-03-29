using BusinessLayer.Interfaces;
using CommonLayer.UserModel;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _business;

        public UserController(IUserBusiness business)
        {
            _business = business;
        }




        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_business.getUsers());
        }

        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int UId) {
            if (UId <= 0) { return BadRequest(new { message = "Bad Request", result = false }); }

            var user=_business.getUserById(UId);
            if(user == null)
            {
                return NotFound(new {message="User not Found", result=false});
            }
            else
            {
                return Ok(new { message = "User Found", result = true, data = user });
            }
        }



        [HttpPost("CreateUser")]
        public IActionResult Create(User user)
        {
            if (user == null) { return BadRequest(new { message = "Created UnSuccessful, Give data properly", result = false }); }

            var result= _business.Create(user);
            if(result == null) { return BadRequest(new { message = "Created UnSuccessful", result = false }); }
            return Ok( new {message="Created Successfully", result=true, data=result });
        }



        [HttpPost("Login")]
        public IActionResult Login(LoginUser loginUser )
        {
            if(loginUser == null) { return BadRequest(new { message = "Login UnSuccessful, Give data properly", result = false }); }

            var result=_business.Login(loginUser);
            if(result == null) { return NotFound(new { message = "Login UnSuccessful, user not present", result = false }); }
            return Ok(new { message = "Login Success", result = true, data = result });
        }

        [HttpPut("Update")]
        public IActionResult Update(int Id, UpdateUser updateUser)
        {
            if(Id<0 &&  updateUser == null) { return BadRequest(new {message="Provide data properly", result=false}); }

            string result=_business.Update(Id, updateUser);
            if(result == null)
            {
                return NotFound(new { message = "User Not Found", result = false });
            }
            else
            {
                return Ok(new { message = "Successfully Updated", result = true , data=result});
            }
        }

        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string Email)
        {
            try
            {
                var result = _business.ForgetPassword(Email);
                if (result != null)
                {
                    Send send = new Send();

                    send.SendingMail(result.Email, "Password is Trying to Changed is that you....! " + result.Token);//result

                    Uri uri = new Uri("rabbitmq://localhost/NotesEmail_Queue");
                    //var endPoint = bus.GetSendEndpoint(uri);

                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetPassword reset)
        {
            if(reset == null) { return BadRequest(); }

            bool flag=_business.ResetPasswordMethod(reset);
            if (flag)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


    }
}
