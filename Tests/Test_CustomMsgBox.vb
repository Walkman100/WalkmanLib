Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace Tests
    Module Tests_CustomMsgBox
        Function Test_CustomMsgBox1() As Boolean
            Task.Run(Sub()
                         Thread.Sleep(600)
                         SendKeys.SendWait("{ENTER}")
                     End Sub)

            Dim result As DialogResult = WalkmanLib.CustomMsgBox("test")

            Return TestNumber("CustomMsgBox1", result, DialogResult.OK)
        End Function

        Function Test_CustomMsgBox2() As Boolean
            Task.Run(Sub()
                         Thread.Sleep(600)
                         SendKeys.SendWait("{ENTER}")
                     End Sub)

            Dim result As DialogResult = WalkmanLib.CustomMsgBox("test", buttons:=MessageBoxButtons.YesNoCancel)

            Return TestNumber("CustomMsgBox2", result, DialogResult.Yes)
        End Function

        Function Test_CustomMsgBox3() As Boolean
            Task.Run(Sub()
                         Thread.Sleep(600)
                         SendKeys.SendWait("{ESC}")
                     End Sub)

            Dim result As DialogResult = WalkmanLib.CustomMsgBox("test", buttons:=MessageBoxButtons.YesNoCancel)

            Return TestNumber("CustomMsgBox3", result, DialogResult.Cancel)
        End Function

        Function Test_CustomMsgBox4() As Boolean
            Task.Run(Sub()
                         Thread.Sleep(600)
                         SendKeys.SendWait("{ENTER}")
                     End Sub)

            Dim result As DialogResult = WalkmanLib.CustomMsgBox("test", buttons:=MessageBoxButtons.AbortRetryIgnore)

            Return TestNumber("CustomMsgBox4", result, DialogResult.Abort)
        End Function

        Function Test_CustomMsgBox5() As Boolean
            Task.Run(Sub()
                         Thread.Sleep(600)
                         SendKeys.SendWait("{ESC}")
                     End Sub)

            Dim result As DialogResult = WalkmanLib.CustomMsgBox("test", buttons:=MessageBoxButtons.AbortRetryIgnore)

            Return TestNumber("CustomMsgBox5", result, DialogResult.Ignore)
        End Function

        Function Test_CustomMsgBox6() As Boolean
            Task.Run(Sub()
                         Thread.Sleep(600)
                         SendKeys.SendWait("{ENTER}")
                     End Sub)

            Dim result As String = WalkmanLib.CustomMsgBox("test", Nothing, "Test Button One", "Test Button Two")

            Return TestString("CustomMsgBox6", result, "Test Button One")
        End Function

        Function Test_CustomMsgBox7() As Boolean
            Task.Run(Sub()
                         Thread.Sleep(700)
                         SendKeys.SendWait("{ESC}")
                     End Sub)

            Dim result As String = WalkmanLib.CustomMsgBox("test", Nothing, "Test Button One", "Test Button Two", "Test Button Three")

            Return TestString("CustomMsgBox7", result, "Test Button Three")
        End Function

        Function Test_CustomMsgBox8() As Boolean
            Task.Run(Sub()
                         WalkmanLib.CustomMsgBox("test", "TestTitle")
                     End Sub)

            Thread.Sleep(700)
            Dim result As String = ShowPropertiesTestsHelper.GetActiveWindowText()
            SendKeys.SendWait("{ENTER}")

            Return TestString("CustomMsgBox8", result, "TestTitle")
        End Function
    End Module
End Namespace
