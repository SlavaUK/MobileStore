<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformationPage.aspx.cs" Inherits="MobileStore.Pages.InformationPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/Site.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title>Информация о ресурсе</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="text-align: center">
            <h1>Информация о ресурсе</h1>
            <br />
            <div class="text-block" style="margin-left: auto; margin-right: auto">
                <h4>Описание функций</h4>
                <p>
                    Благодаря данному программному продукту можно автоматизировать учёт товаров на складе, ускорить и улучшить работу отдела кадров и отдела продаж. У программного продукта удобный и понятный интерфейс, в котором легко разобраться.
                    <br />
                    Программный продукт обладает системой авторизации, регистрации и разделением функций и интерфейса по ролям, функциями для хранения, манипулирования и управления большим объёмом данных, а также функциями для создания документов.
                </p>
            </div>
            <br />
            <div class="text-block" style="margin-left: auto; margin-right: auto">
                <h4>Информация о магазине</h4>
                <p>
                    Магазин сотовой связи осуществляет закупку и продажу мелкой электроники, такой как плееры, 
                    наушники, телефоны и аксессуары к ним. Основными целями организации является получение прибыли 
                    от проданных товаров и удовлетворение потребностей клиентов.
                </p>
            </div>
            <br />
            <div class="text-block" style="margin-left: auto; margin-right: auto">
                <h4>Установочный пакет</h4>
                <p>Установить программный продукт можно по данной ссылке: <a href="https://github.com/SlavaUK/MobileStore">Установочный пакет</a></p>
            </div>
            <br />
            <div class="text-block" style="margin-left: auto; margin-right: auto; background-color: #eceaea; padding: 5px">
                <h4>Галерея</h4>
                <br />
                <div class="row">
                    <div class="column">
                        <img src="../Content/img/Authoriz.png" style="width: 100%" onclick="myFunction(this);">
                    </div>
                    <div class="column">
                        <img src="../Content/img/Main.png" style="width: 100%" onclick="myFunction(this);">
                    </div>
                    <div class="column">
                        <img src="../Content/img/Order.png" style="width: 100%" onclick="myFunction(this);">
                    </div>
                    <div class="column">
                        <img src="../Content/img/Reg.png" style="width: 100%" onclick="myFunction(this);">
                    </div>
                    <div class="column">
                        <img src="../Content/img/Return.png" style="width: 100%" onclick="myFunction(this);">
                    </div>
                     <div class="column">
                        <img src="../Content/img/Employee.png" style="width: 100%" onclick="myFunction(this);">
                    </div>
                    <div class="column">
                        <img src="../Content/img/Product.png" style="width: 100%" onclick="myFunction(this);">
                    </div>
                     <div class="column">
                        <img src="../Content/img/Type.png" style="width: 100%" onclick="myFunction(this);">
                    </div>
                    <div class="column">
                        <img src="../Content/img/Fired.png" style="width: 100%" onclick="myFunction(this);">
                    </div>
                </div>
                <div class="container-img">
                    <span onclick="this.parentElement.style.display='none'" class="closebtn" style="color:black">×</span>
                    <img id="expandedImg" style="width: 100%">
                </div>
            </div>
        </div>
    </form>
    <script>
        function myFunction(imgs) {
            var expandImg = document.getElementById("expandedImg");
            expandImg.src = imgs.src;
            expandImg.parentElement.style.display = "block";
        }
    </script>
</body>
</html>
