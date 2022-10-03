Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnEntrar_Click(sender As Object, e As EventArgs)
        lblerro.InnerText = ""

        'aqui simplifiquei, mas aqui que fariamos a consulta no BD para buscar o usuário
        'Parametros(0) = New SqlParameter() With {.ParameterName = "@nome", .Value = inputlogin.Value, .MySqlDbType = MySqlDbType.VarChar, .Size = 50}
        'Parametros(1) = New SqlParameter() With {.ParameterName = "@senha", .Value = passwordEncryptSHA(password.Value), .MySqlDbType = MySqlDbType.VarChar, .Size = 50}

        If inputlogin.Value = "leandro" And password.Value = "123" Then
            FormsAuthentication.Initialize()
            'Definimos quanto tempo o usuário irá permanecer logado após deixar o site sem efetuar o logout
            Dim fat As FormsAuthenticationTicket = New FormsAuthenticationTicket(1, inputlogin.Value, DateTime.Now, DateTime.Now.AddMinutes(30), False, inputlogin.Value, FormsAuthentication.FormsCookiePath)
            'Criamos o cookie com os dados do usuário/seção
            Response.Cookies.Add(New HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)))
            Response.Redirect(FormsAuthentication.GetRedirectUrl(inputlogin.Value, False))
        Else
            lblerro.InnerText = "Usuário ou senha inválido!!"
        End If

    End Sub

End Class