using MySql.Data.MySqlClient;
using PetAdmin.Core.Interfaces;

namespace Petadmin.Repository.Interfaces
{
    public interface IDbMappers
    {
        /// <summary>
        /// Convert - Reader To Owner
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New IGenericDbEntity.
        /// </returns>
        IGenericDbEntity OwnerMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To Animal
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New IGenericDbEntity.
        /// </returns>
        IGenericDbEntity AnimalMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To AnimalSearch
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New IGenericDbEntity.
        /// </returns>
        IGenericDbEntity AnimalSearchMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To Visit
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New IGenericDbEntity.
        /// </returns>
        IGenericDbEntity VisitMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To string (animal type)
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return string.
        /// </returns>
        string AnimalTypeMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To string (animal gender)
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return string.
        /// </returns>
        string AnimalGenderMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To string (animal breed)
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return string.
        /// </returns>
        IGenericDbEntity AnimalBreedMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To string (animal color)
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return string.
        /// </returns>
        string AnimalColorMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To string (animal reminder)
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return string.
        /// </returns>
        string AnimalReminderMapper(MySqlDataReader reader);
        

        /// <summary>
        /// Convert - Reader To PreventiveReminder
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New PreventiveReminder.
        /// </returns>
        IGenericDbEntity PreventiveReminderMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To Treatment
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New Treatment.
        /// </returns>
        IGenericDbEntity TreatmentMapper(MySqlDataReader reader);
        
        /// <summary>
        /// Convert - Reader To Diagnosis
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New Diagnosis.
        /// </returns>
        IGenericDbEntity DiagnosisMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To PreventiveTreatment
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New PreventiveTreatment.
        /// </returns>
        IGenericDbEntity PreventiveTreatmentMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To Prescription
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New Prescription.
        /// </returns>
        IGenericDbEntity PrescriptionMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To Drug string
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New string.
        /// </returns>
        string DrugMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To DrugPeriod string
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New string.
        /// </returns>
        string DrugPeriodMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To DrugFrequency string
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New string.
        /// </returns>
        string DrugFrequencyMapper(MySqlDataReader reader);
        
        /// <summary>
        /// Convert - Reader To DrugDosage string
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New string.
        /// </returns>
        string DrugDosageMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To Debt
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New Debt.
        /// </returns>
        IGenericDbEntity DebtMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To FollowUp
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New FollowUp.
        /// </returns>
        IGenericDbEntity FollowUpMapper(MySqlDataReader reader);


        /// <summary>
        /// Convert - Reader To FollowUpAllItem
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New FollowUpAllItem.
        /// </returns>
        IGenericDbEntity FollowUpAllItemMapper(MySqlDataReader reader);


        /// <summary>
        /// Convert - Reader To ApplicationUser
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New ApplicationUser.
        /// </returns>
        IGenericDbEntity ApplicationUserMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To ApplicationRole
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New ApplicationRole.
        /// </returns>
        IGenericDbEntity ApplicationRoleMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To ApplicationUserRole
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New ApplicationUserRole.
        /// </returns>
        IGenericDbEntity ApplicationUserRoleMapper(MySqlDataReader reader);


        /// <summary>
        /// Convert - Reader To UserRole string
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New string.
        /// </returns>
        public string UserRoleMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To gender string
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New string.
        /// </returns>
        public string GenderMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To RabiesReport
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New RabiesReport.
        /// </returns>
        IGenericDbEntity RabiesReportMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To DebtSheetItem
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New DebtSheetItem .
        /// </returns>
        IGenericDbEntity DebtSheetItemMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To VisitedOwnersItem
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New VisitedOwnersItem .
        /// </returns>
        IGenericDbEntity VisitedOwnersItemMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To SmsTemplate
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New SmsTemplate .
        /// </returns>
        IGenericDbEntity SmsTemplateMapper(MySqlDataReader reader);

        /// <summary>
        /// Convert - Reader To SmsPreventiveReminder
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New SmsPreventiveReminder.
        /// </returns>
        IGenericDbEntity SmsPreventiveReminderMapper(MySqlDataReader reader);
    }
}
