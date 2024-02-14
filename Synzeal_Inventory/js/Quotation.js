var apiDomain = '';
var suggestedquotedetailsid = '';
var suggestedpricecompany = '';
var suggestedPriceSearchValue = '';
var delpw = 'sz@123$$$';
var securityCode = 'sz123$$$';
var multiProductArray = [];
var usdToInrPrice = 0;
var isApiSelected = false;
var isHistoryDataEnable = false;
var isValidateQuotePrice = false;
var Quote = {
    findPrice: function () {
        var qty = '';
        var mg1 = $(".clstoponemg").val();
        var mg2 = $(".clstoptwomg").val();
        var mg3 = $(".clstopthreemg").val();
        var mg4 = $(".clstopfourmg").val();
        if (mg1 !== "") {
            qty += mg1 + ',';
        }
        if (mg2 !== "") {
            qty += mg2 + ',';
        }
        if (mg3 !== "") {
            qty += mg3 + ',';
        }
        if (mg4 !== "") {
            qty += mg4 + ',';
        }
        var obj = {
            "id": $("#dvProInfo").find("#productId").val(),
            "quoteid": $("#QuoteId").val(),
            "qty": qty
        };
        if (qty === '') {
            toastr.error("Please entered Qty.");
            return false;
        }
        $.ajax({
            url: '/Form/findProductPrice',
            data: obj,
            type: 'POST',
            success: function (result) {
                if (result.type === "success") {
                    var qty = result.data[0].qty;

                    if (qty !== '') {
                        var loop = 1;
                        var splitpri = result.data[0].qty.split(',');
                        $(splitpri).each(function (i, v) {
                            if (v === '') {
                                return;
                            }
                            var prisplt = v.split('-');
                            if (loop === 1) {
                                $(".clstoponemg").val(prisplt[0].trim());
                                if (prisplt[1].trim() !== "0") {
                                    $(".clstoponeprice").val(prisplt[1].trim());
                                }
                            }
                            if (loop === 2) {
                                $(".clstoptwomg").val(prisplt[0].trim());
                                if (prisplt[1].trim() !== "0") {
                                    $(".clstoptwoprice").val(prisplt[1].trim());
                                }
                            }
                            if (loop === 3) {
                                $(".clstopthreemg").val(prisplt[0].trim());
                                if (prisplt[1].trim() !== "0") {
                                    $(".clstopthreeprice").val(prisplt[1].trim());
                                }
                            }
                            if (loop === 4) {
                                $(".clstopfourmg").val(prisplt[0].trim());
                                if (prisplt[1].trim() !== "0") {
                                    $(".clstopfourprice").val(prisplt[1].trim());
                                }
                            }
                            loop += 1;
                        });
                    }
                }
            }
        });

        isValidateQuotePrice = true;
    },

    quotecompleted: function () {
        $.ajax({
            url: '/Form/quotecompleted?id=' + $("#QuoteId").val(),
            data: {},
            type: 'GET',
            success: function (data) {
                toastr.success("You quotation is updated.");
            }
        });
    },
    //search product from quote Page
    searchProduct: function () {
        $("#dvProInfo").hide();
        isApiSelected = false
        $.ajax({
            url: '/Form/ProductDetailsBynamecasandcat?ProductName=' + $("#ProductName").val() /*+ '&casno=' + $("#CasNo").val() + '&catNo=' + $("#CATNo").val()*/,
            data: {},
            type: 'POST',
            success: function (data) {
                isApiSelected = data.isApi;
                if (data.type === 'success') {
                    var objDatas = data.data;
                    if (objDatas !== undefined && objDatas !== null && objDatas.length == 1) {
                        var obj = objDatas[0];
                        $("#txtchemicalname").html("<b>Chemical Name : </b>" + obj.ChemicalName);
                        $("#txtsynonym").html("<b>Synonym : </b>" + obj.Synonym);
                        $("#txtmolweight").html("<b>M.W. : </b>" + obj.MolecularWeight);
                        $("#txtmolformula").html("<b>M.F. : </b>" + obj.MolFormula);
                        if (obj.IsControlledSubstance === true) {
                            $("#txtmessagenote").html("<b style='color:red'>This product is Controlled Substance.</b>");
                        }
                        else {
                            $("#txtmessagenote").html("");
                        }
                        $("#productId").val(obj.Id);
                        $("#detFromDBProductName").val(obj.Name);
                        $("#detFromDBCasno").val(obj.CasNo);
                        $("#detFromDBCatno").val(obj.Sku);
                        $("#leadtime").val(obj.LeadTime);

                        if (obj.LeadTime === '' || obj.LeadTime === null || obj.LeadTime === undefined) {
                            $("#leadtime").val(obj.ProductInstockStatus);
                        }
                        if (obj.InventoryModel !== undefined && obj.InventoryModel !== null && obj.InventoryModel.length > 0) {
                            var strinv = "";
                            $(obj.InventoryModel).each(function (i, v) {
                                strinv += "<tr><td>" + v.BatchNo + "</td><td>" + v.Qty + "</td></tr>";
                            });
                            $("#detFromBatchDetails").html(strinv);
                        }
                        else {
                            var strinvs = "";
                            strinvs += "<tr><td colspan='2'>No Data Available</td></tr>";
                            $("#detFromBatchDetails").html(strinvs);
                        }
                        if (obj.DefaultPictureModel !== undefined && obj.DefaultPictureModel !== null) {
                            $("#detFromDBImage").attr('src', obj.DefaultPictureModel.ImageUrl);
                            $("#detFromDBImage").attr('data-catno', obj.Sku);
                            $("#proDBImagePath").val(obj.DefaultPictureModel.ImageUrl);
                        }

                        if (obj.Quotationreceived === null) {
                            obj.Quotationreceived = "";
                        }
                        if (obj.PurchaseComment === null) {
                            obj.PurchaseComment = "";
                        }

                        var strquotereceived = "<table class='table table-bordered table-striped dataTable no-footer' style='width:100%'><thead><tr><th>Quotation Received</th><th>Comment</th></tr></thead><tr><td>" + obj.Quotationreceived + "</td><td>" + obj.PurchaseComment + "</td></tr></table>";
                        strquotereceived += "<p>" + data.PurchaseSummary + "</p>"; 
                        $("#dbquotereceived").html(strquotereceived);

                        GetPurchaseExcelData(obj.CasNo, 'dbothersupplierdata');
                        $("#price").val(obj.INRPrice);

                        if (obj.PriceModel !== null) {
                            if (!obj.PriceModel.IsPriceApproved) {
                                var strpri = "";
                                strpri += "<tr><td colspan='4'>Price not approved yet.</td></tr>";
                                $("#bodypriceTbl").html(strpri);
                            }
                            else {
                                if ($('input[name=CountryType]:checked').val() === "Export") {
                                    //USD Record
                                    var pristrs = "";
                                    pristrs += "<tr><td>10 MG</td><td>" + isNull(obj.PriceModel.TenUSD) + "</td> <td>" + obj.PriceModel.LeadTime + "</td><td> <a href='javascript:void(0)' onclick='copyprice(&#39;usd&#39;, 10, &#39;" + obj.PriceModel.LeadTime + "&#39;," + isNull(obj.PriceModel.TenUSD) + ")'>Copy</a> </td></tr>";
                                    pristrs += "<tr><td>25 MG</td><td>" + isNull(obj.PriceModel.TwentyfiveUSD) + "</td> <td>" + obj.PriceModel.LeadTime + "</td><td><a href='javascript:void(0)' onclick='copyprice(&#39;usd&#39;, 25,  &#39;" + obj.PriceModel.LeadTime + "&#39;," + isNull(obj.PriceModel.TwentyfiveUSD) + ")'>Copy</a> </td></tr>";
                                    pristrs += "<tr><td>50 MG</td><td>" + isNull(obj.PriceModel.FiftyUSD) + "</td> <td>" + obj.PriceModel.LeadTime + "</td><td><a href='javascript:void(0)' onclick='copyprice(&#39;usd&#39;, 50,  &#39;" + obj.PriceModel.LeadTime + "&#39;," + isNull(obj.PriceModel.FiftyUSD) + ")'>Copy</a> </td></tr>";
                                    pristrs += "<tr><td>100 MG</td><td>" + isNull(obj.PriceModel.OnehundredUSD) + "</td> <td>" + obj.PriceModel.LeadTime + "</td><td><a href='javascript:void(0)' onclick='copyprice(&#39;usd&#39;, 100, &#39;" + obj.PriceModel.LeadTime + "&#39;," + isNull(obj.PriceModel.OnehundredUSD) + ")'>Copy</a> </td></tr>";
                                    pristrs += "<tr><td>250 MG</td><td>" + isNull(obj.PriceModel.TwohundredFiftyUSD) + "</td> <td>" + obj.PriceModel.LeadTime + "</td><td><a href='javascript:void(0)' onclick='copyprice(&#39;usd&#39;, 250, &#39;" + obj.PriceModel.LeadTime + "&#39;," + isNull(obj.PriceModel.TwohundredFiftyUSD) + ")'>Copy</a> </td></tr>";
                                    pristrs += "<tr><td>500 MG</td><td>" + isNull(obj.PriceModel.FivehundredUSD) + "</td> <td>" + obj.PriceModel.LeadTime + "</td><td><a href='javascript:void(0)' onclick='copyprice(&#39;usd&#39;, 500,  &#39;" + obj.PriceModel.LeadTime + "&#39;," + isNull(obj.PriceModel.FivehundredUSD) + ")'>Copy</a> </td></tr>";
                                    pristrs += "<tr><td>1000 MG</td><td>" + isNull(obj.PriceModel.OneThousandUSD) + "</td> <td>" + obj.PriceModel.LeadTime + "</td><td><a href='javascript:void(0)' onclick='copyprice(&#39;usd&#39;, 1000, &#39;" + obj.PriceModel.LeadTime + "&#39;," + isNull(obj.PriceModel.OneThousandUSD) + ")'>Copy</a> </td></tr>";
                                    $("#bodypriceTbl").html(pristrs);
                                }
                                else {
                                    //INR Record
                                    var pristr = "";
                                    pristr += "<tr><td>10 MG</td><td>" + isNull(obj.PriceModel.TenPrice) + "</td> <td>" + obj.PriceModel.LeadTime + "</td><td><a href='javascript:void(0)' onclick='copyprice(&#39;inr&#39;, 10,  &#39;" + obj.PriceModel.LeadTime + "&#39;," + isNull(obj.PriceModel.TenPrice) + ")'>Copy</a> </td></tr>";
                                    pristr += "<tr><td>25 MG</td><td>" + isNull(obj.PriceModel.TwentyFivePrice) + "</td> <td>" + obj.PriceModel.LeadTime + "</td><td><a href='javascript:void(0)' onclick='copyprice(&#39;inr&#39;, 25,  &#39;" + obj.PriceModel.LeadTime + "&#39;," + isNull(obj.PriceModel.TwentyFivePrice) + ")'>Copy</a> </td></tr>";
                                    pristr += "<tr><td>50 MG</td><td>" + isNull(obj.PriceModel.FiftyPrice) + "</td> <td>" + obj.PriceModel.LeadTime + "</td><td><a href='javascript:void(0)' onclick='copyprice(&#39;inr&#39;, 50,  &#39;" + obj.PriceModel.LeadTime + "&#39;," + isNull(obj.PriceModel.FiftyPrice) + ")'>Copy</a> </td></tr>";
                                    pristr += "<tr><td>100 MG</td><td>" + isNull(obj.PriceModel.HundredPrice) + "</td> <td>" + obj.PriceModel.LeadTime + "</td><td><a href='javascript:void(0)' onclick='copyprice(&#39;inr&#39;, 100, &#39;" + obj.PriceModel.LeadTime + "&#39;," + isNull(obj.PriceModel.HundredPrice) + ")'>Copy</a> </td></tr>";
                                    pristr += "<tr><td>250 MG</td><td>" + isNull(obj.PriceModel.TwoHundredPrice) + "</td> <td>" + obj.PriceModel.LeadTime + "</td><td><a href='javascript:void(0)' onclick='copyprice(&#39;inr&#39;, 250, &#39;" + obj.PriceModel.LeadTime + "&#39;," + isNull(obj.PriceModel.TwoHundredPrice) + ")'>Copy</a> </td></tr>";
                                    pristr += "<tr><td>500 MG</td><td>" + isNull(obj.PriceModel.FivehundredPrice) + "</td> <td>" + obj.PriceModel.LeadTime + "</td><td><a href='javascript:void(0)' onclick='copyprice(&#39;inr&#39;, 500, &#39;" + obj.PriceModel.LeadTime + "&#39;," + isNull(obj.PriceModel.FivehundredPrice) + ")'>Copy</a> </td></tr>";
                                    pristr += "<tr><td>1000 MG</td><td>" + isNull(obj.PriceModel.OneThousandPrice) + "</td> <td>" + obj.PriceModel.LeadTime + "</td><td><a href='javascript:void(0)' onclick='copyprice(&#39;inr&#39;, 1000,  &#39;" + obj.PriceModel.LeadTime + "&#39;," + isNull(obj.PriceModel.OneThousandPrice) + ")'>Copy</a> </td></tr>";
                                    $("#bodypriceTbl").html(pristr);
                                }
                            }
                        }
                        else {
                            var strpri = "";
                            strpri += "<tr><td colspan='3'>No Data Available</td></tr>";
                            $("#bodypriceTbl").html(strpri);
                        }

                        $("#dvProInfo").show();
                    }
                    else if (objDatas.length > 1) {
                        multiProductArray = [];
                        var strHTML = "<table width='100%' class='table table-bordered table-striped'><thead><tr><th>Product Name</th><th>CAS No</th><th>CAT No</th><th>Batch No.</th><th></th></tr></thead>";
                        $(objDatas).each(function (i, v) {
                            var arrayList = [];
                            if (v.InventoryModel != null && v.InventoryModel != undefined && v.InventoryModel.length > 0) {
                                $(v.InventoryModel).each(function (k, z) {
                                    arrayList.push(z.BatchNo + "(" + z.Qty + ")");
                                });
                            }
                            var inventoryStr = arrayList.join();

                            strHTML += "<tr><td>" + v.Name + "</td><td>" + v.CasNo + "</td><td>" + v.Sku + "</td><td>" + inventoryStr + "</td><td><a href='javascript:void(0)' onclick=Quote.selectthisProduct(" + v.Id + ")>Select</a></td></tr>";
                            multiProductArray.push(v);
                        });
                        strHTML += "</table>";
                        $('.modal-body').html(strHTML);
                        $('#myModal').modal({ show: true });
                        $(".modal-dialog").css("width", "1080px");
                    }
                }
            }
        });
    },

    selectthisProduct: function (id) {
        multiProductArray.forEach(function (e) {
            if (id == e.Id) {
                var obj = e;
                $("#productId").val(obj.Id);
                $("#detFromDBProductName").val(obj.Name);
                $("#detFromDBCasno").val(obj.CasNo);
                $("#detFromDBCatno").val(obj.Sku);
                $("#txtchemicalname").html("<b>Chemical Name : </b>" + obj.ChemicalName);
                $("#txtsynonym").html("<b>Synonym : </b>" + obj.Synonym);
                $("#txtmolweight").html("<b>M.W. : </b>" + obj.MolecularWeight);
                $("#txtmolformula").html("<b>M.F. : </b>" + obj.MolFormula);
                if (obj.InventoryModel !== undefined && obj.InventoryModel !== null && obj.InventoryModel.length > 0) {
                    var strinv = "";
                    $(obj.InventoryModel).each(function (i, v) {
                        strinv += "<tr><td>" + v.BatchNo + "</td><td>" + v.Qty + "</td></tr>";
                    });
                    $("#detFromBatchDetails").html(strinv);
                    $("#leadtime").val("In Stock");
                }
                else {
                    var strinvs = "";
                    strinvs += "<tr><td colspan='2'>No Data Available</td></tr>";
                    $("#detFromBatchDetails").html(strinvs);
                }
                if (obj.DefaultPictureModel !== undefined && obj.DefaultPictureModel !== null) {
                    $("#detFromDBImage").attr('src', obj.DefaultPictureModel.ImageUrl);
                    $("#detFromDBImage").attr('data-catno', obj.Sku);
                    $("#proDBImagePath").val(obj.DefaultPictureModel.ImageUrl);
                }

                var sameAsArray = [];
                multiProductArray.forEach(function (q) {
                    if (id !== q.Id && sameAsArray.indexOf(q.Sku) === -1) {
                        sameAsArray.push(q.Sku);
                    }
                });

                if (sameAsArray.length > 0 && !isApiSelected) {
                    $("#productremark").val("Also Known as " + sameAsArray.join(', '));
                }

                var strquotereceived = "<table class='table table-bordered table-striped dataTable no-footer' style='width:100%'><thead><tr><th>Quotation Received</th><th>Comment</th></tr></thead><tr><td>" + obj.Quotationreceived + "</td><td>" + obj.PurchaseComment + "</td></tr></table>";
                strquotereceived += "<p>" + obj.PurchaseSummary + "</p>"; 
                $("#dbquotereceived").html(strquotereceived);

                GetPurchaseExcelData(obj.CasNo, 'dbothersupplierdata');

                $("#dvProInfo").show();
                $('.modal-body').html("");
                $('#myModal').modal('hide');
            }
        });
    },

    findProduct: function () {
        $("#dvFindProductList").html("<div style='text-align: center;'><img src='/images/loader.gif' style='height: 170px;'/></div>");
        $.ajax({
            url: '/Form/ProductListBycategory?ProductName=' + $("#ProductName").val(),
            data: {},
            type: 'POST',
            success: function (data) {
                if (data.type === 'success') {
                    $("#dvFindProductList").html(data.html);
                }
            }
        });
    },

    GenerateReport: function () {
        var compId = $("#companyId").val();
        window.location.href = '/Form/PrintReportPDF?companyId=' + compId;
    },

    GenerateExcelReport: function () {
        var compId = $("#companyId").val();
        window.location.href = '/Form/GenerateReportExcel?companyId=' + compId;
    },

    addFindProducts: function (id) {
        var selected = [];
        selected.push(id);

        $.ajax({
            type: "POST",
            url: "/Form/saveFindProductData",
            dataType: "json",
            traditional: true,
            data: { ids: selected, uniqueId: $("#UniqueId").val(), quoteId: $("#QuoteId").val(), companyId: $("#companyId").val() },
            success: function (result) {
                toastr.success("You have added new product.");
                // Quote.tempProductListForQuote();
                Quote.getProductListForQuote($("#QuoteId").val());
                $(".clsfindproduct").each(function () {
                    if ($(this).is(":checked")) {
                        $(this).prop('checked', false);
                    }
                });
            }
        });
    },

    Park: function (id) {

        var str = "<div class='row'><div class='col-md-12'><p>Please enter park reason:<p> <textarea id='txtparkreason' style='width:100%'></textarea></div></div>";
        swal({
            title: "Are you sure want to park it?",
            //text: str,
            //html: true,
            type: "warning", customClass: 'swalwide',
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, Added it!",
            cancelButtonText: "No, cancel!",
            closeOnConfirm: false,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    //var parkreason = $('#txtparkreason').val();
                    //if (parkreason != '' && parkreason.trim() !== '') {
                    var parkreason = "";
                    $.ajax({
                        type: "GET",
                        url: "/Form/ParkQuotationdetail?status=" + true + "&id=" + id + "&parkreason=" + parkreason,
                        dataType: "json",
                        traditional: true,
                        data: {},
                        success: function (result) {
                            toastr.success("You have added new product.");
                            //window.location.reload();
                            window.location.href = "/Form/TodayDashboard";
                        }
                    });
                    //}
                    //else {
                    //    toastr.error("Please enter park reason.");
                    //    return false;
                    //}
                } else {
                    toastr.error("Your added quotation is safe :)");
                }
            });

    },

    unPark: function (id) {
        $.ajax({
            type: "GET",
            url: "/Form/ParkQuotationdetail?status=" + false + "&id=" + id,
            dataType: "json",
            traditional: true,
            data: {},
            success: function (result) {
                toastr.success("You have added new product.");
                window.location.reload();
            }
        });
    },

    saveFindProducts: function () {

        if ($("#CompanyId").val() !== "" && $("#CompanyId").val() !== "0") {
            $.ajax({
                type: "POST",
                url: "/Form/CheckDuplicateQuote?companyId=" + $("#CompanyId").val() + '&catno=' + $("#detFromDBCatno").val(),
                data: {},
                success: function (results) {

                    if (results.type === "available") {
                        var str = "<div class='row'><div class='col-md-2'><b>Quote Id : </b> </div><div class='col-md-10' style='text-align:left'><a href='http://spms.synzeal.com/Form/Quote/" + results.quotation.Id + "'>" + results.quotation.Ref + "</a></div>";
                        str += "<div class='col-md-2'><b>Company Name : </b> </div><div class='col-md-10'  style='text-align:left'>" + results.quotation.CompanyName + "</div>";
                        str += "<div class='col-md-2'><b>Email Address : </b> </div><div class='col-md-10'  style='text-align:left'>" + results.quotation.EmailAddress + "</div></div>";
                        str += "<table><tr><th>Product Name</th><th>CAS No</th><th>CAT No</th><th>Price</th><th>Lead Time</th></tr>";
                        $(results.quotationdetail).each(function (i, v) {
                            str += "<tr><td>" + v.ProductName + "</td><td><a target='_blank' href='https://synzeal.com/search?q=" + v.CASNo + "' >" + v.CASNo + "</a></td><td>" + v.CATNo + "</td><td>" + v.Price + "</td><td>" + v.LeadTime + "</td></tr>";
                        });
                        str += "</table>";
                        swal({
                            title: "Are you sure want to create it?",
                            text: str,
                            html: true,
                            //text: "You have already created quotation for this company.",
                            type: "warning",
                            customClass: 'swalwide',
                            showCancelButton: true,
                            confirmButtonClass: "btn-danger",
                            confirmButtonText: "Yes, Added it!",
                            cancelButtonText: "No, cancel!",
                            closeOnConfirm: true,
                            closeOnCancel: true
                        },
                            function (isConfirm) {
                                if (isConfirm) {
                                    var selected = [];
                                    $(".clsfindproduct").each(function () {
                                        if ($(this).is(":checked")) {
                                            selected.push($(this).val());
                                        }
                                    });

                                    $.ajax({
                                        type: "POST",
                                        url: "/Form/saveFindProductData",
                                        dataType: "json",
                                        traditional: true,
                                        data: { ids: selected, uniqueId: $("#UniqueId").val(), quoteId: $("#QuoteId").val(), companyId: $("#companyId").val() },
                                        success: function (result) {

                                            toastr.success("You have added new product.");
                                            //Quote.tempProductListForQuote();
                                            Quote.getProductListForQuote($("#QuoteId").val());
                                            $(".clsfindproduct").each(function () {
                                                if ($(this).is(":checked")) {
                                                    $(this).prop('checked', false);
                                                }
                                            });
                                        }
                                    });
                                } else {
                                    toastr.error("Your added quotation is safe :)");
                                }
                            });
                    }
                    else {

                        var selected = [];
                        $(".clsfindproduct").each(function () {
                            if ($(this).is(":checked")) {
                                selected.push($(this).val());
                            }
                        });

                        $.ajax({
                            type: "POST",
                            url: "/Form/saveFindProductData",
                            dataType: "json",
                            traditional: true,
                            data: { ids: selected, uniqueId: $("#UniqueId").val(), quoteId: $("#QuoteId").val(), companyId: $("#companyId").val() },
                            success: function (result) {
                                toastr.success("You have added new product.");
                                Quote.tempProductListForQuote();

                                $(".clsfindproduct").each(function () {
                                    if ($(this).is(":checked")) {
                                        $(this).prop('checked', false);
                                    }
                                });
                            }
                        });
                    }
                }
            });
        }
        else {

            var selected = [];
            $(".clsfindproduct").each(function () {
                if ($(this).is(":checked")) {
                    selected.push($(this).val());
                }
            });

            $.ajax({
                type: "POST",
                url: "/Form/saveFindProductData",
                dataType: "json",
                traditional: true,
                data: { ids: selected, uniqueId: $("#UniqueId").val(), quoteId: $("#QuoteId").val(), companyId: $("#companyId").val() },
                success: function (result) {
                    toastr.success("You have added new product.");
                    Quote.tempProductListForQuote();

                    $(".clsfindproduct").each(function () {
                        if ($(this).is(":checked")) {
                            $(this).prop('checked', false);
                        }
                    });
                }
            });
        }
    },

    getProductDetails: function () {
        isValidateQuotePrice = false;
        Quote.getPreviousInfoFromDB();
        Quote.searchProduct();
    },

    //get previous quote information from database
    getPreviousInfoFromDB: function (isApiData = false, catNo = '') {

        $("#dvPreviousProductList").html("<div style='text-align: center;'><img src='/images/loader.gif' style='height: 170px;'/></div>");
        var pName = $("#ProductName").val();
        if (catNo !== '') {
            pName = catNo;
        }
        $.ajax({
            url: '/Form/getPreviousInfoFromDB?ProductName=' + pName + '&casno=' + $("#CasNo").val() + '&catNo=' + $("#CATNo").val() + '&company=' + $("#CompanyId").val() + '&QuoteId=' + $("#QuoteId").val() + '&isApi=' + isApiData,
            data: {},
            async: true,
            cache: true,
            type: 'POST',
            success: function (data) {
                $("#dvPreviousProductList").html(data);

                if ($('input[name=CountryType]:checked').val() === "Export") {
                    $("#filtype").val("Export");
                }
                else {
                    $("#filtype").val("Domestic");
                }

                $("#filtype").trigger('change');

            }
        });
    },

    addMultipleProduct: function () {
        if ($("#ProductName").val() === "") {
            toastr.error("Please enter catalog number");
            return false;
        }
        $.ajax({
            url: '/Form/addMultipleProduct?catno=' + $("#ProductName").val() + '&company=' + $("#CompanyId").val() + '&QuoteId=' + $("#QuoteId").val(),
            data: {},
            type: 'POST',
            success: function (data) {
                Quote.getProductListForQuote($("#QuoteId").val());

                //Quote.tempProductListForQuote();
                toastr.success("All catalog number added.");
            }
        });
    },

    getPreviousQuoteInformationByCatNo: function (catNo, casNo, quoteId = 0, quotedetailsid = 0, productId = 0) {
        if (catNo === '') {
            //swal("Oops", "Catalog number not found", "error");
            //return false;
            catNo = casNo;
            if (catNo === '') {
                toastr.error("Catalog number not found");
                return false;
            }
        }

        $(".clshistory").hide();
        setTimeout(function () {
            $(".clshistory").show();
        }, 4000);

        suggestedpricecompany = $("#CompanyId").val();
        suggestedPriceSearchValue = catNo;
        suggestedquotedetailsid = quotedetailsid;

        $("#dvPreviousProductList").html("<div style='text-align: center;'><img src='/images/loader.gif' style='height: 170px;'/></div>");

        // Modified Logic and move into paging level
        $.ajax({
            url: '/Form/ValidateQuoteHistory?ProductName=' + catNo + '&company=' + $("#CompanyId").val() + '&quoteid=' + quoteId + '&quotedetailsid=' + quotedetailsid + '&productId=' + productId,
            data: {},
            type: 'POST',
            async: true,
            success: function (data) {
                if (data.success) {
                    if (data.redicon) {
                        $('.modal-body').html("<div style='text-align:center'><b style='font-size: 20px'>CAT No:" + catNo +" </b><br> <p style='font-size: 20px'>Check Price - Last 5 Quotation did not fetch Purchase Order</p> </div>");
                        $('#myModal').modal({ show: true });
                        $(".modal-dialog").css("width", "600px");
                        $(".modal-dialog").find(".modal-body").css('background', '#effcff'); 
                        $(".modal-dialog").find(".modal-header").css('display', 'table-column'); 
                    }
                    if (data.redtenicon) {
                        $('.modal-body').html("<div style='text-align:center'><b style='font-size: 20px'>CAT No:" + catNo + " </b><br> <p style='font-size: 20px'>Re-visit  Price - Last 10 Quotation did not fetch Purchase Order</p> </div>");
                        $('#myModal').modal({ show: true });
                        $(".modal-dialog").css("width", "600px");
                        $(".modal-dialog").find(".modal-body").css('background', '#effcff');
                        $(".modal-dialog").find(".modal-header").css('display', 'table-column'); 
                    }

                    if (data.firsttime) {
                        $('.modal-body').html("<div style='text-align:center'><b style='font-size: 20px'>CAT No:" + catNo + " </b><br> <p style='font-size: 20px'>This is your first quotation - Be careful in pricing</p> </div>");
                        $('#myModal').modal({ show: true });
                        $(".modal-dialog").css("width", "600px");
                        $(".modal-dialog").find(".modal-body").css('background', '#effcff');
                        $(".modal-dialog").find(".modal-header").css('display', 'table-column'); 
                    } 

                    if (data.noporeceived) {
                        $('.modal-body').html("<div style='text-align:center'><b style='font-size: 20px'>CAT No:" + catNo + " </b><br> <p style='font-size: 20px'>No PO received for this product</p> </div>");
                        $('#myModal').modal({ show: true });
                        $(".modal-dialog").css("width", "600px");
                        $(".modal-dialog").find(".modal-body").css('background', '#effcff');
                        $(".modal-dialog").find(".modal-header").css('display', 'table-column'); 
                    } 

                }
            }
        });


        $.ajax({
            url: '/Form/getPreviousInfoFromDB?ProductName=' + catNo + '&casno=&catNo=&company=' + $("#CompanyId").val() + '&QuoteId=' + quoteId + '&quotedetailsid=' + quotedetailsid + '&productIds=' + productId,
            data: {},
            type: 'POST',
            async: true,
            success: function (data) {
                $("#dvPreviousProductList").html(data);

                if ($('input[name=CountryType]:checked').val() === "Export") {
                    $("#filtype").val("Export");
                }
                else {
                    $("#filtype").val("Domestic");
                }

                $("#filtype").trigger('change');
            }
        });


        $("#dvProInfo").hide();
        $.ajax({
            url: '/Form/ProductDetailsBynamecasandcat?ProductName=' + catNo /*+ '&casno=' + $("#CasNo").val() + '&catNo=' + $("#CATNo").val()*/,
            data: {},
            type: 'POST',
            async: true,
            cache: true,
            dataType: "json",
            success: function (data) {
                if (data.type === 'success') {
                    var objDatas = data.data;
                    if (objDatas !== undefined && objDatas !== null && objDatas.length == 1) {
                        var obj = objDatas[0];
                        $("#productId").val(obj.Id);
                        $("#txtchemicalname").html("<b>Chemical Name : </b>" + obj.ChemicalName);
                        $("#txtsynonym").html("<b>Synonym : </b>" + obj.Synonym);
                        $("#txtmolweight").html("<b>M.W. : </b>" + obj.MolecularWeight);
                        $("#txtmolformula").html("<b>M.F. : </b>" + obj.MolFormula);

                        $("#detFromDBProductName").val(obj.Name);

                        $("#detFromDBCasno").val(obj.CasNo);
                        $("#detFromDBCatno").val(obj.Sku);
                        $("#leadtime").val(obj.LeadTime);
                        if (obj.LeadTime === '' || obj.LeadTime === null || obj.LeadTime === undefined) {
                            $("#leadtime").val(obj.ProductInstockStatus);
                        }
                        if (obj.InventoryModel !== undefined && obj.InventoryModel !== null && obj.InventoryModel.length > 0) {
                            var strinv = "";
                            $(obj.InventoryModel).each(function (i, v) {
                                strinv += "<tr><td>" + v.BatchNo + "</td><td>" + v.Qty + "</td></tr>";
                            });
                            $("#detFromBatchDetails").html(strinv);
                        }
                        else {
                            var strinvs = "";
                            strinvs += "<tr><td colspan='2'>No Data Available</td></tr>";
                            $("#detFromBatchDetails").html(strinvs);
                        }
                        if (obj.DefaultPictureModel !== undefined && obj.DefaultPictureModel !== null) {
                            $("#detFromDBImage").attr('src', obj.DefaultPictureModel.ImageUrl);
                            $("#detFromDBImage").attr('data-catno', obj.Sku);
                            $("#proDBImagePath").val(obj.DefaultPictureModel.ImageUrl);
                        }
                        if (obj.Quotationreceived === null) {
                            obj.Quotationreceived = "";
                        }
                        if (obj.PurchaseComment === null) {
                            obj.PurchaseComment = "";
                        }
                        var strquotereceived = "<table class='table table-bordered table-striped dataTable no-footer' style='width:100%'><thead><tr><th>Quotation Received</th><th>Comment</th></tr></thead><tr><td>" + obj.Quotationreceived + "</td><td>" + obj.PurchaseComment + "</td></tr></table>";
                        strquotereceived += "<p>" + data.PurchaseSummary +"</p>"; 
                        $("#dbquotereceived").html(strquotereceived);

                        GetPurchaseExcelData(obj.CasNo, 'dbothersupplierdata');

                        $("#dvProInfo").show();
                    }
                    else if (objDatas.length > 1) {
                        multiProductArray = [];
                        var strHTML = "<table width='100%' class='table table-bordered table-striped'><thead><tr><th>Product Name</th><th>CAS No</th><th>CAT No</th><th>Batch No.</th><th></th></tr></thead>";
                        $(objDatas).each(function (i, v) {
                            var arrayList = [];
                            if (v.InventoryModel != null && v.InventoryModel != undefined && v.InventoryModel.length > 0) {
                                $(v.InventoryModel).each(function (k, z) {
                                    arrayList.push(z.BatchNo + "(" + z.Qty + ")");
                                });
                            }
                            var inventoryStr = arrayList.join();

                            strHTML += "<tr><td>" + v.Name + "</td><td>" + v.CasNo + "</td><td>" + v.Sku + "</td><td>" + inventoryStr + "</td><td><a href='javascript:void(0)' onclick=Quote.selectthisProduct(" + v.Id + ")>Select</a></td></tr>";
                            multiProductArray.push(v);
                        });
                        strHTML += "</table>";
                        $('.modal-body').html(strHTML);
                        $('#myModal').modal({ show: true });
                        $(".modal-dialog").css("width", "1080px");
                    }
                }
            }
        });

    },

    checkDuplicateQuote: function () {
        $.ajax({
            type: "POST",
            url: "/Form/CheckDuplicateQuote?companyId=" + $("#CompanyId").val(),
            data: {},
            success: function (results) {

            }
        });
    },

    //add product in quotation from db
    addFromDBProduct: function () {
        isValidateQuotePrice = true;
        if (isValidateQuotePrice === false) {
            toastr.error("Please validate price.");
            return false;
        }

        var pricearr = [];
        var currency = $('input[name=CountryType]:checked').val() === 'Domestic' ? "INR" : "USD";
        if ($(".clstoponemg").val() !== '') {
            var str = $(".clstoponemg").val() + ' @' + $(".clstoponeprice").val() + ' ' + currency;
            if ($(".clstoponesize").val() !== '') {
                str += " X " + $(".clstoponesize").val();
            }
            pricearr.push(str);
        }
        if ($(".clstoptwomg").val() !== '') {
            var str = $(".clstoptwomg").val() + ' @' + $(".clstoptwoprice").val() + ' ' + currency;
            if ($(".clstoptwosize").val() !== '') {
                str += " X " + $(".clstoptwosize").val();
            }
            pricearr.push(str);
        }
        if ($(".clstopthreemg").val() !== '') {
            var str = $(".clstopthreemg").val() + ' @' + $(".clstopthreeprice").val() + ' ' + currency;
            if ($(".clstopthreesize").val() !== '') {
                str += " X " + $(".clstopthreesize").val();
            }
            pricearr.push(str);
        }
        if ($(".clstopfourmg").val() !== '') {
            var str = $(".clstopfourmg").val() + ' @' + $(".clstopfourprice").val() + ' ' + currency;
            if ($(".clstopfoursize").val() !== '') {
                str += " X " + $(".clstopfoursize").val();
            }
            pricearr.push(str);
        }
        var price = pricearr.join();

        if ($("#CompanyId").val() !== "" && $("#CompanyId").val() !== "0") {

            saveallalreadyProduct($('#IsCOA').is(":checked"));

            $.ajax({
                type: "POST",
                url: "/Form/CheckDuplicateQuote?companyId=" + $("#CompanyId").val() + '&catno=' + $("#detFromDBCatno").val(),
                data: {},
                success: function (results) {

                    if (results.type === "available") {
                        var str = "<div class='row'><div class='col-md-2'><b>Quote Id : </b> </div><div class='col-md-10' style='text-align:left'><a target='_blank' href='/Form/Quote/" + results.quotation.Id + "' >" + results.quotation.Ref + "</a></div>";
                        str += "<div class='col-md-2'><b>Company Name : </b> </div><div class='col-md-10'  style='text-align:left'>" + results.quotation.CompanyName + "</div>";
                        str += "<div class='col-md-2'><b>Email Address : </b> </div><div class='col-md-10'  style='text-align:left'>" + results.quotation.EmailAddress + "</div></div>";
                        str += "<table><tr><th>Product Name</th><th>CAS No</th><th>CAT No</th><th>Price</th><th>Lead Time</th></tr>";
                        $(results.quotationdetail).each(function (i, v) {
                            var pri = v.Price;
                            if (v.Price == null) {
                                pri = "";
                            }
                            str += "<tr><td>" + v.ProductName + "</td><td>" + v.CASNo + "</td><td><a target='_blank' href='https://synzeal.com/search?q=" + v.CATNo + "' >" + v.CATNo + "</a></td><td>" + v.Price + "</td><td>" + v.LeadTime + "</td></tr>";
                        });
                        str += "</table>";
                        swal({
                            title: "Are you sure want to create it?",
                            text: str,
                            html: true,
                            //text: "You have already created quotation for this company.",
                            type: "warning", customClass: 'swalwide',
                            showCancelButton: true,
                            confirmButtonClass: "btn-danger",
                            confirmButtonText: "Yes, Added it!",
                            cancelButtonText: "No, cancel!",
                            closeOnConfirm: true,
                            closeOnCancel: true
                        },
                            function (isConfirm) {
                                if (isConfirm) {

                                    var passData = {
                                        'QuoteId': $("#QuoteId").val(),
                                        'ProductId': $("#productId").val(),
                                        'ProductName': $("#detFromDBProductName").val(),
                                        'CASNo': $("#detFromDBCasno").val(),
                                        'UniqueId': $("#UniqueId").val(),
                                        'Price': price,
                                        'LeadTime': $("#leadtime").val(),
                                        'ImagePath': $("#proDBImagePath").val(),
                                        'CATNo': $("#detFromDBCatno").val(),
                                        'Productremark': $("#productremark").val(),
                                        'isClubQuote': $("#isClubQuote").val(),
                                        'CompanyId': $("#CompanyId").val()
                                    };
                                    $.ajax({
                                        type: "POST",
                                        url: "/Form/SaveProductInQuoteDetails",
                                        data: passData,
                                        async: true,
                                        success: function (results) {
                                            toastr.success("You have added new product.");
                                            Quote.getProductListForQuote($("#QuoteId").val());
                                            $("#dvProInfo").hide();
                                            $("#productId").val("0");
                                            $("#detFromDBProductName").val("");
                                            $("#detFromDBCasno").val("");
                                            $("#detFromDBCatno").val("");
                                            $("#price").val("");
                                            $("#leadtime").val("");
                                            $("#productremark").val("");

                                            var strinv = "";
                                            strinv += "<tr><td colspan='2'>No Data Available</td></tr>";
                                            $("#detFromBatchDetails").html(strinv);
                                            $("#detFromDBImage").attr('src', "");
                                            $("#proDBImagePath").val("");
                                        }
                                    });
                                } else {
                                    toastr.error("Your added quotation is safe");
                                }
                            });
                    }
                    else {

                        var passData = {
                            'QuoteId': $("#QuoteId").val(),
                            'ProductId': $("#productId").val(),
                            'ProductName': $("#detFromDBProductName").val(),
                            'CASNo': $("#detFromDBCasno").val(),
                            'UniqueId': $("#UniqueId").val(),
                            'Price': price,
                            'LeadTime': $("#leadtime").val(),
                            'ImagePath': $("#proDBImagePath").val(),
                            'CATNo': $("#detFromDBCatno").val(),
                            'Productremark': $("#productremark").val(),
                            'isClubQuote': $("#isClubQuote").val(),
                            'CompanyId': $("#CompanyId").val()
                        };
                        $.ajax({
                            type: "POST",
                            url: "/Form/SaveProductInQuoteDetails",
                            data: passData,
                            async: true,
                            success: function (results) {

                                toastr.success("You have added new product.");
                                Quote.getProductListForQuote($("#QuoteId").val());
                                $("#dvProInfo").hide();
                                $("#productId").val("0");
                                $("#detFromDBProductName").val("");
                                $("#detFromDBCasno").val("");
                                $("#detFromDBCatno").val("");

                                $("#price").val("");
                                $("#leadtime").val("");
                                $("#productremark").val("");

                                var strinv = "";
                                strinv += "<tr><td colspan='2'>No Data Available</td></tr>";
                                $("#detFromBatchDetails").html(strinv);
                                $("#detFromDBImage").attr('src', "");
                                $("#proDBImagePath").val("");
                            }
                        });
                    }
                }
            });
        }
        else {

            var passData = {
                'ProductId': $("#productId").val(),
                'ProductName': $("#detFromDBProductName").val(),
                'CASNo': $("#detFromDBCasno").val(),
                'UniqueId': $("#UniqueId").val(),
                'Price': price,
                'LeadTime': $("#leadtime").val(),
                'ImagePath': $("#proDBImagePath").val(),
                'CATNo': $("#detFromDBCatno").val(),
                'Productremark': $("#productremark").val(),
                'isClubQuote': $("#isClubQuote").val(),
                'CompanyId': $("#CompanyId").val()
            };
            $.ajax({
                type: "POST",
                url: "/Form/SaveProductInTempQuote",
                data: passData,
                success: function (results) {
                    toastr.success("You have added new product.");
                    Quote.tempProductListForQuote();
                    $("#dvProInfo").hide();
                    $("#productId").val("0");
                    $("#detFromDBProductName").val("");
                    $("#detFromDBCasno").val("");
                    $("#detFromDBCatno").val("");

                    $("#price").val("");
                    $("#leadtime").val("");
                    $("#productremark").val("");

                    var strinv = "";
                    strinv += "<tr><td colspan='2'>No Data Available</td></tr>";
                    $("#detFromBatchDetails").html(strinv);
                    $("#detFromDBImage").attr('src', "");
                    $("#proDBImagePath").val("");
                }
            });
        }
    },
    //add Scientist Data
    addScientistStatusList: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#name").val() === "") {
            $("#name").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        $.ajax({
            type: "POST",
            url: "/Form/ScientistStatusListData?name=" + $("#name").val() + "&id=" + $("#compid").val(),
            data: {},
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Status updated.");
                setTimeout(function () { window.location.href = "/Form/ScientistStatusList"; }, 2000);

            }
        });
    },

    //add sample Reason data
    addSampleReasonList: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#name").val() === "") {
            $("#name").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        $.ajax({
            type: "POST",
            url: "/Form/SampleReasonListData?name=" + $("#name").val() + "&id=" + $("#compid").val(),
            data: {},
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Status updated.");
                setTimeout(function () { window.location.href = "/Form/SampleReasonList"; }, 2000);

            }
        });
    },

    //add user management data
    addUsermanagementList: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var model = {
            Id: $("#Id").val(),
            UserId: $("#UserId").val(),
            CompanyIds: $("#CompanyIds").val().join(','),
            IsPaymentShow: $("#IsPaymentShow").is(":checked"),
            IsNotification: $("#IsNotification").is(":checked"),
            isNewsLetter: $("#isNewsLetter").is(":checked"),
            DefaultCurrencyId: $("#DefaultCurrencyId").val()
        };
        $.ajax({
            type: "POST",
            url: "/Form/UserManagementData",
            data: model,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("User access updated.");
                setTimeout(function () { window.location.href = "/Form/UserManagement"; }, 2000);

            }
        });
    },

    //add payment term data
    addPaymentTermList: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#name").val() === "") {
            $("#name").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        $.ajax({
            type: "POST",
            url: "/Form/AddpaymentTermData?name=" + $("#name").val() + "&day=" + $("#Day").val() + "&note=" + $("#Note").val() + "&id=" + $("#id").val(),
            data: {},
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Payment terms updated.");
                setTimeout(function () { window.location.href = "/Form/PaymentTerm"; }, 2000);

            }
        });
    },

    //add Physical state Data
    //add Courier Data
    addHSNCodeData: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#name").val() === "") {
            $("#name").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        $.ajax({
            type: "POST",
            url: "/Form/AddHSNCodeData?name=" + $("#name").val() + "&id=" + $("#compid").val(),
            data: {},
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("HSN Code updated.");
                setTimeout(function () { window.location.href = "/Form/HSNCodeList"; }, 2000);

            }
        });
    },

    addGeographicalData: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#name").val() === "") {
            $("#name").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        $.ajax({
            type: "POST",
            url: "/Form/AddGeographicalData?name=" + $("#name").val() + "&id=" + $("#compid").val(),
            data: {},
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Geographical updated.");
                setTimeout(function () { window.location.href = "/Form/GeographicalList"; }, 2000);

            }
        });
    },

    addreservedqtydata: function () {
        
        var radiovalue = $('input[name="namebatch"]:checked').val();
        if (radiovalue === "" || radiovalue === undefined) {
            toastr.error("Please select batch number.");
            return false;
        }
        if ($("#txtresqty").val() === "") {
            toastr.error("Please enter reserved qty.");
            return false;
        }
        if ($("#txtReason").val() === "") {
            toastr.error("Please enter reason.");
            return false;
        }

        $.ajax({
            type: "POST",
            url: "/Form/AddResearvedQtyData?batchid=" + radiovalue + "&qty=" + $("#txtresqty").val() + "&reason=" + $("#txtReason").val() + "&refdata=" + $("#txtrefdata").val(),
            data: {},
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Reserved Qty updated.");
                $('#invoiceQueryModal').modal('hide');
            }
        });
    },

    //add Courier Data
    addCourierData: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#name").val() === "") {
            $("#name").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        $.ajax({
            type: "POST",
            url: "/Form/AddCourierData?name=" + $("#name").val() + "&id=" + $("#compid").val(),
            data: {},
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Courier updated.");
                setTimeout(function () { window.location.href = "/Form/CourierList"; }, 2000);

            }
        });
    },

    addAdditionalData: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#Test").val() === "") {
            $("#Test").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        var model = {
            Id: $("#testid").val(), 
            Test: $("#Test").val(),
            INR: $("#INR").val(),
            USD: $("#USD").val(),
            GBP: $("#GBP").val(),
            EURO: $("#EURO").val(),
            DisplayOrder: $("#DisplayOrder").val(),
            MakeDefault: $("#MakeDefault").is(":checked")
        };

        $.ajax({
            type: "POST",
            url: "/Form/AddAdditionalTestdata",
            data: model,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Additional Data updated.");
                setTimeout(function () { window.location.href = "/Form/AdditionalTestList"; }, 2000);

            }
        });
    },

    getCompInformation: function (id) {
        $.ajax({
            type: "POST",
            url: "/Form/GetCompanyInformationByCompanyId?id=" + id,
            data: {},
            success: function (results) {
                if (results != null) {
                    if (results.IsBlockCompany) {
                        toastr.error("Your selected company is blocked. You can not make a quotation.");
                        $("#CompanyId").val("");
                        return false;
                    }
                    $("#TermsId").val(results.TermsId);
                    $("#PaymentTerm").val(results.PaymentTerms);
                    $("#IncoTerm").val(results.InccoTerm); 
                    if (results.CountryType != null && results.CountryType != "") {
                        $("input[name=CountryType][value=" + results.CountryType + "]").prop('checked', true);
                        $("input[name=UserDistType][value=" + results.UserDistType + "]").prop('checked', true);

                        if (results.CountryType === "Export") {
                            $('#IsAnalyticalData').attr('checked', true);
                            $('#Currency').val('USD');
                        }
                        else {
                            $('#Currency').val('INR');
                        }
                    }
                }
            }
        });
    },

    transferCompanyData: function (isDelete) {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#TransferCompanyId").val() === "") {
            $("#TransferCompanyId").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        if (isDelete) {
            $('#btntransfertrue').prop('disabled', true);
        }
        else {
            $('#btntransferfalse').prop('disabled', true);
        }


        $.ajax({
            type: "POST",
            url: "/Form/TransferCompanyData?id=" + $("#compid").val() + "&transferCompanyId=" + $('#TransferCompanyId').val() + "&isDelete=" + isDelete,
            data: {},
            success: function (results) {
                if (isDelete) {
                    $('#btntransfertrue').prop('disabled', false);
                }
                else {
                    $('#btntransferfalse').prop('disabled', false);
                }
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Company transfered updated.");
                window.location.reload(true);
            }
        });
    },
    addnewrawinquote: function () {
        debugger;
        // Create FormData object      
        var fileData = new FormData();
        fileData.append("QuoteId", $("#hdnQuoteId").val());
        fileData.append("CATNo", $("#txtcatbumber").val());
        fileData.append("DisplayOrder", $("#hdnDisplayOrder").val());
        $.ajax({
            type: "POST",
            url: "/Form/AddNewRawInQuote",
            type: "POST",
            contentType: false,
            processData: false,
            data: fileData,
            success: function (results) {

            }
        });
    },

    addPopUpData: function () {
        debugger;
        // Create FormData object      
        var fileData = new FormData();
        fileData.append("Id", $("#Id").val());
        fileData.append("Name", $("#Name").val());
        fileData.append("FilePath", $("#FilePath").val());
        fileData.append("IsGeneral", $("#IsGeneral").is(":checked"));
        fileData.append("IsActive", $("#IsActive").is(":checked"));
        fileData.append("IsEmailOrGroup", $('input[name=IsEmailOrGroup]:checked').val());
        if ($("#GroupId").val() !== null && $("#GroupId").val().length > 0) {
            fileData.append("GroupId", $("#GroupId").val().join());
        }
        if ($("#UserId").val() !== null && $("#UserId").val().length > 0) {
            fileData.append("UserId", $("#UserId").val().join());
        }
        var fileUpload = $("#FilePath").get(0);
        var files = fileUpload.files;
        // Looping over all files and add it to FormData object      
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        $.ajax({
            type: "POST",
            url: "/Form/AddPopUpData",
            type: "POST",
            contentType: false,
            processData: false,
            data: fileData,
            success: function (results) {

            }
        });

    },

    //add company
    addCompanyData: function (isRFQPage = false) {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#name").val() === "") {
            $("#name").addClass('errorcls');
            errorCount += 1;
        }
        if ($('input[name=UserDistType]:checked').val() === "" ||
            $('input[name=UserDistType]:checked').val() === undefined ||
            $('input[name=UserDistType]:checked').val() === "undefined") {
            toastr.error("Please select User/Dist");
            errorCount += 1;
        }
        if ($('input[name=CountryType]:checked').val() === "" ||
            $('input[name=CountryType]:checked').val() === undefined ||
            $('input[name=CountryType]:checked').val() === "undefined") {
            toastr.error("Please select Domestic/Export");
            errorCount += 1;
        }

        if ($("#GeographicalLocation").val() === "") {
            toastr.error("Please select Geographical Location");
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }
        var address = $("#address").val();
        var location = $("#Location").val();
        var IsPaymentPending = $("#IsPaymentPending").is(":checked");
        var IsBlockCompany = $("#IsBlockCompany").is(":checked");
        var model = {
            "name": $("#name").val(),
            "Location": location,
            "address": address,
            "masteremail": $("#masteremail").val(),
            "id": $("#compid").val(),
            "CountryType": $('input[name=CountryType]:checked').val(),
            "TermsId": $("#TermsId").val(),
            "UserDistType": $('input[name=UserDistType]:checked').val(),
            "Country": $("#Country").val(),
            "Contact": $("#Contact").val(),
            "FollowupTime": $("#FollowupTime").val(),
            "PaymentTerms": $("#PaymentTerms").val(),
            "IsPaymentPending": IsPaymentPending,
            "IsBlockCompany": IsBlockCompany,
            "Branch": $("#Branch").val(),
            "Add1": $("#Add1").val(),
            "Add2": $("#Add2").val(),
            "City": $("#City").val(),
            "State": $("#State").val(),
            "PostCode": $("#PostCode").val(),
            "ShipAdd1": $("#ShipAdd1").val(),
            "ShipAdd2": $("#ShipAdd2").val(),
            "ShipCity": $("#ShipCity").val(),
            "ShipState": $("#ShipState").val(),
            "ShipCountry": $("#ShipCountry").val(),
            "Telno": $("#Telno").val(),
            "ShipTelno": $("#ShipTelno").val(),
            "ShipPostCode": $("#ShipPostCode").val(),
            "IsConversionReport": $("#IsConversionReport").is(":checked"),
            "BDTeam": $("#BDTeam").val(),
            "ProjectTeam": $("#ProjectTeam").val(),
            "SAPCode": $("#SAPCode").val(),
            "AnalyticalData": $("textarea#AnalyticalData").val(),
            "ClientSuggestedComment": $("textarea#ClientSuggestedComment").val(),
            "GeographicalLocation": $("#GeographicalLocation").val(),
            "IsQuoteHide": $("#IsQuoteHide").is(":checked"),
            "IsApprovedFirst": $("#IsApprovedFirst").is(":checked"),
        };
        $.ajax({
            type: "POST",
            url: "/Form/AddCompanyData",
            data: model,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Company updated.");

                if (isRFQPage) {

                    backselecton();
                    //reload company list

                    $.ajax({
                        type: "GET",
                        url: "/Form/LoadAllCompanyList",
                        data: {},
                        success: function (results) {
                            $('#tblrfqcompselection').DataTable().clear().destroy();

                            var str = "";
                            $(results).each(function (i, v) {
                                str += "<tr><td> " + v.name + "</td><td><a href='javascript:void(0)' class='btn btn-primary' onclick='selectCompany(" + v.id + ")'>Select</a></td></tr>";
                            });
                            $("#tblrfqcompselection").find("tbody").html(str);
                            $("#tblrfqcompselection").DataTable();
                        }
                    });
                }
                else {
                    setTimeout(function () { window.location.href = "/Form/CompanyList"; }, 2000);
                }
            }
        });
    },

    addNewPurchaseRFQData: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#casno").val() === "") {
            $("#casno").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        $.ajax({
            type: "POST",
            url: "/Form/addNewPurchaseRFQData",
            data: {
                CATNo: $("#catno").val(),
                CASNo: $("#casno").val(),
                ChemicalName: $("#chemicalname").val(),
                Comment: $("#comment").val(),
                RefBy: $("#RefBy").val()
            },
            success: function (results) {
                $('#invoiceQueryModal').modal('hide');
                window.location.reload(true);
                //$('#tblpurchaserfq').DataTable().ajax.reload(null, false);
            }
        });
    },

    //add new company data from partial view and bind dropdown
    addNewCompanyData: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#name").val() === "") {
            $("#name").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        var address = $("#address").val();
        var location = $("#Location").val();
        $.ajax({
            type: "POST",
            url: "/Form/AddCompanyData?name=" + encodeURIComponent($("#name").val()) + "&Location=" + location + "&address=" + address + "&masteremail=" + encodeURIComponent($("#masteremail").val()) + "&id=" + $("#compid").val() + "&CountryType=" + $('input[name=CountryType]:checked').val() + "&TermsId=" + encodeURIComponent($("#TermsId").val()) + "&UserDistType=" + $('input[name=UserDistType]:checked').val() + "&Country=" + encodeURIComponent($("#Country").val()) + "&Contact=" + encodeURIComponent($("#Contact").val()) + "&FollowupTime=" + encodeURIComponent($("#FollowupTime").val()) + "&PaymentTerms=" + encodeURIComponent($("#PaymentTerms").val()),
            data: {},
            success: function (results) {
                $.ajax({
                    type: "GET",
                    url: "/Form/GetAllCompanydata",
                    data: {},
                    success: function (resultscop) {
                        $("#CompanyId").empty();
                        var str = "";

                        $(resultscop).each(function (i, v) {
                            str += "<option value='" + v.Value + "'>" + v.Text + "</option>";
                        });

                        $("#CompanyId").html(str);

                        $('#myModal').modal('hide');
                    }
                });
            }
        });
    },

    // add category master data 
    addCategoryMasterData: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#Name").val() === "") {
            $("#Name").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#Ten").val() === "") {
            $("#Ten").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#Twentyfive").val() === "") {
            $("#Twentyfive").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#Fifty").val() === "") {
            $("#Fifty").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#Onehundred").val() === "") {
            $("#Onehundred").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#TwohundredFifty").val() === "") {
            $("#TwohundredFifty").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#Fivehundred").val() === "") {
            $("#Fivehundred").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#OneThousand").val() === "") {
            $("#OneThousand").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#TenUSD").val() === "") {
            $("#TenUSD").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#TwentyfiveUSD").val() === "") {
            $("#TwentyfiveUSD").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#FiftyUSD").val() === "") {
            $("#FiftyUSD").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#OnehundredUSD").val() === "") {
            $("#OnehundredUSD").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#TwohundredFiftyUSD").val() === "") {
            $("#TwohundredFiftyUSD").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#FivehundredUSD").val() === "") {
            $("#FivehundredUSD").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#OneThousandUSD").val() === "") {
            $("#OneThousandUSD").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        var obj = {
            Id: $("#categorymasterid").val(),
            Name: $("#Name").val(),
            Ten: $("#Ten").val(),
            Twentyfive: $("#Twentyfive").val(),
            Fifty: $("#Fifty").val(),
            Onehundred: $("#Onehundred").val(),
            TwohundredFifty: $("#TwohundredFifty").val(),
            Fivehundred: $("#Fivehundred").val(),
            OneThousand: $("#OneThousand").val(),
            TenUSD: $("#TenUSD").val(),
            TwentyfiveUSD: $("#TwentyfiveUSD").val(),
            FiftyUSD: $("#FiftyUSD").val(),
            OnehundredUSD: $("#OnehundredUSD").val(),
            TwohundredFiftyUSD: $("#TwohundredFiftyUSD").val(),
            FivehundredUSD: $("#FivehundredUSD").val(),
            OneThousandUSD: $("#OneThousandUSD").val(),
            Rating: $("#Rating").val()
        };

        $.ajax({
            type: "POST",
            url: "/Form/AddCategoryMasterData",
            data: obj,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Category master updated.");
                setTimeout(function () { window.location.href = "/Form/CategoryMasterList"; }, 2000);
            }
        });
    },

    //add category master range data 
    addCategoryMasterRangeData: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#TenStartRange").val() === "") {
            $("#TenStartRange").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#TenEndRange").val() === "") {
            $("#TenEndRange").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#TwentyfiveStartRange").val() === "") {
            $("#TwentyfiveStartRange").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#TwentyfiveEndRange").val() === "") {
            $("#TwentyfiveEndRange").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#FiftyStartRange").val() === "") {
            $("#FiftyStartRange").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#FiftyEndRange").val() === "") {
            $("#FiftyEndRange").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#OnehundredStartRange").val() === "") {
            $("#OnehundredStartRange").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#OnehundredEndRange").val() === "") {
            $("#OnehundredEndRange").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#TwohundredFiftyStartRange").val() === "") {
            $("#TwohundredFiftyStartRange").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#TwohundredFiftyEndRange").val() === "") {
            $("#TwohundredFiftyEndRange").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#FivehundredStartRange").val() === "") {
            $("#FivehundredStartRange").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#FivehundredEndRange").val() === "") {
            $("#FivehundredEndRange").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#OneThousandStartRange").val() === "") {
            $("#OneThousandStartRange").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#OneThousandEndRange").val() === "") {
            $("#OneThousandEndRange").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        var obj = {
            TenStartRange: $("#TenStartRange").val(),
            TenEndRange: $("#TenEndRange").val(),
            TwentyfiveStartRange: $("#TwentyfiveStartRange").val(),
            TwentyfiveEndRange: $("#TwentyfiveEndRange").val(),
            FiftyStartRange: $("#FiftyStartRange").val(),
            FiftyEndRange: $("#FiftyEndRange").val(),
            OnehundredStartRange: $("#OnehundredStartRange").val(),
            OnehundredEndRange: $("#OnehundredEndRange").val(),
            TwohundredFiftyStartRange: $("#TwohundredFiftyStartRange").val(),
            TwohundredFiftyEndRange: $("#TwohundredFiftyEndRange").val(),
            FivehundredStartRange: $("#FivehundredStartRange").val(),
            FivehundredEndRange: $("#FivehundredEndRange").val(),
            OneThousandStartRange: $("#OneThousandStartRange").val(),
            OneThousandEndRange: $("#OneThousandEndRange").val()
        };
        $.ajax({
            type: "POST",
            url: "/Form/AddCategoryMasterRangeData",
            data: obj,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Category master range updated.");


                setTimeout(function () { window.location.href = "/Form/CategoryMasterList"; }, 2000);

            }
        });
    },


    addMSDSData: function (isRFQPage = false) {
        var formdata = $("#frmMSDS").serializeObject();

        $.ajax({
            type: "POST",
            url: "/Form/AddMSDSData",
            data: formdata,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                else {
                    window.open('/Form/DownloadMSDS', '_blank')
                }
            }
        });
    },


    addRemarkData: function () {

        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("textarea#ClientRemark").val() === "") {
            $("textarea#ClientRemark").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }
        var passdata = {
            Remark: jQuery("#ClientRemark").val(),
            Id: $("#qdid").val(),
            isClientSection: $("#isClientSection").val()
        };
        var address = ""; //$("#address").val()
        $.ajax({
            type: "POST",
            url: "/Form/AddClientRemarks",
            data: passdata,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Remarks updated.");
                setTimeout(function () { window.location.reload(true); }, 2000);

            }
        });
    },

    addQuotePrice: function () {
        var passdata = {
            QuotePrice: $("textarea#QuotePrice").val(),
            Id: $("#quotepriceproductId").val()
        };
        var address = "";
        $.ajax({
            type: "POST",
            url: "/Form/AddQuotePriceData",
            data: passdata,
            success: function (results) {
                toastr.success("Remark updated.");
                $('#invoiceQueryModal').modal('hide');
                if (window.location.href.toLowerCase().indexOf("quotationlist") === "-1") {
                    Quote.getProductListForQuote($("#QuoteId").val());
                    Quote.tempProductListForQuote();
                }
            }
        });
    },

    LinkBatchNo: function (quotedetailsid) {
        var selected = [];
        var formid = 0;
        $(".clslinkproduct").each(function () {
            if ($(this).is(":checked")) {
                selected.push($(this).val());
            }
        });
        if (selected.length === 0) {
            toastr.error("Please select only one product");
            return false;
        }
        if (selected.length > 1) {
            toastr.error("Please select only one product");
            return false;
        }
        formid = selected[0];
        var passdata = {
            formid: formid,
            Id: quotedetailsid
        };

        $.ajax({
            url: '/Form/LinkedFormData',
            data: passdata,
            type: 'POST',
            traditional: true, // add this
            dataType: 'json',
            success: function (data) {
                toastr.success("Data linked updated.");
                $('#invoiceQueryModal').modal('hide');
                window.location.reload(true);
            }
        });
    },

    hideQuoteDetails: function () {
        var ids = [];
        $(".clsprepro").each(function () {
            if ($(this).is(":checked")) {
                ids.push($(this).val());
            }
        });
        if (ids.length === 0) {
            toastr.error("Please select only one product");
            return false;
        }

        $.ajax({
            url: '/Form/HideQuoteDetail',
            data: { id: ids },
            type: 'POST',
            traditional: true, // add this
            dataType: 'json',
            success: function (data) {
                if (data.success) {
                    toastr.success('History Hide Successfully.');
                }
            }
        });
    },

    addRetest: function () {
        var selected = [];
        $(".selectretest").each(function () {
            if ($(this).is(":checked")) {
                selected.push($(this).val());
            }
        });
        var passdata = {
            Inventoryid: selected,
            Id: $("#catproductId").val()
        };

        $.ajax({
            url: '/Form/addRetestData',
            data: passdata,
            type: 'POST',
            traditional: true, // add this
            dataType: 'json',
            success: function (data) {
                toastr.success("Re-Test list updated.");
                $('#invoiceQueryModal').modal('hide');
            }
        });
    },

    updateBatchnoInQuotation: function () {
        var selected = [];
        $(".selectretest").each(function () {
            if ($(this).is(":checked")) {
                selected.push($(this).val());
            }
        });
        var passdata = {
            quotationdetailId: $("#catquotedetaid").val(),
            batchId: selected[0]
        };

        $.ajax({
            url: '/Form/updateBatchnoInQuotation',
            data: passdata,
            type: 'POST',
            traditional: true, // add this
            dataType: 'json',
            success: function (data) {
                toastr.success("batch no updated.");
                $('#invoiceQueryModal').modal('hide');
            }
        });
    },

    addFollowupDescription: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("textarea#FollowupDescription").val() === "") {
            $("textarea#FollowupDescription").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }
        var passdata = {
            FollowupDescription: jQuery("#FollowupDescription").val(),
            Id: $("#FollowupId").val()
        };
        var address = "";
        $.ajax({
            type: "POST",
            url: "/Form/AddFollowupDescriptionData",
            data: passdata,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Followup description updated.");
                $('#invoiceQueryModal').modal('hide');

            }
        });
    },

    ChangeAvailableDataforForm: function () {
        var formData = new FormData();
        formData.append("formId", $('#formId').val());
        var dataradioAdditionalAnalysis = new Array();
        $("input[name='radioAdditionalAnalysis']:checked").each(function (i) {
            dataradioAdditionalAnalysis.push($(this).val());
        });
        formData.append("radiorbAdditionalAnalysis", dataradioAdditionalAnalysis.join());
        var files1 = $("#invoiceQueryModal").find("#IRAttachment").get(0).files;
        if (files1.length > 0) {
            formData.append("IRAttachment", files1[0]);
        }
        var files2 = $("#invoiceQueryModal").find("#MassAttachment").get(0).files;
        if (files2.length > 0) {
            formData.append("MassAttachment", files2[0]);
        }

        var files3 = $("#invoiceQueryModal").find("#PLCAttachment").get(0).files;
        if (files3.length > 0) {
            formData.append("PLCAttachment", files3[0]);
        }

        var files4 = $("#invoiceQueryModal").find("#NMRAttchment").get(0).files;
        if (files4.length > 0) {
            formData.append("NMRAttchment", files4[0]);
        }

        var files5 = $("#invoiceQueryModal").find("#QNMRAttchment").get(0).files;
        if (files5.length > 0) {
            formData.append("QNMRAttchment", files5[0]);
        }

        var files6 = $("#invoiceQueryModal").find("#TGAAttachment").get(0).files;
        if (files6.length > 0) {
            formData.append("TGAAttachment", files6[0]);
        }

        var files7 = $("#invoiceQueryModal").find("#CMRAttchment").get(0).files;
        if (files7.length > 0) {
            formData.append("CMRAttchment", files7[0]);
        }

        var files8 = $("#invoiceQueryModal").find("#DEPTAttachment").get(0).files;
        if (files8.length > 0) {
            formData.append("DEPTAttachment", files8[0]);
        }

        var files9 = $("#invoiceQueryModal").find("#HRMSAttachment").get(0).files;
        if (files9.length > 0) {
            formData.append("HRMSAttachment", files9[0]);
        }

        var files10 = $("#invoiceQueryModal").find("#ROIAttachment").get(0).files;
        if (files10.length > 0) {
            formData.append("ROIAttachment", files10[0]);
        }
        var files11 = $("#invoiceQueryModal").find("#ElementralAttachment").get(0).files;
        if (files11.length > 0) {
            formData.append("ElementralAttachment", files11[0]);
        }
        var files12 = $("#invoiceQueryModal").find("#SERAttachment").get(0).files;
        if (files12.length > 0) {
            formData.append("SERAttachment", files12[0]);
        }

        var files13 = $("#invoiceQueryModal").find("#GCAttachment").get(0).files;
        if (files13.length > 0) {
            formData.append("GCAttachment", files13[0]);
        }
        var files14 = $("#invoiceQueryModal").find("#ELSDAttachment").get(0).files;
        if (files14.length > 0) {
            formData.append("ELSDAttachment", files14[0]);
        }
        var files15 = $("#invoiceQueryModal").find("#ChiralAttachmenrt").get(0).files;
        if (files15.length > 0) {
            formData.append("ChiralAttachmenrt", files15[0]);
        }
        $.ajax({
            type: 'POST',
            url: '/Form/SubmitAvailableDataForm',
            data: formData,
            enctype: 'multipart/form-data',
            processData: false,
            contentType: false,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("You have updated available data.");
                $('#invoiceQueryModal').modal('hide');
                var menulink = '';
                $('.nav').find('li').each(function () {
                    if ($(this).hasClass('active')) {
                        menulink = $(this).find('a').attr('data-tabname');
                    }
                });

                var tableId = '';
                if (menulink === 'all') {
                    tableId = 'example1';
                }
                else if (menulink === 'dispatch') {
                    tableId = 'tbldispatch';
                }
                else if (menulink === 'workingstandard') {
                    tableId = 'tblworkingstandard';
                }
                else if (menulink === 'inventory') {
                    tableId = 'tblinventory';
                }
                else if (menulink === 'approved') {
                    tableId = 'tblapproved';
                }
                $('#' + tableId).DataTable().ajax.reload(null, false);
            }
        });
    },

    addSOPData: function () {
        var formData = new FormData();
        formData.append("DepartmentName", $('#DepartmentName').val());
        formData.append("FileName", $('#FileName').val());
        var files13 = $("#myModal").find("#FilePath").get(0).files;
        if (files13.length > 0) {
            formData.append("FilePath", files13[0]);
        }
        $.ajax({
            type: 'POST',
            url: '/Form/SubmitSOPData',
            data: formData,
            enctype: 'multipart/form-data',
            processData: false,
            contentType: false,
            success: function (results) {
                $('#myModal').modal('hide');
            }
        });
    },
    addApprovedComment: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("textarea#CommentText").val() === "") {
            $("textarea#CommentText").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }
        var passdata = {
            CommentText: jQuery("#CommentText").val(),
            Id: $("#Commentid").val(),
            isSCientist: $("#CommentisSCientist").val(),
        };
        var address = "";
        $.ajax({
            type: "POST",
            url: "/Form/AddApprovedCommentData",
            data: passdata,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Approved comment updated.");
                $('#invoiceQueryModal').modal('hide');

            }
        });
    },

    addCOARemark: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("textarea#COARemarkText").val() === "") {
            $("textarea#COARemarkText").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }
        var passdata = {
            COARemarkText: jQuery("#COARemarkText").val(),
            Id: $("#remarkId").val(),
            Page: jQuery("#Page").val(),
        };
        var address = "";
        $.ajax({
            type: "POST",
            url: "/Form/AddCOARemarkData",
            data: passdata,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("COA Remark updated.");
                $('#invoiceQueryModal').modal('hide');

            }
        });
    },

    addInhouseRemark: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("textarea#InhouseRemarkText").val() === "") {
            $("textarea#InhouseRemarkText").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }
        var passdata = {
            InhouseRemarkText: jQuery("#InhouseRemarkText").val(),
            Id: $("#InhouseRemarkid").val()
        };
        var address = "";
        $.ajax({
            type: "POST",
            url: "/Form/AddInhouseRemarkData",
            data: passdata,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Inhouse Remark updated.");
                $('#invoiceQueryModal').modal('hide');

            }
        });
    },


    addJourneyComment: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("textarea#JourneyCommentText").val() === "") {
            $("textarea#JourneyCommentText").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }
        var passdata = {
            JourneyCommentText: jQuery("#JourneyCommentText").val(),
            Id: $("#JourneyCommentid").val()
        };
        var address = "";
        $.ajax({
            type: "POST",
            url: "/Form/JourneyCommentData",
            data: passdata,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Journey Comment updated.");
                $('#invoiceQueryModal').modal('hide');
                $('#tblcontrolledsubstance').DataTable().ajax.reload(null, false);
            }
        });
    },

    addPurchaseSummary: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("textarea#SummaryText").val() === "") {
            $("textarea#SummaryText").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }
        var passdata = {
            SummaryText: jQuery("#SummaryText").val(),
            Id: $("#Summaryid").val()
        };
        var address = "";
        $.ajax({
            type: "POST",
            url: "/Form/PurchaseSummaryData",
            data: passdata,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Summary updated.");
                $('#invoiceQueryModal').modal('hide');
                debugger;
                if (jQuery("#PageName").val() === '') {
                    window.location.reload(true);
                }
            }
        });
    },


    addPurchaseRemark: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("textarea#RemarkText").val() === "") {
            $("textarea#RemarkText").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }
        var passdata = {
            RemarkText: jQuery("#RemarkText").val(),
            Id: $("#Remarkid").val()
        };
        var address = "";
        $.ajax({
            type: "POST",
            url: "/Form/PurchaseRemarkData",
            data: passdata,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Remark updated.");
                $('#invoiceQueryModal').modal('hide');
                window.location.reload(true);
            }
        });
    },


    addPurchasemanagementRemark: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("textarea#ManagementRemarkText").val() === "") {
            $("textarea#ManagementRemarkText").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }
        var passdata = {
            ManagementRemarkText: jQuery("#ManagementRemarkText").val(),
            Id: $("#ManagementRemarkid").val()
        };
        var address = "";
        $.ajax({
            type: "POST",
            url: "/Form/PurchaseManagementRemarkData",
            data: passdata,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Remark updated.");
                $('#invoiceQueryModal').modal('hide');
                window.location.reload(true);
            }
        });
    },

    addExplanation: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("textarea#ExplanationText").val() === "") {
            $("textarea#ExplanationText").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }
        var passdata = {
            ExplanationText: jQuery("#ExplanationText").val(),
            Id: $("#Explanationid").val()
        };
        var address = "";
        $.ajax({
            type: "POST",
            url: "/Form/AddExplainationData",
            data: passdata,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Explanation updated.");
                $('#invoiceQueryModal').modal('hide');

            }
        });
    },

    addGLPRemark: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("textarea#GLPRemark").val() === "") {
            $("textarea#GLPRemark").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }
        var passdata = {
            GLPRemark: jQuery("#GLPRemark").val(),
            Id: $("#GLPRemarkid").val()
        };
        var address = "";
        $.ajax({
            type: "POST",
            url: "/Form/AddGLPRemarkData",
            data: passdata,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("GLP Remark updated.");
                $('#invoiceQueryModal').modal('hide');

            }
        });
    },

    addQueryData: function () {

        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("textarea#QueryText").val() === "") {
            $("textarea#QueryText").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }
        var passdata = {
            QueryText: jQuery("#QueryText").val(),
            Id: $("#qdids").val(),
            Origin: $("#Origin").val(),
            Type: $("#Type").val(),
            Email: $("#Email").val(),
            QuerySubject: $("#QuerySubject").val(),
            //sectionName: $("#sectionName").val(),
            //scientistId: $("#scientistId").val(),
            //estDate: $("#esticompleteDate").val()
        };
        var address = ""; //$("#address").val()
        $.ajax({
            type: "POST",
            url: "/Form/AddQueryData",
            data: passdata,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Query updated.");
                setTimeout(function () { window.location.reload(true); }, 2000);
            }
        });
    },

    //add master data
    addNameMasterDatas: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#sku").val() === "") {
            $("#sku").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#ep").val() === "") {
            $("#ep").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#usp").val() === "") {
            $("#usp").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#other").val() === "") {
            $("#other").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#chemDraw").val() === "") {
            $("#chemDraw").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        var address = ""; //$("#address").val()
        $.ajax({
            type: "POST",
            url: "/Form/AddNameMasterData?sku=" + $("#sku").val() + "&ep=" + $("#ep").val()
                + "&usp=" + $("#usp").val()
                + "&other=" + $("#other").val()
                + "&chemDraw=" + $("#chemDraw").val() + "&id=" + $("#masterid").val(),
            data: {},
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("Name master updated.");
                setTimeout(function () { window.location.href = "/Form/Namemasterlist"; }, 2000);

            }
        });
    },

    //add term
    addTermData: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#term").val() === "") {
            $("#term").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        $.ajax({
            type: "POST",
            url: "/Form/AddTermsData?term=" + $("#term").val(),
            data: {},
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("You have added new term.");
                setTimeout(function () { window.location.href = "/Form/Terms"; }, 2000);

            }
        });
    },

    //add missive
    addMissiveData: function () {
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#term").val() === "") {
            $("#term").addClass('errorcls');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        $.ajax({
            type: "POST",
            url: "/Form/AddMissiveData?term=" + $("#term").val(),
            data: {},
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("You have added new term.");
                setTimeout(function () { window.location.href = "/Form/MissiveResponse"; }, 2000);

            }
        });
    },

    ///save club quotation
    saveClubQuote: function (saveandpdf) {

        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if ($("#CompanyId").val() === "") {
            $("#CompanyId").addClass('errorcls');
            errorCount += 1;
        }
        if ($("#Email").val() === "") {
            $("#Email").addClass('errorcls');
            errorCount += 1;
        }

        if (errorCount !== 0) {
            return false;
        }

        var passData = {
            'CompanyId': $("#CompanyId").val(),
            'Email': $("#Email").val(),
            'PONumber': $("#PONumber").val(),
            'IsImageAttach': $('#IsImageAttach').is(":checked"),
            'Remark': $("#Remarks").val(),
            'TermsId': $("#TermsId").val()
        };
        $.ajax({
            type: "POST",
            url: "/Form/SaveClubQuote",
            data: passData,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                toastr.success("You have added new quotation.");
                setTimeout(function () { window.location.href = "/Form/QuotationList"; }, 2000);

            }
        });
    },

    disableButton: function () {
        $(".btn").each(function () {
            $(this).attr("disabled", "disabled");
        });
    },

    enableButton: function () {
        $(".btn").each(function () {
            $(this).attr("disabled", false);
        });
    },

    savePI: function (saveandpdf) {
        $("#SecurityCode").css('border', '');
        if ($("#hdnIsControlledSubstance").val() === '1') {
            if ($("#SecurityCode").val() === '') {
                $("#SecurityCode").css('border', '1px solid red');
                toastr.error('Please enter validate code.');
                return false;
            }

            if ($("#SecurityCode").val() !== securityCode) {
                toastr.error('Please enter valid code.');
                return false;
            }
        }

        var IsIGST = null;
        var cgstigst = $("input[name='fltgst']:checked").val();
        if ("IGST" === cgstigst) {
            IsIGST = true;
        }
        if ("CGST" === cgstigst) {
            IsIGST = false;
        }
        var Id = $("#Id").val();
        if ($("#CompanyId").val() === "") {
            toastr.error('Please select company.');
            errorCount += 1;
        }
        if ($("#PerformaInvoiceNo").val() === "") {
            toastr.error('Please select Invoice number.');
            errorCount += 1;
        }
        if ($("#CustomerPoNo").val() === "") {
            toastr.error('Please select PO Number.');
            errorCount += 1;
        }
        if ($("#PerformaInvoiceDate").val() === "") {
            toastr.error('Please select invoice date.');
            errorCount += 1;
        }

        SaveallPIPartialProductDetails();

        var passData = {
            'Id': $("#Id").val(),
            'CompanyId': $("#CompanyId").val(),
            'PerformaInvoiceNo': $("#PerformaInvoiceNo").val(),
            'CustomerPoNo': $("#CustomerPoNo").val(),
            'PerformaInvoiceDate': $("#PerformaInvoiceDate").val(),
            'Currency': $("#Currency").val(),
            'PaymentTerm': $('#PaymentTerm').val(),
            'BillCompanyName': $("#BillCompanyName").val(),
            'SaveAndPdf': saveandpdf,
            'BillAddress': $("#BillAddress").val(),
            'BillCountry': $("#BillCountry").val(),
            'BillTelno': $("#BillTelno").val(),
            'ShipCompanyName': $("#ShipCompanyName").val(),
            'ShipAddress': $("#ShipAddress").val(),
            'ShipCountry': $("#ShipCountry").val(),
            'ShipTelno': $("#ShipTelno").val(),
            'GrossWeight': $("#GrossWeight").val(),
            'NetWeight': $("#NetWeight").val(),
            'HSNCode': $("#HSNCode").val(),
            'Courier': $("#Courier").val(),
            'PortOfDischarge': $("#PortOfDischarge").val(),
            'Incoterm': $("#Incoterm").val(),
            'ShippingCost': $("#ShippingCost").val(),
            'BillAddressId': $("#ddlBillingAddress").val(),
            'ShipAddressId': $("#ddlShippingAddress").val(),
            'BillGSTNo': $("#BillGSTNo").val(),
            'ShipGSTNo': $("#ShipGSTNo").val(),
            'IsIGST': IsIGST
        };

        $.ajax({
            type: "POST",
            url: "/Form/SavePerformaInvoice",
            data: passData,
            success: function (results) {
                if (!results.success) {
                    toastr.error(results.message);
                    return false;
                }
                else {
                    if (saveandpdf) {
                        window.open(
                            results.returnFilePath,
                            '_blank' // <- This is what makes it open in a new window.
                        );
                    }

                    window.location.href = "/Form/PI/" + results.piId;
                }
            }
        });

    },

    savePRRequest: function () {
        var errorCount = 0;
        if ($("CATNo").val() === '') {
            toastr.error('Please select domestic or export.');
            errorCount += 1;
        }

        if (errorCount !== 0) {
            return false;
        }

        var passData = {
            'RaisedBy': $("#RaisedBy").val(),
            'RaisedByUserId': $("#RaisedByUserId").val(),
            'Id': $("#Id").val(),
            'ProductId': $("#ProductId").val(),
            'APIID': $("#APIID").val(),
            'ProductId': $("#ProductId").val(),
            'Labno': $("#Labno").val(),
            'SubscientistId': $("#SubscientistId").val(),
            'EmployeeCode': $("#EmployeeCode").val(),
            'CATNo': $("#CATNo").val(),
            'CASNo': $("#CASNo").val(),
            'APIName': $("#APIName").val(),
            'ProductName': $("#ProductName").val(),
            'MolFormula': $("#MolFormula").val(),
            'MolWeight': $("#MolWeight").val(),
            'Density': $("#Density").val(),
            'PackSize': $("#PackSize").val(),
            'NoofPack': $("#NoofPack").val(),
            'TotalQty': $("#TotalQty").val(),
            'UoM': $("#UoM").val(),
            'Make': $("#Make").val(),
            'SupplierRef': $("#SupplierRef").val()
        }

        $.ajax({
            type: "POST",
            url: "/Form/SavePRRequest",
            data: passData,
            success: function (results) {
                if (results.success === true) {
                    toastr.success("You have added new PR Request.");
                    window.location.href = '/Form/SciPRRequestList'
                }
                else {
                    toastr.error("Something went wrong.");
                    return false;
                }
            }
        });
    },

    //save quotation
    saveQuote: function (saveandpdf, sendmail = false, gotodashboard = false, closetab = false) {
        var quoteID = $("#QuoteId").val();
        $(".errorcls").each(function () {
            $(this).removeClass('errorcls');
        });

        var errorCount = 0;
        if (!$("input:radio[name='CountryType']").is(":checked")) {
            toastr.error('Please select domestic or export.');
            errorCount += 1;
        }
        if (!$("input:radio[name='UserDistType']").is(":checked")) {
            toastr.error('Please select user or distributor.');
            errorCount += 1;
        }
        if ($("#CompanyId").val() === "") {
            toastr.error('Please select company.');
            errorCount += 1;
        }
        if ($("#Email").val() === "") {
            toastr.error('Please select email address.');
            errorCount += 1;
        }
        if ($("#TermsId").val() === "" || $("#TermsId").val() === undefined || $("#TermsId").val() === null) {
            toastr.error('Please select terms.');
            errorCount += 1;
        }
        if ($("#Currency").val() === "") {
            toastr.error('Please select currency.');
            errorCount += 1;
        }
        if (errorCount !== 0) {
            return false;
        }

        var sizeOfproducts = $("#dvTempProductList").find("table").length;
        if (sizeOfproducts === 0 && quoteID !== "0") {
            toastr.error("Please add atleast one product.");
            return false;
        }
        var CountryType = $('input[name=CountryType]:checked').val();
        if (CountryType === "" || CountryType === null || CountryType === undefined) {
            toastr.error('Please select domestic or export.');
            return false;
        }
        Quote.disableButton();

        if ($("#isClubQuote").val() === "True") {
            SaveallTempPartialProductDetails();
        }

        saveallalreadyProduct($('#IsCOA').is(":checked"));

        var multiAttach = [];
        $('.attachitems').each(function () {
            multiAttach.push($(this).attr("data-filename"));
        });
        var Attachment = '';
        if (multiAttach.length > 0) {
            debugger;
            Attachment = multiAttach.join();
        }

        if (closetab === true) {
            $('#IsQuoteApproved').prop("checked", true)
        }
        var passData = {
            'CompanyId': $("#CompanyId").val(),
            'Email': $("#Email").val(),
            'ClientRef': $("#ClientRef").val(),
            'UniqueId': $("#UniqueId").val(),
            'PONumber': $("#PONumber").val(),
            'IsImageAttach': $('#IsImageAttach').is(":checked"),
            'QuoteId': $("#QuoteId").val(),
            'SaveAndPdf': saveandpdf,
            'Remark': $("#Remarks").val(),
            'TermsId': $("#TermsId").val(),
            'IsClubQuotation': $("#isClubQuote").val(),
            'CountryType': $("input[name='CountryType']:checked").val(),
            'UserDistType': $("input[name='UserDistType']:checked").val(),
            'IsToBe': $('#IsToBe').is(":checked"),
            'IsQuoteApproved': $('#IsQuoteApproved').is(":checked"),
            'Auction': $('#Auction').is(":checked"),
            'PODate': $("#PODate").val(),
            'SuggChemName': $("#SuggChemName").val(),
            'Attachment': Attachment,
            'SendMail': sendmail,
            'EmailCC': $("#EmailCC").val(),
            'IsCOA': $('#IsCOA').is(":checked"),
            'IsFollowupRequired': $('#IsFollowupRequired').is(":checked"),
            'IsInstock': $('#IsInstock').is(":checked"),
            'IsCustomSynthesis': $('#IsCustomSynthesis').is(":checked"),
            'LayoutType': $("input[name='LayoutType']:checked").val(),
            'IsReviewed': $('#IsReviewed').is(":checked"),
            'IsPreviewed': $('#IsPreviewed').is(":checked"),
            'QuoteComment': $("#quotecomment").val(),
            'ExternalComment': $("#externalcomment").val(), 
            'PaymentTerm': $("#PaymentTerm").val(),
            'IsShippedCharge': $("#IsShippedCharge").is(":checked"),
            'IsShowDashboard': $("#IsShowDashboard").is(":checked"),
            'IsAnalyticalData': $("#IsAnalyticalData").is(":checked"),
            'IsStudy': $("#IsStudy").is(":checked"),
            'PurchaseName': $("#PurchaseName").val(),
            'PurchaseContactNo': $("#PurchaseContactNo").val(),
            'PurchaseEmail': $("#PurchaseEmail").val(),
            'PurchaseAddress': $("#PurchaseAddress").val(),
            'PurchaseCity': $("#PurchaseCity").val(),
            'PurchaseCountry': $("#PurchaseCountry").val(),
            'TechnicalName': $("#TechnicalName").val(),
            'TechnicalContactNo': $("#TechnicalContactNo").val(),
            'TechnicalEmail': $("#TechnicalEmail").val(),
            'TechnicalAddress': $("#TechnicalAddress").val(),
            'TechnicalCity': $("#TechnicalCity").val(),
            'TechnicalCountry': $("#TechnicalCountry").val(),
            'Currency': $("#Currency").val(),
            'IsRegret': $("#IsRegret").is(":checked"),
            'IsRFQManually': $("#IsRFQManually").is(":checked"),
            'QuotePDF': $("#txtquotepdf").val(),
            'EmailReceivedDate': /*$("#txtemailreceiveddate").data().date*/ null,
            'IsPreApproved': $("#IsPreApproved").is(":checked"),
            'IsRevision': $("#IsRevision").is(":checked"),
            'AssignTo': $("#AssignTo").val(),
            'IsUnderCorrection': $("#IsUnderCorrection").is(":checked"),
            'HelpScoatNumber': /*$("#HelpScoatNumber").val()*/ "",
            'DocumentTypeId': $("#DocumentTypeId").val(),
            'IsCustomPDFLayout': $("#IsCustomPDFLayout").is(":checked"),
            'Url': $("#Url").val(),
            'ConversionNumber': $("#ConversionNumber").val(),
            'IncoTerm': $("#IncoTerm").val(),
            'POUrl': $("#POUrl").val(), 
            'ShippingCharges': $("#ShippingCharges").val(),
            'FollowupAdminRemark': $("#FollowupAdminRemark").val(), 
            'ReminderCount': $("#ReminderCount").val(),
        };
        debugger;
        $.ajax({
            type: "POST",
            url: "/Form/SaveQuote",
            data: passData,
            success: function (results) {
                if (!results.success) {
                    Quote.enableButton();
                    toastr.error(results.message);
                    return false;
                }

                if (gotodashboard) {
                    window.location.href = "/Form/TodayDashboard";
                    return true;
                }

                toastr.success("You have added new quotation.");
                if (quoteID === "0") {
                    window.location.href = "/Form/Quote/" + results.quoteId
                    return true;
                }
                if (!saveandpdf && !sendmail) {
                    if (closetab === true) {
                        window.close();
                    }
                    else {
                        setTimeout(function () { window.location.href = "/Form/QuotationList"; }, 2000);
                    }
                }
                else if (!saveandpdf && sendmail) {
                    swal({
                        title: "Ready to send?",
                        text: "Are you sure to send this quotation to client?",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Yes",
                        cancelButtonText: "No",
                        closeOnConfirm: true,
                        closeOnCancel: true
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                $.ajax({
                                    type: "GET",
                                    url: "/Form/SaveReadyToSendQuoteDetails?id=" + results.quoteId,
                                    dataType: "json",
                                    traditional: true,
                                    data: {},
                                    success: function (result) {

                                        setTimeout(function () { window.location.href = "/Form/QuotationList"; }, 2000);
                                    },
                                    error: function (err) {
                                        alert(err.statusText);
                                    }
                                });
                            } else {
                                setTimeout(function () { window.location.href = "/Form/QuotationList"; }, 2000);
                            }
                        });
                }
                else {
                    var closepopup = true;
                    if ($("#IsShowDashboard").is(":checked")) {
                        closepopup = false;
                    }
                    var coadownloadcount = 1;
                    swal({
                        title: "Ready to send?",
                        text: "Are you sure to send this quotation to client?",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Yes",
                        cancelButtonText: "No",
                        closeOnConfirm: closepopup,
                        closeOnCancel: closepopup
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                if ($("#IsShowDashboard").is(":checked")) {
                                    swal({
                                        title: "Remove from Dashboard?",
                                        text: "Are you sure want to remove this quote from dashboard?",
                                        type: "warning",
                                        showCancelButton: true,
                                        confirmButtonClass: "btn-danger",
                                        confirmButtonText: "Yes",
                                        cancelButtonText: "No",
                                        closeOnConfirm: true,
                                        closeOnCancel: true
                                    },
                                        function (isConfirm) {
                                            if (isConfirm) {
                                                $.ajax({
                                                    type: "GET",
                                                    url: "/Form/RemoveShowOnDashboard?id=" + results.quoteId,
                                                    dataType: "json",
                                                    async: false,
                                                    data: {},
                                                    success: function (result) {
                                                    }
                                                });
                                                setTimeout(function () {
                                                    Quote.SaveReadyToSendQuoteDetails(results, coadownloadcount);
                                                }, 1500);

                                            }
                                            else {
                                                Quote.SaveReadyToSendQuoteDetails(results, coadownloadcount);
                                            }
                                        });
                                }
                                else {
                                    Quote.SaveReadyToSendQuoteDetails(results, coadownloadcount);
                                }
                            } else {
                                if ($("#IsShowDashboard").is(":checked")) {
                                    swal({
                                        title: "Remove from Dashboard?",
                                        text: "Are you sure want to remove this quote from dashboard?",
                                        type: "warning",
                                        showCancelButton: true,
                                        confirmButtonClass: "btn-danger",
                                        confirmButtonText: "Yes",
                                        cancelButtonText: "No",
                                        closeOnConfirm: true,
                                        closeOnCancel: true
                                    },
                                        function (isConfirm) {
                                            if (isConfirm) {
                                                if ($('#IsCOA').is(":checked")) {
                                                    var proidscount = 0;
                                                    $(".clspartiallistbatchnos").each(function () {
                                                        proidscount += 1;
                                                    });

                                                    $(".clspartiallistbatchnos").each(function () {
                                                        var selectedvalues = $(this).val();
                                                        if (selectedvalues !== "0" && selectedvalues !== undefined) {
                                                            var bNo = selectedvalues.trim().split(' ')[0];
                                                            $.ajax({
                                                                type: "GET",
                                                                url: "/Form/GetMasterCOAIdFromBatchNo?batchNo=" + bNo,
                                                                dataType: "json",
                                                                async: false,
                                                                data: {},
                                                                success: function (result) {
                                                                },
                                                                complete: function (data) {
                                                                    Quote.successCOADOwnloadfromquoteAlert(coadownloadcount, proidscount, results);
                                                                    coadownloadcount += 1;
                                                                }
                                                            });
                                                        }
                                                        else {
                                                            coadownloadcount += 1;
                                                        }
                                                    });
                                                }
                                            }
                                            else {
                                                if ($('#IsCOA').is(":checked")) {
                                                    var proidscount = 0;
                                                    $(".clspartiallistbatchnos").each(function () {
                                                        proidscount += 1;
                                                    });

                                                    $(".clspartiallistbatchnos").each(function () {
                                                        var selectedvalues = $(this).val();
                                                        if (selectedvalues !== "0" && selectedvalues !== undefined) {
                                                            var bNo = selectedvalues.trim().split(' ')[0];
                                                            $.ajax({
                                                                type: "GET",
                                                                url: "/Form/GetMasterCOAIdFromBatchNo?batchNo=" + bNo,
                                                                dataType: "json",
                                                                async: false,
                                                                data: {},
                                                                success: function (result) {
                                                                },
                                                                complete: function (data) {
                                                                    Quote.successCOADOwnloadfromquoteAlert(coadownloadcount, proidscount, results);
                                                                    coadownloadcount += 1;
                                                                }
                                                            });
                                                        }
                                                        else {
                                                            coadownloadcount += 1;
                                                        }
                                                    });
                                                }
                                            }
                                        });
                                }
                                else {
                                    if ($('#IsCOA').is(":checked")) {
                                        var proidscount = 0;
                                        $(".clspartiallistbatchnos").each(function () {
                                            proidscount += 1;
                                        });

                                        $(".clspartiallistbatchnos").each(function () {
                                            var selectedvalues = $(this).val();
                                            if (selectedvalues !== "0" && selectedvalues !== undefined) {
                                                var bNo = selectedvalues.trim().split(' ')[0];
                                                $.ajax({
                                                    type: "GET",
                                                    url: "/Form/GetMasterCOAIdFromBatchNo?batchNo=" + bNo,
                                                    dataType: "json",
                                                    async: false,
                                                    data: {},
                                                    success: function (result) {
                                                    },
                                                    complete: function (data) {
                                                        Quote.successCOADOwnloadfromquoteAlert(coadownloadcount, proidscount, results);
                                                        coadownloadcount += 1;
                                                    }
                                                });
                                            }
                                            else {
                                                coadownloadcount += 1;
                                            }
                                        });
                                    }
                                }
                            }


                            if (results.QuotePDF != '') {
                                //var link = document.createElement('a');
                                //link.href = '/Content/Attachment/' + results.QuotePDF;
                                //link.download = 'Supporting_Document';
                                //link.click();


                                //var win = window.open('/Content/Attachment/' + results.QuotePDF, '_blank');
                                //if (win) {
                                //    //Browser has allowed it to be opened
                                //    win.focus();
                                //} else {
                                //    //Browser has blocked it
                                //    /*alert('Please allow popups for this website');*/
                                //}
                            }

                        });
                }
            }
        });
    },

    SaveReadyToSendQuoteDetails: function (results, coadownloadcount) {
        $.ajax({
            type: "GET",
            url: "/Form/SaveReadyToSendQuoteDetails?id=" + results.quoteId,
            dataType: "json",
            traditional: true,
            data: {},
            success: function (result) {
                window.open(
                    '/Form/DownloadQuote/' + results.quoteId + '?saverecord=true',
                    '_blank' // <- This is what makes it open in a new window.
                );

                if ($('#IsCOA').is(":checked")) {
                    var proidscount = 0;
                    $(".clspartiallistbatchnos").each(function () {
                        proidscount += 1;
                    });

                    var arrayofbatch = [];
                    $(".clspartiallistbatchnos").each(function () {
                        var selectedvalues = $(this).val();
                        if (selectedvalues !== "0" && selectedvalues !== undefined) {
                            var bNo = selectedvalues.trim().split(' ')[0];
                            arrayofbatch.push(bNo);
                        }
                        else {
                            coadownloadcount += 1;
                        }
                    });


                    $.ajax({
                        url: '/Form/GetMasterCOAIdFromBatchNos',
                        data: {
                            id: arrayofbatch
                        },
                        type: 'POST',
                        traditional: true, // add this
                        dataType: 'json',
                        success: function (result) {

                            if (result.notavailable !== null) {
                                var str = '';
                                str += "<div class='col-md-12'>Not Available COA : " + result.notavailable.join(", ") + "</div>";
                                $('#myModal').find('.modal-body').html(str);
                                $('#myModal').modal({ show: true });
                                $('#myModal').find(".modal-dialog").css("width", "580px");
                            }

                            $(result.data).each(function (i, v) {
                                if ($('#IsAnalyticalData').is(":checked")) {
                                    window.open(
                                        '/Form/DownloadMasterCOAAndAnylyticalData/' + v + "?isScanCopy=" + false,
                                        '_blank' // <- This is what makes it open in a new window.
                                    );
                                }
                                else {
                                    window.open(
                                        '/Form/DownloadMasterCOA/' + v + "?isScanCopy=" + false,
                                        '_blank' // <- This is what makes it open in a new window.
                                    );
                                }
                            });
                        },
                        complete: function (data) {
                            Quote.successCOADOwnloadfromquoteAlert(coadownloadcount, proidscount, results);
                            coadownloadcount += 1;
                        }
                    });



                    //$.ajax({
                    //    type: "GET",
                    //    url: "/Form/GetMasterCOAIdFromBatchNo?batchNo=" + bNo,
                    //    dataType: "json",
                    //    async: false,
                    //    data: {},
                    //    success: function (result) {
                    //        if (result == 0) {
                    //            toastr.error(bNo + " Master COA not available.");
                    //        }
                    //        else {

                    //            if ($('#IsAnalyticalData').is(":checked")) {
                    //                window.open(
                    //                    '/Form/DownloadMasterCOAAndAnylyticalData/' + result + "?isScanCopy=" + false,
                    //                    '_blank' // <- This is what makes it open in a new window.
                    //                );
                    //            }
                    //            else {
                    //                window.open(
                    //                    '/Form/DownloadMasterCOA/' + result + "?isScanCopy=" + false,
                    //                    '_blank' // <- This is what makes it open in a new window.
                    //                );
                    //            }
                    //        }
                    //    },
                    //    complete: function (data) {
                    //        Quote.successCOADOwnloadfromquoteAlert(coadownloadcount, proidscount, results);
                    //        coadownloadcount += 1;
                    //    }
                    //});


                    //$(".clspartiallistbatchnos").each(function () {
                    //    var selectedvalues = $(this).val();
                    //    if (selectedvalues !== "0" && selectedvalues !== undefined) {
                    //        var bNo = selectedvalues.trim().split(' ')[0];
                    //        arrayofbatch.push(bNo);
                    //        $.ajax({
                    //            type: "GET",
                    //            url: "/Form/GetMasterCOAIdFromBatchNo?batchNo=" + bNo,
                    //            dataType: "json",
                    //            async: false,
                    //            data: {},
                    //            success: function (result) {
                    //                if (result == 0) {
                    //                    toastr.error(bNo + " Master COA not available.");
                    //                }
                    //                else {

                    //                    if ($('#IsAnalyticalData').is(":checked")) {
                    //                        window.open(
                    //                            '/Form/DownloadMasterCOAAndAnylyticalData/' + result + "?isScanCopy=" + false,
                    //                            '_blank' // <- This is what makes it open in a new window.
                    //                        );
                    //                    }
                    //                    else {
                    //                        window.open(
                    //                            '/Form/DownloadMasterCOA/' + result + "?isScanCopy=" + false,
                    //                            '_blank' // <- This is what makes it open in a new window.
                    //                        );
                    //                    }
                    //                }
                    //            },
                    //            complete: function (data) {
                    //                Quote.successCOADOwnloadfromquoteAlert(coadownloadcount, proidscount, results);
                    //                coadownloadcount += 1;
                    //            }
                    //        });
                    //    }
                    //    else {
                    //        coadownloadcount += 1;
                    //    }
                    //});
                }

                setTimeout(function () {
                    window.location.href = "/Form/Quote/" + results.quoteId;
                }, 2500);
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    },

    successCOADOwnloadfromquoteAlert: function (count, forloopcount, results) {
        if (count === forloopcount) {
            setTimeout(function () {
                window.location.href = "/Form/Quote/" + results.quoteId;
            }, 2500);
        }
    },

    importCOA: function () {
        // Checking whether FormData is available in browser  
        if (window.FormData !== undefined) {

            var fileUpload = $("#importCOAFile").get(0);
            var files = fileUpload.files;

            // Create FormData object  
            var fileData = new FormData();

            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            // Adding one more key to FormData object  
            fileData.append('UniqueId', $("#UniqueId").val());

            $.ajax({
                url: "/Form/ImportCOAFromexcel",
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (result) {
                    toastr.success("You have added new COA.");
                    window.location.reload(true);

                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        } else {
            alert("FormData is not supported.");
        }
    },

    importQuote: function () {
        // Checking whether FormData is available in browser  
        if (window.FormData !== undefined) {

            var fileUpload = $("#importQuoteFile").get(0);
            var files = fileUpload.files;

            // Create FormData object  
            var fileData = new FormData();

            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            // Adding one more key to FormData object  
            fileData.append('QuoteId', $("#QuoteId").val());

            $.ajax({
                url: "/Form/ImportQuotationDetailsDataFromexcel",
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (result) {
                    toastr.success("You have added new quotation.");
                    window.location.reload(true);
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        } else {
            alert("FormData is not supported.");
        }
    },

    /// Import previous quote from quotation list page
    importPreviousQuote: function () {
        // Checking whether FormData is available in browser  
        if (window.FormData !== undefined) {

            var fileUpload = $("#importPreviousQuoteFile").get(0);
            var files = fileUpload.files;

            // Create FormData object  
            var fileData = new FormData();

            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            // Adding one more key to FormData object  
            fileData.append('UniqueId', $("#UniqueId").val());

            $.ajax({
                url: "/Form/ImportNewQuoteFromexcel",
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (result) {
                    toastr.success("You have added new quotation.");
                    window.location.reload(true);
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        } else {
            alert("FormData is not supported.");
        }
    },

    importPurchaseProductFile: function () {
        // Checking whether FormData is available in browser  
        if (window.FormData !== undefined) {

            var fileUpload = $("#importPurchaseProductFile").get(0);
            var files = fileUpload.files;

            // Create FormData object  
            var fileData = new FormData();

            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            // Adding one more key to FormData object  
            /*fileData.append('UniqueId', $("#UniqueId").val());*/

            $.ajax({
                url: "/Form/importPurchaseProductFile",
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (result) {
                    if (result.success) {
                        window.location.reload(true);
                        toastr.success("You have imported excel file.");
                    }
                    else {
                        toastr.error(result.message);
                    }
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        } else {
            alert("FormData is not supported.");
        }
    },

    importNewProduct: function () {
        // Checking whether FormData is available in browser  
        if (window.FormData !== undefined) {

            var fileUpload = $("#importNewFile").get(0);
            var files = fileUpload.files;

            // Create FormData object  
            var fileData = new FormData();

            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            // Adding one more key to FormData object  
            fileData.append('UniqueId', $("#UniqueId").val());

            $.ajax({
                url: "/Form/ImportNewProductInTempQuote",
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (result) {
                    toastr.success("You have added new quotation.");
                    Quote.tempProductListForQuote();
                    $("#dvProInfo").hide();
                    $("#productId").val("0");
                    $("#detFromDBProductName").val("");
                    $("#detFromDBCasno").val("");
                    $("#detFromDBCatno").val("");
                    var strinv = "";
                    strinv += "<tr><td colspan='2'>No Data Available</td></tr>";
                    $("#detFromBatchDetails").html(strinv);
                    $("#detFromDBImage").attr('src', "");
                    $("#proDBImagePath").val("");
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        } else {
            alert("FormData is not supported.");
        }
    },

    checkCompEmailStatus: function (companyName, emailaddress) {
        $.ajax({
            type: "POST",
            url: "/Form/checkCompEmailStatus?companyName=" + companyName + "&emailaddress=" + emailaddress,
            data: passData,
            success: function (results) {

            }
        });
    },


    addNewProduct: function () {

        // Checking whether FormData is available in browser  
        if (window.FormData !== undefined) {

            var fileUpload = $("#detNewFile").get(0);
            var files = fileUpload.files;

            // Create FormData object  
            var fileData = new FormData();

            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            // Adding one more key to FormData object  
            fileData.append('ProductId', '0');
            fileData.append('QuoteId', $("#QuoteId").val());
            fileData.append('ProductName', $("#detNewProductName").val());
            fileData.append('CASNo', $("#detNewCasno").val());
            fileData.append('UniqueId', $("#UniqueId").val());
            fileData.append('Price', $("#dtNewprice").val());
            fileData.append('LeadTime', $("#detNewleadtime").val());
            fileData.append('Productremark', $("#detNewproductremark").val());
            fileData.append('isClubQuote', $("#isClubQuote").val());
            fileData.append('CompanyId', $("#CompanyId").val());
            $.ajax({
                url: "/Form/SaveProductInTempQuote",
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (result) {
                    toastr.success("You have added new quotation.");
                    // Quote.tempProductListForQuote();
                    Quote.getProductListForQuote($("#QuoteId").val());

                    $("#dvProInfo").hide();
                    $("#productId").val("0");
                    $("#detFromDBProductName").val("");
                    $("#detFromDBCasno").val("");
                    $("#detFromDBCatno").val("");
                    var strinv = "";
                    strinv += "<tr><td colspan='2'>No Data Available</td></tr>";
                    $("#detFromBatchDetails").html(strinv);
                    $("#detFromDBImage").attr('src', "");
                    $("#proDBImagePath").val("");
                    $("#detNewProductName").val("");
                    $("#detNewCasno").val("");
                    $("#dtNewprice").val("");
                    $("#detNewleadtime").val("");
                    $("#detNewproductremark").val("");
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        } else {
            alert("FormData is not supported.");
        }

    },

    LoadClubPIData: function () {
        var uniqueId = $("#Id").val();
        $.ajax({
            type: "POST",
            url: "/Form/PartialPIProductList?Id=" + uniqueId,
            data: {},
            success: function (results) {
                $("#dvTempProductList").html(results);
            }
        });
    },

    tempProductListForQuote: function () {
        var uniqueId = $("#UniqueId").val();
        var isCulbQuotation = $("#isClubQuote").val();
        $.ajax({
            type: "POST",
            url: "/Form/PartialTempProductList?uniqueId=" + uniqueId + "&isClubQuote=" + isCulbQuotation,
            data: {},
            success: function (results) {

                var isCulbQuotation = $("#isClubQuote").val();
                var quoteId = $("#QuoteId").val();
                if (quoteId === undefined || quoteId === '0' || quoteId === "" && isCulbQuotation !== 'True') {
                    $("#dvTempProductList").html(results);
                }
                else {
                    Quote.getProductListForQuote(quoteId, results);
                }
            }
        });
    },

    //get product list for quotation
    getProductListForQuote: function (quoteId, tempProductData) {
        $("#dvTempProductList").html("<div style='text-align: center;'><img src='/images/loader.gif' style='height: 170px;'/></div>");

        $.ajax({
            type: "POST",
            url: "/Form/PartialProductList?quoteId=" + quoteId + "&Layout=" + $('input[name="LayoutType"]:checked').val(),
            data: {},
            success: function (results) {
                $("#dvTempProductList").html(results);
                if (tempProductData !== undefined && tempProductData !== '0' && tempProductData !== "") {
                    $("#dvTempProductList").append(tempProductData);
                }

                refreshDiscountPrice();
                LoaddefaultRedIcon();
                $('.clstooltip').tooltip();
            }
        });
    },

    getProductListForHistoryQuote: function () {

        if (isHistoryDataEnable === false) {
            isHistoryDataEnable = true;
        }
        else {
            isHistoryDataEnable = false;
        }

        $("#dvTempProductList").html("<div style='text-align: center;'><img src='/images/loader.gif' style='height: 170px;'/></div>");

        $.ajax({
            type: "POST",
            url: "/Form/PartialProductList?quoteId=" + $("#QuoteId").val() + "&Layout=" + $('input[name="LayoutType"]:checked').val() + "&isHistoryLayout=" + isHistoryDataEnable,
            data: {},
            success: function (results) {
                $("#dvTempProductList").html(results);
                if (tempProductData !== undefined && tempProductData !== '0' && tempProductData !== "") {
                    $("#dvTempProductList").append(tempProductData);
                }

                refreshDiscountPrice();
                LoaddefaultRedIcon();
                $('.clstooltip').tooltip();
            }
        });
    },

    deleteProductFromQuoteDetails: function (id) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this product!",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            closeOnConfirm: true,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        type: "POST",
                        url: "/Form/DeleteProductFromQuoteDetails?id=" + id,
                        data: {},
                        success: function (results) {
                            if (results) {
                                Quote.tempProductListForQuote();
                                toastr.success("Your product has been deleted.");
                            }
                        }
                    });

                } else {
                    toastr.error("Your added product is safe :)");
                }
            });

    },

    //delete temp product from quotation
    deleteTempProduct: function (id) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this product!",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            closeOnConfirm: true,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    var isCulbQuotation = $("#isClubQuote").val();

                    $.ajax({
                        type: "POST",
                        url: "/Form/DeleteTempProduct?id=" + id + "&isClubQuote=" + isCulbQuotation,
                        data: {},
                        success: function (results) {
                            if (results) {
                                Quote.tempProductListForQuote();
                                toastr.success("Your product has been deleted.");
                            }
                        }
                    });

                } else {
                    toastr.error("Your added product is safe :)");
                }
            });

    },

    RemoveSynthesisLog: function (id) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this product again in synthesis log!",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            closeOnConfirm: true,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        type: "POST",
                        url: "/Form/RemoveSynthesisLog?id=" + id,
                        data: {},
                        success: function (results) {
                            if (results) {
                                toastr.success("Your product has been removed from synthesis log.");
                                setTimeout(function () { window.location.reload(true); }, 1000);
                            }
                        }
                    });

                } else {
                    toastr.error("Your added product is safe :)");
                }
            });
    },

    deleteQuote: function (id, isdirectquote = false) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this quote!",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            closeOnConfirm: true,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        type: "POST",
                        url: "/Form/DeleteQuotation?id=" + id,
                        data: {},
                        success: function (results) {
                            if (results) {
                                toastr.success("Your quote has been deleted.");
                                if (isdirectquote) {
                                    /*window.location.href = "/Form/QuotationList";*/
                                    window.location.href = "/Form/TodayDashboard";
                                }
                                else {
                                    setTimeout(function () { window.location.reload(true); }, 2000);
                                }
                            }
                        }
                    });

                } else {
                    toastr.error("Your added product is safe :)");
                }
            });
    },

    addPreviousProduct: function (id) {
        $.ajax({
            url: '/Form/addPreviousProductByQuoteDetailsId?id=' + id + "&quoteId=" + $("#QuoteId").val(),
            data: {},
            type: 'POST',
            success: function (data) {
                if (data.success) {
                    toastr.success("Your have added new product.");
                    Quote.tempProductListForQuote();
                }
            }
        });
    },

    copyProduct: function (id) {
        $.ajax({
            url: '/Form/getProductDetailsByQuoteDetailsId?id=' + id,
            data: {},
            type: 'POST',
            success: function (data) {
                if (data.success) {
                    var selectedRadio = $('input[name=productsRadios]:checked').val();
                    if (selectedRadio === 'db') {
                        //add records in db section
                        $("#dvProInfo").show();
                        $("#detFromDBProductName").val(data.record.ProductName);
                        $("#detFromDBCasno").val(data.record.CASNo);
                        $("#detFromDBCatno").val(data.record.CATNo);
                        if (data.record.Price !== '' && data.record.Price !== null && data.record.Price !== undefined) {
                            var copypricestr = '';
                            var pricstr = data.record.Price.split(',');
                            for (var i = 0; i < pricstr.length; i++) {
                                var mgtext = pricstr[i].split('@')[0].match(/\d+/);
                                var pricetext = pricstr[i].split('@').length > 1 ? pricstr[i].split('@')[1].split('X')[0].match(/\d+/) : "";
                                var sizetext = "";
                                if (pricetext == '' || pricetext == null || pricetext == undefined) {
                                    sizetext = "";
                                }
                                else {
                                    sizetext = pricstr[i].split('@')[1].split('X').length > 1 ? pricstr[i].split('@')[1].split('X')[1].match(/\d+/) : "";
                                }
                                copypricestr += mgtext + " @ " + pricetext + " @ " + sizetext + ",";
                                if (i === 0) {
                                    $(".clstoponemg").val(mgtext);
                                    $(".clstoponeprice").val(pricetext);
                                    $(".clstoponesize").val(sizetext);

                                }
                                if (i === 1) {
                                    $(".clstoptwomg").val(mgtext);
                                    $(".clstoptwoprice").val(pricetext);
                                    $(".clstoptwosize").val(sizetext);
                                }
                                if (i === 2) {
                                    $(".clstopthreemg").val(mgtext);
                                    $(".clstopthreeprice").val(pricetext);
                                    $(".clstopthreesize").val(sizetext);
                                }
                                if (i === 3) {
                                    $(".clstopfourmg").val(mgtext);
                                    $(".clstopfourprice").val(pricetext);
                                    $(".clstopfoursize").val(sizetext);
                                }
                            }

                            copyToClipboard(copypricestr);
                        }
                        //$("#price").val(data.record.Price);
                        $("#leadtime").val(data.record.LeadTime);
                        $("#productId").val(data.record.ProductId);
                        $("#proDBImagePath").val(data.record.ImagePath);

                        var obj = data.inventoryModel;

                        if (obj !== undefined && obj !== null && obj.length > 0) {
                            var strinv = "";
                            $(obj).each(function (i, v) {
                                strinv += "<tr><td>" + v.BatchNo + "</td><td>" + v.Qty + "</td></tr>";
                            });
                            $("#detFromBatchDetails").html(strinv);
                        }
                        else {
                            var strinvs = "";
                            strinvs += "<tr><td colspan='2'>No Data Available</td></tr>";
                            $("#detFromBatchDetails").html(strinvs);
                        }

                    }
                    else {
                        //add records in new product section
                    }
                }
            }
        });
    },

    addTempQuick: function () {
        $("#txtsearch").css('border', '');
        var search = $("#txtsearch").val();
        if (search !== "") {
            $.ajax({
                url: '/Form/addTempQuick?search=' + search,
                data: {},
                type: 'GET',
                success: function (data) {
                    if (data.success) {
                        $("#txtsearch").val("");
                        toastr.success("Your have added new product.");
                        GetTempQuickData();
                    }
                }
            });
        }
        else {
            $("#txtsearch").css('border', '1px solid red');
            return false;
        }
    },

    SaveQuickAdd: function () {
        var errorcnt = 0;
        $('.trquickadd').each(function () {
            $(this).find('.clsQty').css('border', '');
            $(this).find('.clsScientist').css('border', '');
            var qty = $(this).find('.clsQty').val();
            var sci = $(this).find('.clsScientist').val();
            if (qty === "" || qty == "0" || qty.trim() == "") {
                errorcnt += 1;
                $(this).find('.clsQty').css('border', '1px solid red');
            }
            if (sci === "") {
                errorcnt += 1;
                $(this).find('.clsScientist').css('border', '1px solid red');
            }
        });

        if (errorcnt !== 0) {
            return false;
        }

        var formdata = $("#frmquickadd").serializeObject();
        $.ajax({
            url: '/Form/SaveQuickAddDataQuotation',
            data: formdata,
            type: 'POST',
            success: function (data) {
                window.location.href = "../quote/quotationlist";
                if (data.success) {
                    toastr.success("Your have added new product.");
                }
                window.location.reload();
            }
        });
    },

    //getPreviousQuoteInformationByCatNoForPrice: function (catNo, casNo) {

    //    if (catNo === '') {
    //        //swal("Oops", "Catalog number not found", "error");
    //        //return false;
    //        catNo = casNo;
    //        if (catNo === '') {
    //            toastr.error("Catalog number not found");
    //            return false;
    //        }
    //    }

    //    $("#dvPreviousProductList").html("<div style='text-align: center;'><img src='/images/loader.gif' style='height: 170px;'/></div>");
    //    $.ajax({
    //        url: '/Form/getPreviousInfoFromDB?ProductName=' + catNo + '&casno=&catNo=&company=' + $("#CompanyId").val(),
    //        data: {},
    //        type: 'POST',
    //        async: true,
    //        cache: true,
    //        success: function (data) {
    //            $("#dvPreviousProductList").html(data);



    //            if ($('input[name=CountryType]:checked').val() === "Export") {
    //                $("#filtype").val("Export");
    //            }
    //            else {
    //                $("#filtype").val("Domestic");
    //            }

    //            $("#filtype").trigger('change');
    //        }
    //    });
    //},

    getPreviousQuoteInformationByCatNoForPrice: function (catNo, casNo) {

        if (catNo === '') {
            catNo = casNo;
            if (catNo === '') {
                toastr.error("Catalog number not found");
                return false;
            }
        }

        $("#dvPreviousProductList").html("<div style='text-align: center;'><img src='/images/loader.gif' style='height: 170px;'/></div>");
        $.ajax({
            url: '/Form/getPreviousInfoFromDB?ProductName=' + catNo + '&casno=&catNo=&company=' + $("#CompanyId").val(),
            data: {},
            type: 'POST',
            async: true,
            cache: true,
            success: function (data) {
                $("#dvPreviousProductList").html(data);

                if ($('input[name=CountryType]:checked').val() === "Export") {
                    $("#filtype").val("Export");
                }
                else {
                    $("#filtype").val("Domestic");
                }

                $("#filtype").trigger('change');
            }
        });
    },

    removeFollowupOverview: function () {
        var ids = [];

        $('.clsFollowOverview').each(function () {
            if ($(this).is(':checked')) {
                var id = $(this).val();
                ids.push(id);
            }
        });

        if (ids.length === 0) {
            toastr.error("Please select atleast one Product");
            return false;
        }
        $.ajax({
            url: '/Form/removeFollowupOverview',
            data: { id: ids },
            type: 'POST',
            traditional: true, // add this
            dataType: 'json',
            success: function (data) {
                if (data.success) {
                    toastr.success("You have removed quote from followup section.");

                    window.location.reload(true);
                }
            }
        });
    },

    saveFollowupOverview: function () {
        var quotes = [];

        $('.clsFollowOverview').each(function () {
            if ($(this).is(':checked')) {
                var id = $(this).val();
                quotes.push({
                    Id: id,
                    QuoteTotal: $("#quotetotal_" + id).val(),
                    FollowUpComment: $("#followupcomment_" + id).val()
                });
            }
        });

        if (quotes.length === 0) {
            toastr.error("Please select atleast one Product");
            return false;
        }
        quotes = JSON.stringify({ 'quotes': quotes });
        $.ajax({
            url: '/Form/SaveFollowupOverview',
            data: quotes,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.success) {
                    toastr.success("You have updated quote information.");
                }
            }
        });
    }
};


