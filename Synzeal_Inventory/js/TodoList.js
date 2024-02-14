var DeletePw = 'SynZealpAssW0rd'
function editLink(cellValue, options, rowdata, action) {
    if (rowdata.ProductId != null && rowdata.ProductId != '0') {
        var
            outputstr = "<a style='float:left'  href='javascript:void(0)' data-toggle='modal' onclick='OpenAddInventoryModel(" + rowdata.ProductId + ")' class='btn btn-success' >Add</a>";
        //"<a style='float:left' href='/Form/CreateInventory/" + rowdata.ProductId + "' class='btn btn-success'>Add</a>  ";

        if (rowdata.BatchNo != null && rowdata.BatchNo != "") {
            outputstr += "<a style='float:left'  href='javascript:void(0)' data-toggle='modal' onclick='OpenEdnitInventoryModel(" + rowdata.InvId + ")' class='btn btn-info' >Edit</a>";
            //outputstr += "<a style='float:left'  href='/Form/EditInventory/" + rowdata.InvId + "' class='ui-icon ui-icon-pencil' ></a>";
            outputstr += "<a style='float:left' onclick='return deleteInventory(" + rowdata.InvId + ")'  href='javascript:void(0)' class='btn btn-danger'>Del.</a>";
        }
        outputstr += "<div class='showimage' style='display:none'><img src='" + rowdata.DefaultPictureModel.ImageUrl + "' /></div> ";
        outputstr += "<div class='showSynonym' style='display:none'>" + rowdata.Synonym + "</div> ";

        return outputstr;
    }
    else {
        return "";
    }
}
function OpenAddInventoryModel(ProductId) {
    var url = '../Form/CreateInventory/' + ProductId;
    // var url = '../Form/Test';
    openModal(url, 'slide-right', 'lg', 'Add Inventory');
}
function OpenEdnitInventoryModel(invId) {
    var url = '../Form/EditInventory/' + invId;
    // var url = '../Form/Test';
    openModal(url, 'slide-right', 'lg', 'Edit Inventory')
}
function deleteInventory(InvId) {
    swal({
        title: 'Are you sure?',
        text: "Enter Password for delete:",
        input: 'password',
        inputPlaceholder: 'Enter your password',
        inputAttributes: {
            'autocapitalize': 'off',
            'autocorrect': 'off'
        },
        showCancelButton: true,
        closeOnConfirm: false,
        inputPlaceholder: "Write password",
        showLoaderOnConfirm: true,
        confirmButtonText: "Yes, delete it!",
        confirmButtonClass: "btn-danger",
    }).then(function (password) {
        if (password) {
            if (password === false) return false;
            if (password != DeletePw) {
                swal.showInputError('Invalid Password!');
                return false;
            }

            setTimeout(function () {
                var url = "../Form/DeleteInventory";
                var formData = JSON.stringify({
                    "id": InvId
                });
                var results = _Ajax(url, formData, 'POST');
                if (results.success) {
                    $("#grid").jqGrid('setGridParam', {
                        postData: {
                            searchString: APIName,
                            searchField: 'MainCatName'
                        }
                    }).trigger('reloadGrid');
                    swal("Deleted!", "Your imaginary inventory has been deleted.", "success");
                } else {
                    swal(results.message)
                }
            }, 1000);
        }
    })
    //swal({
    //    title: "Are you sure?",
    //    text: "Enter Password for delete:",
    //    input: 'password',
    //    inputAttributes: {
    //        'autocapitalize': 'off',
    //        'autocorrect': 'off'
    //    },
    //    showCancelButton: true,
    //    closeOnConfirm: false,
    //    inputPlaceholder: "Write password",
    //    showLoaderOnConfirm: true,
    //    confirmButtonText: "Yes, delete it!",
    //    confirmButtonClass: "btn-danger",
    //}, function (inputValue) {
    //    debugger;
    //    if (inputValue === false) return false;
    //    if (inputValue != DeletePw) {
    //        swal.showInputError('Invalid Password!');
    //        return false;
    //    }

    //    setTimeout(function () {
    //        var url = "../Form/DeleteInventory";
    //        var formData = JSON.stringify({
    //            "id": InvId
    //        });
    //        var results = _Ajax(url, formData, 'POST');
    //        if (results.success) {
    //            $("#grid").jqGrid('setGridParam', {
    //                postData: {
    //                    searchString: APIName,
    //                    searchField: 'MainCatName'
    //                }
    //            }).trigger('reloadGrid');
    //            swal("Deleted!", "Your imaginary inventory has been deleted.", "success");
    //        } else {
    //            swal(results.message)
    //        }
    //    }, 1000);

    //});
}
function numDaysBetween(d1, d2) {
    var diff = Math.abs(d1.getTime() - d2.getTime());
    return diff / (1000 * 60 * 60 * 24);
}
function ConvertJsonDateString(jsonDate) {
    var shortDate = null;
    if (jsonDate) {
        var regex = /-?\d+/;
        var matches = regex.exec(jsonDate);
        var dt = new Date(parseInt(matches[0]));
        var month = dt.getMonth() + 1;
        var monthString = month > 9 ? month : '0' + month;
        var day = dt.getDate();
        var dayString = day > 9 ? day : '0' + day;
        var year = dt.getFullYear();
        //shortDate =  dayString + '/' + monthString + '/' + year;
        shortDate = new Date(year, monthString, dayString);
    }
    return shortDate;
};

function ConvertJsonDateStringNew(jsonDate) {
    var shortDate = null;
    if (jsonDate) {
        var regex = /-?\d+/;
        var matches = regex.exec(jsonDate);
        var dt = new Date(parseInt(matches[0]));
        var month = dt.getMonth();
        var monthString = month > 9 ? month : '0' + month;
        var day = dt.getDate();
        var dayString = day > 9 ? day : '0' + day;
        var year = dt.getFullYear();
        //shortDate =  dayString + '/' + monthString + '/' + year;
        shortDate = new Date(year, monthString, dayString);
    }
    return shortDate;
};
function InvoiceLink(cellValue, options, rowdata, action) {

    if (rowdata.ProjectDetailId != null && rowdata.ProjectDetailId != '0') {
        var outputstr = "<a style='float:left' onclick='openInvoiceEdit(" + rowdata.ProjectDetailId + "," + rowdata.InvoiceId + ")' href='javascript:void(0)' class='btn btn-info' >Edit</a>  ";
        outputstr += "<div class='showimage' style='display:none'><img src='" + rowdata.DefaultPictureModel.ImageUrl + "' /></div> ";
        outputstr += "<div class='showSynonym' style='display:none'>" + rowdata.Synonym + "</div> ";

        return outputstr;
    }
    else {
        return "";
    }
}

function RecentAditionLink(cellValue, options, rowdata, action) {

    if (rowdata.ProductId != null && rowdata.ProductId != '0') {
        var outputstr = "<div class='showimage' style='display:none'><img src='" + rowdata.DefaultPictureModel.ImageUrl + "' /></div> ";
        outputstr += "<div class='showSynonym' style='display:none'>" + rowdata.Synonym + "</div> ";

        return outputstr;
    }
    else {
        return "";
    }
}

function openInvoiceEdit(pdid, id) {
    var url = "../Form/ManageInvoice?pdid=" + pdid + "&id=" + id;
    openModal(url, 'slide-right', 'lg', 'Edit Invoice')

}
function COAPathLink(cellValue, options, rowdata, action) {
    if (rowdata.COAPath != "" && rowdata.COAPath != null) {
        return "<a href='" + rowdata.COAPath.replace("~", "..") + "' download class='ui-icon ui-icon-document' ></a>";
    }
    return "";
}
function StdDataPathLink(cellValue, options, rowdata, action) {
    if (rowdata.StdDataPath != "" && rowdata.StdDataPath != null) {
        return "<a href='" + rowdata.StdDataPath.replace("~", "..") + "' download class='ui-icon ui-icon-document' ></a>";
    }
    return "";
}
function AddDataPathLink(cellValue, options, rowdata, action) {
    if (rowdata.AddDataPath != "" && rowdata.AddDataPath != null) {
        return "<a href='" + rowdata.AddDataPath.replace("~", "..") + "' download class='ui-icon ui-icon-document' ></a>";
    }
    return "";
}

