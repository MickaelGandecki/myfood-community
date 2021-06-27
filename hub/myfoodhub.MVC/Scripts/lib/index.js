'use strict';

var map;
var markers;
/*global L */


function onEachFeature(feature, layer) {
    layer.on({
		mouseover: onMouseOver,
		click: onClick,
	});
}


function onClick(e)
{
	var layer = e.target;
	if (!$(layer._icon).hasClass('marker-cluster') && window.parent && window.parent.window && 'onClickShow' in window.parent.window) {
		window.parent.window.onClickShow(layer.feature.geometry.coordinates);
		if (!layer.getTooltip()) {
			layer.bindTooltip(layer.feature.properties.name).openTooltip();
		}
	}
}

function onMouseOver(e) {
    var layer = e.target;
    if (!$(layer._icon).hasClass('marker-cluster') && window.parent && window.parent.window && 'onClusterShow' in window.parent.window) {
        window.parent.window.onClusterShow(layer.feature.geometry.coordinates);
        if(!layer.getTooltip())
        {
            layer.bindTooltip(layer.feature.properties.name).openTooltip();
        }
    }
}

var worker = new Worker('../Scripts/lib/worker.js');
var ready = false;

worker.onmessage = function (e) {
    if (e.data.ready) {
        ready = true;
        initializeClusterMap(e.data.result);
        update();
    } else {
        markers.clearLayers();
        markers.addData(e.data);
    }
};

function initializeClusterMap(data) {
    map = L.map(data.Name).setView([data.CenterLatitude, data.CenterLongitude], data.Zoom);
    // var map = L.map(mapName).setView([lat, long], zoom);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', //data.TileUrlTemplate
    {
        attribution: data.TileAttribution,
        subdomains: data.TileSubdomains
    }).addTo(map);

    markers = L.geoJson(null, {
        pointToLayer: createClusterIcon,
        onEachFeature: onEachFeature
    }).addTo(map);

    map.on('moveend', update);
    map.on('zoomend', update);   
}

function update() {
    if (!ready) return;
    var bounds = map.getBounds();
    worker.postMessage({
        bbox: [bounds.getWest(), bounds.getSouth(), bounds.getEast(), bounds.getNorth()],
        zoom: map.getZoom()
    });
}

    

function createClusterIcon(feature, latlng) {
    if (!feature.properties.cluster) {
        var greenIcon = new L.Icon({
            iconUrl: '../Content/marker-icon-2x-green.png',
            shadowUrl: '../Content/marker-shadow.png',
            iconSize: [25, 41],
            iconAnchor: [12, 41],
            popupAnchor: [1, -34],
            shadowSize: [41, 41]
        });

        return L.marker(latlng, { icon: greenIcon });
    }

    var count = feature.properties.point_count;
    var size =
        count < 100 ? 'small' :
        count < 1000 ? 'medium' : 'large';
    var icon = L.divIcon({
        html: '<div><span>' + feature.properties.point_count_abbreviated + '</span></div>',
        className: 'marker-cluster marker-cluster-' + size,
        iconSize: L.point(40, 40)
    });
        
    return L.marker(latlng, {
        icon: icon
    });
}