function isNumber(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function formate(date) {
    if (typeof date == "string")
        date = new Date(date);

    var day = (date.getDate() <= 9 ? "0" + date.getDate() : date.getDate());
    var month = (date.getMonth() + 1 <= 9 ? "0" + (date.getMonth() + 1) : (date.getMonth() + 1));
    var dateString = day + "/" + month + "/" + date.getFullYear();

    return dateString;
}

//recentadditiontab  instocktab   synthesistab purchasetab

var noactionload = false;
var cancelledload = false;
var synthesisload = false;
var domesticload = false;
var assignedload = false;
var controlledsubstanceload = false;
function noactiontab() {
    if (noactionload === false) {
        var noactiontbl = $('#tblnoaction').DataTable({
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "scrollX": true,
            "scrollY": "550px",
            "fnDrawCallback": function (oSettings) {
                $(".clsSaverownoaction").click(function () {
                    if ($(this).is(":checked")) {
                        $('#tblnoaction').find('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $('#tblnoaction').find(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
                $('#tblnoaction').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });

                $('#tblnoaction').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '') {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });

                var api = this.api();
                var rows = api.rows({ page: 'current' }).nodes();
                var last = null;
                var groupingCounts = [];
                var counter = 1;
                api.column(7, { page: 'current' }).data().each(function (group, i) {

                    if (last !== group) {
                        if (last !== undefined) {
                            groupingCounts[last] = counter;
                        }
                        $(rows).eq(i).before(
                            '<td class="group" colspan="24" style="' + (rows[i].attributes["data-IsConversionReport"] != null && rows[i].attributes["data-IsConversionReport"].value == "true" ? "BACKGROUND-COLOR:#95647d" : "BACKGROUND-COLOR:#3c8dbc") + ';font-weight:700;color:#fff;padding:10px;"><a style="color: #fff; text-decoration: underline;"  href="javascript:void(0)" onclick="Multiplequoteopen(' + rows[i].attributes["data-QuoteId"].value + ')">' + group + "</a> | " + rows[i].attributes["data-company"].value + ' | ' + rows[i].attributes["data-podate"].value + ' </td>'
                        );

                        last = group;
                        counter = 1;
                    } else {
                        counter++;
                    }
                });

                noactiontbl.columns.adjust();
                noactionload = true;

                $(document).on("change", ".clsProjType", function () {
                    var value = $(this).val();
                    var id = $(this).attr('id').replace('projecttype_', '');
                    if (value === '3') {
                        $('#pursummary_' + id).css('display', 'block');
                    }
                    else {
                        $('#pursummary_' + id).css('display', 'none');
                    }
                });

            },
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadProductNoActionData",
                "type": "POST",
                "datatype": "json"
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.ReadyToDeliverScientistStatusId === aData.ScientistStatus) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                    $('td', nRow).css('background-color', '#ffe6b3');
                }
                if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                    $('td', nRow).css('background-color', '#ffccb3');
                }
                if (aData.MoveToInvoice) {
                    $('td', nRow).css('background-color', '#ff9999');
                }
                if (aData.IsControlledSubstance) {
                    $('td', nRow).css('color', 'red');
                }
                if (aData.SelectedProStatus === "Attention") {
                    $('td', nRow).css('color', 'blue');
                }
            },
            "columnDefs": [{ targets: 3, visible: false }, { targets: 7, visible: false },
            { targets: 8, visible: false }],
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                console.log('ac');
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);
                $(row).attr('data-QuoteId', data.QuoteId);
                $(row).attr('data-pono', data.PONumber);
                $(row).attr('data-email', data.EmailAddress);
                $(row).attr('data-ProductCount', data.ProductCount);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-IsConversionReport', data.IsConversionReport);
            },
            "columns": [
                { "data": "ChkSaveRow", "autoWidth": true },
                { "data": "ChkFirstRow", "autoWidth": true },
                //{ "data": "LastRowText", "autoWidth": true },
                { "data": "SrPo", "autoWidth": true },
                { "data": "MoveProjectDateText", "name": "MoveProjectDateText" },
                { "data": "LeadTime" },
                { "data": "IsPriorityText" },
                { "data": "DifficultyLevelText" },
                { "data": "PONumber", "name": "PONumber" },
                { "data": "CompanyName", "name": "CompanyName" },
                { "data": "ProjectTypeText", "orderable": false },
                { "data": "ScientistName", "orderable": false },
                { "data": "SubScientistName", "orderable": false },
                { "data": "MoveToScientistDateStr", "orderable": false },
                { "data": "ProductName" },
                { "data": "CASNo", "name": "CASNo", width: '150px' },
                { "data": "CATNo", "name": "CATNo", width: '100px' },
                { "data": "RequiredQtyTxt", "name": "Ref", className: "fontbold" },
                { "data": "EstimateCompleteDateText", "orderable": false },
                { "data": "AdditionalBatchNoText", "name": "ProductName" },
                //{ "data": "GetAllBatch", "name": "GetAllBatch" },
                //{ "data": "ScientistStatustext", "orderable": false },

                //                { "data": "BatchNoText", "name": "Price", "autoWidth": true },
                //{ "data": "ProjectStatustext", "orderable": false, "autoWidth": true },
                //              { "data": "ScientistRemark", "orderable": false, "autoWidth": true },

                { "data": "RemarkText", "orderable": false },
                { "data": "ReasonText", "orderable": false },
                { "data": "OrderRemarkText", "orderable": false },
                { "data": "ExplainationText", "orderable": false }
            ]
        });
    }
}
var watchListload = false;
function watchlisttab() {
    if (watchListload === false) {
        var watchListtbl = $('#tblwatchList').dataTable({
            "searching": false,
            "ordering": false,
            "scrollX": true,
            "scrollY": "550px",
            "sPaginationType": "listbox",
            //"autoWidth" : false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "pageLength": 25,
            "fnDrawCallback": function (oSettings) {
                
                $("#selectWatchclsMoveDispatch").click(function () {
                    if ($(this).is(":checked")) {
                        $('.clsMoveDispatch').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $(".clsMoveDispatch").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });

                $("#selectWatchclsSaverow").click(function () {
                    if ($(this).is(":checked")) {
                        $('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
                $('#tblwatchList').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });


                $('#tblwatchList').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '') {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });

                var api = this.api();
                var rows = api.rows({ page: 'current' }).nodes();
                var last = null;
                var groupingCounts = [];
                var counter = 1;
                api.column(7, { page: 'current' }).data().each(function (group, i) {
                    if (last !== group) {
                        if (last !== undefined) {
                            groupingCounts[last] = counter;
                        }
                        $(rows).eq(i).before(
                            //'<td class="group" colspan="26" style="color:#fff;font-weight:700;padding:10px;' + (rows[i].attributes["data-IsPaymentPending"].value === "true" ? "BACKGROUND-COLOR:#e25b5b;" : "BACKGROUND-COLOR:#3c8dbc;") + '"><a style="color: #fff; text-decoration: underline;" target="_blank" href="/Form/Quote/' + rows[i].attributes["data-QuoteId"].value + '">' + group + "</a> | " + rows[i].attributes["data-company"].value + ' | ' + rows[i].attributes["data-podate"].value + ' ' + (rows[i].attributes["data-IsPaymentPending"].value === "true" ? "(Payment Pending)" : "") + " " + (rows[i].attributes["data-PaymentTerms"] === undefined || rows[i].attributes["data-PaymentTerms"].value === "" ? "" : " | Payment Terms : " + rows[i].attributes["data-PaymentTerms"].value)
                            '<td class="group" id="grouping_' + group.trim() + '" data-groupname="' + group + '" colspan="36" style="color:#fff;font-weight:700;padding:10px;' + (rows[i].attributes["data-IsConversionReport"] != null && rows[i].attributes["data-IsConversionReport"].value == "true" ? "BACKGROUND-COLOR:#95647d" : (rows[i].attributes["data-IsPaymentPending"].value === "true" ? "BACKGROUND-COLOR:#e25b5b;" : "BACKGROUND-COLOR:#3c8dbc;")) + '"><div style="width:28.5%;float:left"><i class="fa fa-copy" onclick="copyToClipboard(&#39;' + group + '&#39;)"></i> <a style="color: #fff; text-decoration: underline;"  href="javascript:void(0)" onclick="Multiplequoteopen(' + rows[i].attributes["data-QuoteId"].value + ')">' + group + "</a> | " + rows[i].attributes["data-company"].value + '  <i class="fa fa-map-marker" onclick="showquoteaddress(' + rows[i].attributes["data-QuoteId"].value + ')"></i> | ' + (rows[i].attributes["data-SAPCode"] === null ? "" : rows[i].attributes["data-SAPCode"].value) + '  | ' + (rows[i].attributes["data-ProjectAssignName"] === null ? "" : rows[i].attributes["data-ProjectAssignName"].value) + '</div><div style="float:left">' + rows[i].attributes["data-podate"].value + ' ' + (rows[i].attributes["data-IsPaymentPending"].value === "true" ? "(Payment Pending)" : "") + " " + (rows[i].attributes["data-PaymentTerms"] === undefined || rows[i].attributes["data-PaymentTerms"].value === "" ? "" : " | Payment Terms : " + rows[i].attributes["data-PaymentTerms"].value) + ' | <i class="fa fa-envelope" onclick="opensendemailpopup(' + rows[i].attributes["data-QuoteId"].value + ')"></i></div>'
                            + ' </td>'
                        );

                        last = group;
                        counter = 1;
                    } else {
                        counter++;
                    }
                });

                //alltbl.columns.adjust();
                $('.clstooltip').tooltip();

                groupingCounts[last] = counter;
                CountRows(groupingCounts, 'tblwatchList');

                //watchListtbl.columns.adjust();
                watchListload = true;
            },
            "columnDefs":
                [
                    { targets: 4, visible: false },
                    { targets: 7, visible: false },
                    { targets: 8, visible: false }
                ],
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },

            "ajax": {
                "url": "../Form/LoadWatchListProductData",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.sciname = $("#watchlist").find("#ScientistListItem").val();
                    d.subsciname = $("#watchlist").find("#SubScientistListItem").val();
                    d.fltprostatusItem = $("#watchlist").find("#fltprostatusItem").val();
                    d.fltactivity = $("#watchlist").find("#fltactivity").val();
                    d.radiobuton = $("#watchlist").find('input[name="filterAll"]:checked').val();
                    d.searchbox = $("#watchlist").find("#allsearchbox").val();
                    d.fltprotypeItem = $("#watchlist").find("#fltprotypeItem").val();
                    d.fltestdate = $("#watchlist").find("#fltestdate").val();
                    d.fltpriority = $("#watchlist").find("#chkallpriority").is(":checked");
                    d.fltgeographicallocation = $("#watchlist").find("#fltgeographicallocation").val();
                    d.fltglp = $("#watchlist").find("#fltglp").val();
                    d.fltProjectTeam = $("#watchlist").find("#ProjectTeam").val();
                }
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.SelectedProjectType === "In-Stock") {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                    $('td', nRow).css('background-color', '#ffe6b3');
                }
                if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                    $('td', nRow).css('background-color', 'rgb(250, 209, 255)');
                }
                if (aData.MoveToInvoice) {
                    $('td', nRow).css('background-color', '#ff9999');
                }
                if (aData.IsControlledSubstance) {
                    $('td', nRow).css('color', 'red');
                }
                if (aData.SelectedProStatus === "Attention") {
                    $('td', nRow).css('color', 'blue');
                }
            },
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                $(row).attr('data-IsConversionReport', data.IsConversionReport);
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-email', data.EmailAddress);
                $(row).attr('data-company', data.CompanyName);
                $(row).attr('data-companyId', data.CompanyId);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-pono', data.PONumber);
                $(row).attr('data-ProductCount', data.ProductCount);
                $(row).attr('data-PaymentTerms', data.PaymentTerms);
                $(row).attr('data-QuoteId', data.QuoteId);
                $(row).attr('data-SAPCode', (data.SAPCode == null ? "" : data.SAPCode));
                $(row).attr('data-IsPaymentPending', (data.IsPaymentPending == null ? false : data.IsPaymentPending));
                $(row).attr('data-ProjectAssignName', (data.ProjectAssignName == null ? "" : data.ProjectAssignName));
            },
            "columns": [
                { "data": "ChkSaveRow" },
                { "data": "ChkFirstRow" },
                { "data": "ApprovedForClientText" },
                //{ "data": "LastRowText", "autoWidth": true },
                { "data": "SrPo" },
                { "data": "MoveProjectDateText", "name": "MoveProjectDateText" },
                /*{ "data": "LeadTime" },*/
                { "data": "IsPriorityText" },
                { "data": "DifficultyLevelText" },
                { "data": "PONumber", "name": "PONumber" },
                { "data": "CompanyName", "name": "CompanyName" },
                { "data": "ProjectTypeText", "orderable": false },
                { "data": "ScientistName", "orderable": false },
                { "data": "SubScientistName", "orderable": false },
                { "data": "InhouseRemarkText", "orderable": false },
                { "data": "LeadTime" },
                { "data": "SynStartDate", "name": "SynStartDate" },
                { "data": "MoveToScientistDateStr", "orderable": false },
                { "data": "ProductScheme" },
                { "data": "SAPRawMaterial" },
                { "data": "ProductName" },
                { "data": "CASNo", "name": "CASNo", width: '150px' },
                { "data": "CATNo", "name": "CATNo", width: '100px' },
                { "data": "Attachment", "name": "Attachment" },
                { "data": "RequiredQtyTxt", "name": "Ref", className: "fontbold", },
                { "data": "AdditionalBatchNoText", "name": "ProductName", width: '150px' },
                //{ "data": "COARefNumber", "name": "COARefNumber" },
                { "data": "COAText", "name": "COAText" },
                { "data": "GetAllBatch", "name": "GetAllBatch" },
                { "data": "ReactionMatrix", "name": "ReactionMatrix" },
                { "data": "ActivityStatusText", "orderable": false },
                { "data": "ProStatusText", "orderable": false },
                { "data": "EstimateCompleteDateText", "orderable": false },
                { "data": "CountEstDate", "orderable": false },
                { "data": "ExplainationSecondText", "orderable": false },
                { "data": "ExplainationText", "orderable": false },
                { "data": "RemarkText", "orderable": false },
                { "data": "ReasonText", "orderable": false },
                { "data": "TechnicalEmail", "orderable": false },
                { "data": "OrderRemarkText", "orderable": false },
                { "data": "GLP", "orderable": false },
                { "data": "GLPRemark", "orderable": false },
                { "data": "GLPStatus", "orderable": false },
            ]
        });
    }
}

