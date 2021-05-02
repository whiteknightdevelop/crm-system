using System.Collections.Generic;

namespace Petadmin.Repository.DbEntitys
{
    public interface IEntitiesList
    {
        List<IDbEntity> List { get; }
    }
}
