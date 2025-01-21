using QuizCore.Common.Enums;

namespace QuizCore.Modules.UserModule.Identities;

public class UserPremissionRequest
{
     public Guid UserId {get;set;}
     public ClaimType ClaimType {get;set;}=ClaimType.Admin;
     public List<UserPermissionDto> UserPermission {get; set;} = new();
     public class UserPermissionDto
     {
          public required string Claims {get; set;}
     }
}