function assignedtab() {
    if (assignedload === false) {
        var assignedtbl = $('#tblassigned').DataTable({
            "searching": false,
            "ordering": false,
            "scrollX": true,
            "scrollY": "550px",
            "sPaginationType": "listbox",
            //"autoWidth" : false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "pageLength": 25,
            "fnDrawCallback": function (oSettings) {
                $("#selectAllclsMoveDispatch").click(function () {
                    if ($(this).is(":checked")) {
                        $('.clsMoveDispatch').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $(".clsMoveDispatch").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });

                $("#selectAllclsSaverow").click(function () {
                    if ($(this).is(":checked")) {
                        $('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
                $('#tblassigned').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });


                $('#tblassigned').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '') {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });

                var api = this.api();
                var rows = api.rows({ page: 'current' }).nodes();
                var last = null;
                var groupingCounts = [];
                var counter = 1;
                api.column(7, { page: 'current' }).data().each(function (group, i) {
                    if (last !== group) {
                        if (last !== undefined) {
                            groupingCounts[last] = counter;
                        }
                        $(rows).eq(i).before(
                            //'<td class="group" colspan="26" style="color:#fff;font-weight:700;padding:10px;' + (rows[i].attributes["data-IsPaymentPending"].value === "true" ? "BACKGROUND-COLOR:#e25b5b;" : "BACKGROUND-COLOR:#3c8dbc;") + '"><a style="color: #fff; text-decoration: underline;" target="_blank" href="/Form/Quote/' + rows[i].attributes["data-QuoteId"].value + '">' + group + "</a> | " + rows[i].attributes["data-company"].value + ' | ' + rows[i].attributes["data-podate"].value + ' ' + (rows[i].attributes["data-IsPaymentPending"].value === "true" ? "(Payment Pending)" : "") + " " + (rows[i].attributes["data-PaymentTerms"] === undefined || rows[i].attributes["data-PaymentTerms"].value === "" ? "" : " | Payment Terms : " + rows[i].attributes["data-PaymentTerms"].value)
                            '<td class="group" id="grouping_' + group.trim() + '" data-groupname="' + group + '" colspan="30" style="color:#fff;font-weight:700;padding:10px;' + (rows[i].attributes["data-IsConversionReport"] != null && rows[i].attributes["data-IsConversionReport"].value == "true" ? "BACKGROUND-COLOR:#95647d" : (rows[i].attributes["data-IsPaymentPending"].value === "true" ? "BACKGROUND-COLOR:#e25b5b;" : "BACKGROUND-COLOR:#3c8dbc;")) + '"><div style="width:28.5%;float:left"><i class="fa fa-copy" onclick="copyToClipboard(&#39;' + group + '&#39;)"></i> <a style="color: #fff; text-decoration: underline;"  href="javascript:void(0)" onclick="Multiplequoteopen(' + rows[i].attributes["data-QuoteId"].value + ')">' + group + "</a> | " + rows[i].attributes["data-company"].value + '  <i class="fa fa-map-marker" onclick="showquoteaddress(' + rows[i].attributes["data-QuoteId"].value + ')"></i> | ' + (rows[i].attributes["data-SAPCode"] === null ? "" : rows[i].attributes["data-SAPCode"].value) + ' </div><div style="float:left">' + rows[i].attributes["data-podate"].value + ' ' + (rows[i].attributes["data-IsPaymentPending"].value === "true" ? "(Payment Pending)" : "") + " " + (rows[i].attributes["data-PaymentTerms"] === undefined || rows[i].attributes["data-PaymentTerms"].value === "" ? "" : " | Payment Terms : " + rows[i].attributes["data-PaymentTerms"].value) + ' | <i class="fa fa-envelope" onclick="opensendemailpopup(' + rows[i].attributes["data-QuoteId"].value + ')"></i></div>'
                            + ' </td>'
                        );

                        last = group;
                        counter = 1;
                    } else {
                        counter++;
                    }
                });

                assignedtbl.columns.adjust();
                $('.clstooltip').tooltip();

                groupingCounts[last] = counter;
                CountRows(groupingCounts, 'example1');
            },
            "columnDefs":
                [
                    { targets: 4, visible: false },
                    { targets: 7, visible: false },
                    { targets: 8, visible: false }
                ],
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadProductAssignedData",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.sciname = $("#tabassigned").find("#ScientistListItem").val();
                    d.subsciname = $("#tabassigned").find("#SubScientistListItem").val();
                    d.fltprostatusItem = $("#tabassigned").find("#fltprostatusItem").val();
                    d.fltactivity = $("#tabassigned").find("#fltactivity").val();
                    d.radiobuton = $("#tabassigned").find('input[name="filterAll"]:checked').val();
                    d.searchbox = $("#tabassigned").find("#allsearchbox").val();
                    d.fltprotypeItem = $("#tabassigned").find("#fltprotypeItem").val();
                    d.fltestdate = $("#tabassigned").find("#fltestdate").val();
                }
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.SelectedProjectType === "In-Stock") {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                    $('td', nRow).css('background-color', '#ffe6b3');
                }
                if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                    $('td', nRow).css('background-color', 'rgb(250, 209, 255)');
                }
                if (aData.MoveToInvoice) {
                    $('td', nRow).css('background-color', '#ff9999');
                }
                if (aData.IsControlledSubstance) {
                    $('td', nRow).css('color', 'red');
                }
                if (aData.SelectedProStatus === "Attention") {
                    $('td', nRow).css('color', 'blue');
                }
            },
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-email', data.EmailAddress);
                $(row).attr('data-company', data.CompanyName);
                $(row).attr('data-companyId', data.CompanyId);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-pono', data.PONumber);
                $(row).attr('data-ProductCount', data.ProductCount);
                $(row).attr('data-PaymentTerms', data.PaymentTerms);
                $(row).attr('data-QuoteId', data.QuoteId);
                $(row).attr('data-SAPCode', (data.SAPCode == null ? "" : data.SAPCode));
                $(row).attr('data-IsPaymentPending', (data.IsPaymentPending == null ? false : data.IsPaymentPending));
                $(row).attr('data-IsConversionReport', data.IsConversionReport);
            },
            "columns": [
                { "data": "ChkSaveRow" },
                { "data": "ChkFirstRow" },
                { "data": "ApprovedForClientText" },
                //{ "data": "LastRowText", "autoWidth": true },
                { "data": "SrPo" },
                { "data": "MoveProjectDateText", "name": "MoveProjectDateText" },
                /*{ "data": "LeadTime" },*/
                { "data": "IsPriorityText" },
                { "data": "DifficultyLevelText" },
                { "data": "PONumber", "name": "PONumber" },
                { "data": "CompanyName", "name": "CompanyName" },
                { "data": "ProjectTypeText", "orderable": false },
                { "data": "ScientistName", "orderable": false },
                { "data": "SubScientistName", "orderable": false },
                { "data": "InhouseRemarkText", "orderable": false },
                { "data": "LeadTime" },
                { "data": "MoveToScientistDateStr", "orderable": false },
                { "data": "ProductScheme" },
                { "data": "ProductName" },
                { "data": "CASNo", "name": "CASNo", width: '150px' },
                { "data": "CATNo", "name": "CATNo", width: '100px' },
                { "data": "Attachment", "name": "Attachment" },
                { "data": "RequiredQtyTxt", "name": "Ref", className: "fontbold", },
                { "data": "AdditionalBatchNoText", "name": "ProductName", width: '150px' },
                //{ "data": "COARefNumber", "name": "COARefNumber" },
                { "data": "COAText", "name": "COAText" },
                { "data": "GetAllBatch", "name": "GetAllBatch" },
                { "data": "ActivityStatusText", "orderable": false },
                { "data": "ProStatusText", "orderable": false },
                { "data": "EstimateCompleteDateText", "orderable": false },
                { "data": "CountEstDate", "orderable": false },
                { "data": "ExplainationSecondText", "orderable": false },
                { "data": "ExplainationText", "orderable": false },
                { "data": "RemarkText", "orderable": false },
                { "data": "ReasonText", "orderable": false },
                { "data": "OrderRemarkText", "orderable": false },
            ]
        });
    }
}


