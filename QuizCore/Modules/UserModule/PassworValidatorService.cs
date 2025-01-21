namespace QuizCore.Modules.UserModule.Identities;
public static class PassworValidatorService 
{
     public static bool PasswordValidator(string password)
     {
          if (password.Length < 6 || password.Length > 32)
          return false;
          if(!password.Any(Char.IsDigit))
          return false;
          if (!password.Any(char.IsUpper))
          return false;
          if (!password.Any(char.IsLower))
          return false;
          if (password.Contains(' '))
          return false;
          string specialCh = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
          char[] specialChArray = specialCh.ToCharArray();
          foreach (char ch in specialChArray) {
          if (password.Contains(ch))
          return true;
          }
          return false;
     }
}