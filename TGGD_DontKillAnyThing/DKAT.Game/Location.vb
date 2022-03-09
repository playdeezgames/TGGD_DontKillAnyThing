Imports DKAT.Data
Public Class Location
    ReadOnly Property Id As Long
    Private Sub Populate()
        If Not LocationData.ReadPopulated(Id) Then
            Dim directions As New HashSet(Of Direction) From {Direction.North, Direction.South, Direction.East, Direction.West}
            Dim existingDoors = DoorData.ReadForToLocation(Id)
            For Each existingDoor In existingDoors
                Dim direction = CType(DoorData.ReadDirection(existingDoor).Value, Direction)
                directions.Remove(direction.Opposite)
                DoorData.Create(Id, direction.Opposite, DoorData.ReadFromLocation(existingDoor).Value)
            Next
            Dim doorCount = RNG.FromRange(0, directions.Count)
            If doorCount = 0 AndAlso LocationData.UnpopulatedCount = 1 Then
                doorCount = 1
            End If
            While doorCount > 0
                Dim locationId = LocationData.Create(GenerateLocationType)
                Dim direction = RNG.FromGenerator(directions)
                directions.Remove(direction)
                Dim doorId = DoorData.Create(Id, direction, locationId)
                doorCount -= 1
            End While
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
    ReadOnly Property LocationType As LocationType
        Get
            Return CType(LocationData.ReadLocationType(Id).Value, LocationType)
        End Get
    End Property
    Function GetDoor(direction As Direction) As Door
        Return Doors.SingleOrDefault(Function(door)
                                         Return door.Direction = direction
                                     End Function)
    End Function
End Class