function cancelledproducttab() {
    if (cancelledload === false) {
        var cancelledtbl = $('#tblcancelledproduct').DataTable({
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "scrollX": true,
            "scrollY": "550px",
            "fnDrawCallback": function (oSettings) {
                $("#selectAllclsSaverow").click(function () {
                    if ($(this).is(":checked")) {
                        $('#tblcancelledproduct').find('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $('#tblcancelledproduct').find(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
                $('#tblcancelledproduct').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });

                $('#tblcancelledproduct').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '') {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });

                var api = this.api();
                var rows = api.rows({ page: 'current' }).nodes();
                var last = null;
                var groupingCounts = [];
                var counter = 1;
                api.column(7, { page: 'current' }).data().each(function (group, i) {

                    if (last !== group) {
                        if (last !== undefined) {
                            groupingCounts[last] = counter;
                        }
                        $(rows).eq(i).before(
                            '<td class="group" colspan="28" style="' + (rows[i].attributes["data-IsConversionReport"] != null && rows[i].attributes["data-IsConversionReport"].value == "true" ? "BACKGROUND-COLOR:#95647d" : "BACKGROUND-COLOR:#3c8dbc") +';font-weight:700;color:#fff;padding:10px;">' + group + " | " + rows[i].attributes["data-company"].value + ' | ' + rows[i].attributes["data-podate"].value + ' </td>'
                        );

                        last = group;
                        counter = 1;
                    } else {
                        counter++;
                    }
                });

                cancelledtbl.columns.adjust();
                cancelledload = true;
            },
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadProductCancelledData",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.sciname = $("#tabcancelledproduct").find("#ScientistListItem").val();
                    d.subsciname = $("#tabcancelledproduct").find("#SubScientistListItem").val();
                    d.fltprostatusItem = $("#tabcancelledproduct").find("#fltprostatusItem").val();
                    d.fltactivity = $("#tabcancelledproduct").find("#fltactivity").val();
                    d.radiobuton = $("#tabcancelledproduct").find('input[name="filterAll"]:checked').val();
                    d.searchbox = $("#tabcancelledproduct").find("#allsearchbox").val();
                }
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.ReadyToDeliverScientistStatusId === aData.ScientistStatus) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                    $('td', nRow).css('background-color', '#ffe6b3');
                }
                if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                    $('td', nRow).css('background-color', '#ffccb3');
                }
                if (aData.MoveToInvoice) {
                    $('td', nRow).css('background-color', '#ff9999');
                }
                if (aData.IsControlledSubstance) {
                    $('td', nRow).css('color', 'red');
                }
                if (aData.SelectedProStatus === "Attention") {
                    $('td', nRow).css('color', 'blue');
                }
            },
            "columnDefs": [{ targets: 3, visible: false }, { targets: 7, visible: false },
            { targets: 8, visible: false }],
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                console.log('ac');
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);

                $(row).attr('data-pono', data.PONumber);
                $(row).attr('data-email', data.EmailAddress);
                $(row).attr('data-ProductCount', data.ProductCount);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-IsConversionReport', data.IsConversionReport);
            },
            "columns": [
                { "data": "ChkSaveRow" },
                //{ "data": "LastRowText", "autoWidth": true },
                { "data": "SrPo" },
                { "data": "MoveProjectDateText", "name": "MoveProjectDateText" },
                { "data": "LeadTime" },
                { "data": "IsPriorityText" },
                { "data": "DifficultyLevelText" },
                { "data": "PONumber", "name": "PONumber" },
                { "data": "CompanyName", "name": "CompanyName" },
                { "data": "ProjectTypeText", "orderable": false },
                { "data": "ScientistName", "orderable": false },
                { "data": "SubScientistName", "orderable": false },
                { "data": "MoveToScientistDateStr", "orderable": false },
                { "data": "ProductName" },
                { "data": "CASNo", "name": "CASNo", width: '150px' },
                { "data": "CATNo", "name": "CATNo", width: '100px' },
                { "data": "Attachment", "name": "Attachment" },
                { "data": "RequiredQtyTxt", "name": "Ref", className: "fontbold", },
                { "data": "EstimateCompleteDateText", "orderable": false },
                { "data": "AdditionalBatchNoText", "name": "ProductName" },
                //{ "data": "COARefNumber", "name": "COARefNumber" },
                { "data": "COAText", "name": "COAText" },
                { "data": "GetAllBatch", "name": "GetAllBatch" },
                { "data": "ActivityStatusText", "orderable": false },
                { "data": "ProStatusText", "orderable": false },
                //{ "data": "ScientistStatustext", "orderable": false },
                { "data": "ExplainationSecondText", "orderable": false },
                { "data": "ExplainationText", "orderable": false },
                { "data": "RemarkText", "orderable": false },
                { "data": "ReasonText", "orderable": false },
                { "data": "OrderRemarkText", "orderable": false },
            ]
        });
    }
}


