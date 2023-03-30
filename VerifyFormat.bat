dotnet format BooksDemo.sln

if %ERRORLEVEL% EQU 0 (
   echo Format is OK
) else (
   echo Failure
)
pause
