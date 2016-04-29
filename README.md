# par
Console calculator

#How to use it
call the exe from command line with this syntax: ``` <exe name> <number><operation symbol (* , + , /)><number> ``` no spaces between numbers and operator (```2*3``` --> OK , ```2 * 3``` --> NO!)

type ``` <exe name> --help ``` for the list of available operations

#How to use par.dll
1. Import par.dll library to your C# project;
``` 
  using par;
``` 
2. Create an instance of the class Par and pass the args[] to the constructor
``` 
  Par p = new Par(args);
``` 
3. Call the instance method ``` ManageCmd() ``` that returns the output string.
``` 
  Console.Write(p.ManageCmd());
``` 

#Examples:
**1:**
INPUT:
``` 
>name.exe 8*8 -ores 9+9 -sumall 2+20 1+2 -allores
``` 
OUTPUT:
``` 
8*8=64
18
2+20=22
1+2=3
25
```
**2:**
INPUT:
``` 
>name.exe -ores 8*8 5/9
``` 
OUTPUT:
``` 
64
5/9=0,555555555555556
``` 

</br>
>Pay attention:This program doesn't have code for check the correct input format (any crashes may be due to a number format parsing error)
