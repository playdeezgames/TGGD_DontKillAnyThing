Imports System.Runtime.CompilerServices

Public Enum ItemType
    None
End Enum
Public Module ItemTypeExtensions
    <Extension()>
    Function Name(itemType As ItemType) As String
        Select Case itemType
            Case ItemType.None
                Return "nothing"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
