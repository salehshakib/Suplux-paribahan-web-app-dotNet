//global variables
const businessClassMultiSeats = [
    'A-1', '', 'A-2', 'A-3',
    'B-1', '', 'B-2', 'B-3',
    'C-1', '', 'C-2', 'C-3',
    'D-1', '', 'D-2', 'D-3',
    'E-1', '', 'E-2', 'E-3',
    'F-1', '', 'F-2', 'F-3',
    'G-1', '', 'G-2', 'G-3',
    'H-1', '', 'H-2', 'H-3',
    'I-1', '', 'I-2', 'I-3',
    'J-1', 'J-2', 'J-3', 'J-4'
];

const businessClassBiSeats = [
    'A-1', '', 'A-2', 'A-3',
    'B-1', '', 'B-2', 'B-3',
    'C-1', '', 'C-2', 'C-3',
    'D-1', '', 'D-2', 'D-3',
    'E-1', '', 'E-2', 'E-3',
    'F-1', '', 'F-2', 'F-3',
    'G-1', '', 'G-2', 'G-3',
    'H-1', '', 'H-2', 'H-3',
    'I-1', 'I-2', 'I-3', 'I-4'
];

const economyClassNonAcSeats = [
    'A-1', 'A-2', '', 'A-3', 'A-4',
    'B-1', 'B-2', '', 'B-3', 'B-4',
    'C-1', 'C-2', '', 'C-3', 'C-4',
    'D-1', 'D-2', '', 'D-3', 'D-4',
    'E-1', 'E-2', '', 'E-3', 'E-4',
    'F-1', 'F-2', '', 'F-3', 'F-4',
    'G-1', 'G-2', '', 'G-3', 'G-4',
    'H-1', 'H-2', '', 'H-3', 'H-4',
    'I-1', 'I-2', '', 'I-3', 'I-4',
    'J-1', 'J-2', 'J-3', 'J-4', 'J-5',
];

const coachResultRowContainers = document.getElementsByClassName('coach-result-row-container');
const coachResultRows = document.getElementsByClassName('coach-result-row');
const tripDetailsContainers = document.getElementsByClassName('trip-details-container');
const businessClassSeatContainers = document.getElementsByClassName('business-class-seat-container');
const economyClassSeatContainers = document.getElementsByClassName('economy-class-seat-container');
const fareDatas = document.getElementsByClassName('fare-data');

//selected table 
const seatTableBody = document.getElementsByClassName('selected-seats-table-body');
const seatTableTotalFare = document.getElementsByClassName('total-fare');

let selectedSeatsCount = 0;
let isFourSeatsSelected = false;

//toggling the search section
document.getElementById('cross-btn').addEventListener('click', () => {

    document.getElementById('cross-btn').classList.add('d-none');
    document.getElementById('modify-search-container').classList.add('d-none');
    document.getElementById('mobile-edit-hide-container').classList.remove('d-none');
});

document.getElementById('mobile-edit-btn').addEventListener('click', () => {

    document.getElementById('cross-btn').classList.remove('d-none');
    document.getElementById('modify-search-container').classList.remove('d-none');
    document.getElementById('mobile-edit-hide-container').classList.add('d-none');
});

