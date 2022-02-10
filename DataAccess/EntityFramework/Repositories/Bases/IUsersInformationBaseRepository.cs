using AppCore.DataAccess.EntityFramework.Bases;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntityFramework.Repositories.Bases {
    public class IUsersInformationBaseRepository:RepositoryBase<UserInformation> {
        public IUsersInformationBaseRepository(DbContext db):base(db) {

        }
    }
}
