﻿using System.Data.Entity.Infrastructure;
using Libellus.DataAccess.Domain;
using Libellus.DataAccess.Repositories.Interface;
using Libellus.DataAccess.Database;

namespace Libellus.DataAccess.Repositories.Implementation
{
    public class FacultyRoleRepository : BaseRepository<FacultyRole>, IFacultyRoleRepository
    {
        public FacultyRoleRepository(LibellusDbContext dbContext) : base(dbContext)
        {
        }
    }
}
