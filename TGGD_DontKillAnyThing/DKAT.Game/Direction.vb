Imports System.Runtime.CompilerServices

Public Enum Direction As Long
    North
    East
    South
    West
End Enum
Public Module DirectionExtensionMethods
    <Extension()>
    Function Name(direction As Direction) As String
        Select Case direction
            Case Direction.East
                Return "east"
            Case Direction.North
                Return "north"
            Case Direction.South
                Return "south"
            Case Direction.West
                Return "west"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
