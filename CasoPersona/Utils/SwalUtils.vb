Imports System.Web.UI

Public Class SwalUtils
    Public Shared Sub ShowAlert(page As Page, title As String, message As String, icon As String)
        Dim cleanMsg As String = message.Replace("'", "\'").Replace(vbCrLf, " ")
        Dim script As String = $"Swal.fire({{ title: '{title}', text: '{cleanMsg}', icon: '{icon}' }});"
        ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), script, True)
    End Sub

    Public Shared Sub ShowSuccess(page As Page, message As String)
        ShowAlert(page, "¡Éxito!", message, "success")
    End Sub

    Public Shared Sub ShowError(page As Page, message As String)
        ShowAlert(page, "Error", message, "error")
    End Sub

    Public Shared Sub ShowWarning(page As Page, message As String)
        ShowAlert(page, "Atención", message, "warning")
    End Sub
End Class