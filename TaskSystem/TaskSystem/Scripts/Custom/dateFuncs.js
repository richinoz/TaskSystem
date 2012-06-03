$(function () {
    DatePicker("#DueDate");
});


DatePicker = function (elementclass) {
    $(elementclass).datepicker({ showOn: 'both', buttonImage: "../../Content/calendar.gif", dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, minDate: new Date(2012, 1, 1) });
};
