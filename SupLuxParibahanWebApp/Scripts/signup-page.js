const emailWrapper = document.getElementById('signup-email-input-wrapper');
const mobileWrapper = document.getElementById('signup-mobile-input-wrapper');
const passwordWrapper = document.getElementById('signup-password-input-wrapper');

const emailField = document.getElementById('signup-email-field');
const mobileField = document.getElementById('signup-mobile-field');
const passwordField = document.getElementById('signup-password-field');

/*
 *  making the password field visible and invisible
 */
document.getElementById('pasword-icon').addEventListener('click', function () {

    const passwordEye = document.getElementById('password-eye-icon');

    passwordEye.classList.toggle('fa-eye');
    passwordEye.classList.toggle('fa-eye-slash');

    if (passwordEye.classList.contains('fa-eye')) {

        passwordField.type = 'password';
    } else {
        passwordField.type = 'text';
    }
});

/*
 *  checking if the fields are filled properly
 */
document.getElementById('signup-button').addEventListener('click', function () {

    if (emailField.value == '' || !emailField.value.includes('@')) {
        emailField.classList.add('input-empty-error');
        emailWrapper.classList.add('input-empty-error');
    }

    if (mobileField.value == '') {
        mobileField.classList.add('input-empty-error');
        mobileWrapper.classList.add('input-empty-error');
    }

    if (passwordField.value == '') {
        passwordField.classList.add('input-empty-error');
        passwordWrapper.classList.add('input-empty-error');
    }
});

emailField.addEventListener('click', function () {

    emailField.classList.remove('input-empty-error');
    emailWrapper.classList.remove('input-empty-error');
})

mobileField.addEventListener('click', function () {

    mobileField.classList.remove('input-empty-error');
    mobileWrapper.classList.remove('input-empty-error');
})

passwordField.addEventListener('click', function () {

    passwordField.classList.remove('input-empty-error');
    passwordWrapper.classList.remove('input-empty-error');
})