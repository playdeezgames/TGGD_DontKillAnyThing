Imports System.Runtime.CompilerServices

Public Enum LocationType As Long
    Forest
    Pasture
    Field
    RiverBank
    Mountain
    Desert
End Enum
Public Module LocationTypeExtensions
    Private ReadOnly locationTypeGenerator As New Dictionary(Of LocationType, Integer) From
        {
            {LocationType.Pasture, 4},
            {LocationType.Forest, 4},
            {LocationType.Field, 4},
            {LocationType.RiverBank, 3},
            {LocationType.Mountain, 3},
            {LocationType.Desert, 1}
        }
    Function GenerateLocationType() As LocationType
        Return RNG.FromGenerator(locationTypeGenerator)
    End Function
    <Extension()>
    Function Name(locationType As LocationType) As String
        Select Case locationType
            Case LocationType.Desert
                Return "desert"
            Case LocationType.Field
                Return "field"
            Case LocationType.Forest
                Return "forest"
            Case LocationType.Mountain
                Return "mountain"
            Case LocationType.Pasture
                Return "pasture"
            Case LocationType.RiverBank
                Return "river bank"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
