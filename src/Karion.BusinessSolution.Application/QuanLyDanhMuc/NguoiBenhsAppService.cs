

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Karion.BusinessSolution.QuanLyDanhMuc.Dtos;
using Karion.BusinessSolution.Dto;
using Abp.Application.Services.Dto;
using Karion.BusinessSolution.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Abp.UI;
using Karion.BusinessSolution.Extension.Karion.BusinessSolution.Common;
using Microsoft.AspNetCore.Identity;

namespace Karion.BusinessSolution.QuanLyDanhMuc
{
	[AbpAuthorize(AppPermissions.Pages_NguoiBenhs)]
    public class NguoiBenhsAppService : BusinessSolutionAppServiceBase, INguoiBenhsAppService
    {
		 private readonly IRepository<NguoiBenh> _nguoiBenhRepository;

		  public NguoiBenhsAppService(IRepository<NguoiBenh> nguoiBenhRepository ) 
		  {
			_nguoiBenhRepository = nguoiBenhRepository;
		  }

		 public async Task<PagedResultDto<GetNguoiBenhForViewDto>> GetAll(GetAllNguoiBenhsInput input)
         {
			
			var filteredNguoiBenhs = _nguoiBenhRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.HoVaTen.Contains(input.Filter) || e.GioiTinh.Contains(input.Filter) || e.DiaChi.Contains(input.Filter) || e.UserName.Contains(input.Filter) || e.PhoneNumber.Contains(input.Filter) || e.EmailAddress.Contains(input.Filter) || e.EmailConfirmationCode.Contains(input.Filter) || e.PasswordResetCode.Contains(input.Filter) || e.ProfilePicture.Contains(input.Filter) || e.Password.Contains(input.Filter) || e.Token.Contains(input.Filter) || e.SoTheBHYT.Contains(input.Filter) || e.NoiDkBanDau.Contains(input.Filter) || e.MaDonViBHXH.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.HoVaTenFilter),  e => e.HoVaTen == input.HoVaTenFilter)
						.WhereIf(input.MinNgaySinhFilter != null, e => e.NgaySinh >= input.MinNgaySinhFilter)
						.WhereIf(input.MaxNgaySinhFilter != null, e => e.NgaySinh <= input.MaxNgaySinhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.GioiTinhFilter),  e => e.GioiTinh == input.GioiTinhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiFilter),  e => e.DiaChi == input.DiaChiFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter),  e => e.UserName == input.UserNameFilter)
						.WhereIf(input.MinAccessFailedCountFilter != null, e => e.AccessFailedCount >= input.MinAccessFailedCountFilter)
						.WhereIf(input.MaxAccessFailedCountFilter != null, e => e.AccessFailedCount <= input.MaxAccessFailedCountFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNumberFilter),  e => e.PhoneNumber == input.PhoneNumberFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailAddressFilter),  e => e.EmailAddress == input.EmailAddressFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailConfirmationCodeFilter),  e => e.EmailConfirmationCode == input.EmailConfirmationCodeFilter)
						.WhereIf(input.IsActiveFilter > -1,  e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive) )
						.WhereIf(input.IsEmailConfirmedFilter > -1,  e => (input.IsEmailConfirmedFilter == 1 && e.IsEmailConfirmed) || (input.IsEmailConfirmedFilter == 0 && !e.IsEmailConfirmed) )
						.WhereIf(input.IsNhanVienFilter > -1,  e => (input.IsNhanVienFilter == 1 && e.IsNhanVien) || (input.IsNhanVienFilter == 0 && !e.IsNhanVien) )
						.WhereIf(input.IsPhoneNumberConfirmedFilter > -1,  e => (input.IsPhoneNumberConfirmedFilter == 1 && e.IsPhoneNumberConfirmed) || (input.IsPhoneNumberConfirmedFilter == 0 && !e.IsPhoneNumberConfirmed) )
						.WhereIf(!string.IsNullOrWhiteSpace(input.PasswordResetCodeFilter),  e => e.PasswordResetCode == input.PasswordResetCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProfilePictureFilter),  e => e.ProfilePicture == input.ProfilePictureFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PasswordFilter),  e => e.Password == input.PasswordFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TokenFilter),  e => e.Token == input.TokenFilter)
						.WhereIf(input.MinTokenExpireFilter != null, e => e.TokenExpire >= input.MinTokenExpireFilter)
						.WhereIf(input.MaxTokenExpireFilter != null, e => e.TokenExpire <= input.MaxTokenExpireFilter)
						.WhereIf(input.MinThangSinhFilter != null, e => e.ThangSinh >= input.MinThangSinhFilter)
						.WhereIf(input.MaxThangSinhFilter != null, e => e.ThangSinh <= input.MaxThangSinhFilter)
						.WhereIf(input.MinNamSinhFilter != null, e => e.NamSinh >= input.MinNamSinhFilter)
						.WhereIf(input.MaxNamSinhFilter != null, e => e.NamSinh <= input.MaxNamSinhFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SoTheBHYTFilter),  e => e.SoTheBHYT == input.SoTheBHYTFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NoiDkBanDauFilter),  e => e.NoiDkBanDau == input.NoiDkBanDauFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaDonViBHXHFilter),  e => e.MaDonViBHXH == input.MaDonViBHXHFilter)
						.WhereIf(input.MinGiaTriSuDungTuNgayFilter != null, e => e.GiaTriSuDungTuNgay >= input.MinGiaTriSuDungTuNgayFilter)
						.WhereIf(input.MaxGiaTriSuDungTuNgayFilter != null, e => e.GiaTriSuDungTuNgay <= input.MaxGiaTriSuDungTuNgayFilter)
						.WhereIf(input.MinThoiDiemDuNamFilter != null, e => e.ThoiDiemDuNam >= input.MinThoiDiemDuNamFilter)
						.WhereIf(input.MaxThoiDiemDuNamFilter != null, e => e.ThoiDiemDuNam <= input.MaxThoiDiemDuNamFilter);

			var pagedAndFilteredNguoiBenhs = filteredNguoiBenhs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var nguoiBenhs = from o in pagedAndFilteredNguoiBenhs
                         select new GetNguoiBenhForViewDto() {
							NguoiBenh = new NguoiBenhDto
							{
                                HoVaTen = o.HoVaTen,
                                NgaySinh = o.NgaySinh,
                                GioiTinh = o.GioiTinh,
                                DiaChi = o.DiaChi,
                                UserName = o.UserName,
                                AccessFailedCount = o.AccessFailedCount,
                                PhoneNumber = o.PhoneNumber,
                                EmailAddress = o.EmailAddress,
                                EmailConfirmationCode = o.EmailConfirmationCode,
                                IsActive = o.IsActive,
                                IsEmailConfirmed = o.IsEmailConfirmed,
                                IsPhoneNumberConfirmed = o.IsPhoneNumberConfirmed,
                                PasswordResetCode = o.PasswordResetCode,
                                ProfilePicture = o.ProfilePicture,
                                Password = o.Password,
                                Token = o.Token,
                                TokenExpire = o.TokenExpire,
                                ThangSinh = o.ThangSinh,
                                NamSinh = o.NamSinh,
                                SoTheBHYT = o.SoTheBHYT,
                                NoiDkBanDau = o.NoiDkBanDau,
                                MaDonViBHXH = o.MaDonViBHXH,
                                GiaTriSuDungTuNgay = o.GiaTriSuDungTuNgay,
                                ThoiDiemDuNam = o.ThoiDiemDuNam,
                                Id = o.Id
							}
						};

            var totalCount = await filteredNguoiBenhs.CountAsync();

            return new PagedResultDto<GetNguoiBenhForViewDto>(
                totalCount,
                await nguoiBenhs.ToListAsync()
            );
         }
		 public async Task<GetNguoiBenhForViewDto> GetNguoiBenhForView(int id)
		 {
			 var nguoiBenh = await _nguoiBenhRepository.GetAsync(id);

			 var output = new GetNguoiBenhForViewDto { NguoiBenh = ObjectMapper.Map<NguoiBenhDto>(nguoiBenh) };
			
			 return output;
		 }
		 [AbpAuthorize(AppPermissions.Pages_NguoiBenhs_Edit)]
		 public async Task<GetNguoiBenhForEditOutput> GetNguoiBenhForEdit(EntityDto input)
         {
            var nguoiBenh = await _nguoiBenhRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetNguoiBenhForEditOutput {NguoiBenh = ObjectMapper.Map<CreateOrEditNguoiBenhDto>(nguoiBenh)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditNguoiBenhDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_NguoiBenhs_Create)]
		 protected virtual async Task Create(CreateOrEditNguoiBenhDto input)
         {
            var nguoiBenh = ObjectMapper.Map<NguoiBenh>(input);
			nguoiBenh.IsNhanVien = true;
			nguoiBenh.Password = input.Password.ToMd5().ToLower();			
			var confirmationCode = (nguoiBenh.UserName.clearSpace().ToLower() + nguoiBenh.Password.ToMd5()).ToMd5();
			nguoiBenh.EmailConfirmationCode = confirmationCode;
            await _nguoiBenhRepository.InsertAsync(nguoiBenh);
         }

		 [AbpAuthorize(AppPermissions.Pages_NguoiBenhs_Edit)]
		 protected virtual async Task Update(CreateOrEditNguoiBenhDto input)
         {
            var nguoiBenh = await _nguoiBenhRepository.FirstOrDefaultAsync((int)input.Id);
            if (!input.Password.IsNullOrWhiteSpace())
            {
	            nguoiBenh.Password = input.Password.ToMd5().ToLower();
            }
             ObjectMapper.Map(input, nguoiBenh);
         }

		 [AbpAuthorize(AppPermissions.Pages_NguoiBenhs_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _nguoiBenhRepository.DeleteAsync(input.Id);
         }

         [AbpAuthorize(AppPermissions.Pages_NguoiBenhs_Edit)]
         public async Task UpdateImageProfile(UpdateImageProfileInput input)
         {
	         var nguoiBenh = await _nguoiBenhRepository.GetAsync(input.NguoiBenhId);

	         // Kiểm tra định dạng file hợp lệ
	         var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
	         var extension = Path.GetExtension(input.JpegFileName)?.ToLowerInvariant();

	         if (string.IsNullOrWhiteSpace(extension) || !allowedExtensions.Contains(extension))
	         {
		         throw new UserFriendlyException("Chỉ cho phép upload ảnh định dạng .jpg, .jpeg, .png");
	         }

	         using (var stream = new MemoryStream(input.Data))
	         using (var image = System.Drawing.Image.FromStream(stream))
	         {
		         // Tạo đường dẫn và tên file an toàn
		         var fileName = Path.GetFileNameWithoutExtension(input.JpegFileName);
		         fileName = fileName + extension;

		         var folderPath = Path.Combine("wwwroot", "Common", "Images", "UserProfilePicture");
		         Directory.CreateDirectory(folderPath); // đảm bảo thư mục tồn tại
		         var savePath = Path.Combine(folderPath, fileName);

		         // Lưu ảnh theo đúng định dạng
		         if (extension == ".jpg" || extension == ".jpeg")
			         image.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
		         else if (extension == ".png")
			         image.Save(savePath, System.Drawing.Imaging.ImageFormat.Png);

		         // Cập nhật đường dẫn ảnh
		         nguoiBenh.ProfilePicture = $"https://ttkc.techber.vn/Common/Images/UserProfilePicture/{fileName}";
		         await _nguoiBenhRepository.UpdateAsync(nguoiBenh);
	         }
         }

	}
}