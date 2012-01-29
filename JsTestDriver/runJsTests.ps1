$rootDir = Resolve-Path .
$java = "$env:ProgramFiles\Java\jre6\bin\java.exe"

$firefox = "$env:ProgramFiles\Mozilla Firefox\firefox.exe"
$ie = "$env:ProgramFiles\Internet Explorer\iexplore.exe"
$chrome = "$env:LOCALAPPDATA\Google\Chrome\Application\chrome.exe"

$jsUnitTestReportPath = Join-Path $rootDir "reports"
$jsTestDriver = Join-Path $rootDir "JsTestDriver-1.3.3d.jar"

function RunTests () {
	exec { & $java -jar $jsTestDriver --port 9876 --config jsTestDriver.conf --browser "$firefox" --tests all --testOutput $jsUnitTestReportPath }
}

RunTests