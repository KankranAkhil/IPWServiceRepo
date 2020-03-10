using IPWService.IRepository;
using IPWService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPWService.Repository
{
    public class UsersRepository : IUsersRepository
    {
        IPW_DevContext _db;
        public UsersRepository(IPW_DevContext db)
        {
            _db = db;
        }
        public async Task<int> AddUser(Models.Users User)
        {
            if (_db != null)
            {
                await _db.Users.AddAsync(new Users() {

                    Fname=User.Fname,
                    Lname=User.Lname,
                    EmailId=User.EmailId,
                    Phone=User.Phone,
                    CompanyId=User.CompanyId,
                    ImgPath=User.ImgPath,
                    StatusId=User.StatusId,
                    RoleId=User.RoleId
                });
                await _db.SaveChangesAsync();

                return User.UserId;
            }

            return 0;
        }

        public async Task<int> DeleteUser(int? UserId)
        {
            int result = 0;

            if (_db != null)
            {
                //Find the post for specific post id
                var post = await _db.Users.FirstOrDefaultAsync(x => x.UserId == UserId);

                if (post != null)
                {
                    //Delete that post
                    _db.Users.Remove(post);

                    //Commit the transaction
                    result = await _db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public async Task<Models.Users> GetUser(int? UserId)
        {
            if (_db != null)
            {

                return await _db.Users.Include(r => r.Role).Include(s => s.Status).Select(u=> new Users() {
                    UserId=u.UserId,
                    Fname=u.Fname,
                    Lname=u.Lname,
                    CompanyId=u.CompanyId,
                    RoleId=u.RoleId,
                    EmailId=u.EmailId,
                    Phone=u.Phone,
                    Company=u.Company==null?null:new Company()
                    {
                        CompanyId=u.Company.CompanyId,
                        Name=u.Company.Name,
                        Address=u.Company.Address
                    },
                    Role=u.Role==null?null:new Roles()
                    {
                        RoleId=u.Role.RoleId,
                        Name=u.Role.Name
                    }
                    ,
                    Status = u.Status == null ? null : new Status()
                    {
                        StatusId = u.Status.StatusId,
                        Name = u.Status.Name
                    }

                }).OrderBy(u => u.UserId).FirstOrDefaultAsync(u=>u.UserId==UserId);
                
            }

            return null;
        }

        public async Task<List<Models.Users>> GetUsers()
        {
            if (_db != null)
            {
                return await _db.Users.Include(r => r.Role).Include(s => s.Status).Select(u => new Users()
                {
                    UserId = u.UserId,
                    Fname = u.Fname,
                    Lname = u.Lname,
                    CompanyId = u.CompanyId,
                    RoleId = u.RoleId,
                    EmailId = u.EmailId,
                    Phone = u.Phone,
                    Company = u.Company == null ? null : new Company()
                    {
                        CompanyId = u.Company.CompanyId,
                        Name = u.Company.Name,
                        Address = u.Company.Address
                    },
                    Role = u.Role == null ? null : new Roles()
                    {
                        RoleId = u.Role.RoleId,
                        Name = u.Role.Name
                    },
                    Status= u.Status==null?null:new Status()
                    {
                        StatusId=u.Status.StatusId,
                        Name=u.Status.Name
                    }

                }).OrderBy(u => u.UserId).ToListAsync();
            }

            return null;
        }

        public async Task UpdateUser(Models.Users User)
        {
            if (_db != null)
            {
                var existingUser = _db.Users.Where(s => s.UserId == User.UserId).FirstOrDefault<Users>();
                if (existingUser != null)
                {
                    existingUser.Fname = User.Fname;
                    existingUser.Lname = User.Lname;
                    existingUser.EmailId = User.EmailId;
                    existingUser.Phone = User.Phone;
                    existingUser.CompanyId = User.CompanyId;
                    existingUser.ImgPath = User.ImgPath;
                    existingUser.StatusId = User.StatusId;
                    existingUser.RoleId = User.RoleId;
                }
                await _db.SaveChangesAsync();
            }
        }
    }
}
