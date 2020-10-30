using BackendSolution.Entity;
using PetShopApplicationSolution.Infrastructure.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackendSolution.Infrastructure.Data
{
    public class DBInitializer : IDBInitializer
    {
        private readonly IAuthenticationHelper authenticationHelper;

        public DBInitializer(IAuthenticationHelper authHelper)
        {
            authenticationHelper = authHelper;
        }

        public void SeedDB(BackendContext btx)
        {
            // Delete the database, if it already exists. I do this because an
            // existing database may not be compatible with the entity model,
            // if the entity model was changed since the database was created.
            // This operation has no effect for an in-memory database.
            btx.Database.EnsureDeleted();

            // Create the database, if it does not already exists. This operation
            // has no effect for an in-memory database.
            btx.Database.EnsureCreated();

            string password = "1234";
            byte[] passwordHashJoe, passwordSaltJoe, passwordHashAnn, passwordSaltAnn, passwordHashJanet, passwordSaltJanet, passwordHashAni, passwordSaltAni;
            authenticationHelper.CreatePasswordHash(password, out passwordHashJoe, out passwordSaltJoe);
            authenticationHelper.CreatePasswordHash(password, out passwordHashAnn, out passwordSaltAnn);
            authenticationHelper.CreatePasswordHash(password, out passwordHashJanet, out passwordSaltJanet);
            authenticationHelper.CreatePasswordHash(password, out passwordHashAni, out passwordSaltAni);

            List<User> users = new List<User>
            {
                new User{Username = "UserJoe", PasswordHash = passwordHashJoe,
                    PasswordSalt = passwordSaltJoe, IsAdmin = false},
                new User{Username = "AdminAnn", PasswordHash = passwordHashAnn,
                    PasswordSalt = passwordSaltAnn, IsAdmin = true},
                new User{Username = "UserJanet", PasswordHash = passwordHashJanet,
                    PasswordSalt =passwordSaltJanet, IsAdmin = false},
                new User{Username = "AdminAni", PasswordHash = passwordHashAni,
                    PasswordSalt = passwordSaltAni, IsAdmin = true},
            };

            List<Product> products = new List<Product>
            {
               new Product{Name = "Chocolate Ice Cream", Color = "Brown", Type = "Treat", Price = 999, CreatedDate = new DateTime(2011, 6, 4)},
               new Product{Name = "Vanilla Froyo", Color = "White", Type = "Treat", Price = 01, CreatedDate = new DateTime(2010, 5, 5)},
               new Product{Name = "Ice Cream Sandwich", Color = "Indigo", Type = "Treat", Price = 90, CreatedDate = new DateTime(2012, 7, 8)},
               new Product{Name = "Chili Corn Dog", Color = "Red", Type = "Treat", Price = 12, CreatedDate = new DateTime(1999, 1, 1)},
            };

            btx.AddRange(users);
            btx.AddRange(products);
            btx.SaveChanges();



        }
    }
}
