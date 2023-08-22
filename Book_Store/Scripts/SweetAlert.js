function Success(text) {
    swal("Success", text, "success");
}

function info(type) {
    swal("The Operation Couldn't be complete!!", type, "warning");
}

function ErrorAlert(text) {
    swal( "Error",
         text,
         "error");
}
function ErrorWithModal(type) {
    let arr = type.split('&');
    swal({
        title: "Authenication Failed",
        text: "there is " + (arr.length - 1) + " row doesn't match our pattern",
        type: "warning",
        buttons: ['Cancel', 'Show Errors'],
        dangerMode: true,
    }).then((Showwerror) => {
        if (Showwerror) {
            for (var i = 1; i < arr.length; i++) {
                $("#Errorslist").append("<li class='list-group-item d-flex justify-content-between align-items-center'>" + arr[i] + "<span class='badge badge-primary badge-pill'>" + i + "</span>" + "</li>")
            }
            $("#errormodal").modal();
        }
    });
}
