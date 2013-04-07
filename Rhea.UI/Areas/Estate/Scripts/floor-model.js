function Floor(id, number, name, buildArea, usableArea, imageUrl, remark) {
    var self = this;
	self.id = id;
	self.number = number;
    self.name = name;
	self.buildArea = buildArea;
	self.usableArea = usableArea;
	self.imageUrl = imageUrl;
	self.remark = remark;
}

function Room(id, name, number, floor, property) {
	var self = this;
	self.id = id;
	self.name = name;
	self.number = number;
	self.floor = floor;
	self.property = property;
}

function FloorViewModel() {
	var self = this;

	self.floors = ko.observableArray();	
	
	self.chosenFloor = ko.observable();
	
	self.rooms = ko.observableArray();
	
	self.addFloor = function(id, number, name, buildArea, usableArea, imageUrl, remark) {  
		self.floors.push(new Floor(id, number, name, buildArea, usableArea, imageUrl, remark));
    };	
	
	self.findFloor = function(id) {
		var lists = self.floors();
		return ko.utils.arrayFirst(lists, function(lists) {
            return lists.id === id;
        });
	};
	
	self.setFloor = function(f) {
		self.chosenFloor(f);
	};
	
	self.getRooms = function(buildingId) {
		$.getJSON("/Estate/Room/List", { buildingId: buildingId }, function(data) {
			//ko.mapping.fromJSON(data, self.rooms);			
			$.each(data, function(i, item) {
				self.rooms.push(new Room(item.Id, item.Name, item.Number, item.Floor, item.Function.Property));
			});
		});
	};
}

/*floorModel.currentFloorId.subscribe(function(newValue) {
	floorModel.setFloor(newValue);
});*/