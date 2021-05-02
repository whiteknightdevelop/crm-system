using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.DbContext
{
    public partial class PetadminContext
    {
        public Task<IEnumerable<FollowUp>> GetFollowUpsListByAnimalIdAsync(int animalId)
        {
            var mySqlCommandParameters = new Dictionary<string, object>
            {
                {"in_id_num", animalId}
            };
            return Task.Run(() => 
                GetListFromDbAsync("get_followups_by_animal_id", _dbMappers.FollowUpMapper, mySqlCommandParameters).Cast<FollowUp>()
            );
        }

        public Task<IEnumerable<FollowUpAllItem>> GetFollowUpAllList(DateTime from)
        {
            var mySqlCommandParameters = new Dictionary<string, object>
            {
                {"in_from_date", from}
            };
            return Task.Run(() => 
                GetListFromDbAsync("get_followups_list_by_date_from", _dbMappers.FollowUpAllItemMapper, mySqlCommandParameters).Cast<FollowUpAllItem>()
            );
        }
    }
}
