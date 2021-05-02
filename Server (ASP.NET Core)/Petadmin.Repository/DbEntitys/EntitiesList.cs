using System.Collections.Generic;

namespace Petadmin.Repository.DbEntitys
{
    public class EntitiesList: IEntitiesList
    {
        public List<IDbEntity> List { get; } = new()
        {
            new DbEntityOwner(new DbCommon(), new DbMappers()),
            new DbEntityAnimal(new DbCommon(), new DbMappers()),
            new DbEntityPreventiveReminder(new DbCommon(), new DbMappers()),
            new DbEntityPreventiveTreatment(new DbCommon(), new DbMappers()),
            new DbEntityVisit(new DbCommon(), new DbMappers()),
            new DbEntityPrescription(new DbCommon(), new DbMappers()), 
            new DbEntityFollowUp(new DbCommon(), new DbMappers()), 
            new DbEntityDebt(new DbCommon(), new DbMappers()),
            new DbEntityApplicationUser(new DbCommon(), new DbMappers()),
            new DbEntityApplicationRole(new DbCommon(), new DbMappers()),
        };
    }
}