//rendering seat map
const renderSeats = (coachType, thisElement) => {

    let index = 0;
    selectedSeatsCount = 0;

    for (let i = 0; i < coachResultRowContainers.length; i++) {

        if (coachResultRowContainers[i].contains(thisElement)) {

            index = i;
        }
    }

    for (let i = 0; i < tripDetailsContainers.length; i++) {

        if (i !== index) {

            tripDetailsContainers[i].classList.add('d-none');
            coachResultRows[i].classList.remove('show-seat-map');
        }
    }

    if (tripDetailsContainers[index].classList.contains('d-none')) {

        tripDetailsContainers[index].classList.remove('d-none');
        tripDetailsContainers[index].classList.add('active');
        coachResultRows[index].classList.add('show-seat-map');
    }

    else {

        tripDetailsContainers[index].classList.add('d-none');
        coachResultRows[index].classList.remove('show-seat-map');
    }


    if (coachType === 'multi') {

        for (const container of businessClassSeatContainers) {

            container.textContent = '';

            for (const businessSeat of businessClassMultiSeats) {

                const div = document.createElement('div');

                div.classList.add('seat');

                div.setAttribute('onclick', `selectSeat('${businessSeat}', 'Business', this, ${index})`);

                if (businessSeat === '') {

                    div.innerHTML = '';
                }

                else {
                    div.innerHTML = `
                        <svg viewBox="0 0 100 100">
                            <use xlink:href="#seat-svg"></use>
                        </svg>
                        <p class="seat-no">${businessSeat}</p>
                    `;
                }

                container.appendChild(div);
            }
        }
    }

    else if (coachType === 'bi') {

        for (const container of businessClassSeatContainers) {

            container.textContent = '';

            for (const businessSeat of businessClassBiSeats) {

                const div = document.createElement('div');

                div.classList.add('seat');

                div.setAttribute('onclick', `selectSeat('${businessSeat}', 'Business', this, ${index})`);

                if (businessSeat === '') {

                    div.innerHTML = '';
                }

                else {
                    div.innerHTML = `
                        <svg viewBox="0 0 100 100">
                            <use xlink:href="#seat-svg"></use>
                        </svg>
                        <p class="seat-no">${businessSeat}</p>
                    `;
                }

                container.appendChild(div);
            }
        }
    }

    else if (coachType === 'non-ac') {

        for (const container of economyClassSeatContainers) {

            container.textContent = '';

            for (const economySeat of economyClassNonAcSeats) {

                const div = document.createElement('div');

                div.classList.add('seat');

                div.setAttribute('onclick', `selectSeat('${economySeat}', 'Economy', this, ${index})`);

                if (economySeat === '') {

                    div.innerHTML = '';
                }

                else {
                    div.innerHTML = `
                        <svg viewBox="0 0 100 100">
                            <use xlink:href="#seat-svg"></use>
                        </svg>
                        <p class="seat-no">${economySeat}</p>
                    `;
                }

                container.appendChild(div);
            }
        }
    }
}

//selecting seats and showing in the table
const selectSeat = (seatNo, seatType, thisElement, index) => {

    console.log(fareDatas[index].innerText.slice(4));

    if (selectedSeatsCount < 4) {

        thisElement.classList.toggle('selected');

        if (thisElement.classList.contains('selected')) {

            const tr = document.createElement('tr');

            tr.innerHTML = `
                <td class="seat-table-data">${seatNo}</td>
                <td class="seat-table-data">${seatType}</td>
                <td class="seat-table-data">${fareDatas[index].innerText}</td>
            `;

            seatTableBody[index].appendChild(tr);

            selectedSeatsCount++;
            seatTableTotalFare[index].innerHTML = `BDT ${parseInt(seatTableTotalFare[index].innerText.slice(4)) + parseInt(fareDatas[index].innerText.slice(4))}`;

        }

        else {
            const seatTableDatas = document.getElementsByClassName('seat-table-data');

            for (const data of seatTableDatas) {

                if (data.innerText === seatNo) {

                    seatTableBody[index].removeChild(data.parentNode);
                }
            }

            selectedSeatsCount--;

            seatTableTotalFare[index].innerHTML = `BDT ${parseInt(seatTableTotalFare[index].innerText.slice(4)) - parseInt(fareDatas[index].innerText.slice(4))}`;

            isFourSeatsSelected = false;
        }
    }

    else if (thisElement.classList.contains('selected')) {

        thisElement.classList.toggle('selected');

        const seatTableDatas = document.getElementsByClassName('seat-table-data');

        for (const data of seatTableDatas) {

            if (data.innerText === seatNo) {

                seatTableBody[index].removeChild(data.parentNode);
            }
        }

        selectedSeatsCount--;
        seatTableTotalFare[index].innerHTML = `BDT ${parseInt(seatTableTotalFare[index].innerText.slice(4)) - parseInt(fareDatas[index].innerText.slice(4))}`;

        isFourSeatsSelected = false;
    }

    //showing modal if user tries to select more than 4 seats
    if (isFourSeatsSelected) {

        $('#seatMaxedOutModal').modal('show');
    }

    if (selectedSeatsCount == 4) {

        isFourSeatsSelected = true;
    }

    console.log(selectedSeatsCount);
}