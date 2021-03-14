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
        infoWindow = new google.maps.InfoWindow({
            position: mapsMouseEvent.latLng,
        });
        infoWindow.setContent(
            JSON.stringify(mapsMouseEvent.latLng.toJSON(), null,5)
        );
        infoWindow.open(map);
    });

    var latlon = "Latitude :=" + position.coords.latitude + "," + "Longitude :=" + position.coords.longitude;
    alert(latlon)
    //var img_url = "http://maps.googleapis.com/maps/api/staticmap?center=" + latlondata + "&zoom=14&size=400x300&sensor=false";
    //document.getElementById("mapholder").innerHTML = "<img src='" + img_url + "'/>";
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
