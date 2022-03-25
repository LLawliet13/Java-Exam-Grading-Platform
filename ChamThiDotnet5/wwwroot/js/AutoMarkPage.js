function getExamInfo(classid, examid, rowid) {

    // test
    console.log(rowid);

    let stu_table = document.getElementsByClassName('studentList')[0];
    //neu co class dang active thi xoa di
    if (stu_table.getElementsByClassName('table-active').length > 0) {
        stu_table.getElementsByClassName('table-active')[0].classList.remove('table-active')
    }
    //active dong dc chon
    rowid.classList.add('table-active')

    // lay danh sach hoc sinh
    $.ajax({

        url: "/Teacher/getPendingExam_Student",
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

function autoMark() {
    let table = document.getElementsByClassName('u-container-layout-3')[0];

    let selectedRow = table.getElementsByClassName("table-active")[0]
    if (selectedRow == null) alert("please choose exam");
    else {
        selectedRow = selectedRow.getElementsByTagName('button')[0];
        let classid = selectedRow.getAttribute('classid');
        let examid = selectedRow.getAttribute('examid');
        // tesst
        console.log(classid + "," + examid);
        // lay view 


        // alert + chuyen tap
        if (confirm("Automark ?") == true) {

            $.ajax({

                url: '/AutoMark/AutoMarkAClass_Exam',
                type: 'Post',
                data: {
                    'ClassId': parseInt(classid),
                    'ExamId': parseInt(examid)
                },
                dataType: 'text',
                success: function (data) {
                    alert("Automark Successfully");
                    updateExamResultTable(classid,examid);
                },
                error: function (request, error) {
                    alert("Request: " + JSON.stringify(request));
                }
            });
            //checking
            
            

        }
        

    }
}

function updateExamResultTable(classid, examid) {
    $.ajax({

        url: '/Teacher/updateExamResultTable',
        type: 'Get',
        data: {
            
        },
        dataType: 'text',
        success: function (data) {
            $('#resultExamTable').html(data);
            updatePendingExamTable(classid, examid);
            
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}

function updatePendingExamTable(classid, examid) {
    
    $.ajax({

        url: '/Teacher/updatePendingExamTable',
        type: 'Post',
        data: {

        },
        dataType: 'text',
        success: function (data) {
            $("#pendingExamTable").html(data);
            ResetPendingStudentList(classid, examid);
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}

function updateResultStudentList(classid, examid, rowid) {
    // test
    console.log(rowid);

    let stu_table = document.getElementsByClassName('resultStudentList')[0];
    //neu co class dang active thi xoa di
    if (stu_table.getElementsByClassName('table-active').length > 0) {
        stu_table.getElementsByClassName('table-active')[0].classList.remove('table-active')
    }
    console.log(stu_table);
    //active dong dc chon
    rowid.classList.add('table-active')
   
        // lay danh sach hoc sinh
        $.ajax({

            url: "/Teacher/updateResultStudentList",
            type: 'post',
            data: {
                'ClassId': classid,
                'ExamId': examid,
            },
            dataType: 'text',
            success: function (data) {
                $('#resultClass_exam').html(data);
            },
            error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
    //}
    
    
}

function ResetPendingStudentList(classid, examid) {
    $.ajax({

        url: "/Teacher/getPendingExam_StudentList",
        type: 'Get',
        data: {
            
        },
        dataType: 'text',
        success: function (data) {
            $('#class_exam').html(data);
            document.getElementById('link-tab-fa2d').click();
            document.getElementById(classid + '_' + examid).click();
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });

}