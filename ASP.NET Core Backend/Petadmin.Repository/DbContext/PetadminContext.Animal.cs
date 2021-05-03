using Petadmin.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PetAdmin.Core.Models;

namespace Petadmin.Repository.DbContext
{
    public partial class PetadminContext
    {
        public Task<Owner> GetAnimalOwnerByIdAsync(int animalId)
        {
            return Task.Run(() =>
            {
                using var connection = new MySqlConnection(_dbCommon.ConnectionString);
                connection.Open();
                var cmd = _dbCommon.ProcedureQuery(connection, "get_owner_by_animal_id");
                cmd.Parameters.AddWithValue("@id_num", animalId);

                var owner = _dbCommon.GenericReaderToMapper(cmd, _dbMappers.OwnerMapper);

                connection.Close();
                return owner as Owner;
            });
        }

        public Task<IEnumerable<Visit>> GetVisitsListByAnimalIdAsync(int animalId)
        {
            var mySqlCommandParameters = new Dictionary<string, object> {{"@id_num", animalId}};
            return Task.Run(() => 
                GetListFromDbAsync("get_animal_visits_list", _dbMappers.VisitMapper, mySqlCommandParameters).Cast<Visit>()
            );
        }

        public Task<IEnumerable<string>> GetAnimalsTypesListAsync()
        {
            return Task.Run(() => GetListFromDbAsync("get_animals_types_list", _dbMappers.AnimalTypeMapper));
        }

        public Task<IEnumerable<string>> GetAnimalsGendersListAsync()
        {
            return Task.Run(() => GetListFromDbAsync("get_animals_genders_list", _dbMappers.AnimalGenderMapper));
        }

        public Task<IEnumerable<Breed>> GetAnimalsBreedsListAsync()
        {
            return Task.Run(() => GetListFromDbAsync("get_animals_breed_list", _dbMappers.AnimalBreedMapper).Cast<Breed>());
        }

        public Task<IEnumerable<string>> GetAnimalsColorsListAsync()
        {
            return Task.Run(() => GetListFromDbAsync("get_animals_colors_list", _dbMappers.AnimalColorMapper));
        }

        public Task<IEnumerable<string>> GetAnimalsRemindersListAsync()
        {
            return Task.Run(() => GetListFromDbAsync("get_animals_reminders_drop_list", _dbMappers.AnimalReminderMapper));
        }

        public Task<IEnumerable<PreventiveReminder>> GetPreventiveRemindersListAsync(int animalId)
        {
            var mySqlCommandParameters = new Dictionary<string, object> {{"@id_num", animalId}};
            return Task.Run(() => 
                GetListFromDbAsync("get_preventive_reminders_by_animal_id", _dbMappers.PreventiveReminderMapper, mySqlCommandParameters).Cast<PreventiveReminder>()
            );
        }

        public Task<Animal> GetAnimalByVisitIdAsync(int visitId)
        {
            return Task.Run(() =>
            {
                using var connection = new MySqlConnection(_dbCommon.ConnectionString);
                connection.Open();
                var cmd = _dbCommon.ProcedureQuery(connection, "get_animal_by_visit_id");
                cmd.Parameters.AddWithValue("@id_num", visitId);

                var animal = _dbCommon.GenericReaderToMapper(cmd, _dbMappers.AnimalMapper);

                connection.Close();
                return animal as Animal;
            });

        }

        public Task<IEnumerable<AnimalSearch>> FindAnimalAsync(Animal animal)
        {
            var mySqlCommandParameters = new Dictionary<string, object>
            {
                {"animal_name", animal.Name},
                {"animal_type", animal.Type},
                {"animal_breed", animal.Breed},
                {"animal_color", animal.Color},
                {"animal_gender", animal.Gender},
                {"animal_chip_number", animal.ChipNumber},
            };
            return Task.Run(() => 
                GetListFromDbAsync("search_animal_with_owner", _dbMappers.AnimalSearchMapper, mySqlCommandParameters).Cast<AnimalSearch>()
            );
        }
    }
}
