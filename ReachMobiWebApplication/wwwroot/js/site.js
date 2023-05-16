// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('a').click(function () {
        // Remove the 'active' class from all links
        $('a').removeClass('active');

        // Add the 'active' class to the clicked link
        $(this).addClass('active');
    });
});