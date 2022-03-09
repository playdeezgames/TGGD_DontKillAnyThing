Imports System.Runtime.CompilerServices

Public Enum CharacterType
    None
    Player
    Bandit
End Enum
Public Module CharacterTypeExtension
    Private ReadOnly characterTypeGenerator As New Dictionary(Of CharacterType, Integer) From
        {
            {CharacterType.None, 1},'18},
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
    <Extension()>
    Function AttackVerb(characterType As CharacterType) As String
        If characterType = CharacterType.Player Then
            Return "attack"
        Else
            Return "attacks"
        End If
    End Function
    <Extension()>
    Function HitVerb(characterType As CharacterType) As String
        If characterType = CharacterType.Player Then
            Return "hit"
        Else
            Return "hits"
        End If
    End Function
    <Extension()>
    Function MissVerb(characterType As CharacterType) As String
        If characterType = CharacterType.Player Then
            Return "miss"
        Else
            Return "misses"
        End If
    End Function
    <Extension()>
    Function RunVerb(characterType As CharacterType) As String
        If characterType = CharacterType.Player Then
            Return "run"
        Else
            Return "runs"
        End If
    End Function
    <Extension()>
    Function DetermineReaction(characterType As CharacterType) As AttackReaction
        Select Case characterType
            Case CharacterType.Player, CharacterType.None
                Return AttackReaction.DoNothing
            Case CharacterType.Bandit
                Return AttackReaction.RunAway
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
