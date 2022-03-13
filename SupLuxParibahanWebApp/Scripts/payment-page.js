//global variables
const totalTicketFare = document.getElementById('ticket-total-fare');
const paymentInfo = document.getElementById('payment-info');

let bookOrPurchase = '';

//showing payment options
const openPaymentOptions = thisElement => {

    document.getElementById('payment-options-container').classList.toggle('d-none');

    bookOrPurchase = thisElement.innerText;
}

//opening payment modal
const openPaymentModal = method => {

    if (method === 'bkash') {

        if (bookOrPurchase == 'Book') {

            document.getElementById('payment-modal-title').innerText = 'Bkash Booking Payment';
            bookPaymentInfo();
        }

        else {

            document.getElementById('payment-modal-title').innerText = 'Bkash Purchase Payment';
            purchasePaymentInfo();
        }

    }

    else if (method === 'rocket') {

        if (bookOrPurchase == 'Book') {

            document.getElementById('payment-modal-title').innerText = 'Rocket Booking Payment';
            bookPaymentInfo();
        }

        else {

            document.getElementById('payment-modal-title').innerText = 'Rocket Purchase Payment';
            purchasePaymentInfo();
        }

    }

    else {

        if (bookOrPurchase == 'Book') {

            document.getElementById('payment-modal-title').innerText = 'Nagad Booking Payment';
            bookPaymentInfo();
        }

        else {

            document.getElementById('payment-modal-title').innerText = 'Nagad Purchase Payment';
            purchasePaymentInfo();
        }
       
    }

    $('#paymentModal').modal('show');
}

//calculating booking payment details
const bookPaymentInfo = () => {

    paymentInfo.innerHTML = `
        <p class="ticket-info">Total Fare:</p>
        <p class="ticket-data">${totalTicketFare.innerText}</p>
        <p class="ticket-info">Payable Fare:</p>
        <p class="ticket-data">BDT ${parseInt(totalTicketFare.innerText.slice(4)) * 0.2}</p>
        <p class="ticket-info">Due Fare:</p>
        <p class="ticket-data">BDT ${parseInt(totalTicketFare.innerText.slice(4)) - (parseInt(totalTicketFare.innerText.slice(4)) * 0.2)}</p>
    `;
}

//calculating purchase payment details
const purchasePaymentInfo = () => {

    paymentInfo.innerHTML = `
        <p class="ticket-info">Total Fare:</p>
        <p class="ticket-data">${totalTicketFare.innerText}</p>
        <p class="ticket-info">Discount Fare:</p>
        <p class="ticket-data">BDT ${parseInt(totalTicketFare.innerText.slice(4)) * 0.05}</p>
        <p class="ticket-info">Payable Fare:</p>
        <p class="ticket-data">BDT ${parseInt(totalTicketFare.innerText.slice(4)) - (parseInt(totalTicketFare.innerText.slice(4)) * 0.2)}</p>
    `;
}

const passDataToConfirm = () => {

    const ticketInfo = {
        tripDate: document.getElementById('ticket-date').innerText,
        seats: document.getElementById('ticket-seats').innerText,
        coachNo: document.getElementById('ticket-coach').innerText
    }

    fetch('ConfirmPayment', {
        method: 'POST',
        body: JSON.stringify(ticketInfo),
        headers: {
            'Content-type': 'application/json; charset=UTF-8',
        },
    }).then(res => res.json()).then(data => console.log(data));
}