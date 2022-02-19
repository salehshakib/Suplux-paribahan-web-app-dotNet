const navBar = document.getElementById('suplux-navbar');

var lastScrollTop = 0;

window.onscroll = function () {

    var st = window.pageYOffset || document.documentElement.scrollTop;
    if (st > lastScrollTop) {

        if (window.screen.width > 990) {
            navBar.classList.add('nav-bg');
            navBar.classList.add('fixed');
        }

    } else {

        if (st === 0 && window.screen.width > 990 && location.pathname == '/') {
            navBar.classList.remove('nav-bg');
            navBar.classList.remove('fixed');
        }

    }
    lastScrollTop = st <= 0 ? 0 : st;
}

/*
 *  switching display none to nav user profile
 */
document.getElementById('logged-in-user-div').addEventListener('click', function () {

    document.getElementById('nav-user-dropdown-menu').classList.toggle('d-none');
});