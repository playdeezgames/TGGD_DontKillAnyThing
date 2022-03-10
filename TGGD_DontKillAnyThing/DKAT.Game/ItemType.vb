Imports System.Runtime.CompilerServices

Public Enum ItemType
    None
    Rock
    OreNugget
    Stick
    Milkweed
    WildCarrot
    ClayLump
End Enum
Public Module ItemTypeExtensions
    <Extension()>
    Function Name(itemType As ItemType) As String
        Select Case itemType
            Case ItemType.None
                Return "nothing"
            Case ItemType.Rock
                Return "rock"
            Case ItemType.OreNugget
                Return "nugget of ore"
            Case ItemType.Stick
                Return "stick"
            Case ItemType.Milkweed
                Return "stalk of milkweed"
            Case ItemType.WildCarrot
                Return "wild carrot"
            Case itemType.ClayLump
                Return "lump of clay"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
