Public Module DoorData
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Doors]
            (
                [DoorId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [FromLocationId] INT NOT NULL,
                [Direction] INT NOT NULL,
                [ToLocationId] INT NOT NULL,
                UNIQUE([FromLocationId],[Direction]),
                UNIQUE([ToLocationId],[Direction]),
                FOREIGN KEY ([FromLocationId]) REFERENCES [Locations]([LocationId]),
                FOREIGN KEY ([ToLocationId]) REFERENCES [Locations]([LocationId])
            );")
    End Sub
    Function ReadForFromLocation(fromLocationId As Long) As List(Of Long)
        Initialize()
        Using command = CreateCommand(
            "SELECT [DoorId] FROM [Doors] WHERE [FromLocationId]=@LocationId",
            MakeParameter("@LocationId", fromLocationId))
            Using reader = command.ExecuteReader
                Dim result As New List(Of Long)
                While reader.Read
                    result.Add(CLng(reader("DoorId")))
                End While
                Return result
            End Using
        End Using
    End Function
    Function Create(fromLocationId As Long, direction As Long, toLocationId As Long) As Long
        Initialize()
        Using command = CreateCommand(
            "INSERT INTO [Doors]([FromLocationId],[Direction],[ToLocationId]) VALUES(@FromLocationId,@Direction,@ToLocationId);",
            MakeParameter("@FromLocationId", fromLocationId),
            MakeParameter("@Direction", direction),
            MakeParameter("@ToLocationId", toLocationId))
            command.ExecuteNonQuery()
            Return LastInsertRowId
        End Using
    End Function
    Function ReadDirection(doorId As Long) As Long?
        Initialize()
        Using command = CreateCommand("SELECT [Direction] FROM [Doors] WHERE [DoorId]=@DoorId;", MakeParameter("@DoorId", doorId))
            Return ExecuteScalar(Of Long)(command)
        End Using
    End Function
End Module
