using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using test.Entity;
using test.DAO;

namespace test
{
    public partial class Form1 : Form
    {

        String dbConnStr = ConfigurationManager.ConnectionStrings["dbConnStr"].ToString();
        public  Form1()
        {
            InitializeComponent();
           
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            UsersDAO usersDao = new UsersDAO();
            //通过用户名返回用户
            users = usersDao.getUsersByName(textBox1.Text);
            if (users == null) { 
                MessageBox.Show("用户名不存在!","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
            }
            else if (textBox2.Text != users.Password) { 
                MessageBox.Show("密码错误!","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
            }
            else if (comboBox1.Text != users.Permission)
            {
                MessageBox.Show("用户类型错误!", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else{
                if (users.Permission.Equals("学生"))
                {
                    studentview stu = new studentview(users);
                    stu.Show();
                }
                else {
                    teacherview teacher = new teacherview();
                    teacher.Show();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
