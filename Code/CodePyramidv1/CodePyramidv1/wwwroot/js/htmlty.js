(function () {
    var runBtn = document.getElementById("runBtn");
    var inputBox = document.getElementById("inputBox");
    var resultBox = document.getElementById("resultBox");

    function runCode() {
        resultBox.innerHTML = '';
        resultBox.innerHTML = inputBox.value;
    }

    function init() {
        runBtn.addEventListener("click", runCode);
    }
    init();
})();