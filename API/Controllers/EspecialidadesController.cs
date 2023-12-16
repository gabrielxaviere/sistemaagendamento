using App._Core;
using App._Core.Models;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeUser(AccessLevel = "USUARI")]
    public class EspecialidadesController : ControllerBase
    {
        /// <summary>
        /// Método que retorna todas as especialidades do banco
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getall")]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IEnumerable<Especialidades> Get()
        {
            EspecialidadesRepository rep = new EspecialidadesRepository();
            var a = rep.GetAll(e => e.Id != 0);
            return a;
        }
        /// <summary>
        /// Pegar especialidade para editar
        /// </summary>
        /// <param name="id">id da especialidade</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getforedit/{id}")]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public Especialidades GetForEdit(int id = 0)
        {
            EspecialidadesRepository rep = new EspecialidadesRepository();

            var usu = rep.GetById(id);

            return usu;
        }

        /// <summary>
        ///     Criar especialidade
        /// </summary>
        /// <param name="usuarios"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IActionResult Create(Especialidades usuarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EspecialidadesRepository rep = new EspecialidadesRepository();

                    rep.Add(usuarios);

                    return Ok(new CreateReturn(usuarios.Id));
                }
                return NoContent();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        ///     Atualizar especialidade 
        /// </summary>
        /// <param name="usuarios"></param>
        /// <returns></returns>
        [HttpPut]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IActionResult Update(Especialidades usuarios)
        {
            EspecialidadesRepository rep = new EspecialidadesRepository();
            var usr = rep.GetById(usuarios.Id);

            if (usr.Id > 0)
            {
                rep.Update(usr);
            }

            return NoContent();
        }

        /// <summary>
        /// Deletar especialidade
        /// </summary>
        /// <param name="id">id da especialidade</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IActionResult Delete(int id)
        {
            EspecialidadesRepository rep = new EspecialidadesRepository();

            var dados = rep.GetById(id);

            if (dados.Id > 0)
            {
                rep.Remove(dados);
            }

            return NoContent();
        }
        /// <summary>
        /// Pegar lista de especialidade
        /// </summary>
        /// <param name="id">id da especialidade</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCombo/{id}")]
        [AuthorizeUser(AccessLevel = "GENERIC")]
        public IEnumerable<Especialidades> GetCombo(int id)
        {
            EspecialidadesRepository rep = new EspecialidadesRepository();
            var emps = rep.GetAll(x => x.Id == id);

            return emps;
        }
    }
}
