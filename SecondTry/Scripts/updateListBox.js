function updateListBox() {
    var x = document.getElementById("listboxProcFunc").value;
    $.getJSON('@Url.Action("ProcUnit")', { procFunc: x }, function (procUnit) {
        var test = $('#listboxProcUnit');
        test.empty();
        $.each(procUnit, function (index, procUnit) {
            test.append($('<option/>', {
                value: procUnit,
                text: procUnit
            }));
        });
    });
}