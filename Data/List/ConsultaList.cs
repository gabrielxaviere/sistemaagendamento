using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.List
{
    public class ConsultaList
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdProfissional { get; set; }
        public DateTime Data { get; set; }
        public int Status { get; set; }
        public string ProfissionalNome { get; set; }

        public string PacienteNome { get; set; }
    }
}