function editPriceLink(cellValue, options, rowdata, action) {
    if (rowdata.ProductId != null && rowdata.ProductId != '0') {
        var invid = rowdata.InvId;
        if (rowdata.InvId == null) {
            invid = 0;
        }


        //var outputstr = "<a style='float:left' href='/Form/ManagePrice/" + rowdata.ProductId + "?InvId=" + invid + "' class='btn btn-info'>Edit</a>  ";
        var outputstr = "<a style='float:left' onClick='EditPricePopup(" + rowdata.ProductId + "," + invid + ")' href='javascript:void(0)' class='btn btn-info'>Edit</a>  ";


        if (invid == null || invid == "" || invid == "undefined" || rowdata.INRPrice != null) {
            invid = 0;
            if (rowdata.PriceId != null && rowdata.PriceId != '0') {
                outputstr += "<a style='float:left' onclick='return DeletePrice(" + rowdata.PriceId + ")'  href='javascript:void(0)' class='btn btn-danger'>Del.</a>";
            }
            outputstr += "<div class='showimage' style='display:none'><img src='" + rowdata.DefaultPictureModel.ImageUrl + "' /></div> ";
            outputstr += "<div class='showSynonym' style='display:none'>" + rowdata.Synonym + "</div> ";
        }
        return outputstr;
    }
    return "";
}
function EditPricePopup(ProductId, InvId) {
    var url = "../Form/ManagePrice/" + ProductId + "?InvId=" + InvId;
    openModal(url, 'slide-right', 'lg', 'Edit Price')

}
function DeletePrice(PriceId) {
    swal({
        title: 'Are you sure?',
        text: "Enter Password for delete:",
        input: 'password',
        inputPlaceholder: 'Enter your password',
        inputAttributes: {
            'autocapitalize': 'off',
            'autocorrect': 'off'
        },
        showCancelButton: true,
        closeOnConfirm: false,
        inputPlaceholder: "Write password",
        showLoaderOnConfirm: true,
        confirmButtonText: "Yes, delete it!",
        confirmButtonClass: "btn-danger",
    }).then(function (password) {
        if (password) {
            if (password === false) return false;
            if (password != DeletePw) {
                swal.showInputError('Invalid Password!');
                return false;
            }

            setTimeout(function () {
                var url = "../Form/DeletePrice";
                var formData = JSON.stringify({
                    "id": PriceId
                });
                var results = _Ajax(url, formData, 'POST');
                if (results.success) {
                    $("#grid").jqGrid('setGridParam', {
                        postData: {
                            searchString: APIName,
                            searchField: 'MainCatName'
                        }
                    }).trigger('reloadGrid');
                    swal("Deleted!", results.message, "success");
                } else {
                    swal(results.message)
                }
            }, 1000);
        }
    })
    //swal({
    //    title: "Are you sure?",
    //    text: "Enter Password for delete:",
    //    type: "input",
    //    showCancelButton: true,
    //    closeOnConfirm: false,
    //    inputPlaceholder: "Write password",
    //    showLoaderOnConfirm: true,
    //    confirmButtonText: "Yes, delete it!",
    //    confirmButtonClass: "btn-danger",
    //}, function (inputValue) {
    //    if (inputValue === false) return false;
    //    if (inputValue != DeletePw) {
    //        swal.showInputError('Invalid Password!');
    //        return false;
    //    }

    //    setTimeout(function () {
    //        var url = "../Form/DeletePrice";
    //        var formData = JSON.stringify({
    //            "id": PriceId
    //        });
    //        var results = _Ajax(url, formData, 'POST');
    //        if (results.success) {
    //            $("#grid").jqGrid('setGridParam', {
    //                postData: {
    //                    searchString: APIName,
    //                    searchField: 'MainCatName'
    //                }
    //            }).trigger('reloadGrid');
    //            swal("Deleted!", results.message, "success");
    //        } else {
    //            swal(results.message)
    //        }
    //    }, 1000);

    //});

}

//Expande Price
function PriceCollapseExpandGrid(actionName) {
    if (actionName == "expand") {
        $("#grid").jqGrid('setGridParam', {
            groupingView: {
                groupField: ['Sku'],
                groupText: ['<b>Catalogue # {0}</b>'],
                groupCollapse: false,
                groupDataSorted: true
            }
        }).trigger('reloadGrid');

        $('#collapsediv').attr('onclick', 'PriceCollapseExpandGrid("collapse")');
        $('#collapsediv').text('Collapse');
    } else {
        $("#grid").jqGrid('setGridParam', {
            groupingView: {
                groupField: ['Sku'],
                groupText: ['<b>Catalogue # {0} </b>'],
                groupCollapse: true,
                groupDataSorted: true
            }
        }).trigger('reloadGrid');
        $('#collapsediv').attr('onclick', 'PriceCollapseExpandGrid("expand")');
        $('#collapsediv').text('Expand');
    }
}


function ProjectDispatchLink(cellValue, options, rowdata, action) {
    if (rowdata.ProductId != null && rowdata.ProductId != '0') {
        var invid = rowdata.InvId;
        if (invid == null || invid == "" || invid == "undefined") {
            invid = 0;
        }
        var outputstr = "";
        if (rowdata.Status == "In Progress") {
            outputstr += "<a onclick='return confirmProjectDispatch(" + rowdata.ProjectDetailId + ")' href='javascript:void(0)' class='btn'>Disp.</a>  ";
        }
        if (rowdata.Status == "Packed") {
            outputstr += "<a  onclick='return confirmProjectInvoice(" + rowdata.ProjectDetailId + ")' href='javascript:void(0)' class='btn'>Invo.</a>  ";
        }

        outputstr += "<div class='showimage' style='display:none'><img src='" + rowdata.DefaultPictureModel.ImageUrl + "' /></div> ";
        outputstr += "<div class='showSynonym' style='display:none'>" + rowdata.Synonym + "</div> ";
        //outputstr += "<div class='showimage' style='display:none'><img src='" + rowdata.DefaultPictureModel.ImageUrl + "' /></div> ";
        //outputstr += "<div class='showSynonym' style='display:none'>" + rowdata.Synonym + "</div> ";

        return outputstr;
    }
    return "";
}

function confirmProjectDispatch(ProjectDetailId) {
    swal({
        title: "Are you sure?",
        text: "",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Yes!",
        closeOnConfirm: false,
        showLoaderOnConfirm: true
    },
        function () {
            setTimeout(function () {
                var url = "../Form/ReadyForDispatch";
                var formData = JSON.stringify({
                    "id": ProjectDetailId
                });
                var results = _Ajax(url, formData, 'POST');
                if (results.success) {
                    ProjectGrid("", false);
                    swal("Good job!", results.msg, "success");
                } else {
                    swal(results.msg)
                }
            }, 1000);

        });
}

function confirmProjectInvoice(ProjectDetailId) {

    swal({
        title: "Are you sure?",
        text: "",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Yes!",
        closeOnConfirm: false,
        showLoaderOnConfirm: true
    },
        function () {
            setTimeout(function () {
                var url = "../Form/ReadyForInvoice";
                var formData = JSON.stringify({
                    "id": ProjectDetailId
                });
                var results = _Ajax(url, formData, 'POST');
                if (results.success) {
                    ProjectGrid("", false);
                    swal("Good job!", results.msg, "success");
                } else {
                    swal(results.msg)
                }
            }, 1000);

        });


}

function ProjectLink(cellValue, options, rowdata, action) {
    if (rowdata.ProjectId != null && rowdata.ProjectId != '0') {
        var invid = rowdata.InvId;
        if (invid == null || invid == "" || invid == "undefined") {
            invid = 0;
        }
        var outputstr = "<a  href='javascript:void(0)' data-toggle='modal' onclick='OpenEditProjectModel(" + rowdata.ProjectId + ")' class='btn btn-info' >Edit</a>";
        //outputstr = "<a style='float:left' href='/Form/ManageProject/" + rowdata.ProjectId + "' class='ui-icon ui-icon-pencil' ></a>  ";

        return outputstr;
    }
    return "";
}

//edit paritial view in project

function OpenEditProjectModel(ProductId) {
    var url = '../Form/ManageProject/' + ProductId;
    // var url = '../Form/Test';
    openModal(url, 'slide-right', 'lg', 'Edit Project')
}

function ProjectDetailsLink(cellValue, options, rowdata, action) {
    if (rowdata.ProjectId != null && rowdata.ProjectId != '0') {
        var invid = rowdata.InvId;
        if (invid == null || invid == "" || invid == "undefined") {
            invid = 0;
        }
        var outputstr = "";
        if (rowdata.Status != "Ready for dispatch") {
            outputstr += "<a href='javascript:void(0)' data-toggle='modal' onclick='OpenDetailProjectModel(" + rowdata.ProjectDetailId + ")' class='btn btn-success' >Det.</a> ";
        }
        return outputstr;
    }
    return "";
}
//Detail page project
function OpenDetailProjectModel(ProjectDetailId) {
    var url = '../Form/ManageProjectDetails/' + ProjectDetailId;
    // var url = '../Form/Test';
    openModal(url, 'slide-right', 'lg', 'Project Detail')
}

function ProjectDeleteLink(cellValue, options, rowdata, action) {
    if (rowdata.ProjectId != null && rowdata.ProjectId != '0') {
        var invid = rowdata.InvId;
        if (invid == null || invid == "" || invid == "undefined") {
            invid = 0;
        }
        var outputstr = "<a href='javascript:void(0)' data-toggle='modal' onclick='return deleteproject(" + rowdata.ProjectId + ", " + rowdata.ProjectDetailId + ")' class='btn btn-danger'>Del.</a>  ";
        return outputstr;
    }
    return "";
}

