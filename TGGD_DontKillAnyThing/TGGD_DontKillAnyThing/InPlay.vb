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
    Private Sub UpdateActions()
        Dim actions As New List(Of InPlayAction)
        Dim character As New PlayerCharacter()
        Dim doors = character.Location.Doors
        For Each door In doors
            actions.Add(New InPlayAction($"Go {door.Direction.Name}", MoveDirection(door.Direction)))
        Next
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
