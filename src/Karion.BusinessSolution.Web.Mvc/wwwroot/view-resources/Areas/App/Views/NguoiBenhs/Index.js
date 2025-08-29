(function () {
    $(function () {

        var _$nguoiBenhsTable = $('#NguoiBenhsTable');
        var _nguoiBenhsService = abp.services.app.nguoiBenhs;
		var _entityTypeFullName = 'Karion.BusinessSolution.QuanLyDanhMuc.NguoiBenh';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.NguoiBenhs.Create'),
			edit: abp.auth.hasPermission('Pages.NguoiBenhs.Edit'),
			viewNguoiThan: abp.auth.hasPermission('Pages.NguoiThans'),
            'delete': abp.auth.hasPermission('Pages.NguoiBenhs.Delete')
        };
		var _updateImageProfileModal = new app.ModalManager({
			viewUrl: abp.appPath + 'App/NguoiBenhs/UpdateImageProfileModal',
			scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/NguoiBenhs/_UpdateImageProfileModal.js',
			modalClass: 'UpdateImageProfileModal'
		});
         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/NguoiBenhs/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/NguoiBenhs/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditNguoiBenhModal'
		 });
		//begin khai bao modal xem nguoi than
		var _viewNguoiThanModal = new app.ModalManager({
			viewUrl: abp.appPath + 'App/NguoiThans/ViewDanhSachNguoiThanModal',
			modalClass: 'ViewDanhSachNguoiThanModal',
			modalSize: 'modal-full modal-dialog-scrollable',
			scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/NguoiThans/_ViewListNguoiThanModal.js',
		});
		//endmodal
		 var _viewNguoiBenhModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/NguoiBenhs/ViewnguoiBenhModal',
            modalClass: 'ViewNguoiBenhModal'
        });

		        var _entityTypeHistoryModal = app.modals.EntityTypeHistoryModal.create();
		        function entityHistoryIsEnabled() {
            return abp.auth.hasPermission('Pages.Administration.AuditLogs') &&
                abp.custom.EntityHistory &&
                abp.custom.EntityHistory.IsEnabled &&
                _.filter(abp.custom.EntityHistory.EnabledEntities, entityType => entityType === _entityTypeFullName).length === 1;
        }

        var getDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT00:00:00Z"); 
        }

        var dataTable = _$nguoiBenhsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _nguoiBenhsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#NguoiBenhsTableFilter').val(),
					hoVaTenFilter: $('#HoVaTenFilterId').val(),
					tuoiFilter: $('#TuoiFilterId').val(),
					gioiTinhFilter: $('#GioiTinhFilterId').val(),
					diaChiFilter: $('#DiaChiFilterId').val(),
					userNameFilter: $('#UserNameFilterId').val(),
					minAccessFailedCountFilter: $('#MinAccessFailedCountFilterId').val(),
					maxAccessFailedCountFilter: $('#MaxAccessFailedCountFilterId').val(),
					phoneNumberFilter: $('#PhoneNumberFilterId').val(),
					emailAddressFilter: $('#EmailAddressFilterId').val(),
					emailConfirmationCodeFilter: $('#EmailConfirmationCodeFilterId').val(),
					isActiveFilter: $('#IsActiveFilterId').val(),
					isEmailConfirmedFilter: $('#IsEmailConfirmedFilterId').val(),
					isNhanVienFilter: $('#IsNhanVienFilterId').val(),
					isPhoneNumberConfirmedFilter: $('#IsPhoneNumberConfirmedFilterId').val(),
					passwordResetCodeFilter: $('#PasswordResetCodeFilterId').val(),
					profilePictureFilter: $('#ProfilePictureFilterId').val(),
					passwordFilter: $('#PasswordFilterId').val(),
					tokenFilter: $('#TokenFilterId').val(),
					minTokenExpireFilter:  getDateFilter($('#MinTokenExpireFilterId')),
					maxTokenExpireFilter:  getDateFilter($('#MaxTokenExpireFilterId'))
                    };
                }
            },
            columnDefs: [
                {
                    width: 120,
                    targets: 0,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-brand dropdown-toggle',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
						{
                                text: app.localize('View'),
                                action: function (data) {
                                    _viewNguoiBenhModal.open({ id: data.record.nguoiBenh.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.nguoiBenh.id });                                
                            }
                        },
							{
								text: "Cập nhật ảnh",
								visible: function () {
									return _permissions.edit;
								},
								action: function (data) {
									_updateImageProfileModal.open({ id: data.record.nguoiBenh.id });
								}
							},
							{
								text: app.localize('ViewNguoiThan'),
								visible: function () {
									return _permissions.viewNguoiThan;
								},
								action: function (data) {
									_viewNguoiThanModal.open({ id: data.record.nguoiBenh.id });
								}
							},
                        {
                            text: app.localize('History'),
                            visible: function () {
                                return entityHistoryIsEnabled();
                            },
                            action: function (data) {
                                _entityTypeHistoryModal.open({
                                    entityTypeFullName: _entityTypeFullName,
                                    entityId: data.record.nguoiBenh.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteNguoiBenh(data.record.nguoiBenh);
                            }
                        }]
                    }
                },

					{
						targets: 1,
						 data: "nguoiBenh.hoVaTen",
						 name: "hoVaTen"   
					},
					{
						targets: 2,
						 data: "nguoiBenh.ngaySinh",
						 name: "ngaySinh",
						render: function (notification, type, row, meta) {
							console.log(row);
							return row.nguoiBenh.ngaySinh+"/"+row.nguoiBenh.thangSinh+"/"+row.nguoiBenh.namSinh;
						}
					},
					{
						targets: 3,
						 data: "nguoiBenh.gioiTinh",
						 name: "gioiTinh"   
					},
					{
						targets: 4,
						 data: "nguoiBenh.diaChi",
						 name: "diaChi"   
					},
					{
						targets: 5,
						 data: "nguoiBenh.userName",
						 name: "userName"   
					},
					{
						targets: 6,
						 data: "nguoiBenh.accessFailedCount",
						 name: "accessFailedCount"   
					},
					{
						targets: 7,
						 data: "nguoiBenh.phoneNumber",
						 name: "phoneNumber"   
					},
					{
						targets: 8,
						 data: "nguoiBenh.emailAddress",
						 name: "emailAddress"   
					},
					{
						targets: 9,
						 data: "nguoiBenh.emailConfirmationCode",
						 name: "emailConfirmationCode"   
					},
					{
						targets: 10,
						 data: "nguoiBenh.isActive",
						 name: "isActive"  ,
						render: function (isActive) {
							if (isActive) {
								return '<div class="text-center"><i class="fa fa-check kt--font-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					},
					{
						targets: 11,
						 data: "nguoiBenh.isEmailConfirmed",
						 name: "isEmailConfirmed"  ,
						render: function (isEmailConfirmed) {
							if (isEmailConfirmed) {
								return '<div class="text-center"><i class="fa fa-check kt--font-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					},
					{
						targets: 12,
						 data: "nguoiBenh.isPhoneNumberConfirmed",
						 name: "isPhoneNumberConfirmed"  ,
						render: function (isPhoneNumberConfirmed) {
							if (isPhoneNumberConfirmed) {
								return '<div class="text-center"><i class="fa fa-check kt--font-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					},
					{
						targets: 13,
						 data: "nguoiBenh.passwordResetCode",
						 name: "passwordResetCode"   
					},
					{
						targets: 14,
						 data: "nguoiBenh.profilePicture",
						 name: "profilePicture"   
					},
					{
						targets: 15,
						 data: "nguoiBenh.password",
						 name: "password"   
					},
					{
						targets: 16,
						 data: "nguoiBenh.token",
						 name: "token"   
					},
					{
						targets: 17,
						 data: "nguoiBenh.tokenExpire",
						 name: "tokenExpire" ,
					render: function (tokenExpire) {
						if (tokenExpire) {
							return moment(tokenExpire).format('L');
						}
						return "";
					}
			  
					}
            ]
        });

        function getNguoiBenhs() {
            dataTable.ajax.reload();
        }

        function deleteNguoiBenh(nguoiBenh) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _nguoiBenhsService.delete({
                            id: nguoiBenh.id
                        }).done(function () {
                            getNguoiBenhs(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

		$('#ShowAdvancedFiltersSpan').click(function () {
            $('#ShowAdvancedFiltersSpan').hide();
            $('#HideAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideDown();
        });

        $('#HideAdvancedFiltersSpan').click(function () {
            $('#HideAdvancedFiltersSpan').hide();
            $('#ShowAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideUp();
        });

        $('#CreateNewNguoiBenhButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _nguoiBenhsService
                .getNguoiBenhsToExcel({
				filter : $('#NguoiBenhsTableFilter').val(),
					hoVaTenFilter: $('#HoVaTenFilterId').val(),
					tuoiFilter: $('#TuoiFilterId').val(),
					gioiTinhFilter: $('#GioiTinhFilterId').val(),
					diaChiFilter: $('#DiaChiFilterId').val(),
					userNameFilter: $('#UserNameFilterId').val(),
					minAccessFailedCountFilter: $('#MinAccessFailedCountFilterId').val(),
					maxAccessFailedCountFilter: $('#MaxAccessFailedCountFilterId').val(),
					phoneNumberFilter: $('#PhoneNumberFilterId').val(),
					emailAddressFilter: $('#EmailAddressFilterId').val(),
					emailConfirmationCodeFilter: $('#EmailConfirmationCodeFilterId').val(),
					isActiveFilter: $('#IsActiveFilterId').val(),
					isEmailConfirmedFilter: $('#IsEmailConfirmedFilterId').val(),
					isPhoneNumberConfirmedFilter: $('#IsPhoneNumberConfirmedFilterId').val(),
					passwordResetCodeFilter: $('#PasswordResetCodeFilterId').val(),
					profilePictureFilter: $('#ProfilePictureFilterId').val(),
					passwordFilter: $('#PasswordFilterId').val(),
					tokenFilter: $('#TokenFilterId').val(),
					minTokenExpireFilter:  getDateFilter($('#MinTokenExpireFilterId')),
					maxTokenExpireFilter:  getDateFilter($('#MaxTokenExpireFilterId'))
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditNguoiBenhModalSaved', function () {
            getNguoiBenhs();
        });

		$('#GetNguoiBenhsButton').click(function (e) {
            e.preventDefault();
            getNguoiBenhs();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getNguoiBenhs();
		  }
		});
    });
})();