//delete page on project

function deleteproject(ProjectId, ProjectDetailId) {
    swal({
        title: 'Are you sure?',
        text: "Enter Password for delete:",
        input: 'password',
        inputPlaceholder: 'Enter your password',
        inputAttributes: {
            'autocapitalize': 'off',
            'autocorrect': 'off'
        },
        showCancelButton: true,
        closeOnConfirm: false,
        inputPlaceholder: "Write password",
        showLoaderOnConfirm: true,
        confirmButtonText: "Yes, delete it!",
        confirmButtonClass: "btn-danger",
    }).then(function (password) {
        if (password) {
            if (password === false) return false;
            if (password != DeletePw) {
                swal.showInputError('Invalid Password!');
                return false;
            }

            setTimeout(function () {
                var url = "../Form/DeleteProductDetails?ProjectId=" + ProjectId + "&ProjectDetailId=" + ProjectDetailId;
                var formData = JSON.stringify({
                    "ProjectId": ProjectId,
                    "ProjectDetailId": ProjectDetailId
                });
                var results = _Ajax(url, formData, 'POST');

                if (results.success) {
                    ProjectGrid("", false);
                    closeModal();
                    swal("Deleted!", "Your imaginary poject has been deleted.", "success");
                } else {
                    swal(results.message)
                }
            }, 1000);
        }
    });
    //swal({
    //    title: "Are you sure?",
    //    text: "Enter Password:",
    //    type: "input",
    //    showCancelButton: true,
    //    closeOnConfirm: false,
    //    inputPlaceholder: "Write password",
    //    showLoaderOnConfirm: true,
    //    confirmButtonText: "Yes, delete it!",
    //    confirmButtonClass: "btn-danger",
    //}, function (inputValue) {
    //    if (inputValue === false) return false;
    //    if (inputValue != DeletePw) {
    //        swal.showInputError('Invalid Password!');
    //        return false;
    //    }

    //    setTimeout(function () {
    //        var url = "../Form/DeleteProductDetails?ProjectId=" + ProjectId + "&ProjectDetailId=" + ProjectDetailId;
    //        var formData = JSON.stringify({
    //            "ProjectId": ProjectId,
    //            "ProjectDetailId": ProjectDetailId
    //        });
    //        var results = _Ajax(url, formData, 'POST');

    //        if (results.success) {
    //            ProjectGrid("", false);
    //            closeModal();
    //            swal("Deleted!", "Your imaginary poject has been deleted.", "success");
    //        } else {
    //            swal(results.message)
    //        }
    //    }, 1000);

    //});







    //   swal({
    //       title: "Are you sure?",
    //       text: "Your will not be able to recover this project record!",
    //       type: "warning",
    //       showCancelButton: true,
    //       confirmButtonClass: "btn-danger",
    //       confirmButtonText: "Yes, delete it!",
    //       closeOnConfirm: false,
    //       showLoaderOnConfirm: true
    //   },
    //function () {

    //    setTimeout(function () {
    //        var url = "../Form/DeleteProductDetails?ProjectId=" + ProjectId + "&ProjectDetailId=" + ProjectDetailId;
    //        var formData = JSON.stringify({
    //            "ProjectId": ProjectId,
    //            "ProjectDetailId": ProjectDetailId
    //        });
    //        var results = _Ajax(url, formData, 'POST');

    //        if (results.success) {
    //            ProjectGrid("", false);
    //            closeModal();
    //            swal("Deleted!", "Your imaginary poject has been deleted.", "success");
    //        } else {
    //            swal(results.message)
    //        }
    //    }, 1000);

    //});

}

function ProjectUpdateLink(cellValue, options, rowdata, action) {
    if (rowdata.ProjectId != null && rowdata.ProjectId != '0') {
        var invid = rowdata.InvId;
        if (invid == null || invid == "" || invid == "undefined") {
            invid = 0;
        }
        var outputstr = "<a class='btn btn-warning'  href='javascript:void(0)' data-toggle='modal' onclick='OpenUpdateProjectModel(" + rowdata.ProjectDetailId + ")'>U/D</a> ";
        return outputstr;
    }
    return "";
}

//detail page project
function OpenUpdateProjectModel(ProjectDetailId) {
    var url = '../Form/UpdateProductdetails/' + ProjectDetailId;
    // var url = '../Form/Test';
    openModal(url, 'slide-right', 'lg', 'Update Project')
}
//function onconfirm() {
//    //    confirm("Are you sure?")
//    if (swal("Deleted!", "Your imaginary inventory has been deleted.", "success")) {
//        return true;
//    }
//    return false;
//}
function DispatchLink(cellValue, options, rowdata, action) {
    if (rowdata.ProjectDetailId != null && rowdata.ProjectDetailId != '0') {
        var invid = rowdata.InvId;
        if (invid == null || invid == "" || invid == "undefined") {
            invid = 0;
        }
        var outputstr = "";
        outputstr += "<a style='float:left' onclick='return confirmProjectPackedStatus(" + rowdata.ProjectDetailId + ")' href='javascript:void(0)' class='btn btn-info'>Pack</a>  ";
        outputstr += "<div class='showimage' style='display:none'><img src='" + rowdata.DefaultPictureModel.ImageUrl + "' /></div> ";
        outputstr += "<div class='showSynonym' style='display:none'>" + rowdata.Synonym + "</div> ";
        return outputstr;
    }
    return "";
}


function confirmProjectPackedStatus(ProjectDetailId) {

    swal({
        title: "Are you sure?",
        text: "",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Yes!",
        closeOnConfirm: false,
        showLoaderOnConfirm: true
    },
        function () {
            setTimeout(function () {
                var url = "../Form/PackedStatus";
                var formData = JSON.stringify({
                    "id": ProjectDetailId
                });
                var results = _Ajax(url, formData, 'POST');
                if (results.success) {
                    //DispatchGrid();
                    $("#DispatchGrid").jqGrid('setGridParam', {
                        postData: {
                            searchString: status,
                            searchField: 'Status'
                        }
                    }).trigger('reloadGrid');
                    swal("Good job!", results.msg, "success");
                } else {
                    swal(results.msg)
                }
            }, 1000);

        });

    //swal({
    //    title: "Are you sure?",
    //    text: "Enter Password:",
    //    type: "input",
    //    showCancelButton: true,
    //    closeOnConfirm: false,
    //    inputPlaceholder: "Write password",
    //    showLoaderOnConfirm: true,
    //    confirmButtonText: "Yes!",
    //    confirmButtonClass: "btn-danger",
    //}, function (inputValue) {
    //    if (inputValue === false) return false;
    //    if (inputValue != DeletePw) {
    //        swal.showInputError('Invalid Password!');
    //        return false;
    //    }

    //    setTimeout(function () {
    //        var url = "../Form/PackedStatus";
    //        var formData = JSON.stringify({
    //            "id": ProjectDetailId
    //        });
    //        var results = _Ajax(url, formData, 'POST');
    //        if (results.success) {
    //            //DispatchGrid();
    //            $("#DispatchGrid").jqGrid('setGridParam', {
    //                postData: {
    //                    searchString: status,
    //                    searchField: 'Status'
    //                }
    //            }).trigger('reloadGrid');
    //            swal("Good job!", results.msg, "success");
    //        } else {
    //            swal(results.msg)
    //        }
    //    }, 1000);
    //});
}


function deleteconfirm() {
    if (confirm('Some message')) {
        alert('Thanks for confirming');
    } else {
        alert('Why did you press cancel? You should have confirmed');
    }
}

function InventoryCollapseExpandGrid(actionName) {
    if (actionName == "expand") {
        $("#grid").jqGrid('setGridParam', {
            groupingView: {
                groupField: ['Sku'],
                groupText: ['<b>Catalogue # {0}</b>'],
                groupCollapse: false,
                groupDataSorted: true
            }
        }).trigger('reloadGrid');

        $('#collapsediv').attr('onclick', 'InventoryCollapseExpandGrid("collapse")');
        $('#collapsediv').text('Collapse');
    } else {
        $("#grid").jqGrid('setGridParam', {
            groupingView: {
                groupField: ['Sku'],
                groupText: ['<b>Catalogue # {0} </b>'],
                groupCollapse: true,
                groupDataSorted: true
            }
        }).trigger('reloadGrid');
        $('#collapsediv').attr('onclick', 'InventoryCollapseExpandGrid("expand")');
        $('#collapsediv').text('Expand');
    }
}
var APIName = "";

