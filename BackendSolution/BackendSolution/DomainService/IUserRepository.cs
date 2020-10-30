using BackendSolution.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackendSolution.Core.DomainService
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(long id);
        void Add(User newUser);
        void Edit(User updateUser);
        void Remove(long id);
    }
}
