using DataAccess.EntityFramework.Repositories.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntityFramework.Repositories {
    public class UsersRepository:IUsersBaseRepository {
        public UsersRepository(DbContext db):base(db) {

        }
    }
}
