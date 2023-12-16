using App._Core;
using App._Core.Models;
using Data.Repositories;
using JwsCreationSample;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        [AuthorizeUser(AccessLevel = "NOVALIDATE")]
        public Credential Get(string email = "", string password = "")
        {
            UsuariosRepository rep = new UsuariosRepository();

            var usu = rep.GetUserToLogon(email, password);

            if (usu.Id > 0)
            {
                var token = JWT.Create(email);

                return new Credential
                {
                    id = usu.Id,
                    username = usu.Nome,
                    email = usu.Email,
                    accessToken = token,
                    refreshToken = "",
                    roles = null,
                    pic = "",
                    fullname = usu.Nome + " " + usu.SobreNome,
                };
            }

            return new Credential
            {
                id = 0
            };
        }
    }
}
