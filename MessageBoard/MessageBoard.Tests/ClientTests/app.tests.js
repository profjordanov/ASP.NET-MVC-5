/// <reference path="c:\users\jordan\documents\github\csharp-mvc-frameworks---asp.net\messageboard\messageboard.tests\scripts\jasmine\jasmine.js" />
/// <reference path="../../messageboard/js/myapp.js" />


//description of group of tests used and list of actual definitions of the test
describe("myapp tests->",
    function() {
        it("isDebug", //subgrouping
            function() {
                expect(app.isDebug).toEqual(true);
            });
        it("log",
            function() {
                expect(app.log).toBeDefined();
                app.log("testing");
            });
    });