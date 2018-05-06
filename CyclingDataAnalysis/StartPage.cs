using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MaterialSkin.Controls;

namespace CyclingDataAnalysis
{
    public partial class StartPage : MaterialForm
    {
        //creating object to input file  
        private OpenFileDialog openFileDialog1 = new OpenFileDialog();



        //defining array variables to store file contents 

    
        public static string filename;
        public StartPage()
        {
            InitializeComponent();
        }


/// <summary>
/// defining the class to store the data of the directory folder
/// </summary>
        class FileDetails
        {
            public string filePath { get; set; }
            public string date { get; set; }
            public string start { get; set; }
            public string length { get; set; }

        }
        List<FileDetails> fd = new List<FileDetails>();

    
        //store filePaths
      
        /// <summary>
        /// stroing all file details in a list using directory
        /// </summary>
        private void fi()
        {
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\Windows-PC\Desktop\files\");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.hrm"); //Getting Text files
            //string[] filePaths = Directory.GetFiles(@"C:\Users\User\Desktop\DataFromNewPowerMeter","*.hrm");

            foreach (FileInfo file in Files)
            {
                string[] paramsValue = new string[3];
                string[] fileLines;
                string filName=file.FullName;
                fileLines = File.ReadAllLines(filName);

                int j = 4;
                //seperating the values with tabs
                string[] newline = fileLines[j].Split('\t');
                string value = newline[0];
                int add = 0;
                int b = 1;

                do
                {
                    //looping the value string to get the original value
                    foreach (char ab in value)
                    {
                        if (ab == '=')
                        {
                            paramsValue[add] = value.Substring(b, value.Length - b);

                        }

                        b++;
                    }
                    b = 1;
                    add++;
                    j++;
                    newline = fileLines[j].Split('\t');
                    value = newline[0];

                } while (j < 7);

                fd.Add(new FileDetails {
                  filePath= filName,
                  date=paramsValue[0],
                  start= paramsValue[1],
                 length =paramsValue[2]});
            }
        }
      

      


        /// <summary>
        /// The shown method takes the input of file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseFile_Click(object sender, EventArgs e)
        {
           
            openFileDialog1.Filter = "HRM files (*.hrm)|*.hrm";
            openFileDialog1.Title="Select a File";
         
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (openFileDialog1.FileName != null)
                    {
                        // filename = openFileDialog1.FileName;
                        finalpath= openFileDialog1.FileName; ;
                        //form where the processing is done is called using an object.
                        new Application().Show();
                        this.Hide();
                    }
                }catch(Exception e1)
                {
                    MessageBox.Show("Cannot Load the file " + e1.Message);
                }
              
                    }
                }
        public   static string  fname()
        {

            return filename ;
        }

        public static string finalpath;
        public static string filepathname, filepathname1;
        public static  int counter = 0;
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
           
            string calendarDate = monthCalendar1.SelectionEnd.ToString("dd/MM/yyyy");
            sDate.Text = "";
            foreach(FileDetails data  in fd)
            {
               DateTime start1, duration1;
                string dateValue = data.date.ToString();
                string dateFormat = dateValue.Substring(0, 4) + "/" + dateValue.Substring(4, 2) + "/" + dateValue.Substring(6, 2);
                DateTime date1 = DateTime.Parse(dateFormat);
                string dateCompare=date1.ToString("dd/MM/yyyy");

                start1 = Convert.ToDateTime(data.start);
                TimeSpan time = TimeSpan.Parse(data.length.ToString());
                duration1 = start1.Add(time);
                

                if (calendarDate.Equals(dateCompare))
                {
                    counter++;
                    
                    if (counter == 1)
                    {
                        sDate.Text = "Collected Data Duration : " + start1.ToShortTimeString() + " - " + duration1.ToShortTimeString();
                        ViewFile.Text = Path.GetFileName(data.filePath).ToString();
                        filepathname = data.filePath.ToString();
                        sDate2.Hide();
                        viewFile2.Hide();
                    }else if (counter == 2)
                    {
                        sDate2.Show();
                        viewFile2.Show();
                        sDate2.Text = "Collected Data Duration : " + start1.ToShortTimeString() + " - " + duration1.ToShortTimeString();
                        viewFile2.Text = Path.GetFileName(data.filePath).ToString();
                        filepathname1 = data.filePath.ToString();
                    }
                    panel1.Visible = true;
                  //  monthCalendar1.Hide();
                   // panel1.Show();
                }
                 
            }

        }


        /// <summary>
        ///   loading the method of storing the credentials in the calendar
        /// </summary>

        private void StartPage_Load(object sender, EventArgs e)
        {
            panel1.Hide();
            fi();
        }

        /// <summary>
        /// 
        /// sending the more details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sDate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            finalpath = filepathname;
            new Application().Show();
        }
        public static string finame()
        {
            
                return finalpath;
            

           
        }

        private void mfile_Click(object sender, EventArgs e)
        {
            new InputFiles().Show();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void viewFile2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            finalpath = filepathname1;
            new Application().Show();
        }
    }
}
