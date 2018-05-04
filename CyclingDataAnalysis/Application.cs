using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace CyclingDataAnalysis
{
    public partial class Application : MaterialForm
    {

        private OpenFileDialog openFileDialog1 = new OpenFileDialog();

        //defining array variables to store file contents 

        private string[] fileLines;



        public Application()
        {
            InitializeComponent();
        }
        /// <summary>
        /// loading the methods to be executed in the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            selectGraph.Hide();
            panel2.Hide();
            panel3.Hide();

            browseFile();
            dataDisplay();
            calculations();

            showGraph();
            interval_detection();


        }

    



    

        /// <summary>
        /// defining class for headername
        /// </summary>

        public class header
        {
            public string headername { get; set; }
            public int headerline { get; set; }


        }


        //creating object for header class
        List<header> headerlist = new List<header>();


        /// <summary>
        /// method promoting to input file
        /// </summary>
        
        public void browseFile()
        {

            // string filename = openFileDialog1.FileName;

            //    System.IO.StreamReader file = new System.IO.StreamReader(filename);
            string filename = StartPage.finame();
                fileLines = File.ReadAllLines(filename);
                // string newLine;

                for (int i = 0; i < fileLines.Length; i++)
                {
                    if (fileLines[i].Length > 0)
                    {
                       //checking the first and last char of the word in a line  to find header
                        if (fileLines[i][0] == '[' && fileLines[i][fileLines[i].Length - 1] == ']')
                        {
                            //adding headername and headerline in headerlist object of header class
                            headerlist.Add(new header { headername = fileLines[i].Substring(1, fileLines[i].Length - 2), headerline = i });
                           
                        }
                    
                }


            }
            //calling method to store particular set of data in objects

            datacheck();

        }

        
        /// <summary>
        /// creating object to store HRData Values
        /// </summary>

        public class HRData
        {

            //the parameters are named as corresponding to the data given for version 106
            public string HeartRate { get; set; }
            public string Speed { get; set; }
            public string Cadence { get; set; }
            public string Altitude { get; set; }

            public string Power { get; set; }
            public string PowerBalancePedalling { get; set; }
         //   public int PowerBalancePedalling { get; set; }

            //this parameter is found only in version 107
            public string AirPressure { get; set; }

        }

        //getting the data  for HRData using objects
         List<HRData> hrdata = new List<HRData>();
        List<string> power = new List<string>();

        

        //creating object for HRData
        HRData hr = new HRData();
        //creating class objects to store params values;

        /// <summary>
            /// Creating params class to store paramas value from file
         /// </summary>
           public class Params
            {
            public string  Version{ get; set; }
            public string Monitor { get; set; }
            public string SMode { get; set; }
            public string Date { get; set; }
            public string StartTime { get; set; }
            public string Length { get; set; }
            public string Interval { get; set; }
            public string Upper1 { get; set; }
            public string Lower1 { get; set; }
            public string Upper2 { get; set; }
            public string Lower2 { get; set; }
            public string Upper3 { get; set; }
            public string Lower3 { get; set; }
            public string Timer1 { get; set; }
            public string Timer2 { get; set; }
            public string Timer3 { get; set; }
            public string ActiveLimit { get; set; }
            public string MaxHR { get; set; }
            public string RestHR { get; set; }
            public string StartDelay { get; set; }
            public string VO2max { get; set; }
            public string Weight{ get; set; }
        }

        //creating an jobect for this class
         Params param1 = new Params();

        //defining smode
        //  int smodeLength;
        char[] smodeDataValues;
        string smodeData;
      
       
        //defining a variable
        int lineno;


        /// <summary>
        /// checking the data from headerlist and adding it in HRData methods
        /// </summary>
        public void datacheck()
        {
            //checking the filelist array with HRData header
            foreach (header a in headerlist)
            {
                lineno = a.headerline;

                //checking the condition
                switch (a.headername) {
                    case "Params":
                        {
                            insertParamsValue();



                            break;
                        }
                    case "HRData":
                        {


                            //using the loop to till the end of array to get values 
                            for (int j = lineno + 1; j < fileLines.Length; j++)
                            {
                                //seperating the values with tabs
                                string[] newline = fileLines[j].Split('\t');

                                int value = newline.Length;
                                //adding the version name 
                                switch (param1.Version) {

                                    case "105":
                                        {
                                            hrdata.Add(new HRData
                                            {
                                                HeartRate = newline[0],
                                                Speed = newline[1],
                                                Cadence =newline[2]
                                            });

                                            break;
                                        }
                                    case "106":
                                        {
                                           
                                            //adding the values to the parameters
                                            if (value == 6)
                                            {
                                               
                                                hrdata.Add(new HRData
                                                {
                                                    HeartRate = newline[0],
                                                    Speed = newline[1],
                                                    Cadence = newline[2],
                                                    Altitude = newline[3],
                                                    Power = newline[4],
                                                    // PowerBalancePedalling = int.Parse(newline[5])
                                                    PowerBalancePedalling = newline[5]
                                                });
                                            }
                                            else
                                            {
                                                hrdata.Add(new HRData
                                                {
                                                    HeartRate = newline[0],
                                                    Speed = newline[1],
                                                    Cadence = newline[2],
                                                    Altitude = newline[3],
                                                    Power = newline[4]
                                                    //  PowerBalancePedalling = null
                                                });
                                            }
                                          


                                            break;
                                        }
                                    case "107": {
                                            //adding the values to the parameters
                                            hrdata.Add(new HRData
                                            {
                                                HeartRate = newline[0],
                                                Speed = newline[1],
                                                Cadence = newline[2],
                                                Altitude = newline[3],
                                                Power = newline[4],
                                                PowerBalancePedalling = newline[5],
                                                //  PowerBalancePedalling = int.Parse(newline[5]),
                                                AirPressure = newline[6]
                                            });
                                            break;
                                        }
                                }
                            }
                            break;
                        }

                }
            }
        }

        /// <summary>
        /// defining string array to store params value from the file
        /// </summary>
        string[] paramsValue = new string[22];

        /// <summary>
        /// defining a methd to store the params value in their respective values.
        /// </summary>
        private void insertParamsValue()
        {
            int j = lineno + 1;
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

            } while (j < 24);

            storeParamsValue();
        }

        /// <summary>
        /// storing the value of params
        /// </summary>
        private void storeParamsValue()
        {

            //paramsData.Add(new Params
            //inserting data in the params
            param1.Version = paramsValue[0];
            param1.Monitor = paramsValue[1];
            param1.SMode = paramsValue[2];
            param1.Date = paramsValue[3];
            param1.StartTime = paramsValue[4];
            param1.Length = paramsValue[5];
            param1.Interval = paramsValue[6];
            param1.Upper1 = paramsValue[7];
            param1.Lower1 = paramsValue[8];
            param1.Upper2 = paramsValue[9];
            param1.Lower2 = paramsValue[10];
            param1.Upper3 = paramsValue[11];
            param1.Lower3 = paramsValue[12];
            param1.Timer1 = paramsValue[13];
            param1.Timer2 = paramsValue[14];
            param1.Timer3 = paramsValue[15];
            param1.ActiveLimit = paramsValue[16];
            param1.MaxHR = paramsValue[17];
            param1.RestHR = paramsValue[18];
            param1.StartDelay = paramsValue[19];
            param1.VO2max = paramsValue[20];
            param1.Weight = paramsValue[21];


            //adding the getter and setter

            SetTime();

            //adding vlaues to the labels of the application
            monitorValue.Text = param1.Monitor;
            Date.Text = paramDate;
            StartTime.Text =timestart;
            Interval.Text = param1.Interval;
            startDelayValue.Text = param1.StartDelay;
            activeLimitValue.Text = param1.ActiveLimit;
            vmaxValue.Text = param1.VO2max;
            weightValue.Text = param1.Weight;
            lengthValue.Text = param1.Length;
            versionValue.Text = param1.Version;
            smodeValue.Text = param1.SMode;
        //    smodeLength = param1.SMode.Length;
            smodeData = param1.SMode;
         
            smodeDataValues = smodeData.ToCharArray();
            //inserting the vaues in datagrid view 
            // dataDisplay();
        }

        //creating datatables 
        DataTable dt = new DataTable();

        /// <summary>
        /// method to define the column of datagridview and then displaying the values
        /// </summary>
        private void dataDisplay() {

            SetTime();
           //putting column name is data grid view as per version of parmas
            string[] columnNames = {"Time"," HeartRate"," Speed", " Cadence"," Altitude","Power","PowerBalancePedalling","AirPressure" };
            int j = 0;
            int k = Convert.ToInt32(param1.Interval); 
            
            //adding data in data grid view as per version 
            switch (param1.Version)
            {
                case "105":
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            dt.Columns.Add(columnNames[i]);
                        }
                        foreach (HRData hd in hrdata)
                        {
                            dt.Rows.Add(viewDate.AddSeconds(j).ToLongTimeString(),
                                hd.HeartRate, 
                                hd.Speed, 
                                hd.Cadence);
                            j+=k;
                        }
                        break;
                    }
                case "106":
                    {
                       

                            for (int i = 0; i < 7; i++)
                        {
                            dt.Columns.Add(columnNames[i]);

                        }
                        

                        //adding row for the datagridview
                        foreach (HRData hd in hrdata)
                        {
                           /* if (smodeDataValues[0] == '0')
                            {
                                hd.Speed = 0;
                            }
                            */


                            if (hd.PowerBalancePedalling != null)
                            {
                               

                                dt.Rows.Add(viewDate.AddSeconds(j).ToLongTimeString()
                                    ,hd.HeartRate, 
                                    hd.Speed, 
                                    hd.Cadence,
                                    hd.Altitude,
                                    hd.Power, 
                                    hd.PowerBalancePedalling);
                                j += k;
                            } else
                            {
                                dt.Rows.Add(viewDate.AddSeconds(j).ToLongTimeString(),
                                    hd.HeartRate,
                                    hd.Speed, 
                                    hd.Cadence,
                                    hd.Altitude, 
                                    hd.Power, 
                                    null);
                                j += k;
                            }
                        }
                       

                        break;
                    }
                case "107":
                    {

                        foreach (string col in columnNames)
                        {
                            dt.Columns.Add(col);
                        }
                        
                        foreach (HRData hd in hrdata)
                        {


                            dt.Rows.Add(viewDate.AddSeconds(j).ToLongTimeString(),
                                hd.HeartRate,
                                hd.Speed,
                                hd.Cadence,
                                hd.Altitude, 
                                hd.Power, 
                                hd.PowerBalancePedalling, 
                                hd.AirPressure);
                            j += k;
                        }
                        break;
                    }

                   
            }
           // if (smodeDataValues[0] == '0')
           // {
               // dt.Columns[2] = 0;
           // }
            DataDisplay.DataSource = dt;
           
        }



        //variables initiated for heart rate
        double minHeartRate = 1000;
        double maxHeartRate = 0, sum = 0;
        double count = 0;

        //variables for altitude ,power,speed

        double minAltitude = 1000;
        double maxAltitude = 0;
        double minPower = 1000;
        double maxPower = 0;
        double minSpeed = 1000;
        double maxSpeed = 0;
        double minCadence = 1000;
        double maxCadence = 0;
      
        //defining calculation variables
        double count1 = 0, sum1 = 0;
        double distance;
        double speed;
        double sum2 = 0, sum3 = 0;
        double TP;
        /// <summary>
        /// method to calculate maximum,minimum values of parameters
        /// </summary>
        /// 
        private void calculations()
        {
            //calculating the data to find maximum,minimum

            foreach (HRData value in hrdata)
            {
                //for heart rate
                double hrValue =double.Parse( value.HeartRate);
                if (hrValue < minHeartRate)
                {
                    minHeartRate = hrValue;
                }
                else if (hrValue > maxHeartRate)
                {
                    maxHeartRate = hrValue;
                }
                sum += hrValue;
                count++;
                //for altitude 
                double altValue =double.Parse( value.Altitude);
                if (altValue < minAltitude)
                {
                    minAltitude = altValue;
                }
                else if (altValue > maxAltitude)
                {
                    maxAltitude = altValue;
                }
                sum2 += altValue;
                //for power
                double PowerValue = double.Parse(value.Power);
                if (PowerValue < minPower)
                {
                    minPower = PowerValue;
                }
                else if (altValue > maxPower)
                {
                    maxPower = PowerValue;
                }
                sum3 += PowerValue;
                //speed

                double SpeedValue = double.Parse(value.Speed);
                if (SpeedValue < minSpeed)
                {
                    minSpeed = SpeedValue;
                }
                else if (SpeedValue > maxSpeed)
                {
                    maxSpeed = SpeedValue;
                }
                sum1 += SpeedValue;
                count1++;
                //caderance
                double CadenceValue = double.Parse(value.Cadence);
                if (CadenceValue < minCadence)
                {
                    minCadence = CadenceValue;
                }
                else if (SpeedValue > maxCadence)
                {
                    maxCadence = SpeedValue;
                }


            }
             double averagePower = sum3/count;

            ////Calculating Functional Threshold Power
            double FTP = averagePower * 0.05;
            double totalFTP = averagePower - FTP;
            //  pb.Text = totalFTP.ToString();
            pb.Text = Math.Round(averagePower * 0.95, 2).ToString();

            ////Calculating Normalized Power
            double f = Math.Pow(averagePower, 4.0);
            double m = f * 66.0;
            double time = m / 60.0;
            double np1 = Math.Pow(time, (1.0 / 4));
            np.Text = Math.Round(np1,2).ToString();

            ////Calculating Intensity Factor
            double IF = np1 / totalFTP;
            ifactor.Text = Math.Round(IF,2).ToString();

            ////Calculating TSS
            double tss1 = ((3960 * np1 * IF) / (totalFTP * 3600)) * 100;
            tss.Text = Math.Round(tss1,2).ToString();

            //calculating TP
            double pbal = Math.Round(averagePower * 0.95, 2);
            TP = Math.Round(pbal * 1.05, 2);
            //calculating the stats of heart rate

            averageHR.Text = Math.Round((sum / count),2).ToString() +" BPM";
            maxHR.Text = maxHeartRate.ToString()+ " BPM";
            minHR.Text = minHeartRate.ToString()+" BPM";

            //calculating for altitude ,power ,speed
            maxiPower.Text = maxPower.ToString()+ " watts";
            miniPower.Text = minPower.ToString() + " watts";
            avPower.Text=Math.Round((sum3 /count),2).ToString() + " watts";
            //in feet 
            double fmaxAltitude = maxAltitude * 3.28;
            double fminAltitude = minAltitude * 3.28;
            maxiAltitude.Text = maxAltitude.ToString()+" m"+"("+fmaxAltitude+" ft)";
            miniAltitude.Text = minAltitude.ToString() + " m" + "(" + fminAltitude + " ft)";
            avAlti.Text = Math.Round((sum2 / count),2).ToString() + " m";

            maxiSpeed.Text = (maxSpeed/10).ToString()+" km/hr";
            miniSpeed.Text = Math.Round((minSpeed/10),2).ToString()+" km/hr";

            //calulating the total distance covered
            speed = sum1 / (count1*10);
            double interval = double.Parse(param1.Interval);
            double totalDataCount = hrdata.Count * interval;
        
            double  totalTime = (double)totalDataCount/3600;
            distance =  speed * totalTime;
            totalDistance.Text = Math.Round(distance,2).ToString() + "km";


           
        }
    
       
        
       
        /// <summary>
        /// button to exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        /// <summary>
        /// button to call the datagridview tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewData_Click(object sender, EventArgs e)
        {
            DataHR.SelectTab(1);
           //viewData.SelectedIndex = hrdata1;
        }

        
      
        /// <summary>
        /// defining the variables for the graph
        /// </summary>
        string paramDate;
        static DateTime start, duration, viewDate;
        string timestart;
      

        /// <summary>
        /// method to convert different values of parameter for different measurment units
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lengthMeasure_CheckedChanged(object sender, EventArgs e)
        {
            if (lengthMeasure.Checked)
            {
                double newDistance =distance*0.62 ;
                double newD = (double)newDistance;

                double newMaxSpeed =(maxSpeed/10)*0.62;
                double newMinSpeed=(minSpeed/10)*0.62;
                maxiSpeed.Text = newMaxSpeed.ToString() + "miles/hr";
                miniSpeed.Text = newMinSpeed.ToString() + "miles/hr";
                totalDistance.Text = Math.Round(newD,2).ToString()+"Miles";

            }else
            {

                maxiSpeed.Text = (maxSpeed / 10).ToString() + "km/hr";
                miniSpeed.Text = (minSpeed / 10).ToString() + "km/hr";
                totalDistance.Text =Math.Round( distance,2).ToString() + "km";
            }
           
        }

        /// <summary>
        /// defining various time and date variable to be executed in graphs
        /// </summary>
        private void SetTime()
        {
            //defining variables to get time and startdate
            string startDate = param1.Date;
            string time1 = param1.StartTime;

            //using substring to get year,month,day
            int year = Convert.ToInt32(startDate.Substring(0, 4));
            int month = Convert.ToInt32(startDate.Substring(4, 2));
            int day = Convert.ToInt32(startDate.Substring(6, 2));

            //getting hour,minute,second
            paramDate = year.ToString() + "/" + month.ToString() + "/" + day.ToString();
            string[] timeValues = time1.Split(':');
            int hour = Convert.ToInt32(timeValues[0]);
            int min = Convert.ToInt32(timeValues[1]);
            int sec = (int)Convert.ToDouble(timeValues[2]);
           

            //using datetime to set time and date
            viewDate = new DateTime(year, month, day, hour, min, sec);
            timestart = viewDate.ToLongTimeString();

            start = new DateTime(year, month, day, hour, min, int.Parse(param1.Interval));
            duration = start.AddSeconds(double.Parse(param1.Interval));
        }
        /// <summary>
        /// for calculating and displaying interval detection
        /// </summary>
        private void interval_detection()
        {
            DataTable dt_interval = new DataTable();
            dt_interval.Columns.Add("Intervals");
            dt_interval.Columns.Add("Heart Rate(bpm)");
            dt_interval.Columns.Add("Speed(km/hr)");
            dt_interval.Columns.Add("Cadence(RPM)");
            dt_interval.Columns.Add("Altitude(m)");
            dt_interval.Columns.Add("Power(watts)");


            int interval_no = 0;
            bool isInterval = false;
             
            for (int d = 0; d < hrdata.Count(); d++)
            {
                //case1
                if (isInterval == false)
                {
                    int j ;
                    for (j = 0; j < 10; j++)
                    {
                        if (double.Parse(hrdata[d + j].Power) <= TP)
                            break;
                    }
                    if (j == 10)
                    {
                        isInterval = true;
                        interval_no ++;
                    }
                }
                //case2 
                if (isInterval == true)
                {
                    if (double.Parse(hrdata[d].Power )> TP)
                        dt_interval.Rows.Add("Interval" + interval_no,
                            hrdata[d].HeartRate,
                            hrdata[d].Speed,
                            hrdata[d].Cadence,
                            hrdata[d].Altitude,
                            hrdata[d].Power);
                    else
                        isInterval = false;
                }


            }
            intDetect.DataSource= dt_interval;

            DataGridViewRow row;
            interval_no = 1;

            string interva;
            string interval_name = "Interval1";

            double interval_heartrSum = 0.0, interval_speeedSum = 0.0, interval_cade = 0.0, interval_alti = 0.0, interval_pow = 0.0;
            int rows = 0, count = 0;

            DataTable dt_interval1 = new DataTable();
            dt_interval1.Columns.Add("Intervals");
            dt_interval1.Columns.Add("Heart Rate(bpm)");
            dt_interval1.Columns.Add("Speed(km/hr)");
            dt_interval1.Columns.Add("Cadence(RPM)");
            dt_interval1.Columns.Add("Altitude(m)");
            dt_interval1.Columns.Add("Power(watts)");

            intervalC.DataSource = dt_interval1;
            do
            {
           
                row = intDetect.Rows[rows];

                interva = (row.Cells[0].Value.ToString());

                if (interva.Equals(interval_name))
                {
                    interval_heartrSum += Convert.ToDouble(row.Cells[1].Value);
                    interval_speeedSum += Convert.ToDouble(row.Cells[2].Value);
                    interval_cade += Convert.ToDouble(row.Cells[3].Value);
                    interval_alti += Convert.ToDouble(row.Cells[4].Value);
                    interval_pow += Convert.ToDouble(row.Cells[5].Value);

                    count++;
                }
                else
                {
                   
                    dt_interval1.Rows.Add(interval_name,
                        Math.Round(interval_heartrSum / count,2),
                       Math.Round(interval_speeedSum / count, 2),
                       Math.Round(interval_cade / count, 2),
                       Math.Round(interval_alti / count, 2),
                       Math.Round(interval_pow / count, 2));
                    count = 0;
                    interval_name = interva;
                    interval_heartrSum = 0;
                    interval_speeedSum = 0;
                    interval_pow = 0;
                    interval_alti = 0;
                    interval_cade = 0;
                }

                rows++;
            }
            while (rows < intDetect.Rows.Count - 1);

            dt_interval1.Rows.Add(interval_name,
                Math.Round(interval_heartrSum / count, 2),
                         Math.Round(interval_speeedSum / count,2),
                         Math.Round(interval_cade / count,2),
                         Math.Round(interval_alti / count,2),
                         Math.Round(interval_pow / count,2));
        }

        public static string starttimeGraph;
        /// <summary>
        /// Selectable Information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataDisplay_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            List<double> heartRate = new List<double>();
            List<double> speed = new List<double>();
            List<double> cadence = new List<double>();
            List<double> power = new List<double>();
            List<double> altitude = new List<double>();
            List<string> starttime = new List<string>();

            int grdCount = DataDisplay.SelectedRows.Count;

            foreach (DataGridViewRow row in DataDisplay.SelectedRows)
            {
                starttime.Add(row.Cells[0].Value.ToString());
                heartRate.Add(Convert.ToDouble(row.Cells[1].Value));
                speed.Add(Convert.ToDouble(row.Cells[2].Value));
                cadence.Add(Convert.ToDouble(row.Cells[3].Value));
                power.Add(Convert.ToDouble(row.Cells[4].Value));
                altitude.Add(Convert.ToDouble(row.Cells[5].Value));
            }

           
           

            starttimeGraph = starttime[0];
            double[] hr = heartRate.ToArray();
            double[] sp = speed.ToArray();
            double[] cd = cadence.ToArray();
            double[] al = altitude.ToArray();
            double[] po = power.ToArray();
           int chunkDivision ;
            using (var form = new ChunkData())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    chunkDivision = form.chunkGet;
                    ChunkDataSummary ck = new ChunkDataSummary(chunkDivision, hr, sp, cd, al, po);
                    ck.Show();
                }
            }
           

            PointPairList hrPairList = new PointPairList();
            PointPairList spPairList = new PointPairList();
            PointPairList cdPairList = new PointPairList();
            PointPairList alPairList = new PointPairList();
            PointPairList poPairList = new PointPairList();



            double avgHR = 0;
            double avgSP = 0;
            double avgCD = 0;
            double avgAL = 0;
            double avgPO = 0;

            double n = 0;

            for (int count = 0; count < hr.Length; count++)
            {
                avgAL = avgAL + al[count];
                avgHR = avgHR + hr[count];
                avgSP = avgSP + sp[count];
                avgCD = avgCD + cd[count];
                avgPO = avgPO + po[count];
                n = n + 1;

                hrPairList.Add(count, hr[count]);
                spPairList.Add(count, sp[count]);
                cdPairList.Add(count, cd[count]);
                alPairList.Add(count, al[count]);
                poPairList.Add(count, po[count]);
            }


            drawgraph(hrPairList, spPairList, cdPairList, alPairList, poPairList);

            //DataDisplay.Hide();
            read1.Hide();
            DataHR.SelectTab(5);
            selectGraph.Show();
            panel2.Show();
            panel3.Show();


            //summay
            avgAL = Math.Round(avgAL / n, 2);
            avgHR = Math.Round(avgHR / n, 2);
            avgCD = Math.Round(avgCD / n, 2);
            avgPO = Math.Round(avgPO / n, 2);
            avgSP = Math.Round(avgSP / n, 2);

            double FTP1 = avgPO * 0.05;
            double totalFTP1 = avgPO - FTP1;
            ahr1.Text = avgHR.ToString();
            as1.Text = avgSP.ToString();
            //avgCD 
            aa1.Text = avgAL.ToString();
            ap1.Text= avgPO.ToString();

            //advanced metrics
            pb2.Text = Math.Round(avgPO * 0.95, 2).ToString();

            ////Calculating Normalized Power
            double f = Math.Pow(avgPO, 4.0);
            double m = f * 66.0;
            double time = m / 60.0;
            double np1 = Math.Pow(time, (1.0 / 4));
            np2.Text = Math.Round(np1,2).ToString();

            ////Calculating Intensity Factor
            double IF = np1 / totalFTP1;
            if2.Text = Math.Round(IF,2).ToString();

            ////Calculating TSS
            double ts2 = ((3960 * np1 * IF) / (totalFTP1 * 3600)) * 100;
            tss2.Text = Math.Round(ts2,2).ToString();

        }

        LineItem curve1, curve2, curve3, curve4, curve5;
        ZedGraphControl summaryZedGraph;
        GraphPane myPane1;
        public void drawgraph(PointPairList hrPairList, PointPairList spPairList, PointPairList cdPairList, PointPairList alPairList, PointPairList poPairList)
        {
             summaryZedGraph = selectGraph;
             myPane1 = summaryZedGraph.GraphPane;

            myPane1.CurveList.Clear();
            myPane1.XAxis.Title.Text = "time in seconds";
            myPane1.YAxis.Title.Text = "Values of HRData";



            //using substring to get year,month,day
            string startDate1 = param1.Date;
            int year1 = Convert.ToInt32(startDate1.Substring(0, 4));
            int month1 = Convert.ToInt32(startDate1.Substring(4, 2));
            int day1 = Convert.ToInt32(startDate1.Substring(6, 2));

            DateTime start1, duration1;
            string time1 = starttimeGraph.ToString();
            string[] timeValues = time1.Split(':');
            int hour = Convert.ToInt32(timeValues[0]);
            int min = Convert.ToInt32(timeValues[1]);
            // int sec = (int)Convert.ToDouble(timeValues[2]);
            start1 = new DateTime(year1, month1, day1, hour, min, int.Parse(param1.Interval));
            duration1 = start1.AddSeconds(double.Parse(param1.Interval));
            //////defining x axis
           // myPane1.XAxis.Type = AxisType.Date;
           //myPane1.XAxis.Scale.Format = "HH:mm:ss";
           // myPane1.XAxis.MinorGrid.IsVisible = true;
           // myPane1.XAxis.MajorGrid.IsVisible = true;

           // //////setting the intervals
           // myPane1.XAxis.Scale.Min = new XDate(start1);
           // myPane1.XAxis.Scale.Max = new XDate(duration1);
           //// myPane1.XAxis.Scale.MinorUnit = DateUnit.Second;
           // myPane1.XAxis.Scale.MajorUnit = DateUnit.Second;




            curve1 = myPane1.AddCurve("Heart Rate", hrPairList, Color.Red, SymbolType.Square);
            curve2 = myPane1.AddCurve("Speed", spPairList, Color.Blue, SymbolType.Square);
            curve3 = myPane1.AddCurve("Cadence", cdPairList, Color.Yellow, SymbolType.Square);
            curve4 = myPane1.AddCurve("Altitude", alPairList, Color.Green, SymbolType.Square);
            curve5 = myPane1.AddCurve("Power", poPairList, Color.Pink, SymbolType.Square);

            summaryZedGraph.AxisChange();
            summaryZedGraph.Invalidate();
        }

        /// <summary>
        /// checkbox to show altitudegraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void altitudeGraph_CheckedChanged(object sender, EventArgs e)
        {
            if (smodeDataValues[2] == '0')
            {
                altitudeGraph.Visible = false;
            }
            else
            {

                if (altitudeGraph.Checked)
                {
                    altCurve.IsVisible = true;
                    zgc.Invalidate();
                }
                else
                {
                    altCurve.IsVisible = false;
                    zgc.Invalidate();
                }
            }

        }


        /// <summary>
        /// defining credentials to make graph in zedgraph
        /// </summary>
        GraphPane graphPanel;

        ZedGraphControl zgc;

        LineItem hrCurve, spdCurve, altCurve, cadCurve, pwrCurve;


        /// <summary>
        /// method to create and display graph
        /// </summary>
        private void showGraph()
        {
            
            //defining point pair list for thread curves
            PointPairList heartratePPList = new PointPairList();
            PointPairList speedPPList = new PointPairList();
            PointPairList altitudePPList = new PointPairList();
            PointPairList caderancePPList = new PointPairList();
            PointPairList powerPPList = new PointPairList();


            double x, yaxis1, y2, y3, y4, y5;

            //defining the graph panel
            zgc = zedGraphStatic;
            graphPanel = zgc.GraphPane;
            zgc.IsZoomOnMouseCenter = true;
            zgc.IsEnableHZoom = true;
            zgc.IsEnableVZoom = false;

            //defining headers for graph
            graphPanel.Chart.Border.IsVisible = false;
            graphPanel.Title.Text = "Static Graph for the HRData";
            graphPanel.XAxis.Title.Text = "Time (minutes)";
            graphPanel.IsAlignGrids = true;

            //max and min values of lists are used as max and min scale in respective yaxis
            
            //defining yaxis for heart rate
            
            graphPanel.YAxis.Title.Text = "Heart Rate(BPM)";
            graphPanel.YAxis.Color = Color.Red;
            graphPanel.YAxis.Scale.Max =maxHeartRate ;
            graphPanel.YAxis.Scale.Min = minHeartRate;

                //defining yaxis for speed
           
                graphPanel.Y2Axis.IsVisible = true;
                graphPanel.Y2Axis.Title.Text = "Speed(km/hr)";
                graphPanel.Y2Axis.Color = Color.Green;
                graphPanel.Y2Axis.Scale.Max = maxSpeed;
                graphPanel.Y2Axis.Scale.Min = minSpeed;
            
            //defining yaxis for cadence
            var Y3Axis = graphPanel.AddYAxis("Cadence");
            graphPanel.YAxisList[Y3Axis].Color = Color.Blue;
            graphPanel.YAxisList[Y3Axis].Scale.Max = maxCadence;
            graphPanel.YAxisList[Y3Axis].Scale.Min = minCadence;

            //defining yaxis for altitude
            var Y4Axis = graphPanel.AddYAxis("Altitude(m)");
            graphPanel.YAxisList[Y4Axis].Color = Color.Gray;
            graphPanel.YAxisList[Y4Axis].Scale.Max =maxAltitude;
            graphPanel.YAxisList[Y4Axis].Scale.Min =minAltitude;

            //defining yaxis for power
            var Y5Axis = graphPanel.AddYAxis("Power(Watts)");
            graphPanel.YAxisList[Y5Axis].Color = Color.Magenta;
            graphPanel.YAxisList[Y5Axis].Scale.Max = maxPower;
            graphPanel.YAxisList[Y5Axis].Scale.Min = minPower;

  
            //defining x axis
            graphPanel.XAxis.Type = AxisType.Date;
            graphPanel.XAxis.Scale.Format = "HH:mm:ss";
            graphPanel.XAxis.MinorGrid.IsVisible = true;
            graphPanel.XAxis.MajorGrid.IsVisible = true;

            SetTime();

          

        
            int i = 0;
           //adding data
            foreach(HRData hr in hrdata)
            {
               
                x = (double)new XDate(start.AddSeconds(i));
                yaxis1 = double.Parse(hr.HeartRate);
                y2 =double.Parse( hr.Cadence);
                y3 =double.Parse( hr.Speed);
                y4 = double.Parse(hr.Altitude);
                y5 = double.Parse(hr.Power);

                //points added to point pair list
                heartratePPList.Add(x, yaxis1);
                caderancePPList.Add(x, y2);
                speedPPList.Add(x, y3);
                altitudePPList.Add(x, y4);
                powerPPList.Add(x, y5);
                i++;
            }

            //adding curves to zgc
            hrCurve = graphPanel.AddCurve("HeartRate",
                 heartratePPList, Color.Red, SymbolType.None);


            if (smodeDataValues[1] == '1')
            {
                cadCurve = graphPanel.AddCurve("Cadence",
                 caderancePPList, Color.Blue, SymbolType.None);
                cadCurve.YAxisIndex = Y3Axis;
            }

            if (smodeDataValues[0] == '1')
            {
                spdCurve = graphPanel.AddCurve("Speed",
                     speedPPList, Color.Green, SymbolType.None);
                spdCurve.IsY2Axis = true;
            }

            if (smodeDataValues[2] == '1')
            {
                altCurve = graphPanel.AddCurve("Altitude",
                 altitudePPList, Color.Gray, SymbolType.None);
                altCurve.YAxisIndex = Y4Axis;
            }

            if (smodeDataValues[3] == '1')
            {
                pwrCurve = graphPanel.AddCurve("Power",
                 powerPPList, Color.Magenta, SymbolType.None);
                pwrCurve.YAxisIndex = Y5Axis;
            }

         //   SetSize();
            zgc.AxisChange();
            //refresh
            zgc.Invalidate();
        }

       
        /// <summary>
        /// checking the checkbox to display hrgraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hrGraph_CheckedChanged(object sender, EventArgs e)
        {

            if (hrGraph.Checked)
            {
                hrCurve.IsVisible = true;
                zgc.Invalidate();
            }
            else
            {
                hrCurve.IsVisible = false;
                zgc.Invalidate();
            }

        }

        /// <summary>
        /// checking the checkbox to display speed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void speedGraph_CheckedChanged(object sender, EventArgs e)
        {
            if (smodeDataValues[0] == '0')
            {
                speedGraph.Visible = false;
            }
            else
            {
                if (speedGraph.Checked)
                {
                    spdCurve.IsVisible = true;
                    zgc.Invalidate();
                }
                else
                {
                    spdCurve.IsVisible = false;
                    zgc.Invalidate();
                }
            }
        }

        /// <summary>
        /// checking checkbox to display cadencegraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cadenceGraph_CheckedChanged(object sender, EventArgs e)
        {
            if (smodeDataValues[1] == '0')
            {
                cadenceGraph.Visible = false;
            }
            else
            {

                if (cadenceGraph.Checked)
                {
                    cadCurve.IsVisible = true;
                    zgc.Invalidate();
                }
                else
                {
                    cadCurve.IsVisible = false;
                    zgc.Invalidate();
                }
            }
        }

        private void pb_Click(object sender, EventArgs e)
        {

        }

        private void versionValue_Click(object sender, EventArgs e)
        {

        }