function controlledsubstancetab() {
    if (controlledsubstanceload === false) {
        var controlledsubstancetbl = $('#tblcontrolledsubstance').DataTable({
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "scrollX": true,
            "scrollY": "550px",
            "fnDrawCallback": function (oSettings) {
                $(".clscontsubstance").click(function () {
                    if ($(this).is(":checked")) {
                        $('#tblcontrolledsubstance').find('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $('#tblcontrolledsubstance').find(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
                $('#tblcontrolledsubstance').find('.clsApplicationDate').datepicker({
                    format: 'dd/mm/yyyy'
                });
                $('#tblcontrolledsubstance').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });
                $('#tblcontrolledsubstance').find('.clsExportPermitReceivedDate').datepicker({
                    format: 'dd/mm/yyyy'
                }); $('#tblcontrolledsubstance').find('.clsExportPermitReceivedDate').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '' && dateValue !== undefined) {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });
                $('#tblcontrolledsubstance').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '' && dateValue !== undefined) {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });
                $('#tblcontrolledsubstance').find('.clsApplicationDate').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '' && dateValue !== undefined) {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });

                var api = this.api();
                var rows = api.rows({ page: 'current' }).nodes();
                var last = null;
                var groupingCounts = [];
                var counter = 1;
                api.column(1, { page: 'current' }).data().each(function (group, i) {

                    if (last !== group) {
                        if (last !== undefined) {
                            groupingCounts[last] = counter;
                        }
                        $(rows).eq(i).before(
                            '<td class="group" colspan="29" style="' + (rows[i].attributes["data-IsConversionReport"] != null && rows[i].attributes["data-IsConversionReport"].value == "true" ? "BACKGROUND-COLOR:#95647d" : "BACKGROUND-COLOR:#3c8dbc") +';font-weight:700;color:#fff;padding:10px;">' + group + " | " + rows[i].attributes["data-company"].value + ' | ' + rows[i].attributes["data-podate"].value + ' </td>'
                        );

                        last = group;
                        counter = 1;
                    } else {
                        counter++;
                    }
                });

                controlledsubstancetbl.columns.adjust();
                controlledsubstanceload = true;
            },
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadProductControlledsubstanceData",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.radiobuton = $("#tabcontrolledsubstance").find('input[name="filterAll"]:checked').val();
                    d.searchbox = $("#tabcontrolledsubstance").find("#allsearchbox").val();
                    if ($("#tabcontrolledsubstance").find("#fltLicesenStatus").val() !== undefined
                        && $("#tabcontrolledsubstance").find("#fltLicesenStatus").val() !== null) {
                        d.fltLicesenStatus = $("#tabcontrolledsubstance").find("#fltLicesenStatus").val().join();
                    }
                }
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.ReadyToDeliverScientistStatusId === aData.ScientistStatus) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                    $('td', nRow).css('background-color', '#ffe6b3');
                }
                if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                    $('td', nRow).css('background-color', '#ffccb3');
                }
                if (aData.MoveToInvoice) {
                    $('td', nRow).css('background-color', '#ff9999');
                }

                if (aData.SelectedProStatus === "Attention") {
                    $('td', nRow).css('color', 'blue');
                }
            },
            "columnDefs": [{ targets: 1, visible: false }],
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                console.log('ac');
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);

                $(row).attr('data-pono', data.PONumber);
                $(row).attr('data-email', data.EmailAddress);
                $(row).attr('data-ProductCount', data.ProductCount);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-IsConversionReport', data.IsConversionReport);
            },
            "columns": [
                { "data": "srNo", "name": "srNo" },
                { "data": "PONumber", "name": "PONumber" },
                { "data": "ChkSaveRow" },
                { "data": "ChkFirstRow" },
                { "data": "ProductName" },
                { "data": "CATNo", "name": "CATNo", width: '100px' },
                { "data": "CASNo", "name": "CASNo", width: '150px' },
                { "data": "RequiredQtyTxt", "name": "Ref", className: "fontbold", },
                { "data": "EstimateCompleteDateText", "orderable": false },
                { "data": "JourneyComment", "name": "JourneyComment" },
                { "data": "Category", "name": "Category" },
                { "data": "ImpExpo", "name": "ImpExpo" },
                { "data": "LicesenStatusText", "name": "LicesenStatusText" },
                { "data": "PermitRequired", "name": "PermitRequired" },
                { "data": "ImportPermit", "name": "ImportPermit" },
                { "data": "Declaration", "name": "Declaration" },
                { "data": "ApplicationDateText", "name": "ApplicationDateText" },
                { "data": "ExportPermitReceivedDateText", "name": "ExportPermitReceivedDateText" },
                { "data": "TechnicalWriteup", "name": "TechnicalWriteup" },

                { "data": "QuaterlyDataToSubmit", "name": "QuaterlyDataToSubmit" },
                { "data": "QuaterlyDataSubmited", "name": "QuaterlyDataSubmited" },
                { "data": "NextQuater", "name": "NextQuater" },
                { "data": "AdditionalBatchNoText", "name": "ProductName" },
                { "data": "APIRequired", "name": "APIRequired" },
                { "data": "APIImpExport", "name": "APIImpExport" },
                { "data": "APIName", "name": "APIName" },
                { "data": "ActionRow", "name": "ActionRow" }
            ]
        });
    }
}

var completedcontrolledsubstanceload = false;
function completedcontrolledsubstancetab() {
    if (completedcontrolledsubstanceload === false) {
        var controlledsubstancetbl = $('#tblcompletedcontrolledsubstance').DataTable({
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "scrollX": true,
            "scrollY": "550px",
            "fnDrawCallback": function (oSettings) {
                $(".clscontsubstance").click(function () {
                    if ($(this).is(":checked")) {
                        $('#tblcompletedcontrolledsubstance').find('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $('#tblcompletedcontrolledsubstance').find(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
                $('#tblcompletedcontrolledsubstance').find('.clsApplicationDate').datepicker({
                    format: 'dd/mm/yyyy'
                });
                $('#tblcompletedcontrolledsubstance').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });
                $('#tblcompletedcontrolledsubstance').find('.clsExportPermitReceivedDate').datepicker({
                    format: 'dd/mm/yyyy'
                }); $('#tblcompletedcontrolledsubstance').find('.clsExportPermitReceivedDate').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '' && dateValue !== undefined) {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });
                $('#tblcompletedcontrolledsubstance').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '' && dateValue !== undefined) {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });
                $('#tblcompletedcontrolledsubstance').find('.clsApplicationDate').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '' && dateValue !== undefined) {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });

                var api = this.api();
                var rows = api.rows({ page: 'current' }).nodes();
                var last = null;
                var groupingCounts = [];
                var counter = 1;
                api.column(1, { page: 'current' }).data().each(function (group, i) {

                    if (last !== group) {
                        if (last !== undefined) {
                            groupingCounts[last] = counter;
                        }
                        $(rows).eq(i).before(
                            '<td class="group" colspan="29" style="' + (rows[i].attributes["data-IsConversionReport"] != null && rows[i].attributes["data-IsConversionReport"].value == "true" ? "BACKGROUND-COLOR:#95647d" : "BACKGROUND-COLOR:#3c8dbc") +';font-weight:700;color:#fff;padding:10px;">' + group + " | " + rows[i].attributes["data-company"].value + ' | ' + rows[i].attributes["data-podate"].value + ' </td>'
                        );

                        last = group;
                        counter = 1;
                    } else {
                        counter++;
                    }
                });

                controlledsubstancetbl.columns.adjust();
                completedcontrolledsubstanceload = true;
            },
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadProductControlledsubstanceData?iscompleted=true",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.radiobuton = $("#tabcompletedcontrolledsubstance").find('input[name="filterAll"]:checked').val();
                    d.searchbox = $("#tabcompletedcontrolledsubstance").find("#allsearchbox").val();
                    if ($("#tabcompletedcontrolledsubstance").find("#fltLicesenStatus").val() !== undefined
                        && $("#tabcompletedcontrolledsubstance").find("#fltLicesenStatus").val() !== null) {
                        d.fltLicesenStatus = $("#tabcompletedcontrolledsubstance").find("#fltLicesenStatus").val().join();
                    }
                }
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.ReadyToDeliverScientistStatusId === aData.ScientistStatus) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                    $('td', nRow).css('background-color', '#ffe6b3');
                }
                if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                    $('td', nRow).css('background-color', '#ffccb3');
                }
                if (aData.MoveToInvoice) {
                    $('td', nRow).css('background-color', '#ff9999');
                }

                if (aData.SelectedProStatus === "Attention") {
                    $('td', nRow).css('color', 'blue');
                }
            },
            "columnDefs": [{ targets: 1, visible: false }],
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                console.log('ac');
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);

                $(row).attr('data-pono', data.PONumber);
                $(row).attr('data-email', data.EmailAddress);
                $(row).attr('data-ProductCount', data.ProductCount);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-IsConversionReport', data.IsConversionReport);
            },
            "columns": [
                { "data": "srNo", "name": "srNo" },
                { "data": "PONumber", "name": "PONumber" },
                { "data": "ChkSaveRow" },
                { "data": "ChkFirstRow" },
                { "data": "ProductName" },
                { "data": "CATNo", "name": "CATNo", width: '100px' },
                { "data": "CASNo", "name": "CASNo", width: '150px' },
                { "data": "RequiredQtyTxt", "name": "Ref", className: "fontbold", },
                { "data": "EstimateCompleteDateText", "orderable": false },
                { "data": "JourneyComment", "name": "JourneyComment" },
                { "data": "Category", "name": "Category" },
                { "data": "ImpExpo", "name": "ImpExpo" },
                { "data": "LicesenStatusText", "name": "LicesenStatusText" },
                { "data": "PermitRequired", "name": "PermitRequired" },
                { "data": "ImportPermit", "name": "ImportPermit" },
                { "data": "Declaration", "name": "Declaration" },
                { "data": "ApplicationDateText", "name": "ApplicationDateText" },
                { "data": "ExportPermitReceivedDateText", "name": "ExportPermitReceivedDateText" },
                { "data": "TechnicalWriteup", "name": "TechnicalWriteup" },

                { "data": "QuaterlyDataToSubmit", "name": "QuaterlyDataToSubmit" },
                { "data": "QuaterlyDataSubmited", "name": "QuaterlyDataSubmited" },
                { "data": "NextQuater", "name": "NextQuater" },
                { "data": "AdditionalBatchNoText", "name": "ProductName" },
                { "data": "APIRequired", "name": "APIRequired" },
                { "data": "APIImpExport", "name": "APIImpExport" },
                { "data": "APIName", "name": "APIName" },
                { "data": "ActionRow", "name": "ActionRow" }
            ]
        });
    }
}


var glpload = false;
function glptab() {
    if (glpload === false) {
        var glptbl = $('#tblglp').DataTable({
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "scrollX": true,
            "scrollY": "550px",
            "fnDrawCallback": function (oSettings) {
                $('#tblglp').find('.clsApplicationDate').datepicker({
                    format: 'dd/mm/yyyy'
                });
                $('#tblglp').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });
                $('#tblglp').find('.clsExportPermitReceivedDate').datepicker({
                    format: 'dd/mm/yyyy'
                });
                $('#tblglp').find('.clsExportPermitReceivedDate').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '' && dateValue !== undefined) {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });
                $('#tblglp').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '' && dateValue !== undefined) {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });
                $('#tblglp').find('.clsApplicationDate').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '' && dateValue !== undefined) {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });

                

                //var api = this.api();
                //var rows = api.rows({ page: 'current' }).nodes();
                //var last = null;
                //var groupingCounts = [];
                //var counter = 1;
                //api.column(2, { page: 'current' }).data().each(function (group, i) {

                //    if (last !== group) {
                //        if (last !== undefined) {
                //            groupingCounts[last] = counter;
                //        }
                //        $(rows).eq(i).before(
                //            '<td class="group" colspan="29" style="BACKGROUND-COLOR:#3c8dbc;font-weight:700;color:#fff;padding:10px;">' + group + " | " + rows[i].attributes["data-company"].value + ' | ' + rows[i].attributes["data-podate"].value + ' </td>'
                //        );

                //        last = group;
                //        counter = 1;
                //    } else {
                //        counter++;
                //    }
                //});
                glptbl.columns.adjust();
                glpload = true;
            },
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadProductGLPData",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.radiobuton = $("#tblglp").find('input[name="filterAll"]:checked').val();
                    d.searchbox = $("#tblglp").find("#allsearchbox").val();
                    d.fltglpstatus = $("#tabglp").find("#fltglpstatus").val(); 
                    if ($("#tblglp").find("#fltLicesenStatus").val() !== undefined
                        && $("#tblglp").find("#fltLicesenStatus").val() !== null) {
                        d.fltLicesenStatus = $("#tblglp").find("#fltLicesenStatus").val().join();
                    }
                }
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.ReadyToDeliverScientistStatusId === aData.ScientistStatus) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                    $('td', nRow).css('background-color', '#ffe6b3');
                }
                if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                    $('td', nRow).css('background-color', '#ffccb3');
                }
                if (aData.MoveToInvoice) {
                    $('td', nRow).css('background-color', '#ff9999');
                }

                if (aData.SelectedProStatus === "Attention") {
                    $('td', nRow).css('color', 'blue');
                }
            },
            "columnDefs": [],
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                console.log('ac');
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);
                $(row).attr('data-pono', data.PONumber);
                $(row).attr('data-email', data.EmailAddress);
                $(row).attr('data-ProductCount', data.ProductCount);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-IsConversionReport', data.IsConversionReport);
            },
            "columns": [
                { "data": null },
                { "data": "ChkSaveRow" },
                { "data": "GLPAssignDateText" }, 
                { "data": "ProductName" },
                { "data": "SelectedAdditionalBatchNo", "name": "ProductName", width: '150px' },
                { "data": "CASNo", "name": "CASNo", width: '150px' },
                { "data": "CATNo", "name": "CATNo", width: '100px' },
                { "data": "SelectedScientistName", "orderable": false },
                { "data": "SelectedSubScientistName", "orderable": false },
                //{ "data": "ReceivedFrom", "orderable": false },
                { "data": "GLPStatus", "orderable": false },
                { "data": "AnalysisCompletionDate", "orderable": false },
                { "data": "GLPRemark", "orderable": false },
                { "data": "IsPriorityText" },
                { "data": "CompanyName", "name": "CompanyName" },
                { "data": "PONumber", "name": "PONumber" },
                { "data": "LeadTime", "name": "PONumber" },
                { "data": "AdditionalBatchNoText", "name": "PONumber" },
                { "data": "GLPStabilityPath" },
                { "data": "GLPAMVPath" },
                { "data": "ActionRow" }
            ]
        });

        glptbl.on('draw.dt', function () {
            var PageInfo = $('#tblglp').DataTable().page.info();
            glptbl.column(0, { page: 'current' }).nodes().each(function (cell, i) {
                cell.innerHTML = i + 1 + PageInfo.start;
            });
        });

    }
}

