:::::::::::::::::::::::::::::
:: This is the LEXDRIVER it runs the test cases for the toy language from CS426
::
:: use double :'s for a line comment
:: use > to create and redirect output to a file
:: use >> to append output to a file
:::::::::::::::::::::::::::::

:: Run good test cases
echo Lt Col Graham's 426 Semantic Analyzer for ToyLanguage > results.txt
echo Running Correct Test Cases >> Results.txt
echo. >> results.txt

echo Testing correct first program >> results.txt
bin\Debug\ConsoleApplication.exe tests\good.txt >> results.txt
echo. >> results.txt

echo Testing correct second program >> results.txt
bin\Debug\ConsoleApplication.exe tests\FullProgram.txt >> results.txt
echo. >> results.txt


:: Fill in your test cases here

echo ----------------------------------- >> results.txt
echo Testing incorrect fail cases >> results.txt
bin\Debug\ConsoleApplication.exe tests\FailTest.txt >> results.txt
echo. >> results.txt

