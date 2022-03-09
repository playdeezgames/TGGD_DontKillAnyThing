Public Class InPlayAction
    ReadOnly Property Text As String
    ReadOnly Property Action As Action
    Sub New(text As String, action As Action)
        Me.Text = text
        Me.Action = action
    End Sub
    Public Overrides Function ToString() As String
        Return Text
    End Function
End Class
