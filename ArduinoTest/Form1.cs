using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace ArduinoTest
{
    public partial class Form1 : Form
    {
        SerialPort serialPort1 = new SerialPort();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen) return;
            serialPort1.Write("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen) return;
            serialPort1.Write("0");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") return;
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Write("0");
                    serialPort1.Close();
                }
                else
                {
                    serialPort1.PortName = comboBox1.SelectedItem.ToString(); // "COM3" 선택;
                    serialPort1.BaudRate = 9600;                              
                    serialPort1.DataBits = 8;
                    serialPort1.StopBits = StopBits.One;                      // "StopBits": 정지비트의 수를 나타내는 열거형
                    serialPort1.Parity = Parity.None;                         // "Parity Bit": 데이터 전송 중 오류를 감지하는데 활용
                    serialPort1.Open();                                       // 직렬 포트 활성화 > 읽기/쓰기 작업 수행
                }
            }
            catch (Exception)
            {
                MessageBox.Show("연결에러", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            button3.Text = serialPort1.IsOpen ? "Disconnect" : "Connect";
            comboBox1.Enabled = !serialPort1.IsOpen;
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (var item in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(item);
            }
        }
    }
}
