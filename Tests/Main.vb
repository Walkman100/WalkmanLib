Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System

Module Program
    Sub Main()
        If Environment.GetCommandLineArgs().Length = 2 AndAlso Environment.GetCommandLineArgs()(1) = "getAdmin" Then
            Console.WriteLine(WalkmanLib.IsAdmin)
            Environment.Exit(0)
        End If
        
        Console.WriteLine(Microsoft.VisualBasic.vbNewLine & "All tests completed successfully: " & Tests.RunTests())
        
        Console.Write("Press any key to continue . . . ")
        Console.ReadKey(True)
    End Sub
End Module
