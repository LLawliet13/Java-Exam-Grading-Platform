function submitFiles() {
    
    var fdata = new FormData();
    var fileInput = $('#file')[0];
    var file = fileInput.files[0];
    fdata.append("files", file);
    
    $.ajax({

        url: "/Loadfile/Index",
        type: 'POST',
        data: fdata,
        processData: false,
        contentType: false,
        success: function (data) {
            /*$('#ExamBank').html(data);*/
            alert("ok");
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
    console.log("oke e2");
}
console.log("oke e");
    
