Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System

Namespace Tests
    Public Class RedirectConsole
        Implements IDisposable

        Private ReadOnly oldConsoleOut As IO.TextWriter
        Private ReadOnly oldConsoleErr As IO.TextWriter
        Private disposed As Boolean = False

        Public Sub New(redirectTo As IO.StringWriter)
            oldConsoleOut = Console.Out
            oldConsoleErr = Console.Error

            Console.SetOut(redirectTo)
            Console.SetError(redirectTo)
        End Sub

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposed Then
                If disposing Then
                    Console.SetOut(oldConsoleOut)
                    Console.SetError(oldConsoleErr)
                End If
                disposed = True
            End If
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
    End Class
End Namespace
