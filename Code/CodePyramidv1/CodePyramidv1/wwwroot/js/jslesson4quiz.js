var quiz1 = (function () {
    var submitBtn = document.getElementById("btnSubmit");
    var q1c1 = document.getElementById("q1c1");
    var q1c2 = document.getElementById("q1c2");
    var q1c3 = document.getElementById("q1c3");
    var q1c4 = document.getElementById("q1c4");
    var q2c1 = document.getElementById("q2c1");
    var q2c2 = document.getElementById("q2c2");
    var q2c3 = document.getElementById("q2c3");
    var q2c4 = document.getElementById("q2c4");
    var answers = [3, 2];
    var chosenAnswers = [0, 0];

    var questions = document.getElementById("questions");
    var results = document.getElementById("results");
    var score = document.getElementById("score");
    var missed = document.getElementById("missed");
    var printBtn = document.getElementById("btnPrint");
    var scoreNum = 0;
    var missedList = [];

    function selectq1c1() {
        q1c1.style.border = "5px solid green";
        q1c2.style.border = "";
        q1c3.style.border = "";
        q1c4.style.border = "";
        chosenAnswers[0] = 1;
    }

    function selectq1c2() {
        q1c2.style.border = "5px solid green";
        q1c1.style.border = "";
        q1c3.style.border = "";
        q1c4.style.border = "";
        chosenAnswers[0] = 2;
    }

    function selectq1c3() {
        q1c3.style.border = "5px solid green";
        q1c2.style.border = "";
        q1c1.style.border = "";
        q1c4.style.border = "";
        chosenAnswers[0] = 3;
    }

    function selectq1c4() {
        q1c4.style.border = "5px solid green";
        q1c2.style.border = "";
        q1c3.style.border = "";
        q1c1.style.border = "";
        chosenAnswers[0] = 4;
    }

    function selectq2c1() {
        q2c1.style.border = "5px solid green";
        q2c2.style.border = "";
        q2c3.style.border = "";
        q2c4.style.border = "";
        chosenAnswers[1] = 1;
    }

    function selectq2c2() {
        q2c2.style.border = "5px solid green";
        q2c1.style.border = "";
        q2c3.style.border = "";
        q2c4.style.border = "";
        chosenAnswers[1] = 2;
    }

    function selectq2c3() {
        q2c3.style.border = "5px solid green";
        q2c2.style.border = "";
        q2c1.style.border = "";
        q2c4.style.border = "";
        chosenAnswers[1] = 3;
    }

    function selectq2c4() {
        q2c4.style.border = "5px solid green";
        q2c2.style.border = "";
        q2c3.style.border = "";
        q2c1.style.border = "";
        chosenAnswers[1] = 4;
    }

    function hideTest() {
        results.style.display = 'block';
        questions.style.display = 'none';
    }

    function gradeTest() {
        hideTest();
        scoreNum = 0;
        for (var i = 0; i < 2; i++) {
            if (answers[i] == chosenAnswers[i]) {
                scoreNum++;
            } else {
                missedList.push(i + 1);
            }
        }
        score.innerText = scoreNum;
        var missedString = '';
        var tempString = '';
        for (var i = 0; i < missedList.length; i++) {
            if (i == (missedList.length - 1)) {
                tempString = missedList[i].toString();
            } else {
                tempString = missedList[i].toString() + ", ";
            }
            missedString += tempString;
        }
        if (missedList.length == 0) {
            missed.innerText = "None";
        } else {
            missed.innerText = missedString;
        }

        document.getElementById("percentScore").value = ((100 / 2) * scoreNum).toString();
        document.getElementById("assessmentName").value = "Javascript Quiz 4";

        document.getElementById("jsquiz4form").submit();

    }

    function init() {
        q1c1.addEventListener("click", selectq1c1);
        q1c2.addEventListener("click", selectq1c2);
        q1c3.addEventListener("click", selectq1c3);
        q1c4.addEventListener("click", selectq1c4);
        q2c1.addEventListener("click", selectq2c1);
        q2c2.addEventListener("click", selectq2c2);
        q2c3.addEventListener("click", selectq2c3);
        q2c4.addEventListener("click", selectq2c4);
        submitBtn.addEventListener("click", gradeTest);
    }

    init();
})();
