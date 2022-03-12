const months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
const days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

const datePicker = document.getElementById('suplux-datepicker');
const showDay = document.getElementById('suplux-day');

let fullDate = '';

/*
 *  picking the journey date
 */
$("#suplux-datepicker").datepicker({
    minDate: 0,
    maxDate: '16d',
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

    fullDate = datePicker.value;
    const day = new Date(fullDate).getDay();

    const month = new Date(fullDate).getMonth() + 1;
    const year = new Date(fullDate).getFullYear();

    const date = fullDate.split(/\//);
    datePicker.value = date[1] + ' ' + months[--date[0]] + "'" + date[2].slice(2, 4);

    showDay.innerText = days[day];

    fullDate = year + '-' + (month < 10 ? '0' : '') + month + '-' + (date[1] < 10 ? '0' : '') + date[1];
}

/*
 *  setting current date when the home page loads
 */
window.onload = function () {

    var d = new Date();

    var month = d.getMonth() + 1;

    fullDate = d.getFullYear() + '-' + (month < 10 ? '0' : '') + month + '-' + (d.getDate() < 10 ? '0' : '') + d.getDate();
    const day = new Date(fullDate).getDay();

    const date = fullDate.split('-');
    datePicker.value = date[2] + ' ' + months[--date[1]] + "'" + date[0].slice(2, 4);

    showDay.innerText = days[day];
}

//getting JSON trip data to back end
document.getElementById('bus-search-btn').addEventListener('click', () => {

    const searchData = {
        from: document.getElementById('starting-point').value,
        to: document.getElementById('ending-point').value,
        date: fullDate
    };

    fetch('Bus/GetJourneyData', {
        method: 'POST',
        body: JSON.stringify(searchData),
        headers: {
            'Content-type': 'application/json; charset=UTF-8',
        },
    }).then(res => res.json()).then(data => console.log(data));
});