/// Load synthesis tab
function synthesistab() {
    if (synthesisload === false) {
        $('#tblsynthesis').DataTable({
            "searching": false,
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "fnDrawCallback": function (oSettings) {
                $(".clsSaverowsynthesis").click(function () {
                    if ($(this).is(":checked")) {
                        $('#tblsynthesis').find('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $('#tblsynthesis').find(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
                $('#tblsynthesis').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });

                $('#tblsynthesis').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '') {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });


                var api = this.api();
                var rows = api.rows({ page: 'current' }).nodes();
                var last = null;
                var groupingCounts = [];
                var counter = 1;
                api.column(4, { page: 'current' }).data().each(function (group, i) {

                    if (last !== group) {
                        if (last !== undefined) {
                            groupingCounts[last] = counter;
                        }
                        $(rows).eq(i).before(
                            '<td class="group" id="grouping_' + group.trim() + '" data-groupname="' + group + '" colspan="22" style="' + (rows[i].attributes["data-IsConversionReport"] != null && rows[i].attributes["data-IsConversionReport"].value == "true" ? "BACKGROUND-COLOR:#95647d" :"BACKGROUND-COLOR:#3c8dbc") +';font-weight:700;color:#fff;padding:10px;">' + group + " | " + rows[i].attributes["data-company"].value + ' | ' + rows[i].attributes["data-podate"].value + ' </td>'
                        );

                        last = group;
                        counter = 1;
                    } else {
                        counter++;
                    }
                });

                synthesisload = true;
                groupingCounts[last] = counter;
                CountRows(groupingCounts, 'tblsynthesis');
            },
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadProductSynthesisData",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.radiobuton = $("#tabsynthesis").find('input[name="filterAll"]:checked').val();
                    d.searchbox = $("#tabsynthesis").find("#allsearchbox").val();
                    d.sciname = $("#tabsynthesis").find("#ScientistListItem").val();
                    d.subsciname = $("#tabsynthesis").find("#SubScientistListItem").val();
                    d.fltprostatusItem = $("#tabsynthesis").find("#fltprostatusItem").val();
                    d.fltactivity = $("#tabsynthesis").find("#fltactivity").val();
                    d.fltprotypeItem = $("#tabsynthesis").find("#fltprotypeItem").val();
                    d.fltestdate = $("#tabsynthesis").find("#fltestdate").val();
                }
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                //if (aData.ReadyToDeliverScientistStatusId === aData.ScientistStatus) {
                //    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                //}
                if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                    $('td', nRow).css('background-color', '#ffe6b3');
                }
                if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                    $('td', nRow).css('background-color', '#ffccb3');
                }
                if (aData.MoveToInvoice) {
                    $('td', nRow).css('background-color', '#ff9999');
                }
                if (aData.IsControlledSubstance) {
                    $('td', nRow).css('color', 'red');
                }
                if (aData.SelectedProStatus === "Attention") {
                    $('td', nRow).css('color', 'blue');
                }
            },
            "columnDefs": [{ targets: 4, visible: false },
            { targets: 5, visible: false }],
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-pono', data.PONumber);
                $(row).attr('data-IsConversionReport', data.IsConversionReport);
            },
            "columns": [
                { "data": "ChkSaveRow", "autoWidth": true },
                { "data": "ChkFirstRow", "autoWidth": true },
                { "data": "LastRowText", "autoWidth": true },
                { "data": "SrPo", "autoWidth": true },
                { "data": "PONumber", "name": "PONumber", "autoWidth": true },
                { "data": "CompanyName", "name": "CompanyName", "autoWidth": true },
                { "data": "ProjectTypeText", "autoWidth": true, "orderable": false },
                { "data": "ScientistName", "autoWidth": true, "orderable": false },
                { "data": "SubScientistName", "autoWidth": true, "orderable": false },
                { "data": "ProductScheme" },
                { "data": "LeadTime" },
                { "data": "SynStartDate", "name": "SynStartDate" },
                { "data": "ProductName", "width": "18%" },
                { "data": "CASNo", "name": "CompanyName", "width": "6%" },
                { "data": "CATNo", "name": "PONumber", "width": "6%" },
                { "data": "RequiredQtyTxt", "name": "Ref", "autoWidth": true },
                { "data": "EstimateCompleteDateText", "orderable": false, "autoWidth": true },
                { "data": "ProStatusText", "orderable": false },
                //{ "data": "ScientistStatustext", "orderable": false, "autoWidth": true },
                { "data": "AdditionalBatchNoText", "name": "ProductName", "autoWidth": true },
                //           { "data": "BatchNoText", "name": "Price", "autoWidth": true },
                //   { "data": "ProjectStatustext", "orderable": false, "autoWidth": true },
                //           { "data": "ScientistRemark", "orderable": false, "autoWidth": true },
                { "data": "RemarkText", "orderable": false, "autoWidth": true },
                { "data": "ReasonText", "orderable": false },
                { "data": "OrderRemarkText", "orderable": false, "width": "15%" },
            ]

        });
    }
}

function domestictab() {
    if (domesticload === false) {
        $('#tbldomestic').DataTable({
            "searching": false,
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "fnDrawCallback": function (oSettings) {
                $(".clsSaverowdomestic").click(function () {
                    if ($(this).is(":checked")) {
                        $('#tbldomestic').find('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $('#tbldomestic').find(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
                $('#tbldomestic').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });

                $('#tbldomestic').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '') {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });

                var api = this.api();
                var rows = api.rows({ page: 'current' }).nodes();
                var last = null;
                var groupingCounts = [];
                var counter = 1;
                api.column(4, { page: 'current' }).data().each(function (group, i) {

                    if (last !== group) {
                        if (last !== undefined) {
                            groupingCounts[last] = counter;
                        }
                        $(rows).eq(i).before(
                            '<td class="group" id="grouping_' + group.trim() + '" data-groupname="' + group + '" colspan="22" style="font-weight:700;color:#fff;padding:10px;' + (rows[i].attributes["data-IsConversionReport"] != null && rows[i].attributes["data-IsConversionReport"].value == "true" ? "BACKGROUND-COLOR:#95647d" : (rows[i].attributes["data-IsPaymentPending"].value === "true" ? "BACKGROUND-COLOR:#e25b5b;" : "BACKGROUND-COLOR:#3c8dbc;")) + '">' + group + " | " + rows[i].attributes["data-company"].value + ' | ' + rows[i].attributes["data-podate"].value + ' ' + (rows[i].attributes["data-IsPaymentPending"].value === "true" ? "(Payment Pending)" : "") + ' </td>'
                        );

                        last = group;
                        counter = 1;
                    } else {
                        counter++;
                    }
                });

                groupingCounts[last] = counter;
                CountRows(groupingCounts, 'tbldomestic');
                domesticload = true;
            },
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadDomesticInstockData",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.fltactivity = $("#tabdomestic").find("#fltactivity").val();
                    d.fltdomesticprostatusItem = $("#tabdomestic").find("#fltdomesticprostatusItem").val();
                    d.radiobuton = $("#tabdomestic").find('input[name="filterAll"]:checked').val();
                    d.searchbox = $("#tabdomestic").find("#allsearchbox").val();
                }
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.ReadyToDeliverScientistStatusId === aData.ScientistStatus) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                    $('td', nRow).css('background-color', '#ffe6b3');
                }
                if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                    $('td', nRow).css('background-color', 'rgb(250, 209, 255)');
                }
                if (aData.MoveToInvoice) {
                    $('td', nRow).css('background-color', '#ff9999');
                } if (aData.IsControlledSubstance) {
                    $('td', nRow).css('color', 'red');
                }
                if (aData.SelectedProStatus === "Attention") {
                    $('td', nRow).css('color', 'blue');
                }
            },
            "columnDefs": [
                { targets: 4, visible: false },
                { targets: 5, visible: false }],
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-pono', data.PONumber);
                $(row).attr('data-IsPaymentPending', (data.IsPaymentPending == null ? false : data.IsPaymentPending));
                $(row).attr('data-IsConversionReport', data.IsConversionReport);
            },
            "columns": [
                { "data": "ChkSaveRow", "autoWidth": true },
                { "data": "ChkFirstRow", "autoWidth": true },
                { "data": "LastRowText", "autoWidth": true },
                { "data": "SrPo", "autoWidth": true },
                { "data": "PONumber", "name": "PONumber", "autoWidth": true },
                { "data": "CompanyName", "name": "CompanyName", "autoWidth": true },
                { "data": "ProjectTypeText", "autoWidth": true, "orderable": false },
                { "data": "ScientistName", "autoWidth": true, "orderable": false },
                { "data": "SubScientistName", "autoWidth": true, "orderable": false },
                { "data": "ProductName", "width": "18%" },
                { "data": "CASNo", "name": "CompanyName", "width": "6%" },
                { "data": "CATNo", "name": "PONumber", "width": "6%" },
                { "data": "Attachment", "name": "Attachment" },
                { "data": "RequiredQtyTxt", "name": "Ref", "autoWidth": true },
                { "data": "EstimateCompleteDateText", "orderable": false, "autoWidth": true },
                //{ "data": "ScientistStatustext", "orderable": false, "autoWidth": true },
                { "data": "AdditionalBatchNoText", "name": "ProductName", "autoWidth": true },
                { "data": "COARefNumber", "name": "COARefNumber" },
                { "data": "ActivityStatusText", "orderable": false },
                { "data": "OtherProStatusText", "orderable": false },
                { "data": "RemarkText", "orderable": false, "autoWidth": true },
                { "data": "ReasonText", "orderable": false },
                { "data": "OrderRemarkText", "orderable": false, "width": "15%" },
            ]
        });
    }
}

var synthesislogtabloaded = false;
function synthesislogtab() {
    if (synthesislogtabloaded === false) {
        $('#tblsynthesislog').DataTable({
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "fnDrawCallback": function (oSettings) {

                $(".clsSaverowsynthesislog").click(function () {
                    if ($(this).is(":checked")) {
                        $('#tblsynthesislog').find('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $('#tblsynthesislog').find(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
                $('#tblsynthesislog').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });

                $('#tblsynthesislog').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '') {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });
                synthesislogtabloaded = true;
            },
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadProductSynthesisLogData",
                "type": "POST",
                "datatype": "json"
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.ReadyToDeliverScientistStatusId === aData.ScientistStatus) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                    $('td', nRow).css('background-color', '#ffe6b3');
                }
                if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                    $('td', nRow).css('background-color', '#ffccb3');
                }
                if (aData.MoveToInvoice) {
                    $('td', nRow).css('background-color', '#ff9999');
                }
                if (aData.IsControlledSubstance) {
                    $('td', nRow).css('color', 'red');
                }
                if (aData.SelectedProStatus === "Attention") {
                    $('td', nRow).css('color', 'blue');
                }
            },
            "columnDefs": [{ targets: 2, visible: false },],
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-pono', data.PONumber);
            },
            "columns": [
                { "data": "ChkSaveRow", "autoWidth": true },
                { "data": "ChkFirstRow", "autoWidth": true },
                { "data": "LastRowText", "autoWidth": true },
                { "data": "SrPo", "autoWidth": true },
                { "data": "PONumber", "name": "PONumber", "autoWidth": true },
                { "data": "CompanyName", "name": "CompanyName", "autoWidth": true },
                { "data": "ProjectTypeText", "autoWidth": true, "orderable": false },
                { "data": "ScientistName", "autoWidth": true, "orderable": false },
                { "data": "SubScientistName", "autoWidth": true, "orderable": false },
                { "data": "ProductName", "width": "18%" },
                { "data": "CASNo", "name": "CompanyName", "width": "6%" },
                { "data": "CATNo", "name": "PONumber", "width": "6%" },
                { "data": "RequiredQtyTxt", "name": "Ref", "autoWidth": true },
                { "data": "AdditionalBatchNoText", "name": "ProductName", "autoWidth": true },
                { "data": "PurchaseStatusText", "orderable": false, "autoWidth": true },
                { "data": "RemarkText", "orderable": false, "autoWidth": true },
                { "data": "ReasonText", "orderable": false }
            ]
        });
    }
}


var purchasetabloaded = false;
function purchasetab() {
    if (purchasetabloaded === false) {
        $('#tblpurchase').DataTable({
            "searching": false,
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "fnDrawCallback": function (oSettings) {
                $(".clsSaverowpurchase").click(function () {
                    if ($(this).is(":checked")) {
                        $('#tblpurchase').find('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $('#tblpurchase').find(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
                $('#tblpurchase').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });

                $('#tblpurchase').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '') {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });
                purchasetabloaded = true;
            },
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadPurchaseData",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.purchasestatus = $("#filpurchasestatus").val();
                    d.radiobuton = $("#tabpurchase").find('input[name="filterAll"]:checked').val();
                    d.searchbox = $("#tabpurchase").find("#allsearchbox").val();
                }
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.ReadyToDeliverScientistStatusId === aData.ScientistStatus) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                    $('td', nRow).css('background-color', '#ffe6b3');
                }
                if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                    $('td', nRow).css('background-color', '#ffccb3');
                }
                if (aData.MoveToInvoice) {
                    $('td', nRow).css('background-color', '#ff9999');
                }
                if (aData.IsControlledSubstance) {
                    $('td', nRow).css('color', 'red');
                }
                if (aData.SelectedProStatus === "Attention") {
                    $('td', nRow).css('color', 'blue');
                }
            },
            "columnDefs": [{ targets: 2, visible: false },],
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-pono', data.PONumber);
                $(row).attr('data-IsConversionReport', data.IsConversionReport);
            },
            "columns": [
                { "data": "ChkSaveRow", "autoWidth": true },
                { "data": "PurchaseDateText", "orderable": false, "autoWidth": true },
                { "data": "PONumber", "name": "PONumber", "autoWidth": true },
                { "data": "CompanyName", "name": "CompanyName", "autoWidth": true },
                { "data": "PurchaseDDLStatusText", "name": "PurchaseDDLStatusText", "autoWidth": true },
                { "data": "ScientistName", "autoWidth": true, "orderable": false },
                { "data": "ProductName", "width": "18%" },
                { "data": "CASNo", "name": "CompanyName", "width": "6%" },
                { "data": "CATNo", "name": "PONumber", "width": "6%" },
                { "data": "RequiredQtyTxt", "name": "Ref", "autoWidth": true },
                { "data": "AdditionalBatchNoText", "name": "ProductName", "autoWidth": true },
                { "data": "EstimateCompleteDateText", "orderable": false, "autoWidth": true },
                { "data": "PurchaseStatusText", "orderable": false, "autoWidth": true },
                { "data": "PurchaseRemarkText", "autoWidth": true, "orderable": false },
                { "data": "ReasonText", "orderable": false },

            ]

        });
    }
}

var purchaserfqtabloaded = false;
function purchaserfqtab() {
    if (purchaserfqtabloaded === false) {
        var tblpurchaserfq = $('#tblpurchaserfq').DataTable({
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "fnDrawCallback": function (oSettings) {
                $('#tblpurchaserfq').find("#selectAllclsSaverow").click(function () {
                    if ($(this).is(":checked")) {
                        $('#tblpurchaserfq').find('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $('#tblpurchaserfq').find(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
                $('#tblpurchaserfq').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });

                $('#tblpurchaserfq').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '') {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });

                tblpurchaserfq.columns.adjust();

                purchaserfqtabloaded = true;
            },
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadPurchaseRFQData",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.purchasestatus = $("#filpurchaserfqstatus").val();
                }
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                //if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                //    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                //}
                //if (aData.ReadyToDeliverScientistStatusId === aData.ScientistStatus) {
                //    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                //}
                //if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                //    $('td', nRow).css('background-color', '#ffe6b3');
                //}
                //if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                //    $('td', nRow).css('background-color', '#ffccb3');
                //}
                //if (aData.MoveToInvoice) {
                //    $('td', nRow).css('background-color', '#ff9999');
                //}
            },
            "columnDefs": [{ targets: 2, visible: false },],
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-pono', data.PONumber);
            },
            "columns": [
                { "data": "ChkSaveRow", "autoWidth": true },
                { "data": "PurchaseStatusText", "name": "PurchaseStatusText", "autoWidth": true },
                { "data": "RfqNo", "name": "RfqNo", "autoWidth": true },
                { "data": "AssignedDateText", "orderable": false, "autoWidth": true },
                { "data": "ChemicalNameText", "width": "18%" },
                { "data": "CASNoText", "name": "CASNoText", "width": "6%" },
                { "data": "CATNoText", "name": "CATNoText", "width": "6%" },
                { "data": "CommentText", "name": "CommentText", "autoWidth": true },
                { "data": "SummaryText", "name": "SummaryText", "autoWidth": true },
                { "data": "EstdateText", "orderable": false, "autoWidth": true },
                { "data": "PurchaseRemarkText", "autoWidth": true, "orderable": false },
                { "data": "RefBy", "autoWidth": true, "orderable": false },
            ]
        });
    }
}


var purchaseloginrfqtabloaded = false;
function purchaseloginrfqtab() {
    if (purchaseloginrfqtabloaded === false) {
        var tblpurchaseloginrfq = $('#tblpurchaserfq').DataTable({
            //"fixedHeader": {
            //    header: true
            //},
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "ajax": {
                "url": "../Form/LoadPurchaseAllData?status=20",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    /*d.instockother = $("#filterinstock").is(":checked") + "," + $("#filterother").is(":checked")*/
                    d.purchasestatusddl = $('#filrfqpurchaserfqstatus').val()
                }
            },
            "fnDrawCallback": function (oSettings) {
                calledExtra();
                purchaseloginrfqtabloaded = true;
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                //if (aData.IsBatchAvailable) {
                //    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                //}
                //$('td:eq(2)', nRow).addClass("imgclshover");

            },
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                $(row).attr('id', 'tr_' + data.Id);
                $(row).attr('class', 'clstr');
            },
            "columns": [
                { "data": "ChkRow", "autoWidth": true, "orderable": false },
                { "data": "StrPurchaseDate", "autoWidth": true, "orderable": false },
                { "data": "StrPurchaseDDLStatus", "autoWidth": true, "orderable": false },
                { "data": "PONumber", "autoWidth": true, "orderable": false },
                { "data": "ProductName", "autoWidth": true, "orderable": false },
                { "data": "CASNo", "autoWidth": true, "orderable": false },
                { "data": "CATNo", "autoWidth": true, "orderable": false },
                { "data": "RequiredQty", "autoWidth": true, "orderable": false },
                { "data": "BatchNo", "autoWidth": true, "orderable": false },
                { "data": "StrEstimateCompleteDate", "autoWidth": true, "orderable": false },
                { "data": "LeadTime", "autoWidth": true, "orderable": false }, 
                { "data": "PurchaseStatus", "autoWidth": true, "orderable": false },
                { "data": "PurchaseRemark", "autoWidth": true, "orderable": false },
                { "data": "Reason", "autoWidth": true, "orderable": false },
                { "data": "PurMangRemark", "autoWidth": true, "orderable": false },
                { "data": "StrPurClientPODate", "autoWidth": true, "orderable": false },
                { "data": "StrPurOurPODt", "autoWidth": true, "orderable": false },
                { "data": "StrPurExpectedReceiptDt", "autoWidth": true, "orderable": false },
                { "data": "StrPurCurrentExpDt", "autoWidth": true, "orderable": false },
                { "data": "StrPurActualReceiptDt", "autoWidth": true, "orderable": false },
                { "data": "StrPurTargetDispatchDt", "autoWidth": true, "orderable": false },
                { "data": "PurSZPONo", "autoWidth": true, "orderable": false },
                { "data": "PurPrice", "autoWidth": true, "orderable": false },
            ]
        });
    }
}

var purchaseenquiryloaded = false;
function purchaseenquirytab() {
    if (purchaseenquiryloaded === false) {
        var tblpurchaseloginrfq = $('#tblpurchaseenquiry').DataTable({
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "fnDrawCallback": function (oSettings) {
                $('#tblpurchaseenquiry').find("#selectAllclsSaverow").click(function () {
                    if ($(this).is(":checked")) {
                        $('#tblpurchaseenquiry').find('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $('#tblpurchaseenquiry').find(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
                $('#tblpurchaseenquiry').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });
                $('#tblpurchaseenquiry').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '') {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });

                tblpurchaseloginrfq.columns.adjust();

                purchaseenquiryloaded = true;
            },
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadPurchaseLoginRFQData",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.purchasestatus = $("#filpurchaserfqstatus").val();
                }
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                //if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                //    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                //}
                //if (aData.ReadyToDeliverScientistStatusId === aData.ScientistStatus) {
                //    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                //}
                //if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                //    $('td', nRow).css('background-color', '#ffe6b3');
                //}
                //if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                //    $('td', nRow).css('background-color', '#ffccb3');
                //}
                //if (aData.MoveToInvoice) {
                //    $('td', nRow).css('background-color', '#ff9999');
                //}
            },
            "columnDefs": [{ targets: 2, visible: false },],
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-pono', data.PONumber);
            },
            "columns": [
                { "data": "ChkSaveRow", "autoWidth": true },
                { "data": "PurchaseStatusText", "name": "PurchaseStatusText", "autoWidth": true },
                { "data": "RfqNo", "name": "RfqNo", "autoWidth": true },
                { "data": "AssignedDateText", "orderable": false, "autoWidth": true },
                { "data": "ChemicalNameText", "width": "18%" },
                { "data": "CASNoText", "name": "CASNoText", "width": "6%" },
                { "data": "CATNoText", "name": "CATNoText", "width": "6%" },
                { "data": "CommentText", "name": "CommentText", "autoWidth": true },
                { "data": "SummaryText", "name": "SummaryText", "autoWidth": true },
                { "data": "EstdateText", "orderable": false, "autoWidth": true },
                { "data": "PurchaseRemarkText", "autoWidth": true, "orderable": false },
                { "data": "RefBy", "autoWidth": true, "orderable": false },
            ]

        });
    }
}


var purchasesubmittedtabloaded = false;
function purchasesubmittedtab() {
    if (purchasesubmittedtabloaded === false) {
        var tblpurchaseloginrfq = $('#tblpurchaserfqsubmitted').DataTable({
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "fnDrawCallback": function (oSettings) {
                $('#tblpurchaserfq').find("#selectAllclsSaverow").click(function () {
                    if ($(this).is(":checked")) {
                        $('#tblpurchaserfqsubmitted').find('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $('#tblpurchaserfqsubmitted').find(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
                $('#tblpurchaserfqsubmitted').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });

                $('#tblpurchaserfqsubmitted').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '') {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });

                tblpurchaseloginrfq.columns.adjust();

                purchasesubmittedtabloaded = true;
            },
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadPurchaseSubmittedData",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.purchasestatus = $("#filpurchaserfqstatus").val();
                }
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                //if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                //    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                //}
                //if (aData.ReadyToDeliverScientistStatusId === aData.ScientistStatus) {
                //    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                //}
                //if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                //    $('td', nRow).css('background-color', '#ffe6b3');
                //}
                //if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                //    $('td', nRow).css('background-color', '#ffccb3');
                //}
                //if (aData.MoveToInvoice) {
                //    $('td', nRow).css('background-color', '#ff9999');
                //}
            },
            "columnDefs": [{ targets: 2, visible: false },],
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-pono', data.PONumber);
            },
            "columns": [
                { "data": "PurchaseStatus", "name": "PurchaseStatusText", "autoWidth": true },
                { "data": "AssignedDateText", "orderable": false, "autoWidth": true },
                { "data": "ChemicalName", "width": "18%" },
                { "data": "CASNo", "name": "CASNoText", "width": "6%" },
                { "data": "CATNo", "name": "CATNoText", "width": "6%" },
                { "data": "Comment", "name": "CommentText", "autoWidth": true },
                { "data": "Summary", "name": "SummaryText", "autoWidth": true },
                { "data": "EstdateText", "orderable": false, "autoWidth": true },
                { "data": "PurchaseRemark", "autoWidth": true, "orderable": false },
                { "data": "RefBy", "autoWidth": true, "orderable": false },
            ]

        });
    }
}

var holdtabloaded = false;
function holdtab() {
    if (holdtabloaded === false) {
        $('#example2').DataTable({
            "searching": false,
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "fnDrawCallback": function (oSettings) {
                holdtabloaded = true;

                $('#example2').find("#selectAllclsSaverow").click(function () {
                    if ($(this).is(":checked")) {
                        $('#example2').find('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $('#example2').find(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
            },
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadProductHoldData",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.radiobuton = $("#menu1").find('input[name="filterAll"]:checked').val();
                    d.searchbox = $("#menu1").find("#allsearchbox").val();
                }
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.IsControlledSubstance) {
                    $('td', nRow).css('color', 'red');
                }
            },
            "columnDefs": [
            ],
            "createdRow": function (row, data, dataIndex) {
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-pono', data.PONumber);
                $(row).attr('data-IsPaymentPending', (data.IsPaymentPending == null ? false : data.IsPaymentPending));
                $(row).attr('data-IsConversionReport', data.IsConversionReport);
            },
            "columns": [
                { "data": "SZ_QuotationProductModel[0].CheckboxRaw", "autoWidth": true },
                { "data": "SZ_QuotationProductModel[0].SrPo", "autoWidth": true },
                { "data": "SZ_QuotationProductModel[0].LeadTime", "autoWidth": true },
                { "data": "CompanyName", "autoWidth": true },
                { "data": "PONumber", "autoWidth": true },
                { "data": "SZ_QuotationProductModel[0].DifficultyLevelText", "autoWidth": true },
                { "data": "SZ_QuotationProductModel[0].ProjectType", "autoWidth": true },
                { "data": "SZ_QuotationProductModel[0].ScientistName", "autoWidth": true },
                { "data": "SZ_QuotationProductModel[0].SubScientistName", "autoWidth": true },
                { "data": "SZ_QuotationProductModel[0].MoveToScientistDateText", "autoWidth": true },
                { "data": "SZ_QuotationProductModel[0].ProductName", "autoWidth": true },
                { "data": "SZ_QuotationProductModel[0].CASNo", "autoWidth": true },
                { "data": "SZ_QuotationProductModel[0].CATNo", "autoWidth": true },
                { "data": "SZ_QuotationProductModel[0].RequiredQty", "autoWidth": true },
                { "data": "SZ_QuotationProductModel[0].EstimateCompleteDateText", "autoWidth": true },
                //{ "data": "SZ_QuotationProductModel[0].AdminScientistStatus", "autoWidth": true },
                { "data": "SZ_QuotationProductModel[0].AdditionalBatchNoText", "autoWidth": true },
                { "data": "SZ_QuotationProductModel[0].Remark", "autoWidth": true },
                { "data": "SZ_QuotationProductModel[0].Reason", "autoWidth": true },
            ]

        });
    }
}

var instocktabloaded = false;
function instocktab() {
    if (instocktabloaded === false) {
        $('#tblinstock').DataTable({
            "searching": false,
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "fnDrawCallback": function (oSettings) {

                $(".clsSaverowinstock").click(function () {
                    if ($(this).is(":checked")) {
                        $('#tblinstock').find('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $('#tblinstock').find(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
                $('#tblinstock').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });

                $('#tblinstock').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '') {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });


                var api = this.api();
                var rows = api.rows({ page: 'current' }).nodes();
                var last = null;
                var groupingCounts = [];
                var counter = 1;
                api.column(4, { page: 'current' }).data().each(function (group, i) {

                    if (last !== group) {
                        if (last !== undefined) {
                            groupingCounts[last] = counter;
                        }
                        $(rows).eq(i).before(
                            '<td class="group" colspan="22" id="grouping_' + group.trim() + '" data-groupname="' + group + '" style="font-weight:700;color:#fff;padding:10px;' + (rows[i].attributes["data-IsConversionReport"] != null && rows[i].attributes["data-IsConversionReport"].value == "true" ? "BACKGROUND-COLOR:#95647d;" : (rows[i].attributes["data-IsPaymentPending"].value === "true" ? "BACKGROUND-COLOR:#e25b5b;" : "BACKGROUND-COLOR:#3c8dbc;")) + '">' + group + " | " + rows[i].attributes["data-company"].value + ' | ' + rows[i].attributes["data-podate"].value + ' ' + (rows[i].attributes["data-IsPaymentPending"].value === "true" ? "(Payment Pending)" : "") + ' </td>'
                        );

                        last = group;
                        counter = 1;
                    } else {
                        counter++;
                    }
                });

                instocktabloaded = true;
                groupingCounts[last] = counter;
                CountRows(groupingCounts, 'tblinstock');
            },
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadProductInstockData",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.fltinstockprostatusItem = $("#fltinstockprostatusItem").val();
                    d.radiobuton = $("#tabinstock").find('input[name="filterAll"]:checked').val();
                    d.searchbox = $("#tabinstock").find("#allsearchbox").val();
                }
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.ReadyToDeliverScientistStatusId === aData.ScientistStatus) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                    $('td', nRow).css('background-color', '#ffe6b3');
                }
                if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                    $('td', nRow).css('background-color', '#ffccb3');
                }
                if (aData.MoveToInvoice) {
                    $('td', nRow).css('background-color', '#ff9999');
                }
                if (aData.IsControlledSubstance) {
                    $('td', nRow).css('color', 'red');
                }
                if (aData.SelectedProStatus === "Attention") {
                    $('td', nRow).css('color', 'blue');
                }
            },
            "columnDefs": [{ targets: 4, visible: false },
            { targets: 5, visible: false }],
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-pono', data.PONumber);
                $(row).attr('data-IsPaymentPending', (data.IsPaymentPending == null ? false : data.IsPaymentPending));
                $(row).attr('data-IsConversionReport', data.IsConversionReport);
            },
            "columns": [
                { "data": "ChkSaveRow", "autoWidth": true },
                { "data": "ChkFirstRow", "autoWidth": true },
                { "data": "LastRowText", "autoWidth": true },
                { "data": "SrPo", "autoWidth": true },
                { "data": "PONumber", "name": "PONumber", "autoWidth": true },
                { "data": "CompanyName", "name": "CompanyName", "autoWidth": true },
                { "data": "ProjectTypeText", "autoWidth": true, "orderable": false },
                { "data": "ScientistName", "autoWidth": true, "orderable": false },
                { "data": "SubScientistName", "autoWidth": true, "orderable": false },
                { "data": "ProductName", "width": "18%" },
                { "data": "CASNo", "name": "CompanyName", "width": "6%" },
                { "data": "CATNo", "name": "PONumber", "width": "6%" },
                { "data": "Attachment", "name": "Attachment" },
                { "data": "RequiredQtyTxt", "name": "Ref", "autoWidth": true },
                { "data": "EstimateCompleteDateText", "orderable": false, "autoWidth": true },
                //{ "data": "ScientistStatustext", "orderable": false, "autoWidth": true },
                { "data": "AdditionalBatchNoText", "name": "ProductName", "autoWidth": true },
                { "data": "COARefNumber", "name": "COARefNumber" },
                { "data": "ActivityStatusText", "orderable": false },
                { "data": "OtherProStatusText", "orderable": false },
                //  { "data": "BatchNoText", "name": "Price", "autoWidth": true },
                //{ "data": "ProjectStatustext", "orderable": false, "autoWidth": true },
                //  { "data": "ScientistRemark", "orderable": false, "autoWidth": true },
                { "data": "RemarkText", "orderable": false, "autoWidth": true },
                { "data": "ReasonText", "orderable": false },
                { "data": "OrderRemarkText", "orderable": false },
            ]

        });
    }
}

var querytabloaded = false;

