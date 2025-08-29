(function ($) {
    app.modals.CreateOrEditNguoiBenhModal = function () {

        var _nguoiBenhsService = abp.services.app.nguoiBenhs;

        var _modalManager;
        var _$nguoiBenhInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$nguoiBenhInformationForm = _modalManager.getModal().find('form[name=NguoiBenhInformationsForm]');
            _$nguoiBenhInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$nguoiBenhInformationForm.valid()) {
                return;
            }

            var nguoiBenh = _$nguoiBenhInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _nguoiBenhsService.createOrEdit(
				nguoiBenh
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditNguoiBenhModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);