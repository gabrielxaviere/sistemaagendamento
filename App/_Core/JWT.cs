using JsonWebToken;
namespace JwsCreationSample
{
    public static class JWT
    {
        public static string Create(string email)
        {
            var signingKey = SymmetricJwk.FromBase64Url("R9MyWaEoyiMYViVWo8Fk4TUGWiSoaW6U1nOqXri8_XU");

            var descriptor = new JwsDescriptor(signingKey, SignatureAlgorithm.HS256)
            {
                Payload = new JwtPayload
                {
                    { JwtClaimNames.Iat, EpochTime.UtcNow },
                    { JwtClaimNames.Exp, EpochTime.UtcNow + 0.05 },
                    { JwtClaimNames.Iss, "https://localhost:4200/" },
                    { JwtClaimNames.Aud, "636C69656E745F6964" },
                    { JwtClaimNames.Sub, email },
                    { JwtClaimNames.Jti, Guid.NewGuid().ToString() },
                    { JwtClaimNames.Nbf, EpochTime.UtcNow }
                }
            };

            var writer = new JwtWriter();
            var token = writer.WriteTokenString(descriptor);

            return token;
        }

        public static bool Validate(string token)
        {
            var key = SymmetricJwk.FromBase64Url("R9MyWaEoyiMYViVWo8Fk4TUGWiSoaW6U1nOqXri8_XU");

            var policy = new TokenValidationPolicyBuilder()
                           .RequireSignature("https://idp.example.com/", key, SignatureAlgorithm.HS256)
                           .RequireAudience("636C69656E745F6964")
                           .Build();

            string sub = string.Empty;

            if (Jwt.TryParse(token, policy, out Jwt jwt))
            {
                jwt.Dispose();
                return true;
            }
            else
            {
                jwt.Dispose();
                return false;
            }
        }
    }
}
