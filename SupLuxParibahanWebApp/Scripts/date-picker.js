$("#suplux-datepicker").datepicker({
    minDate: 0,
    maxDate: new Date(new Date().setMonth(new Date().getMonth() + 2)),
    showOtherMonths: true,
    changeMonth: true,
    changeYear: true,
    showAnim: "slideDown",
    dateFormat: 'dd-M-yy'
});

$(".date").mousedown(function () {
    $(".ui-datepicker").addClass("active");
});