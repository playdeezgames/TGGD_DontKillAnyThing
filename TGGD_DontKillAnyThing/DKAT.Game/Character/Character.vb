Imports System.Text
Imports DKAT.Data

Public Class Character
    ReadOnly Property Id As Long
    Sub New(characterId As Long)
        Id = characterId
    End Sub
    Property Location As Location
        Get
            Return New Location(CharacterData.ReadLocation(Id).Value)
        End Get
        Set(value As Location)
            CharacterData.WriteLocation(Id, value.Id)
        End Set
    End Property
    ReadOnly Property CharacterType As CharacterType
        Get
            Dim result = CharacterData.ReadCharacterType(Id)
            If result.HasValue Then
                Return CType(result.Value, CharacterType)
            End If
            Return CharacterType.None
        End Get
    End Property
    Function MoveDirection(direction As Direction) As Boolean
        Dim door = Location.GetDoor(direction)
        If door IsNot Nothing Then
            Location = door.ToLocation
            Return True
        End If
        Return False
    End Function
    Function RollAttack() As Long
        'TODO: factor in weapon
        Return GetCharacteristic(CharacteristicType.Dexterity).Roll()
    End Function
    Function GetCharacteristic(characteristicType As CharacteristicType) As Characteristic
        Return New Characteristic(Id, characteristicType)
    End Function
    Function RollDefend() As Long
        'TODO: factor in armor
        Return GetCharacteristic(CharacteristicType.Dexterity).Roll()
    End Function
    Function RollDamage() As Long
        Return Math.Max(0, GetCharacteristic(CharacteristicType.Strength).Roll())
    End Function
    Sub TakeDamage(damage As Long)
        Throw New NotImplementedException
    End Sub
    ReadOnly Property IsDead As Boolean
        Get
            Throw New NotImplementedException
        End Get
    End Property
    Sub AddKill()
        Throw New NotImplementedException
    End Sub
    Function Attack(enemy As Character) As String
        Dim builder As New StringBuilder
        builder.AppendLine($"{Me.CharacterType.Name} {Me.CharacterType.AttackVerb} {enemy.CharacterType.Name}")
        Dim attackRoll = Me.RollAttack()
        Dim defendRoll = enemy.RollDefend()
        If attackRoll > defendRoll Then
            builder.AppendLine($"{Me.CharacterType.Name} {Me.CharacterType.HitVerb} {enemy.CharacterType.Name}")
            Dim damage = RollDamage()
            enemy.TakeDamage(damage)
            If enemy.IsDead Then
                Me.AddKill()
                builder.AppendLine($"{Me.CharacterType.Name} {Me.CharacterType.KillVerb} {enemy.CharacterType.Name}")
            End If
        Else
            builder.AppendLine($"{Me.CharacterType.Name} {Me.CharacterType.MissVerb} {enemy.CharacterType.Name}")
        End If
        Select Case enemy.CharacterType.DetermineReaction
            Case AttackReaction.RunAway
                builder.AppendLine($"{enemy.CharacterType.Name} {enemy.CharacterType.RunVerb}")
                enemy.RunAway()
        End Select
        Return builder.ToString
    End Function
    Sub RunAway()
        Dim directions = New HashSet(Of Direction)(Location.Doors.Select(Function(door)
                                                                             Return door.Direction
                                                                         End Function))
        MoveDirection(RNG.FromGenerator(directions))
    End Sub
End Class
