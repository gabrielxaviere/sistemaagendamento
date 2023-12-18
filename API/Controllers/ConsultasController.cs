using App._Core;
using App._Core.Models;
using Data.List;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeUser(AccessLevel = "USUARI")]
    public class ConsultasController : ControllerBase
    {
        /// <summary>
        /// Método de pesquisa de consultas considerando parâmetros como:
        /// </summary>
        /// <param name="data">Data de pesquisa</param>
        /// <param name="status">Status relacionado à consulta</param>
        /// <param name="responsavel">Administrador responsavel pelo grupo de usuários cadastrados</param>
        /// <param name="idUsuario">id do usuário que faz a requisição</param>
        /// <param name="nome">Campo para pesquisa</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IEnumerable<Consultas> Get(DateTime? data, int? status, int? responsavel, int? idUsuario, string nome = "")
        {
            ConsultasRepository rep = new ConsultasRepository();

            var consultas = rep.GetAll(e =>
                (string.IsNullOrEmpty(nome) || e.Usuarios.Nome.Contains(nome)) &&
                (!data.HasValue || e.Data.Date.Equals(data.Value.Date)) &&
                (!status.HasValue || e.Status == status.Value) &&
                 (!idUsuario.HasValue || e.IdPaciente == idUsuario.Value) &&
                 (!responsavel.HasValue || e.Usuarios.Responsavel == responsavel.Value)
            );

            return consultas;
        }

        /// <summary>
        /// Método de pesquisa de consultas incluindo o nome do médico considerando parâmetros como:
        /// </summary>
        /// <param name="data">Data de pesquisa</param>
        /// <param name="status">Status relacionado à consulta</param>
        /// <param name="responsavel">Administrador responsavel pelo grupo de usuários cadastrados</param>
        /// <param name="idUsuario">id do usuário que faz a requisição</param>
        /// <param name="nome">Campo para pesquisa</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getwithmedicos")]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IEnumerable<ConsultaList> GetWithMedicos(DateTime? data, int? status, int? responsavel, int? idUsuario, string nome = "")
        {
            ConsultasRepository rep = new ConsultasRepository();

            var consultas = rep.GetAllWithMedico(e =>
                (string.IsNullOrEmpty(nome) || e.Usuarios.Nome.Contains(nome)) &&
                (!data.HasValue || e.Data.Date.Equals(data.Value.Date)) &&
                (!status.HasValue || e.Status == status.Value) &&
                 (!idUsuario.HasValue || e.IdPaciente == idUsuario.Value) &&
                 (!responsavel.HasValue || e.Usuarios.Responsavel == responsavel.Value)
            );

            return consultas;
        }

        /// <summary>
        /// Método de pesquisa de consultas considerando responsavel
        /// </summary>
        /// <param name="responsavel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getalldisableddate/{responsavel}")]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IEnumerable<Consultas> GetAllDisabledDate(int? responsavel)
        {
            ConsultasRepository rep = new ConsultasRepository();

            var consultas = rep.GetAll(e =>
                 (!responsavel.HasValue || e.Usuarios.Responsavel == responsavel.Value) &&
                 (!responsavel.HasValue || e.Usuarios.Status == 0)
            );

            return consultas;
        }

        /// <summary>
        /// Obter a consulta para ser editada
        /// </summary>
        /// <param name="id">id relacionado à consulta</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getforedit/{id}")]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public Consultas GetForEdit(int id = 0)
        {
            ConsultasRepository rep = new ConsultasRepository();

            var usu = rep.GetById(id);

            return usu;
        }
        /// <summary>
        /// Método usado para criar consultas
        /// </summary>
        /// <param name="usuarios"></param>
        /// <returns>Retorna o Id do usuário criado</returns>
        [HttpPost]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IActionResult Create(Consultas usuarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ConsultasRepository rep = new ConsultasRepository();
                    usuarios.Data = usuarios.Data.AddHours(-3);

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
        /// Cancelar consulta pelo id fornecido como parâmetro
        /// </summary>
        /// <param name="id">Parâmetro usado como chave na consulta</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [AuthorizeUser(AccessLevel = "USUARI")]
        public IActionResult Cancelar(int id)
        {
            ConsultasRepository rep = new ConsultasRepository();

            var dados = rep.GetById(id);

            if (dados.Id > 0)
            {
                dados.Status = 1;
                rep.Update(dados);
            }

            return NoContent();
        }
    }
}
