const emailWrapper = document.getElementById('login-email-input-wrapper');
const passwordWrapper = document.getElementById('login-password-input-wrapper');

const emailField = document.getElementById('login-email-field');
const passwordField = document.getElementById('login-password-field');

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
document.getElementById('login-button').addEventListener('click', function () {

    if (emailField.value == '' || !emailField.value.includes('@')) {
        emailField.classList.add('input-empty-error');
        emailWrapper.classList.add('input-empty-error');
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

passwordField.addEventListener('click', function () {

    passwordField.classList.remove('input-empty-error');
    passwordWrapper.classList.remove('input-empty-error');
})