function exporttab() {
    if (exporttabloaded === false) {
        var tblexport = $('#tblexport').DataTable({
            "searching": false,
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "fnDrawCallback": function (oSettings) {
                $('#tblexport').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });

                $('#tblexport').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '') {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });
                var api = this.api();
                var rows = api.rows({ page: 'current' }).nodes();
                var last = null;
                var groupingCounts = [];
                var counter = 1;
                api.column(5, { page: 'current' }).data().each(function (group, i) {
                    if (last !== group) {
                        if (last !== undefined) {
                            groupingCounts[last] = counter;
                        }
                        $(rows).eq(i).before(
                            '<td class="group" id="grouping_' + group.trim() + '" data-groupname="' + group + '" colspan="24" style="font-weight:700;color:#fff;padding:10px;' + (rows[i].attributes["data-IsConversionReport"] != null && rows[i].attributes["data-IsConversionReport"].value == "true" ? "BACKGROUND-COLOR:#95647d" : (rows[i].attributes["data-IsPaymentPending"].value === "true" ? "BACKGROUND-COLOR:#e25b5b;" : "BACKGROUND-COLOR:#3c8dbc;")) + '">' + group + " | " + rows[i].attributes["data-company"].value + ' | ' + rows[i].attributes["data-podate"].value + ' ' + (rows[i].attributes["data-IsPaymentPending"].value === "true" ? "(Payment Pending)" : "") + ' </td>'
                        );

                        last = group;
                        counter = 1;
                    } else {
                        counter++;
                    }
                });
                groupingCounts[last] = counter;
                CountRows(groupingCounts, 'tblexport');

                tblexport.columns.adjust();
                $(".clsIsPayment").click(function () {
                    var status = $(this).is(":checked");
                    $.ajax({
                        url: '/Form/ChangePaymentStatus?id=' + $(this).val() + '&status=' + status,
                        data: {},
                        type: 'GET',
                        success: function (data) {
                            if (status) {
                                toastr.success("You have made payment.");
                            }
                            else {
                                toastr.success("You have cancelled payment.");
                            }
                        }
                    });
                });
                exporttabloaded = true;


                $("#selectAllclsMoveDispatch").click(function () {
                    if ($(this).is(":checked")) {
                        $('.clsMoveDispatch').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $(".clsMoveDispatch").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });

                $("#selectAllclsSaverow").click(function () {
                    if ($(this).is(":checked")) {
                        $('.clsSaverow').each(function () {
                            $(this).prop("checked", true);
                        });
                    }
                    else {
                        $(".clsSaverow").each(function () {
                            $(this).prop("checked", false);
                        });
                    }
                });
                $('#tblexport').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });

                $('#tblexport').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '') {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });
            },
            "columnDefs":
                [
                    { targets: 2, visible: false },
                    { targets: 4, visible: false },
                    { targets: 5, visible: false },
                    { targets: 6, visible: false }
                ],
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadProductExportData",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.extra_search = $("#ddlexport").val();
                    d.fltactivity = $("#tabexport").find("#fltactivity").val();
                    d.fltexportprostatusItem = $("#tabexport").find("#fltexportprostatusItem").val();
                    d.instockother = $("#filterinstock").is(":checked") + "," + $("#filterother").is(":checked");
                    d.radiobuton = $("#tabexport").find('input[name="filterAll"]:checked').val();
                    d.searchbox = $("#tabexport").find("#allsearchbox").val();
                }
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {



                if (aData.SelectedProjectType === "In-Stock" && aData.MoveToDispatch != true) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.ReadyToDeliverScientistStatusId === aData.ScientistStatus) {
                    $('td', nRow).css('background-color', 'rgb(202, 251, 217)');
                }
                if (aData.MoveToDispatch && (aData.DispatchedStatus == "0" || aData.DispatchedStatus == null)) {
                    $('td', nRow).css('background-color', '#ffe6b3');
                }
                if (aData.MoveToDispatch && aData.DispatchedStatus == "1") {
                    $('td', nRow).css('background-color', '#ffccb3');
                }
                if (aData.MoveToInvoice) {
                    $('td', nRow).css('background-color', '#ff9999');
                } if (aData.IsControlledSubstance) {
                    $('td', nRow).css('color', 'red');
                }
                if (aData.SelectedProStatus === "Attention") {
                    $('td', nRow).css('color', 'blue');
                }
                //var date = new Date(aData.MoveProjectDateText);
                //var month = date.getMonth() + 1;
                //aData.MoveProjectDateText = (month.length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
            },
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-pono', data.PONumber);
                $(row).attr('data-IsPaymentPending', (data.IsPaymentPending == null ? false : data.IsPaymentPending));
                $(row).attr('data-IsConversionReport', data.IsConversionReport);
            },
            "columns": [
                { "data": "ChkSaveRow", "autoWidth": true },
                { "data": "ChkFirstRow", "autoWidth": true },
                { "data": "LastRowText", "autoWidth": true },
                { "data": "SrPo", "autoWidth": true },
                { "data": "MoveProjectDateText", "name": "MoveProjectDateText", "autoWidth": true },
                { "data": "PONumber", "name": "PONumber", "autoWidth": true },
                { "data": "CompanyName", "name": "CompanyName", "autoWidth": true },
                { "data": "ProjectTypeText", "autoWidth": true, "orderable": false },
                { "data": "ScientistName", "autoWidth": true, "orderable": false },
                { "data": "SubScientistName", "autoWidth": true, "orderable": false },
                { "data": "ProductName", "width": "18%" },
                { "data": "CASNo", "name": "CompanyName", "width": "6%" },
                { "data": "CATNo", "name": "PONumber", "width": "6%" },
                { "data": "RequiredQtyTxt", "name": "Ref", "autoWidth": true },
                { "data": "EstimateCompleteDateText", "orderable": false, "autoWidth": true },
                { "data": "ReportInvoiceDateText", "orderable": false, "autoWidth": true },
                //{ "data": "ScientistStatustext", "orderable": false, "autoWidth": true },
                { "data": "AdditionalBatchNoText", "name": "ProductName", "width": "5%" },
                { "data": "COARefNumber", "name": "COARefNumber" },
                { "data": "ActivityStatusText", "orderable": false },
                { "data": "OtherProStatusText", "orderable": false },
                //                { "data": "BatchNoText", "name": "Price", "autoWidth": true },
                //{ "data": "ProjectStatustext", "orderable": false, "autoWidth": true },
                //              { "data": "ScientistRemark", "orderable": false, "autoWidth": true },
                { "data": "IsPaymentText", "name": "IsPaymentText", "width": "5%" },
                { "data": "RemarkText", "orderable": false, "width": "15%" },
                { "data": "OrderRemarkText", "orderable": false, "width": "15%" },
                { "data": "ReasonText", "orderable": false, "width": "15%" },
                { "data": "TechnicalEmail", "orderable": false, "width": "15%" },
                { "data": "MSDS", "orderable": false, "width": "15%" },
            ]

        });
    }
}

var exporttabloaded = false;

function querytab() {
    if (querytabloaded === false) {
        $('#tblquery').DataTable({
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "fnDrawCallback": function (oSettings) {

                $('#tblquery').find('.datepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });

                $('#tblquery').find('.datepicker').each(function () {
                    var dateValue = $(this).attr('data-value');
                    if (dateValue !== '') {
                        dateValue = dateValue.toString().replace(' 00:00:00', '');
                        $(this).datepicker("setDate", dateValue);
                    }
                });
            },
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "initComplete": function (settings, json) {

            },
            "ajax": {
                "url": "../Form/LoadProductQueryData",
                "type": "POST",
                "datatype": "json"
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.IsQueryResolved == true) {
                    $('td', nRow).css('background-color', 'rgb(169, 230, 169)');
                }
            },
            "columnDefs": [],
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                $(row).attr('id', 'tr_' + data.QuoteDetailsId);
                $(row).attr('data-company', data.CompanyName);
                $(row).attr('data-podate', data.MoveProjectDateText);
                $(row).attr('data-pono', data.PONumber);
                if (data.IsAssignProjectQuery == true) {
                    $(row).find('td:first').css('background-color', 'red');
                }

            },
            "columns": [
                { "data": "ChkSaveRow", "autoWidth": true },
                //{ "data": "ChkFirstRow", "autoWidth": true },
                //{ "data": "LastRowText", "autoWidth": true },
                { "data": "SrPo", "autoWidth": true },
                { "data": "MoveProjectDateText", "name": "MoveProjectDateText", "autoWidth": true },
                { "data": "PONumber", "name": "PONumber", "autoWidth": true },
                { "data": "CompanyName", "name": "CompanyName", "autoWidth": true },
                { "data": "ProjectTypeText", "autoWidth": true, "orderable": false },
                { "data": "ScientistName", "autoWidth": true, "orderable": false },
                //{ "data": "SubScientistName", "autoWidth": true, "orderable": false },
                { "data": "ProductName", "width": "18%" },
                { "data": "CASNo", "name": "CompanyName", "width": "6%" },
                { "data": "CATNo", "name": "PONumber", "width": "6%" },
                { "data": "RequiredQtyTxt", "name": "Ref", "autoWidth": true },
                { "data": "EstimateCompleteDateText", "orderable": false, "autoWidth": true },
                //{ "data": "ScientistStatustext", "orderable": false, "autoWidth": true },
                { "data": "AdditionalBatchNoText", "name": "ProductName", "width": "5%" },
                //                { "data": "BatchNoText", "name": "Price", "autoWidth": true },
                //{ "data": "ProjectStatustext", "orderable": false, "autoWidth": true },
                //              { "data": "ScientistRemark", "orderable": false, "autoWidth": true },
                //{ "data": "IsPaymentText", "name": "IsPaymentText", "width": "5%" },
                //{ "data": "RemarkText", "orderable": false, "width": "15%" }
                { "data": "ResolvedQueryText", "name": "ResolvedQueryText", "width": "5%" },
            ]

        });
    }
}
var copyTextPrice = "";
function copyToClipboard(text) {
    copyTextPrice = text;
    var textArea = document.createElement("textarea");
    textArea.value = text;
    textArea.id = "txtcopytext";
    document.body.appendChild(textArea);

    textArea.select();

    try {
        var successful = document.execCommand('copy');
        var msg = successful ? 'successful' : 'unsuccessful';
        msg = 'Copying text command was ' + msg;
        toastr.success("Copy Done!!!");
        //swal("Copy Done!", msg, "success")
    } catch (err) {
        console.log('Oops, unable to copy');
    }

    document.body.removeChild(textArea);
}

function calculatePerc(price, discount) {
    var calcPrice = parseInt((price - (price * discount / 100)).toFixed(2));
    return calcPrice;
}

function INRRecord(id) {
    var objData = [];
    var discount = 0;
    if ($("#Discount_" + id).val() !== '') {
        discount = $("#Discount_" + id).val();
    }
    var array = [];
    var tenmg = $("#TenPrice_" + id).val();
    var twentyfivemg = $("#TwentyFivePrice_" + id).val();
    var fiftymg = $("#FiftyPrice_" + id).val();
    var hundredmg = $("#HundredPrice_" + id).val();
    var twohundredmg = $("#TwoHundredPrice_" + id).val();
    var leadtime = $("#LeadTime_" + id).val();
    var FivehundredPrice = $("#FivehundredPrice_" + id).val();
    var OneThousandPrice = $("#OneThousandPrice_" + id).val();
    if (tenmg != '') {
        objData.push("10 mg@" + calculatePerc(tenmg, discount) + " INR+");
    }
    if (twentyfivemg != '') {
        objData.push("25 mg@" + calculatePerc(twentyfivemg, discount) + " INR+");
    }
    if (fiftymg != '') {
        objData.push("50 mg@" + calculatePerc(fiftymg, discount) + " INR+");
    }
    if (hundredmg != '') {
        objData.push("100 mg@" + calculatePerc(hundredmg, discount) + " INR+");
    }
    if (twohundredmg != '') {
        objData.push("250 mg@" + calculatePerc(twohundredmg, discount) + " INR+");
    }
    if (FivehundredPrice != '') {
        objData.push("500 mg@" + calculatePerc(FivehundredPrice, discount) + " INR+");
    }
    if (OneThousandPrice != '') {
        objData.push("1000 mg@" + calculatePerc(OneThousandPrice, discount) + " INR+");
    }

    if (leadtime != '') {
        objData.push(leadtime);
    }

    var text = objData.join(' , ');
    copyToClipboard(text);
}

function USDRecord(id) {

    var objData = [];
    var discount = 0;
    if ($("#Discount_" + id).val() !== '') {
        discount = $("#Discount_" + id).val();
    }

    var array = [];
    var tenmg = $("#TenUSD_" + id).val();
    var twentyfivemg = $("#TwentyfiveUSD_" + id).val();
    var fiftymg = $("#FiftyUSD_" + id).val();
    var hundredmg = $("#OnehundredUSD_" + id).val();
    var twohundredmg = $("#TwohundredFiftyUSD_" + id).val();
    var leadtime = $("#LeadTime_" + id).val();
    var FivehundredUSD = $("#FivehundredUSD_" + id).val();
    var OneThousandUSD = $("#OneThousandUSD_" + id).val();
    if (tenmg != '') {
        //tenmg = tenmg / usdToInrPrice;
        objData.push("10 mg@" + calculatePerc(tenmg, discount) + " USD");
    }
    if (twentyfivemg != '') {
        //twentyfivemg = twentyfivemg / usdToInrPrice;
        objData.push("25 mg@" + calculatePerc(twentyfivemg, discount) + " USD");
    }
    if (fiftymg != '') {
        //fiftymg = fiftymg / usdToInrPrice;
        objData.push("50 mg@" + calculatePerc(fiftymg, discount) + " USD");
    }
    if (hundredmg != '') {
        //hundredmg = hundredmg / usdToInrPrice;
        objData.push("100 mg@" + calculatePerc(hundredmg, discount) + " USD");
    }
    if (twohundredmg != '') {
        //twohundredmg = twohundredmg / usdToInrPrice;
        objData.push("250 mg@" + calculatePerc(twohundredmg, discount) + " USD");
    }
    if (FivehundredUSD != '') {
        //FivehundredUSD = FivehundredUSD / usdToInrPrice;
        objData.push("500 mg@" + calculatePerc(FivehundredUSD, discount) + " USD");
    }
    if (OneThousandUSD != '') {
        //twohundredmg = twohundredmg / usdToInrPrice;
        objData.push("1000 mg@" + calculatePerc(OneThousandUSD, discount) + " USD");
    }
    if (leadtime != '') {
        objData.push(leadtime);
    }
    var text = objData.join(' , ');
    copyToClipboard(text);
}

function LoadUsdInrPrice() {
    // var url = 'https://api.exchangeratesapi.io/latest?symbols=USD,INR&base=USD';
    var url = 'https://free.currconv.com/api/v7/convert?q=INR_USD&compact=ultra&apiKey=959999c8ae8c4ebed5b8';
    $.ajax({
        url: url,
        data: {},
        type: 'GET',
        success: function (data) {
            usdToInrPrice = parseFloat(data.rates.INR).toFixed(2);
        }
    });
}

function ConvertJSONDate(jsonDate) {
    var dateString = jsonDate.substr(6);
    var currentTime = new Date(parseInt(dateString));
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    var date = day + "/" + month + "/" + year;
    return date;
}

function isNull(val) {
    if (val === '' || val === '' || val === 'undefined' || val === null) {
        return "";
    }
    return val;
}

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

function copyprice(symbol, size, leadtime, value) {
    copyToClipboard(size + " mg@" + value + " " + symbol.toUpperCase());
    var clstoponemg = $(".clstoponemg").val();
    var clstoptwomg = $(".clstoptwomg").val();
    var clstopthreemg = $(".clstopthreemg").val();
    var clstopfourmg = $(".clstopfourmg").val();
    if (leadtime !== "") {
        $("#leadtime").val(leadtime);
    }
    if (clstoponemg === "") {
        $(".clstoponemg").val(size);
        $(".clstoponeprice").val(value);
        return false;
    }
    else if (clstoptwomg === "") {
        $(".clstoptwomg").val(size);
        $(".clstoptwoprice").val(value);
        return false;
    }
    else if (clstopthreemg === "") {
        $(".clstopthreemg").val(size);
        $(".clstopthreeprice").val(value);
        return false;
    }
    else if (clstopfourmg === "") {
        $(".clstopfourmg").val(size);
        $(".clstopfourprice").val(value);
        return false;
    }
}


function copypricerecord(id) {
    var pricemg1 = $("#pricemg_1_" + id).val();
    var price1 = $("#price_1_" + id).val();
    var packsize1 = $("#packsize_1_" + id).val();
    var pricemg2 = $("#pricemg_2_" + id).val();
    var price2 = $("#price_2_" + id).val();
    var packsize2 = $("#packsize_2_" + id).val();
    var pricemg3 = $("#pricemg_3_" + id).val();
    var price3 = $("#price_3_" + id).val();
    var packsize3 = $("#packsize_3_" + id).val();
    var pricemg4 = $("#pricemg_4_" + id).val();
    var price4 = $("#price_4_" + id).val();
    var packsize4 = $("#packsize_4_" + id).val();
    var str = "";
    if (pricemg1 !== "") {
        str += pricemg1 + " @ " + price1 + " @ " + packsize1 + ",";
    }
    if (pricemg2 !== "") {
        str += pricemg2 + " @ " + price2 + " @ " + packsize2 + ",";
    }
    if (pricemg3 !== "") {
        str += pricemg3 + " @ " + price3 + " @ " + packsize3 + ",";
    }
    if (pricemg4 !== "") {
        str += pricemg4 + " @ " + price4 + " @ " + packsize4 + ",";
    }
    copyToClipboard(str);
}

function pastepricerecord(id) {
    if (copyTextPrice === "") {
        toastr.error("Please copy price.");
        return false;
    }
    var data = copyTextPrice.split(',');
    for (var i = 1; i <= data.length; i++) {
        var val = data[i - 1].split('@');
        if (val.length > 1) {
            $("#tblalready").find("#pricemg_" + i + "_" + id).val(parseFloat(val[0]));
            $("#tblalready").find("#price_" + i + "_" + id).val(parseFloat(val[1]));
            $("#tblalready").find("#packsize_" + i + "_" + id).val(parseFloat(val[2]));
        }
        else {
            $("#pricemg" + i + "_" + id).val(val[0]);
        }
    }
    //  refreshDiscountPrice();
}

function copyproductremarkrecord(id) {
    var str = $("#productremark_" + id).val();
    copyToClipboard(str);
}

function pasteproductremarkrecord(id) {
    if (copyTextPrice === "") {
        toastr.error("Please copy product remark.");
        return false;
    }
    var data = copyTextPrice.split(',');
    $("#productremark_" + id).val(data);
}


function finaldiscountcall() {
    debugger;
    var discountMain = $("#myModal").find("#txtfinaldiscount").val();
    var number = $("#myModal").find("#txtfinaldiscount").val();
    if (number !== 0 && number !== '') {
        number = parseFloat(number);
    }
    var totalPrice = 0;
    $("#tblorderconf").find('.clsfinalprice').each(function () {
        var $this = $(this);
        var allprice = [];
        var price = $(this).attr("data-price");
        var quotedetailsId = $(this).attr("data-quotedetailid");
        var qty = $(this).attr("data-qty");
        var itemdis = $("#myModal").find("#itemdiscount_" + quotedetailsId).val();
        if (itemdis !== 0 && itemdis !== '') {
            number = parseFloat(itemdis);
        }

        var coldShipCost = $(this).attr("data-ColdShipCost");
        var addTestCost = $(this).attr("data-AddTestCost");
        var tracebilityCost = $(this).attr("data-TracebilityCost");
        if (price !== '' && price !== null && price !== undefined) {
            var priarr = price.split(',');
            for (var i = 0; i < priarr.length; i++) {
                var filpri = priarr[i];
                if (filpri !== '' && filpri !== null && filpri !== undefined) {
                    var splpri = filpri.split('@');

                    if (splpri[1].indexOf('=') !== -1) {
                        // == present
                        var mg = parseInt(splpri[0].match(/\d+/)[0]);
                        var packsize = parseInt(splpri[1].split('X')[1].split('=')[0]);
                        if (packsize > 1) {
                            mg = mg * packsize;
                        }

                        var pricedata = splpri[1].split('X')[1].split('=')[1].match(/\d+/) !== null ? splpri[1].split('X')[1].split('=')[1].match(/\d+/)[0] : "0";
                        var price = parseInt(pricedata);
                        if (coldShipCost !== '' && coldShipCost !== undefined && coldShipCost !== null) {
                            price += parseInt(coldShipCost);
                        }
                        if (addTestCost !== '' && addTestCost !== undefined && addTestCost !== null) {
                            price += parseInt(addTestCost);
                        }
                        if (tracebilityCost !== '' && tracebilityCost !== undefined && tracebilityCost !== null) {
                            price += parseInt(tracebilityCost);
                        }

                        var arr = {
                            "MG": mg,
                            /*"PRICE": splpri[1].split('X')[1].split('=')[1].match(/\d+/) !== null ? splpri[1].split('X')[1].split('=')[1].match(/\d+/)[0] : "0",*/
                            "PRICE": price,
                            "CURRENCY": splpri[1].split('X')[1].split('=')[1].replace(/\d+/g, '') !== null ? splpri[1].split('X')[1].split('=')[1].replace(/\d+/g, '').trim() : "",
                        };
                        allprice.push(arr);
                    }
                    else {
                        var pricedata = splpri[1].match(/\d+/) !== null ? splpri[1].match(/\d+/)[0] : "0";
                        var price = parseInt(pricedata);
                        if (coldShipCost !== '' && coldShipCost !== undefined && coldShipCost !== null) {
                            price += parseInt(coldShipCost);
                        }
                        if (addTestCost !== '' && addTestCost !== undefined && addTestCost !== null) {
                            price += parseInt(addTestCost);
                        }
                        if (tracebilityCost !== '' && tracebilityCost !== undefined && tracebilityCost !== null) {
                            price += parseInt(tracebilityCost);
                        }
                        var arr = {
                            "MG": splpri[0].match(/\d+/)[0],
                            "PRICE": price,
                            "CURRENCY": splpri[1].replace(/\d+/g, '') !== null ? splpri[1].replace(/\d+/g, '').trim() : "",
                        };
                        allprice.push(arr);
                    }
                }
            }
            if (allprice.length > 0) {
                var result = jQuery.grep(allprice, function (n, i) {
                    return (n.MG == qty);
                });
                if (result.length > 0) {
                    if (number !== 0 && number !== '') {
                        var disPrice = parseFloat(result[0].PRICE) - ((parseFloat(result[0].PRICE) / 100) * number)
                        $(".finalprice_" + parseInt(quotedetailsId)).val(disPrice + " " + result[0].CURRENCY);
                        totalPrice += disPrice;
                    }
                    else {
                        totalPrice += parseFloat(result[0].PRICE);
                        $(".finalprice_" + parseInt(quotedetailsId)).val(result[0].PRICE + " " + result[0].CURRENCY);
                    }
                }
            }
        }
    });
    $("#myModal").find("#totalrecordcount").html("Total : " + totalPrice);
}

$(document).on("keyup", '#txtfinaldiscount', function () {
    var value = $(this).val();
    $("#tblorderconf").find('.clsfinalprice').each(function () {
        var quotedetailsId = $(this).attr("data-quotedetailid");
        $("#myModal").find("#itemdiscount_" + quotedetailsId).val(value);
    });
});
/*$("#myModal").find("#txtfinaldiscount")*/

