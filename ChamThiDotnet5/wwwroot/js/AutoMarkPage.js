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

                url: '/AutoMark/AutoMark',
                type: 'Post',
                data: {
                    'ClassId': parseInt(classid),
                    'ExamId': parseInt(examid)
                },
                dataType: 'text',
                success: function (data) {
                    alert("Sent");
                },
                error: function (request, error) {
                    alert("Request: " + JSON.stringify(request));
                }
            });

        }
        document.getElementById('link-tab-fa2d').click();

    }
    }