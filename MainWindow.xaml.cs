using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace scitcnt
{
    public partial class MainWindow : Window{
        public MainWindow(){
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e){

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList){
                //过滤IPv4
                if (ip.AddressFamily == AddressFamily.InterNetwork){
                    //MessageBox.Show(ip.ToString());
                    IPDZcs.Text = ip.ToString(); //更新控件
                }
            }

        }

        private void Button0_Click(object sender, RoutedEventArgs e){
            var fileName = @"C:\scitcnet\use.txt";
            var file = File.ReadLines(fileName).ToList();
            int count = file.Count();
            Random rnd = new Random();
            int skip = rnd.Next(0, count);
            string line = file.Skip(skip).First();
            MessageBox.Show("分享账号："+line);
        }

        private void Button1_Click(object sender, RoutedEventArgs e){
            //取用户账号
            var fileName = @"C:\scitcnet\use.txt";
            var file = File.ReadLines(fileName).ToList();
            int count = file.Count();
            Random rnd = new Random();
            int skip = rnd.Next(0, count);
            string use = file.Skip(skip).First();
            
            //curl调用
            using (Process myPro = new Process()){
                
                //curl绝对路径  
                myPro.StartInfo.FileName = @"C:\Windows\SysWOW64\curl.exe";
                myPro.StartInfo.UseShellExecute = false;
                myPro.StartInfo.RedirectStandardInput = true;
                myPro.StartInfo.RedirectStandardOutput = true;
                myPro.StartInfo.RedirectStandardError = true;
                myPro.StartInfo.CreateNoWindow = true;
                //post参数拼接
                var posta = "-d \"loginType=&auth_type=0&isBindMac=0&pageid=1&userId=";
                var postb = use;
                var postc = "&passwd=123456\" ";
                var urlqz = posta + postb + postc;

                //拼接
                var urlip = this.IPDZcs.Text;
                var urlcgi = "\"http://10.10.11.14/webauth.do?wlanuserip=";
                var urlwg = "&wlanacname=XF_BRAS\"";
                var rzpj = urlqz + urlcgi + urlip + urlwg;
                myPro.StartInfo.Arguments = rzpj;
                myPro.Start();
                myPro.StandardInput.AutoFlush = true;

                //截获cmd
                //string output = myPro.StandardOutput.ReadToEnd();
                //myPro.WaitForExit();
                //myPro.Close();
                //MessageBox.Show(output);
                MessageBox.Show("认证完成，请等待几秒再刷新网页！");
            }

        }

    }
}
