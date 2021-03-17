var latitude1 = 0, longitude1 = 0;
function getLocation() {

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition, showError);
    }
    else { x.innerHTML = "Geolocation is not supported by this browser."; }
}
function showPosition(position) {
    var latlondata = position.coords.latitude + "," + position.coords.longitude;
    latitude1 = position.coords.latitude;
    longitude1 = position.coords.longitude;

 //   alert(latitude1);

    //$("#txtpackHouselatitude").val(jsondata.lat);
    //$("#txtpackHouselongitude").val(jsondata.lng);


    const myLatlng = { lat: position.coords.latitude, lng: position.coords.longitude };
    // const myLatlng = { lat: 19.987, lng: 73.779 };
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 12,
        center: myLatlng,
    });
    // Create the initial InfoWindow.
    let infoWindow = new google.maps.InfoWindow({
        content: "Click the map to get Lat/Lng!",
        position: myLatlng,
    });
    infoWindow.open(map);
    // Configure the click listener.
    map.addListener("click", (mapsMouseEvent) => {
        // Close the current InfoWindow.
        infoWindow.close();
        // Create a new InfoWindow.

        


        //$.ajax({
        //    // url: 'http://localhost:3413/api/person?Name=Sourav',
        //    url:"https://maps.google.com/maps/api/geocode/xml?latlng=19.9870703,73.7749433&sensor=false",
        //   // url: "http://maps.google.com/maps/api/geocode/xml?latlng=19.9870502,73.7793207&sensor=false",
        //   // url: "https://maps.googleapis.com/maps/api/geocode/json?latlng=19.9870703,73.7749433&key=AIzaSyDcXKKT1uwx4B0IrReicMNqBbL9kSSFw3g",

        //    type: 'GET',
        //    dataType: 'json',
        //    success: function (data, textStatus, xhr) {
        //        console.log(data);
        //    },
        //    error: function (xhr, textStatus, errorThrown) {
        //        //error: function (error) {
        //        console.log('Error in Operation');

        //        // console.log(textStatus+'Error in Operation');
        //    }
        //});
        var loc = 0;
        infoWindow = new google.maps.InfoWindow({
            position: mapsMouseEvent.latLng
            
        });
        infoWindow.setContent(

            JSON.stringify(mapsMouseEvent.latLng.toJSON(), null, 5)
            

        );
       // alert(mapsMouseEvent.latLng);
        const myObjStr = JSON.stringify(mapsMouseEvent.latLng);
        //console.log(myObjStr);
        var jsondata = JSON.parse(myObjStr);
       // console.log(jsondata);

        //var res = mapsMouseEvent.latLng.replace("(", "");
        //console.log(res);
        //res += res.latLng.replace(")", "");
        //console.log(res);
        //var res1 = res.split(",");
        //console.log(res1);
        $("#txtpackHouselatitude").val(jsondata.lat);
        $("#txtpackHouselongitude").val(jsondata.lng);
        infoWindow.open(map);
         
    });

    var latlon = "Latitude :=" + position.coords.latitude + "," + "Longitude :=" + position.coords.longitude;
    //$("#txtpackHouselatitude").val(position.coords.latitude);
    //$("#txtpackHouselongitude").val(position.coords.longitude);
    //alert(latlon)
    //const geocoder = new google.maps.Geocoder();
    //const infowindow = new google.maps.InfoWindow();
    //document.getElementById("submit").addEventListener("click", () => {
    //    geocodeLatLng(geocoder, map, infowindow);
    //});
    //$.ajax({
    //    // url: 'http://localhost:3413/api/person?Name=Sourav',
    //    url: "https://maps.googleapis.com/maps/api/geocode/json?latlng=19.987,73.779&key=AIzaSyDcXKKT1uwx4B0IrReicMNqBbL9kSSFw3g",

    //    type: 'GET',
    //    dataType: 'json',
    //    success: function (data, textStatus, xhr) {
    //        console.log(data);
    //    },
    //    error: function (xhr, textStatus, errorThrown) {
    //        //error: function (error) {
    //        console.log('Error in Operation');

    //        // console.log(textStatus+'Error in Operation');
    //    }
    //});
   
    //var img_url = "http://maps.googleapis.com/maps/api/staticmap?center=" + latlondata + "&zoom=14&size=400x300&sensor=false";
    //document.getElementById("mapholder").innerHTML = "<img src='" + img_url + "'/>";
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
