var datatable;
$(document).ready(function () {


    
});

$("#mainCategroyId").change(function () {

    alert("main category");
    var url = $("#txtURLpath").val() + 'subcategory/GetSubCategorybyMaincategoryId';

    var ddlsource = "#mainCategroyId";
    //console.log(url);
    //console.log($(ddlsource).val());
    $.getJSON(url,
        { maincategoryId: $(ddlsource).val() },
        function (data) {

            var item = '<option> select </option >';
            $("#subCategroyId").empty();
            $.each(data, function (i, row) {
                //console.log("--------------");
                //console.log(row);
                item += "<option value='" + row.id + "'>" + row.name + "  </option > ";

            });
            $("#subCategroyId").html(item);
        }
    )
});

$("#subCategroyId").change(function () {
    alert("sub category");
    // alert($("#subCategroyId").val());
    var url = loadtable("/Admin/UserLogin/GetALL?subcategoryId=" + $("#subCategroyId").val())
    //LoadData(url);
});

function loadtable(url) {
  // alert(url);
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": url
            // "type": "GET",
            //"datatype": "json"
        },
        "columns": [
                   

            { "data": "tradeId", "width": "10% " },
            { "data": "productName", "width": "10% " },
            { "data": "spotRate", "width": "10% " },
            { "data": "packingType", "width": "10% " },
            { "data": "productSize", "width": "10% " },
            { "data": "cityName", "width": "10% " },
            { "data": "rateTill", "width": "10% " },
            {
                "data": "id",
                "render": function (data) {
                    return `
<div class="text-center">
    <a href="/Admin/UserLogin/BagDetails/${data}" class="btn btn-sm btn-success text-white" style="cursor:pointer">
   <i class="os-icon os-icon-ui-49"></i>
         Bag Details
    </a>
 &nbsp;
    <a  class="btn btn-sm btn-danger text-white" style="cursor:pointer" onclick=Delete("/Admin/UserLogin/Delete/${data}")>
        <i class="os-icon os-icon-ui-15"></i>Delete
    </a>
</div>`
                }, "width": "30%"

            }

//             {
//                "data": "id",
//                "render": function (data) {
//                    return `
//<div class="text-center">
//    <a href="/Admin/UserLogin/Edit/${data}" class="btn btn-sm btn-success text-white" >
//   <i class="os-icon os-icon-ui-49"></i>
//          Bag Details
//    </a>
 
//    <a  class="btn btn-sm btn-danger text-white"  onclick=Delete("/Admin/UserLogin/Delete/${data}")>
//        <i class="os-icon os-icon-ui-15"></i>Delete
//    </a>
//</div>`
//                }, "width": "30%"

//            }
      
         

        ],
        destroy: true
    });
}


function Delete(url) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the data",
        icon: "warning",
        buttons: true,
        dangerMode: true

    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                      /*  dataTable.ajax.reload()*/;
                        $('#tblData').DataTable().ajax.reload()
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }


    });

}