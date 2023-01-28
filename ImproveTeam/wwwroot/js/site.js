$(document).ready(function () {
    showNavbar('header-toggle', 'nav-bar', 'body-pd', 'header');
})

function showNavbar(toggleId, navId, bodyId, headerId) {
    const toggle = document.getElementById(toggleId);
    const nav = document.getElementById(navId);
    const bodypd = document.getElementById(bodyId);
    const headerpd = document.getElementById(headerId);

    if (toggle && nav && bodypd && headerpd) {
        $(toggle).click(function () {
            $(nav).toggleClass('show');
            $(toggle).toggleClass('bx-x');
            $(bodypd).toggleClass('body-pd');
            $(headerpd).toggleClass('body-pd');

            //$($.fn.dataTable.tables(true)).css('width', '100%');
            //$($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw();
        })
    }
}

function onLogoutBtnClick(button) {
    $(button).closest('form').submit();
}