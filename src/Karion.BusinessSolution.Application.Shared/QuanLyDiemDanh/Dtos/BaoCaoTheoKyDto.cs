using System;
using System.Collections.Generic;

namespace Karion.BusinessSolution.QuanLyDiemDanh.Dtos
{
    public class BaoCaoTongHopResultDto
    {
        public List<BaoCaoTongHopNhanVienDto> NhanVienReports { get; set; }
        public BaoCaoTongHopNhanVienDto TongHop { get; set; }
    }
    public class BaoCaoTongHopNhanVienDto
    {
        public long? NhanVienId { get; set; }
        public string TenNhanVien { get; set; }
        public List<ChamCongKyDto> ChiTietKy { get; set; }
        public int TongNgayLam { get; set; }
        public int TongNgayNghi { get; set; }
        public int SoNgayDiTre { get; set; }
        public int SoNgayVeSom { get; set; }
    }
    public class ChamCongKyDto
    {
        public string Ky { get; set; } // ngày/tháng/năm
        public string TrangThai { get; set; }
        public int DiTrePhut { get; set; }
        public int VeSomPhut { get; set; }
    }
}