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
            Dim characterType = GenerateCharacterType()
            If characterType <> CharacterType.None Then
                CharacterData.Create(Id, characterType)
            End If
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
    ReadOnly Property NonplayerCharacters As List(Of Character)
        Get
            Return CharacterData.
                ReadForLocation(Id).
                Select(Function(characterId)
                           Return New Character(characterId)
                       End Function).
                       Where(Function(character)
                                 Return character.CharacterType <> CharacterType.Player
                             End Function).ToList
        End Get
    End Property
    Private forageGenerators As New Dictionary(Of LocationType, Dictionary(Of ItemType, Integer)) From
        {
            {
                LocationType.Desert,
                New Dictionary(Of ItemType, Integer) From
                {
                    {ItemType.None, 1}
                }
            },
            {
                LocationType.Field,
                New Dictionary(Of ItemType, Integer) From
                {
                    {ItemType.None, 1}
                }
            },
            {
                LocationType.Forest,
                New Dictionary(Of ItemType, Integer) From
                {
                    {ItemType.None, 1}
                }
            },
            {
                LocationType.Mountain,
                New Dictionary(Of ItemType, Integer) From
                {
                    {ItemType.None, 1}
                }
            },
            {
                LocationType.Pasture,
                New Dictionary(Of ItemType, Integer) From
                {
                    {ItemType.None, 1}
                }
            },
            {
                LocationType.RiverBank,
                New Dictionary(Of ItemType, Integer) From
                {
                    {ItemType.None, 1}
                }
            }
        }
    Function GenerateForage() As ItemType?
        Dim generator = forageGenerators(LocationType)
        Dim result = RNG.FromGenerator(generator)
        If result = ItemType.None Then
            Return Nothing
        End If
        Return result
    End Function
End Class
