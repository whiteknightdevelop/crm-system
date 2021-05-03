using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;
using System.Threading;

namespace Petadmin.Repository.DbContext
{
    public interface IPetadminDbContext
    {
        /// <summary>
        /// Returns database entity by id. 
        /// </summary>
        /// <param name="id">integer type</param>
        /// <returns>
        /// Return Task of TEntity.
        /// </returns>
        TEntity Get<TEntity>(int id) where TEntity : class, new();

        /// <summary>
        /// Returns database entity by id asynchronously. 
        /// </summary>
        /// <param name="id">integer type</param>
        /// <returns>
        /// Return Task of TEntity.
        /// </returns>
        Task<TEntity> GetAsync<TEntity>(int id) where TEntity : class, new();
        
        /// <summary>
        /// Add new entity to database. 
        /// </summary>
        /// <param name="entity">TEntity type</param>
        /// <returns>
        /// Return confirmation integer - LAST_INSERT_ID().
        /// </returns>
        int Add<TEntity>(TEntity entity) where TEntity : class, new();

        /// <summary>
        /// Add new entity to database asynchronously. 
        /// </summary>
        /// <param name="entity">TEntity type</param>
        /// <returns>
        /// Return confirmation integer - LAST_INSERT_ID().
        /// </returns>
        Task<int> AddAsync<TEntity>(TEntity entity) where TEntity : class, new();
        
        /// <summary>
        /// Update entity in database. 
        /// </summary>
        /// <param name="entity">TEntity type</param>
        /// <returns>
        /// Return boolean that indicates if number of rows was affected.
        /// </returns>
        bool Update<TEntity>(TEntity entity) where TEntity : class, new();

        /// <summary>
        /// Update entity in database asynchronously.  
        /// </summary>
        /// <param name="entity">TEntity type</param>
        /// <returns>
        /// Return boolean that indicates if number of rows was affected.
        /// </returns>
        Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, new();

        /// <summary>
        /// Remove entity from database. 
        /// </summary>
        /// <param name="entity">TEntity type</param>
        /// <returns>
        /// Return boolean that indicates if number of rows was affected.
        /// </returns>
        bool Remove<TEntity>(TEntity entity) where TEntity : class, new();
        Task<bool> RemoveAsync<TEntity>(TEntity entity, CancellationToken token) where TEntity : class, new();
        Task<bool> RemoveAsync<TEntity>(TEntity entity, CancellationTokenSource cts, CancellationToken token) where TEntity : class, new();

        /// <summary>
        /// Remove entity from database asynchronously. 
        /// </summary>
        /// <param name="entity">TEntity type</param>
        /// <returns>
        /// Return boolean that indicates if number of rows was affected.
        /// </returns>
        Task<bool> RemoveAsync<TEntity>(TEntity entity) where TEntity : class, new();

        /// <summary>
        /// Get animal owner by animal id from database asynchronously. 
        /// </summary>
        /// <param name="animalId">int type</param>
        /// <returns>
        /// Return animal owner.
        /// </returns>
        Task<Owner> GetAnimalOwnerByIdAsync(int animalId);
        
        /// <summary>
        /// Get animal list by owner id from database asynchronously. 
        /// </summary>
        /// <param name="ownerId">int type</param>
        /// <returns>
        /// Return list of owner animals.
        /// </returns>
        Task<IEnumerable<Animal>> GetAnimalsListByOwnerIdAsync(int ownerId);

        /// <summary>
        /// Get visit list by animal id from database asynchronously. 
        /// </summary>
        /// <param name="animalId">int type</param>
        /// <returns>
        /// Return list of animal visits.
        /// </returns>
        Task<IEnumerable<Visit>> GetVisitsListByAnimalIdAsync(int animalId);

        /// <summary>
        /// Get animals types list from database asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of animals types.
        /// </returns>
        Task<IEnumerable<string>> GetAnimalsTypesListAsync();

        /// <summary>
        /// Get animals genders list from database asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of animals genders.
        /// </returns>
        Task<IEnumerable<string>> GetAnimalsGendersListAsync();

