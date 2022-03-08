Imports DKAT.Data
Public Module Game
    Private Function CreateSpawnLocation() As Location
        Dim locationId = LocationData.Create
        'TODO: create four other locations connected to spawn
        'TODO: set spawn location to populated
        Return New Location(locationId)
    End Function
    Private Sub CreatePlayerCharacter(location As Location)
        Dim characterId = CharacterData.Create(location.Id)
        PlayerData.SetCharacterId(characterId)
    End Sub
    Sub Start()
        Store.Reset()
        Dim location = CreateSpawnLocation()
        CreatePlayerCharacter(location)
    End Sub
    Sub Finish()
        Store.ShutDown()
    End Sub
End Module
