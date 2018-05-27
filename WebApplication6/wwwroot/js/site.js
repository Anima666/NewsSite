$(document).ready(function () {
    $(document).on('change', '.btn-file :file', function () {
        var input = $(this),
            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        input.trigger('fileselect', [label]);
    });

    $('.btn-file :file').on('fileselect', function (event, label) {

        var input = $(this).parents('.input-group').find(':text'),
            log = label;

        if (input.length) {
            input.val(log);
        } else {
            if (log) alert(log);
        }

    });
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#img-upload').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
    $("#imgInp").change(function () {
        readURL(this);
    });
});

function CreateInputTag() {

    for (var i = 0; i < instance.getSelection().length; i++) {
        var elementJq = $('<input>', {
            value: instance.getSelection()[i].name,
            name: "tags[" + i + "].Name",
            style: "display:none",
        });

        $('#FormEdit').append(elementJq);
    }
}

function like(id) {
    $.ajax({
        type: "POST",
        url: "/Post/LikeThis/" + id,
        dataType: "json",
        success: function (msg) {
            var i = $("#like-" + id + " i");
            i.removeClass("fa-heart-o").addClass("fa-heart");
            $("#like-" + id + " span").text(msg);
        }
    });
}
function SaveMarkDownText() {
    $("#Post_Text").val(simplemde.value());

    CreateInputTag();
}