        /// <summary>
        /// Get animals breeds list from database asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of animals breeds.
        /// </returns>
        Task<IEnumerable<Breed>> GetAnimalsBreedsListAsync();

        /// <summary>
        /// Get animals colors list from database asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of animals colors.
        /// </returns>
        Task<IEnumerable<string>> GetAnimalsColorsListAsync();

        /// <summary>
        /// Get preventive reminders list from database asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of preventive reminders.
        /// </returns>
        Task<IEnumerable<PreventiveReminder>> GetPreventiveRemindersListAsync(int animalId);
        
        /// <summary>
        /// Get animal by visit id from database asynchronously. 
        /// </summary>
        /// <returns>
        /// Return animal.
        /// </returns>
        Task<Animal> GetAnimalByVisitIdAsync(int visitId);

        /// <summary>
        /// Get owner by visit id from database asynchronously. 
        /// </summary>
        /// <returns>
        /// Return owner.
        /// </returns>
        Task<Owner> GetOwnerByVisitIdAsync(int visitId);

        /// <summary>
        /// Get owner total debt amount from database asynchronously. 
        /// </summary>
        /// <returns>
        /// Return total debt amount.
        /// </returns>
        Task<int> GetOwnerTotalDebtAmountAsync(int ownerId);

        /// <summary>
        /// Get treatment list from database asynchronously. 
        /// </summary>
        /// <returns>
        /// Return treatment.
        /// </returns>
        Task<IEnumerable<Treatment>> GetTreatmentListAsync();

        /// <summary>
        /// Get diagnoses list from database asynchronously. 
        /// </summary>
        /// <returns>
        /// Return diagnosis.
        /// </returns>
        Task<IEnumerable<Diagnosis>> GetDiagnosesListAsync();

        /// <summary>
        /// Get visit preventive treatments list from database asynchronously. 
        /// </summary>
        /// <returns>
        /// Return preventive treatment.
        /// </returns>
        Task<IEnumerable<PreventiveTreatment>> GetPreventiveTreatmentsListByVisitIdAsync(int visitId);

        /// <summary>
        /// Get all preventive treatments list from database asynchronously. 
        /// </summary>
        /// <returns>
        /// Return preventive treatment.
        /// </returns>
        Task<IEnumerable<PreventiveTreatment>> GetPreventiveTreatmentsListAsync();

        /// <summary>
        /// Get all owners that match the parameters asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of owners.
        /// </returns>
        Task<IEnumerable<Owner>> FindOwnerAsync(Owner owner);

        /// <summary>
        /// Get all animals that match the parameters asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of animals.
        /// </returns>
        Task<IEnumerable<AnimalSearch>> FindAnimalAsync(Animal animal);

        /// <summary>
        /// Get visit all prescriptions asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of prescriptions.
        /// </returns>
        Task<IEnumerable<Prescription>> GetPrescriptionsListByVisitIdAsync(int visitId);

        /// <summary>
        /// Get list of all drugs asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of strings.
        /// </returns>
        Task<IEnumerable<string>> GetDrugsListAsync();

        /// <summary>
        /// Get list of all drugs periods asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of strings.
        /// </returns>
        Task<IEnumerable<string>> GetDrugPeriodsListAsync();

        /// <summary>
        /// Get list of all drug frequencies asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of strings.
        /// </returns>
        Task<IEnumerable<string>> GetDrugFrequencysListAsync();

        /// <summary>
        /// Get list of all drug dosages asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of strings.
        /// </returns>
        Task<IEnumerable<string>> GetDrugDosagesListAsync();

        /// <summary>
        /// Get list of all debts asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of debt.
        /// </returns>
        Task<IEnumerable<Debt>> GetDebtsListByOwnerIdAsync(int ownerId);

        /// <summary>
        /// Get list of all FollowUps asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of FollowUps.
        /// </returns>
        Task<IEnumerable<FollowUp>> GetFollowUpsListByAnimalIdAsync(int animalId);

        /// <summary>
        /// Get user by username. 
        /// </summary>
        /// <returns>
        /// Return ApplicationUser.
        /// </returns>
        Task<ApplicationUser> FindByUserNameAsync(string normalizedUserName, CancellationToken cancellationToken);

