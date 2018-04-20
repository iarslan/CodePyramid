(function () {
    var runBtn = document.getElementById("runBtn");
    var inputBox = document.getElementById("inputBox");
    var resultBox = document.getElementById("resultBox");

    function runCode() {
        resultBox.innerHTML = '';
        resultBox.innerHTML = inputBox.value;
        if (inputBox.value.includes("<script>")) {
            var str = inputBox.value;
            var jsCode = str.substring(str.lastIndexOf("<script>") + 8, str.lastIndexOf("</script>"));
            eval(jsCode);
        }
    }

    function init() {
        runBtn.addEventListener("click", runCode);
    }
    init();
})();