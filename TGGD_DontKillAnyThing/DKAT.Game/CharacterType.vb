Imports System.Runtime.CompilerServices

Public Enum CharacterType
    Player
End Enum
Public Module CharacterTypeExtension
    <Extension()>
    Function Name(characterType As CharacterType) As String
        Select Case characterType
            Case CharacterType.Player
                Return "you"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
