:::::::::::::::::::::::::::::
:: This is the LEXDRIVER it runs the test cases for the toy language from CS426
::
:: use double :'s for a line comment
:: use > to create and redirect output to a file
:: use >> to append output to a file
:::::::::::::::::::::::::::::

:: Run good test cases
echo Tricia's 426 LEXER for ToyLanguage > results.txt
echo Running Correct Test Cases >> Results.txt
echo. >> results.txt

echo Testing correct assignment operator := >> results.txt
bin\Debug\ConsoleApplication.exe tests\Assign.txt >> results.txt
echo. >> results.txt

echo Testing correct plus operator + >> results.txt
bin\Debug\ConsoleApplication.exe tests\Plus.txt >> results.txt
echo. >> results.txt

echo Testing correct negative operator * >> results.txt
bin\Debug\ConsoleApplication.exe tests\Negative.txt >> results.txt
echo. >> results.txt

echo Testing correct mult operator * >> results.txt
bin\Debug\ConsoleApplication.exe tests\Mult.txt >> results.txt
echo. >> results.txt

echo Testing correct divide operator * >> results.txt
bin\Debug\ConsoleApplication.exe tests\Divide.txt >> results.txt
echo. >> results.txt

echo Testing correct left parentheses operator * >> results.txt
bin\Debug\ConsoleApplication.exe tests\Left_parentheses.txt >> results.txt
echo. >> results.txt

echo Testing correct right parentheses operator * >> results.txt
bin\Debug\ConsoleApplication.exe tests\Right_parentheses.txt >> results.txt
echo. >> results.txt

echo Testing correct AND operator * >> results.txt
bin\Debug\ConsoleApplication.exe tests\AND.txt >> results.txt
echo. >> results.txt

echo Testing correct OR operator * >> results.txt
bin\Debug\ConsoleApplication.exe tests\OR.txt >> results.txt
echo. >> results.txt

echo Testing correct NOT operator * >> results.txt
bin\Debug\ConsoleApplication.exe tests\NOT.txt >> results.txt
echo. >> results.txt

echo Testing correct equal operator * >> results.txt
bin\Debug\ConsoleApplication.exe tests\Equal.txt >> results.txt
echo. >> results.txt

echo Testing correct greater than or equal operator * >> results.txt
bin\Debug\ConsoleApplication.exe tests\GreaterEqual.txt >> results.txt
echo. >> results.txt

echo Testing correct less than or equal operator * >> results.txt
bin\Debug\ConsoleApplication.exe tests\LessEqual.txt >> results.txt
echo. >> results.txt

echo Testing correct greater operator * >> results.txt
bin\Debug\ConsoleApplication.exe tests\Greater.txt >> results.txt
echo. >> results.txt

echo Testing correct less than operator * >> results.txt
bin\Debug\ConsoleApplication.exe tests\Less.txt >> results.txt
echo. >> results.txt

echo Testing correct EOL operator ; >> results.txt
bin\Debug\ConsoleApplication.exe tests\EOL.txt >> results.txt
echo. >> results.txt

echo Testing correct Integers >> results.txt
bin\Debug\ConsoleApplication.exe tests\Integers.txt >> results.txt
echo. >> results.txt

echo Testing correct Floats >> results.txt
bin\Debug\ConsoleApplication.exe tests\Floats.txt >> results.txt
echo. >> results.txt

echo Testing correct Comments >> results.txt
bin\Debug\ConsoleApplication.exe tests\Comments.txt >> results.txt
echo. >> results.txt

echo Testing correct IDs >> results.txt
bin\Debug\ConsoleApplication.exe tests\ID.txt >> results.txt
echo. >> results.txt

echo Testing correct String >> results.txt
bin\Debug\ConsoleApplication.exe tests\String.txt >> results.txt
echo. >> results.txt

echo Testing correct Char >> results.txt
bin\Debug\ConsoleApplication.exe tests\Char.txt >> results.txt
echo. >> results.txt

echo Testing correct complex case >> results.txt
bin\Debug\ConsoleApplication.exe tests\ComplexCase.txt >> results.txt
echo. >> results.txt

echo ----------------------------------- >> results.txt

echo Testing failure cases >> results.txt
bin\Debug\ConsoleApplication.exe tests\FailCases.txt >> results.txt
echo. >> results.txt
