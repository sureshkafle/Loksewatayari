using QuizCore.Common.Enums;

namespace QuizCore.Modules.UserModule.Identities;

public class GetUsersResponse
{
     public int TotalPages {get; set;}
     public List<UserDto> Users { get; set; } =[];
     public class UserDto
     {
          public Guid Id { get; set; }
          public required string UserName { get; set; }
          public ActiveStatus ActiveStatus { get; set; }
    }
}
public sealed class UserFilterRequest
{
     public int Limit { get; set; } = 50;
     public int Offset { get; set; } = 0;
     public string? Username { get; set; } = null;
     public ActiveStatus? ActiveStatus { get; set; }

}
