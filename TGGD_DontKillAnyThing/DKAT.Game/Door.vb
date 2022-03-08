Imports DKAT.Data

Public Class Door
    ReadOnly Property Id As Long
    Sub New(doorId As Long)
        Id = doorId
    End Sub
    ReadOnly Property Direction As Direction
        Get
            Return CType(DoorData.ReadDirection(Id).Value, Direction)
        End Get
    End Property
    ReadOnly Property ToLocation As Location
        Get
            Return New Location(DoorData.ReadToLocation(Id).Value)
        End Get
    End Property
End Class
