using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using Petadmin.Models;

namespace Petadmin.Services.Interfaces
{
    public interface IVisitService
    {
        Task<VisitPageLists> GetVisitPageLists();
        Task<VisitPage> GetVisitPageByIdAsync(int visitId);
        Task<IEnumerable<PreventiveTreatment>> GetPreventiveTreatmentsListByVisitIdAsync(int visitId);
        Task<int> AddVisitAsync(Visit visit);
        Task<int> AddPreventiveTreatmentAsync(PreventiveTreatment treatment);
        Task<int> AddPreventiveTreatmentListAsync(List<PreventiveTreatment> list);
        Task<bool> UpdateVisitAsync(Visit visit);
        Task<bool> DeleteVisitAsync(Visit visit);
        Task<bool> DeleteSelectedPreventiveTreatmentsAsync(List<PreventiveTreatment> list);
    }
}