function searchBacthNoInventory() {
    var value = $('#searchBatchNo').val();
    if (value === "") {
        swal("Please enter batch no.")
        return false;
    }

    $("#grid").jqGrid({
        url: "/Form/GetInventoryList",
        //  url: "/Form/GetInventoryList",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['Id', 'Product Name', 'Catalogue #', 'CAS #', 'MW', 'Batch#', 'Qty', 'Re-Test date', 'COA', 'Std Data', 'Add Data', 'Remarks', 'Action'],
        rowattr: function (cellValue, options, rowdata, action) {
            var classname = "";
            if (options.Qty <= 100) {
                classname = "rowqty ";
            }

            if (options.ReTestDate != null) {
                var retest = ConvertJsonDateString(options.ReTestDate);
                var diff = numDaysBetween(new Date(), retest);

                if (diff <= 120) {
                    classname += " clsretest";
                }
            }
            if (options.ProductId != null && options.ProductId != '0') {
                return { "data-mydata": options.DefaultPictureModel.ImageUrl, 'class': classname };
            }
            return "";
        },
        postData: {
            searchString: value,
            searchField: 'BatchNo'
        },
        gridComplete: function () {
            $('.classSKU').mouseover(function () {
                $(this).closest('tr').find('.showimage').show();
            });
            $('.classSKU').mouseout(function () {
                $(this).closest('tr').find('.showimage').hide();
            });
            $('.className').mouseover(function () {
                $(this).closest('tr').find('.showSynonym').show();
            });
            $('.className').mouseout(function () {
                $(this).closest('tr').find('.showSynonym').hide();
            });
        },
        rownumbers: true,
        colModel: [
            { key: true, hidden: true, name: 'ProductId', index: 'ProductId', editable: true, resizable: false, },
            //{ key: false, name: 'MainCatName', index: 'MainCatName', width: 110, editable: true, searchoptions: { sopt: apiname }, resizable: false, },
            { key: false, name: 'Name', index: 'Name', editable: true, classes: 'className', width: 82 },
            { key: false, name: 'Sku', index: 'Sku', width: 38, editable: true, search: false, resizable: false, classes: 'classSKU', align: 'center' },
            { key: false, name: 'ManufacturerPartNumber', width: 47, index: 'CAS No', editable: true, search: false, resizable: false, align: 'center' },
            { key: false, name: 'MolecularWeight', index: 'MolecularWeight', width: 26, editable: true, search: false, resizable: false, align: 'center' },
            { key: false, name: 'BatchNo', index: 'BatchNo', width: 50, editable: true, search: false, resizable: false, align: 'center' },
            { key: false, name: 'Qty', index: 'Qty', width: 20, classes: 'redrowqty', editable: true, search: false, resizable: false, align: 'center' },
            { key: false, name: 'ReTestDate', index: 'ReTestDate', classes: 'retestlarge', editable: true, search: false, resizable: false, width: 40, formatter: "date", align: 'center' },
            { name: 'COAPath', search: false, index: 'COAPath', width: 10, sortable: false, formatter: COAPathLink, resizable: false, align: 'center' },
            { name: 'StdDataPath', search: false, index: 'StdDataPath', width: 10, sortable: false, formatter: StdDataPathLink, resizable: false, align: 'center' },
            { name: 'AddDataPath', search: false, index: 'AddDataPath', width: 10, sortable: false, formatter: AddDataPathLink, resizable: false, align: 'center' },
            { key: false, name: 'Remarks', index: 'Remarks', width: 30, editable: true, search: false, resizable: false },
            { name: 'ProductId', search: false, index: 'ProductId', width: 68, sortable: false, formatter: editLink, resizable: false },
        ],
        pager: jQuery('#pager'),
        rowNum: 20,
        rowList: [20, 50, 100],
        height: '110%',
        viewrecords: true,
        caption: 'Inventory List',
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        grouping: true,
        groupingView: {
            groupField: ['Sku'],
            groupText: ['<b>Catalogue # {0}<b>'],
            groupCollapse: true,
            groupDataSorted: true
        },

        shrinkToFit: true,
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: false, add: false, del: false, search: true, refresh: true },
        {
            // edit options
            zIndex: 100,
            url: '/TodoList/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // add options
            zIndex: 100,
            url: "/TodoList/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // delete options
            zIndex: 100,
            url: "/TodoList/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this task?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        }
        );

    $("#grid").jqGrid('setGridParam', {
        postData: {
            searchString: value,
            searchField: 'BatchNo'
        }
    }).trigger('reloadGrid');
}

function InventoryGrid(apiname) {
    //$('#grid').jqGrid("clearGridData");
    //$('#grid').jqGrid('GridDestroy');
    //$('#grid').remove();
    //$('#gridContainer').empty();
    // $("#grid").trigger('reloadGrid', [{ searchString: apiname, searchField: 'MainCatName' }]);


    APIName = apiname;
    $("#grid").jqGrid({
        url: "/Form/GetInventoryList",
        //  url: "/Form/GetInventoryList",
        datatype: 'json',
        mtype: 'Get',

        colNames: ['Id', 'Product Name', 'Catalogue #', 'CAS #', 'MW', 'Batch#', 'Qty', 'Re-Test date', 'COA', 'Std Data', 'Add Data', 'Remarks', 'Action'],
        rowattr: function (cellValue, options, rowdata, action) {
            var classname = "";
            if (options.Qty <= 100) {
                classname = "rowqty ";
            }

            if (options.ReTestDate != null) {
                var retest = ConvertJsonDateString(options.ReTestDate);
                var diff = numDaysBetween(new Date(), retest);

                if (diff <= 120) {
                    classname += " clsretest";
                }
            }
            if (options.ProductId != null && options.ProductId != '0') {
                return { "data-mydata": options.DefaultPictureModel.ImageUrl, 'class': classname };
            }
            return "";
        },
        postData: {
            searchString: apiname,
            searchField: 'MainCatName'
        },
        gridComplete: function () {
            $('.classSKU').mouseover(function () {
                $(this).closest('tr').find('.showimage').show();
            });
            $('.classSKU').mouseout(function () {
                $(this).closest('tr').find('.showimage').hide();
            });
            $('.className').mouseover(function () {
                $(this).closest('tr').find('.showSynonym').show();
            });
            $('.className').mouseout(function () {
                $(this).closest('tr').find('.showSynonym').hide();
            });
        },
        rownumbers: true,
        colModel: [
            { key: true, hidden: true, name: 'ProductId', index: 'ProductId', editable: true, resizable: false, },
            //{ key: false, name: 'MainCatName', index: 'MainCatName', width: 110, editable: true, searchoptions: { sopt: apiname }, resizable: false, },
            { key: false, name: 'Name', index: 'Name', editable: true, classes: 'className', width: 82 },
            { key: false, name: 'Sku', index: 'Sku', width: 38, editable: true, search: false, resizable: false, classes: 'classSKU', align: 'center' },
            { key: false, name: 'ManufacturerPartNumber', width: 47, index: 'CAS No', editable: true, search: false, resizable: false, align: 'center' },
            { key: false, name: 'MolecularWeight', index: 'MolecularWeight', width: 26, editable: true, search: false, resizable: false, align: 'center' },
            { key: false, name: 'BatchNo', index: 'BatchNo', width: 50, editable: true, search: false, resizable: false, align: 'center' },
            { key: false, name: 'Qty', index: 'Qty', width: 20, classes: 'redrowqty', editable: true, search: false, resizable: false, align: 'center' },
            { key: false, name: 'ReTestDate', index: 'ReTestDate', classes: 'retestlarge', editable: true, search: false, resizable: false, width: 40, formatter: "date", align: 'center' },
            { name: 'COAPath', search: false, index: 'COAPath', width: 10, sortable: false, formatter: COAPathLink, resizable: false, align: 'center' },
            { name: 'StdDataPath', search: false, index: 'StdDataPath', width: 10, sortable: false, formatter: StdDataPathLink, resizable: false, align: 'center' },
            { name: 'AddDataPath', search: false, index: 'AddDataPath', width: 10, sortable: false, formatter: AddDataPathLink, resizable: false, align: 'center' },
            { key: false, name: 'Remarks', index: 'Remarks', width: 30, editable: true, search: false, resizable: false },
            { name: 'ProductId', search: false, index: 'ProductId', width: 68, sortable: false, formatter: editLink, resizable: false },
        ],
        pager: jQuery('#pager'),
        rowNum: 20,
        rowList: [20, 50, 100],
        height: '110%',
        viewrecords: true,
        caption: 'Inventory List',
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        grouping: true,
        groupingView: {
            groupField: ['Sku'],
            groupText: ['<b>Catalogue # {0}<b>'],
            groupCollapse: true,
            groupDataSorted: true
        },

        shrinkToFit: true,
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: false, add: false, del: false, search: true, refresh: true },
        {
            // edit options
            zIndex: 100,
            url: '/TodoList/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // add options
            zIndex: 100,
            url: "/TodoList/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // delete options
            zIndex: 100,
            url: "/TodoList/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this task?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        }
        );

    $("#grid").jqGrid('setGridParam', {
        postData: {
            searchString: apiname,
            searchField: 'MainCatName'
        }
    }).trigger('reloadGrid');



}

var APIName = "";
function PriceGrid(apiname) {
    APIName = apiname;
    $("#grid").jqGrid({
        url: "/Form/GetPriceList",
        datatype: 'json',
        mtype: 'Get',
        rownumbers: true,
        colNames: ['Id', 'Product Name', 'Catalogue #', 'Qty', 'Category', 'INR Price', 'Revised Price', 'Lead Time', 'Action'],
        rowattr: function (cellValue, options, rowdata, action) {
            if (options.ProductId != null && options.ProductId != '0') {
                return { "data-mydata": options.DefaultPictureModel.ImageUrl };
            }
            return "";
        },
        postData: {
            searchString: APIName,
            searchField: 'MainCatName'
        },
        gridComplete: function () {
            $('.classSKU').mouseover(function () {
                $(this).closest('tr').find('.showimage').show();
            });
            $('.classSKU').mouseout(function () {
                $(this).closest('tr').find('.showimage').hide();
            });
            $('.className').mouseover(function () {
                $(this).closest('tr').find('.showSynonym').show();
            });
            $('.className').mouseout(function () {
                $(this).closest('tr').find('.showSynonym').hide();
            });
        },
        colModel: [
            { key: true, hidden: true, name: 'ProductId', index: 'ProductId', editable: true },
            //{ key: false, name: 'MainCatName', index: 'MainCatName', editable: true ,resizable: false, },
            { key: false, name: 'Name', index: 'Name', width: 90, editable: true, classes: 'className' },
            { key: false, name: 'Sku', index: 'Sku', width: 55, editable: true, search: false, resizable: false, classes: 'classSKU', align: 'center' },
            // { key: false, name: 'ManufacturerPartNumber', index: 'CAS No', editable: true, search: true, width: 50, resizable: false, align: 'center' },
            //{ key: false, name: 'MolecularWeight', index: 'MolecularWeight', editable: true, search: false, width: 35 },
            // { key: false, name: 'BatchNo', index: 'BatchNo', editable: true, search: false, width: 40, resizable: false, align: 'center' },
            { key: false, name: 'Qty', index: 'Qty', width: 40, editable: true, search: false, resizable: false, align: 'center' },
            //{ key: false, name: 'Appearance', index: 'Appearance', editable: true, search: false, width: 90 },
            //{ name: 'COAPath', search: false, index: 'COAPath', width: 30, sortable: false, formatter: COAPathLink },
            //{ name: 'StdDataPath', search: false, index: 'StdDataPath', width: 30, sortable: false, formatter: StdDataPathLink },
            //{ name: 'AddDataPath', search: false, index: 'AddDataPath', width: 30, sortable: false, formatter: AddDataPathLink },
            //{ key: false, name: 'Price', index: 'Price', editable: true, search: false, width: 45, resizable: false, align: 'center' },

            { key: false, name: 'Category', index: 'Category', editable: true, search: false, width: 35, resizable: false, align: 'center' },
            { key: false, name: 'INRPrice', index: 'INRPrice', editable: true, search: false, width: 70, resizable: false }, // cellattr: function (rowId, tv, rawObject, cm, rdata) { return 'style="white-space: normal;"' }
            { key: false, name: 'USDPrice', index: 'USDPrice', editable: true, search: false, width: 75, resizable: false },
            //{ key: false, name: 'MaxDiscount', index: 'MaxDiscount', editable: true, search: false, width: 45, resizable: false },
            { key: false, name: 'LeadTime', index: 'LeadTime', editable: true, search: false, width: 45, resizable: false, align: 'center' },

            //{ key: false, name: 'Remarks', index: 'Remarks', editable: true, search: false, width: 80, resizable: false },
            { name: 'ProductId', search: false, index: 'ProductId', width: 68, sortable: false, formatter: editPriceLink, resizable: false },
        ],
        pager: jQuery('#pager'),
        rowNum: 20,
        rowList: [20, 50, 100],
        height: '100%',
        viewrecords: true,
        caption: 'Price List',
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        grouping: true,
        groupingView: {
            groupField: ['Sku'],
            groupText: ['<b>Catalogue # {0}<b>'],
            groupCollapse: true,
            groupDataSorted: true
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: false, add: false, del: false, search: true, refresh: true },
        {
            // edit options
            zIndex: 100,
            url: '/TodoList/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // add options
            zIndex: 100,
            url: "/TodoList/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // delete options
            zIndex: 100,
            url: "/TodoList/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this task?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        }
        );
    $("#grid").jqGrid('setGridParam', {
        postData: {
            searchString: APIName,
            searchField: 'MainCatName'
        }
    }).trigger('reloadGrid');
}


function ProjectCollapseExpandGrid(actionName) {

    if (actionName == "expand") {
        //$("#projectgrid").jqGrid('setGridParam', {
        //    groupingView: {
        //        groupField: ['Company', 'PONo'],
        //        groupColumnShow: [true, true],
        //        groupCollapse: false,
        //        groupDataSorted: true
        //    }
        //}).trigger('reloadGrid');
        $('#projectgrid').jqGrid('GridUnload');
        ProjectGrid(APIName, false);

        $('#collapsediv').attr('onclick', 'ProjectCollapseExpandGrid("collapse")');
        $('#collapsediv').text('Collapse');
    } else {
        //$("#projectgrid").jqGrid('setGridParam', {
        //    groupingView: {
        //        groupField: ['Company', 'PONo'],
        //        groupColumnShow: [true, true],
        //        groupCollapse: true,
        //        groupDataSorted: true
        //    }
        //}).trigger('reloadGrid');
        $('#projectgrid').jqGrid('GridUnload');
        ProjectGrid(APIName, true);
        $('#collapsediv').attr('onclick', 'ProjectCollapseExpandGrid("expand")');
        $('#collapsediv').text('Expand');
    }
}


function DispatchCollapseExpandGrid(actionName) {
    if (actionName == "expand") {
        $("#DispatchGrid").jqGrid('setGridParam', {
            groupingView: {
                groupField: ['PONo'],
                groupColumnShow: [true, true],
                groupCollapse: false,
                groupDataSorted: true
            }
        }).trigger('reloadGrid');

        $('#collapsediv').attr('onclick', 'DispatchCollapseExpandGrid("collapse")');
        $('#collapsediv').text('Collapse');
    } else {
        $("#DispatchGrid").jqGrid('setGridParam', {
            groupingView: {
                groupField: ['PONo'],
                groupColumnShow: [true, true],
                groupCollapse: true,
                groupDataSorted: true
            }
        }).trigger('reloadGrid');
        $('#collapsediv').attr('onclick', 'DispatchCollapseExpandGrid("expand")');
        $('#collapsediv').text('Expand');
    }
}

var countProjectLoad = 0;
function ProjectGrid(status, collapse) {
    $('#divLoading').css('display', 'block');
    var cc = collapse;
    //var expand = true;
    APIName = status;
    countProjectLoad += 1;
    var mydata = [];
    $.ajax({
        url: "/Form/GetProjectListTest?status=" + status,
        type: 'GET',
        dataType: 'json',
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (data) {
            mydata = data.rows;

            if (status != "" || countProjectLoad != 0) {
                jQuery('#projectgrid').jqGrid('clearGridData');
                jQuery('#projectgrid').jqGrid('setGridParam', { data: mydata });
                jQuery('#projectgrid').trigger('reloadGrid');
            }

            $("#projectgrid").jqGrid({
                data: mydata,
                datatype: "local",
                loadComplete: function (data) {
                    var grid = jQuery("#projectgrid"),
                        pageSize = parseInt(grid.jqGrid("getGridParam", "rowNum")),
                        //emptyRows = pageSize - data.rows.length;
                        emptyRows = 6;

                    if (emptyRows > 0) {
                        for (var i = 1; i <= emptyRows; i++) {
                            // Send rowId as undefined to force jqGrid to generate random rowId
                            grid.jqGrid('addRowData', undefined, {});
                        }
                        // adjust the counts at lower right
                        grid.jqGrid("setGridParam", {
                            reccount: grid.jqGrid("getGridParam", "reccount") - emptyRows,
                            records: grid.jqGrid("getGridParam", "records") - emptyRows
                        });
                        grid[0].updatepager();

                        for (var i = 1; i <= emptyRows; i++) {
                            $('#jqg' + i).find('td').html('');
                        }

                    }
                },
                sortable: false,                gridComplete: function () {
                    $('.classSKU').mouseover(function () {
                        $(this).closest('tr').find('.showimage').show();
                    });
                    $('.classSKU').mouseout(function () {
                        $(this).closest('tr').find('.showimage').hide();
                    });
                    $('.className').mouseover(function () {
                        $(this).closest('tr').find('.showSynonym').show();
                    });
                    $('.className').mouseout(function () {
                        $(this).closest('tr').find('.showSynonym').hide();
                    });
                },
                rowattr: function (cellValue, options, rowdata, action) {
                    if (options.Status == "Ready for dispatch") {
                        return { "style": "background:#c6e0b4" };
                    }
                    if (options.Status == "Packed") {
                        return { "style": "background:#ddebf7" };
                    }

                    return "";
                },
                grouping: true,
                groupingView: {
                    groupField: ['Company', 'PONo'],
                    groupColumnShow: [true, true],
                    groupCollapse: cc,
                    groupDataSorted: true,
                    groupOrder: ["asc"], groupText: ['<b>Company Name : {0}</b>', '<b>PO # {0}</b>'],

                },
                rownumbers: true,
                loadonce: true,
                colNames: ['Id', 'PO#', 'Company', 'Schedule Dispatch', 'Product Name', 'Label Requirements', 'Catalogue #', 'Batch#', 'Scientist', 'Total Qty', 'Product Status', 'Project Update', '', '', '', '', ''],
                colModel: [
                    { key: true, hidden: true, name: 'ProductId', index: 'ProductId', editable: true, sortable: false },
                    { key: false, name: 'PONo', index: 'PONo', editable: true, resizable: false, width: 60, sortable: false, align: 'center' },
                    { key: false, name: 'Company', index: 'Company', editable: true, width: 50, resizable: false, align: 'center', sortable: false },
                    { key: false, name: 'ScheduleDispatch', index: 'ScheduleDispatch', editable: true, resizable: false, search: false, formatter: "date", width: 65, align: 'center', sortable: false },
                    { key: false, name: 'Name', index: 'Name', editable: true, classes: 'className', search: false, sortable: false },
                    { key: false, name: 'LabelRequirement', index: 'LabelRequirement', editable: true, width: 75, search: false, sortable: false, align: 'center' },
                    { key: false, name: 'Sku', index: 'Sku', width: 65, editable: true, resizable: false, classes: 'classSKU', align: 'center', sortable: false },
                    //{ key: false, name: 'ManufacturerPartNumber', index: 'CAS No', editable: true, search: false, width: 70, resizable: false },
                    //{ key: false, name: 'MolecularWeight', index: 'MolecularWeight', editable: true, search: false, width: 35 },
                    { key: false, name: 'BatchNo', index: 'BatchNo', editable: true, width: 65, resizable: false, align: 'center', sortable: false },

                    { key: false, name: 'Scientist', index: 'Scientist', editable: true, width: 40, resizable: false, align: 'center', sortable: false },
                    { key: false, name: 'Qty', index: 'Qty', width: 20, editable: true, search: false, resizable: false, align: 'center', sortable: false },
                    { key: false, name: 'Status', index: 'Status', editable: true, search: false, width: 70, resizable: false, sortable: false, align: 'center' },
                    { key: false, name: 'Remarks', index: 'Remarks', width: 50, editable: true, search: false, resizable: false, sortable: false, align: 'center' },
                    { name: 'ProductId', search: false, index: 'ProductId', width: 40, sortable: false, formatter: ProjectLink, resizable: false, align: 'center' },
                    { name: 'ProductId', search: false, index: 'ProductId', width: 40, sortable: false, formatter: ProjectDetailsLink, resizable: false, align: 'center' },
                    { name: 'ProductId', search: false, index: 'ProductId', width: 40, sortable: false, formatter: ProjectUpdateLink, resizable: false, align: 'center' },
                    { name: 'ProductId', search: false, index: 'ProductId', width: 40, sortable: false, formatter: ProjectDeleteLink, resizable: false, align: 'center' },
                    { name: 'ProductId', search: false, index: 'ProductId', width: 50, sortable: false, formatter: ProjectDispatchLink, resizable: false, align: 'center' },
                ],
                pager: jQuery('#pager'),
                rowNum: 20,
                rowList: [20, 50, 100],
                height: '100%',
                viewrecords: true,
                caption: 'Project List',
                emptyrecords: 'No records to display',
                jsonReader: {
                    root: "rows",
                    page: "page",
                    total: "total",
                    records: "records",
                    repeatitems: true,
                    Id: "0"
                },
                autowidth: true,
                multiselect: false
            }).navGrid('#pager', { edit: false, add: false, del: false, search: true, refresh: true }
                );

            $('#divLoading').css('display', 'none');
            //jQuery('#projectgrid').jqGrid('setGridParam', { data: mydata });
            //jQuery('#projectgrid').trigger('reloadGrid');


        },
        error: function () {
            $('#divLoading').css('display', 'none');
            alert('Something went wrong');
        }

    })

    //$("#projectgrid").jqGrid({
    //    url: "/Form/GetProjectList",
    //    datatype: 'json',
    //    mtype: 'Get',
    //    postData: {
    //        searchString: status,
    //        searchField: 'Status'
    //    },
    //    gridComplete: function () {
    //        $('.classSKU').mouseover(function () {
    //            $(this).closest('tr').find('.showimage').show();
    //        });
    //        $('.classSKU').mouseout(function () {
    //            $(this).closest('tr').find('.showimage').hide();
    //        });
    //        $('.className').mouseover(function () {
    //            $(this).closest('tr').find('.showSynonym').show();
    //        });
    //        $('.className').mouseout(function () {
    //            $(this).closest('tr').find('.showSynonym').hide();
    //        });
    //    },
    //    rowattr: function (cellValue, options, rowdata, action) {
    //        if (options.Status == "Ready for dispatch") {
    //            return { "style": "background:#c6e0b4" };
    //        }
    //        if (options.Status == "Packed") {
    //            return { "style": "background:#ddebf7" };
    //        }

    //        return "";
    //    },
    //    grouping: true,
    //    groupingView: {
    //        groupField: ['Company', 'PONo'],
    //        groupColumnShow: [true, true],
    //        groupCollapse: true,
    //        groupDataSorted: true
    //    },
    //    rownumbers: true,
    //    colNames: ['Id', 'PO#', 'Company', 'Schedule Dispatch', 'Product Name', 'Label Requirements', 'Catalogue #', 'Batch#', 'Scientist', 'Total Qty', 'Product Status', 'Project Update', '', '','', '', ''],
    //    colModel: [
    //        { key: true, hidden: true, name: 'ProductId', index: 'ProductId', editable: true },
    //        { key: false, name: 'PONo', index: 'PONo', editable: true, resizable: false, width: 60 },
    //        { key: false, name: 'Company', index: 'Company', editable: true, width: 50, resizable: false, align: 'center'},
    //        { key: false, name: 'ScheduleDispatch', index: 'ScheduleDispatch', editable: true, resizable: false, search: false, formatter: "date", width: 65, align: 'center' },
    //         { key: false, name: 'Name', index: 'Name', editable: true, classes: 'className', search: false },
    //         { key: false, name: 'LabelRequirement', index: 'LabelRequirement', editable: true, width: 75, search: false, },
    //        { key: false, name: 'Sku', index: 'Sku', width: 65, editable: true, resizable: false, classes: 'classSKU', align: 'center' },
    //       //{ key: false, name: 'ManufacturerPartNumber', index: 'CAS No', editable: true, search: false, width: 70, resizable: false },
    //        //{ key: false, name: 'MolecularWeight', index: 'MolecularWeight', editable: true, search: false, width: 35 },
    //        { key: false, name: 'BatchNo', index: 'BatchNo', editable: true, width: 65, resizable: false, align: 'center' },

    //        { key: false, name: 'Scientist', index: 'Scientist', editable: true, width: 40, resizable: false, align: 'center' },
    //        { key: false, name: 'Qty', index: 'Qty', width: 20, editable: true, search: false, resizable: false, align: 'center' },
    //        { key: false, name: 'Status', index: 'Status', editable: true, search: false, width: 70, resizable: false },
    //        { key: false, name: 'Remarks', index: 'Remarks', width: 70, editable: true, search: false, resizable: false },
    //        { name: 'ProductId', search: false, index: 'ProductId', width: 10, sortable: false, formatter: ProjectLink, resizable: false },
    //        { name: 'ProductId', search: false, index: 'ProductId', width: 10, sortable: false, formatter: ProjectDetailsLink, resizable: false },
    //        { name: 'ProductId', search: false, index: 'ProductId', width: 10, sortable: false, formatter: ProjectUpdateLink, resizable: false },
    //        { name: 'ProductId', search: false, index: 'ProductId', width: 10, sortable: false, formatter: ProjectDeleteLink, resizable: false },
    //        { name: 'ProductId', search: false, index: 'ProductId', width: 30, sortable: false, formatter: ProjectDispatchLink, resizable: false },
    //    ],
    //    pager: jQuery('#pager'),
    //    rowNum: 30,
    //    rowList: [10, 20, 30, 40],
    //    height: '100%',
    //    viewrecords: true,
    //    caption: 'Project List',
    //    emptyrecords: 'No records to display',
    //    jsonReader: {
    //        root: "rows",
    //        page: "page",
    //        total: "total",
    //        records: "records",
    //        repeatitems: false,
    //        Id: "0"
    //    },
    //    autowidth: true,
    //    multiselect: false
    //}).navGrid('#pager', { edit: false, add: false, del: false, search: true, refresh: true }
    //   );

    //setTimeout(function() {
    //    $("#projectgrid").jqGrid('setGridParam', {
    //        grouping: true,
    //        groupingView: {
    //            groupField: ['Company', 'PONo'],
    //            groupColumnShow: [true, true],
    //            groupCollapse: true,
    //            groupDataSorted: true
    //        }
    //    }).trigger('reloadGrid');
    //}, 5000);


    //$("#projectgrid").jqGrid('setGridParam', {
    //    postData: {
    //        searchString: status,
    //        searchField: 'Status'
    //    }
    //}).trigger('reloadGrid');
}

function DispatchGrid() {
    $("#DispatchGrid").jqGrid({
        url: "/Form/GetDispatchList",
        datatype: 'json',
        mtype: 'Get',
        postData: {
            searchString: status,
            searchField: 'Status'
        },
        gridComplete: function () {
            $('.classSKU').mouseover(function () {
                $(this).closest('tr').find('.showimage').show();
            });
            $('.classSKU').mouseout(function () {
                $(this).closest('tr').find('.showimage').hide();
            });
            $('.className').mouseover(function () {
                $(this).closest('tr').find('.showSynonym').show();
            });
            $('.className').mouseout(function () {
                $(this).closest('tr').find('.showSynonym').hide();
            });
        },
        rowattr: function (cellValue, options, rowdata, action) {
            //if (options.Status == "Ready for dispatch") {
            //    return { "style": "background:#c6e0b4" };
            //}
            //if (options.Status == "Packed") {
            //    return { "style": "background:rgb(198, 224, 180)" };
            //}

            return "";
        },
        rownumbers: true,
        //colNames: ['Id', 'PO#', 'Schedule Dispatch', 'Product Name', 'Label Requirements', 'Catalogue #', 'Batch#', 'Company', 'Scientist', 'Pack Size', 'No of Pack', 'Total Qty', 'Data Required', 'Remarks', ''],
        colNames: ['Id', 'PO#', 'Schedule Dispatch', 'Product Name', 'Label Requirements', 'Catalogue #', 'Batch#', 'Scientist', 'Pack Size', 'No of Pack', 'Total Qty', 'Data Required', 'Remarks', ''],
        colModel: [
            { key: true, hidden: true, name: 'ProductId', index: 'ProductId', editable: true },
            { key: false, name: 'PONo', index: 'PONo', editable: true, resizable: false, width: 45, align: 'center' },
            { key: false, name: 'ScheduleDispatch', index: 'ScheduleDispatch', editable: true, resizable: false, search: false, formatter: "date", width: 45, align: 'center' },
            { key: false, name: 'Name', index: 'Name', editable: true, classes: 'className', width: 95, search: false, },
            { key: false, name: 'LabelRequirement', index: 'LabelRequirement', editable: true, width: 200, search: false, align: 'center' },
            { key: false, name: 'Sku', index: 'Sku', width: 45, editable: true, resizable: false, classes: 'classSKU', align: 'center' },
            //{ key: false, name: 'ManufacturerPartNumber', index: 'CAS No', editable: true, search: false, width: 70, resizable: false },
            //{ key: false, name: 'MolecularWeight', index: 'MolecularWeight', editable: true, search: false, width: 35 },
            { key: false, name: 'BatchNo', index: 'BatchNo', editable: true, width: 45, resizable: false, align: 'center' },
            //{ key: false, name: 'Company', index: 'Company', editable: true, width: 40, resizable: false },

            { key: false, name: 'Scientist', index: 'Scientist', editable: true, width: 40, resizable: false, align: 'center' },

            { key: false, name: 'PackSize', index: 'PackSize', width: 20, editable: true, search: false, resizable: false, align: 'center' },
            { key: false, name: 'NoOfPack', index: 'NoOfPack', width: 20, editable: true, search: false, resizable: false, align: 'center' },

            { key: false, name: 'Qty', index: 'Qty', width: 20, editable: true, search: false, resizable: false, align: 'center' },
            { key: false, name: 'DataRequired', index: 'DataRequired', editable: true, search: false, width: 70, resizable: false, align: 'center' },
            { key: false, name: 'Remarks', index: 'Remarks', width: 70, editable: true, search: false, resizable: false, align: 'center' },
            { name: 'ProductId', search: false, index: 'ProductId', width: 30, sortable: false, formatter: DispatchLink, resizable: false, align: 'center' },
        ],
        pager: jQuery('#pager'),
        rowNum: 20,
        rowList: [20, 50, 100],
        height: '100%',
        viewrecords: true,
        caption: 'Dispatch List',
        emptyrecords: 'No records to display',
        grouping: true,
        groupingView: {
            groupField: ['PONo'],
            groupColumnShow: [true, true],
            groupText: ['<b>PO # {0}</b>'], groupCollapse: true,

        },
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: false, add: false, del: false, search: true, refresh: true },
        {
            // edit options
            zIndex: 100,
            url: '/TodoList/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // add options
            zIndex: 100,
            url: "/TodoList/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // delete options
            zIndex: 100,
            url: "/TodoList/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this task?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        }
        );
    $("#projectgrid").jqGrid('setGridParam', {
        postData: {
            searchString: status,
            searchField: 'Status'
        }
    }).trigger('reloadGrid');
}


function InvoiceGrid(status) {
    $("#projectgrid").jqGrid({
        url: "/Form/GetInvoiceList",
        datatype: 'json',
        mtype: 'Get',
        postData: {
            searchString: status,
            searchField: 'Status'
        },
        gridComplete: function () {
            $('.classSKU').mouseover(function () {
                $(this).closest('tr').find('.showimage').show();
            });
            $('.classSKU').mouseout(function () {
                $(this).closest('tr').find('.showimage').hide();
            });
            $('.className').mouseover(function () {
                $(this).closest('tr').find('.showSynonym').show();
            });
            $('.className').mouseout(function () {
                $(this).closest('tr').find('.showSynonym').hide();
            });
        },
        rowattr: function (cellValue, options, rowdata, action) {
            if (options.DueDate != null) {
                var dueDate = ConvertJsonDateStringNew(options.DueDate);
                var todayDate = new Date();
                var diff = numDaysBetween(todayDate, dueDate);

                if (todayDate > dueDate && diff >= 30 && options.PaymentStatus != "Received") {
                    return { "style": "background:red" };
                }
            }
            //if (options.Status == "Ready for dispatch") {
            //    return { "style": "background:#c6e0b4" };
            //}
            //if (options.Status == "Packed") {
            //    return { "style": "background:#ddebf7" };
            //}

            //return "";
        },
        rownumbers: true,
        colNames: ['Id', 'PO Date', 'PO#', 'Dispatch Dt', 'Company', 'Product Name', 'Label Requirements', 'Catalogue #', 'Pack Size', 'No of Pack', 'Total Qty',
            'Basic Value', 'Invoice#', ''],
        colModel: [
            { key: true, hidden: true, name: 'ProductId', index: 'ProductId', editable: true },

            { key: false, name: 'PODate', index: 'PODate', editable: true, resizable: false, width: 60, formatter: "date" },

            { key: false, name: 'PONo', index: 'PONo', editable: true, resizable: false, width: 60 },
            { key: false, name: 'DispatchDate', index: 'DispatchDate', editable: true, resizable: false, width: 60, formatter: "date", },
            { key: false, name: 'Company', index: 'Company', editable: true, width: 50, resizable: false, align: 'center' },
            { key: false, name: 'Name', index: 'Name', editable: true, classes: 'className', search: false },
            { key: false, name: 'LabelRequirement', index: 'LabelRequirement', editable: true, width: 75, search: false, },
            { key: false, name: 'Sku', index: 'Sku', width: 65, editable: true, resizable: false, classes: 'classSKU', align: 'center' },
            { key: false, name: 'PackSize', index: 'PackSize', editable: true, width: 65, resizable: false, align: 'center' },
            { key: false, name: 'NoOfPack', index: 'NoOfPack', editable: true, width: 40, resizable: false, align: 'center' },
            { key: false, name: 'Qty', index: 'Qty', width: 20, editable: true, search: false, resizable: false, align: 'center' },
            { key: false, name: 'BasicValue', index: 'BasicValue', editable: true, search: false, width: 70, resizable: false },
            { key: false, name: 'InvoiceNo', index: 'InvoiceNo', width: 70, editable: true, search: false, resizable: false },
            { name: 'ProjectDetailId', search: false, index: 'ProjectDetailId', width: 30, sortable: false, formatter: InvoiceLink, resizable: false },
        ],
        pager: jQuery('#pager'),
        rowNum: 20,
        rowList: [20, 50, 100],
        height: '100%',
        viewrecords: true,
        caption: 'Invoice List',
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: false, add: false, del: false, search: true, refresh: true },
        {
            // edit options
            zIndex: 100,
            url: '/TodoList/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // add options
            zIndex: 100,
            url: "/TodoList/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // delete options
            zIndex: 100,
            url: "/TodoList/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this task?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        }
        );
    $("#projectgrid").jqGrid('setGridParam', {
        postData: {
            searchString: status,
            searchField: 'Status'
        }
    }).trigger('reloadGrid');
}


function RecentAditionGrid() {
    //$('#grid').jqGrid("clearGridData");
    //$('#grid').jqGrid('GridDestroy');
    //$('#grid').remove();
    //$('#gridContainer').empty();
    // $("#grid").trigger('reloadGrid', [{ searchString: apiname, searchField: 'MainCatName' }]);


    $("#grid").jqGrid({
        url: "/Form/RecentAdditionList",
        //  url: "/Form/GetInventoryList",
        datatype: 'json',
        mtype: 'Get',

        colNames: ['Id', 'Product Name', 'Catalogue #', 'CAS #', 'MW', 'Batch#', 'Qty', 'Re-Test date', 'COA', 'Std Data', 'Add Data', 'Remarks'],
        rowattr: function (cellValue, options, rowdata, action) {
            var classname = "";
            if (options.Qty <= 100) {
                classname = "rowqty ";
            }

            //if (options.ReTestDate != null) {
            //    var retest = ConvertJsonDateString(options.ReTestDate);
            //    var diff = numDaysBetween(new Date(), retest);
            //    if (diff > 120) {
            //        classname += " clsretest";
            //    }
            //}
            if (options.ReTestDate != null) {
                var retest = ConvertJsonDateString(options.ReTestDate);
                var diff = numDaysBetween(new Date(), retest);

                if (diff <= 120) {
                    classname += " clsretest";
                }
            }
            if (options.ProductId != null && options.ProductId != '0') {
                return { "data-mydata": options.DefaultPictureModel.ImageUrl, 'class': classname };
            }
            return "";
        },
        postData: {},
        gridComplete: function () {
            $('.classSKU').mouseover(function () {
                $(this).closest('tr').find('.showimage').show();
            });
            $('.classSKU').mouseout(function () {
                $(this).closest('tr').find('.showimage').hide();
            });
            $('.className').mouseover(function () {
                $(this).closest('tr').find('.showSynonym').show();
            });
            $('.className').mouseout(function () {
                $(this).closest('tr').find('.showSynonym').hide();
            });
        },
        rownumbers: true,
        colModel: [
            { key: true, hidden: true, name: 'ProductId', index: 'ProductId', editable: true, resizable: false, },
            //{ key: false, name: 'MainCatName', index: 'MainCatName', width: 110, editable: true, searchoptions: { sopt: apiname }, resizable: false, },
            { key: false, name: 'Name', index: 'Name', editable: true, classes: 'className' },
            { key: false, name: 'Sku', index: 'Sku', width: 42, editable: true, search: false, resizable: false, classes: 'classSKU', align: 'center' },
            { key: false, name: 'ManufacturerPartNumber', width: 47, index: 'CAS No', editable: true, search: false, resizable: false, align: 'center' },
            { key: false, name: 'MolecularWeight', index: 'MolecularWeight', width: 26, editable: true, search: false, resizable: false, align: 'center' },
            { key: false, name: 'BatchNo', index: 'BatchNo', width: 60, editable: true, search: false, resizable: false, align: 'center' },
            { key: false, name: 'Qty', index: 'Qty', width: 40, classes: 'redrowqty', editable: true, search: false, resizable: false, align: 'center' },
            { key: false, name: 'ReTestDate', index: 'ReTestDate', classes: 'retestlarge', editable: true, search: false, resizable: false, width: 100, formatter: "date", },
            { name: 'COAPath', search: false, index: 'COAPath', width: 30, sortable: false, formatter: COAPathLink, resizable: false, align: 'center' },
            { name: 'StdDataPath', search: false, index: 'StdDataPath', width: 30, sortable: false, formatter: StdDataPathLink, resizable: false, align: 'center' },
            { name: 'AddDataPath', search: false, index: 'AddDataPath', width: 30, sortable: false, formatter: AddDataPathLink, resizable: false, align: 'center' },
            { key: false, name: 'Remarks', index: 'Remarks', width: 60, editable: true, search: false, resizable: false, formatter: RecentAditionLink, },
            //{ name: 'ProductId', search: false, index: 'ProductId', width: 30, sortable: false, formatter: RecentAditionLink, resizable: false },
            //{ name: 'ProductId', search: false, index: 'ProductId', width: 80, sortable: false, formatter: editLink, resizable: false },
        ],
        pager: jQuery('#pager'),
        rowNum: 20,
        rowList: [20, 50, 100],
        height: '100%',
        viewrecords: true,
        caption: 'Recent Addition List',
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        grouping: false,
        //groupingView: {
        //    groupField: ['Sku'],
        //    groupText: ['<b>Catalogue # {0}<b>'],
        //    groupCollapse: true,
        //    groupDataSorted: true
        //},
        autowidth: true,
        multiselect: false,
        sortname: 'InvId',
        sortorder: 'desc'
    }).navGrid('#pager', { edit: false, add: false, del: false, search: true, refresh: true });

    $("#grid").jqGrid('setGridParam', {
        postData: {

        },
        sortname: 'InvId',
        sortorder: 'desc'
    }).trigger('reloadGrid');


}

/*====================================================================*/
/*       Generic AJAX Call       */
/*====================================================================*/

function _AjaxWithOutData(action, Type) {
    var obj;

    jQuery.ajax({
        url: action
        , type: Type
        , dataType: "json"
        , async: false
        , cache: false
        , contentType: "application/json; charset=utf-8"
        , beforeSend: function (xhr) { xhr.setRequestHeader("X-Requested-With", "test-value"); }
        , success: function (data) {
            obj = data;
        }
        , error: function (xhr, textStatus, errorThrown) {
            if (xhr.status == 0) {
                alert("You are offline!!\n Please Check Your Network.");
            } else if (xhr.status == 404) {
                alert("Requested URL not found.");
            } else if (xhr.status == 500) {
                // window.location.href = "http://www.google.com";
                alert("Internel Server Error.");
            } else if (textStatus == "parsererror") {
                alert("Error.\nParsing JSON Request failed.");
            } else if (textStatus == "timeout") {
                alert("Request Time out.");
            } else {
                alert("Unknow Error.\n" + xhr.responseText);
            }

        }
    });
    return obj;
}

function _AjaxWithOutData(action, Type, Async) {
    var obj;

    jQuery.ajax({
        url: action
        , type: Type
        , dataType: "json"
        , async: Async
        , cache: false
        , contentType: "application/json; charset=utf-8"
        , beforeSend: function (xhr) { xhr.setRequestHeader("X-Requested-With", "test-value"); }
        , success: function (data) {
            obj = data;
        }
        , error: function (xhr, textStatus, errorThrown) {
            if (xhr.status == 0) {
                alert("You are offline!!\n Please Check Your Network.");
            } else if (xhr.status == 404) {
                alert("Requested URL not found.");
            } else if (xhr.status == 500) {
                // window.location.href = "http://www.google.com";
                alert("Internel Server Error.");
            } else if (textStatus == "parsererror") {
                alert("Error.\nParsing JSON Request failed.");
            } else if (textStatus == "timeout") {
                alert("Request Time out.");
            } else {
                alert("Unknow Error.\n" + xhr.responseText);
            }

        }
    });
    return obj;
}

function _Ajax(action, data, actionType) {
    var obj;
    jQuery.ajax({
        url: action,
        type: actionType,
        data: data,
        dataType: "json",
        async: false,
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            obj = data;
        }
        , failure: function (xhr, textStatus, errorThrown) {

            if (xhr.status == 0) {
                alert("You are offline!!\n Please Check Your Network.");
            } else if (xhr.status == 404) {
                alert("Requested URL not found.");
            } else if (xhr.status == 500) {
                //window.location.href = "http://www.google.com";
                alert("Internel Server Error.");
            } else if (textStatus == "parsererror") {
                alert("Error.\nParsing JSON Request failed.");
            } else if (textStatus == "timeout") {
                alert("Request Time out.");
            } else {
                alert("Unknow Error.\n" + xhr.responseText);
            }

        }
    });
    return obj;
}

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || "");
        } else {
            o[this.name] = this.value || "";
        }
    });
    return o;
};