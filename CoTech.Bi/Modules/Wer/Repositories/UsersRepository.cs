using System.Collections.Generic;
using System.Linq;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Wer.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Wer.Repositories
{
    public class UsersRepository
    {
        
        private BiContext _context;
        
        private DbSet<PermissionEntity> _Permission
        {
            get { return _context.Set<PermissionEntity>(); }
        }
        
        private DbSet<CompanyEntity> _Company
        {
            get { return _context.Set<CompanyEntity>(); }
        }
        
        public UsersRepository(BiContext _context){
            this._context = _context;
        }

        public List<UserResponse> GetUsersByCompany(long idCompany, List<UserResponse> usersList)
        {
            var users = usersList ?? new List<UserResponse>();
            var company = _Company.Find(idCompany);
            if (company != null)
            {
                var userFound = _Permission
                    .Include(p => p.User)
                    .Where(p => p.CompanyId == idCompany && (p.RoleId == 603 || p.RoleId == 602))
                    .Select(p => new UserResponse(p.User)).ToList();
                users = users.Concat(userFound).ToList();
                if (company.ParentId != null)
                {
                    return users.Concat(this.GetUsersByCompany(company.ParentId.Value, users)).ToList();
                }
            }
            return users;
        }
    }
}