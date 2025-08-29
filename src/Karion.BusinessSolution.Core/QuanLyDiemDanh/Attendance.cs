using Karion.BusinessSolution.QuanLyDanhMuc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Karion.BusinessSolution.QuanLyDiemDanh
{
	[Table("Attendances")]
    public class Attendance : AuditedEntity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual double CheckInLatitude { get; set; }
		
		public virtual double CheckInLongitude { get; set; }
		
		public virtual bool IsCheckInFaceMatched { get; set; }
		
		public virtual bool IsWithinLocation { get; set; }
		
		public virtual string CheckInDeviceInfo { get; set; }
		
		public virtual string PhotoPath { get; set; }
		
		public virtual double CheckInFaceMatchPercentage { get; set; }
		
		public virtual DateTime CheckIn { get; set; }
		
		public virtual bool IsLateCheckIn { get; set; }
		
		public virtual bool IsOvertime { get; set; }
		
		public virtual DateTime? OvertimeStart { get; set; }
		
		public virtual DateTime? OvertimeEnd { get; set; }
		

		public virtual int? NguoiBenhId { get; set; }
		
        [ForeignKey("NguoiBenhId")]
		public NguoiBenh NguoiBenhFk { get; set; }
		
    }
}