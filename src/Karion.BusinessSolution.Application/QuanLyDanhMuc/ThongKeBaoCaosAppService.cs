using Abp.Domain.Repositories;
using Karion.BusinessSolution.QuanLyDiemDanh;

namespace Karion.BusinessSolution.QuanLyDanhMuc
{
    public class ThongKeBaoCaosAppService : BusinessSolutionAppServiceBase, IThongKeBaoCaosAppService
    {
        private readonly IRepository<Attendance> _diemDanhRepository;
        
        public ThongKeBaoCaosAppService(
            IRepository<Attendance> diemDanhRepository
        
        ) 
        {
            _diemDanhRepository = diemDanhRepository;
        }
    }
}