/// <reference path="c:\users\jordan\documents\github\csharp-mvc-frameworks---asp.net\messageboard\messageboard.tests\scripts\jasmine\jasmine.js" />
/// <reference path="../../messageboard/scripts/angular.js" />
/// <reference path="../../messageboard/scripts/angular-mocks.js" />
/// <reference path="../../messageboard/js/home-index.js" />
/// <reference path="../../messageboard/js/home-index.min.js" />


describe("home-index Tests->", function () {

    beforeEach(function () {
        module("homeIndex");
    });

    var $httpBackend; //mock up the backend

    beforeEach(inject(function ($injector) {

        $httpBackend = $injector.get("$httpBackend");

        $httpBackend.when("GET", "/api/v1/topics?includeReplies=true")
          .respond([ //returning this babies
            {
                title: "first title",
                body: "some body",
                id: 1,
                created: "20050401"
            },
            {
                title: "second title",
                body: "some body",
                id: 1,
                created: "20050401"
            },
            {
                title: "third title",
                body: "some body",
                id: 1,
                created: "20050401"
            },
          ]);

    }));

    afterEach(function () { //called everytime after test 
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    describe("dataService->", function () { // jasmine knows how to understand 

        it("can load topics", inject(function (dataService) { //wraps the callback function

            expect(dataService.topics).toEqual([]); // firstly no items in dataservice

            $httpBackend.expectGET("/api/v1/topics?includeReplies=true");
            dataService.getTopics();
            $httpBackend.flush();
            expect(dataService.topics.length).toBeGreaterThan(0);
            expect(dataService.topics.length).toEqual(3);

        }));

    });

    describe("topicsController->", function () {

        it("load data", inject(function ($controller, $http, dataService) {

            var theScope = {};

            $httpBackend.expectGET("/api/v1/topics?includeReplies=true"); //expect some standart called to be made

            var ctrl = $controller("topicsController", { //got from the injection
                $scope: theScope,
                $http: $http,
                dataService: dataService
            });

            $httpBackend.flush();

            expect(ctrl).not.toBeNull();
            expect(theScope.data).toBeDefined();
        }));

    });

});