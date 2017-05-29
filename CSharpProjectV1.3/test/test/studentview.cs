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

using StudentDormitoryManager.Entity;
using StudentDormitoryManager.DAO;
using test.Entity;
using System.Collections;
using System.Globalization;

namespace test
{
    public partial class studentview : Form
    {
        String content = null;

        Users users = new Users();
        public studentview(Users user)
        {
            this.users = user;
            InitializeComponent();
        }

        /// <summary>
        /// 创建节点
        /// </summary>
        private void CreateTreeVie()
        {
            DepartmentDAO dptDAO = new DepartmentDAO();
            ArrayList dptList = dptDAO.GetAllDepartments();
            TreeNode rootNode = null;
            foreach (Department dpt in dptList)
            {
                rootNode = new TreeNode(dpt.Depname.ToString());
                treeView1.Nodes.Add(rootNode);
            }
        }

        public ListView GetDormitoryInformation(String content) {
            return null;
        }
        /// <summary>
        /// 退出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// studentview视图加载时，加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void studentview_Load(object sender, EventArgs e)
        {
            //显示节点
            CreateTreeVie();
            //显示学生基本信息
            Student stu = new Student();
            StudentDAO stuDao = new StudentDAO();
            stu = stuDao.getAllStudentById(users.Username);
            textBox1.Text = stu.Stuname;
            textBox3.Text = stu.Stuid;
            DormitoryDAO dmtDao = new DormitoryDAO();
            Dormitory dmt = dmtDao.getDormitoriesByDmt(stu.Dorid); 
        }

        /// <summary>
        /// 查询别的寝室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            //获取学生信息数据
            Student stu = new Student();
            StudentDAO stuDao = new StudentDAO();
            stu = stuDao.getAllStudentById(textBox2.Text);
            //获取寝室有关数据
            DormitoryDAO dmtDao = new DormitoryDAO();
            HealthScoreDAO hsDao = new HealthScoreDAO();

            //提高加载速度，挂起ui
            listView1.BeginUpdate();
            //开始往listview中添加数据
            ListViewItem item = new ListViewItem();
            ArrayList hsList = hsDao.getHealthScoreByDorid(stu.Dorid);
            foreach (HealthScore hs in hsList)
            {
                DateTime dt = DateTime.Now;
                String month = dt.ToString().Substring(5, 2);
                //只显示当前月分数信息
                if (month.Equals(hs.Checkdate.ToString().Substring(5, 2)))
                {
                    Dormitory dmt = dmtDao.getDormitoriesByDmt(stu.Dorid);
                    item.Text = stu.Stuid.ToString();   //学号
                    item.SubItems.Add(stu.Stuname.ToString());  //姓名
                    item.SubItems.Add(stu.Dorid.ToString());    //寝室号
                    item.SubItems.Add(hs.Score.ToString());    //寝室分数
                    item.SubItems.Add(hs.Checkdate.ToString());    //修改时间
                    item.SubItems.Add(dmt.Dorhonor.ToString()); //寝室荣誉
                    listView1.Items.Add(item);
                }
               
            }
            //结束数据处理，开始一次绘制
            listView1.EndUpdate();
        }

        /// <summary>
        /// 选中节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode == null) {
                MessageBox.Show("请选择一个节点","提示信息",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            content = e.Node.Text.ToString();
            String info = "当前月";
            GetStuInfomation(content,info);
        }

        /// <summary>
        /// 根据选定班级加载信息
        /// </summary>
        /// <param name="content"></param>
        public void GetStuInfomation(String content,String info) {
            listView1.Items.Clear();
            //获取班级信息表
            Department dpt = new Department();
            DepartmentDAO dptDAO = new DepartmentDAO();
            //获取到班级id
            Int32 selectDorid = dptDAO.GetDepidByDepName(content);
            //获取学生信息数据
            ArrayList stuList = new ArrayList();
            StudentDAO stuDao = new StudentDAO();
            stuList = stuDao.getAllStudentByDepid(selectDorid);
            //获取寝室有关数据
            DormitoryDAO dmtDao = new DormitoryDAO();
            HealthScoreDAO hsDao = new HealthScoreDAO();

            if (selectDorid != -1)
            {
                //提高加载速度，挂起ui
                listView1.BeginUpdate();
                //开始往listview中添加数据
                foreach (Student stu in stuList)
                {
                    ListViewItem item = new ListViewItem();
                    ArrayList hsList = hsDao.getHealthScoreByDorid(stu.Dorid);
                    foreach (HealthScore hs in hsList)
                    {
                        DateTime dt = DateTime.Now;
                        String month = dt.ToString().Substring(5, 2);
                        //只显示当前月分数信息
                        if (info.Equals("当前月"))
                        {
                            if (month.Equals(hs.Checkdate.ToString().Substring(5, 2)))
                            {
                                Dormitory dmt = dmtDao.getDormitoriesByDmt(stu.Dorid);
                                item.Text = stu.Stuid.ToString();   //学号
                                item.SubItems.Add(stu.Stuname.ToString());  //姓名
                                item.SubItems.Add(stu.Dorid.ToString());    //寝室号
                                item.SubItems.Add(hs.Score.ToString());    //寝室分数
                                item.SubItems.Add(hs.Checkdate.ToString());    //修改时间
                                item.SubItems.Add(dmt.Dorhonor.ToString()); //寝室荣誉
                                listView1.Items.Add(item);
                            }
                        }
                        else if(info.Equals("所有")){
                            Dormitory dmt = dmtDao.getDormitoriesByDmt(stu.Dorid);
                            item.Text = stu.Stuid.ToString();   //学号
                            item.SubItems.Add(stu.Stuname.ToString());  //姓名
                            item.SubItems.Add(stu.Dorid.ToString());    //寝室号
                            item.SubItems.Add(hs.Score.ToString());    //寝室分数
                            item.SubItems.Add(hs.Checkdate.ToString());    //修改时间
                            item.SubItems.Add(dmt.Dorhonor.ToString()); //寝室荣誉
                            listView1.Items.Add(item);
                        }
                    }
                }
                //结束数据处理，开始一次绘制ui
                listView1.EndUpdate();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetStuInfomation(content,comboBox1.SelectedItem.ToString());

        }

    }
}
