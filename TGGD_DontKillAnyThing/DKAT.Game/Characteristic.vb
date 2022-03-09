Imports DKAT.Data

Public Class Characteristic
    ReadOnly Property Id As Long
    ReadOnly Property CharacteristicType As CharacteristicType
    Sub New(characterId As Long, characteristicType As CharacteristicType)
        Me.Id = characterId
        Me.CharacteristicType = characteristicType
    End Sub
    ReadOnly Property Score As Long
        Get
            Dim base = CharacteristicData.ReadBase(Id, CharacteristicType)
            If Not base.HasValue Then
                base = RNG.FromDice(2, 6)
                CharacteristicData.Write(Id, CharacteristicType, base.Value, 0)
                Return base.Value
            End If
            Dim delta = CharacteristicData.ReadDelta(Id, CharacteristicType).Value
            Return base.Value + delta
        End Get
    End Property
    ReadOnly Property Modifier As Long
        Get
            Select Case Score
                Case 0, 1, 2
                    Return -2
                Case 3, 4, 5
                    Return -1
                Case 6, 7, 8
                    Return 0
                Case 9, 10, 11
                    Return 1
                Case 12, 13, 14
                    Return 2
                Case Else
                    Throw New NotImplementedException
            End Select
        End Get
    End Property
    Function Roll() As Long
        Return FromDice(2, 6) + Modifier
    End Function
End Class
