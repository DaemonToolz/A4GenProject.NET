using DataAccess.SQL.Token;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
//using System.IdentityModel.Tokens;
using TokenManagementService.Security;
//using System.Security.Claims;
//using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Data.Entity.Validation;
using System.Threading;
using System.IO;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TokenGenerationService" in code, svc and config file together.
[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
public class TokenGenerationService : ITokenGenerationService, ITokenValidationService{

    #region Common
    private static readonly TokensEntities TokenEF;
    private static IdGenerator idGeneration;
    private static readonly Int32 _ID_GEN_SIZE = (int)TokenManagementUtil._MEDIUM_ID_SIZE;
    private static readonly ClaimsIdentity Claims;
    private static readonly System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler TokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
    private static readonly Timer TokenVerificationTrigger;
    #endregion

    static TokenGenerationService(){
        TokenEF = new TokensEntities();

        // Claims commun à tous les tokens
        Claims = new ClaimsIdentity(new List<Claim>(){
            new Claim(ClaimTypes.NameIdentifier, "axel.maciejewski@viacesi.com"),
            new Claim(ClaimTypes.Role, "NET_Expert"),
            new Claim(ClaimTypes.Name, "White Hat & co."),
        }, "Custom");

        idGeneration = new IdGenerator(1);
        for (int i = 0; i < 2; ++i) idGeneration.GenerateId(_ID_GEN_SIZE);

        // Nettoyage des tokens expirés
        TokenVerificationTrigger = new Timer((_=>CheckTokenStates()),null,2000,(60*1000));
        
    }

    private static void CheckTokenStates(){
        try
        {
            TokenEF.Token.RemoveRange(TokenEF.Token.Where(token => token.exp != null && token.exp < DateTime.Now));
            TokenEF.SaveChanges();
        }
        catch
        {

        }

    }

    private static bool TokenExists(string input) {
        return TokenEF.Token.Any(token => token.token1.Equals(input));
    }

    /// <summary>
    /// Vérifie que le token est valide
    /// </summary>
    /// <param name="input"></param>
    /// <param name="stoken"></param>
    /// <returns></returns>
    private static bool IsValidToken(string input, SecurityToken stoken){
        Token token = TokenEF.Token.Single(tken => tken.token1.Equals(input));

        //
        if (token.exp != null) {
            if (DateTime.Now > token.exp)
            {
                // Si expiré
                TokenEF.Token.Remove(token);
                TokenEF.SaveChanges();
                return false;
            }
            else {
                // Paramtères de validation
                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidAudiences = new string[] { "White Hat Agents" },
                    ValidIssuers = new string[] { "White Hat & co." },
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token.jni))
                };

                string realToken = input.Substring(0, input.Length - 12);
                SecurityToken Validated;
                TokenHandler.ValidateToken(realToken, tokenValidationParameters, out Validated);
  
                return token.boundmac.Equals(Validated.ToString());
            }
        }
        else
            return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="forceExisting"></param>
    /// <returns></returns>
    [OperationBehavior(Impersonation = ImpersonationOption.NotAllowed)]
    public bool CheckToken(string input, bool forceExisting = false){
        try {
            string realToken = input.Substring(0,input.Length - 12);    // On récupère le vrai token
            string salt = input.Substring(input.Length - 12);           // Les 12 derniers caractères
            if (TokenHandler.CanReadToken(realToken))
            {
                SecurityToken stoken;
                try {
                    stoken = TokenHandler.ReadJwtToken(realToken);
                }
                catch {
                    stoken = TokenHandler.ReadToken(realToken);
                }

                if (forceExisting && TokenExists(input))
                    if (IsValidToken(input, stoken)) return true;
                    else return false;
                return true;
            }
            return false;
        }
        catch {
            return false;
        }
        //return false;
    }

    /// <summary>
    /// Génère le token de connection
    /// </summary>
    /// <returns></returns>
    [OperationBehavior(Impersonation = ImpersonationOption.NotAllowed)]
    public String GenerateToken(){
        try
        {
            bool changes = false;

            // Informations du token
            String jni = idGeneration.GenerateId(_ID_GEN_SIZE);
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jni));
            signingKey.KeyId = idGeneration.GenerateId(24);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);// SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);
            
            // Descripteur
            DateTime CallDate = DateTime.Now, ExpirationDate = CallDate.AddMinutes(5.0d);
            var securityTokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
            {
                
                Issuer = "White Hat & co.",
                IssuedAt = CallDate,
                Expires = ExpirationDate,
                Audience = "White Hat Agents",

                Subject = Claims,
                SigningCredentials = signingCredentials,
            };


            // Token encodé
            var plainToken = TokenHandler.CreateToken(securityTokenDescriptor);
            var signedAndEncodedToken = TokenHandler.WriteToken(plainToken);

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudiences = new string[] { "White Hat Agents" },
                ValidIssuers = new string[] { "White Hat & co." },
                RequireExpirationTime = true,
                IssuerSigningKey = signingKey
            };

            SecurityToken validatedToken;
            TokenHandler.ValidateToken(signedAndEncodedToken, tokenValidationParameters, out validatedToken);

            signedAndEncodedToken += idGeneration.GenerateId(12);

            // Vérifie le token (existe ou non)
            if (!TokenExists(signedAndEncodedToken) && CheckToken(signedAndEncodedToken))
            {
                //var signingKey = new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(plainTextSecurityKey));
                TokenEF.Token.Add(new Token()
                {
                    jni = jni,
                    t_key = signingKey.KeyId.ToString(),
                    token1 = signedAndEncodedToken,
                    beg = CallDate,
                    exp = ExpirationDate,

                    boundmac = validatedToken.ToString() // To rename
                });
                changes = true;
            }

            if (TokenExists(signedAndEncodedToken))
            {
                TokenEF.Token.Remove(TokenEF.Token.First(token => token.token1.Equals(signedAndEncodedToken)));
                changes = true;
            }

            if (changes)
                TokenEF.SaveChanges();

            return signedAndEncodedToken;//validatedToken.ToString();
        }
        catch (DbEntityValidationException e) {
            String final = "";
            foreach (var eve in e.EntityValidationErrors){
                final += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    final += String.Format("- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage);
                }
            }
            return final;
        } catch (Exception e) {
            return e.Message;
        }
    }
}
