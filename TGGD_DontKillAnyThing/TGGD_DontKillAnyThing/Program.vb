Imports Terminal.Gui

Module Program
    Sub Main(args As String())
        Console.Title = "A Game in VB.NET About Not Killing Stuff"
        Application.Init()
        MessageBox.Query("", "
A Game in VB.NET About Not Killing Stuff

A Presentation of TheGrumpyGameDev

", "GO!")
        MainMenu.Run()
    End Sub
End Module
