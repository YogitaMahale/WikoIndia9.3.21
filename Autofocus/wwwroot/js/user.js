var datatable;
$(document).ready(function () {

    $("#roleId").change(function () {
       
        loadtable1("/Admin/User/GetALL?id="+$("#roleId").val())
    });
  //  loadtable();
});

function loadtable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/User/GetALL"
            // "type": "GET",
            //"datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "30% " },

            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            //{ "data": "company.name", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                "data": {
                    id: "id", lockoutEnd: "lockoutEnd"
                },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        return `
<div class="text-center">
 <a class="btn btn-primary text-white btn-sm" data-toggle="tooltip" data-original-title="View" href="/Admin/User/EditBasicInfo/${data.id}" >
                                                <i class="fa fa-edit">
                                                </i>Basic Information
                                            </a>
    <a  class="btn btn-danger text-white btn-sm" style="cursor:pointer" onclick=Lockunlock('${data.id}')>
       <i class="fa fa-lock-open"></i> Unlock
    </a>
  <a  class="btn btn-danger text-white btn-sm" style="cursor:pointer" onclick=Delete('${data.id}')>
       <i class="fa fa-trash-alt"></i> Delete
    </a>
</div>`
                    }
                    else {
                        return `
<div class="text-center">
 <a class="btn btn-primary text-white btn-sm" data-toggle="tooltip" data-original-title="View" href="/Admin/User/EditBasicInfo/${data.id}" >
                                                <i class="fa fa-edit">
                                                </i>Basic Information
                                            </a>
    <a  class="btn btn-success text-white btn-sm" style="cursor:pointer" onclick=Lockunlock('${data.id}')>
       <i class="fa fa-unlock-alt"></i> Lock
    </a>
  <a  class="btn btn-danger text-white btn-sm" style="cursor:pointer" onclick=Delete('${data.id}')>
       <i class="fa fa-trash-alt"></i> Delete
    </a>
</div>`
                    }




                }, "width": "40%"

            }

        ]
        , "bDestroy": true
    });
}

function Lockunlock(id) {
    
            $.ajax({
                type: "POST",
                url: '/Admin/User/Lockunlock',
                data: JSON.stringify(id),
                contentType:"application/json",

                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        //dataTable.ajax.reload();
                        $('#tblData').DataTable().ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
         

}
function Delete(id) {
        swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the data",
        icon: "warning",
        buttons: true,
        dangerMode: true

    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "POST",
                url: '/Admin/User/Delete',
                data: JSON.stringify(id),
                contentType: "application/json",

                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        //dataTable.ajax.reload();
                        $('#tblData').DataTable().ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });

        }


    });
  

}



function loadtable1(url) {
    //alert(url);
    datatable = $('#tblData').DataTable({
        "ajax": {
            //"url": "/Admin/User/GetALL"
            "url": url
            // "type": "GET",
            //"datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "30% " },

            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            //{ "data": "company.name", "width": "15%" },
            //{ "data": "role", "width": "15%" },
            {
                "data": {
                    id: "id", lockoutEnd: "lockoutEnd"
                },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        return `
<div class="text-center">
 <a class="btn btn-primary text-white btn-sm" data-toggle="tooltip" data-original-title="View" href="/Admin/User/EditBasicInfo/${data.id}" >
                                                <i class="fa fa-edit">
                                                </i>Edit Basic Information
                                            </a>
    <a  class="btn btn-danger text-white btn-sm" style="cursor:pointer" onclick=Lockunlock('${data.id}')>
       <i class="fa fa-lock-open"></i> Unlock
    </a>
  <a  class="btn btn-danger text-white btn-sm" style="cursor:pointer" onclick=Delete('${data.id}')>
       <i class="fa fa-trash-alt"></i> Delete
    </a>
</div>`
                    }
                    else {
                        return `
<div class="text-center">
 <a class="btn btn-primary text-white btn-sm" data-toggle="tooltip" data-original-title="View" href="/Admin/User/EditBasicInfo/${data.id}" >
                                                <i class="fa fa-edit">
                                                </i>Edit Basic Information
                                            </a>
    <a  class="btn btn-success text-white btn-sm" style="cursor:pointer" onclick=Lockunlock('${data.id}')>
       <i class="fa fa-unlock-alt"></i> Lock
    </a>
  <a  class="btn btn-danger text-white btn-sm" style="cursor:pointer" onclick=Delete('${data.id}')>
       <i class="fa fa-trash-alt"></i> Delete
    </a>
</div>`
                    }




                }, "width": "40%"

            }

        ]
        , "bDestroy": true
    });
}
