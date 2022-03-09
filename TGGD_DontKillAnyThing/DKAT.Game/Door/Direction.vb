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
    <Extension()>
    Function Opposite(direction As Direction) As Direction
        Select Case direction
            Case Direction.East
                Return Direction.West
            Case Direction.North
                Return Direction.South
            Case Direction.South
                Return Direction.North
            Case Direction.West
                Return Direction.East
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
