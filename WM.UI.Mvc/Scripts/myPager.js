//var CurrentPage = 1;
//var totalRow = 0; //model.length;
var pageSize = 15;
var totalPage = 0;
var currentPage = 1;

function sayfalamaYap(model, pagingSize, gecerliSayfa) {
    //alert('model')
    if (parseInt(pagingSize) > 0) {
        pageSize = pagingSize;
    }
    if (parseInt(gecerliSayfa) > 0) {
        currentPage = parseInt(gecerliSayfa);
    }
    //table a id gridT ve tr ye class gridtr vermeyi unutma, loading için en üst div id= div1
    var totalRow = model.length;
    //alert('toplam kayıt sayısı: ' + totalRow)
    if (totalRow != 0 && pageSize != 0) {
        totalPage = Math.ceil(totalRow / pageSize);
        //alert(totalPage)
        currentPage = currentPage;//CurrentPage;//model[0].Pager.CurrentPage;
    }
    //alert(totalPage)
    $("#toplamKayitSayisi").text(totalRow)
    //$("#toplamKayitSayisi").text(currentPage + "-" + parseInt(pageSize) + "/" + totalRow)
    //$("#toplamKayitSayisi").text(pageSize * currentPage + "-" + parseInt(pageSize * currentPage + pageSize) + "/" + totalRow)

    //if (totalPage > 1) {
    //alert(1)
    CreatePagingButtons();
    getPaging(currentPage);
    //}
}
function CreatePagingButtons() {
    //alert('totalPage: ' + totalPage)
    var curPage = parseInt(currentPage); //1;//CurrentPage;//model[0].Pager.CurrentPage;
    //tablo içindeki controllera gönderilen çağrılar bu değeri kullanıyor, geri dönünce aynı sayfaya gelsin diye:
    $("#page").val(curPage)
    //alert('curPage: ' + curPage)
    //myPaging div e ul eklenir

    $("#myPaging").html("<ul class=\"pagination tableRowDesign\"></ul>");
    //$("#myPaging > ul.pagination").prepend(
    //    $("<li class=\"page-item disabled\" id=\"total\"><a href='#' class=\"page-link\" >Toplam Eczane: " + totalRow + "</a></li>")
    //);
    //geri tuşu eklenir
    if (totalPage > 5) {
        $("#myPaging > ul.pagination").append(
            $("<li id=\"ilkSayfa\" class=\"page-item yonlendirmeButonu disabled\"><a class=\"page-link\" ><i class=\"fa fa-step-backward\"></a></li>")
        );
        $("#myPaging > ul.pagination").append(
            $("<li id=\"oncekiSayfa\" class=\"page-item yonlendirmeButonu disabled\"><a class=\"page-link\" ><i class=\"fa fa-chevron-left\"></a></li>")
        );
    }
    //sayılar eklenir
    if (totalPage > 1) {
        $("#myPaging > ul.pagination").each(function (i, el) {
            for (var i = 0; i < totalPage; i++) {
                if (i == totalPage - (curPage - 1) - 1)
                    $(this).append("<li class=\"page-item sayfaNumarasi active\"></li>");
                else
                    $(this).append("<li class=\"page-item sayfaNumarasi\"></li>");
            }
        });
    }
    //sayılara page-link classı eklenir
    $("#myPaging > ul > li.sayfaNumarasi").each(function (i, el) {
        $(this).append(
            $("<a id=\"page_Link_" + i + "\" >" + (i + 1) + "</a>").addClass("page-link")
        );

        if (Math.abs(i - curPage) < 4) {
            $("#page_Link_" + i).css('display', 'block');
        }
        else {
            $("#page_Link_" + i).css('display', 'none');
        }
    });
    //ileri tuşu eklenir
    if (totalPage > 5) {
        $("#myPaging > ul.pagination").append(
            $("<li id=\"sonrakiSayfa\" class=\"page-item yonlendirmeButonu\"><a class=\"page-link\" ><i class=\"fa fa-chevron-right\"></a></li>")
        );
        $("#myPaging > ul.pagination").append(
            //<i class=\"fa fa-step-forward\">
            $("<li id=\"sonSayfa\" class=\"page-item yonlendirmeButonu\"><a class=\"page-link\" >" + totalPage + "</a></li>")
        );
    }

    //sayılara basınca ne olcağını .on ile belirledik
    $("#myPaging > ul > li.sayfaNumarasi").on("click", function () {
        //alert($(this).text());
        //alert('pageSize: ' + pageSize)
        $("#page").val($(this).text())
        getPaging($(this).text())
        //makeActive($(this).text())
    });
    //yönlendirme butonlarına basınca yapılacaklar
    $("#ilkSayfa").on("click", function () {
        var currPage = parseInt(currentPage);
        //alert(currPage)
        $("#page").val(1)
        if (currPage == 1) {
            getPaging(currPage)
        }
        else {
            getPaging(1)
        }
    });
    $("#sonSayfa").on("click", function () {
        var currPage = parseInt(currentPage);
        //alert(currPage)
        $("#page").val(totalPage)
        if (currPage == totalPage) {
            getPaging(currPage)
        }
        else {
            getPaging(totalPage)
        }
    });
    $("#oncekiSayfa").on("click", function () {
        var currPage = parseInt(currentPage);
        //alert(currPage)
        $("#page").val(totalPage)
        if (currPage == 1) {
            getPaging(currPage)
        }
        else {
            getPaging(currPage - 1)
        }
    });
    $("#sonrakiSayfa").on("click", function () {
        var currPage = parseInt(currentPage);
        //alert(currPage)
        $("#page").val(totalPage)
        if (currPage == totalPage) {
            getPaging(currPage)
        }
        else {
            getPaging(currPage + 1)
        }
    });
}
function getPaging(prCurrentPage) {
    $("#main #gridT > tbody > tr td.expandm").each(function (i, el) {
        $(this).toggleClass("expandm collapsem");
        $(this).parent().closest("tr").next().slideToggle(1);
    });
    $("#gridT > tbody > tr.gridtr").each(function (i, el) {
        if (i >= pageSize * (prCurrentPage - 1) && i < pageSize * prCurrentPage) {
            $(this).css('display', 'table-row');
            //if (parseInt(currentPage) == prCurrentPage) {
            //    $(this).addClass('gosterilen');
            //}
            //else {
            //    $(this).removeClass('gosterilen');
            //}
            //alert('gridtr')
        }
        else {
            $(this).css('display', 'none');
        }
    });
    makeActive(prCurrentPage)
}
function makeActive(curPage) {
    var indexSayi = 0
    $("#myPaging > ul.pagination > li.sayfaNumarasi").each(function (i, el) {
        var sayi = parseInt(curPage) - 1
        indexSayi = sayi //- 2
        if (i == (indexSayi)) {
            $(this).addClass("active");
            currentPage = curPage
        }
        else {
            $(this).removeClass("active")
        }
    });
    //alert((indexSayi -1))
    for (var i = 0; i < totalPage; i++) {

        if (indexSayi == 0) {
            $("#oncekiSayfa").addClass("disabled");
            $("#ilkSayfa").addClass("disabled");
        }
        else {
            $("#oncekiSayfa").removeClass("disabled");
            $("#ilkSayfa").removeClass("disabled");
        }

        if (indexSayi == totalPage - 1) {
            $("#sonrakiSayfa").addClass("disabled");
            $("#sonSayfa").addClass("disabled");
        }
        else {
            $("#sonrakiSayfa").removeClass("disabled");
            $("#sonSayfa").removeClass("disabled");
        }

        if (indexSayi < 3) {
            if (i < 5) {
                $("#page_Link_" + i).css('display', 'block');
            }
            else {
                $("#page_Link_" + i).css('display', 'none');
            }
        }
        else if (indexSayi > totalPage - 5) {
            if (totalPage - 5 <= i) {
                $("#page_Link_" + i).css('display', 'block');
            }
            else {
                $("#page_Link_" + i).css('display', 'none');
            }
        }
        else if (Math.abs(i - indexSayi) < 3) {
            $("#page_Link_" + i).css('display', 'block');
        }
        else {
            $("#page_Link_" + i).css('display', 'none');
        }
    }
}