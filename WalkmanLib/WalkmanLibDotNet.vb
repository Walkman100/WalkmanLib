Imports System
Imports System.IO
Public Partial Class WalkmanLib
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="path"></param>
    Shared Sub TakeOwnership(path As String)
        
    End Sub
    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Shared Function GetFolderIconPath(path As String) As String
        
    End Function
    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="path"></param>
    ''' <param name="fileAttributes"></param>
    ''' <returns></returns>
    Shared Function SetAttribute(path As String, fileAttributes As FileAttributes) As Boolean
        
    End Function
    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="path"></param>
    ''' <param name="fileAttributes"></param>
    ''' <returns></returns>
    Shared Function AddAttribute(path As String, fileAttributes As FileAttributes) As Boolean
        
    End Function
    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="path"></param>
    ''' <param name="fileAttributes"></param>
    ''' <returns></returns>
    Shared Function RemoveAttrubute(path As String, fileAttributes As FileAttributes) As Boolean
        
    End Function
    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="path"></param>
    ''' <param name="arguments"></param>
    Shared Sub RunAsAdmin(path As String, Optional arguments As String = Nothing)
        
    End Sub
    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="path"></param>
    Shared Sub OpenWith(path As String)
        
    End Sub
    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Shared Function IsAdmin() As Boolean
        
    End Function
    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="text"></param>
    ''' <param name="successMessage"></param>
    ''' <param name="showErrors"></param>
    ''' <returns></returns>
    Shared Function SafeSetText(text As String, Optional successMessage As String = Nothing, Optional showErrors As Boolean = True) As Boolean
        
    End Function
    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ex"></param>
    Shared Sub ErrorDialog(ex As Exception)
        
    End Sub
End Class
