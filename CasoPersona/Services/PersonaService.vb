Imports Microsoft.Ajax.Utilities

Public Class PersonaService
    Private ReadOnly _personaDB As New PersonaDB()

    Public Function ListarPersonas() As List(Of Persona)
        Return _personaDB.GetAll()
    End Function

    Public Function ObtenerPorId(id As Integer) As Persona
        If id <= 0 Then
            Throw New ArgumentException("El ID de la persona no es válido.")
        End If
        Return _personaDB.GetById(id)
    End Function

    Public Function ObtenerPorIdentificacion(id As String) As Persona
        If id.IsNullOrWhiteSpace Then
            Throw New ArgumentException("El Identificación de la persona no es válido.")
        End If
        Return _personaDB.GetByIdentificacion(id)
    End Function
    Public Sub GuardarPersona(p As Persona)

        ValidarPersona(p)

        If p.IdPersona = 0 Then
            Dim existente As Persona = _personaDB.GetByIdentificacion(p.Identificacion)

            If existente IsNot Nothing AndAlso existente.IdPersona > 0 Then
                Throw New ArgumentException("la identificacion de la persona ya existe.")
            End If
            _personaDB.Insert(p)

        Else _personaDB.Update(p)

        End If
    End Sub

    Public Sub EliminarPersona(id As String)
        If id.IsNullOrWhiteSpace Then
            Throw New ArgumentException("Seleccione un registro válido para eliminar.")
        End If
        _personaDB.Delete(id)
    End Sub

    Private Sub ValidarPersona(p As Persona)
        If p Is Nothing Then Throw New ArgumentException("Datos requeridos.")
        If String.IsNullOrWhiteSpace(p.Identificacion) Then Throw New ArgumentException("La identificación es obligatoria.")
        If String.IsNullOrWhiteSpace(p.Nombre) Then Throw New ArgumentException("El nombre es obligatorio.")
        If String.IsNullOrWhiteSpace(p.Apellidos) Then Throw New ArgumentException("Los apellidos son obligatorios.")
        If String.IsNullOrWhiteSpace(p.Correo) OrElse Not p.Correo.Contains("@") Then Throw New ArgumentException("Ingrese un correo válido.")
        If p.Edad < 0 OrElse p.Edad > 120 Then Throw New ArgumentException("La edad debe estar entre 0 y 120 años.")
    End Sub
End Class
