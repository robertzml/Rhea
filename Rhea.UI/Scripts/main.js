//window resize events
$(window).resize(function(){
	//get the window size
	var wsize =  $(window).width();
	if (wsize > 980 ) {
		$('.shortcuts.hided').removeClass('hided').attr("style","");
		$('.sidenav.hided').removeClass('hided').attr("style","");
	}

	var size ="Window size is:" + $(window).width();
	//console.log(size);
});

$(window).load(function(){
	var wheight = $(window).height();
	$('#sidebar.scrolled').css('height', wheight-63+'px');
});

// document ready function
$(document).ready(function(){ 		

	//prevent font flickering in some browsers 
	(function(){
	  //if firefox 3.5+, hide content till load (or 3 seconds) to prevent FOUT
	  var d = document, e = d.documentElement, s = d.createElement('style');
	  if (e.style.MozTransform === ''){ // gecko 1.9.1 inference
	    s.textContent = 'body{visibility:hidden}';
	    e.firstChild.appendChild(s);
	    function f(){ s.parentNode && s.parentNode.removeChild(s); }
	    addEventListener('load',f,false);
	    setTimeout(f,3000); 
	  }
	})();
	
  	//Disable certain links
    $('a[href^=#]').click(function (e) {
      e.preventDefault()
    })

    $('.search-btn').addClass('nostyle');//tell uniform to not style this element

 
	//------------- Navigation -------------//

	mainNav = $('.mainnav>ul>li');
	mainNav.find('ul').siblings().addClass('hasUl').append('<span class="hasDrop icon16 icomoon-icon-arrow-down-2"></span>');
	mainNavLink = mainNav.find('a').not('.sub a');
	mainNavLinkAll = mainNav.find('a');
	mainNavSubLink = mainNav.find('.sub a').not('.sub li .sub a');
	mainNavCurrent = mainNav.find('a.current');

	/*Auto current system in main navigation */
	var domain = document.domain;
	var absoluteUrl = 0; //put value of 1 if use absolute path links. example http://www.host.com/dashboard instead of /dashboard

	function setCurrentClass(mainNavLinkAll, url) {
		mainNavLinkAll.each(function(index) {
			//convert href to array and get last element
			var href= $(this).attr('href');

			if(href == url) {
				//set new current class
				$(this).addClass('current');

				ulElem = $(this).closest('ul');
				if(ulElem.hasClass('sub')) {
					//its a part of sub menu need to expand this menu
					aElem = ulElem.prev('a.hasUl').addClass('drop');
					ulElem.addClass('expand');
				} 
			}
		});
	}


	if(domain === '') {
		//domain not found
		var pageUrl = window.location.pathname.split( '/' );
		var winLoc = pageUrl.pop(); // get last item

		setCurrentClass(mainNavLinkAll, winLoc);

	} else {
		if(absoluteUrl === 0) {
			//absolute url is disabled
			var afterDomain = window.location.pathname;
			
			setCurrentClass(mainNavLinkAll, afterDomain);
		
		} else {
			//absolute url is enabled
			var newDomain = 'http://' + domain + window.location.pathname;
			
			setCurrentClass(mainNavLinkAll, newDomain);
		}
	}

	//hover magic add blue color to icons when hover - remove or change the class if not you like.
	mainNavLinkAll.hover(
	  function () {
	    $(this).find('span.icon16').addClass('blue');
	  }, 
	  function () {
	    $(this).find('span.icon16').removeClass('blue');
	  }
	);

	//click magic
	mainNavLink.click(function(event) {
		$this = $(this);
		//console.log($this)
		if($this.hasClass('hasUl')) {
			event.preventDefault();
			if($this.hasClass('drop')) {
				$(this).siblings('ul.sub').slideUp(500).siblings().removeClass('drop');
			} else {
				$(this).siblings('ul.sub').slideDown(500).siblings().addClass('drop');
			}			
		} 
	});
	mainNavSubLink.click(function(event) {
		$this = $(this);
		
		if($this.hasClass('hasUl')) {
			event.preventDefault();
			if($this.hasClass('drop')) {
				$(this).siblings('ul.sub').slideUp(500).siblings().removeClass('drop');
			} else {
				$(this).siblings('ul.sub').slideDown(250).siblings().addClass('drop');
			}			
		} 
	});

	//responsive buttons
	$('.resBtn>a').click(function(event) {
		$this = $(this);
		if($this.hasClass('drop')) {

			$('#header').css({'overflow-x': 'visible', 'width' : 'auto'});
			$('#header').animate({ 'margin-left' : '0'}, 300, function() {});
			$('#content').css({'overflow-x': 'visible', 'width' : 'auto'});
			$('#content').animate({ 'margin-left' : '0'}, 300, function() {});

			$('#sidebar>.shortcuts').slideUp(200).addClass('hided');
			$('#sidebar>.sidenav').slideUp(200).addClass('hided');
			$('#sidebar>.sidebar-widget').slideUp(200);

			$('#sidebar-right>.shortcuts').slideUp(200).addClass('hided');
			$('#sidebar-right>.sidenav').slideUp(200).addClass('hided');
			$('#sidebar-right>.sidebar-widget').slideUp(200);

			$('#sidebarbg').css('display', 'none');
			$('.resBtn').removeClass('offCanvas');

			$this.removeClass('drop');
		} else {
			if($('#sidebar').length) {
				$('#sidebar').css('display', 'block');
				if($('#sidebar-right').length) {
					$('#sidebar-right').css({'display' : 'block', 'margin-top' : '0'});
				}
			}
			if($('#sidebar-right').length) {
				$('#sidebar-right').css('display', 'block');
			}
			
			$('#header').css({'overflow-x': 'hidden', 'width' : '100%'});
			$('#header').animate({ 'margin-left' : '211px'}, 300, function() {});
			$('#content').css({'overflow-x': 'hidden', 'width' : '100%'});
			$('#content').animate({ 'margin-left' : '211px'}, 300, function() {});

			$('#sidebar>.shortcuts').slideDown(250);
			$('#sidebar>.sidenav').slideDown(250);
			$('#sidebar>.sidebar-widget').slideDown(250);

			$('#sidebar-right>.shortcuts').slideDown(250);
			$('#sidebar-right>.sidenav').slideDown(250);
			$('#sidebar-right>.sidebar-widget').slideDown(250);

			$('#sidebarbg').css('display', 'block');
			$('.resBtn').addClass('offCanvas');
			
			$this.addClass('drop');
		}
	});

	$('.resBtnSearch>a').click(function(event) {
		$this = $(this);
		if($this.hasClass('drop')) {
			$('.search').slideUp(500);
			$this.removeClass('drop');
		} else {
			$('.search').slideDown(250);
			$this.addClass('drop');
		}
	});
	
	//Hide and show sidebar btn
	$( '.collapseBtn' ).bind( 'click', function(){
		$this = $(this);

		//left sidbar clicked
		if ($this.hasClass('leftbar')) {
			
			if($(this).hasClass('hide')) {
				//show sidebar
				$('#sidebarbg').css('margin-left','0');
				$('#content').css('margin-left', '213'+'px');
				$('#content-two').css('margin-left', '213'+'px');
				$('#sidebar').css({'left' : '0', 'margin-left' : '0'});
								
				$this.removeClass('hide');
				$('.collapseBtn.leftbar').css('top', '120'+'px').css('left', '170'+'px').removeClass('shadow');
				$this.children('a').attr('title','Hide Left Sidebar');

			} else {
				//hide sidebar
				$('#sidebarbg').css('margin-left','-299'+'px');
				$('#sidebar').css('margin-left','-299'+'px');
				$('.collapseBtn.leftbar').animate({ //use .hide() if you experience heavy animation :)
				    left: '220',
				    top: '20'
				  }, 500, 'easeInExpo', function() {
				    // Animation complete.
				  
				}).addClass('shadow');				
				//expand content
				$this.addClass('hide');
				$this.children('a').attr('title','Show Left Sidebar');
				if($('#content').length) {
					$('#content').css('margin-left', '0');
				}
				if($('#content-two').length) {
					$('#content-two').css('margin-left', '0');
				}
							
			}

		}
	});	

	//------------- To top plugin  -------------//
	$().UItoTop({ 
		//containerID: 'toTop', // fading element id
		//containerHoverID: 'toTopHover', // fading element hover id
		//scrollSpeed: 1200,
		easingType: 'easeOutQuart' 
	});	
	
	//------------- Uniform  -------------//
	//add class .nostyle if not want uniform to style field
	//$("input, textarea, select").not('.nostyle').uniform();

	//remove loadstate class from body and show the page
	setTimeout('$("html").removeClass("loadstate")',500);

});


