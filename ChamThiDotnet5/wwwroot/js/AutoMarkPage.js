function chooseCourse(obj) {
    let val = obj.value;
    // tesst
    console.log(val)
    

    $.ajax({

        url: 'AutoMark/',
        type: 'GET',
        data: {
            'numberOfWords': 10
        },
        dataType: 'json',
        success: function (data) {
            alert('Data: ' + data);
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}

function displayCourse(){
    let classId = $('#classid').val();
    // tesst
    console.log(classId)

    $.ajax({

        url: 'http://voicebunny.comeze.com/index.php',
        type: 'GET',
        data: {
            'numberOfWords': 10
        },
        dataType: 'json',
        success: function (data) {
            alert('Data: ' + data);
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}