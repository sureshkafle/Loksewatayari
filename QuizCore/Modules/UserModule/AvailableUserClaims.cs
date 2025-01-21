using QuizCore.Common.Enums;

namespace QuizCore.Modules.UserModule.Identities;

public class AvailableUserClaims
{

    private static string[] permissions { get; set; }=[];

    public static string[] GetAvailableClaims()
    {
        GetPermissions();
        return permissions;
    }

    public static string GetClaimName(AvailableClaims claim)
    {
        return Enum.GetName(claim)??string.Empty;
    }
    private static void GetPermissions()
    {
        permissions = new string[] {
               
                AvailableClaims.Admin.GetClaimName(),
                AvailableClaims.User.GetClaimName(),
                AvailableClaims.Developer.GetClaimName(),
            };
    }
}
internal static class AvailableClaimExtensions
{
    public static string GetClaimName(this AvailableClaims claim)
    {
        return Enum.GetName(claim)??string.Empty;
    }

}