//additional functions for data table
$.fn.dataTableExt.oApi.fnPagingInfo = function ( oSettings )
{
	return {
		"iStart":         oSettings._iDisplayStart,
		"iEnd":           oSettings.fnDisplayEnd(),
		"iLength":        oSettings._iDisplayLength,
		"iTotal":         oSettings.fnRecordsTotal(),
		"iFilteredTotal": oSettings.fnRecordsDisplay(),
		"iPage":          Math.ceil( oSettings._iDisplayStart / oSettings._iDisplayLength ),
		"iTotalPages":    Math.ceil( oSettings.fnRecordsDisplay() / oSettings._iDisplayLength )
	};
}

$.extend($.fn.dataTableExt.oPagination, {
	"bootstrap": {
		"fnInit": function( oSettings, nPaging, fnDraw ) {
			var oLang = oSettings.oLanguage.oPaginate;
			var fnClickHandler = function ( e ) {
				e.preventDefault();
				if ( oSettings.oApi._fnPageChange(oSettings, e.data.action) ) {
					fnDraw( oSettings );
				}
			};

			$(nPaging).addClass('pagination').append(
				'<ul>'+					
					'<li class="prev disabled"><a href="#">&larr; '+oLang.sPrevious+'</a></li>'+
					'<li class="next disabled"><a href="#">'+oLang.sNext+' &rarr; </a></li>'+
				'</ul>'
			);
			var els = $('a', nPaging);			
			$(els[0]).bind( 'click.DT', { action: "previous" }, fnClickHandler );
			$(els[1]).bind( 'click.DT', { action: "next" }, fnClickHandler );
		},

		"fnUpdate": function ( oSettings, fnDraw ) {
			var iListLength = 5;
			var oPaging = oSettings.oInstance.fnPagingInfo();
			var an = oSettings.aanFeatures.p;
			var i, j, sClass, iStart, iEnd, iHalf=Math.floor(iListLength/2);

			if ( oPaging.iTotalPages < iListLength) {
				iStart = 1;
				iEnd = oPaging.iTotalPages;
			}
			else if ( oPaging.iPage <= iHalf ) {
				iStart = 1;
				iEnd = iListLength;
			} else if ( oPaging.iPage >= (oPaging.iTotalPages-iHalf) ) {
				iStart = oPaging.iTotalPages - iListLength + 1;
				iEnd = oPaging.iTotalPages;
			} else {
				iStart = oPaging.iPage - iHalf + 1;
				iEnd = iStart + iListLength - 1;
			}

			for ( i=0, iLen=an.length ; i<iLen ; i++ ) {
				// remove the middle elements
				$('li:gt(0)', an[i]).filter(':not(:last)').remove();

				// add the new list items and their event handlers
				for ( j=iStart ; j<=iEnd ; j++ ) {
					sClass = (j==oPaging.iPage+1) ? 'class="active"' : '';
					$('<li '+sClass+'><a href="#">'+j+'</a></li>')
						.insertBefore( $('li:last', an[i])[0] )
						.bind('click', function (e) {
							e.preventDefault();
							oSettings._iDisplayStart = (parseInt($('a', this).text(),10)-1) * oPaging.iLength;
							fnDraw( oSettings );
						} );
				}

				// add / remove disabled classes from the static elements
				if ( oPaging.iPage === 0 ) {
					$('li:first', an[i]).addClass('disabled');
				} else {
					$('li:first', an[i]).removeClass('disabled');
				}

				if ( oPaging.iPage === oPaging.iTotalPages-1 || oPaging.iTotalPages === 0 ) {
					$('li:last', an[i]).addClass('disabled');
				} else {
					$('li:last', an[i]).removeClass('disabled');
				}
			}
		}
	}
});