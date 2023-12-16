using System.ComponentModel;

namespace Data.Utils
{
    public enum TipoUsuario
    {
        [Description("Nível 0 - ADMIN")]
        ADMIN = 0,
        [Description("Nível 1 - PROFISSIONAL_SAUDE")]
        PROFISSIONAL_SAUDE = 1,
        [Description("Nível 2 - PACIENTE")]
        PACIENTE = 2,
        [Description("Nível 3 - ATENDENTE")]
        ATENDENTE = 3
    }
}
