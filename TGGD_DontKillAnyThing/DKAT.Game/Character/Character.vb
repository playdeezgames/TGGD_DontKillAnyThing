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
        If GetCharacteristic(CharacteristicType.Strength).Roll() >= 0 Then
            'TODO: according to weapon
            Return RNG.FromDice(1, 6)
        End If
        Return 0
    End Function
    Private Sub TakeOneDamage()
        Dim characteristicGenerator As New Dictionary(Of CharacteristicType, Integer) From
            {
                {CharacteristicType.Dexterity, CInt(GetCharacteristic(CharacteristicType.Dexterity).Score)},
                {CharacteristicType.Constitution, CInt(GetCharacteristic(CharacteristicType.Constitution).Score)},
                {CharacteristicType.Strength, CInt(GetCharacteristic(CharacteristicType.Strength).Score)}
            }
        Dim damageCharacteristic = RNG.FromGenerator(characteristicGenerator)
        GetCharacteristic(damageCharacteristic).ChangeDelta(-1)
    End Sub
    Sub TakeDamage(damage As Long)
        While damage > 0 AndAlso Not IsDead
            TakeOneDamage()
            damage -= 1
        End While
    End Sub
    ReadOnly Property IsDead As Boolean
        Get
            Return GetCharacteristic(CharacteristicType.Constitution).Score <= 0 AndAlso GetCharacteristic(CharacteristicType.Dexterity).Score <= 0 AndAlso GetCharacteristic(CharacteristicType.Strength).Score <= 0
        End Get
    End Property
    Private Function GetCounter(counterType As CounterType) As Long
        Dim value = CounterData.Read(Id, counterType)
        If value.HasValue Then
            Return value.Value
        End If
        Return 0
    End Function
    Private Sub ChangeCounter(counterType As CounterType, change As Long)
        CounterData.Write(Id, counterType, GetCounter(counterType) + change)
    End Sub
    Sub AddKill()
        ChangeCounter(CounterType.Kills, 1)
    End Sub
    Sub Destroy()
        CharacterData.Clear(Id)
    End Sub
    Function Attack(enemy As Character) As String
        Dim builder As New StringBuilder
        builder.AppendLine($"{CharacterType.Name} {CharacterType.AttackVerb} {enemy.CharacterType.Name}")
        Dim attackRoll = RollAttack()
        Dim defendRoll = enemy.RollDefend()
        If attackRoll >= 0 AndAlso attackRoll > defendRoll Then
            builder.AppendLine($"{CharacterType.Name} {CharacterType.HitVerb} {enemy.CharacterType.Name}")
            Dim damage = RollDamage()
            enemy.TakeDamage(damage)
            If enemy.IsDead Then
                AddKill()
                builder.AppendLine($"{CharacterType.Name} {CharacterType.KillVerb} {enemy.CharacterType.Name}")
                enemy.Destroy()
            End If
        Else
            builder.AppendLine($"{CharacterType.Name} {CharacterType.MissVerb} {enemy.CharacterType.Name}")
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
