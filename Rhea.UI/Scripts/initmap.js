$(function () {
	var myAppMap = new FUI.Map("mapFrame", {
            server: "http://smart.jiangnan.edu.cn:8888",
            tag: "estate",
            name: "江南大学",
            initZoom: 3,
            setup: function (config) {
            },
            select: function (ponits) {
                var tag;
                for (tag in ponits) {	//遍历选中标签
                }
            },
            pin: function (config, x, y, zoom) {	//手工标注
            }
        });

        myAppMap.init(function () {
		
			$.getJSON('/Home/MapPointsData', function(response) {
				$.each(response, function(i, item) {
					var tag = item.TargetId;
					var cfg = {
						name: item.Name,
						html: item.Content,
						mapX: item.PointX,
						mapY: item.PointY,
						mapZoom: item.Zoom,
						icon: item.Pin,
						color: "red",
						width: 160,
						active: false,
						type: item.TargetType,
						fn: function (point, tag) {
							var url = "/Home/MapDetails?targetId=" + tag + "&targetType=" + cfg.type;
							$('.modal-body').load(url);
							$('#myModalLabel').text(cfg.name);
							$('#mapModal').modal();
							$('#map-point-enter').click(function() {
								window.location.href = '/Home/Estate/' + tag;
							});
						}
					};
					
					myAppMap.addPoint(tag, cfg);
				});
			});

        }); //加载标注层

        myAppMap.show("normal");
});