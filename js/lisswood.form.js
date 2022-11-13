$(document).ready(function () {

    //Form.initSubmitAddProductBtn();
    Form.initGetAddProductPartialViewBtn();
});


var Form = {

    //// Register click button - add product
    initGetAddProductPartialViewBtn: function () {

        $(document).on("click",
            "#AddAnotherProductBtn",
            function () {
                // Submit product
                Form.getAddProductPartialView();
            });

    },


    //// Register click button - add product
    //initSubmitAddProductBtn: function () {

    //    $(document).on("click",
    //        "#submitProductBtn",
    //        function () {

    //            var btn = $(this);

    //            var formvalid = $("form").valid();

    //            console.log(formvalid, "formvalid");

    //            // TODO: Make product json object
    //            var product = {
    //                Name: $('#AddProduct_Name').val(),
    //                Description: $('#AddProduct_Description').val(),
    //                ProductFrameColor_Id: $('input[name="AddProduct.ProductFrameColor_Id"]:checked').val(),
    //                ProductSize_Id: $('input[name="AddProduct.ProductSize_Id"]:checked').val(),
    //                ProductLayout_Id: $('input[name="AddProduct.ProductLayout_Id"]:checked').val(),
    //            }

    //            var data = {
    //                Name: "",
    //                AddProduct: product
    //            }

    //            // Submit product
    //            Form.submitProduct(data);
    //        });

    //},


    //// Odešle product na server
    //submitProduct: function (data) {

    //    console.log(data);

    //    $.ajax({
    //        url: "/Home/AddProductToSession",
    //        type: "POST",
    //        data: { data: data },
    //        success: function (result) {

    //            console.log(result);

    //        },
    //        error: function (result) {

    //        }
    //    });

    //},

    // Product added
    addProductOnSuccess: function (response) {
        $('#AddProductContainer').html("");

        // Update product count
        console.log(response);
        $('#ProductCount').val(response.productCount);

        Form.getProductsPartialView();
    },

    addProductOnFailure: function () {
        return;
    },

    addProductOnComplete: function () {
        return;
    },

    // Vrací seznam přidaných produktů
    getProductsPartialView: function () {

        //GetProductPartialView
        var key = $('#orderForm #OrderKey').val();
        console.log(key);

        $.ajax({
            url: "/Home/GetProductsPartialView",
            type: "POST",
            data: { orderkey: key },
            success: function (result) {
                $('#Products').html(result);
            },
            error: function (result) {

            }
        });
    },

    // Vrací nový formulář na přidání nového produktu
    getAddProductPartialView() {
        //GetProductPartialView
        var key = $('#orderForm #OrderKey').val();
        console.log(key);

        $.ajax({
            url: "/Home/GetAddProductPartialView",
            type: "POST",
            data: { orderkey: key },
            success: function (result) {
                $('#AddProductContainer').html(result);
            },
            error: function (result) {

            }
        });
    }
}