
//jQuery kodlarım
//jQuery()  ---  //$()

$(document).ready(function () {
    //console.log("ready!");

    var d1 = $('#wizard').is(':visible') ? 1 : 0;

    function f_ackapa() {
        switch (d1) {
            case 0:
                $('#chkAcKapa').prop("checked", true).change();
                $('#wizard').show();
                d1 = 1;
                break;
            case 1:
                $('#chkAcKapa').prop("checked", false).change();
                $('#wizard').hide();
                d1 = 0;
                break;
        }
    };

    ///////////***********///////////////////////
    //wizard gizleme butonu
    $('#btnGizle').click(function () {
        $('#wizard').hide();
        $('#btnAcKapa').show();
        $('#chkAcKapa').prop("checked", false).change();
    });

    //toggle: açma-kapama butonu
    $('#btnAcKapa').click(f_ackapa);

    //açma kapama kutusu -- on/off
    $('#chkAcKapa').change(function () {

        if (this.checked) {
            $('#wizard').show();
        }
        else {
            $('#wizard').hide();
        }
    });

});


