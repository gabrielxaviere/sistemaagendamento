using App._Core;
using App._Core.Models;
using Data.Models;
using Data.Repositories;
using Data.Repository.Utils;
using Data.Utils;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeUser(AccessLevel = "USUARI")]
    public class UsuariosController : ControllerBase
    {
        /// <summary>
        /// Retorna uma lista de usuários com base nos parâmetros fornecidos
        /// </summary>
        /// <param name="responsavel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IEnumerable<Usuarios> Get(int? responsavel, string text = "")
        {
            UsuariosRepository rep = new UsuariosRepository();
            return rep.GetAll(e => (string.IsNullOrEmpty(text) || e.Nome.Contains(text) || e.Sobrenome.Contains(text))
                                && (!responsavel.HasValue || e.Responsavel == responsavel.Value));
        }
        /// <summary>
        /// Retorna uma lista de profissionais com base na especialidade
        /// </summary>
        /// <param name="especialidade"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getprofissional")]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IEnumerable<Usuarios> GetProfissional(int especialidade, int? responsavel)
        {
            UsuariosRepository rep = new UsuariosRepository();
            var a = rep.GetAll(e => e.Tipo == (int)TipoUsuario.PROFISSIONAL_SAUDE && e.IdEspecialidade == especialidade && e.Responsavel == responsavel);
            return a;
        }
        /// <summary>
        /// Retorna uma lista de usuarios com base em seu tipo
        /// </summary>
        /// <param name="especialidade"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("gettipo/{tipo}")]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IEnumerable<Usuarios> GetByTipo(int tipo)
        {
            UsuariosRepository rep = new UsuariosRepository();
            var a = rep.GetAll(e => e.Tipo == tipo);
            return a;
        }
        /// <summary>
        /// Pega um usuário específico para ser editado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getforedit/{id}")]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public Usuarios GetForEdit(int id = 0)
        {
            UsuariosRepository rep = new UsuariosRepository();
            var usu = rep.GetById(id);

            return usu;
        }
        /// <summary>
        /// Cria um novo usuário com base nas informações fornecidas
        /// </summary>
        /// <param name="usuarios"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IActionResult Create(Usuarios usuarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuarios.Senha = Criptography.AesEncrypt(usuarios.Senha);

                    UsuariosRepository rep = new UsuariosRepository();

                    var usuarioExistente = rep.GetUserByEmail(usuarios.Email);

                    if (usuarioExistente == null)
                    {
                        rep.Add(usuarios);

                        if (Convert.ToInt32(TipoUsuario.PACIENTE) != usuarios.Tipo)
                        {
                            if (usuarios.Responsavel == 0)
                            {
                                var a = rep.GetById(usuarios.Id);
                                if (a.Id > 0)
                                {
                                    a.Responsavel = a.Id;
                                    rep.Update(a);
                                }
                            }
                        }

                        return Ok(new CreateReturn(usuarios.Id));
                    }
                    return NoContent();
                }
                return NoContent();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        /// <summary>
        /// Atualiza informações do usuário
        /// </summary>
        /// <param name="usuarios"></param>
        /// <returns></returns>
        [HttpPut]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IActionResult Update(Usuarios usuarios)
        {
            UsuariosRepository rep = new UsuariosRepository();
            var usr = rep.GetById(usuarios.Id);

            if (usuarios.Senha == usr.Senha)
            {
                usr.Senha = Criptography.AesEncrypt(usuarios.Senha);
                usr.Nome = usuarios.Nome;
                usr.Sobrenome = usuarios.Sobrenome;
                usr.Email = usuarios.Email;
                usr.Tipo = usuarios.Tipo;
                rep.Update(usr);
            }
            else
            {
                return StatusCode(700, "Senha Inválida!");
            }

            return NoContent();
        }
        /// <summary>
        /// Deleta usuário com base no id
        /// </summary>
        /// <param name="id">Id do usuário a ser deletado</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IActionResult Delete(int id)
        {
            UsuariosRepository rep = new UsuariosRepository();

            var usuarios = rep.GetById(id);

            if (usuarios.Id > 0)
            {
                rep.Remove(usuarios);
            }

            return NoContent();
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("GetCombo/{id}")]
        //[AuthorizeUser(AccessLevel = "GENERIC")]
        //public IEnumerable<Usuarios> GetCombo2(int id)
        //{
        //    UsuariosRepository rep = new UsuariosRepository();
        //    var emps = rep.GetAll(x => x.Id == id);

        //    return emps;
        //}
    }
}
