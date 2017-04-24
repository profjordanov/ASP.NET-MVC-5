// home-index.js
var module = angular.module("homeIndex", []);

module.config(function($routeProvider) {
    $routeProvider.when("/",
    {
        controller: "topicsController",
        templateUrl: "/templates/topicsView.html"
    });

    $routeProvider.when("/newmessage",
        {
            controller: "newTopicController",
            templateUrl: "/templates/newTopicView.html"
        }
    );

    $routeProvider.when("/message/:id",
    {
        controller: "singletopicController",
        templateUrl: "/template/singleTopicView.html"
    });

    $routeProvider.otherwise({redirectTo: "/"});
});

module.factory("dataService",
    function ($http, $q) {
        var _topics = [];
        var _isInit = false;

        var _isReady = function() {
            return _isInit;
        }

        var _getTopics = function () {

            var deferred = $q.defer();

            $http.get("/api/v1/topics?includeReplies=true")
                .then(function(result) {
                        // success
                    angular.copy(result.data, _topics);
                        _isInit = true;
                        deferred.resolve();
                    },
                    function() {
                        // error
                        deferred.reject();

                    });
            return deferred.promise;
        };

        var _addTopic = function(newTopic) {
            var deferred = $q.defer();

            $http.post("api/v1/topics", newTopic)
          .then(function (result) {
              //success
              var newlyCreatedTopic = result.data;
                        _topics.splice(0, 0, newlyCreatedTopic);
                        deferred.resolve(newlyCreatedTopic);
                    },
          function () {
              //error
              deferred.reject();
          });

            return deferred.promise;
        };

        return {
            topics: _topics,
            getTopics: _getTopics
        }
    });

function topicsController($scope, $http, dataService) {
  $scope.data = dataService;
  $scope.isBusy = false;

  if (dataService.isReady() == false) {
      $scope.isBusy = true;
        dataService.getTopics()
      .then(function () {
          //success
      },
      function () {
          //error
          alert("could not load topics");
      })
  .then(function () {
      $scope.isBusy = false;
  });
    }
  
};

function newTopicController($scope, $http, $window) {
    $scope.newTopic = {};

    $scope.save = function() {

        dataService.addTopic($scope.newTopic)
            .then(function() {
                //suc
                    $window.location = "#/";
                },
                function() {
                    //error
                    alert("could not save the new topic");
                });
    };
}

