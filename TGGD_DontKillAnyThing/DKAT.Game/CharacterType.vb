Imports System.Runtime.CompilerServices

Public Enum CharacterType
    None
    Player
    Bandit
End Enum
Public Module CharacterTypeExtension
    Private ReadOnly characterTypeGenerator As New Dictionary(Of CharacterType, Integer) From
        {
            {CharacterType.None, 18},
            {CharacterType.Bandit, 1}
        }
    Function GenerateCharacterType() As CharacterType
        Return RNG.FromGenerator(characterTypeGenerator)
    End Function
    <Extension()>
    Function Name(characterType As CharacterType) As String
        Select Case characterType
            Case CharacterType.None
                Return ""
            Case CharacterType.Player
                Return "you"
            Case CharacterType.Bandit
                Return "bandit"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
