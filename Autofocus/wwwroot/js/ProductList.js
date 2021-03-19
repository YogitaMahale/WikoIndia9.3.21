var datatable;
$(document).ready(function () {
    LoadData();
});

 

function LoadData() {
   
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/UserLogin/GetALL"             
            // "type": "GET",
            //"datatype": "json"
        },
        "columns": [

            

            { "data": "tradeId", "width": "10% " },
            { "data": "productName", "width": "20% " },
            { "data": "spotRate", "width": "15%" },
            { "data": "packingType", "width": "15%" },
            { "data": "productSize", "width": "10%" },
            { "data": "cityName", "width": "10%" },
            { "data": "rateTill", "width": "20%" }
//            {
//                "data": {
//                    id: "id", lockoutEnd: "lockoutEnd"
//                },
//                "render": function (data) {

                     

//                    var today = new Date().getTime();
//                    var lockout = new Date(data.lockoutEnd).getTime();
//                    if (lockout > today) {
                       
                      




//                        return `
//<div class="text-center">
  
//    <a  class="btn btn-danger text-white btn-sm" style="cursor:pointer" onclick=Lockunlock('${data.id}')>
//    <i class="ion-unlocked" data-toggle="tooltip" title="ion-unlocked"></i> Unlock
//    </a>
//  <a  class="btn btn-danger text-white btn-sm" style="cursor:pointer" onclick=Delete('${data.id}')>
//       <i class="ion-trash-b" data-toggle="tooltip" title="ion-trash-b"></i>Delete
//    </a>
//</div>`
//                    }
//                    else {
//                        return `
//<div class="text-center">
  
//    <a  class="btn btn-success text-white btn-sm" style="cursor:pointer" onclick=Lockunlock('${data.id}')>
       

//<i class="ion-locked" data-toggle="tooltip" title="ion-locked"></i>
//Lock
//    </a>
//  <a  class="btn btn-danger text-white btn-sm" style="cursor:pointer" onclick=Delete('${data.id}')>
//      <i class="ion-trash-b" data-toggle="tooltip" title="ion-trash-b"></i>Delete
//    </a>
//</div>`
//                    }




//                }, "width": "40%"

//            }

        ]
        , "bDestroy": true
    });
}
