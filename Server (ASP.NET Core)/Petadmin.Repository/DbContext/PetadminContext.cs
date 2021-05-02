using Petadmin.Repository.DbEntitys;
using Petadmin.Repository.Interfaces;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Petadmin.Repository.DbContext
{
    /// <summary>
    /// MySQL DbContext for Petadmin.
    /// </summary>
    public partial class PetadminContext : IPetadminDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly List<IDbEntity> _entitiesList;
        private readonly IDbCommon _dbCommon;
        private readonly IDbMappers _dbMappers;

        public PetadminContext(IConfiguration configuration, IEntitiesList entitiesList, IDbCommon dbCommon, IDbMappers dbMappers)
        {
            _configuration = configuration;
            _entitiesList = entitiesList.List;
            _dbCommon = dbCommon;
            _dbMappers = dbMappers;
        }
    }
}
