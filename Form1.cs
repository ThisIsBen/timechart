using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;


using System.IO;

using System.Text;

using System.Runtime.Serialization.Json;

public class JSonHelper
{

    public string ConvertObjectToJSon<T>(T obj)
    {

        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

        MemoryStream ms = new MemoryStream();

        ser.WriteObject(ms, obj);

        string jsonString = Encoding.UTF8.GetString(ms.ToArray());

        ms.Close();

        return jsonString;

    }
}




namespace socket_experiment
{
   

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class TagsValue
        {

            public string nonverbal_factor;
            /*
            public int year;
            public int month;
            public int day;
             * */
        }
        public class FieldsValue
        {

            public int intensity;
            /*
            public int year;
            public int month;
            public int day;
             * */
        }

        public class Lad
        {
            public string measurement;
            public TagsValue tags;
            public string time;
            public FieldsValue fields;

           
        }
        

        //test new json converter
        public class Smile

        {

            public int intensity { get; set; }

            public string nonverbal_factor { get; set; }

            public string time { get; set; }

           

        }



        //

       
        private void button1_Click(object sender, EventArgs e)
        {
            //string returnData = "";
/*      
UdpClient udpClient = new UdpClient();

udpClient.Connect(txtbHost.Text, 8080);

Byte[] senddata = Encoding.ASCII.GetBytes("Hello World");

udpClient.Send(senddata, senddata.Length);
*/

            /*udp
            UdpClient udpClient = new UdpClient();
            udpClient.Connect("127.0.0.1", 8082);
            Byte[] sendBytes = Encoding.UTF8.GetBytes("socket");
            try
            {
                udpClient.Send(sendBytes, sendBytes.Length);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
            }
           
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
          
                label1.Text = "you've hit the btn!";
             * 
           */
           


            
            ////create obj array with len=2
            /*
            Lad[] obj = new Lad[2];
           
               
            
            obj[0] = new Lad
            {
                measurement = "sb",
                tags = new TagsValue
                {
                    nonverbal_factor = "smile_intensity0"
                },
                time = System.DateTime.Now.ToString("u"),//The Universal Full ("U") Format Specifier.
                fields = new FieldsValue
                {
                    intensity = 29
                }

            };

            obj[1] = new Lad
            {
                measurement = "sb",
                tags = new TagsValue
                {
                    nonverbal_factor = "smile_intensity1"
                },
                time = System.DateTime.Now.ToString("u"),//The Universal Full ("U") Format Specifier.
                fields = new FieldsValue
                {
                    intensity = 32
                }

            };
             * */
            ////
            /*
            var obj = new Lad
            {
                measurement = "Smile",
                tags = new TagsValue
                {
                    nonverbal_factor="smile_intensity"
                },
                time = nonverbalFactorRecordTime.ToString(),
                fields = new FieldsValue
                {
                    intensity=29
                }

            };
              */
          


            //test new json converter

            Smile[] product = new Smile[3];
            for (int i=0;i<3;i++)
            {
                product[i] = new Smile();
            }
            product[0].intensity = 1001;

            product[0].nonverbal_factor = "Samsung Galaxy III";

            product[0].time = System.DateTime.Now.ToString("u");//The Universal Full ("U") Format Specifier.

            
         
            ///sleep
            System.Threading.Thread.Sleep(1000);
            //
            /////////
            product[1].intensity = 1002;

            product[1].nonverbal_factor = "HTC";

            product[1].time = System.DateTime.Now.ToString("u");//The Universal Full ("U") Format Specifier.
            ///sleep
            System.Threading.Thread.Sleep(1000);
            //
            /////////
            product[2].intensity = 1003;

            product[2].nonverbal_factor = "Sony";

            product[2].time = "-";//The Universal Full ("U") Format Specifier.
           


            JSonHelper helper0 = new JSonHelper();

            string jsonResult = helper0.ConvertObjectToJSon(product);
            Console.WriteLine("jsonResult Converter test" + jsonResult);


           


            //string json= obj.ToJSON();

            Console.WriteLine(jsonResult);
            //json = System.DateTime.Now.ToString("u");//The Universal Full ("U") Format Specifier.
            //Console.Write(json);
         
            //tcp 
            //send "hi there" to server,and this senetence should show up in server's terminal.
            TcpClient tcpclnt = new TcpClient();
            label1.Text="Connecting.....";
            tcpclnt.Connect("54.191.185.244", 8084); // use the ipaddress as in the server program
            label1.Text="Connected";
           
            Stream stm = tcpclnt.GetStream();
  
            //encode UTF8
            Byte[] ba = Encoding.UTF8.GetBytes(jsonResult);
            
            //encode ASCII
            /*
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(jsonResult);
            */
             label1.Text="Transmitting.....";
            stm.Write(ba, 0, ba.Length);

            tcpclnt.Close();


        }
    }
}
