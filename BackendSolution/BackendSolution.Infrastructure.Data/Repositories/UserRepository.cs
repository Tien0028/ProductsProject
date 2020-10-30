using BackendSolution.Core.DomainService;
using BackendSolution.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackendSolution.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BackendContext _btx;

        public UserRepository(BackendContext btx)
        {
            _btx = btx;
        }

        public void Add(User newUser)
        {
            _btx.Users.Add(newUser);
            _btx.SaveChanges();
        }

        public void Edit(User updateUser)
        {
            _btx.Entry(updateUser).State = EntityState.Modified;
            _btx.SaveChanges();
        }

        public User Get(long id)
        {
            return _btx.Users.FirstOrDefault(u => u.ID == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _btx.Users.ToList();
        }

        public void Remove(long id)
        {
            var user = _btx.Users.FirstOrDefault(u => u.ID == id);
            _btx.Users.Remove(user);
            _btx.SaveChanges();
        }
    }
}
