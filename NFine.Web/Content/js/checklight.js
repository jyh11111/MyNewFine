////////////////////////////////////////////////////////////////////////////////////////////
//计算两个日期天数差的函数，通用后面的大于前面的日期
////////////////////////////////////////////////////////////////////////////////////////////
function DateDiff(sDate1, sDate2) {  //sDate1和sDate2是yyyy-MM-dd格式

    var aDate, oDate1, oDate2, iDays;

    oDate1 = new Date(sDate1);  //转换为yyyy-MM-dd格式        
    oDate2 = new Date(sDate2);
    iDays = parseInt(Math.abs(oDate2 - oDate1) / 1000 / 60 / 60 / 24); //把相差的毫秒数转换为天数

    return iDays;  //返回相差天数
}
//1..绿色 2..黄色 3..红色

function getmkcolor() {
    if ($('#InvoiceNo').val() != "") return 1;
    var t1 = $.formatDate('yyyy-MM-dd', new Date());
    var t2 = $('#MK_inputDate').val();
    if (t2 == "") return 0;//0..没输日期       

    var d1 = DateDiff(t2, t1);
    if (d1 <= 14) {
        return 1;
    } else if (d1 <= 21)
    { return 2; }
    else { return 3; }
}

function getencolor() {
    if ($('#HandOverDate').val() != "") return 1;
    var t1 = $.formatDate('yyyy-MM-dd', new Date());
    var t2 = $('#EN_inputDate').val();
    if (t2 == "") return 0;//0..没输日期       

    var d1 = DateDiff(t2, t1);
    if (d1 <= 7) {
        return 1;
    } else if (d1 <= 14)
    { return 2; }
    else { return 3; }
}

function getascolor() {
    if ($('#HandOverDate').val() == "") return 0; //工程部没录入
    var t1 = $.formatDate('yyyy-MM-dd', new Date());
    var t2 = $('#HandOverDate').val();
    var d1 = $('#RecvState').val();
    if (d1 == 0 || d1 == 3) { return 3; }
    if (d1 == 1 || d1 == 2) { return 1; }

    var d2 = DateDiff(t2, t1);
    if (d2 <= 7) { return 1; }
    else if (d2 <= 14) { return 2; }
    else { return 3; }
}
function checklight() {
    i1 = getmkcolor();
    i2 = getencolor();
    i3 = getascolor();
    k = i1;
    if (k < i2) { k = i2 };
    if (k < i3) { k = i3 };
    $('#Asset_state').val(k);
    $('#mklight').show();
    switch (i1) {
        case 1: $('#mklight').css('color', 'green'); break;
        case 2: $('#mklight').css('color', 'yellow'); break;
        case 3: $('#mklight').css('color', 'red'); break;
        default: $('#mklight').hide(); break;
    }
    $('#mklight').show();
    switch (i2) {
        case 1: $('#enlight').css('color', 'green'); break;
        case 2: $('#enlight').css('color', 'yellow'); break;
        case 3: $('#enlight').css('color', 'red'); break;
        default: $('#enlight').hide(); break;
    }
    $('#aslight').show();
    switch (i3) {
        case 1: $('#aslight').css('color', 'green'); break;
        case 2: $('#aslight').css('color', 'yellow'); break;
        case 3: $('#aslight').css('color', 'red'); break;
        default: $('#aslight').hide(); break;
    }

}
