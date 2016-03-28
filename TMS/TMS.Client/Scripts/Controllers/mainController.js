(function () {
    'use strict';

    angular.module('app').controller('mainController', mainController);

    mainController.$inject = ['$scope', 'Tasks'];

    function mainController($scope, Tasks) {
        var vm = this;

        var tasks = Tasks.query(function () {
            console.log(tasks);
        });

        //$scope.tasks = tasks;

        vm.tasks = [
            { col: 0 },
            { col: 0 },
            { col: 0 },
            { col: 0 },
            { col: 0 },
            { col: 0 },
            { col: 0 },
            { col: 0 },
            { col: 0 },
            { col: 0 },
            { col: 0 }
        ];
    }

})();

