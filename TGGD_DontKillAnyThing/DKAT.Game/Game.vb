Imports System.Text
Imports DKAT.Data
Public Module Game
    Private Sub SpawnLocation(fromLocationId As Long, direction As Direction)
        Dim toLocationId = LocationData.Create(GenerateLocationType)
        DoorData.Create(fromLocationId, direction, toLocationId)
    End Sub
    Private Function CreateStartingLocation() As Location
        Dim locationId = LocationData.Create(GenerateLocationType)
        SpawnLocation(locationId, Direction.North)
        SpawnLocation(locationId, Direction.East)
        SpawnLocation(locationId, Direction.South)
        SpawnLocation(locationId, Direction.West)
        LocationData.SetPopulated(locationId, True)
        Return New Location(locationId)
    End Function
    Private Sub CreatePlayerCharacter(location As Location)
        Dim characterId = CharacterData.Create(location.Id, CharacterType.Player)
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
    Public Event PlaySfx As Action(Of Sfx)
    Sub Play(sfx As Sfx)
        RaiseEvent PlaySfx(sfx)
    End Sub
    Private Sub FinishBanditTurn(character As Character, npc As Character, builder As StringBuilder)
        If Not character.Inventory.IsEmpty Then
            Dim item = RNG.FromList(character.Inventory.Items)
            npc.Inventory.Add(item)
            builder.AppendLine($"{npc.CharacterType.Name} {npc.CharacterType.StealVerb} {item.ItemType.Name} from {character.CharacterType.Name}")
        End If
    End Sub
    Private Sub FinishTurn(character As Character, npc As Character, builder As StringBuilder)
        Select Case npc.CharacterType
            Case CharacterType.Bandit
                FinishBanditTurn(character, npc, builder)
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub
    Function FinishTurn() As String
        Dim builder As New StringBuilder
        Dim character As New PlayerCharacter
        Dim npcs = character.Location.NonplayerCharacters
        For Each npc In npcs
            FinishTurn(character, npc, builder)
        Next
        Return builder.ToString()
    End Function
End Module
