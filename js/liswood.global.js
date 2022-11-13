//############################################### GLOBAL FUNKCE, Dostupné přes Global. ###########################################

var Global = {
    //inicializuje všechny selectBoxy jako multiselectboxy (PRO DATATABLES)
    initMultiSelectBox: function (indexyMultiSelectu) {
        for (var i = 0; i < indexyMultiSelectu.length; i++) {
            var id = "#SEARCH_" + indexyMultiSelectu[i] + "_pom";

            var s = $(id).multiselect({
                menuWidth: 350,
                height: 350,
                classes: "NORMALNI_MULTI"
            });

            $(".ui-multiselect-none").trigger("click");

            $(document).on("change", id, function () {

                var pomId = $(this).attr("id");
                var cislo = pomId.substr(0, pomId.length - 4);
                var form = $("#" + cislo);
                var idMenu = "#multiselect_" + pomId + "_VALUES";
                var hodnota = "";

                $(idMenu).find("input[aria-selected='true']").each(function () {
                    var h = $(this).attr("value");
                    hodnota = hodnota + ";" + h;
                });

                form.val(hodnota);
                form.trigger("change");
            });

            //$(id).on("change", function () {

            //});
        }
        return;
    },

    //Nastavý vybrané preselect volby v multiselectBoxech (PRO DATATABLES)
    nastavPreselectMultiBox: function (boxId, hodnoty) {
        var fo = $(boxId);
        var elementy = fo.children("option");
        elementy.removeAttr("preSelected");
        var pravda = false;
        $(hodnoty).each(function () {
            var hodnota = this;
            if ((hodnota.length < 1)) return;
            var hodnotaInt = parseInt(hodnota);

            elementy.each(function () {
                var element = $(this);
                var val = parseInt($(this).attr("value"));
                if (val === hodnotaInt) {
                    element.attr("preSelected", "true");
                    pravda = true;
                }
            });
        });

        if (pravda) {
            $(boxId).multiselect("refresh2");
        }
    },
    //obsahuje INDEX (PRO DATATABLES)
    containsIndexyMultiSelectu: function (a, obj) {
        for (var i = 0; i < a.length; i++) {
            if (a[i] === obj) {
                return true;
            }
        }
        return false;
    },
    // Inicializuje klik na tlačítko reset filtry pro základní použití
    initResetButton: function (pocetInputu, dt) {
        $("#resetFiltrButton").on("click",
            function (e) {

                e.preventDefault();
                $(".ui-multiselect-none").trigger("click");

                for (var i = 0; i <= pocetInputu; i++) {
                    console.log($("#SEARCH_" + i));
                    $("#SEARCH_" + i).val("");
                    $("#SEARCH_" + i + "_pom").val("");
                    dt.column(i).search("");
                }

                for (var x = 0; x <= pocetInputu; x++) {
                    $("#SEARCH_" + x).trigger("change");
                    $("#SEARCH_" + x).trigger("keyup");
                }

                return;
            });
    },

    // Vrací aktuálně nastavenou kultůru
    getCurrentCultureName: function () {
        //Ask ASP.NET what culture we prefer, because we stuck it in a meta tag
        var data = $("meta[name='accept-language']").attr("content");
        //Tell jQuery to figure it out also on the client side.
        return data;
    }
}