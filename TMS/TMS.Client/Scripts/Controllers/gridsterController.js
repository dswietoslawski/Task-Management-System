(function () {
    'use strict';

    angular.module('app').controller('gridsterController', gridsterController);

    gridsterController.$inject = ['$scope'];

    function gridsterController($scope) {
    	var vm = this;

    	vm.gridsterOpts = {
    		minRows: 2, // the minimum height of the grid, in rows
    		maxRows: 100,
    		columns: 5, // the width of the grid, in columns
    		colWidth: 'auto', // can be an integer or 'auto'.  'auto' uses the pixel width of the element divided by 'columns'
    		rowHeight: 'match', // can be an integer or 'match'.  Match uses the colWidth, giving you square widgets.
    		margins: [10, 20], // the pixel distance between each widget
    		defaultSizeX: 1, // the default width of a gridster item, if not specifed
    		defaultSizeY: 1, // the default height of a gridster item, if not specified
    		mobileBreakPoint: 600, // if the screen is not wider that this, remove the grid layout and stack the items
    		resizable: {
    			enabled: false,
    			start: function (event, uiWidget, $element) { }, // optional callback fired when resize is started,
    			resize: function (event, uiWidget, $element) { }, // optional callback fired when item is resized,
    			stop: function (event, uiWidget, $element) { } // optional callback fired when item is finished resizing
    		},
    		draggable: {
    			enabled: true, // whether dragging items is supported
    			handle: '.my-class', // optional selector for resize handle
    			start: function (event, uiWidget, $element) { }, // optional callback fired when drag is started,
    			drag: function (event, uiWidget, $element) { }, // optional callback fired when item is moved,
    			stop: function (event, uiWidget, $element) { } // optional callback fired when item is finished dragging
    		}
    	};
    }

})();

