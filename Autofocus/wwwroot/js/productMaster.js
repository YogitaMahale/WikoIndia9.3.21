var datatable;
 
$(document).ready(function () {

    $("#mainCategroyId").change(function () {

        var url = $("#txtURLpath").val() + 'subcategory/GetSubCategorybyMaincategoryId';        
        
        var ddlsource = "#mainCategroyId";
        console.log(url);
        console.log($(ddlsource).val());
        $.getJSON(url,
            { maincategoryId: $(ddlsource).val() },
            function (data) {
               
                var item = '<option> select </option >';
                $("#subCategroyId").empty();
                $.each(data, function (i, row) {
                    console.log("--------------");
                    console.log(row);
                    item += "<option value='" + row.id + "'>" + row.name + "  </option > ";
                    
                });
                $("#subCategroyId").html(item);
            }
        )
    });
    $("#subCategroyId").change(function () {

        loadtable("/Admin/ProductMaster/GetALL?subCategroyId=" + $("#subCategroyId").val())
    });
   // loadtable();
});

function loadtable(url) {
    datatable = $('#tblData').DataTable({
        "ajax": {
            //"url": "/Admin/ProductMaster/GetALL"
            "url": url
            // "type": "GET",
            //"datatype": "json"
        },
        "columns": [
            
            { "data": "name", "width": "20% " },          
            {
                "data": "img",
                "render": function (data) {
                    return ` <img src='${data}'   width="50" height="50"/>`
                }, "width": "20%"

            },
            { "data": "description", "width": "40% " },
            {
                "data": "id",
                "render": function (data) {
                    return `
<div class="text-center">
    <a href="/Admin/ProductMaster/Edit/${data}" class="btn btn-sm btn-success text-white" style="cursor:pointer">
   <i class="os-icon os-icon-ui-49"></i>
         Edit
    </a>
 &nbsp;
    <a  class="btn btn-sm btn-danger text-white" style="cursor:pointer" onclick=Delete("/Admin/ProductMaster/Delete/${data}")>
        <i class="os-icon os-icon-ui-15"></i>Delete
    </a>
</div>`
                }, "width": "20%" 

            }

        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to delete?",
        text:"You will not be able to restore the data",
        icon:"warning",
        buttons: true,
        dangerMode:true

    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url:url,
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