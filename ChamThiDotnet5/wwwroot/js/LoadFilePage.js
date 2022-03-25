function submitFiles() {
    
    var fdata = new FormData();
    var fileInput = $('#fileDe')[0];
    var file = fileInput.files[0];
    fdata.append("fileDe", file);

    var fdata2 = new FormData();
    var fileInput2 = $('#fileTest')[0];
    var file2 = fileInput2.files[0];
    fdata.append("fileTest", file2);
    $.ajax({

        url: "/Loadfile/Index",
        type: 'POST',
        data: fdata,
        processData: false,
        contentType: false,
        success: function (data) {
            /*$('#ExamBank').html(data);*/
            alert("Upload Success!");
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
    console.log("Upload Success.");
}
console.log("Upload Success...");


    
