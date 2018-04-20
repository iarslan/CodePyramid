var comicSans = (function () {
    var pList = document.getElementsByTagName("p");
    var h1List = document.getElementsByTagName("h1");
    var h2List = document.getElementsByTagName("h2");
    var aList = document.getElementsByTagName("a");
    var toggleSans = document.querySelector("#comicSansBtn");
    function toggle() {


        if (pList.length > 0) {
            if (pList[0].style.fontFamily.includes('cursive')) {
                for (var i = 0; i < pList.length; i++) {
                    pList[i].style.fontFamily = "";
                }
            } else {
                for (var i = 0; i < pList.length; i++) {
                    pList[i].style.fontFamily = "'Comic Sans MS', cursive, sans-serif";
                }
            }
        }

        if (h1List.length > 0) {
            if (h1List[0].style.fontFamily.includes('cursive')) {
                for (var i = 0; i < h1List.length; i++) {
                    h1List[i].style.fontFamily = "";
                }
            } else {
                for (var i = 0; i < h1List.length; i++) {
                    h1List[i].style.fontFamily = "'Comic Sans MS', cursive, sans-serif";
                }
            }
        }

        if (h2List.length > 0) {
            if (h2List[0].style.fontFamily.includes('cursive')) {
                console.log("hdsk");
                for (var i = 0; i < h2List.length; i++) {
                    h2List[i].style.fontFamily = "";
                }
            } else {
                for (var i = 0; i < h2List.length; i++) {
                    console.log("change to h2");
                    h2List[i].style.fontFamily = "'Comic Sans MS', cursive, sans-serif";
                }
            }
        }

        if (aList.length > 0) {
            if (aList[0].style.fontFamily.includes('cursive')) {
                for (var i = 0; i < aList.length; i++) {
                    aList[i].style.fontFamily = "";
                }
            } else {
                for (var i = 0; i < aList.length; i++) {
                    aList[i].style.fontFamily = "'Comic Sans MS', cursive, sans-serif";
                }
            }
        }

        
    }


    function init() {
        toggleSans.addEventListener("click", toggle);

    }

    init();

})();