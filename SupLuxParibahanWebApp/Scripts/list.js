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