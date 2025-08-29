using System;

namespace Karion.BusinessSolution.QuanLyDanhMuc.Dtos
{
    public class AttendanceDailyStatusDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? MorningCheckIn { get; set; }
        public bool MorningIsLate { get; set; }
        public DateTime? AfternoonCheckIn { get; set; }
        public bool AfternoonIsLate { get; set; }
    }
}