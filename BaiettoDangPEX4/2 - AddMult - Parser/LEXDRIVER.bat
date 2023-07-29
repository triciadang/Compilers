:::::::::::::::::::::::::::::
:: This is the LEXDRIVER it runs the test cases for the toy language from CS426
::
:: use double :'s for a line comment
:: use > to create and redirect output to a file
:: use >> to append output to a file
:::::::::::::::::::::::::::::

:: Run good test cases
echo Lt Col Graham's 426 Parser for ToyLanguage > results.txt
echo Running Correct Test Cases >> Results.txt
echo. >> results.txt

echo Testing program cases >> results.txt
bin\Debug\ConsoleApplication.exe tests\Program.txt >> results.txt
echo. >> results.txt

echo Testing program 2 cases >> results.txt
bin\Debug\ConsoleApplication.exe tests\Program2.txt >> results.txt
echo. >> results.txt

echo Testing math cases >> results.txt
bin\Debug\ConsoleApplication.exe tests\Math.txt >> results.txt
echo. >> results.txt

echo ----------------------------------- >> results.txt

echo Testing failure main >> results.txt
bin\Debug\ConsoleApplication.exe tests\FailMain.txt >> results.txt
echo. >> results.txt

echo Testing failure method >> results.txt
bin\Debug\ConsoleApplication.exe tests\FailMethod.txt >> results.txt
echo. >> results.txt

echo Testing failure loop >> results.txt
bin\Debug\ConsoleApplication.exe tests\FailLoop.txt >> results.txt
echo. >> results.txt

echo Testing failure ifelse >> results.txt
bin\Debug\ConsoleApplication.exe tests\FailIfElse.txt >> results.txt
echo. >> results.txt

echo Testing failure declare >> results.txt
bin\Debug\ConsoleApplication.exe tests\FailDeclare.txt >> results.txt
echo. >> results.txt

echo Testing failure function >> results.txt
bin\Debug\ConsoleApplication.exe tests\FailFunction.txt >> results.txt
echo. >> results.txt

echo Testing failure conditional >> results.txt
bin\Debug\ConsoleApplication.exe tests\FailConditional.txt >> results.txt
echo. >> results.txt

echo Testing failure assignment >> results.txt
bin\Debug\ConsoleApplication.exe tests\FailAssignment.txt >> results.txt
echo. >> results.txt

echo Testing failure math >> results.txt
bin\Debug\ConsoleApplication.exe tests\FailMath.txt >> results.txt
echo. >> results.txt