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
using StudentDormitoryManager.DAO;
using StudentDormitoryManager.Entity;
using System.Collections;

namespace test
{
    public partial class teacherview : Form
    {
        public teacherview()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 学生信息按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control subform in panel1.Controls) {
                if (subform.Name.ToString().Equals("StuInfoManagement")) {
                    subform.Show();
                    return;
                }
                else if(subform.Name.ToString().Equals("DorInfoManagement")){
                    subform.Dispose();
                }
                else if (subform.Name.ToString().Equals("HealthRecordComparison")) {
                    subform.Dispose();
                }
            }
            StuInfoManagement sim = new StuInfoManagement();
            sim.TopLevel = false;
            sim.Parent = panel1;
            sim.Dock = DockStyle.Fill;
            sim.Show();
        }


        /// <summary>
        /// 宿舍管理按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control subform in panel1.Controls)
            {
                if (subform.Name.ToString().Equals("DorInfoManagement"))
                {
                    subform.Show();
                    return;
                }
                else if (subform.Name.ToString().Equals("StuInfoManagement")) {
                    subform.Dispose();
                }
                else if (subform.Name.ToString().Equals("HealthRecordComparison"))
                {
                    subform.Dispose();
                }
            }
            DorInfoManagement dim = new DorInfoManagement();
            dim.TopLevel = false;
            dim.Parent = panel1;
            dim.Dock = DockStyle.Fill;
            dim.Show();
        }

        /// <summary>
        /// 卫生评比按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Control subform in panel1.Controls)
            {
                if (subform.Name.ToString().Equals("HealthRecordComparison"))
                {
                    subform.Show();
                    return;
                }
                else if (subform.Name.ToString().Equals("DorInfoManagement"))
                {
                    subform.Dispose();
                }
                else if (subform.Name.ToString().Equals("StuInfoManagement"))
                {
                    subform.Dispose();
                }
            }
            HealthRecordComparison hrc = new HealthRecordComparison();
            hrc.TopLevel = false;
            hrc.Parent = panel1;
            hrc.Dock = DockStyle.Fill;
            hrc.Show();
        }

    }
}
