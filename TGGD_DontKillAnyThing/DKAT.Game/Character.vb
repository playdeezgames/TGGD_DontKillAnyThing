﻿Imports DKAT.Data

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
    Function MoveDirection(direction As Direction) As Boolean
        Dim door = Location.GetDoor(direction)
        If door IsNot Nothing Then
            Location = door.ToLocation
            Return True
        End If
        Return False
    End Function
End Class
