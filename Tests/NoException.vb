Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System

Namespace Tests
    Public Class NoException
        Inherits Exception

        Public Sub New()
            MyBase.New("No Exception was thrown")
        End Sub
    End Class
End Namespace
