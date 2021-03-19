var lblpackHouselatitude = 0, lblpackHouselongitude = 0, lbluserlatitude = 0, lbluserlongitude = 0;

 

lblpackHouselatitude = document.getElementById("lblpackHouselatitude").value;
lblpackHouselongitude = document.getElementById("lblpackHouselongitude").value;
var lblpackHouseAddress = document.getElementById("lblpackHouseAddress").value;


lbluserlatitude = document.getElementById("lbluserlatitude").value;
lbluserlongitude = document.getElementById("lbluserlongitude").value;
//alert(latitude1);
//latitude1 = 19.9870501;
//longitude1 = 73.7749433;
function getLocation() {

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition, showError);
    }
    else { x.innerHTML = "Geolocation is not supported by this browser."; }
}
function showPosition(position) {
    var latlondata = position.coords.latitude + "," + position.coords.longitude;
    lblpackHouselatitude = position.coords.latitude;
    lblpackHouselongitude = position.coords.longitude;

    const myLatlng = { lat: position.coords.latitude, lng: position.coords.longitude };
    // const myLatlng = { lat: 19.987, lng: 73.779 };
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 12,
        center: myLatlng,
    });
    // Create the initial InfoWindow.
    let infoWindow = new google.maps.InfoWindow({
        content: lblpackHouseAddress,//Click the map to get Lat/Lng!
        position: myLatlng,
    });
    var myAddress = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
    infoWindow.open(map);
    var marker = new google.maps.Marker({
        position: myAddress,
        animation: google.maps.Animation.BOUNCE,
    });

    marker.setMap(map);
    //----------------------user map -------------------------------------

    

    const myLatlng1 = { lat: lbluserlatitude, lng: lbluserlongitude };
    // const myLatlng = { lat: 19.987, lng: 73.779 };
    const map1 = new google.maps.Map(document.getElementById("usermap"), {
        zoom: 12,
        center: myLatlng,
    });
    // Create the initial InfoWindow.
    let infoWindow1 = new google.maps.InfoWindow({
        content: lbluserlatitude+','+lbluserlongitude,//Click the map to get Lat/Lng!
        position: myLatlng1,
    });
    var myAddress1 = new google.maps.LatLng(lbluserlatitude, lbluserlongitude);
    infoWindow1.open(map1);
    var marker1 = new google.maps.Marker({
        position: myAddress1,
        animation: google.maps.Animation.BOUNCE,
    });

    marker1.setMap(map1);



    //----------------------user map -------------------------------------


    // Configure the click listener.
    //map.addListener("click", (mapsMouseEvent) => {
    //    // Close the current InfoWindow.
    //    infoWindow.close();
      
    //    var loc = 0;
    //    infoWindow = new google.maps.InfoWindow({
    //        position: mapsMouseEvent.latLng
            
    //    });
    //    infoWindow.setContent(

    //        JSON.stringify(mapsMouseEvent.latLng.toJSON(), null, 5)
            

    //    );
    //   // alert(mapsMouseEvent.latLng);
    //    const myObjStr = JSON.stringify(mapsMouseEvent.latLng);
    //    //console.log(myObjStr);
    //    var jsondata = JSON.parse(myObjStr);
      
    //    $("#txtpackHouselatitude").val(jsondata.lat);
    //    $("#txtpackHouselongitude").val(jsondata.lng);
    //    infoWindow.open(map);
         
    //});

    var latlon = "Latitude :=" + position.coords.latitude + "," + "Longitude :=" + position.coords.longitude;
   
}

function geocodeLatLng(geocoder, map, infowindow) {
    alert("test");
    const input = document.getElementById("latlng").value;
    const latlngStr = input.split(",", 2);
    const latlng = {
        lat: parseFloat(latlngStr[0]),
        lng: parseFloat(latlngStr[1]),
    };
    geocoder.geocode({ location: latlng }, (results, status) => {
        if (status === "OK") {
            if (results[0]) {
                map.setZoom(11);
                const marker = new google.maps.Marker({
                    position: latlng,
                    map: map,
                });
                infowindow.setContent(results[0].formatted_address);
                infowindow.open(map, marker);
            } else {
                window.alert("No results found");
            }
        } else {
            window.alert("Geocoder failed due to: " + status);
        }
    });
}

function showError(error) {
    if (error.code == 1) {
        x.innerHTML = "User denied the request for Geolocation."
    }
    else if (err.code == 2) {
        x.innerHTML = "Location information is unavailable."
    }
    else if (err.code == 3) {
        x.innerHTML = "The request to get user location timed out."
    }
    else {
        x.innerHTML = "An unknown error occurred."
    }
}

function initMap() {
    getLocation();

}
