var comicSans = (function () {
    var pList = document.getElementsByTagName("p");
    var toggleSans = document.querySelector("#comicSansBtn");

    function toggle() {

        if (pList[0].style.fontFamily.includes('cursive')) {
            for (var i = 0; i < pList.length; i++) {
                pList[i].style.fontFamily = "";
            }
        } else {
            console.log("asdflkj");
            console.log(pList.length);
            for (var i = 0; i < pList.length; i++) {
                pList[i].style.fontFamily = "'Comic Sans MS', cursive, sans-serif";	

            }
        }
    }


    function init() {
        toggleSans.addEventListener("click", toggle);

    }

    init();

})();