using Data.Repositories;
using JwsCreationSample;

namespace App.Utils
{
    public static class Utils
    {
        public static int ValidateAccessToken(string accessLevel, string token, ref int idUser, ref int idInstancia)
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

        public static bool ValidateTokenJWT(string token)
        {
            var retorno = JWT.Validate(token);

            return retorno;
        }
    }
}
