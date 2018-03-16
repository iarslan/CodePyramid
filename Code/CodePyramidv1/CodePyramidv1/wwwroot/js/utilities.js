(function () {
    var mobileMenu = $("#mobileMenu");
    var ul = $(".sidebar-menu");

    function init() {
        mobileMenu.on('click', function (e) {
            e.preventDefault();
            ul.slideToggle('slow', 'swing');
            console.log("mobile menu clicked");
        });
    }

    init();
})();