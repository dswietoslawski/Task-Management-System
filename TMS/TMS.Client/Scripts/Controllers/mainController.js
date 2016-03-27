(function () {
    'use strict';

    angular.module('app').controller('mainController', mainController);

    mainController.$inject = ['$scope', 'Tasks'];

    function mainController($scope, Tasks) {
        var vm = this;

        vm.title = "Hello User";

        var tasks = Tasks.query(function () {
            console.log(tasks);
        });

        //$scope.tasks = tasks;

        vm.tasks = [
            { row: 0, col: 0 },
            { row: 0, col: 2 },
            { row: 0, col: 4 },
            { row: 0, col: 4 },
            { row: 1, col: 0 },
            { row: 1, col: 4 },
            { row: 1, col: 4 },
            { row: 2, col: 0 },
            { row: 2, col: 1 },
            { row: 2, col: 3 },
            { row: 2, col: 4 }
        ];
    }

})();

