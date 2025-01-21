using System.Security.Claims;
using QuizCore.Common.Exceptions;

namespace QuizCore.Modules.UserModule.Identities;

public class CreateUserClaimHandler
{
     private readonly IdentityService _service;

     public CreateUserClaimHandler(IdentityService service)
     {
          _service = service;
     }
     public async Task<bool> AddUserClaims(UserPremissionRequest request)
     {
          var userName = _service.GetUserNameById(request.UserId);
          if (userName == null)
          {
               throw new CommandExecutionException("User does not exist");
          }
          var existingClaims = await _service.GetUserClaims(request.UserId);
          await AddedNewClaims(request, existingClaims);
          await RemovedNotExistClaims(request, existingClaims);
          return true;
     }
     private async Task AddedNewClaims(UserPremissionRequest request, IList<Claim> existingClaims)
     {
          var claims = new List<Claim>();
          var addedClaims = request.UserPermission
          .Where(p => existingClaims.All(p2 => p2.Value != p.Claims));
          foreach (var permission in addedClaims)
          {
               claims.Add(new Claim(request.ClaimType.ToString(), permission.Claims));
          }
          if (claims.Count > 0)
          {
               await _service.AddUserClaims(request.UserId, claims);
          }
     }
     private async Task RemovedNotExistClaims(UserPremissionRequest request, IList<Claim> existingClaims)
     {
          var claims = new List<Claim>();
          var removedClaims = existingClaims.Where(p => request.UserPermission.All(p2 => p2.Claims != p.Value));
          foreach (var permission in removedClaims)
          {
               claims.Add(new Claim(request.ClaimType.ToString(), permission.Value));
          }
          if (claims.Count > 0)
          {
               await _service.RemoveUserClaims(request.UserId, claims);
          }
     }
}