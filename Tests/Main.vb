Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System

Module Program
    Sub Main()
        Console.WriteLine(Microsoft.VisualBasic.vbNewLine & "All tests completed successfully: " & Tests.RunTests())
        
        Console.Write("Press any key to continue . . . ")
        Console.ReadKey(True)
    End Sub
End Module
