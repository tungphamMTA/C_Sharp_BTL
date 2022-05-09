using QuanLyTrungTam.Bus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTrungTam.AllForms
{
    public partial class LichDay_Tuan : UserControl
    {
        private string ID;
        private string Ma_GiaoVien;
        List<List<Button>> listButton = new List<List<Button>>();

        const int dayOfWeek = 7;
        const int weekOfMonth = 6;
        const int ButtonWidth = 82;
        const int ButtonHeight = 50;
        const int margin = 12;

        private List<string> dayofweek = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        public LichDay_Tuan(string _ID)
        {
            ID = _ID;
            InitializeComponent();

            BUS_GiaoVien busGV = new BUS_GiaoVien();
            Ma_GiaoVien = busGV.getMa_GiaoVien(ID);

            CreateCalender();
        }

        void CreateCalender()
        {
            Button oldBtn = new Button() { Width = 12, Height = 120, Location = new System.Drawing.Point(-margin, 120) };
            for (int i = 0; i < weekOfMonth; i++)
            {
                listButton.Add(new List<Button>());
                for (int j = 0; j < dayOfWeek; j++)
                {
                    Button btn = new System.Windows.Forms.Button();
                    btn.Size = new System.Drawing.Size(ButtonWidth, ButtonHeight);
                    btn.Location = new System.Drawing.Point(oldBtn.Location.X + oldBtn.Width +
                        margin, oldBtn.Location.Y);
                    btn.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btn.UseVisualStyleBackColor = true;
                    pnCal.Controls.Add(btn);
                    oldBtn.Location = new System.Drawing.Point(oldBtn.Location.X + ButtonWidth
                        + margin, oldBtn.Location.Y);
                    btn.Click += Btn_Click;
                    listButton[i].Add(btn);
                }
                oldBtn = new Button()
                {
                    Width = 12,
                    Height = 120,
                    Location = new System.Drawing.Point(-margin, oldBtn.Location.Y + ButtonHeight + 10)
                };
            }

            AddNumber(dateTimePicker1.Value);
        }
        bool EqualDate(DateTime date1, DateTime date2)
        {
            if (date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        int dayOfMonth(DateTime date)
        {
            switch (date.Month)
            {
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                case 2:
                    {
                        if (date.Year % 4 == 0 && date.Year % 100 != 0 || date.Year % 400 == 0)
                            return 29;
                        else
                            return 28;
                    }
                default:
                    return 31;
            }
        }
        void AddNumber(DateTime date)
        {
            DateTime newDay = new DateTime(date.Year, date.Month, 1);

            int line = 0;

            for (int i = 1; i <= dayOfMonth(date); i++)
            {
                int collumns = dayofweek.IndexOf(newDay.DayOfWeek.ToString());

                Button btn = listButton[line][collumns];
                btn.Text = i.ToString();
                ColorButton(btn);
                if (collumns >= 6)
                {
                    line++;
                }

                if (EqualDate(newDay, DateTime.Now))
                {
                    btn.BackColor = Color.Yellow;
                }

                else if (EqualDate(newDay, date))
                {
                    btn.BackColor = Color.Aqua;
                }
                newDay = newDay.AddDays(1);
            }
            foreach (List<Button> list in listButton)
            {
                foreach (Button bt in list)
                {
                    if (bt.Text == "")
                    {
                        bt.Visible = false;
                    }
                    else
                    {
                        bt.Visible = true;
                    }
                }
            }
        }

        void ColorButton(Button btn)
        {
            DateTime dt = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, Int32.Parse(btn.Text));
            BUS_GiaoVien busGV = new BUS_GiaoVien();
            DataTable table = busGV.LayLichHoc_Ngay(Ma_GiaoVien, dt);
            if (table.Rows.Count == 0)
            {
                btn.BackColor = Color.Snow;
            }
            else
            {
                btn.BackColor = Color.Pink;
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text == "")
            {
                return;
            }
            btn.BackColor = Color.Snow;
            DateTime dt = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, Int32.Parse(btn.Text));
            dateTimePicker1.Value = dt;
            BUS_GiaoVien busGV = new BUS_GiaoVien();
            DataTable table = busGV.LayLichHoc_Ngay(Ma_GiaoVien, dt);
            dataGridView1.DataSource = table;
        }
        void ClearTable()
        {
            for (int i = 0; i < weekOfMonth; i++)
            {
                for (int j = 0; j < dayOfWeek; j++)
                {
                    Button btn = listButton[i][j];
                    btn.Text = "";
                    btn.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ClearTable();
            AddNumber(dateTimePicker1.Value);
        }
    }
}
