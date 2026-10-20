Imports System.Data
Imports System.Data.SqlClient

Public Class PersonaDB
    Private ReadOnly _db As New DbHelper()

    Public Function GetAll() As List(Of Persona)
        Dim lista As New List(Of Persona)()
        Dim query As String = "SELECT IdPersona, Identificacion, Nombre, Apellidos, Correo, Edad FROM Personas ORDER BY IdPersona DESC"
        Dim dt As DataTable = _db.ExecuteQuery(query)

        For Each row As DataRow In dt.Rows
            lista.Add(MapRowToPersona(row))
        Next
        Return lista
    End Function

    Public Function GetById(id As Integer) As Persona
        Dim query As String = "SELECT IdPersona, Identificacion, Nombre, Apellidos, Correo, Edad FROM Personas WHERE IdPersona = @IdPersona"
        Dim params As New List(Of SqlParameter) From {
            New SqlParameter("@IdPersona", id)
        }
        Dim dt As DataTable = _db.ExecuteQuery(query, params)
        If dt.Rows.Count > 0 Then
            Return MapRowToPersona(dt.Rows(0))
        End If
        Return Nothing
    End Function

    Public Function GetByIdentificacion(id As String) As Persona
        Dim query As String = "SELECT IdPersona, Identificacion, Nombre, Apellidos, Correo, Edad FROM Personas WHERE Identificacion = @Id"
        Dim params As New List(Of SqlParameter) From {
            New SqlParameter("@Id", id)
        }
        Dim dt As DataTable = _db.ExecuteQuery(query, params)
        If dt.Rows.Count > 0 Then
            Return MapRowToPersona(dt.Rows(0))
        End If
        Return Nothing
    End Function

    Public Function Insert(p As Persona) As Boolean
        Dim query As String = "INSERT INTO Personas (Identificacion, Nombre, Apellidos, Correo, Edad) VALUES (@Identificacion, @Nombre, @Apellidos, @Correo, @Edad)"
        Dim params As New List(Of SqlParameter) From {
            New SqlParameter("@Identificacion", p.Identificacion),
            New SqlParameter("@Nombre", p.Nombre),
            New SqlParameter("@Apellidos", p.Apellidos),
            New SqlParameter("@Correo", p.Correo),
            New SqlParameter("@Edad", p.Edad)
        }
        Return _db.ExecuteNonQuery(query, params) > 0
    End Function

    Public Function Update(p As Persona) As Boolean
        Dim query As String = "UPDATE Personas SET Identificacion = @Identificacion, Nombre = @Nombre, Apellidos = @Apellidos, Correo = @Correo, Edad = @Edad WHERE IdPersona = @IdPersona"
        Dim params As New List(Of SqlParameter) From {
            New SqlParameter("@IdPersona", p.IdPersona),
            New SqlParameter("@Identificacion", p.Identificacion),
            New SqlParameter("@Nombre", p.Nombre),
            New SqlParameter("@Apellidos", p.Apellidos),
            New SqlParameter("@Correo", p.Correo),
            New SqlParameter("@Edad", p.Edad)
        }
        Return _db.ExecuteNonQuery(query, params) > 0
    End Function

    Public Function Delete(id As String) As Boolean
        Dim query As String = "DELETE FROM Personas WHERE Identificacion = @Id"
        Dim params As New List(Of SqlParameter) From {
            New SqlParameter("@Id", id)
        }
        Return _db.ExecuteNonQuery(query, params) > 0
    End Function
    Private Function MapRowToPersona(row As DataRow) As Persona
        Return New Persona() With {
            .IdPersona = Convert.ToInt32(row("IdPersona")),
            .Identificacion = row("Identificacion").ToString(),
            .Nombre = row("Nombre").ToString(),
            .Apellidos = row("Apellidos").ToString(),
            .Correo = row("Correo").ToString(),
            .Edad = Convert.ToInt32(row("Edad"))
        }
    End Function
End Class