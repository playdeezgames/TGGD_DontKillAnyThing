Imports DKAT.Data
Public Module Game
    Private Sub SpawnLocation(fromLocationId As Long, direction As Direction)
        Dim toLocationId = LocationData.Create
        DoorData.Create(fromLocationId, direction, toLocationId)
    End Sub
    Private Function CreateStartingLocation() As Location
        Dim locationId = LocationData.Create
        SpawnLocation(locationId, Direction.North)
        SpawnLocation(locationId, Direction.East)
        SpawnLocation(locationId, Direction.South)
        SpawnLocation(locationId, Direction.West)
        LocationData.SetPopulated(locationId, True)
        Return New Location(locationId)
    End Function
    Private Sub CreatePlayerCharacter(location As Location)
        Dim characterId = CharacterData.Create(location.Id)
        PlayerData.SetCharacterId(characterId)
    End Sub
    Sub Start()
        Store.Reset()
        Dim location = CreateStartingLocation()
        CreatePlayerCharacter(location)
    End Sub
    Sub Finish()
        Store.ShutDown()
    End Sub
End Module
