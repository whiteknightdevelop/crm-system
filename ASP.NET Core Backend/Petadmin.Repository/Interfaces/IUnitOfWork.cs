using System;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAnimalRepository Animals { get; }
        IDiagnosisRepository Diagnosis { get; }
        IDrugRepository Drugs { get; }
        IOwnerRepository Owners { get; }
        ITreatmentRepository Treatments { get; }
        IVisitRepository Visits { get; }
        IPrescriptionRepository Prescriptions { get; }   
        IPreventiveReminderRepository PreventiveReminders { get; }
        IPreventiveTreatmentRepository PreventiveTreatments { get; }  
        IFollowUpRepository FollowUps { get; }
        IDebtRepository Debts { get; }
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IUserRoleRepository UserRoles { get; }
        IReportRepository Reports { get; }
        ISmsRepository Sms { get; }
    }
}