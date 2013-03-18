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

function FloorViewModel() {
	var self = this;

	self.floors = ko.observableArray();	
	
	self.chosenFloor = ko.observable();
	
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
		self.chosenFloor = ko.observable(f);
	};
}


/*
function FloorViewModel() {
	var self = this;
	
	self.chooseFloor = ko.observable(afloors[0])
	
	/*self.floors = ko.observableArray();	
	self.currentFloor = {
		id: ko.observable(300180),
		number: ko.observable(),
		name: ko.observable()
	};		
	
	self.addFloor = function(id, number, name, buildArea, usableArea, imageUrl, remark) {
        self.floors.push(new Floor(id, number, name, buildArea, usableArea, imageUrl, remark));
    };
	
	ko.computed(function() {
		var fid = self.currentFloor.id;
		for (var i = 0; i < self.floors().length; i++) {
			if (self.floors()[i].id == fid) {
				self.currentFloor.number(self.floors()[i].number);
				self.currentFloor.name(self.floors()[i].name);
				break;
			}
		}		
	}, this);*/
	/*self.setFloor = function(floorId) {		
		for (var i = 0; i < self.floors().length; i++) {
			if (self.floors()[i].id == floorId) {
				self.currentFloor.id = self.floors()[i].id;
				self.currentFloor.number = self.floors()[i].number;
				self.currentFloor.name = self.floors()[i].name;
				break;
			}
		}
	};*/
//}


/*floorModel.currentFloorId.subscribe(function(newValue) {
	floorModel.setFloor(newValue);
});*/