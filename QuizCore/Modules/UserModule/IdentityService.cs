using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizCore.Common.Enums;
using QuizCore.Data.Identities;

namespace QuizCore.Modules.UserModule.Identities;

public class IdentityService 
{
    private UserManager<ApplicationUser> _userManager;

    public IdentityService(
     UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    #region commands
    public async Task<Guid> Create(CreateUserRequest entity)
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            
            UserName = entity.UserName,
            Email = entity.Email,
            FirstName = entity.FirstName,
            MiddleName = entity.MiddleName,
            LastName = entity.LastName,
            PasswordHash = entity.Password,
            ActiveStatus = entity.ActiveStatus,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            LoginIdentifier = Guid.NewGuid(),
            PhoneNumber=entity.PhoneNumber,
            DateOfBirth=entity.DateOfBirth

        };
        await _userManager.CreateAsync(user, entity.Password);
        return user.Id;


    }

    public async Task<bool> Delete(Guid id)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(id.ToString()) ;
        if(user==null)
        {
            return false;
        }
        user.LockoutEnabled = true;
        user.ActiveStatus = ActiveStatus.InActive;
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }
    public async Task<bool> UpdatePassword(UpdatePasswordRequest model)
    {
        var user = await _userManager.FindByIdAsync(model.Id.ToString());
        if (user == null)
        {
            return false;
        }
        var pass = model.Password.ToString();
        var confPass = model.ConfirmPassword.ToString();
        if (pass != confPass)
        {
            return false;
        }
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, pass);
        if (!result.Succeeded)
        {
            return false;
        }
        return true;
    }
    public async Task<int> Update(UpdateUserRequest entity)
    {
        var user = await _userManager.FindByIdAsync(entity.Id.ToString());
        if(user is not null)
        {
            user.UserName = entity.UserName;
            user.Email = entity.Email;
            user.FirstName = entity.FirstName;
            user.MiddleName = entity.MiddleName;
            user.LastName = entity.LastName;
            user.ActiveStatus= entity.ActiveStatus;
            user.DateOfBirth=entity.DateOfBirth;
            user.PhoneNumber=entity.PhoneNumber;
            user.UpdatedDate = DateTime.UtcNow;
            var res = await _userManager.UpdateAsync(user);
            if(res.Succeeded)
            {
                return 1;
            }
        }
        return 0;
    }


    public async Task UpdateLoginIdentifier(string userName)
    {
        var user = await _userManager.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
        if (user == null)
        {
            return;
        }
        user.LoginIdentifier = Guid.NewGuid();
        await _userManager.UpdateAsync(user);
    }

    public async Task<bool> AddUserClaims(Guid userId, List<Claim> claims)
    {
        var user= await _userManager.FindByIdAsync(userId.ToString());
        await _userManager.AddClaimsAsync(user!, claims);
        return true;
    }
    public async Task<bool> RemoveUserClaims(Guid userId, IList<Claim> claims)
    {
         var user= await _userManager.FindByIdAsync(userId.ToString());
        await _userManager.RemoveClaimsAsync(user!, claims);
        return true;
    }


    #endregion

    #region Queries
    public async Task<GetUserByIdResponse> FindById(Guid userId)
    {
        var user= await _userManager.FindByIdAsync(userId.ToString());
        if(user is null)
        {
            return new GetUserByIdResponse();
        }
        return new GetUserByIdResponse
        {
            Id =user.Id,
            FirstName =user.FirstName,
            MiddleName =user.MiddleName,
            LastName =user.LastName,
            UserName =user.UserName,
            Email =user.Email,
            ActiveStatus =user.ActiveStatus,
            PhoneNumber=user.PhoneNumber,
            DateOfBirth=user.DateOfBirth
            
        };
    }
    public async Task<List<GetUserByIdResponse>> GetAll()
    {
        var user= await _userManager.Users.Where(x=>x.ActiveStatus==ActiveStatus.Active 
        && x.LockoutEnabled==false).ToListAsync();
        if(user is null)
        {
            return [];
        }
        return user.Select(x => new GetUserByIdResponse
        {
            Id =x.Id,
            MiddleName =x.MiddleName,
            LastName =x.LastName,
            UserName =x.UserName,
            Email =x.Email,
            ActiveStatus =x.ActiveStatus,
            PhoneNumber=x.PhoneNumber,
            DateOfBirth=x.DateOfBirth
        }).ToList();
    }

    public async Task<IList<Claim>> GetUserClaims(Guid userId)
    {
        ApplicationUser? appUser = await _userManager.FindByIdAsync(userId.ToString());
        if (appUser == null)
        {
            return [];
        }
        return await _userManager.GetClaimsAsync(appUser);
    }



    public string GetUserNameById(Guid userId)
    {
        var user =  _userManager.FindByIdAsync(userId.ToString()).GetAwaiter().GetResult();
        if(user==null)
        {
            return string.Empty;
        }
        return user.UserName!;
    }

    public async Task<GetUsersResponse> GetLockedUser()
    {
        GetUsersResponse model = new()
        {
            Users = []
        };
        var q = _userManager.Users.AsNoTracking();
        q = q.Where(x => x.LockoutEnabled == true);

        double total = await q.CountAsync() / 10f;
        model.TotalPages = (int)Math.Ceiling(total);
        q = q.OrderBy(x => x.UserName);
        model.Users = await q.Select(x => new GetUsersResponse.UserDto
        {
            Id = x.Id,
            UserName = x.UserName!,
            ActiveStatus = x.ActiveStatus
        }).ToListAsync();
        return model;
    }

    public async Task<int> UnlockedUser(Guid id)
    {
        return await _userManager.Users.Where(x => x.Id == id)
        .ExecuteUpdateAsync(p =>
            p.SetProperty(x => x.ActiveStatus, x => ActiveStatus.Active)
            .SetProperty(x => x.LockoutEnabled, x => false)
        );

    }

    public async Task<GetUsersResponse> FindAll(UserFilterRequest query, 
    bool isAdminAndHr,Guid id)
    {
        query.Offset*=query.Limit;
        GetUsersResponse model = new();
        var q = await _userManager.Users.Where(x => x.LockoutEnabled == false).AsNoTracking().ToListAsync();
        if(!isAdminAndHr)
        {
            q=q.Where(x=>x.Id==id).ToList();
        }
        if (query.Username != null)
        {
            q = q.Where(x => x.UserName!.Contains(query.Username, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }
        if (query.ActiveStatus > 0)
        {
            q = q.Where(x => x.ActiveStatus == (ActiveStatus)query.ActiveStatus).ToList();
        }
        q = [.. q.OrderBy(x => x.UserName)];
        double total =  (double)q.Count / query.Limit;
        model.TotalPages = (int)Math.Ceiling(total);
        model.Users = q.Select(x => new GetUsersResponse.UserDto
        {
            Id = x.Id,
            UserName = x.UserName!,
            ActiveStatus = x.ActiveStatus
        }).Skip(query.Offset).Take(query.Limit).ToList();
        
        return model;
    }

    public async Task<bool> Authenticate(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
        {
            return false;
        }
        return await _userManager.CheckPasswordAsync(user, password);
    }

    internal async Task<ApplicationUser?> FindByNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    internal async Task<ApplicationUser?> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }
    #endregion

}