/// <summary>
/// for checking heart rate in selectable info
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void sheartrate_CheckedChanged(object sender, EventArgs e)
        {
            if (sheartrate.Checked)
            {
                curve1.IsVisible = true;
                summaryZedGraph.Invalidate();
            }
            else
            {
                curve1.IsVisible = false;
                summaryZedGraph.Invalidate();
            }
        }
        /// <summary>
        /// to restore zoom in selectable graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void restoreZoom_Click(object sender, EventArgs e)
        {
            summaryZedGraph.ZoomOutAll(myPane1);
            summaryZedGraph.Invalidate();
        }

        /// <summary>
        /// for uploading multiple files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void multiplefile_Click(object sender, EventArgs e)
        {
            new InputFiles().Show();
        }

        private void filelink1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void filelink2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }





        //defining array variables to store file contents 








        /// <summary>
        /// for checking speed in selectable info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sspeed_CheckedChanged(object sender, EventArgs e)
        {
            if (sspeed.Checked)
            {
                curve2.IsVisible = true;
                summaryZedGraph.Invalidate();
            }
            else
            {
                curve2.IsVisible = false;
                summaryZedGraph.Invalidate();
            }
        }
        /// <summary>
        /// for checking altitude in selectable info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void salltitude_CheckedChanged(object sender, EventArgs e)
        {
            if (salltitude.Checked)
            {
                curve4.IsVisible = true;
                summaryZedGraph.Invalidate();
            }
            else
            {
                curve4.IsVisible = false;
                summaryZedGraph.Invalidate();
            }
        }

        /// <summary>
        /// for cadence in selectable info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scadence_CheckedChanged(object sender, EventArgs e)
        {
            if (scadence.Checked)
            {
                curve3.IsVisible = true;
                summaryZedGraph.Invalidate();
            }
            else
            {
                curve3.IsVisible = false;
                summaryZedGraph.Invalidate();
            }
        }

        /// <summary>
        /// for power in selectable info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spower_CheckedChanged(object sender, EventArgs e)
        {
            if (cadenceGraph.Checked)
            {
                curve5.IsVisible = true;
                summaryZedGraph.Invalidate();
            }
            else
            {
                curve5.IsVisible = false;
                summaryZedGraph.Invalidate();
            }
        }

        private void info_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// checkbox to show powerGraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void powerGraph_CheckedChanged(object sender, EventArgs e)
        {
            if (smodeDataValues[3] == '0')
            {
                powerGraph.Visible = false;
            }
            else
            {

                if (powerGraph.Checked)
                {
                    pwrCurve.IsVisible = true;
                    zgc.Invalidate();
                }
                else
                {
                    pwrCurve.IsVisible = false;
                    zgc.Invalidate();
                }
            }
        }

        /// <summary>
        /// button to restore zoom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zoomOut_Click(object sender, EventArgs e)
        {
            zgc.ZoomOutAll(graphPanel);
            zgc.Invalidate();
        }

        //menu items
        /// <summary>
        /// menuitem to browse new file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new StartPage().Show();
            this.Close();

        }

        /// <summary>
        /// menuitem to exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }

        /// <summary>
        /// menuitem to show the data of HRdata
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataHR.SelectTab(1);

        }

        /// <summary>
        /// menuitem to show information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataHR.SelectTab(0);
        }

        /// <summary>
        /// menuitem to show calculation tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calculationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataHR.SelectTab(2);
        }

        /// <summary>
        /// menuitem to display graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void graphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataHR.SelectTab(3);
        }

        
    }
}

