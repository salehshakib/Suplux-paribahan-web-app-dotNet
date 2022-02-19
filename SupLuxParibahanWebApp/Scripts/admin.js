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

    sleep(100).then(() => {
        removeClass('down-arrow', 'arrow-rotate');
        removeClass('coach-type-menu', 'visible');
        addClass('coach-type-menu', 'hidden');

    });
});