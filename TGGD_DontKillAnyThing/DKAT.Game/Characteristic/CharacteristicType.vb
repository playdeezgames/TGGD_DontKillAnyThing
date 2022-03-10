Imports System.Runtime.CompilerServices

Public Enum CharacteristicType
    Strength
    Dexterity
    Constitution
    'Intelligence
    'Wisdom
    'Charisma
End Enum
Public Module CharacteristicTypeExtensions
    <Extension()>
    Function Name(characteristicType As CharacteristicType) As String
        Select Case characteristicType
            Case CharacteristicType.Constitution
                Return "Constitution"
            Case CharacteristicType.Dexterity
                Return "Dexterity"
            Case CharacteristicType.Strength
                Return "Strength"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension()>
    Function Abbreviation(characteristicType As CharacteristicType) As String
        Select Case characteristicType
            Case CharacteristicType.Constitution
                Return "CON"
            Case CharacteristicType.Dexterity
                Return "DEX"
            Case CharacteristicType.Strength
                Return "STR"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    Public ReadOnly AllCharacteristicTypes As New List(Of CharacteristicType) From
        {
            CharacteristicType.Strength,
            CharacteristicType.Dexterity,
            CharacteristicType.Constitution
        }
End Module
