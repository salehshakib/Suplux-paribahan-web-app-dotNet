/*
 * functions
 */
function addOrRemoveElement(addElementID, removeElementID) {

    const addElement = document.getElementById(addElementID);
    const removeElement = document.getElementById(removeElementID);

    addElement.classList.add('d-block');
    addElement.classList.remove('d-none');

    removeElement.classList.remove('d-block');
    removeElement.classList.add('d-none');
}

function changeFieldType(passFieldId, event) {

    const passField = document.getElementById(passFieldId);

    event.target.classList.toggle('fa-eye');
    event.target.classList.toggle('fa-eye-slash');

    if (event.target.classList.contains('fa-eye')) {
        passField.type = 'password';
    } else {
        passField.type = 'text';
    }
}


/*
 * displaying user info edit form
 */
document.getElementById('edit-profile-button').addEventListener('click', function () {

    addOrRemoveElement('edit-info-wrapper', 'info-wrapper');
});

/*
 * displaying user info
 */
document.getElementById('close-edit-form-button').addEventListener('click', function () {

    addOrRemoveElement('info-wrapper', 'edit-info-wrapper');
});

/*
 * changing visibility of the current password field
 */
document.getElementById('current-password-eye-icon').addEventListener('click', function (event) {

    changeFieldType('current-pass-field', event);
});

/*
 * changing visibility of the new password field
 */
document.getElementById('new-password-eye-icon').addEventListener('click', function (event) {

    changeFieldType('new-pass-field', event);
});

/*
 * changing visibility of the confirm password field
 */
document.getElementById('confirm-password-eye-icon').addEventListener('click', function (event) {

    changeFieldType('confirm-pass-field', event);
});

document.getElementById('change-pass-button').addEventListener('click', function () {

    addOrRemoveElement('change-password-container', 'password-button-row');
});

document.getElementById('close-change-pass-form-button').addEventListener('click', function () {

    addOrRemoveElement('password-button-row', 'change-password-container');
});