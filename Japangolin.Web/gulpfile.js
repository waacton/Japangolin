/// <binding AfterBuild='copy-v8-runtime-build' />

var gulp = require('gulp');

var exec = require('child_process').exec;

gulp.task("copy-v8-runtime-build", function () {
    var packages = "C:\\Users\\willacto\\.dnx\\packages\\";
    var dest = "C:\\Users\\willacto\\.dnx\\runtimes\\dnx-clr-win-x86.1.0.0-rc1-update1\\ClearScript.V8";

    execLog('if not exist "' + dest + '" mkdir "' + dest + '"');
    execLog('copy /Y "' + packages + 'JavaScriptEngineSwitcher.Core\\1.2.4\\lib\\net40" "' + dest + '"');
    execLog('copy /Y "' + packages + 'JavaScriptEngineSwitcher.V8\\1.4.1\\lib\\net40" "' + dest + '"');
    execLog('copy /Y "' + packages + 'JavaScriptEngineSwitcher.V8\\1.4.1\\content\\ClearScript.V8" "' + dest + '"');
});

gulp.task("copy-v8-runtime-publish", function () {
    var packages = "C:\\Users\\willacto\\.dnx\\packages\\";
    var dest = "C:\\Users\\willacto\\AppData\\Local\\Temp\\PublishTemp\\Japangolin.Web81\\approot\\runtimes\\dnx-clr-win-x86.1.0.0-rc1-update1\\ClearScript.V8";

    execLog('if not exist "' + dest + '" mkdir "' + dest + '"');
    execLog('copy /Y "' + packages + 'JavaScriptEngineSwitcher.Core\\1.2.4\\lib\\net40" "' + dest + '"');
    execLog('copy /Y "' + packages + 'JavaScriptEngineSwitcher.V8\\1.4.1\\lib\\net40" "' + dest + '"');
    execLog('copy /Y "' + packages + 'JavaScriptEngineSwitcher.V8\\1.4.1\\content\\ClearScript.V8" "' + dest + '"');
});

function execLog(command) {
    exec(command, function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
    });
}