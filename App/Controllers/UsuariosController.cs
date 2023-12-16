using App._Core;
using App._Core.Models;
using Data.Models;
using Data.Repositories;
using Data.Repository.Utils;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeUser(AccessLevel = "USUARI")]
    public class UsuariosController : ControllerBase
    {
        [HttpGet]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IEnumerable<Usuarios> Get()
        {
            UsuariosRepository rep = new UsuariosRepository();
            return rep.GetAll(e => e.Inativo == false);
        }

        [HttpGet]
        [Route("{id}")]
        [AuthorizeUser(AccessLevel = "GENERIC")]
        public Usuarios Get(int id)
        {
            UsuariosRepository rep = new UsuariosRepository();
            var usu = rep.GetById(id);

            if (usu.Imagem != null && usu.Imagem.Length > 0)
            {
                usu.ImagemBase64 = Convert.ToBase64String(usu.Imagem);
            }

            usu.Imagem = null;

            return usu;
        }

        [HttpGet]
        [Route("getforedit/{id}/{connectionId}/{tipo}")]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public Usuarios GetForEdit(int id = 0)
        {
            UsuariosRepository rep = new UsuariosRepository();
            var usu = rep.GetById(id);

            return usu;
        }

        [HttpPost]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IActionResult Create(Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(usuarios.ImagemBase64))
                {
                    byte[] bytes = null;

                    var tokens = usuarios.ImagemBase64.Split(',');

                    if (tokens.Length > 1)
                    {
                        bytes = Convert.FromBase64String(tokens[1]);
                    }
                    else
                    {
                        bytes = Convert.FromBase64String(usuarios.ImagemBase64);
                    }
                }

                usuarios.Senha = Criptography.AesEncrypt(usuarios.Senha);

                UsuariosRepository rep = new UsuariosRepository();

                usuarios.Internal = false;

                if (usuarios.Admin)
                {
                    var usu = rep.GetById(usuarios.Id);

                    if (!usu.Admin)
                    {
                        return StatusCode(700, "Usuário logado não é administrador, então não pode criar um usuário com flag de 'Administrador'");
                    }
                }

                rep.Add(usuarios);

                return Ok(new CreateReturn(usuarios.Id));
            }

            return NoContent();
        }

        [HttpPut]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IActionResult Update(Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.Senha = Criptography.AesEncrypt(usuarios.Senha);

                UsuariosRepository rep = new UsuariosRepository();

                rep.Update(usuarios);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IActionResult Delete(int id)
        {
            UsuariosRepository rep = new UsuariosRepository();

            var usuarios = rep.GetById(id);

            if (usuarios.Id > 0)
            {
                return NotFound();
            }

            rep.Remove(usuarios);

            return NoContent();
        }

        [HttpGet]
        [Route("GetCombo/{id}")]
        [AuthorizeUser(AccessLevel = "GENERIC")]
        public IEnumerable<Usuarios> GetCombo(int id)
        {
            UsuariosRepository rep = new UsuariosRepository();
            var emps = rep.GetAll(x => !x.Inativo || x.Id == id);

            return emps;
        }

        [HttpPut]
        [Route("UpdatePassWord")]
        [AuthorizeUser(AccessLevel = "PERFIL")]
        public IActionResult UpdatePassWord(UpdatePass updates)
        {
            UsuariosRepository rep = new UsuariosRepository();
            var usr = rep.GetById(updates.IdUsuario);

            if (Criptography.AesEncrypt(updates.SenhaAtual) == usr.Senha)
            {
                usr.Senha = Criptography.AesEncrypt(updates.NovaSenha);
                rep.Update(usr);
            }
            else
            {
                return StatusCode(700, "Senha Inválida!");
            }

            return NoContent();
        }
    }
}
