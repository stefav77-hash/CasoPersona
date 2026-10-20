Imports System.Data.SqlClient

Public Class Personas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            CargarGrid()
        End If
    End Sub
    Private ReadOnly _service As New PersonaService()
    Private Property IdPersonaEditar As Integer
        Get
            Return If(ViewState("IdPersonaEditar") IsNot Nothing, Convert.ToInt32(ViewState("IdPersonaEditar")), 0)
        End Get
        Set(value As Integer)
            ViewState("IdPersonaEditar") = value
        End Set
    End Property

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Try
            Dim edadValor As Integer = 0
            If Not Integer.TryParse(txtEdad.Text.Trim(), edadValor) Then
                SwalUtils.ShowWarning(Me, "Ingrese una edad numérica válida.")
                Return
            End If
            Dim p As New Persona With {
                .IdPersona = IdPersonaEditar,
                .Identificacion = txtIdentificacion.Text.Trim(),
                .Nombre = txtNombre.Text.Trim(),
                .Apellidos = txtApellidos.Text.Trim(),
                .Correo = txtCorreo.Text.Trim(),
                .Edad = edadValor
            }

            Dim esEdicion As Boolean = (Me.IdPersonaEditar > 0)
            _service.GuardarPersona(p)

            LimpiarFormulario()
            CargarGrid()
            SwalUtils.ShowSuccess(Me, If(esEdicion, "Registro actualizado. ", "Registro insertado"))
        Catch ex As Exception
            SwalUtils.ShowError(Me, ex.Message)
        End Try
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs)
        LimpiarFormulario()
    End Sub

    Protected Sub gvPersonas_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)

    End Sub
    Protected Sub gvPersonas_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Seleccionar" OrElse e.CommandName = "Eliminar" Then
            Try
                Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
                Dim id As Integer = Convert.ToInt32(gvPersonas.DataKeys(rowIndex).Value)



                If e.CommandName = "Seleccionar" Then
                    Dim p As Persona = _service.ObtenerPorIdentificacion(id)
                    If p IsNot Nothing Then
                        Me.IdPersonaEditar = p.IdPersona
                        txtIdentificacion.Text = p.Identificacion
                        txtNombre.Text = p.Nombre
                        txtApellidos.Text = p.Apellidos
                        txtCorreo.Text = p.Correo
                        txtEdad.Text = p.Edad.ToString()
                        lblTituloForm.Text = $"Editando Persona (ID: {p.IdPersona})"
                        btnGuardar.Text = "Actualizar"
                    End If
                ElseIf e.CommandName = "Eliminar" Then
                    _service.EliminarPersona(id)
                    If Me.IdPersonaEditar = id Then LimpiarFormulario()
                    CargarGrid()
                    gvPersonas.DataBind()
                    SwalUtils.ShowSuccess(Me, "Registro eliminado correctamente.")
                End If
            Catch ex As Exception
                SwalUtils.ShowError(Me, ex.Message)
            End Try
        End If
    End Sub
    Private Sub CargarGrid()
        Try
            gvPersonas.DataSource = _service.ListarPersonas()
            gvPersonas.DataBind()
        Catch ex As Exception
            SwalUtils.ShowError(Me, "Error al cargar datos: " & ex.Message)
        End Try
    End Sub


    Private Sub LimpiarFormulario()
        Me.IdPersonaEditar = 0
        txtIdentificacion.Text = ""
        txtNombre.Text = ""
        txtApellidos.Text = ""
        txtCorreo.Text = ""
        txtEdad.Text = ""
        lblTituloForm.Text = "Nueva Persona"
        btnGuardar.Text = "Guardar"
    End Sub
End Class