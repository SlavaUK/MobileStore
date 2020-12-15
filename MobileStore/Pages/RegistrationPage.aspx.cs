using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileStore.Pages
{
    public partial class RegistrationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Регистрация
        protected void btEnter_Click(object sender, EventArgs e)
        {
            DBConnection connection = new DBConnection();
            if (connection.UnqLogin(tbLogin.Text) != 0)
            {
                lblLogin.Visible = true;
            }
            else
            {
                lblLogin.Visible = true;
                if (tbPassword.Text != tbPasswordConfirm.Text)
                {
                    tbPassword.BackColor = System.Drawing.Color.Red;
                    tbPasswordConfirm.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    tbPassword.BackColor = System.Drawing.Color.White;
                    tbPasswordConfirm.BackColor = System.Drawing.Color.White;
                    DBProcedures dBProcedures = new DBProcedures();
                    dBProcedures.RegistrationUsers(tbSurname.Text.ToString(), tbName.Text.ToString(), tbMiddleName.Text.ToString(),
                        tbLogin.Text.ToString(), tbPassword.Text.ToString(), 1);
                    Response.Redirect("AuthorizationPage.aspx");
                }
            }
        }
    }
}