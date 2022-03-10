Imports System.Text
Imports DKAT.Game
Imports Terminal.Gui
Module InPlay
    Private window As Window = Nothing
    Private output As Label = Nothing
    Private actionMenu As ListView = Nothing
    Private Sub UpdateOutput()
        output.Clear()
        Dim builder As New StringBuilder
        Dim character As New PlayerCharacter()
        builder.AppendLine($"Yer alive, and you ain't killed anything yet!")
        Dim location = character.Location
        builder.AppendLine($"Terrain: {location.LocationType.Name}")
        Dim npcs = location.NonplayerCharacters
        If npcs.Any Then
            builder.Append("Npcs: ")
            builder.AppendJoin(", ", npcs.Select(Function(npc)
                                                     Return npc.CharacterType.Name
                                                 End Function))
            builder.AppendLine()
        End If
        Dim doors = location.Doors
        builder.AppendLine($"Exits:")
        builder.AppendJoin(", ", doors.Select(Function(door)
                                                  Return $"{door.Direction.Name}"
                                              End Function))
        output.Text = builder.ToString()
    End Sub
    Private Function MoveDirection(direction As Direction) As Action
        Return Sub()
                   Dim character As New PlayerCharacter
                   character.MoveDirection(direction)
                   UpdateOutput()
                   UpdateActions()
               End Sub
    End Function
    Function AttackCharacter(characterId As Long) As Action
        Return Sub()
                   Dim character As New PlayerCharacter
                   Dim npc As New Character(characterId)
                   Dim attackResult = character.Attack(npc)
                   If character.HasKilled Then
                       MessageBox.ErrorQuery("Game Over!", attackResult, "Ok")
                       Application.RequestStop()
                   Else
                       MessageBox.ErrorQuery("HUZZAH!", attackResult, "Ok")
                       UpdateOutput()
                       UpdateActions()
                   End If
               End Sub
    End Function
    Private Sub ShowCharacteristics()
        Dim builder As New StringBuilder
        Dim character As New PlayerCharacter
        For Each characteristicType In AllCharacteristicTypes
            builder.AppendLine($"{characteristicType.Abbreviation}: {character.GetCharacteristic(characteristicType).Score}")
        Next
        MessageBox.Query("Characteristics:", builder.ToString(), "Ok")
    End Sub
    Private Sub Forage()
        Dim character As New PlayerCharacter
        MessageBox.Query("Forage Result:", character.Forage, "Ok")
    End Sub
    Private Sub UpdateActions()
        Dim actions As New List(Of InPlayAction)
        Dim character As New PlayerCharacter()
        Dim npcs = character.Location.NonplayerCharacters
        For Each npc In npcs
            actions.Add(New InPlayAction($"Attack {npc.CharacterType.Name}", AttackCharacter(npc.Id)))
        Next
        Dim doors = character.Location.Doors
        For Each door In doors
            actions.Add(New InPlayAction($"Go {door.Direction.Name}", MoveDirection(door.Direction)))
        Next
        If Not npcs.Any Then
            actions.Add(New InPlayAction("Forage", AddressOf Forage))
        End If
        actions.Add(New InPlayAction("Characteristics...", AddressOf ShowCharacteristics))
        actionMenu.SetSource(actions)
    End Sub
    Private Sub HandleAction(args As ListViewItemEventArgs)
        Dim inPlayAction = CType(args.Value, InPlayAction)
        inPlayAction.Action.Invoke()
    End Sub
    Sub Run()
        window = New Window
        output = New Label With {.X = Pos.Center, .Y = Pos.Top(window), .Width = [Dim].Fill, .Height = [Dim].Percent(50)}
        actionMenu = New ListView With {.X = Pos.Center, .Y = Pos.Percent(50), .Width = [Dim].Fill, .Height = [Dim].Percent(50)}
        AddHandler actionMenu.OpenSelectedItem, AddressOf HandleAction
        window.Add(output, actionMenu)
        UpdateOutput()
        UpdateActions()
        Application.Run(window)
        actionMenu = Nothing
        output = Nothing
        window = Nothing
    End Sub
End Module
