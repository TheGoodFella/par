# par
Console calculator

#How to use it
call the exe from command line with this syntax: ``` <exe name> <number> <operation symbol (* , + , /)> <number> ```

type ``` <exe name> --help ``` for the list of available operations

#How to use par.dll
1. Import par.dll library to your C# project;
2. Create an instance of the class Par;
3. Call the instance method ``` ManageCmd() ``` that returns the output string.

>This program doesn't have code for check the correct input format (any crashes may be due to a number format parsing error)