        /// <summary>
        /// Get role by name. 
        /// </summary>
        /// <returns>
        /// Return ApplicationRole.
        /// </returns>
        Task<ApplicationRole> FindByRoleNameAsync(string normalizedRoleName, CancellationToken cancellationToken);

        /// <summary>
        /// Get user role. 
        /// </summary>
        /// <returns>
        /// Return ApplicationRole.
        /// </returns>
        Task<ApplicationUserRole> FindUserRoleAsync(int userId, int roleEntityId, CancellationToken cancellationToken);

        /// <summary>
        /// Get list of user roles from database asynchronously. 
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="cancellationToken">Task cancellation token</param>
        /// <returns>
        /// Return list of roles names.
        /// </returns>
        Task<List<string>> GetRolesByUserIdAsync(int userId, CancellationToken cancellationToken);

        /// <summary>
        /// Add new UserRole to database asynchronously. 
        /// </summary>
        /// <param name="entity">ApplicationUserRole type</param>
        /// <param name="cancellationToken">Task cancellation token</param>
        /// <returns>
        /// Return confirmation integer.
        /// </returns>
        Task<int> AddUserRoleAsync(ApplicationUserRole entity, CancellationToken cancellationToken);

        /// <summary>
        /// Update entity in database asynchronously.  
        /// </summary>
        /// <param name="entity">ApplicationUserRole type</param>
        /// <param name="cancellationToken">Task cancellation token</param>
        /// <returns>
        /// Return boolean that indicates if rows was affected.
        /// </returns>
        Task<bool> UpdateUserRoleAsync(ApplicationUserRole entity, CancellationToken cancellationToken);

        /// <summary>
        /// Remove entity from database asynchronously. 
        /// </summary>
        /// <param name="entity">ApplicationUserRole type</param>
        /// <param name="cancellationToken">Task cancellation token</param>
        /// <returns>
        /// Return boolean that indicates if rows was affected.
        /// </returns>
        Task<bool> RemoveUserRoleAsync(ApplicationUserRole entity, CancellationToken cancellationToken);

        /// <summary>
        /// Get genders list asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of genders.
        /// </returns>
        Task<IEnumerable<string>> GetGendersListAsync();

        /// <summary>
        /// Get reminders list asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of reminders.
        /// </returns>
        Task<IEnumerable<string>> GetAnimalsRemindersListAsync();

        /// <summary>
        /// Get rabies list by date interval asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of RabiesReport.
        /// </returns>
        Task<IEnumerable<RabiesReport>> GetRabiesListByDateInterval(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Get all debtors list asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of DebtSheetItem.
        /// </returns>
        Task<IEnumerable<DebtSheetItem>> GetDebtSheet();

        /// <summary>
        /// Get owners visited in the last x days list asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of VisitedOwnersItem.
        /// </returns>
        Task<IEnumerable<VisitedOwnersItem>> GetOwnersVisitedLastXDays(int days);

        /// <summary>
        /// Get owners that didn't visited in the last x days list asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of VisitedOwnersItem.
        /// </returns>
        Task<IEnumerable<VisitedOwnersItem>> GetOwnersNotVisitedLastXDays(int days);

        /// <summary>
        /// Get all followup list asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of FollowUp.
        /// </returns>
        Task<IEnumerable<FollowUpAllItem>> GetFollowUpAllList(DateTime from);

        /// <summary>
        /// Get all sms templates list asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of SmsTemplate.
        /// </returns>
        Task<IEnumerable<SmsTemplate>> GetAllSmsTemplates();

        /// <summary>
        /// Get sms preventive reminders by data interval list asynchronously. 
        /// </summary>
        /// <returns>
        /// Return list of SmsPreventiveReminder.
        /// </returns>
        Task<IEnumerable<SmsPreventiveReminder>> GetPreventiveReminderByDateInterval(DateTime from, DateTime to);

        /// <summary>
        /// Get amount of all visit prescriptions asynchronously. 
        /// </summary>
        /// <returns>
        /// Return number of visit prescriptions.
        /// </returns>
        Task<int> GetVisitPrescriptionsNumberAsync(int visitId);
    }
}
