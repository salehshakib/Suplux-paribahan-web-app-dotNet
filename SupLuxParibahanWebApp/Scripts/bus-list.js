/*
 * functions
 */
function addClass(elementId, className) {

    const element = document.getElementById(elementId);
    element.classList.add(className);
}

function removeClass(elementId, className) {

    const element = document.getElementById(elementId);
    element.classList.remove(className);
}

function sleep(milliseconds) {
    return new Promise(resolve => setTimeout(resolve, milliseconds));
}

/*
 * picking data from coach type dropdown
 */
document.getElementById('coach-type-input').addEventListener('focus', function () {

    addClass('down-arrow', 'arrow-rotate');
    addClass('coach-type-menu', 'visible');
    removeClass('coach-type-menu', 'hidden');
});

document.getElementById('coach-type-input').addEventListener('blur', function () {


    document.querySelectorAll('.coach-menu-item').forEach(item => {
        item.addEventListener('click', event => {

            document.getElementById('coach-type-input').value = event.target.innerText;
        })
    });

    sleep(150).then(() => {
        removeClass('down-arrow', 'arrow-rotate');
        removeClass('coach-type-menu', 'visible');
        addClass('coach-type-menu', 'hidden');

        if (document.getElementById('coach-type-input').value != 'Select Coach Type') {

            document.getElementById('coach-no-input').disabled = true;
        } else {
            document.getElementById('coach-no-input').disabled = false;
        }
    });
});

/*
 * this two functions are enabling and disabling search coach fields
 */
document.getElementById('coach-no-input').addEventListener('keyup', function () {

    if (document.getElementById('coach-no-input').value != '') {

        document.getElementById('coach-type-input').disabled = true;
    } else {
        document.getElementById('coach-type-input').disabled = false;
    }
});

/*
 * this two functions are enabling and disabling maintanence and halt button
 */
const underMaintanenceBuses = document.getElementsByClassName('maintained');
const haltedBuses = document.getElementsByClassName('halt');

for (const bus of underMaintanenceBuses) {

    bus.getElementsByClassName('maintain-btn')[0].disabled = true;
}

for (const bus of haltedBuses) {

    bus.getElementsByClassName('maintain-btn')[0].disabled = true;
    bus.getElementsByClassName('halt-btn')[0].disabled = true;
}

const coachNo = document.getElementsByClassName('coach-no');
const haltBtns = document.getElementsByClassName('halt-btn');
const maintainBtns = document.getElementsByClassName('maintain-btn');

const haltModalTitle = document.getElementById('halt-modal-title');
const maintainModalTitle = document.getElementById('maintain-modal-title');

for (let i = 0; i < haltBtns.length; i++) {

    haltBtns[i].addEventListener('click', function () {

        haltModalTitle.innerText = `Do you really want to halt Coach no. <strong>${coachNo[i].innerText}/<strong>?`;
    });
}

for (let i = 0; i < maintainBtns.length; i++) {

    maintainBtns[i].addEventListener('click', function () {

        maintainModalTitle.innerHTML = `Do you really want to take Coach no. <strong>${coachNo[i].innerText}/<strong> under maintanence?`;
    });
}