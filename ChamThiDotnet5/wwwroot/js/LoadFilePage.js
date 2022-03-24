$("#frmExamBank").submit(function (e) {
    e.preventDefault();
    var fdata = new FormData();
    var fileInput = $('#file')[0];
    var file = fileInput.files[0];
    fdata.append("File", file);
    
    //$.ajax({

    //    url: "/Loadfile/Index",
    //    type: 'post',
    //    data: fdata,
    //    processData: false,
    //    contentType: false,
    //    success: function (data) {
    //        $('#ExamBank').html(data);
    //    },
    //    error: function (request, error) {
    //        alert("Request: " + JSON.stringify(request));
    //    }
    //});
    console.log("oke e");
});
    
