Imports System.Text
Imports DKAT.Game
Imports Terminal.Gui
Module InPlay
    Private window As Window = Nothing
    Private output As Label = Nothing
    Private actionMenu As ListView = Nothing
    Private Sub UpdateOutput()
        Dim builder As New StringBuilder
        Dim character As New PlayerCharacter()
        builder.AppendLine($"Character Id: {character.Id}")
        Dim location = character.Location
        builder.AppendLine($"Location Id: {location.Id}")
        output.Text = builder.ToString()
    End Sub
    Private Sub UpdateActions()
        actionMenu.SetSource(New List(Of String) From {"i am a menu item!"})
    End Sub
    Sub Run()
        window = New Window
        output = New Label With {.X = Pos.Center, .Y = Pos.Top(window), .Width = [Dim].Fill, .Height = [Dim].Percent(50)}
        actionMenu = New ListView With {.X = Pos.Center, .Y = Pos.Percent(50), .Width = [Dim].Fill, .Height = [Dim].Percent(50)}
        window.Add(output, actionMenu)
        UpdateOutput()
        UpdateActions()
        Application.Run(window)
        actionMenu = Nothing
        output = Nothing
        window = Nothing
    End Sub
End Module
