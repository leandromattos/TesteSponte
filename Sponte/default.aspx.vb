﻿Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.IsInRole("leandro") Then
            Response.Redirect("/Paginas/Index.aspx", True)
        End If

    End Sub

End Class