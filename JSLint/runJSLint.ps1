$rootDir = Resolve-Path .
$java = "$env:ProgramFiles\Java\jre6\bin\java.exe"

$jslintReportPath = Join-Path $rootDir "reports\jslintReport.txt"
$jslint4Java = Join-Path $rootDir "jslint4java-2.0.1.jar"

function RunJSLint () {
	$filesToCheck = (Get-ChildItem "src" -Recurse -Include *.js)
	
	& $java -jar $jslint4Java $filesToCheck --report "junit" | Out-File $jslintReportPath
}

RunJSLint