Imports DKAT.Data
Public Class Location
    ReadOnly Property Id As Long
    Private Sub Populate()
        If Not LocationData.ReadPopulated(Id) Then
            Dim directions As New HashSet(Of Direction) From {Direction.North, Direction.South, Direction.East, Direction.West}
            Dim existingDoors = DoorData.ReadForToLocation(Id)
            For Each existingDoor In existingDoors
                Dim direction = CType(DoorData.ReadDirection(existingDoor).Value, Direction)
                DoorData.Create(Id, direction.Opposite, DoorData.ReadFromLocation(existingDoor).Value)
            Next
            'TODO: make 0..N new doors. if this is the last unattached location, make at least one door
            LocationData.SetPopulated(Id, True)
        End If
    End Sub
    Sub New(locationId As Long)
        Id = locationId
        Populate()
    End Sub
    ReadOnly Property Doors As List(Of Door)
        Get
            Return DoorData.ReadForFromLocation(Id).Select(Function(doorId)
                                                               Return New Door(doorId)
                                                           End Function).ToList()
        End Get
    End Property
    Function GetDoor(direction As Direction) As Door
        Return Doors.SingleOrDefault(Function(door)
                                         Return door.Direction = direction
                                     End Function)
    End Function
End Class
