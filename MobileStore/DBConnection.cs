using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.DynamicData;
using Antlr.Runtime.Tree;
using Crypt_Library;

namespace MobileStore
{
    public class DBConnection
    {
        public static int userID, selectedRow, contractID;
        //Подключение к БД 
        public static SqlConnection connection = new SqlConnection(
           "Data Source=DESKTOP-EVJQEB7\\MYSERVER; Initial Catalog=MobileStore;" +
           "Integrated Security=True; Connect Timeout=30; Encrypt=False;" +
           "TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False");

        public static string qrProducts = "select [ID_Product], [Product_Name] as 'Название товара', [Quantity] as 'Количество', " +
            "[Price] as 'Цена', [Type_ID], [Type_Name] as 'Категория' from[Product] " +
            "inner join[Type] on[ID_Type] = [Type_ID]",
            qrType = "select [ID_Type], [Type_Name] as 'Тип товара' from [Type]",
            qrEmployees = "select [ID_Employee], [Employee_Surname] as 'Фамилия', [Employee_Name] as 'Имя', [Employee_Middle_Name] as 'Отчество', [Login] as 'Логин', [Password]," +
            "[ID_Post], [Post_Name] as 'Должность', [Pay] as 'Зарплата', [Contract_Date] as 'Дата приёма', [Contract_ID], [Passport_Series] as 'Серия паспорта', [Passport_Number] as 'Номер паспорта', " +
            "[Contract_Number] as 'Трудовой договор', [Role_ID]  from[Employee] " +
            "inner join[Role] on[ID_Role] = [Role_ID] " +
            "inner join[Contract] on[ID_Contract] = [Contract_ID] " +
            "inner join[Post] on[ID_Post] = [Post_ID] where [Logical_Delete] != 1",
            qrPost = "select [ID_Post], [Post_Name] from [Post]",
            qrRole = "select [ID_Role], [Role_Name] from [Role]",
            qrFiredList = "select [ID_Fired_Order], [ID_Employee], [Employee_Surname] as 'Фамилия', [Employee_Name] as 'Имя', [Employee_Middle_Name] as 'Отчество', [Post_ID], [Post_Name] as 'Должность', " +
            "[Fired_Date] as 'Дата увольнения', [Fired_Reason] as 'Причина увольнения', [Contract_Number] as 'Трудовой договор', [Fired_Order_Number] as 'Приказ об увольнении' from [Fired_Order] " +
            "inner join[Employee] on[ID_Employee] = [Employee_ID] " +
            "inner join[Contract] on[ID_Contract] = [Contract_ID] " +
            "inner join[Post] on[ID_Post] = [Post_ID]",
            qrEmployeeData = "select [ID_Employee], Concat([Employee_Surname], + ' ' +  [Employee_Name], + ' ' + [Employee_Middle_Name]) as 'ФИО' from [Employee]",
            qrOrderList = "select [Product_Name] as 'Название', [Price] as 'Цена' from [Order] inner join[Product] on[Product_ID] = [ID_Product]",
            qrOrder = "select [ID_Order], [Product_ID], [Product_Name] as 'Название товара', [Product_Amount] as 'Количество', [ID_Client], CONCAT([Surname], + ' ' + [Name], + ' ' + [Middle_Name]) as 'Клиент', " +
            "[Sum] as 'Цена', [Date] as 'Дата продажи', [Employee_ID], [Employee_Surname] as 'Кассир', [ID_Check], [Check_Number] as 'Номер чека' from[Order] " +
            "left join[Check] on[Order_ID] = [ID_Order] " +
            "left join[Product] on[Product_ID] = [ID_Product] " +
            "left join[Client] on[Client_ID] = [ID_Client] " +
            "left join[Employee] on[Employee_ID] = [ID_Employee]",
            qrClient = "select [ID_Client], Concat([Surname], + ' ' +  [Name], + ' ' + [Middle_Name]) as 'ФИО' from [Client]",
            qrReturnList = "select [ID_Return_List], [Product_Name] as 'Название товара', [Return_Reason] as 'Причина возврата', [Return_Date] as 'Дата возврата', [Check_ID], " +
            "[Check_Number] as 'Номер чека' from[Return_List] " +
            "inner join[Check] on[ID_Check] = [Check_ID] " +
            "inner join[Order] on[ID_Order] = [Check].[Order_ID] " +
            "inner join[Product] on[Product_ID] = [ID_Product]";

        private SqlCommand command = new SqlCommand("", connection);
        
        //Авторизация
        public Int32 Authorization(string login, string password)
        {
            try
            {
                string passwordEnc = Crypt.Encrypt(password);
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select [ID] from [dbo].[Users] " +
               "where [Логин] = '" + login + "' and [Пароль] = '" + passwordEnc + "'";
                DBConnection.connection.Open();
                userID = Convert.ToInt32(command.ExecuteScalar().ToString());
                connection.Close();
                return (userID);
            }
            catch
            {
                connection.Close();
                userID = 0;
                return (userID);

            }
        }
        //Проверка уникальности логина
        public int UnqLogin(string login)
        {
            int log;
            try
            {
                command.CommandText = "select count(*) from [Employee] where [Login] like '%" + login + "%'";
                connection.Open();
                log = Convert.ToInt32(command.ExecuteScalar().ToString());
                connection.Close();
                return log;
            }
            catch
            {
                connection.Close();
                log = 0;
                return log;
            }
        }
        //Роль пользователя
        public string Role(Int32 User)
        {
            string RoleID;
            int ID_User;
            try
            {
                try
                {
                    command.CommandText = "select [ID_Users] from [Users] where ID like '%" + User + "%'";
                    connection.Open();
                    ID_User = Convert.ToInt32(command.ExecuteScalar().ToString());
                    connection.Close();
                }
                catch
                {
                    ID_User = 0;
                }
                command.CommandText = "select [Роль] from [Users] where [ID_User] like '%" + ID_User + "%'";
                connection.Open();
                RoleID = command.ExecuteScalar().ToString();
                connection.Close();
                return RoleID;
            }
            catch
            {
                connection.Close();
                RoleID = "1";
                return RoleID;
            }
        }
    }
}