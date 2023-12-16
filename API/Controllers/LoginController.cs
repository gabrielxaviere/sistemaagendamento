using App._Core;
using App._Core.Models;
using App.Utils;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// Método para realizar login no sistema, recebendo o email e a senha
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeUser(AccessLevel = "NOVALIDATE")]
        public Credential Get(string email = "", string password = "")
        {

            try
            {

                UsuariosRepository rep = new UsuariosRepository();
                var usu = rep.GetUserToLogon(email, password);

                if (usu != null)
                {
                    var jwtInstance = new JWT(); // Crie uma instância de JWT
                    var token = jwtInstance.Create(usu.Id);

                    return new Credential
                    {
                        id = usu.Id,
                        username = usu.Nome,
                        email = usu.Email,
                        accessToken = token,
                        tipo = usu.Tipo,
                        responsavel = usu.Responsavel,
                        roles = null,
                        fullname = usu.Nome + " " + usu.Sobrenome,
                    };
                }

                return new Credential
                {
                    id = 0
                };
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