$(document).on("keyup", '#txtfltprice', function () {
    var value = $(this).val();
    $("#tbodypreviousproduct").find('tr').each(function () {
        $(this).show();
        var pricedata = $(this).children()[7].innerText;
        if (pricedata.indexOf(value) !== -1) {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    });
});

$(document).on("click", '#detFromDBImage', function () {
    var value = $(this).attr("data-catno");
    if (value !== '') {
        var win = window.open('https://synzeal.com/search?q=' + value, '_blank');
        if (win) {
            //Browser has allowed it to be opened
            win.focus();
        } else {
            //Browser has blocked it
            alert('Please allow popups for this website');
        }
    }
});

$(document).on("keyup", '#txtmg', function () {
    if ($(this).val() === '') {
        $("#invoiceQueryModal").find("#txtcalculationresult").val('');
        return false;
    }
    var mgvalue = parseInt($(this).val());
    var catId = $("#invoiceQueryModal").find("#ddlcategory").val();
    var ddlmg = $("#invoiceQueryModal").find("#ddlmg").val();

    var headerIndex = 0;
    var selectedIndex = 0;
    $("#invoiceQueryModal").find("#tr_mainhead").children().each(function (i, v) {
        if (v.innerText == ddlmg) {
            selectedIndex = headerIndex;
        }
        headerIndex += 1;
    });

    var totalMgQty = parseInt(ddlmg.split(' ')[0]);
    var val = parseInt($("#invoiceQueryModal").find("#tr_" + catId)[0].children[selectedIndex].innerText);

    $("#invoiceQueryModal").find("#txtcalculationresult").val((val / totalMgQty) * mgvalue);
});

$(document).on("keyup", '.clsfinalprice', function () {
    var totalPrice = 0;
    $("#myModal").find("#tblorderconf").find('.clsfinalprice').each(function () {
        var price = $(this).val();
        if (price !== '' && price !== null && price !== undefined) {
            var priarr = price.match(/\d+/)[0];
            totalPrice += parseFloat(priarr);
        }
    });
    $("#myModal").find("#totalrecordcount").html("Total : " + totalPrice);
});


var opentabloaded = false;
var inprocesstabloaded = false;
var solvedtabloaded = false;
var closetabloaded = false;
var QueryModule = {
    LoadOpenQuery: function () {
        if (opentabloaded === false) {
            var alltbl = $('#example1').DataTable({
                "ordering": false,
                "processing": true, // for show progress bar
                "serverSide": true, // for process server side
                "filter": true, // this is for disable filter (search box)
                "orderMulti": false, // for disable multiple column at once
                "pageLength": 25,
                "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },

                "ajax": {
                    "url": '/Form/LoadOpenQueryModuledata',
                    "type": "POST",
                    "datatype": "json",
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {

                },
                "fnDrawCallback": function (oSettings) {
                    setTimeout(function () {
                        alltbl.columns.adjust();
                    }, 2000);
                    opentabloaded = true;
                },
                "columnDefs":
                    [],
                "createdRow": function (row, data, dataIndex) {
                },
                "columns": [
                    { "data": "FirstRow", "autoWidth": true, "orderable": false },
                    { "data": null, "autoWidth": true, "orderable": false },
                    { "data": "QueryDate", "autoWidth": true, "orderable": false },
                    { "data": "QueryNo", "autoWidth": true, "orderable": false },
                    { "data": "CompanyName", "name": "CompanyName", "width": "100px" },
                    //{ "data": "PoDateText", "name": "PoDateText", "width": "200px" },
                    { "data": "PONo", "name": "PONo", "width": "100px" },
                    { "data": "ProductName", "name": "ProductName", "width": "18%" },
                    { "data": "CASNo", "name": "CASNo", "width": "6%" },
                    { "data": "CATNo", "name": "CATNo", "width": "6%" },
                    { "data": "Qty", "name": "Qty", "width": "250px" },
                    { "data": "BatchNo", "name": "BatchNo", "width": "250px" },
                    { "data": "QuerySubject", "name": "QuerySubject", "autoWidth": true },
                    //{ "data": "SynzealRemarkText", "name": "SynzealRemarkText" },
                    { "data": "ActionRow", "orderable": false, "width": "100px" }
                ]
            });

            alltbl.on('draw.dt', function () {
                var PageInfo = $('#example1').DataTable().page.info();
                alltbl.column(1, { page: 'current' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1 + PageInfo.start;
                });
            });
        }
    },

    LoadInProcessQuery: function () {
        if (inprocesstabloaded === false) {
            var alltbl = $('#example2').DataTable({
                "searching": true,
                "ordering": false,
                "processing": true, // for show progress bar
                "serverSide": true, // for process server side
                "filter": true, // this is for disable filter (search box)
                "orderMulti": false, // for disable multiple column at once
                "pageLength": 25,
                "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },

                "ajax": {
                    "url": '/Form/LoadInProcessQueryModuledata',
                    "type": "POST",
                    "datatype": "json",
                    "data": function (d) {
                        d.radiobuton = $("#menu1").find('input[name="filterAll"]:checked').val();
                        d.searchbox = $("#menu1").find("#allsearchbox").val();
                    }
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    var date = new Date();
                    var toddate = new Date();
                    var days = -3;
                    if (aData.ClosingDateText !== '' && aData.ClosingDateText !== null && aData.ClosingDateText !== undefined) {
                        var todayDate = date.setDate(date.getDate() + days);
                        var closingDate = new Date(aData.ClosingDateText.split('/')[2], parseInt(aData.ClosingDateText.split('/')[1]) - 1, aData.ClosingDateText.split('/')[0]);
                        if (closingDate >= todayDate && closingDate <= toddate) {
                            $('td', nRow).css('background-color', 'rgb(0, 192, 239)');
                        }
                        if (closingDate <= todayDate) {
                            $('td', nRow).css('background-color', '#fed8b1');
                        }
                    }
                },
                "fnDrawCallback": function (oSettings) {
                    setTimeout(function () {
                        alltbl.columns.adjust();
                    }, 2000);
                    inprocesstabloaded = true;
                },
                "columnDefs":
                    [],
                "createdRow": function (row, data, dataIndex) {
                },
                "columns": [
                    { "data": null, "autoWidth": true, "orderable": false },
                    { "data": "FirstRow", "autoWidth": true, "orderable": false },
                    { "data": "QueryDate", "autoWidth": true, "orderable": false },
                    { "data": "QueryNo", "autoWidth": true, "orderable": false },
                    { "data": "SubStatus", "autoWidth": true, "orderable": false },
                    { "data": "CompanyName", "name": "CompanyName", "width": "100px" },
                    //{ "data": "PoDateText", "name": "PoDateText", "width": "200px" },
                    { "data": "PONo", "name": "PONo", "width": "100px" },
                    { "data": "ProductName", "name": "ProductName", "width": "18%" },
                    { "data": "CASNo", "name": "CASNo", "width": "6%" },
                    { "data": "CATNo", "name": "CATNo", "width": "6%" },
                    { "data": "Qty", "name": "Qty", "width": "250px" },
                    { "data": "BatchNo", "name": "BatchNo", "width": "250px" },
                    { "data": "QuerySubject", "name": "QuerySubject", "autoWidth": true },
                    //{ "data": "SynzealRemarkText", "name": "SynzealRemarkText" },
                    { "data": "ClosingDateText", "name": "SynzealRemarkText" },
                    { "data": "ActionRow", "orderable": false, "width": "100px" }
                ]
            });

            alltbl.on('draw.dt', function () {
                var PageInfo = $('#example2').DataTable().page.info();
                alltbl.column(0, { page: 'current' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1 + PageInfo.start;
                });
            });
        }
    },

    LoadSolvedQuery: function () {

        if (solvedtabloaded === false) {
            var alltbl = $('#example3').DataTable({
                "ordering": false,
                "processing": true, // for show progress bar
                "serverSide": true, // for process server side
                "filter": true, // this is for disable filter (search box)
                "orderMulti": false, // for disable multiple column at once
                "pageLength": 25,
                "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },

                "ajax": {
                    "url": '/Form/LoadSolvedQueryModuledata',
                    "type": "POST",
                    "datatype": "json",
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {

                },
                "fnDrawCallback": function (oSettings) {


                    setTimeout(function () {
                        alltbl.columns.adjust();
                    }, 2000);
                    solvedtabloaded = true;
                },
                "columnDefs":
                    [],
                "createdRow": function (row, data, dataIndex) {
                },
                "columns": [
                    { "data": null, "autoWidth": true, "orderable": false },
                    { "data": "FirstRow", "autoWidth": true, "orderable": false },
                    { "data": "QueryDate", "autoWidth": true, "orderable": false },
                    { "data": "QueryNo", "autoWidth": true, "orderable": false },
                    { "data": "SubStatus", "autoWidth": true, "orderable": false },
                    { "data": "CompanyName", "name": "CompanyName", "width": "100px" },
                    //{ "data": "PoDateText", "name": "PoDateText", "width": "200px" },
                    { "data": "PONo", "name": "PONo", "width": "100px" },
                    { "data": "ProductName", "name": "ProductName", "width": "18%" },
                    { "data": "CASNo", "name": "CASNo", "width": "6%" },
                    { "data": "CATNo", "name": "CATNo", "width": "6%" },
                    { "data": "Qty", "name": "Qty", "width": "250px" },
                    { "data": "BatchNo", "name": "BatchNo", "width": "250px" },
                    { "data": "QuerySubject", "name": "QuerySubject", "autoWidth": true },
                    //{ "data": "SynzealRemarkText", "name": "SynzealRemarkText" },
                    { "data": "ClosingDateText", "name": "SynzealRemarkText" },
                    { "data": "ActionRow", "orderable": false, "width": "100px" }
                ]
            });

            alltbl.on('draw.dt', function () {
                var PageInfo = $('#example3').DataTable().page.info();
                alltbl.column(0, { page: 'current' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1 + PageInfo.start;
                });
            });
        }
    },

    LoadCloseQuery: function () {
        if (closetabloaded === false) {
            var alltbl = $('#example4').DataTable({
                "ordering": false,
                "processing": true, // for show progress bar
                "serverSide": true, // for process server side
                "filter": true, // this is for disable filter (search box)
                "orderMulti": false, // for disable multiple column at once
                "pageLength": 25,
                "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },

                "ajax": {
                    "url": '/Form/LoadCompletedQueryModuledata',
                    "type": "POST",
                    "datatype": "json",
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {



                },
                "fnDrawCallback": function (oSettings) {


                    setTimeout(function () {
                        alltbl.columns.adjust();
                    }, 2000);
                    closetabloaded = true;
                },
                "columnDefs":
                    [],
                "createdRow": function (row, data, dataIndex) {
                },
                "columns": [
                    { "data": "FirstRow", "autoWidth": true, "orderable": false },
                    { "data": null, "autoWidth": true, "orderable": false },
                    { "data": "QueryDate", "autoWidth": true, "orderable": false },
                    { "data": "QueryNo", "autoWidth": true, "orderable": false },
                    { "data": "SubStatus", "autoWidth": true, "orderable": false },
                    { "data": "CompanyName", "name": "CompanyName", "width": "100px" },
                    //{ "data": "PoDateText", "name": "PoDateText", "width": "200px" },
                    { "data": "PONo", "name": "PONo", "width": "100px" },
                    { "data": "ProductName", "name": "ProductName", "width": "18%" },
                    { "data": "CASNo", "name": "CASNo", "width": "6%" },
                    { "data": "CATNo", "name": "CATNo", "width": "6%" },
                    { "data": "Qty", "name": "Qty", "width": "250px" },
                    { "data": "BatchNo", "name": "BatchNo", "width": "250px" },
                    { "data": "QuerySubject", "name": "QuerySubject", "autoWidth": true },
                    //{ "data": "SynzealRemarkText", "name": "SynzealRemarkText" },
                    { "data": "ClosingDateText", "name": "SynzealRemarkText" },
                    { "data": "ActionRow", "orderable": false, "width": "100px" }
                ]
            });

            alltbl.on('draw.dt', function () {
                var PageInfo = $('#example4').DataTable().page.info();
                alltbl.column(1, { page: 'current' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1 + PageInfo.start;
                });
            });
        }
    },

};


var priceusdarrquote = [];
var priceinrarrquote = [];

function priceclubquote() {
    var cnt = 0; var chkcnt = 0;
    var mgdata = [];

    $(".clschk").each(function () {
        if ($(this).is(":checked")) {
            chkcnt += 1;
            mgdata.push($(this).val());
        }
    });

    if (chkcnt === 0) {
        toastr.error("Please select atleast one price option.");
        return false;
    }

    $(".clsSaverow").each(function () {
        if ($(this).is(":checked")) {
            var id = $(this).val();
            var str = '';
            var strinr = '';
            for (var i = 0; i < mgdata.length; i++) {
                var elementId = '';
                var elementInrId = '';
                if (mgdata[i] === "10") {
                    elementId = 'TenUSD_' + id;
                    elementInrId = 'TenPrice_' + id;
                }
                if (mgdata[i] === "25") {
                    elementId = 'TwentyfiveUSD_' + id;
                    elementInrId = 'TwentyFivePrice_' + id;
                }
                if (mgdata[i] === "50") {
                    elementId = 'FiftyUSD_' + id;
                    elementInrId = 'FiftyPrice_' + id;
                }
                if (mgdata[i] === "100") {
                    elementId = 'OnehundredUSD_' + id;
                    elementInrId = 'HundredPrice_' + id;
                }
                if (mgdata[i] === "250") {
                    elementId = 'TwohundredFiftyUSD_' + id;
                    elementInrId = 'TwoHundredPrice_' + id;
                }
                if (mgdata[i] === "500") {
                    elementId = 'FivehundredUSD_' + id;
                    elementInrId = 'FivehundredPrice_' + id;
                }
                if (mgdata[i] === "1000") {
                    elementId = 'OneThousandUSD_' + id;
                    elementInrId = 'OneThousandPrice_' + id;
                }
                str += mgdata[i] + " @ " + $("#" + elementId).val() + ",";
                strinr += mgdata[i] + " @ " + $("#" + elementInrId).val() + ",";
            }
            var obj = {
                "ProductId": id,
                "Price": str,
                "LeadTime": $("#LeadTime_" + id).val(),
                "ProductRemark": $("#ProductRemark_" + id).val()
            };
            priceusdarrquote.push(obj);
            var objinr = {
                "ProductId": id,
                "Price": strinr,
                "LeadTime": $("#LeadTime_" + id).val(),
                "ProductRemark": $("#ProductRemark_" + id).val()
            };
            priceinrarrquote.push(objinr);
            cnt += 1;
        }
    });

    if (cnt === 0) {
        toastr.error("Please select atleast one product.");
        return false;
    }
    $.ajax({
        type: "GET",
        url: "/Form/GetAllCompanydata",
        data: {},
        success: function (resultscop) {
            $("#CompanyId").empty();
            var str = "<div class='col-md-10'><select id='ddlcompanyid' name='ddlcompanyid' class='form-control'>";

            $(resultscop).each(function (i, v) {
                str += "<option value='" + v.Value + "'>" + v.Text + "</option>";
            });
            str += "</select></div><div class='col-md-2'><a href='javascript:void(0)' class='btn btn-primary' onclick='generatepricescreenquote()'>Generate</a></div><div class='clearfix'></div>";
            $('#myModal').find('.modal-body').html(str);
            $('#myModal').modal({ show: true });
            $('#myModal').find(".modal-dialog").css("width", "580px");
        }
    });



}

function generatepricescreenquote() {
    var selectedCompanyId = $('#ddlcompanyid').val();

    if (selectedCompanyId === '') {
        toastr.error("Please select company from dropdown.");
        return false;
    }
    $.ajax({
        type: "POST",
        url: "/Form/clubquotepricescreen",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        traditional: true, // add this
        data: JSON.stringify({ modelusd: priceusdarrquote, modelinr: priceinrarrquote, companyId: selectedCompanyId }),
        success: function (result) {
            toastr.success("You have added new quote.");
            window.location.href = "/Form/Quote/" + result;
        }
    });
}

function GetAllBatchNo(id, quotedetailsId, visibleFormDownload = false) {
    $.ajax({
        url: '/Form/GetAllBatchNo?id=' + id + '&actiontxt=project&quoteDetailsId=' + quotedetailsId + '&visibleFormDownload=' + visibleFormDownload,
        data: {},
        type: 'GET',
        traditional: true,
        dataType: 'json',
        success: function (data) {
            $('#invoiceQueryModal .modal-body').html(data.html);
            $('#invoiceQueryModal').modal({ show: true });
            $('#invoiceQueryModal .modal-content').css('width', '1080px');
            $('#invoiceQueryModal .modal-dialog').css('width', '1080px');
        }
    });
}

function GetQueryDataFromCatNo(catNo, pagename = '') {
    $.ajax({
        url: '/Form/GetQueryDataFromCatNo?catNo=' + catNo + '&pagename=' + pagename,
        data: {},
        type: 'GET',
        traditional: true,
        dataType: 'json',
        success: function (data) {
            $('#invoiceQueryModal .modal-body').html(data.html);
            $('#invoiceQueryModal').modal({ show: true });
            $('#invoiceQueryModal .modal-content').css('width', '1080px');
            $('#invoiceQueryModal .modal-dialog').css('width', '1080px');
        }
    });
}
$(document).on("click", ".clsadditionaltestquote", function () {
    if ($(this).is(":checked")) {
        $(this).css('background', '#00deff12');
    }
    else {
        $(this).css('background', '');
    }
});

$(document).on("change", ".ddladditionalbatch", function () {
    $.ajax({
        url: '/Form/CheckProductUsingBatchNo?szdetailsid=' + $(this).attr('data-quotationdetailsid') + '&batchId=' + $(this).val(),
        data: {},
        type: 'GET',
        traditional: true, // add this
        dataType: 'json',
        success: function (data) {
            if (data.Data === "Yes") {
                var str = '';
                if (window.location.href.toLowerCase().indexOf("dispatch") !== -1) {
                    str += '<table class="table table-bordered table-striped"><thead><tr><th>PO No</th><th>Project Status</th><th>Team leader</th><th>Product Name</th><th>QTY</th><th>CAS No</th><th>CAT No</th><th>Batch Code</th><th>Estimated Date</th><th>Data Remark</th><th>Reason</th><th>Pack Size</th></tr></thead>';
                    $(data.Record).each(function (i, v) {
                        var clsdata = '';
                        if (v.ProjectTypeText === "In-Stock") {
                            clsdata = 'rgb(202, 251, 217)';
                        }
                        if (v.ProjectTypeText === "In-Stock" && v.MoveToDispatch != true) {
                            clsdata = 'rgb(202, 251, 217)';
                        }
                        if (v.MoveToDispatch && (v.DispatchedStatus == "0" || v.DispatchedStatus == null)) {
                            clsdata = '#ffe6b3';
                        }
                        if (v.MoveToDispatch && v.DispatchedStatus == "1") {
                            clsdata = 'rgb(250, 209, 255)';
                        }
                        if (v.MoveToInvoice) {
                            clsdata = '#ff9999';
                        }

                        str += "<tr style='background-color:" + clsdata + "'>";
                        str += "<td>" + v.PONumberText + "</td><td>" + v.StrProjectStatus + "</td><td>" + v.ScientistName + "</td><td>" + v.ProductNameText + "</td><td>" + v.RequiredQty + "</td><td>" + v.CASNoText + "</td><td>" + v.CATNoText + "</td><td>" + v.AdditionalBatchNoText + "</td><td>" + v.EstimateDispatchDateStr + "</td><td>" + v.DataRemark + "</td><td>" + v.Reason + "</td><td>" + v.OrderRemark + "</td>";
                        str += "</tr>";
                    });
                    str += "</table>";
                }
                else {
                    str += '<table class="table table-bordered table-striped"><thead><tr><th>PO Date</th><th>PO No</th><th>Company Name</th><th>Project Status</th><th>Team leader</th><th>Product Name</th><th>QTY</th><th>CAS No</th><th>CAT No</th><th>Batch Code</th><th>Estimated Date</th><th>Data Remark</th><th>Reason</th><th>Pack Size</th><th>Activity</th><th>Status</th></tr></thead>';
                    $(data.Record).each(function (i, v) {
                        var clsdata = '';
                        if (v.ProjectTypeText === "In-Stock") {
                            clsdata = 'rgb(202, 251, 217)';
                        }
                        if (v.ProjectTypeText === "In-Stock" && v.MoveToDispatch != true) {
                            clsdata = 'rgb(202, 251, 217)';
                        }
                        if (v.MoveToDispatch && (v.DispatchedStatus == "0" || v.DispatchedStatus == null)) {
                            clsdata = '#ffe6b3';
                        }
                        if (v.MoveToDispatch && v.DispatchedStatus == "1") {
                            clsdata = 'rgb(250, 209, 255)';
                        }
                        if (v.MoveToInvoice) {
                            clsdata = '#ff9999';
                        }
                        str += "<tr style='background-color:" + clsdata + "'>";
                        str += "<td>" + v.PODateText + "</td><td>" + v.PONumberText + "</td><td>" + v.CompanyName + "</td><td>" + v.StrProjectStatus + "</td><td>" + v.ScientistName + "</td><td>" + v.ProductNameText + "</td><td>" + v.RequiredQty + "</td><td>" + v.CASNoText + "</td><td>" + v.CATNoText + "</td><td>" + v.AdditionalBatchNoText + "</td><td>" + v.EstimateDispatchDateStr + "</td><td>" + v.DataRemark + "</td><td>" + v.Reason + "</td><td>" + v.OrderRemark + "</td><td>" + v.ActivityStatusText + "</td><td>" + v.StrProjectStatus + "</td>";
                        str += "</tr>";
                    });
                    str += "</table>";
                }

                $('.modal-body').html("<p style='font-size:18px'>Data is already available in project screen. Please check below PO number(s) for comparison. <br><br>" + str);
                $('#myModal').modal({ show: true });
                $(".modal-dialog").css("width", "1200px");
            }
        }
    });

    var quotedetailsid = $(this).attr('data-quotationdetailsid');
    var tablename = $(this).attr("data-tablename");
    var inventoryId = $(this).val();
    var $this = $(this);
    $.ajax({
        url: '/Form/GetCOADetailsFromBatchId?batchId=' + inventoryId,
        data: {},
        type: 'GET',
        traditional: true, // add this
        dataType: 'json',
        success: function (data) {
            $("#" + $this.closest('table').attr("id")).find("#coa_" + quotedetailsid).html(data);
        }
    });


});

function attachment(id) {
    $("#myModal").modal('show');
    $("#myModal").find(".modal-body").load("../Form/AttachmentRecord?id=" + id);

}

function DeleteMoveFromProject(quotedetailId) {
    swal({
        title: "Are you sure want to move back it?",
        //text: str,
        //html: true,
        type: "warning",
        customClass: 'swalwide',
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Yes, Move it!",
        cancelButtonText: "No, cancel!",
        closeOnConfirm: true,
        closeOnCancel: true
    },
        function (isConfirm) {
            if (isConfirm) {
                $.ajax({
                    type: "GET",
                    url: "/Form/BackMoveFromProject?id=" + quotedetailId,
                    dataType: "json",
                    traditional: true,
                    data: {},
                    success: function (result) {
                        toastr.success("You have move back this product.");
                        Quote.getProductListForQuote($("#QuoteId").val());
                    }
                });
            } else {
                toastr.error("Your added product is safe :)");
            }
        });
}


function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

function Multiplequoteopen(id) {
    $.ajax({
        url: '/Form/MultipleQuote?id=' + id,
        data: {},
        type: 'GET',
        traditional: true, // add this
        dataType: 'json',
        success: function (result) {
            if (!result.success) {
                toastr.error(result.message);
                return false;
            }
            else {
                if (result.data.length === 1) {
                    window.open("/Form/Quote/" + result.data[0]);
                }
                else {
                    $(result.data).each(function (i, v) {
                        window.open("/Form/Quote/" + v);
                    });
                }
            }
        }
    });
}

function Quicksearch() {
    $.ajax({
        url: '/Form/Quicksearch',
        data: {},
        type: 'POST',
        traditional: true, // add this
        dataType: 'json',
        success: function (data) {
            $('.modal-body').html(data.html);
            $('#myModal').modal({ show: true });
            $(".modal-dialog").css("width", "1080px");
            $('#myModal').find(".modal-footer").hide();
        }
    });
}
// Read a page's GET URL variables and return them as an associative array.
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function DownloadChemDraw(formid) {
    window.location.href = "/Form/DownloadChemDraw/" + formid;
}

function GenerateInvoice() {
    var InvoiceNo = $("#myModal").find("#InvoiceNo").val();
    var InvoiceDate = $("#myModal").find("#InvoiceDate").val();
    var PaymentTerms = $("#myModal").find("#PaymentTerms").val();
    var TrackingNo = $("#myModal").find("#TrackingNo").val();
    var Add1 = $("#myModal").find("#Add1").val();
    var Add2 = $("#myModal").find("#Add2").val();
    var City = $("#myModal").find("#City").val();
    var State = $("#myModal").find("#State").val();
    var Country = $("#myModal").find("#Country").val();
    var Telno = $("#myModal").find("#Telno").val();
    var ShipAdd1 = $("#myModal").find("#ShipAdd1").val();
    var ShipAdd2 = $("#myModal").find("#ShipAdd2").val();
    var ShipCity = $("#myModal").find("#ShipCity").val();
    var ShipState = $("#myModal").find("#ShipState").val();
    var ShipCountry = $("#myModal").find("#ShipCountry").val();
    var ShipTelno = $("#myModal").find("#ShipTelno").val();

    if (InvoiceNo === '' || InvoiceNo === null || InvoiceNo === undefined) {
        toastr.error("Please enter invoice number.");
        return false;
    }
    if (InvoiceDate === '' || InvoiceDate === null || InvoiceDate === undefined) {
        toastr.error("Please enter invoice date.");
        return false;
    }
    if (TrackingNo === '' || TrackingNo === null || TrackingNo === undefined) {
        toastr.error("Please enter tracking number.");
        return false;
    }
    if (PaymentTerms === '' || PaymentTerms === null || PaymentTerms === undefined) {
        toastr.error("Please enter payment terms.");
        return false;
    }

    if (Add1 === '' || City === '' || State === '' || Country === '' || ShipAdd1 === '' || ShipCity === '' || ShipState === '' || ShipCountry === '') {
        toastr.error("Please enter full address.");
        return false;
    }
    if (Telno === '' || ShipTelno === '') {
        toastr.error("Please enter telephone number.");
        return false;
    }
}

function GetSuggestedPrice() {
    $("#txtsuggestedprice").text("Loading...");
    $.ajax({
        url: '/Form/GetSuggestedPrice?searchValue=' + suggestedPriceSearchValue + '&company=' + suggestedpricecompany + '&quotedetailsid=' + suggestedquotedetailsid,
        data: {},
        async: true,
        type: 'POST',
        success: function (result) {
            if (result.type === "fail") {
                $("#txtsuggestedprice").text("");
                toastr.error(result.data);
            }
            else {
                if (result.data === '') {
                    $("#txtsuggestedprice").text("Sorry, No data found");
                }
                else {
                    $("#txtsuggestedprice").text(result.data);
                }
            }
        }
    });
}

$(document).on("change", '#ddltype', function () {
    GetExchangeRate();
});

var usdinrExchangeRate = '';
var inrusdExchangeRate = '';
function GetExchangeRate() {
    var type = $("#ddltype").val();
    var value = $('#txtbase').val();
    var url = 'https://free.currconv.com/api/v7/convert?q=INR_USD&compact=ultra&apiKey=959999c8ae8c4ebed5b8';
    //var url = "https://api.exchangeratesapi.io/latest?base=" + type;

    if (type === "USD") {
        url = 'https://free.currconv.com/api/v7/convert?q=USD_INR&compact=ultra&apiKey=959999c8ae8c4ebed5b8';
    }

    if (type === "USD" && usdinrExchangeRate !== '') {
        $("#txtdest").val(parseInt(value) * parseFloat(usdinrExchangeRate));
        return false;
    }
    if (type === "INR" && inrusdExchangeRate !== '') {
        $("#txtdest").val(parseInt(value) * parseFloat(inrusdExchangeRate));
        return false;
    }

    $.ajax({
        type: "GET",
        url: url,
        dataType: "json",
        traditional: true,
        data: {},
        success: function (result) {
            if (type === "INR") {
                inrusdExchangeRate = result.INR_USD;
                $("#txtdest").val(parseInt(value) * parseFloat(result.INR_USD));
            }
            else {
                usdinrExchangeRate = result.USD_INR;
                $("#txtdest").val(parseInt(value) * parseFloat(result.USD_INR));
            }
        }
    });
}
$(document).on("keyup", '#txtbase', function () {
    GetExchangeRate();
});

function showquoteaddress(quoteId) {
    $.ajax({
        type: "GET",
        url: "/Form/GetQuoteById?Id=" + quoteId,
        dataType: "json",
        traditional: true,
        data: {},
        success: function (result) {
            $('.modal-body').html(result);
            $('#myModal').modal({ show: true });
            $(".modal-dialog").css("width", "1080px");
        }
    });
}

function CountRows(groupingArray, tableName) {
    var abc = $('#' + tableName);
    var arr = [];

    $(abc).find(".group").each(function (index, value) {
        var groupname = $(this).attr("data-groupname");
        this.innerHTML = this.innerHTML + " (" + groupingArray[groupname] + ")"
    });

}

function openselectcategoryprice(mg, value, type) {
    LoadAllCategoryPrice(mg, value, type);
}

function GetPurchaseExcelData(casNo, elementId) {
    $.ajax({
        type: "GET",
        url: "/Form/GetPurchaseExcelData?casno=" + casNo,
        dataType: "json",
        traditional: true,
        async: true,
        data: {},
        success: function (result) {
            var str = "<table class='table table-bordered table-striped dataTable no-footer' style='width:100%'><thead><tr><th>Product Name</th><th>CAS No</th><th>Supplier Catalogue No</th><th>Make</th><th>Price</th><th>Lead Time</th></tr></thead>";
            if (result.length > 0) {
                $(result).each(function (i, v) {
                    str += "<tr><td style='word-break: break-all;'>" + v.ProductName + "</td><td>" + v.CASNo + "</td><td>" + v.SupplierCATNo + "</td><td>" + v.Make + "</td><td>" + v.Price + "</td><td>" + v.Leadtime + "</td></tr>";
                });
                str += "</table > ";
            }
            $("#" + elementId).html(str);
        }
    });
}

function saveproductremark() {
    var cnt = 0;
    var proidscount = 0;
    //$(".clsproductremarktext").each(function () {
    //    proidscount += 1;
    //});

    $(".delclass").each(function () {
        if ($(this).is(":checked")) {
            proidscount += 1;
        }
    });

    if (proidscount === 0) {
        toastr.error("Please select one product");
        return false;
    }

    $(".delclass").each(function () {
        if ($(this).is(":checked")) {
            var id = $(this).val();
            var val = $("#productremark_").val();
            if (id !== '') {
                $.ajax({
                    type: "GET",
                    url: "/Form/UpdateQuoteDetailsProductRemark?id=" + id + "&productremark=" + val,
                    dataType: "json",
                    async: false,
                    data: {},
                    success: function (result) {
                    },
                    complete: function (data) {
                        cnt += 1;
                        successalertproremark(cnt, proidscount);
                    }
                });
            }
            else {
                cnt += 1;
                successalertproremark(cnt, proidscount);
            }
        }
    });

    //$(".clsproductremarktext").each(function () {
    //    var val = $(this).val();
    //    var id = $(this).attr('id').replace('productremark_', '');
    //    if (val !== '') {
    //        $.ajax({
    //            type: "GET",
    //            url: "/Form/UpdateQuoteDetailsProductRemark?id=" + id + "&productremark=" + val ,
    //            dataType: "json",
    //            async: false,
    //            data: {},
    //            success: function (result) {
    //            },
    //            complete: function (data) {
    //                cnt += 1;
    //                successalertproremark(cnt, proidscount);
    //            }
    //        });
    //    }
    //    else {
    //        cnt += 1;
    //        successalertproremark(cnt, proidscount);
    //    }
    //});
}
function successalertproremark(count, forloopcount) {
    if (count === forloopcount) {
        toastr.success("Product Remark saved successfully..");
    }
}

function UploadProducts() {
    var ids = [];
    var errcnt = 0;
    $("#tblalready").find('.delclass').each(function () {
        if ($(this).is(":checked")) {
            var id = $(this).val();
            var catNo = $("#catno_").val();
            if (catNo !== '' && catNo !== undefined) {
                errcnt += 1;
            }
            else {
                ids.push(id);
            }
        }
    });

    if (errcnt !== 0) {
        toastr.error("Please select product without CAT No");
        return false;
    }

    if (ids.length === 0) {
        toastr.error("Please select atleast one product");
        return false;
    }

    $.ajax({
        url: '/Form/MoveToUploadProduct',
        data: { id: ids },
        type: 'POST',
        traditional: true, // add this
        dataType: 'json',
        success: function (data) {
            if (data.success) {
                toastr.success("You submitted this product for the Upload on website.");
            }
        }
    });
}


function collapseexpanddiv(divid) {
    var status = $("#" + divid).attr("data-collapstatus");
    if (status === "Expand") {
        $("#" + divid).css('display', 'block');
        $("#" + divid).attr("data-collapstatus", "Collapse");
    }
    else {
        $("#" + divid).css('display', 'none');
        $("#" + divid).attr("data-collapstatus", "Expand");
    }
}
$(document).on("keyup", '#myModal #allquicksearchbox, #home #allsearchbox , #tabtobe #allsearchbox , #tabtobedistributor #allsearchbox', function () {
    var value = $(this).val();
    var fltvalue = 'filterAll';
    var parentDivId = $(this).closest('.tab-pane').attr('id');
    if (parentDivId === 'undefined' || parentDivId === undefined) {
        parentDivId = 'myModal';
        fltvalue = 'filterquickAll';
    }
    if (value !== '') {
        var cnt = occurrences(value, '-');
        //if (value.match(/[A-E]/gi)) {
        //    //Company Name
        //    $("#" + parentDivId).find("input[name=" + fltvalue + "][value='company']").prop("checked", true);
        //}
        if (value.match(/^\d/) && cnt < 2) {
            //PO
            $("#" + parentDivId).find("input[name=" + fltvalue + "][value='ponumber']").prop("checked", true);
        }
        if (value.startsWith("SZ-") && cnt === 2) {
            //Quote Ref
            $("#" + parentDivId).find("input[name=" + fltvalue + "][value='quoteid']").prop("checked", true);
        }
        if (value.startsWith("SZ-") && cnt === 1) {
            //Catalogue No
            $("#" + parentDivId).find("input[name=" + fltvalue + "][value='cat']").prop("checked", true);
        }
        if (value.match(/^\d/) && cnt === 2) {
            //CAS No
            $("#" + parentDivId).find("input[name=" + fltvalue + "][value='cas']").prop("checked", true);
        }
    }
});


function occurrences(string, subString, allowOverlapping) {

    string += "";
    subString += "";
    if (subString.length <= 0) return (string.length + 1);

    var n = 0,
        pos = 0,
        step = allowOverlapping ? 1 : subString.length;

    while (true) {
        pos = string.indexOf(subString, pos);
        if (pos >= 0) {
            ++n;
            pos += step;
        } else break;
    }
    return n;
}

function QueryDetail(catNo) {
    $.ajax({
        url: '/Form/GetQueryModuleByProductId?catNo=' + catNo,
        data: {},
        type: 'GET',
        success: function (data) {
            $('.modal-body').html(data);
            $('#myModal').modal({ show: true });
            $(".modal-dialog").css("width", "1180px");
        }
    });
}

function copy(str) {
    copyToClipboard(str);
}

function copymasterRecord(elementId) {
    copy($("#" + elementId).val());
}

function GetQuotationAuditLog(id, propertyname = "") {
    $.ajax({
        url: '/Form/GetQuotationAuditLog?id=' + id + '&propertyName=' + propertyname,
        data: {},
        type: 'GET',
        traditional: true,
        dataType: 'json',
        success: function (data) {
            $('#invoiceQueryModal .modal-body').html(data.html);
            $('#invoiceQueryModal').modal({ show: true });
        }
    });
}

function lessDays(date, day) {
    var newdate = new Date(date);
    newdate.setDate(newdate.getDate() - day); // minus the date
    return new Date(newdate);
}

function getrawmaterialdatabyproject(productId) {
    $.ajax({
        url: '/Form/GetRawMaterialDataByProject?productid=' + productId,
        data: {},
        type: 'GET',
        traditional: true,
        dataType: 'json',
        success: function (data) {
            if (data !== "") {
                $('#invoiceQueryModal .modal-body').html(data);
                $('#invoiceQueryModal').modal({ show: true });
                $('#invoiceQueryModal').find(".modal-dialog").css("width", "1800px");
                $('#invoiceQueryModal').find(".modal-content").css("width", "1800px"); 
            }
            else {
                toastr.error("No Record Found");
            }
        }
    });
}

function getrawmaterialdatabycasno(productId) {
    $.ajax({
        url: '/Form/GetRawMaterialDataByCASNo?productid=' + productId,
        data: {},
        type: 'GET',
        traditional: true,
        dataType: 'json',
        success: function (data) {
            if (data !== "") {
                $('#invoiceQueryModal .modal-body').html(data);
                $('#invoiceQueryModal').modal({ show: true });
                $('#invoiceQueryModal').find(".modal-dialog").css("width", "1800px");
                $('#invoiceQueryModal').find(".modal-content").css("width", "1800px");
            }
            else {
                toastr.error("No Record Found");
            }
        }
    });
}


function addnewraw(displayOrder, quoteid) {
    $.ajax({
        url: '/Form/GetAddNewRawInQuote?displayOrder=' + displayOrder + "&quoteid=" + quoteid,
        data: {},
        type: 'GET',
        traditional: true,
        dataType: 'json',
        success: function (data) {
            if (data !== "") {
                $('#invoiceQueryModal .modal-body').html(data);
                $('#invoiceQueryModal').modal({ show: true });
                $('#invoiceQueryModal').find(".modal-dialog").css("width", "1080px");
                $('#invoiceQueryModal').find(".modal-content").css("width", "1080px");
            }
            else {
                toastr.error("No Record Found");
            }
        }
    });
}
function PurchasesummaryGenerate(id, pagename = '' ) {
    $.ajax({
        url: '/Form/AddPurchaseSummary?id=' + id + '&pagename=' + pagename ,
        data: {},
        type: 'GET',
        traditional: true, // add this
        dataType: 'json',
        success: function (data) {
            $('#invoiceQueryModal .modal-body').html(data.html);
            $('#invoiceQueryModal').modal({ show: true });
        }
    });
} 







//
// $('#element').donetyping(callback[, timeout=1000])
// Fires callback when a user has finished typing. This is determined by the time elapsed
// since the last keystroke and timeout parameter or the blur event--whichever comes first.
//   @callback: function to be called when even triggers
//   @timeout:  (default=1000) timeout, in ms, to to wait before triggering event if not
//              caused by blur.
// Requires jQuery 1.7+
//
; (function ($) {
    $.fn.extend({
        donetyping: function (callback, timeout) {
            timeout = timeout || 1e3; // 1 second default timeout
            var timeoutReference,
                doneTyping = function (el) {
                    if (!timeoutReference) return;
                    timeoutReference = null;
                    callback.call(el);
                };
            return this.each(function (i, el) {
                var $el = $(el);
                // Chrome Fix (Use keyup over keypress to detect backspace)
                // thank you @palerdot
                $el.is(':input') && $el.on('keyup keypress paste', function (e) {
                    // This catches the backspace button in chrome, but also prevents
                    // the event from triggering too preemptively. Without this line,
                    // using tab/shift+tab will make the focused element fire the callback.
                    if (e.type == 'keyup' && e.keyCode != 8) return;

                    // Check if timeout has been set. If it has, "reset" the clock and
                    // start over again.
                    if (timeoutReference) clearTimeout(timeoutReference);
                    timeoutReference = setTimeout(function () {
                        // if we made it here, our timeout has elapsed. Fire the
                        // callback
                        doneTyping(el);
                    }, timeout);
                }).on('blur', function () {
                    // If we can, fire the event since we're leaving the field
                    doneTyping(el);
                });
            });
        }
    });
})(jQuery);


function deleterules(id) {
    swal({
        title: "Are you sure want to delete?",
        text: "",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        closeOnConfirm: true,
        closeOnCancel: true
    },
        function (isConfirm) {
            if (isConfirm) {
                $.ajax({
                    type: "GET",
                    url: "/Form/DeleteRule?id=" + id,
                    dataType: "json",
                    traditional: true,
                    data: {},
                    success: function (result) {
                        window.location.reload(true);
                    },
                    error: function (err) {
                        alert(err.statusText);
                    }
                });
            } else {
                setTimeout(function () { window.location.href = "/Form/RuleManagement"; }, 2000);
            }
        });
}

function uploadglpfiles(id) {
    $("#myModal").modal('show');
    $("#myModal").find(".modal-body").load("../Form/uploadglpfiles?id=" + id);
}




var purchaseinhousetabtabloaded = false;
function purchaseinhousetab() {
    if (purchaseinhousetabtabloaded === false) {
        var tblpurchaseloginrfq = $('#tblpurchaseinhouse').DataTable({
            //"fixedHeader": {
            //    header: true
            //},
            "ordering": false,
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "pageLength": 25,
            "language": { processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> ' },
            "ajax": {
                "url": "../Form/LoadPurchaseInhouseData",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    /*d.instockother = $("#filterinstock").is(":checked") + "," + $("#filterother").is(":checked")*/
                    d.purchasestatusddl = $('#filrfqpurchaserfqstatus').val()
                }
            },
            "fnDrawCallback": function (oSettings) {
                calledExtra();
                purchaseinhousetabtabloaded = true;
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
               
            },
            "createdRow": function (row, data, dataIndex) {
                // Set the data-status attribute, and add a class
                $(row).attr('id', 'tr_' + data.Id);
                $(row).attr('class', 'clstr');
            },
            "columns": [
                { "data": "ChkRow", "autoWidth": true, "orderable": false },
                { "data": "StrPurchaseDate", "autoWidth": true, "orderable": false },
                { "data": "StrPurchaseDDLStatus", "autoWidth": true, "orderable": false },
                { "data": "PONumber", "autoWidth": true, "orderable": false },
                { "data": "ProductName", "autoWidth": true, "orderable": false },
                { "data": "CASNo", "autoWidth": true, "orderable": false },
                { "data": "CATNo", "autoWidth": true, "orderable": false },
                { "data": "RequiredQty", "autoWidth": true, "orderable": false },
                { "data": "BatchNo", "autoWidth": true, "orderable": false },
                { "data": "StrEstimateCompleteDate", "autoWidth": true, "orderable": false },
                { "data": "LeadTime", "autoWidth": true, "orderable": false },
                { "data": "PurchaseStatus", "autoWidth": true, "orderable": false },
                { "data": "PurchaseRemark", "autoWidth": true, "orderable": false },
                { "data": "Reason", "autoWidth": true, "orderable": false },
                { "data": "PurMangRemark", "autoWidth": true, "orderable": false },
                { "data": "StrPurClientPODate", "autoWidth": true, "orderable": false },
                { "data": "StrPurOurPODt", "autoWidth": true, "orderable": false },
                { "data": "StrPurExpectedReceiptDt", "autoWidth": true, "orderable": false },
                { "data": "StrPurCurrentExpDt", "autoWidth": true, "orderable": false },
                { "data": "StrPurActualReceiptDt", "autoWidth": true, "orderable": false },
                { "data": "StrPurTargetDispatchDt", "autoWidth": true, "orderable": false },
                { "data": "PurSZPONo", "autoWidth": true, "orderable": false },
                { "data": "PurPrice", "autoWidth": true, "orderable": false },
            ]
        });
    }
}
