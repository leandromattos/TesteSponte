Imports System.Security.Principal
Imports System.Web.SessionState

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' É acionado quando o aplicativo é iniciado
        Ambiente = enumAmbiente.Dev
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' É acionado quando a sessão é iniciada
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' É acionado no início de cada solicitação
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        Dim booAut As Boolean = False

        ' É acionado ao tentar autenticar o uso
        If Not (HttpContext.Current.User Is Nothing) Then
            'Usando as classes de identidade vamos obter o perfil e carregar na aplicação   
            If HttpContext.Current.User.Identity.IsAuthenticated Then
                If TypeOf HttpContext.Current.User.Identity Is FormsIdentity Then
                    Dim fi As FormsIdentity = CType(HttpContext.Current.User.Identity, FormsIdentity)
                    'obtém o ticket de autenticação
                    Dim fat As FormsAuthenticationTicket = fi.Ticket
                    Dim astrPerfis As String() = fat.UserData.Split("|"c)
                    HttpContext.Current.User = New GenericPrincipal(fi, astrPerfis)
                    booAut = True
                End If
            End If
        End If

        'Se a chamada não veio da página login e não foi autenticado, direciona para a página login
        'Trecho responsável para validar se o diretório foi acessado sem autenticação
        If Not sender.request.filepath.ToString.Contains("Login") And booAut = False Then
            Response.Redirect("../Login.aspx", True)
        End If

    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' É acionado quando ocorre um erro
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' É acionado quando a sessão termina
        Session.Abandon()
        'remove o tickect de autenticação
        Try
            FormsAuthentication.SignOut()
        Catch ex As Exception

        End Try

    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' É acionado quando o aplicativo termina
        Session.Abandon()
        'remove o tickect de autenticação
        'remove o tickect de autenticação
        Try
            FormsAuthentication.SignOut()
        Catch ex As Exception

        End Try
    End Sub

End Class