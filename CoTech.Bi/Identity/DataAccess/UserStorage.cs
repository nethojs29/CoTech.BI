using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Identity.DataAccess
{
  public class UserStorage : IUserStore<UserEntity>, IUserPasswordStore<UserEntity>, IUserEmailStore<UserEntity>
  {
    private readonly BiContext context;
    private DbSet<UserEntity> db {
      get { return context.Set<UserEntity>(); }
    }

    public UserStorage(BiContext context){
      this.context = context;
    }
    public async Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
    {
      db.Add(user);
      var result = await context.SaveChangesAsync();
      if(result > 0){
        return IdentityResult.Success;
      }
      return IdentityResult.Failed(new IdentityError { Description = $"Could not insert user {user.Email}." });
    }

    public async Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken)
    {
      db.Remove(user);
      var result = await context.SaveChangesAsync();
      if(result > 0){
        return IdentityResult.Success;
      }
      return IdentityResult.Failed(new IdentityError { Description = $"Could not remove user {user.Email}." });
    }

    public Task<UserEntity> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
      return db.FindAsync(Int64.Parse(userId));
    }

    public Task<UserEntity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
      return db.FirstOrDefaultAsync(u => u.Email == normalizedUserName);
    }

    public Task<string> GetNormalizedUserNameAsync(UserEntity user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.Email);
    }

    public Task<string> GetUserIdAsync(UserEntity user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.Id.ToString());
    }

    public Task<string> GetUserNameAsync(UserEntity user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.Name);
    }

    public Task SetNormalizedUserNameAsync(UserEntity user, string normalizedName, CancellationToken cancellationToken)
    {
      user.Email = normalizedName;
      return Task.CompletedTask;
    }

    public Task SetUserNameAsync(UserEntity user, string userName, CancellationToken cancellationToken)
    {
      user.Name = userName;
      return Task.CompletedTask;
    }

    public async Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
    {
      db.Update(user);
      await context.SaveChangesAsync();
      return IdentityResult.Success;
    }

    public Task SetPasswordHashAsync(UserEntity user, string passwordHash, CancellationToken cancellationToken)
    {
      user.Password = passwordHash;
      return Task.CompletedTask;
    }

    public Task<string> GetPasswordHashAsync(UserEntity user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.Password);
    }

    public Task<bool> HasPasswordAsync(UserEntity user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.Password != null);
    }

    public Task SetEmailAsync(UserEntity user, string email, CancellationToken cancellationToken)
    {
      user.Email = email;
      return Task.CompletedTask;
    }

    public Task<string> GetEmailAsync(UserEntity user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.Email);
    }

    public Task<bool> GetEmailConfirmedAsync(UserEntity user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.EmailConfirmed);
    }

    public Task SetEmailConfirmedAsync(UserEntity user, bool confirmed, CancellationToken cancellationToken)
    {
      user.EmailConfirmed = confirmed;
      return Task.CompletedTask;
    }

    public async Task<UserEntity> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
      var user = await db.FirstOrDefaultAsync(u => u.Email == normalizedEmail);
      return user;
    }

    public Task<string> GetNormalizedEmailAsync(UserEntity user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.Email);
    }

    public Task SetNormalizedEmailAsync(UserEntity user, string normalizedEmail, CancellationToken cancellationToken)
    {
      user.Email = normalizedEmail;
      return Task.CompletedTask;
    }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          // TODO: dispose managed state (managed objects).
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.

        disposedValue = true;
      }
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~UserStorage() {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      // GC.SuppressFinalize(this);
    }
    #endregion

  }
}