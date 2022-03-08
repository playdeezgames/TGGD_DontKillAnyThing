Imports Terminal.Gui

Module MainMenu
    Private Function ConfirmQuit() As Boolean
        If MessageBox.Query("Confirm Choice", "Are you sure you want to quit?", "No", "Yes") = 1 Then
            Application.RequestStop()
            Return True
        End If
        Return False
    End Function
    Sub Run()
        Dim done As Boolean = False
        While Not done
            Dim window As New Window()
            Dim menu As New ListView With
            {
                .X = Pos.Center,
                .Y = Pos.Center,
                .Width = [Dim].Percent(50),
                .Height = [Dim].Percent(50)
            }
            menu.SetSource(New List(Of String) From {"Embark!", "Quit"})
            AddHandler menu.OpenSelectedItem, Sub(args)
                                                  Select Case CStr(args.Value)
                                                      Case "Quit"
                                                          done = ConfirmQuit()
                                                  End Select
                                              End Sub
            window.Add(menu)
            Application.Run(window)
        End While
    End Sub
End Module
