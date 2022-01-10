$project = "ShopsRUs.sln"
$output = ".\ShopsRUs\bin\Debug"
$dotnet = "C:\Program Files\dotnet\dotnet.exe"
$nl = [Environment]::NewLine
 $nl
 echo 'Build starting..'
 $nl

# clear previous releases
Remove-Item "$output\*" -Exclude *.db -Recurse

& $dotnet build $project -c debug

 echo 'Build step completed.'
 $nl
 
 echo 'Unit test running'
 $nl
 
& $dotnet test $project
 
  echo 'Unit test step completed.'
  $nl