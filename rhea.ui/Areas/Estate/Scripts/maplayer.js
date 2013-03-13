var map, markers;

function initMap() {

	map = new OpenLayers.Map('map', {
		resolutions: [0.17578125, 0.087890625, 0.0439453125, 0.02197265625],
		maxExtent: new OpenLayers.Bounds(-80, -80, 80, 80),
		numZoomLevels: 4
	});
	
	layer3D = new OpenLayers.Layer.TileCache("3D江大",
		["/Areas/Estate/OpenLayers/map/JiangnanUniversity/"],
		"3D",
		{
			serverResolutions: [0.703125, 0.3515625, 0.17578125, 0.087890625,
								0.0439453125, 0.02197265625, 0.010986328125, 0.0054931640625,
								0.00274658203125, 0.001373291015625, 0.0006866455078125, 0.00034332275390625,
								0.000171661376953125, 0.0000858306884765625, 0.00004291534423828125, 0.000021457672119140625]
		}
	);
	layer2D = new OpenLayers.Layer.TileCache("2D江大",
		["/Areas/Estate/OpenLayers/map/JiangnanUniversity/"],
		"2D",
		{
			serverResolutions: [0.703125, 0.3515625, 0.17578125, 0.087890625,
								0.0439453125, 0.02197265625, 0.010986328125, 0.0054931640625,
								0.00274658203125, 0.001373291015625, 0.0006866455078125, 0.00034332275390625,
								0.000171661376953125, 0.0000858306884765625, 0.00004291534423828125, 0.000021457672119140625]
		}
	);
	map.addLayer(layer3D);
	map.addLayer(layer2D);

	markers = new OpenLayers.Layer.Markers("楼群");
	//map.addLayer(markers);


	//addMarkers();
	//getBuildingGroupsInfo();
	
	//map.addControl(new OpenLayers.Control.PanZoomBar());
	//map.addControl(new OpenLayers.Control.KeyboardDefaults());
	map.addControl(new OpenLayers.Control.LayerSwitcher({ 'ascending': false })); 	
	map.addControl(new OpenLayers.Control.MousePosition()); 
	map.setCenter(new OpenLayers.LonLat(0, 0), 1); 	
}

var icon, infos;
function addMarkers() {

	var size = new OpenLayers.Size(24,24);
	var offset = new OpenLayers.Pixel(-(size.w/2), -size.h);
	icon = new OpenLayers.Icon('/Images/bubble.png',size,offset);
	
	var AutoSizeAnchored = OpenLayers.Class(OpenLayers.Popup.Anchored, {
            'autoSize': true,
			'maxSize': new OpenLayers.Size(400, 250)
	});
	var AutoSizeFramedCloud = OpenLayers.Class(OpenLayers.Popup.FramedCloud, {
            'autoSize': true, 
            'maxSize': new OpenLayers.Size(500, 400)
	});
	
	var ll, popupClass, popupContentHTML;
	
	//anchored popup small contents no autosize closebox
	var ll = new OpenLayers.LonLat(5, 11);
	popupClass = AutoSizeFramedCloud;
	popupContentHTML = createHtml(infos[4]);
	addMarker(ll, popupClass, popupContentHTML, true);	
	
	ll = new OpenLayers.LonLat(-14.3, -5.6);
	popupClass = AutoSizeFramedCloud;
	popupContentHTML = createHtml(infos[6]);
	addMarker(ll, popupClass, popupContentHTML, true);	
}

/**
 * Function: addMarker
 * Add a new marker to the markers layer given the following lonlat, 
 *     popupClass, and popup contents HTML. Also allow specifying 
 *     whether or not to give the popup a close box.
 * 
 * Parameters:
 * ll - {<OpenLayers.LonLat>} Where to place the marker
 * popupClass - {<OpenLayers.Class>} Which class of popup to bring up 
 *     when the marker is clicked.
 * popupContentHTML - {String} What to put in the popup
 * closeBox - {Boolean} Should popup have a close box?
 * overflow - {Boolean} Let the popup overflow scrollbars?
 */
function addMarker(ll, popupClass, popupContentHTML, closeBox, overflow) {

	var feature = new OpenLayers.Feature(markers, ll); 
	feature.closeBox = closeBox;
	feature.popupClass = popupClass;
	feature.data.popupContentHTML = popupContentHTML;
	feature.data.overflow = (overflow) ? "auto" : "hidden";
	feature.data.icon = icon.clone();
	
	var marker = feature.createMarker();	
	
	var markerClick = function (evt) {
		if (this.popup == null) {
			this.popup = this.createPopup(this.closeBox);
			map.addPopup(this.popup);
			this.popup.show();
		} else {
			this.popup.toggle();
		}
		currentPopup = this.popup;
		OpenLayers.Event.stop(evt);
	};
	marker.events.register("mousedown", feature, markerClick);

	markers.addMarker(marker);
}

function getBuildingGroupsInfo() {	
	$.ajax({
		url: "/Common/BuildingGroupsInfo",
		type: 'get',		
		success: function (response) {
			infos = response;
			addMarkers();
		},
		error: function (response) {
			alert('Loading data failed');
		}
	});
}

function createHtml(info) {	
	var ht = [];
	ht.push("<div id='popinfo'><h3>" + info.Name + "<a href='/Home/Index?id=bg" + info.Id.toString() + "'>详细</a></h3>");
	ht.push("<div id='infoleft'><ul>");
	ht.push("<li>楼宇栋数: <span>" + info.BuildingCount + "</span></li>");
	ht.push("<li>建筑面积: <span>" + info.BuildArea + "</span></li>");
	ht.push("<li>使用面积: <span>" + info.UsableArea + "</span></li>");
	ht.push("<li>占地面积: <span>" + info.Floorage + "</span></li>");
	ht.push("<li>建筑结构: <span>" + info.BuildStructure + "</span></li>");
	ht.push("<li>建成日期: <span>" + info.BuildDate + "</span></li>");
	ht.push("<li>备注: <span>" + info.Remark + "</span></li>");	
	ht.push("</ul></div><div id='inforight'>");
	ht.push("<img src='" + info.ImageUrl + "' alt='image' />");
	ht.push("</div><div class='clear'></div></div>");
	ht = ht.join('');
	return ht;
}