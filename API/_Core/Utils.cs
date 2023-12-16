using Data.Repositories;

namespace App.Utils
{
    public class Utils
    {
        public int ValidateAccessToken(string accessLevel, string token, ref int idUser, ref int idInstancia)
        {
            if (accessLevel == "NOVALIDATE")
            {
                return 0;
            }

            if (string.IsNullOrEmpty(token))
            {
                return 401;
            }

            if (string.IsNullOrEmpty(accessLevel))
            {
                return 403;
            }

            if (ValidateTokenJWT(token))
            {
                UsuariosRepository usuRep = new UsuariosRepository();

                if (usuRep.PermissionsValdiate(idUser, accessLevel))
                {
                    return 0;
                }
                else
                {
                    return 401;
                }
            }
            else
            {
                return 403;
            }
        }

        public bool ValidateTokenJWT(string token)
        {
            var jwtInstance = new JWT(); // Crie uma instância de JWT
            var retorno = jwtInstance.ValidateToken(token);

            return retorno;
        }
    }
}
