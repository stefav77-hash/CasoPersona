Public Class Persona
    Public Property IdPersona As Integer
    Public Property Identificacion As String
    Public Property Nombre As String
    Public Property Apellidos As String
    Public Property Correo As String
    Public Property Edad As Integer

    Public Sub New()
    End Sub

    Public Sub New(idPersona As Integer, identificacion As String, nombre As String, apellidos As String, correo As String, edad As Integer)
        Me.IdPersona = idPersona
        Me.Identificacion = identificacion
        Me.Nombre = nombre
        Me.Apellidos = apellidos
        Me.Correo = correo
        Me.Edad = edad
    End Sub
End Class
