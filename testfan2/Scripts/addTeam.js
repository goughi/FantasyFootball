var mid = 0;

function chkcontrol(j) {
    var totalDef = 0;
    var totalMid = 0;
    var totalFwd = 0;
    var totalGk = 0;
    var sum = 0;
    var checkBoxes = document.getElementsByName(j.name);

    //var cells = document.getElementsByTagName('td');
    //for (var i = 0; i <= cells.length; i++) {
    //    cells[i].addEventListener('click', clickHandler);
    //}

    //function clickHandler() {
    //    document.getElementById("def1").value = (this.textContent);
    //}
    for (var i = 0; i < document.form1.def.length; i++) {

        if (document.form1.def[i].checked) {
            totalDef = totalDef + 1;
            sum = sum + parseFloat(document.form1.def[i].id);
            //for (var p = 0; i < document.form1.defen.length; p++) {

            //    if (document.form1.def[i].checked) {
            //        totalDef = totalDef + 1;
            //        if (totalDef == 1) {
            //            document.getElementById("def1").value = document.form1.defen[p].innerHTML;
            //        }
            //    }
            //}
        }
        document.getElementById("msg").innerHTML = "Sum :" + sum.toFixed(2);

        if (totalDef > 4) {
            sum = sum - parseFloat(document.form1.def[j].id);

            document.form1.def[j].checked = false;
            alert("Please Select only four defenders");
            document.getElementById("msg").innerHTML = "Sum :" + sum.toFixed(2);

            return false;
        }

        if (sum > 100) {
            sum = sum - parseFloat(document.form1.def[j].id);
            document.form1.def[j].checked = false;
            alert("Sum of the selection can't be more than 100 million");
            document.getElementById("msg").innerHTML = "Sum :" + sum.toFixed(2);
            return false;
        }

    }


    //function chkcontrol(j) {

    //    var totalMid = 0;

    //    var sum = parseFloat(document.getElementById("msg").innerHTML);

    for (var i = 0; i < document.form1.gk.length; i++) {


        if (document.form1.gk[i].checked) {
            totalGk = totalGk + 1;
            sum = sum + parseFloat(document.form1.gk[i].id);

        }
        document.getElementById("msg").innerHTML = "Sum :" + sum.toFixed(2);
        if (totalGk > 1) {
            sum = sum - parseFloat(document.form1.gk[j].id);
            document.form1.gk[j].checked = false;
            alert("Please Select only one goalkeeper");

            document.getElementById("msg").innerHTML = "Sum :" + sum.toFixed(2);
            return;
        }


        if (sum > 100) {
            sum = sum - parseFloat(document.form1.gk[j].id);
            document.form1.gk[j].checked = false;
            alert("Sum of the selection can't be more than 100 million");
            document.getElementById("msg").innerHTML = "Sum :" + sum.toFixed(2);
            return;
        }


    }

    for (var i = 0; i < document.form1.mid.length; i++) {


        if (document.form1.mid[i].checked) {
            totalMid = totalMid + 1;
            mid += 1;
            sum = sum + parseFloat(document.form1.mid[i].id);

        }
        document.getElementById("msg").innerHTML = "Sum :" + sum.toFixed(2);
        if (totalMid > 4) {
            sum = sum - parseFloat(document.form1.mid[j].id);
            document.form1.mid[j].checked = false;
            alert("Please Select only two forwards");

            document.getElementById("msg").innerHTML = "Sum :" + sum.toFixed(2);
            return;
        }


        if (sum > 100) {
            sum = sum - parseFloat(document.form1.mid[j].id);
            document.form1.mid[j].checked = false;
            alert("Sum of the selection can't be more than 100 million");
            document.getElementById("msg").innerHTML = "Sum :" + sum.toFixed(2);
            return;
        }


    }

    //function chkcontrol(j) {

    //    var totalFwd = 0;
    //    var sum = parseFloat(document.getElementById("msg").innerHTML);
    for (var i = 0; i < document.form1.fwd.length; i++) {

        if (document.form1.fwd[i].checked) {
            totalFwd = totalFwd + 1;
            sum = sum + parseFloat(document.form1.fwd[i].id);

        }
        document.getElementById("msg").innerHTML = "Sum :" + sum.toFixed(2);

        if (totalFwd > 2) {
            sum = sum - parseFloat(document.form1.fwd[j].id);
            document.form1.fwd[j].checked = false;
            alert("Please Select only two forwards");


            document.getElementById("msg").innerHTML = "Sum :" + sum.toFixed(2);
            return;
        }


        if (sum > 100) {
            sum = sum - parseInt(document.form1.fwd[j].id);
            document.getElementById.fwd[j].checked = false;
            alert("Sum of the selection can't be more than 100 million");
            document.getElementById("msg").innerHTML = "Sum :" + sum.toFixed(2);
            return;
        }

    }

}

function countTeam() {
    var teamTotal = 0;
    var totalFwd = 0;
    var totalDef = 0;
    var totalMid = 0;
    var totalGk = 0;
    for (var i = 0; i < document.form1.fwd.length; i++) {
        var myfwd = document.form1.fwd[i];
        if (myfwd.checked == true) {
            totalFwd = totalFwd + 1;
            teamTotal += 1;
        }
    }
    for (var i = 0; i < document.form1.def.length; i++) {

        if (document.form1.def[i].checked) {
            totalDef = totalDef + 1;
            teamTotal += 1;
        }
    }
    for (var i = 0; i < document.form1.mid.length; i++) {

        if (document.form1.mid[i].checked) {
            totalMid = totalMid + 1;
            teamTotal += 1;
        }
    }
    for (var i = 0; i < document.form1.gk.length; i++) {

        if (document.form1.gk[i].checked) {
            totalGk = totalGk + 1;
            teamTotal += 1;
        }
    }
    if (teamTotal != 11) {
        alert("Your team must consist of 1 goalkeeper, 4 defenders, 4 midfielders and 2 forwards!");
        return false;
    }
}