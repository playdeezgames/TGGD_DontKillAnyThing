Public Module LocationData
    Friend Sub Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Locations]
            (
                [LocationId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [Populated] INT NOT NULL
            );")
    End Sub
    ReadOnly Property UnpopulatedCount As Long
        Get
            Initialize()
            Using command = CreateCommand("SELECT COUNT(1) FROM [Locations] WHERE [Populated]=0;")
                Return CLng(command.ExecuteScalar)
            End Using
        End Get
    End Property
    Function Create() As Long
        Initialize()
        ExecuteNonQuery("INSERT INTO [Locations]([Populated]) VALUES(0);")
        Return LastInsertRowId
    End Function
    Sub SetPopulated(locationId As Long, populated As Boolean)
        Initialize()
        Using command = CreateCommand(
            "UPDATE [Locations] SET [Populated]=@Populated WHERE [LocationId]=@LocationId;",
            MakeParameter("@LocationId", locationId),
            MakeParameter("@Populated", populated))
            command.ExecuteNonQuery()
        End Using
    End Sub
    Function ReadPopulated(locationId As Long) As Boolean?
        Initialize()
        Using command = CreateCommand(
            "SELECT [Populated] FROM [Locations] WHERE [LocationId]=@LocationId;",
            MakeParameter("@LocationId", locationId))
            Dim result = command.ExecuteScalar
            If result IsNot Nothing Then
                Return CBool(result)
            End If
            Return Nothing
        End Using
    End Function
End Module
