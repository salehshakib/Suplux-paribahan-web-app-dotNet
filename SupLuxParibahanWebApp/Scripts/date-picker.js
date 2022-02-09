const months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
const days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

const datePicker = document.getElementById('suplux-datepicker');
const showDay = document.getElementById('suplux-day');

/*
 *  picking the journey date
 */
$("#suplux-datepicker").datepicker({
    minDate: 0,
    maxDate: new Date(new Date().setMonth(new Date().getMonth() + 2)),
    monthNames: ["January,", "February,", "March,", "April,", "May,", "June,", "July,", "August,", "September,", "October,", "November,", "December,"],
    showOtherMonths: true,
    changeMonth: false,
    changeYear: false,
    gotoCurrent: true,
    showAnim: "slideDown",
    dateFormat: 'mm/dd/yy'
});

$(".date").mousedown(function () {

    $(".ui-datepicker").addClass("active");
});

/*
 *  adding title to the calender
 */
setInterval(function () {
    if ($('#ui-datepicker-div').find('h5').length != 1) {
        $("#ui-datepicker-div").prepend('<h5 class="suplux-datepicker-title">Select journey date</h5>');
    }
}, 1);

/*
 *  formatting the date and the day in the journey date picker 
 */

datePicker.onchange = function () {

    const fullDate = datePicker.value;
    const day = new Date(fullDate).getDay();

    const date = fullDate.split(/\//);
    datePicker.value = date[1] + ' ' + months[--date[0]] + "'" + date[2].slice(2, 4);

    showDay.innerText = days[day];
}

/*
 *  setting current date when the home page loads
 */
window.onload = function () {

    var d = new Date();

    var month = d.getMonth() + 1;

    var fullDate = (month < 10 ? '0' : '') + month + '/' + (d.getDate() < 10 ? '0' : '') + d.getDate() + '/' + d.getFullYear();
    const day = new Date(fullDate).getDay();

    const date = fullDate.split(/\//);
    datePicker.value = date[1] + ' ' + months[--date[0]] + "'" + date[2].slice(2, 4);

    showDay.innerText = days[day];
}