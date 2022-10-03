<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Sponte.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css"/>
    <link href="Css/Login.css" rel="stylesheet" />
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
    <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <title></title>
</head>
<body>
<div class="wrapper fadeInDown">
        <div id="formContent">
            <!-- Tabs Titles -->

            <!-- Icon -->
            <div class="fadeIn first">
                <img src="https://www.sponte.com.br/wp-content/uploads/2020/02/LogoSponteBranca.svg" id="icon" alt="User Icon">                
            </div>

            <!-- Login Form -->
            <form runat="server">
                <div class="form-row">
                    <div class="form-group col-md-12">
                        <input runat="server" type="text" id="inputlogin" class="fadeIn second" name="login" placeholder="Usuário"/>
                        <input runat="server" type="password" id="password" class="fadeIn third" name="login" placeholder="Senha"/>
                        <%--<input runat="server" type="submit" class="fadeIn fourth" value="Entrar" name="btnEntrar"/>--%>                        
                        <asp:Button runat="server" ID="btnEntrar" CssClass="btn botaologin" OnClick="btnEntrar_Click" Text="Entrar" />
                    </div>
                </div>
                <div class="form-group col-md-12">
                    <label runat="server" id="lblerro" style="color: red; font-weight: bold;"></label>
                </div>
            </form>

            <!-- Remind Passowrd -->
<%--            <div id="formFooter">
                <a class="underlineHover" href="#">Forgot Password?</a>
            </div>--%>

        </div>
    </div>
</body>
</html>
