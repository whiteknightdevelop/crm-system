using System;
using Petadmin.Repository.Interfaces;
using Petadmin.Repository.Repositories;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.DbContext
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAnimalRepository Animals { get; }
        public IDiagnosisRepository Diagnosis { get; }
        public IDrugRepository Drugs { get; }
        public IOwnerRepository Owners { get; }
        public ITreatmentRepository Treatments { get; }
        public IVisitRepository Visits { get; }
        public IPrescriptionRepository Prescriptions { get;}
        public IPreventiveReminderRepository PreventiveReminders { get; }
        public IPreventiveTreatmentRepository PreventiveTreatments { get; }
        public IFollowUpRepository FollowUps { get; }
        public IDebtRepository Debts { get; }
        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public IUserRoleRepository UserRoles { get; }
        public IReportRepository Reports { get; }
        public ISmsRepository Sms { get; }

        private readonly IPetadminDbContext _context;

        public UnitOfWork(IPetadminDbContext context)
        {
            _context = context;
            Animals = new AnimalRepository(_context);
            Diagnosis = new DiagnosisRepository(_context);
            Drugs = new DrugRepository(_context);
            Owners = new OwnerRepository(_context);
            Treatments = new TreatmentRepository(_context);
            Visits = new VisitRepository(_context);
            Prescriptions = new PrescriptionRepository(_context);
            PreventiveReminders = new PreventiveReminderRepository(_context);
            PreventiveTreatments = new PreventiveTreatmentRepository(_context);
            FollowUps = new FollowUpRepository(_context);
            Debts = new DebtRepository(_context);
            Users = new UserRepository(_context);
            Roles = new RoleRepository(_context);
            UserRoles = new UserRoleRepository(_context);
            Reports = new ReportRepository(_context);
            Sms = new SmsRepository(_context);
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
