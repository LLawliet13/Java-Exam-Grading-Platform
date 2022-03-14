function getExamInfo(classid, examid,rowid) {
 
    // tesst
    console.log(rowid);

    let stu_table = document.getElementsByClassName('studentList')[0];

    if (stu_table.getElementsByClassName('table-active').length > 0) {
        stu_table.getElementsByClassName('table-active')[0].classList.remove('table-active')
    }
    
    rowid.classList.add('table-active')
    

    $.ajax({

        url: "/Teacher/getExam_Student",
        type: 'post',
        data: {
            'classid': classid,
            'examid': examid,
        },
        dataType: 'text',
        success: function (data) {
            $('#class_exam').html(data);
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}

function autoMark(){
    let selectedRow = document.getElementById("table-active").getElementsByTagName('button')[0]
    let classid = selectedRow.attr('classid');
    let examid = selectedRow.attr('examid');
    // tesst
    console.log(classId + "," + examid);

    //$.ajax({

    //    url: 'http://voicebunny.comeze.com/index.php',
    //    type: 'GET',
    //    data: {
    //        'numberOfWords': 10
    //    },
    //    dataType: 'json',
    //    success: function (data) {
    //        alert('Data: ' + data);
    //    },
    //    error: function (request, error) {
    //        alert("Request: " + JSON.stringify(request));
    //    }
    //});
}