@echo off


cd tests\CodeCoverage

nuget restore packages.config -PackagesDirectory .

cd ..
cd ..

dotnet restore ImageSharp.sln
rem Clean the solution to force a rebuild with /p:codecov=true
dotnet clean ImageSharp.sln -c Release
rem The -threshold options prevents this taking ages...
tests\CodeCoverage\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:"dotnet.exe" -targetargs:"test tests\ImageSharp.Tests\ImageSharp.Tests.csproj -c Release -f netcoreapp2.1 /p:codecov=true" -register:user -threshold:10 -oldStyle -safemode:off -output:.\ImageSharp.Coverage.xml -hideskipped:All -returntargetcode -filter:"+[SixLabors.ImageSharp*]*" 

if %errorlevel% neq 0 exit /b %errorlevel%

SET PATH=C:\\Python34;C:\\Python34\\Scripts;%PATH%

rem Install tool for uploading coverage data to codecov.io; select version
rem 2.0.10 specifically since later versions seem to have a bug due to
rem which calling 'codecov' in a PowerShell script fails.
rem https://github.com/frerich/clcache/pull/321/files#diff-180360612c6b8c4ed830919bbb4dd459R59
pip install https://github.com/codecov/codecov-python/tarball/v2.0.10
codecov -f "ImageSharp.Coverage.xml"
