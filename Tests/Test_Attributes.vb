Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Module Tests_Attributes
        Function Test_Attributes1(rootTestFolder As String) As Boolean
            Dim returnVal As Boolean = True

            Dim testFile As String = Path.Combine(rootTestFolder, "setAttributeTest1.txt")
            File.Create(testFile).Dispose()

            WalkmanLib.SetAttribute(testFile, FileAttributes.Normal)
            If Not TestNumber("Attributes1.1", File.GetAttributes(testFile), FileAttributes.Normal) Then returnVal = False

            WalkmanLib.SetAttribute(testFile, FileAttributes.Hidden)
            If Not TestNumber("Attributes1.2", File.GetAttributes(testFile), FileAttributes.Hidden) Then returnVal = False

            WalkmanLib.SetAttribute(testFile, File.GetAttributes(testFile) Or FileAttributes.System)
            If Not TestNumber("Attributes1.3", File.GetAttributes(testFile), FileAttributes.Hidden Or FileAttributes.System) Then returnVal = False

            File.Delete(testFile)
            Return returnVal
        End Function

        Function Test_Attributes2(rootTestFolder As String) As Boolean
            Dim returnVal As Boolean = True

            Dim testFile As String = Path.Combine(rootTestFolder, "setAttributeTest2.txt")
            File.Create(testFile).Dispose()

            WalkmanLib.SetAttribute(testFile, FileAttributes.Archive)

            WalkmanLib.AddAttribute(testFile, FileAttributes.Normal)
            If Not TestNumber("Attributes2.1", File.GetAttributes(testFile), FileAttributes.Archive) Then returnVal = False

            WalkmanLib.AddAttribute(testFile, FileAttributes.Hidden)
            If Not TestNumber("Attributes2.2", File.GetAttributes(testFile), FileAttributes.Archive Or FileAttributes.Hidden) Then returnVal = False

            WalkmanLib.AddAttribute(testFile, FileAttributes.System)
            If Not TestNumber("Attributes2.3", File.GetAttributes(testFile), FileAttributes.Archive Or FileAttributes.Hidden Or FileAttributes.System) Then returnVal = False

            File.Delete(testFile)
            Return returnVal
        End Function

        Function Test_Attributes3(rootTestFolder As String) As Boolean
            Dim returnVal As Boolean = True

            Dim testFile As String = Path.Combine(rootTestFolder, "setAttributeTest3.txt")
            File.Create(testFile).Dispose()

            WalkmanLib.SetAttribute(testFile, FileAttributes.Normal Or FileAttributes.ReadOnly Or FileAttributes.Hidden Or FileAttributes.System)

            WalkmanLib.RemoveAttribute(testFile, FileAttributes.Normal)
            If Not TestNumber("Attributes3.1", File.GetAttributes(testFile), FileAttributes.ReadOnly Or FileAttributes.Hidden Or FileAttributes.System) Then returnVal = False

            WalkmanLib.RemoveAttribute(testFile, FileAttributes.Hidden)
            If Not TestNumber("Attributes3.2", File.GetAttributes(testFile), FileAttributes.ReadOnly Or FileAttributes.System) Then returnVal = False

            WalkmanLib.RemoveAttribute(testFile, FileAttributes.ReadOnly Or FileAttributes.System)
            If Not TestNumber("Attributes3.3", File.GetAttributes(testFile), FileAttributes.Normal) Then returnVal = False

            File.Delete(testFile)
            Return returnVal
        End Function

        Function Test_Attributes4(rootTestFolder As String) As Boolean
            Dim returnVal As Boolean = True

            Dim testFile As String = Path.Combine(rootTestFolder, "setAttributeTest4.txt")
            File.Create(testFile).Dispose()

            WalkmanLib.SetAttribute(testFile, FileAttributes.System Or FileAttributes.Hidden)

            WalkmanLib.ChangeAttribute(testFile, FileAttributes.Hidden, False)
            If Not TestNumber("Attributes4.1", File.GetAttributes(testFile), FileAttributes.System) Then returnVal = False

            WalkmanLib.ChangeAttribute(testFile, FileAttributes.Hidden, True)
            If Not TestNumber("Attributes4.2", File.GetAttributes(testFile), FileAttributes.System Or FileAttributes.Hidden) Then returnVal = False

            WalkmanLib.ChangeAttribute(testFile, FileAttributes.Archive, True)
            If Not TestNumber("Attributes4.3", File.GetAttributes(testFile), FileAttributes.System Or FileAttributes.Hidden Or FileAttributes.Archive) Then returnVal = False

            File.Delete(testFile)
            Return returnVal
        End Function
    End Module
End Namespace
