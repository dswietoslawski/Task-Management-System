(function () {
    'use strict';

    var tasksService = angular.module('tasksService', ['ngResource']);

    var url = "http://localhost:49687/api/tasks";

    tasksService.factory('Tasks', ['$resource', function ($resource) {
        return $resource(url, {}, {
            query: { method: 'GET', params: {}, isArray: true }
        });
    }]);
})();