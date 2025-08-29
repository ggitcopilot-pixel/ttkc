﻿(function () {
    $(function () {
        var _thongKeBaoCaosService = abp.services.app.thongKeBaoCaosAppservices;
        function renderTable(data) {
            var tbody = $('#attendanceTable tbody');
            tbody.empty();
            if (!data || data.length === 0) {
                tbody.append('<tr><td colspan="5" class="text-center">Không có dữ liệu</td></tr>');
                return;
            }
            data.forEach(function (item) {
                var row =
                    '<tr>' +
                    '<td>' + (item.userName || '') + '</td>' +
                    '<td>' + (item.morningCheckIn ? moment(item.morningCheckIn).format('HH:mm:ss') : '✗') + '</td>' +
                    '<td class="text-center">' + (item.morningCheckIn ? (item.morningIsLate ? '<span class="badge badge-danger">Trễ</span>' : '<span class="badge badge-success">Đúng giờ</span>') : '-') + '</td>' +
                    '<td>' + (item.afternoonCheckIn ? moment(item.afternoonCheckIn).format('HH:mm:ss') : '✗') + '</td>' +
                    '<td class="text-center">' + (item.afternoonCheckIn ? (item.afternoonIsLate ? '<span class="badge badge-danger">Trễ</span>' : '<span class="badge badge-success">Đúng giờ</span>') : '-') + '</td>' +
                    '</tr>';
                tbody.append(row);
            });
        }

        $('#btnLoadAttendance').click(function () {
            var selectedDate = $('#attendanceDate').val();
            if (!selectedDate) {
                abp.message.warn('Vui lòng chọn ngày!');
                return;
            }
            abp.ui.setBusy();
            _thongKeBaoCaosService.getDailyAttendanceStatus(selectedDate)
                .done(function (result) {
                    renderTable(result);
                })
                .always(function () {
                    abp.ui.clearBusy();
                });
        });

        $('#attendanceDate').val(moment().format('YYYY-MM-DD'));
        $('#btnLoadAttendance').click();
    });
})();