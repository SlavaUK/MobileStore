using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Crypt_Library;
namespace MobileStore
{
    public class DBProcedures
    {
        private SqlCommand command = new SqlCommand("", DBConnection.connection);
        private void commandConfig(string config)
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[dbo].[" + config + "]";
            command.Parameters.Clear();
        }

        //Регистрация пользователя
        public void RegistrationUsers(string Surname, string Name, string Middle_Name, string Client_Login,
            string Client_Password, int Role_ID)
        {
            //Шифрование
            string passwordEnc = Crypt.Encrypt(Client_Password);
            commandConfig("Client_Insert");
            command.Parameters.AddWithValue("@Surname", Surname);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Middle_Name", Middle_Name);
            command.Parameters.AddWithValue("@Client_Login", Client_Login);
            command.Parameters.AddWithValue("@Client_Password", passwordEnc);
            command.Parameters.AddWithValue("@Role_ID", Role_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        //Добавление товаров
        public void Product_Insert(string Product_Name, int Quantity, decimal Price, int Type_ID)
        {
            commandConfig("Product_Insert");
            command.Parameters.AddWithValue("@Product_Name", Product_Name);
            command.Parameters.AddWithValue("@Quantity", Quantity);
            command.Parameters.AddWithValue("@Price", Price);
            command.Parameters.AddWithValue("@Type_ID", Type_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление товара
        public void Product_Update(int ID_Product, string Product_Name, int Quantity, decimal Price, int Type_ID)
        {
            commandConfig("Product_Update");
            command.Parameters.AddWithValue("@ID_Product", ID_Product);
            command.Parameters.AddWithValue("@Product_Name", Product_Name);
            command.Parameters.AddWithValue("@Quantity", Quantity);
            command.Parameters.AddWithValue("@Price", Price);
            command.Parameters.AddWithValue("@Type_ID", Type_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление продукта
        public void Product_Delete(int ID_Product)
        {
            commandConfig("Product_Delete");
            command.Parameters.AddWithValue("@ID_Product", ID_Product);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        //Добавление сотрудника
        public void Employee_Insert(string Employee_Surname, string Employee_Name, string Employee_Middle_Name, string Login,
            string Password, bool Logical_Delete, int Role_ID, string Passport_Series, string Passport_Number, int Post_ID, string Contract_Date)
        {
            int contractID;
            //Создание трудового договора
            commandConfig("Contract_Insert");
            command.Parameters.AddWithValue("@Passport_Series", Passport_Series);
            command.Parameters.AddWithValue("@Passport_Number", Passport_Number);
            command.Parameters.AddWithValue("@Post_ID", Post_ID);
            command.Parameters.AddWithValue("@Contract_Date", Contract_Date);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            //Получает id контракта
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT MAX(ID_Contract) FROM [Contract]";
            DBConnection.connection.Open();
            contractID = Convert.ToInt32(command.ExecuteScalar().ToString());
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            //Создание записи о сотрудники
            //Шифрование
            string passwordEnc = Crypt.Encrypt(Password);
            commandConfig("Employee_Insert");
            command.Parameters.AddWithValue("@Employee_Surname", Employee_Surname);
            command.Parameters.AddWithValue("@Employee_Name", Employee_Name);
            command.Parameters.AddWithValue("@Employee_Middle_Name", Employee_Middle_Name);
            command.Parameters.AddWithValue("@Login", Login);
            command.Parameters.AddWithValue("@Password", passwordEnc);
            command.Parameters.AddWithValue("@Logical_Delete", Logical_Delete);
            command.Parameters.AddWithValue("@Role_ID", Role_ID);
            command.Parameters.AddWithValue("@Contract_ID", contractID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление сотрудника
        public void Employee_Delete(int ID_Employee)
        {
            int contractID;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT [Contract_ID] from [Employee] where [ID_Employee] = '" + Convert.ToString(ID_Employee) + "'";
            DBConnection.connection.Open();
            contractID = Convert.ToInt32(command.ExecuteScalar().ToString());
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            commandConfig("Employee_Delete");
            command.Parameters.AddWithValue("@ID_Employee", ID_Employee);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            //Удаление документа
            commandConfig("Contract_Delete");
            command.Parameters.AddWithValue("@ID_Contract", contractID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление данных о сотруднике
        public void Employee_Update(int ID_Employee, string Employee_Surname, string Employee_Name, string Employee_Middle_Name, string Login, int Contract_ID,
            string Password, bool Logical_Delete, int Role_ID, string Passport_Series, string Passport_Number, int Post_ID, string Contract_Date, int ID_Contract, string Contract_Number)
        {
            //Шифрование
            string passwordEnc = Crypt.Encrypt(Password);
            commandConfig("Employee_Update");
            command.Parameters.AddWithValue("@ID_Employee", ID_Employee);
            command.Parameters.AddWithValue("@Employee_Surname", Employee_Surname);
            command.Parameters.AddWithValue("@Employee_Name", Employee_Name);
            command.Parameters.AddWithValue("@Employee_Middle_Name", Employee_Middle_Name);
            command.Parameters.AddWithValue("@Login", Login);
            command.Parameters.AddWithValue("@Password", passwordEnc);
            command.Parameters.AddWithValue("Logical_Delete", Logical_Delete);
            command.Parameters.AddWithValue("@Contract_ID", Contract_ID);
            command.Parameters.AddWithValue("@Role_ID", Role_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            //Обновление контракта
            commandConfig("Contract_Update");
            command.Parameters.AddWithValue("@ID_Contract", ID_Contract);
            command.Parameters.AddWithValue("@Passport_Series", Passport_Series);
            command.Parameters.AddWithValue("@Passport_Number", Passport_Number);
            command.Parameters.AddWithValue("@Post_ID", Post_ID);
            command.Parameters.AddWithValue("@Contract_Date", Contract_Date);
            command.Parameters.AddWithValue("@Contract_Number", Contract_Number);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Блокировка пользователя
        public void Employee_Ban(int ID_Employee)
        {
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "update [Employee] set [Logical_Delete] = 'true' where ID_Employee = '" + Convert.ToString(ID_Employee) + "'";
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Создание личного дела
        public void File_Insert(int Employee_ID, int Fired_Order_ID)
        {
            commandConfig("File_Insert");
            command.Parameters.AddWithValue("@Employee_ID", Employee_ID);
            command.Parameters.AddWithValue("@Fired_Order_ID", Fired_Order_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Создание приказа об увольнении
        public void Fired_Order_Insert(string Fired_Reason, int Employee_ID, string Fired_Date)
        {
            commandConfig("Fired_Order_Insert");
            command.Parameters.AddWithValue("@Fired_Reason", Fired_Reason);
            command.Parameters.AddWithValue("@Employee_ID", Employee_ID);
            command.Parameters.AddWithValue("@Fired_Date", Fired_Date);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            //Блокировка пользователя
            Employee_Ban(Employee_ID);
        }

        //Удаление приказа об увольнении
        public void Fired_Order_Delete(int ID_Fired_Order)
        {
            int fileID;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT [ID_File] from [File] where [Fired_Order_ID] = '" + Convert.ToString(ID_Fired_Order) + "'";
            DBConnection.connection.Open();
            fileID = Convert.ToInt32(command.ExecuteScalar().ToString());
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            commandConfig("File_Delete");
            command.Parameters.AddWithValue("@ID_File", fileID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            commandConfig("Fired_Order_Delete");
            command.Parameters.AddWithValue("@ID_Fired_Order", ID_Fired_Order);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление приказа об увольнении
        public void Fired_Order_Update(int ID_Fired_Order, string Fired_Reason, int Employee_ID, string Fired_Date)
        {
            commandConfig("Fired_Order_Update");
            command.Parameters.AddWithValue("@ID_Fired_Order", ID_Fired_Order);
            command.Parameters.AddWithValue("@Fired_Reason", Fired_Reason);
            command.Parameters.AddWithValue("@Fired_Date", Fired_Date);
            command.Parameters.AddWithValue("@Employee_ID", Employee_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Создание заказа
        public void Order_Insert(int Product_ID, int Product_Amount, int Client_ID, decimal Sum)
        {
            commandConfig("Order_Insert");
            command.Parameters.AddWithValue("@Product_ID", Product_ID);
            command.Parameters.AddWithValue("@Product_Amount", Product_Amount);
            command.Parameters.AddWithValue("@Client_ID", Client_ID);
            command.Parameters.AddWithValue("@Sum", Sum);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление чека
        public void Check_Insert(string Time, string Date, int Employee_ID, int Order_ID)
        {
            commandConfig("Check_Insert");
            command.Parameters.AddWithValue("@Time", Time);
            command.Parameters.AddWithValue("@Date", Date);
            command.Parameters.AddWithValue("@Employee_ID", Employee_ID);
            command.Parameters.AddWithValue("@Order_ID", Order_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление заказа
        public void Order_Delete(int ID_Order)
        {
            commandConfig("Order_Delete");
            command.Parameters.AddWithValue("@ID_Order", ID_Order);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление чека
        public void Check_Delete(int ID_Check)
        {
            commandConfig("Check_Delete");
            command.Parameters.AddWithValue("@ID_Check", ID_Check);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление заказа
        public void Order_Update(int ID_Order, int Product_ID, int Product_Amount, int Client_ID)
        {
            commandConfig("Order_Update");
            command.Parameters.AddWithValue("@ID_Order", ID_Order);
            command.Parameters.AddWithValue("@Product_ID", Product_ID);
            command.Parameters.AddWithValue("@Product_Amount", Product_Amount);
            command.Parameters.AddWithValue("@Client_ID", Client_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление чека
        public void Check_Update(int ID_Check, string Time, string Date, int Order_ID, int Employee_ID)
        {
            commandConfig("Check_Update");
            command.Parameters.AddWithValue("@ID_Check", ID_Check);
            command.Parameters.AddWithValue("@Time", Time);
            command.Parameters.AddWithValue("@Date", Date);
            command.Parameters.AddWithValue("@Order_ID", Order_ID);
            command.Parameters.AddWithValue("@Employee_ID", Employee_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Создание возврата
        public void Return_List_Insert(string Return_Date, int Check_ID, string Return_Reason)
        {
            commandConfig("Return_List_Insert");
            command.Parameters.AddWithValue("@Return_Date", Return_Date);
            command.Parameters.AddWithValue("@Check_ID", Check_ID);
            command.Parameters.AddWithValue("@Return_Reason", Return_Reason);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление возврата возврата
        public void Return_List_Update(int ID_Return_List, string Return_Date, int Check_ID, string Return_Reason)
        {
            commandConfig("Return_List_Update");
            command.Parameters.AddWithValue("@ID_Return_List", ID_Return_List);
            command.Parameters.AddWithValue("@Return_Date", Return_Date);
            command.Parameters.AddWithValue("@Check_ID", Check_ID);
            command.Parameters.AddWithValue("@Return_Reason", Return_Reason);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление возврата
        public void Return_List_Delete(int ID_Return_List)
        {
            commandConfig("Return_List_Delete");
            command.Parameters.AddWithValue("@ID_Return_List", ID_Return_List);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление типа
        public void Type_Insert(string Type_Name)
        {
            commandConfig("Type_Insert");
            command.Parameters.AddWithValue("@Type_Name", Type_Name);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление типа
        public void Type_Update(int ID_Type, string Type_Name)
        {
            commandConfig("Type_Update");
            command.Parameters.AddWithValue("@ID_Type", ID_Type);
            command.Parameters.AddWithValue("@Type_Name", Type_Name);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление типа
        public void Type_Delete(int ID_Type)
        {
            commandConfig("Type_Delete");
            command.Parameters.AddWithValue("@ID_Type", ID_Type);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
    }
}