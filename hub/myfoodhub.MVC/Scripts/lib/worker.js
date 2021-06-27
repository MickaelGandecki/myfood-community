'use strict';

importScripts('../lib/supercluster.js');

var now = Date.now();

var index;

getJSON('../../Home/GetClusterMapData', function (result) {
    var data = JSON.parse(result.data);
    var geojson = data.ClusterData;
   
    index = supercluster({
        log: true,
        radius: 60,
        extent: 256,
        maxZoom: 17
    }).load(geojson.features);

    console.log(index.getTile(0, 0, 0));

    postMessage({ready: true, result: data});
});

self.onmessage = function (e) {
    if (e.data) {
        postMessage(index.getClusters(e.data.bbox, e.data.zoom));
    }
};

function getJSON(url, callback) {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', url, true);
    xhr.responseType = 'json';
    xhr.setRequestHeader('Accept', 'application/json');
    xhr.onload = function () {
        if (xhr.readyState === 4 && xhr.status >= 200 && xhr.status < 300 && xhr.response) {
            console.log(xhr.response);
            callback(xhr.response);
        }
    };
    xhr.send();
}
