using MySql.Data.MySqlClient;
using PetAdmin.Core.Interfaces;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Repository
{
    public class DbMappers: IDbMappers
    {
        /// <summary>
        /// Convert - Reader To Owner
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New IGenericDbEntity.
        /// </returns>
        public IGenericDbEntity OwnerMapper(MySqlDataReader reader)
        {
            return new Owner
            {
                OwnerId = reader.GetInt32("owner_id"),
                IdNumber = reader.IsDBNull(1) ? "" : reader.GetString("id_number"),
                FirstName = reader.IsDBNull(2) ? "" : reader.GetString("first_name"),
                LastName = reader.IsDBNull(3) ? "" : reader.GetString("last_name"),
                DateOfBirth = reader.IsDBNull(4) ? null : reader.GetDateTime("date_of_birth"),
                City = reader.IsDBNull(5) ? "" : reader.GetString("city"),
                City2 = reader.IsDBNull(6) ? "" : reader.GetString("city_2"),
                Street = reader.IsDBNull(7) ? "" : reader.GetString("street"),
                Street2 = reader.IsDBNull(8) ? "" : reader.GetString("street_2"),
                HouseNumber = reader.IsDBNull(9) ? "" : reader.GetString("house_number"),
                HouseNumber2 = reader.IsDBNull(10) ? "" : reader.GetString("house_number_2"),
                ApartmentNumber = reader.IsDBNull(11) ? "" : reader.GetString("apartment_number"),
                ApartmentNumber2 = reader.IsDBNull(12) ? "" : reader.GetString("apartment_number_2"),
                PostalCode = reader.IsDBNull(13) ? "" : reader.GetString("postal_code"),
                PostalCode2 = reader.IsDBNull(14) ? "" : reader.GetString("postal_code_2"),
                Phone = reader.IsDBNull(15) ? "" : reader.GetString("phone"),
                Mobile = reader.IsDBNull(16) ? "" : reader.GetString("mobile"),
                MailBox = reader.IsDBNull(17) ? 0 : reader.GetInt32("mailbox"),
                Email = reader.IsDBNull(18) ? "" : reader.GetString("email"),
                Comment = reader.IsDBNull(19) ? "" : reader.GetString("comment"),
                CreatedDate = reader.IsDBNull(20) ? null : reader.GetDateTime("owner_created_date"),
                UserId = reader.IsDBNull(21) ? 0 : reader.GetInt32("owner_user_id"),
                Archive = reader.IsDBNull(22) || reader.GetBoolean("owner_archive"),
                User = new ApplicationUserMysqlResponse
                {
                    Id = reader.IsDBNull(23) ? 0 : reader.GetInt32("Id"),
                    UserName = reader.IsDBNull(24) ? null : reader.GetString("UserName"),
                    FirstName = reader.IsDBNull(26) ? null : reader.GetString("FirstName"),
                    LastName = reader.IsDBNull(27) ? null : reader.GetString("LastName"),
                    Gender = reader.IsDBNull(28) ? null : reader.GetString("Gender"),
                    License = reader.IsDBNull(29) ? null : reader.GetString("License"),
                    Email = reader.IsDBNull(30) ? null : reader.GetString("Email"),
                    PhoneNumber = reader.IsDBNull(31) ? null : reader.GetString("PhoneNumber"),
                },
            };
        }

        /// <summary>
        /// Convert - Reader To Animal
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New IGenericDbEntity.
        /// </returns>
        public IGenericDbEntity AnimalMapper(MySqlDataReader reader)
        {
            return new Animal
            {
                AnimalId = reader.IsDBNull(0) ? 0 : reader.GetInt32("animal_id"),
                OwnerId = reader.IsDBNull(1) ? 0 : reader.GetInt32("owner_id"),
                CreatedDate = reader.IsDBNull(2) ? null : reader.GetDateTime("created_date"),
                Name = reader.IsDBNull(3) ? null : reader.GetString("name"),
                Type = reader.IsDBNull(4) ? null : reader.GetString("type"),
                Breed = reader.IsDBNull(5) ? null : reader.GetString("breed"),
                Color = reader.IsDBNull(6) ? null : reader.GetString("color"),
                Gender = reader.IsDBNull(7) ? null : reader.GetString("gender"),
                DateOfBirth = reader.IsDBNull(8) ? null : reader.GetDateTime("date_of_birth"),
                Active = reader.IsDBNull(9) || reader.GetBoolean("active"),
                Sterilized = reader.IsDBNull(10) || reader.GetBoolean("sterilized"),
                DateOfSterilization = reader.IsDBNull(11) ? null : reader.GetDateTime("date_of_sterilization"),
                ChipNumber = reader.IsDBNull(12) ? null : reader.GetString("chip_number"),
                ChipMarkDate = reader.IsDBNull(13) ? null : reader.GetDateTime("chip_mark_date"),
                Comment = reader.IsDBNull(14) ? null : reader.GetString("comment"),
                Status = reader.IsDBNull(15) || reader.GetBoolean("status"),
                Archive = reader.IsDBNull(17) || reader.GetBoolean("animal_archive"),
                User = new ApplicationUserMysqlResponse
                {
                    Id = reader.IsDBNull(18) ? 0 : reader.GetInt32("Id"),
                    UserName = reader.IsDBNull(19) ? null : reader.GetString("UserName"),
                    FirstName = reader.IsDBNull(21) ? null : reader.GetString("FirstName"),
                    LastName = reader.IsDBNull(22) ? null : reader.GetString("LastName"),
                    Gender = reader.IsDBNull(23) ? null : reader.GetString("Gender"),
                    License = reader.IsDBNull(24) ? null : reader.GetString("License"),
                    Email = reader.IsDBNull(25) ? null : reader.GetString("Email"),
                    PhoneNumber = reader.IsDBNull(26) ? null : reader.GetString("PhoneNumber"),
                }
            };
        }

        /// <summary>
        /// Convert - Reader To AnimalSearch
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New IGenericDbEntity.
        /// </returns>
        public IGenericDbEntity AnimalSearchMapper(MySqlDataReader reader)
        {
            return new AnimalSearch
            {
                Animal = new Animal
                {
                    AnimalId = reader.IsDBNull(0) ? 0 : reader.GetInt32("animal_id"),
                    OwnerId = reader.IsDBNull(1) ? 0 : reader.GetInt32("owner_id"),
                    Name = reader.IsDBNull(2) ? null : reader.GetString("name"),
                    Type = reader.IsDBNull(3) ? null : reader.GetString("type"),
                    Breed = reader.IsDBNull(4) ? null : reader.GetString("breed"),
                    Color = reader.IsDBNull(5) ? null : reader.GetString("color"),
                    Gender = reader.IsDBNull(6) ? null : reader.GetString("gender"),
                    ChipNumber = reader.IsDBNull(7) ? null : reader.GetString("chip_number")
                },
                Owner = new Owner
                {
                    FirstName = reader.IsDBNull(8) ? "" : reader.GetString("first_name"),
                    LastName = reader.IsDBNull(9) ? "" : reader.GetString("last_name"),
                    City = reader.IsDBNull(10) ? "" : reader.GetString("city"),
                    Street = reader.IsDBNull(11) ? "" : reader.GetString("street"),
                    Phone = reader.IsDBNull(12) ? "" : reader.GetString("phone"),
                    Email = reader.IsDBNull(13) ? "" : reader.GetString("email")
                }
            };
        }

        /// <summary>
        /// Convert - Reader To Visit
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New IGenericDbEntity.
        /// </returns>
        public IGenericDbEntity VisitMapper(MySqlDataReader reader)
        {
            return new Visit
            {
                VisitId = reader.IsDBNull(0) ? 0 : reader.GetInt32("visit_id"),
                AnimalId = reader.IsDBNull(1) ? 0 : reader.GetInt32("animal_id"),
                VisitTime = reader.IsDBNull(2) ? null : reader.GetDateTime("visit_time"),
                NextVisitDate = reader.IsDBNull(3) ? null : reader.GetDateTime("visit_next"),
                Cause = reader.IsDBNull(4) ? null : reader.GetString("cause"),
                Symptoms = reader.IsDBNull(5) ? null : reader.GetString("symptoms"),
                LabResults = reader.IsDBNull(6) ? null : reader.GetString("visit_lab_result"),
                Comment = reader.IsDBNull(7) ? null : reader.GetString("comment"),
                Temperature = reader.IsDBNull(8) ? 0 : reader.GetDecimal("temperature"),
                Weight = reader.IsDBNull(9) ? 0 : reader.GetDecimal("weight"),
                Pulse = reader.IsDBNull(10) ? 0 : reader.GetInt32("pulse"),
                Diagnosis1 = reader.IsDBNull(11) ? null : reader.GetString("diagnosis_1"),
                Diagnosis2 = reader.IsDBNull(12) ? null : reader.GetString("diagnosis_2"),
                Diagnosis3 = reader.IsDBNull(13) ? null : reader.GetString("diagnosis_3"),
                Treatment1 = reader.IsDBNull(14) ? null : reader.GetString("treatment_1"),
                Treatment2 = reader.IsDBNull(15) ? null : reader.GetString("treatment_2"),
                Treatment3 = reader.IsDBNull(16) ? null : reader.GetString("treatment_3"),
                Treatment4 = reader.IsDBNull(17) ? null : reader.GetString("treatment_4"),
                Treatment5 = reader.IsDBNull(18) ? null : reader.GetString("treatment_5"),
                Treatment6 = reader.IsDBNull(19) ? null : reader.GetString("treatment_6"),
                UserId = reader.IsDBNull(20) ? 0 : reader.GetInt32("visit_user_id"),
                Archive = reader.IsDBNull(21) || reader.GetBoolean("visit_archive"),
                User = new ApplicationUserMysqlResponse
                {
                    Id = reader.IsDBNull(22) ? 0 : reader.GetInt32("Id"),
                    UserName = reader.IsDBNull(23) ? null : reader.GetString("UserName"),
                    FirstName = reader.IsDBNull(25) ? null : reader.GetString("FirstName"),
                    LastName = reader.IsDBNull(26) ? null : reader.GetString("LastName"),
                    Gender = reader.IsDBNull(27) ? null : reader.GetString("Gender"),
                    License = reader.IsDBNull(28) ? null : reader.GetString("License"),
                    Email = reader.IsDBNull(29) ? null : reader.GetString("Email"),
                    PhoneNumber = reader.IsDBNull(30) ? null : reader.GetString("PhoneNumber"),
                }
            };
        }

        /// <summary>
        /// Convert - Reader To string (animal type)
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return string.
        /// </returns>
        public string AnimalTypeMapper(MySqlDataReader reader)
        {
            return reader.IsDBNull(0) ? null : reader.GetString("animal_types");
        }

        /// <summary>
        /// Convert - Reader To string (animal gender)
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return string.
        /// </returns>
        public string AnimalGenderMapper(MySqlDataReader reader)
        {
            return reader.IsDBNull(0) ? null : reader.GetString("idgender_ref");
        }

        /// <summary>
        /// Convert - Reader To string (animal breed)
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return Breed type.
        /// </returns>
        public IGenericDbEntity AnimalBreedMapper(MySqlDataReader reader)
        {
            return new Breed
            {
                BreedId = reader.IsDBNull(0) ? null : reader.GetString("id"),
                BreedName = reader.IsDBNull(1) ? null : reader.GetString("name_breed"),
                Type = reader.IsDBNull(2) ? null : reader.GetString("animal_types_ref"),
            };
        }

        /// <summary>
        /// Convert - Reader To string (animal color)
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return string.
        /// </returns>
        public string AnimalColorMapper(MySqlDataReader reader)
        {
            return reader.IsDBNull(0) ? null : reader.GetString("id_colors");
        }

        public string AnimalReminderMapper(MySqlDataReader reader)
        {
            return reader.IsDBNull(0) ? null : reader.GetString("idreminders_drop_list");
        }

        /// <summary>
        /// Convert - Reader To PreventiveReminder
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New PreventiveReminder.
        /// </returns>
        public IGenericDbEntity PreventiveReminderMapper(MySqlDataReader reader)
        {
            return new PreventiveReminder
            {
                VisitId = reader.IsDBNull(0) ? 0 : reader.GetInt32("id_visit_ref"),
                TreatmentId = reader.IsDBNull(1) ? 0 : reader.GetInt32("id_treatment_ref"),
                ReminderId = reader.IsDBNull(2) ? 0 : reader.GetInt32("id_next_treatment"),
                AnimalId = reader.IsDBNull(3) ? 0 : reader.GetInt32("animal_id"),
                PreventiveReminderName = reader.IsDBNull(4) ? null : reader.GetString("next_treatment_name"),
                ReminderDate = reader.IsDBNull(5) ? null : reader.GetDateTime("date_reminder"),
                RemainingNumOfDays = reader.IsDBNull(6) ? 0 : reader.GetInt32("remaining_days"),
                PreventiveTreatmentType = !reader.IsDBNull(7) && reader.GetBoolean("is_preventive_treatment")
            };
        }

        /// <summary>
        /// Convert - Reader To Treatment
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New Treatment.
        /// </returns>
        public IGenericDbEntity TreatmentMapper(MySqlDataReader reader)
        {
            return new Treatment
            {
                TreatmentId = reader.IsDBNull(0) ? 0 : reader.GetInt32("id_treatment"),
                Name = reader.IsDBNull(1) ? null : reader.GetString("name_treatment"),
            };
        }

        /// <summary>
        /// Convert - Reader To Diagnosis
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New Diagnosis.
        /// </returns>
        public IGenericDbEntity DiagnosisMapper(MySqlDataReader reader)
        {
            return new Diagnosis
            {
                DiagnosisId = reader.IsDBNull(0) ? 0 : reader.GetInt32("diagnosis_id"),
                Name = reader.IsDBNull(1) ? null : reader.GetString("name"),
            };
        }

        /// <summary>
        /// Convert - Reader To PreventiveTreatment
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New PreventiveTreatment.
        /// </returns>
        public IGenericDbEntity PreventiveTreatmentMapper(MySqlDataReader reader)
        {
            return new PreventiveTreatment
            {
                VisitId = reader.IsDBNull(0) ? 0 : reader.GetInt32("visit_id"),
                TreatmentId = reader.IsDBNull(1) ? 0 : reader.GetInt32("id_treatment_ref"),
                Name = reader.IsDBNull(2) ? null : reader.GetString("name_treatment"),
                RemainingNumOfDays = reader.IsDBNull(3) ? 0 : reader.GetInt32("remaining_days"),
                NextTreatmentName = reader.IsDBNull(4) ? null : reader.GetString("name_next_treatment"),
                User = new ApplicationUserMysqlResponse
                {
                    Id = reader.IsDBNull(5) ? 0 : reader.GetInt32("Id"),
                    UserName = reader.IsDBNull(6) ? null : reader.GetString("UserName"),
                    FirstName = reader.IsDBNull(7) ? null : reader.GetString("FirstName"),
                    LastName = reader.IsDBNull(8) ? null : reader.GetString("LastName"),
                    Gender = reader.IsDBNull(9) ? null : reader.GetString("Gender"),
                    License = reader.IsDBNull(10) ? null : reader.GetString("License"),
                    Email = reader.IsDBNull(11) ? null : reader.GetString("Email"),
                    PhoneNumber = reader.IsDBNull(12) ? null : reader.GetString("PhoneNumber"),
                }
            };
        }

        /// <summary>
        /// Convert - Reader To Prescription
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New Prescription.
        /// </returns>
        public IGenericDbEntity PrescriptionMapper(MySqlDataReader reader)
        {
            return new Prescription
            {
                PrescriptionId = reader.IsDBNull(0) ? 0 : reader.GetInt32("prescription_id"),
                VisitId = reader.IsDBNull(1) ? 0 : reader.GetInt32("visit_id_prescription"),
                DrugName = reader.IsDBNull(2) ? null : reader.GetString("drug_name_prescription"),
                DrugDosage = reader.IsDBNull(3) ? null : reader.GetString("drug_dosage_prescription"),
                DrugFrequency = reader.IsDBNull(4) ? null : reader.GetString("drug_frequency_prescription"),
                DrugPeriod = reader.IsDBNull(5) ? null : reader.GetString("drug_period_prescription"),
                DrugComment = reader.IsDBNull(6) ? null : reader.GetString("drug_comment_prescription")
            };
        }

        /// <summary>
        /// Convert - Reader To Drug string
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New string.
        /// </returns>
        public string DrugMapper(MySqlDataReader reader)
        {
            return reader.IsDBNull(1) ? null : reader.GetString("name");
        }

        /// <summary>
        /// Convert - Reader To DrugPeriod string
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New string.
        /// </returns>
        public string DrugPeriodMapper(MySqlDataReader reader)
        {
            return reader.IsDBNull(1) ? null : reader.GetString("period");
        }

        /// <summary>
        /// Convert - Reader To DrugFrequency string
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New string.
        /// </returns>
        public string DrugFrequencyMapper(MySqlDataReader reader)
        {
            return reader.IsDBNull(1) ? null : reader.GetString("frequency");
        }

        /// <summary>
        /// Convert - Reader To DrugDosage string
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New string.
        /// </returns>
        public string DrugDosageMapper(MySqlDataReader reader)
        {
            return reader.IsDBNull(1) ? null : reader.GetString("dosage");
        }

        /// <summary>
        /// Convert - Reader To Debt
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New Debt.
        /// </returns>
        public IGenericDbEntity DebtMapper(MySqlDataReader reader)
        {
            return new Debt
            {
                DebtId = reader.IsDBNull(0) ? 0 : reader.GetInt32("debt_id"),
                OwnerId = reader.IsDBNull(1) ? 0 : reader.GetInt32("owner_id_num"),
                AnimalName = reader.IsDBNull(2) ? null : reader.GetString("animal_name"),
                Cause = reader.IsDBNull(3) ? null : reader.GetString("cause_of_debt"),
                DebtAmount = reader.IsDBNull(4) ? 0 : reader.GetInt32("debt_amount"),
                PaidAmount = reader.IsDBNull(5) ? 0 : reader.GetInt32("paid"),
                DebtDate = reader.IsDBNull(6) ? null : reader.GetDateTime("debt_date")
            };
        }

        /// <summary>
        /// Convert - Reader To FollowUp
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New FollowUp.
        /// </returns>
        public IGenericDbEntity FollowUpMapper(MySqlDataReader reader)
        {
            return new FollowUp
            {
                FollowUpId = reader.IsDBNull(0) ? 0 : reader.GetInt32("followup_id"),
                AnimalId = reader.IsDBNull(1) ? 0 : reader.GetInt32("animal_id"),
                Date = reader.IsDBNull(2) ? null : reader.GetDateTime("date_followup"),
                Cause = reader.IsDBNull(3) ? null : reader.GetString("cause_followup"),
                Status = reader.IsDBNull(4) || reader.GetBoolean("status_followup"),
            };
        }

        /// <summary>
        /// Convert - Reader To FollowUpAllItem
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New FollowUpAllItem.
        /// </returns>
        public IGenericDbEntity FollowUpAllItemMapper(MySqlDataReader reader)
        {
            return new FollowUpAllItem
            {
                FollowUp = new FollowUp
                {
                    FollowUpId = reader.IsDBNull(0) ? 0 : reader.GetInt32("followup_id"),
                    AnimalId = reader.IsDBNull(1) ? 0 : reader.GetInt32("animal_id"),
                    Date = reader.IsDBNull(2) ? null : reader.GetDateTime("date_followup"),
                    Cause = reader.IsDBNull(3) ? null : reader.GetString("cause_followup"),
                    Status = reader.IsDBNull(4) || reader.GetBoolean("status_followup"),
                },
                Animal = new Animal
                {
                    OwnerId = reader.IsDBNull(5) ? 0 : reader.GetInt32("owner_id"),
                    CreatedDate = reader.IsDBNull(6) ? null : reader.GetDateTime("created_date"),
                    Name = reader.IsDBNull(7) ? null : reader.GetString("name"),
                    Type = reader.IsDBNull(8) ? null : reader.GetString("type"),
                    Breed = reader.IsDBNull(9) ? null : reader.GetString("breed"),
                    Color = reader.IsDBNull(10) ? null : reader.GetString("color"),
                    Gender = reader.IsDBNull(11) ? null : reader.GetString("gender"),
                    DateOfBirth = reader.IsDBNull(12) ? null : reader.GetDateTime("animal_date_of_birth"),
                    Active = reader.IsDBNull(13) || reader.GetBoolean("active"),
                    Sterilized = reader.IsDBNull(14) || reader.GetBoolean("sterilized"),
                    DateOfSterilization = reader.IsDBNull(15) ? null : reader.GetDateTime("date_of_sterilization"),
                    ChipNumber = reader.IsDBNull(16) ? null : reader.GetString("chip_number"),
                    ChipMarkDate = reader.IsDBNull(17) ? null : reader.GetDateTime("chip_mark_date"),
                    Comment = reader.IsDBNull(18) ? null : reader.GetString("comment"),
                    Status = reader.IsDBNull(19) || reader.GetBoolean("status"),
                    UserId = reader.IsDBNull(20) ? 0 : reader.GetInt32("animal_user_id"),
                    Archive = reader.IsDBNull(21) || reader.GetBoolean("animal_archive"),
                },
                Owner = new Owner
                {
                    OwnerId = reader.IsDBNull(22) ? 0 : reader.GetInt32("owner_id"),
                    IdNumber = reader.IsDBNull(23) ? "" : reader.GetString("id_number"),
                    FirstName = reader.IsDBNull(24) ? "" : reader.GetString("first_name"),
                    LastName = reader.IsDBNull(25) ? "" : reader.GetString("last_name"),
                    DateOfBirth = reader.IsDBNull(26) ? null : reader.GetDateTime("date_of_birth"),
                    City = reader.IsDBNull(27) ? "" : reader.GetString("city"),
                    City2 = reader.IsDBNull(28) ? "" : reader.GetString("city_2"),
                    Street = reader.IsDBNull(29) ? "" : reader.GetString("street"),
                    Street2 = reader.IsDBNull(30) ? "" : reader.GetString("street_2"),
                    HouseNumber = reader.IsDBNull(31) ? "" : reader.GetString("house_number"),
                    HouseNumber2 = reader.IsDBNull(32) ? "" : reader.GetString("house_number_2"),
                    ApartmentNumber = reader.IsDBNull(33) ? "" : reader.GetString("apartment_number"),
                    ApartmentNumber2 = reader.IsDBNull(34) ? "" : reader.GetString("apartment_number_2"),
                    PostalCode = reader.IsDBNull(35) ? "" : reader.GetString("postal_code"),
                    PostalCode2 = reader.IsDBNull(36) ? "" : reader.GetString("postal_code_2"),
                    Phone = reader.IsDBNull(37) ? "" : reader.GetString("phone"),
                    Mobile = reader.IsDBNull(38) ? "" : reader.GetString("mobile"),
                    MailBox = reader.IsDBNull(39) ? 0 : reader.GetInt32("mailbox"),
                    Email = reader.IsDBNull(40) ? "" : reader.GetString("email"),
                    Comment = reader.IsDBNull(41) ? "" : reader.GetString("comment"),
                    CreatedDate = reader.IsDBNull(42) ? null : reader.GetDateTime("owner_created_date"),
                    UserId = reader.IsDBNull(43) ? 0 : reader.GetInt32("owner_user_id"),
                    Archive = reader.IsDBNull(44) || reader.GetBoolean("owner_archive"),
                }
            };
        }

        /// <summary>
        /// Convert - Reader To ApplicationUser
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New ApplicationUser.
        /// </returns>
        public IGenericDbEntity ApplicationUserMapper(MySqlDataReader reader)
        {
            return new ApplicationUser
            {
                Id = reader.IsDBNull(0) ? 0 : reader.GetInt32("Id"),
                UserName = reader.IsDBNull(1) ? null : reader.GetString("UserName"),
                NormalizedUserName = reader.IsDBNull(2) ? null : reader.GetString("NormalizedUserName"),
                FirstName = reader.IsDBNull(3) ? null : reader.GetString("FirstName"),
                LastName = reader.IsDBNull(4) ? null : reader.GetString("LastName"),
                Gender = reader.IsDBNull(5) ? null : reader.GetString("Gender"),
                License = reader.IsDBNull(6) ? null : reader.GetString("License"),
                Email = reader.IsDBNull(7) ? null : reader.GetString("Email"),
                NormalizedEmail = reader.IsDBNull(8) ? null : reader.GetString("NormalizedEmail"),
                EmailConfirmed = reader.IsDBNull(9) || reader.GetBoolean("EmailConfirmed"),
                PhoneNumber = reader.IsDBNull(10) ? null : reader.GetString("PhoneNumber"),
                PhoneNumberConfirmed = reader.IsDBNull(11) || reader.GetBoolean("PhoneNumberConfirmed"),
                PasswordHash = reader.IsDBNull(12) ? null : reader.GetString("PasswordHash"),
                SecurityStamp = reader.IsDBNull(13) ? null : reader.GetString("SecurityStamp"),
                ConcurrencyStamp = reader.IsDBNull(14) ? null : reader.GetString("ConcurrencyStamp"),
                TwoFactorEnabled = reader.IsDBNull(15) || reader.GetBoolean("TwoFactorEnabled"),
                LockoutEnabled = reader.IsDBNull(17) || reader.GetBoolean("LockoutEnabled"),
                AccessFailedCount = reader.IsDBNull(18) ? 0 : reader.GetInt32("AccessFailedCount"),

                RefreshToken = new RefreshToken(
                    reader.IsDBNull(21) ? null : reader.GetString("RefreshToken"),
                    reader.IsDBNull(22) ? default : reader.GetDateTime("Expires"),
                    reader.IsDBNull(0) ? 0 : reader.GetInt32("Id"),
                    reader.IsDBNull(23) ? null : reader.GetString("RemoteIpAddress"))
            };
        }

        /// <summary>
        /// Convert - Reader To ApplicationRole
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New ApplicationRole.
        /// </returns>
        public IGenericDbEntity ApplicationRoleMapper(MySqlDataReader reader)
        {
            return new ApplicationRole
            {
                Id = reader.IsDBNull(0) ? 0 : reader.GetInt32("Id"),
                Name = reader.IsDBNull(1) ? null : reader.GetString("Name"),
                NormalizedName = reader.IsDBNull(2) ? null : reader.GetString("NormalizedName"),
                ConcurrencyStamp = reader.IsDBNull(3) ? null : reader.GetString("ConcurrencyStamp"),
            };
        }

        /// <summary>
        /// Convert - Reader To ApplicationUserRole
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New ApplicationUserRole.
        /// </returns>
        public IGenericDbEntity ApplicationUserRoleMapper(MySqlDataReader reader)
        {
            return new ApplicationUserRole
            {
                UserId = reader.IsDBNull(0) ? 0 : reader.GetInt32("UserId"),
                RoleId = reader.IsDBNull(1) ? 0 : reader.GetInt32("RoleId"),
            };
        }

        /// <summary>
        /// Convert - Reader To UserRole string
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New string.
        /// </returns>
        public string UserRoleMapper(MySqlDataReader reader)
        {
            return reader.IsDBNull(0) ? null : reader.GetString("Name");
        }

        /// <summary>
        /// Convert - Reader To string (gender)
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return string.
        /// </returns>
        public string GenderMapper(MySqlDataReader reader)
        {
            return reader.IsDBNull(0) ? null : reader.GetString("genders");
        }

        /// <summary>
        /// Convert - Reader To RabiesReport
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New RabiesReport.
        /// </returns>
        public IGenericDbEntity RabiesReportMapper(MySqlDataReader reader)
        {
            return new RabiesReport
            {
                Visit = new Visit
                {
                    VisitId = reader.IsDBNull(0) ? 0 : reader.GetInt32("visit_id"),
                    VisitTime = reader.IsDBNull(1) ? default : reader.GetDateTime("visit_time"),
                },
                Owner = new Owner
                {
                    IdNumber = reader.IsDBNull(2) ? null : reader.GetString("id_number"),
                    FirstName = reader.IsDBNull(3) ? null : reader.GetString("first_name"),
                    LastName = reader.IsDBNull(4) ? null : reader.GetString("last_name"),
                    DateOfBirth = reader.IsDBNull(5) ? default : reader.GetDateTime("date_of_birth"),
                    City = reader.IsDBNull(6) ? null : reader.GetString("city"),
                    Street = reader.IsDBNull(7) ? null : reader.GetString("street"),
                    HouseNumber = reader.IsDBNull(8) ? null : reader.GetString("house_number"),
                    ApartmentNumber = reader.IsDBNull(9) ? null : reader.GetString("apartment_number"),
                    Phone = reader.IsDBNull(10) ? null : reader.GetString("phone"),
                },
                Animal = new Animal
                {
                    Name = reader.IsDBNull(11) ? null : reader.GetString("name"),
                    Type = reader.IsDBNull(12) ? null : reader.GetString("type"),
                    Breed = reader.IsDBNull(13) ? null : reader.GetString("breed"),
                    Color = reader.IsDBNull(14) ? null : reader.GetString("color"),
                    Gender = reader.IsDBNull(15) ? null : reader.GetString("gender"),
                    DateOfBirth = reader.IsDBNull(16) ? default : reader.GetDateTime("animal_birth"),
                    ChipNumber = reader.IsDBNull(17) ? null : reader.GetString("chip_number"),
                },
            };
        }

        /// <summary>
        /// Convert - Reader To DebtSheetItem
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New DebtSheetItem.
        /// </returns>
        public IGenericDbEntity DebtSheetItemMapper(MySqlDataReader reader)
        {
            return new DebtSheetItem
            {
                OwnerId = reader.IsDBNull(0) ? 0 : reader.GetInt32("owner_id_num"),
                FirstName = reader.IsDBNull(1) ? null : reader.GetString("first_name"),
                LastName = reader.IsDBNull(2) ? null : reader.GetString("last_name"),
                Phone = reader.IsDBNull(3) ? null : reader.GetString("phone"),
                DebtAmountSum = reader.IsDBNull(5) ? 0 : reader.GetInt32("debt_amount_sum"),
                PaidAmountSum = reader.IsDBNull(6) ? 0 : reader.GetInt32("paid_sum"),
                TotalAmount = reader.IsDBNull(7) ? 0 : reader.GetInt32("total_amount"),
                DebtDate = reader.IsDBNull(8) ? default : reader.GetDateTime("debt_date"),
            };
        }

        /// <summary>
        /// Convert - Reader To VisitedOwnersItem
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New VisitedOwnersItem.
        /// </returns>
        public IGenericDbEntity VisitedOwnersItemMapper(MySqlDataReader reader)
        {
            return new VisitedOwnersItem
            {
                VisitId = reader.IsDBNull(0) ? 0 : reader.GetInt32("visit_id"),
                VisitTime = reader.IsDBNull(1) ? default : reader.GetDateTime("visit_time"),
                AnimalId = reader.IsDBNull(2) ? 0 : reader.GetInt32("animal_id"),
                Name = reader.IsDBNull(3) ? null : reader.GetString("name"),
                Type = reader.IsDBNull(4) ? null : reader.GetString("type"),
                Breed = reader.IsDBNull(5) ? null : reader.GetString("breed"),
                Color = reader.IsDBNull(6) ? null : reader.GetString("color"),
                Gender = reader.IsDBNull(7) ? null : reader.GetString("gender"),
                ChipNumber = reader.IsDBNull(9) ? null : reader.GetString("chip_number"),
                Sterilized = reader.IsDBNull(10) || reader.GetBoolean("sterilized"),
                Active = reader.IsDBNull(11) || reader.GetBoolean("active"),
                OwnerId = reader.IsDBNull(12) ? 0 : reader.GetInt32("owner_id"),
                FirstName = reader.IsDBNull(13) ? null : reader.GetString("first_name"),
                LastName = reader.IsDBNull(14) ? null : reader.GetString("last_name"),
                City = reader.IsDBNull(15) ? null : reader.GetString("city"),
                Street = reader.IsDBNull(16) ? null : reader.GetString("street"),
                HouseNumber = reader.IsDBNull(17) ? null : reader.GetString("house_number"),
                ApartmentNumber = reader.IsDBNull(18) ? null : reader.GetString("apartment_number"),
                Phone = reader.IsDBNull(19) ? null : reader.GetString("phone"),
                NumOfDaysPassed = reader.IsDBNull(20) ? 0 : reader.GetInt32("num_of_days"),
            };
        }

        /// <summary>
        /// Convert - Reader To SmsTemplate
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New SmsTemplate.
        /// </returns>
        public IGenericDbEntity SmsTemplateMapper(MySqlDataReader reader)
        {
            return new SmsTemplate
            {
                Id = reader.IsDBNull(0) ? 0 : reader.GetInt32("id_sms"),
                Title = reader.IsDBNull(1) ? null : reader.GetString("title_sms"),
                Template = reader.IsDBNull(2) ? null : reader.GetString("message_sms"),
                Type = reader.IsDBNull(3) ? null : reader.GetString("type_sms"),
            };
        }

        /// <summary>
        /// Convert - Reader To SmsPreventiveReminder
        /// </summary>
        /// <param name="reader"> MySql Data Reader</param>
        /// <returns>
        /// Return New SmsPreventiveReminder.
        /// </returns>
        public IGenericDbEntity SmsPreventiveReminderMapper(MySqlDataReader reader)
        {
            return new SmsPreventiveReminder
            {
                AnimalId = reader.IsDBNull(0) ? 0 : reader.GetInt32("animal_id"),
                Treatment = reader.IsDBNull(1) ? null : reader.GetString("next_treatment_name"),
                ReminderDate = reader.IsDBNull(2) ? null : reader.GetDateTime("date_reminder"),
                RemainingNumOfDays = reader.IsDBNull(3) ? 0 : reader.GetInt32("remaining_days"),
                Name = reader.IsDBNull(4) ? null : reader.GetString("name"),
                Type = reader.IsDBNull(5) ? null : reader.GetString("type"),
                Breed = reader.IsDBNull(6) ? null : reader.GetString("breed"),
                Color = reader.IsDBNull(7) ? null : reader.GetString("color"),
                Gender = reader.IsDBNull(8) ? null : reader.GetString("gender"),
                Active = reader.IsDBNull(9) || reader.GetBoolean("active"),
                Sterilized = reader.IsDBNull(10) || reader.GetBoolean("sterilized"),
                AnimalArchive = reader.IsDBNull(11) || reader.GetBoolean("animal_archive"),
                OwnerId = reader.IsDBNull(12) ? 0 : reader.GetInt32("owner_id"),
                FirstName = reader.IsDBNull(13) ? null : reader.GetString("first_name"),
                LastName = reader.IsDBNull(14) ? null : reader.GetString("last_name"),
                Phone = reader.IsDBNull(15) ? null : reader.GetString("phone"),
                Mobile = reader.IsDBNull(16) ? null : reader.GetString("mobile"),
                Email = reader.IsDBNull(17) ? null : reader.GetString("email"),
                OwnerArchive = reader.IsDBNull(18) || reader.GetBoolean("owner_archive"),
            };
        }
    }
}
