:::::::::::::::::::::::::::::
:: This is the PARSEDRIVER it runs the test cases for the toy language from CS426
::
:: use double :'s for a line comment
:: use > to create and redirect output to a file
:: use >> to append output to a file
:::::::::::::::::::::::::::::

:: Run good test cases
echo Tricia's 426 PARSER for ToyLanguage > results.txt
echo Running Correct Test Cases >> Results.txt
echo. >> results.txt

:: Fill in your test cases here

echo ----------------------------------- >> results.txt

echo Testing failure cases >> results.txt
bin\Debug\ConsoleApplication.exe tests\FailCases.txt >> results.txt
echo. >> results.txt
