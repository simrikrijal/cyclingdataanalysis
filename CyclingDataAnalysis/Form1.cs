using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace CyclingDataAnalysis
{
    public partial class Form1 : MaterialForm
    {

        string fileData, filename;
        string lengthValue, startTimeValue, intervalValue;

        //TimeSpan[] totalTime = new TimeSpan[500000]; 

        int count = 0;
        IDictionary<string, string> param = new Dictionary<string, string>();
        public static double averageSpeed { get; set; }
        public static double maxSpeed { get; set; }
        public static double averageSpeedMiles { get; set; }
        public static double maxSpeedMiles { get; set; }
        public static double averageHeartRate { get; set; }
        public static double maxHeartRate { get; set; }
        public static double minHeartRate { get; set; }
        public static double averagePower { get; set; }
        public static double maxPower { get; set; }
        public static double averageAltitude { get; set; }
        public static double maxAltitude { get; set; }
        public static double averageAltitudeMile { get; set; }
        public static double maxAltitudeMile { get; set; }
        public static double totalDistance { get; set; }
        public static double totalDistanceMiles { get; set; }
        public static string smode { get; set; }

        // graph 
        public static double[] graphHeartRate { get; set; }
        public static double[] graphSpeed { get; set; }
        public static double[] graphCadence { get; set; }
        public static double[] graphAltitude { get; set; }
        public static double[] graphPower { get; set; }

        int timeArrCount = 0;
        public static List<TimeSpan> totalTime = new List<TimeSpan>();
        TimeSpan startTime, endTime;
        // advance matrix 
        public static List<List<double>> intervalValues = new List<List<double>>();
        public static List<double> powerData = new List<double>(); // used in interval detection as well 
        public static List<double> intervalDetectionData = new List<double>(); // interval detection 
        public static List<double> powerInterval = new List<double>(); // interval detection 
        public static double threholdValueGlobal;  // interval detection 
        List<double> powerDataSlt = new List<double>();

        public static double ftpGlobal { get; set; }
        public static double ifGlobal { get; set; }
        public static double tssGlobal { get; set; }
        public static double avgPowerGlobal { get; set; }
        public static double normalizationPowerGlobal { get; set; }

        List<double> movAvgPow4 = new List<double>();
        List<double> movAvg = new List<double>();
        List<double> movAvgPow4Slt = new List<double>();
        List<double> movAvgSlt = new List<double>();

        double movAvgCount;
        OpenFileDialog open = new OpenFileDialog();

        FolderBrowserDialog fd = new FolderBrowserDialog();
        string[] fdata;
        private string dateFinal;

        // ZED GRAPH

        PointPairList list1;
        PointPairList list2;
        PointPairList list3;
        PointPairList list4;
        PointPairList list5;

        GraphPane graphPane;
        LineItem teamACurve;
        LineItem teamBCurve;
        LineItem teamCCurve;
        LineItem teamDCurve;
        LineItem teamECurve;
        delegate void SetTextCallback(string text);
        delegate void axisChangeZedGraphCallBack(ZedGraphControl zg);
        public Thread garthererThread;

        public Form1()
        {
            InitializeComponent();
            var skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500,
                Accent.LightBlue200, TextShade.WHITE);

            graph_index_panel.Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioUSUnit_CheckedChanged(object sender, EventArgs e)
        {
            if (radioUSUnit.Checked)
            {
                unit_data_us();
            }
        }

        private void radioEuroUnit_CheckedChanged(object sender, EventArgs e)
        {
            if (radioEuroUnit.Checked)
            {
                unit_data_euro();
            }
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "hrm|*.hrm|All|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                filename = open.FileName; // name of the browsed file 
                string location = open.SafeFileName;  // location of the browsed file
                string fileData, fileDataTwo;
                int count = 0;

                readFile(filename);
                loadGraph();
                graph_index_panel.Visible = true;
            }
        }


        public void readFile(string data)
        {
            StreamReader fileReader = new StreamReader(data);

            while (!fileReader.EndOfStream)
            {
                fileData = fileReader.ReadLine();
                if (fileData.Contains("StartTime"))
                {
                    string startTime = fileData;
                    string[] arraStartTime = startTime.Split('=');
                    foreach (String item in arraStartTime)
                    {
                        lblStartTime.Text = item;
                        startTimeValue = item;
                    }
                }
                if (fileData.Contains("Interval"))
                {
                    string interval = fileData;
                    string[] arrayInterval = interval.Split('=');
                    foreach (String itemInterval in arrayInterval)
                    {
                        lblInterval.Text = itemInterval;
                    }
                }
                if (fileData.Contains("Weight"))
                {
                    string weight = fileData;
                    string[] arrayWeight = weight.Split('=');
                    foreach (String itemWeight in arrayWeight)
                    {
                        lblWeight.Text = itemWeight;
                    }
                }
                if (fileData.Contains("Length"))
                {
                    string length = fileData;
                    string[] arrayInterval = length.Split('=');
                    foreach (String itemLength in arrayInterval)
                    {
                        lengthValue = itemLength;
                        lblLength.Text = lengthValue;

                    }
                }
                if (fileData.Contains("Interval"))
                {
                    string interval = fileData;
                    string[] arrayInterval = interval.Split('=');
                    foreach (String itemLength in arrayInterval)
                    {
                        intervalValue = itemLength;
                        lblInterval.Text = itemLength;
                    }
                }
                if (fileData.Contains("Version"))
                {
                    string version = fileData;
                    string[] arrayVersion = version.Split('=');
                    foreach (String itemVersion in arrayVersion)
                    {

                        lblVersion.Text = itemVersion;
                    }
                }
                if (fileData.Contains("Monitor"))
                {
                    string monitor = fileData;
                    string[] arrayMonitor = monitor.Split('=');
                    foreach (String itemMonitor in arrayMonitor)
                    {

                        lblMonitor.Text = itemMonitor;
                    }
                }
                if (fileData.Contains("ActiveLimit"))
                {
                    string activeLimit = fileData;
                    string[] arrayActiveLimit = activeLimit.Split('=');
                    foreach (String itemActiveLimit in arrayActiveLimit)
                    {

                    }
                }
                if (fileData.Contains("MaxHR"))
                {
                    string maxHr = fileData;
                    string[] arrayMaxHr = maxHr.Split('=');
                    foreach (String itemMaxHr in arrayMaxHr)
                    {

                        txtMaxHr.Text = itemMaxHr;
                    }
                }
                if (fileData.Contains("RestHR"))
                {
                    string resetHr = fileData;
                    string[] arrayResetHr = resetHr.Split('=');
                    foreach (String itemResetHr in arrayResetHr)
                    {

                        txtRestHr.Text = itemResetHr;
                    }
                }
                if (fileData.Contains("StartDelay"))
                {
                    string startDelay = fileData;
                    string[] arrayStartDelay = startDelay.Split('=');
                    foreach (String itemStartDelay in arrayStartDelay)
                    {

                        lblStartDelay.Text = itemStartDelay;
                    }
                }
                if (fileData.Contains("VO2max"))
                {
                    string VO2max = fileData;
                    string[] arrayVO2max = VO2max.Split('=');
                    foreach (String itemVO2max in arrayVO2max)
                    {

                        lblVO2max.Text = itemVO2max;
                    }
                }
                if (fileData.Contains("Date"))
                {
                    string Date = fileData;
                    string[] arrayDate = Date.Split('=');
                    char[] diff = arrayDate[1].ToCharArray();
                    foreach (String itemDate in arrayDate)
                    {
                        dateFinal = diff[0].ToString() + diff[1].ToString() + diff[2].ToString() + diff[3].ToString() + "/" + diff[4].ToString() + diff[5].ToString() + "/" + diff[6].ToString() + diff[7].ToString();
                        lblDate.Text = dateFinal;
                    }

                }
                if (fileData.Contains("SMode"))
                {
                    string smodeValue = fileData;
                    string[] arraySmode = smodeValue.Split('=');
                    foreach (String itemSmode in arraySmode)
                    {

                        smode = itemSmode;
                        lblSmode.Text = smode;
                    }
                }
            }

            List<List<string>> hrData = File.ReadLines(data)
                                       .SkipWhile(line => line != "[HRData]")
                                       .Skip(1)
                                       .Select(line => line.Split().ToList())
                                       .ToList();
            count = hrData.Count();

            double speedTotal = 0;
            double heartRateTotal = 0;
            double powerTotal = 0;
            double altitudeTotal = 0;
            double[] arraySpeed = new double[500000];
            double[] arrayHeartRate = new double[500000];
            double[] arrayPower = new double[500000];
            double[] arrayAltitude = new double[500000];
            double[] arrayCadence = new double[500000];
            string[] arrayLength = new string[500000];
            string[] arrayStartTime = new string[500000];
            double intervalResult = 0;

            // time interval 
            arrayStartTime = startTimeValue.Split(':');
            string hour = arrayStartTime[0];
            string minute = arrayStartTime[1];
            double sec = double.Parse(arrayStartTime[2]);
            double min = double.Parse(arrayStartTime[0]);
            double hrs = double.Parse(arrayStartTime[1]);
            double intervalTwo = 0;
            for (int i = 0; i < count; i++)
            {
                timeArrCount++;
                double interval = double.Parse(intervalValue);

                //sec = sec + interval ; 

                intervalTwo = intervalTwo + interval;


                dataGridView1.Rows.Add();
                // dataGridView1.Rows[i].Cells[0].Value = dateFinal+"   |   "+ hour + ":" + minute + ":" + sec;
                DateTime timer = DateTime.ParseExact(startTimeValue, "HH:mm:ss.FFF", CultureInfo.InvariantCulture);
                dataGridView1.Rows[i].Cells[0].Value = dateFinal + " | " + timer.AddSeconds(intervalTwo).TimeOfDay;
                //totalTime[i] = timer.AddSeconds(intervalTwo).TimeOfDay; 
                totalTime.Add(timer.AddSeconds(intervalTwo).TimeOfDay);

                char[] smodeData = smode.ToCharArray();
                char speed = smodeData[0];
                char cadence = smodeData[1];
                char altitude = smodeData[2];
                char power = smodeData[3];
                char powerLRBalance = smodeData[4];
                char PowerPIndex = smodeData[5];
                char hrcc = smodeData[6];
                char usEuroUnit = smodeData[7];
                char airPressure = smodeData[8];
                if (speed == '1') // for speed 
                {
                    lblsmSpeed.Text = "On";
                }
                else if (speed == '0')
                {
                    lblsmSpeed.Text = "Off";
                    chkSpeed.Checked = false;
                    chkSpeed.Enabled = false;
                }

                if (cadence == '1') // for cadence
                {
                    lblsmCad.Text = "On";
                }
                else if (cadence == '0')
                {
                    lblsmCad.Text = "Off";
                }

                if (altitude == '1') // for altitude
                {
                    lblsmAltd.Text = "On";
                }
                else if (altitude == '0')
                {
                    lblsmAltd.Text = "Off";
                }

                if (power == '1') // for power
                {
                    lblsmPower.Text = "On";
                }
                else if (power == '0')
                {
                    lblsmPower.Text = "Off";
                }

                if (powerLRBalance == '1') // for power Left Right Balance 
                {
                    lblsmPwbal.Text = "On";
                }
                else if (powerLRBalance == '0')
                {
                    lblsmPwbal.Text = "Off";
                }

                if (PowerPIndex == '1') // for Power Pedlling Index 
                {
                    lblsmPowerIndex.Text = "On";
                }
                else if (PowerPIndex == '0')
                {
                    lblsmPowerIndex.Text = "Off";
                }

                if (hrcc == '1') // for HR/CC Data 
                {
                    lblSmHrcc.Text = "HR + Cycling Data";
                }
                else if (hrcc == '0')
                {
                    lblSmHrcc.Text = "HR Data Only";
                }

                if (usEuroUnit == '1') // for us/euro unit
                {
                    lblsmUs.Text = "US Unit";
                    dataGridView1.Columns[2].HeaderCell.Value = "Speed (US Unit)";
                    radioUSUnit.Checked = true;
                    radioEuroUnit.Checked = false;
                }
                else if (usEuroUnit == '0')
                {
                    lblsmUs.Text = "Euro Unit";
                    dataGridView1.Columns[2].HeaderCell.Value = "Speed (Euro Unit)";
                    radioEuroUnit.Checked = true;
                    radioUSUnit.Checked = false;
                }

                if (airPressure == '1') // for Air Pressure
                {
                    lblsmAir.Text = "On";
                }
                else if (airPressure == '0')
                {
                    lblsmAir.Text = "Off";
                }
                if (hrcc == '1')
                {
                    dataGridView1.Rows[i].Cells[1].Value = hrData[i][0];
                }
                else if (hrcc == '0')
                {
                    dataGridView1.Rows[i].Cells[1].Value = 0;
                }
                if (speed == '1')
                {
                    dataGridView1.Rows[i].Cells[2].Value = hrData[i][1];
                }
                else if (speed == '0')
                {
                    dataGridView1.Rows[i].Cells[2].Value = 0;
                }
                if (cadence == '1')
                {
                    dataGridView1.Rows[i].Cells[3].Value = hrData[i][2];
                }
                else if (cadence == '0')
                {
                    dataGridView1.Rows[i].Cells[3].Value = 0;
                }

                if (altitude == '1')
                {
                    dataGridView1.Rows[i].Cells[4].Value = hrData[i][3];
                }
                else if (altitude == '0')
                {
                    dataGridView1.Rows[i].Cells[4].Value = 0;
                }
                if (power == '1')
                {
                    dataGridView1.Rows[i].Cells[5].Value = hrData[i][4];
                    powerData.Add(Convert.ToDouble(hrData[i][4]));
                }

                else if (power == '0')
                {
                    dataGridView1.Rows[i].Cells[5].Value = 0;
                }
                if (powerLRBalance == '1')
                {
                    dataGridView1.Rows[i].Cells[6].Value = hrData[i][5];
                    double val = Convert.ToDouble(hrData[i][5]); // calculation of PI and LRB
                    double pi = val / 256;
                    double lrb = val % 256;
                    double rb = 100 - lrb;
                    dataGridView1.Rows[i].Cells[7].Value = Math.Round(pi, 0);
                    dataGridView1.Rows[i].Cells[8].Value = "L" + lrb + "- R" + rb;
                }
                else if (powerLRBalance == '0')
                {
                    dataGridView1.Rows[i].Cells[6].Value = 0;
                }
                if (speed == '1')
                {

                    // cadence 

                    arrayCadence[i] = int.Parse(hrData[i][2]);


                    // average speed 

                    speedTotal = speedTotal + int.Parse(hrData[i][1]);
                    averageSpeed = (speedTotal / count) * 0.1;
                    averageSpeedMiles = averageSpeed / 1.6;



                    // maximum speed  

                    arraySpeed[i] = int.Parse(hrData[i][1]);
                }
                else
                {
                    averageSpeed = 0;
                    averageSpeedMiles = 0;
                    arraySpeed[i] = 0;

                }

                if (hrcc == '1')
                {
                    // average heart rate 
                    heartRateTotal = heartRateTotal + int.Parse(hrData[i][0]);
                    averageHeartRate = heartRateTotal / count;
                    // maximum heart rate
                    arrayHeartRate[i] = int.Parse(hrData[i][0]);
                }
                else
                {
                    averageHeartRate = 0;
                    arrayHeartRate[i] = 0;
                }
                if (power == '1')
                {
                    // average power 
                    powerTotal = powerTotal + int.Parse(hrData[i][4]);
                    averagePower = powerTotal / count;
                    avgPowerGlobal = Math.Round(averagePower, 2);
                    // maximum power 
                    arrayPower[i] = int.Parse(hrData[i][4]);
                }
                else
                {
                    averagePower = 0;
                    arrayPower[i] = 0;
                }
                if (altitude == '1')
                {
                    // average altitude 
                    altitudeTotal = altitudeTotal + int.Parse(hrData[i][3]);
                    averageAltitude = altitudeTotal / count;
                    averageAltitudeMile = averageAltitude / 0.3048;
                    // maximum altitude 
                    arrayAltitude[i] = int.Parse(hrData[i][3]);
                }
                else
                {
                    averageAltitude = 0;
                    averageAltitudeMile = 0;
                    arrayAltitude[i] = 0;
                }
            }
            // normalization power 

            // calculation of moving average 
            int value = powerData.Count();
            // movAvgCount = value;
            //  MessageBox.Show(value.ToString());

            for (int x = 0; x < value; x++)
            {
                double movingAverage30 = 0;
                for (int j = 0; j < 30; j++)
                {
                    int index = x + j;
                    index %= value;
                    movingAverage30 += Convert.ToDouble(powerData[index]);
                }

                movingAverage30 /= 30;

                double movAvgPow = Math.Pow(movingAverage30, 4);
                movAvgPow4.Add(movAvgPow);
                movAvg.Add(movingAverage30);

            }


            // MessageBox.Show(movAvgCount.ToString());
            movAvgCount = movAvgPow4.Count();
            if (movAvgPow4 != null)
            {
                double movAvgPow4Sum = movAvgPow4.Sum();
                double power = movAvgPow4Sum / movAvgCount;
                double normalizationPower = Math.Round(Math.Pow(power, 1.0 / 4), 2);
                double movingAverageSum = movAvg.Sum();
                double movingAverageValue = movingAverageSum / movAvgCount; // moving average value 
                                                                            // movingAverageGlobal = movingAverageValue;  
                normalizationPowerGlobal = normalizationPower;
                // ftp value 
                double ftpData = 0.95 * avgPowerGlobal;
                ftpGlobal = ftpData;
                ifGlobal = normalizationPowerGlobal / ftpGlobal;
                // for tss 

                startTime = totalTime.First();
                endTime = totalTime.Last();
                double startTimeSec = startTime.TotalSeconds;
                double endTimeSec = endTime.TotalSeconds;
                TimeSpan length = TimeSpan.Parse(lengthValue);
                double lengthToSec = length.TotalSeconds;
                double totalTimeDurationSec = lengthToSec;

                double tssGlobalOne = normalizationPowerGlobal * ifGlobal * totalTimeDurationSec; // sec value left  
                double tssGlobalTwo = ftpGlobal * 3600;
                double tssGlobalThree = tssGlobalOne / tssGlobalTwo;
                double tssGlobalFour = tssGlobalThree * 100;
                tssGlobal = tssGlobalFour;   // calculating tss 
                                             // MessageBox.Show(ftpData.ToString()); 

                // string totalTimeDuration = TimeSpan.FromDays(totalTimeDurationSec).ToString(@"dd\:hh\:mm");

                double threholdPowVal = Math.Round((105 * ftpGlobal) / 100, 2);
                // double thresholdPowResul = ftpGlobal - threholdPowVal;
                int intervalCountUp = 0;
                int intervalCountDown = 0;
                List<double> chk = new List<double>();
                //foreach (double pData in powerData) {

                //    if (pData >= threholdPowVal) {
                //        intervalCountDown = pData; 
                //        for (int v = 0; ) { }
                //    }
                //}
                for (int v = 0; v < powerData.Count; v++)
                {
                    if (powerData[v] >= threholdPowVal)
                    {
                        intervalCountUp = v;
                        chk.Add(v);

                        // MessageBox.Show(intervalCountUp.ToString());
                    }
                    if (powerData[v] <= threholdPowVal)
                    {
                        intervalCountDown = v;
                        chk.Add(v);

                        // MessageBox.Show(intervalCountDown.ToString());
                    }
                }
                // intervalDetaction();
            }

            maxSpeed = arraySpeed.Max() * 0.1;
            maxSpeedMiles = (maxSpeed) / 1.6;

            //max heart rate 
            maxHeartRate = arrayHeartRate.Max();


            // min heart rate 
            // minHeartRate = arrayHeartRate.Min();
            minHeartRate = double.MaxValue;


            foreach (double valueHR in arrayHeartRate)
            {
                double num = valueHR;
                if (num < minHeartRate)
                    minHeartRate = num;
            }
            // max power 
            maxPower = arrayPower.Max();
            // max altitude 
            maxAltitude = arrayAltitude.Max();
            maxAltitudeMile = maxAltitude / 0.3048;

            // total distance covered 
            if (arrayLength != null)
            {
                arrayLength = lengthValue.Split(':');
                double hourDis = double.Parse(arrayLength[0]) * 3600;
                double minDis = double.Parse(arrayLength[1]) * 60;
                double secDis = double.Parse(arrayLength[2]);

                double length = hourDis + minDis + secDis;
                double lengthFinal = length / 3600;
                double totalDistanceProcess = averageSpeed * lengthFinal;
                double totalDistanceProcessMiles = (totalDistanceProcess) / 1.6;
                totalDistance = Math.Round(totalDistanceProcess, 2);
                totalDistanceMiles = Math.Round(totalDistanceProcessMiles, 2); ;

            }
            // graph data fetch to global 
            graphHeartRate = arrayHeartRate;
            graphSpeed = arraySpeed;
            graphPower = arrayPower;
            graphAltitude = arrayAltitude;
            graphCadence = arrayCadence;


            // Pre populate the date for summary after all the average, min, max has been calculated.
            char[] smode_data = smode.ToCharArray();
            if (smode_data[7] == '1')
            {
                unit_data_us();
            }
            else
            {
                unit_data_euro();
            }

        }

        public void unit_data_euro()
        {
            lblAverageSpeed.Text = System.Math.Round(averageSpeed, 2) + " Km/h";
            lblMaximumSpeed.Text = maxSpeed.ToString() + " Km/h";
            lblAverageHeartRate.Text = System.Math.Round(averageHeartRate, 2) + " bpm";
            lblMaximumHeartRate.Text = maxHeartRate.ToString() + " bpm";
            lblMinimumHeartRate.Text = minHeartRate.ToString() + " bpm";
            lblAveragePower.Text = System.Math.Round(averagePower, 2) + " W";
            lblMaximumPower.Text = maxPower.ToString() + " W";
            lblAverageAltitude.Text = System.Math.Round(averageAltitude, 2) + " m";
            lblMaximumAltitude.Text = maxAltitude.ToString() + " m";
            lblTotalDistance.Text = totalDistance.ToString() + " Km";
        }

        public void unit_data_us()
        {
            lblAverageSpeed.Text = System.Math.Round(averageSpeedMiles, 2) + " miles";
            lblMaximumSpeed.Text = maxSpeedMiles.ToString() + " miles";
            lblAverageHeartRate.Text = System.Math.Round(averageHeartRate, 2) + " bpm";
            lblMaximumHeartRate.Text = maxHeartRate.ToString() + " bpm";
            lblMinimumHeartRate.Text = minHeartRate.ToString() + " bpm";
            lblAveragePower.Text = System.Math.Round(averagePower, 2) + " W";
            lblMaximumPower.Text = maxPower.ToString() + " W";
            lblAverageAltitude.Text = System.Math.Round(averageAltitudeMile, 2) + " Ft";
            lblMaximumAltitude.Text = System.Math.Round(maxAltitudeMile) + " Ft";
            lblTotalDistance.Text = totalDistanceMiles.ToString() + " miles";
        }

        private void chkHeartRate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHeartRate.Checked == false)
            {
                zedGraphControl1.GraphPane.CurveList.Remove(teamACurve);
                zedGraphControl1.GraphPane.YAxisList[0].IsVisible = false;
            }
            else
            {
                zedGraphControl1.GraphPane.CurveList.Add(teamACurve);
                zedGraphControl1.GraphPane.YAxisList[0].IsVisible = true;
            }
            zedGraphControl1.Refresh();
        }

        private void chkSpeed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSpeed.Checked == false)
            {
                zedGraphControl1.GraphPane.CurveList.Remove(teamBCurve);
                zedGraphControl1.GraphPane.YAxisList[1].IsVisible = false;
            }
            else
            {
                zedGraphControl1.GraphPane.CurveList.Add(teamBCurve);
                zedGraphControl1.GraphPane.YAxisList[1].IsVisible = true;
            }
            zedGraphControl1.Refresh();
        }

        private void chkAltitude_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAltitude.Checked == false)
            {
                zedGraphControl1.GraphPane.CurveList.Remove(teamECurve);
                zedGraphControl1.GraphPane.YAxisList[2].IsVisible = false;
            }
            else
            {
                zedGraphControl1.GraphPane.CurveList.Add(teamECurve);
                zedGraphControl1.GraphPane.YAxisList[2].IsVisible = true;
            }
            zedGraphControl1.Refresh();
        }

        private void chkPower_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPower.Checked == false)
            {
                zedGraphControl1.GraphPane.CurveList.Remove(teamCCurve);
                zedGraphControl1.GraphPane.YAxisList[3].IsVisible = false;
            }
            else
            {
                zedGraphControl1.GraphPane.CurveList.Add(teamCCurve);
                zedGraphControl1.GraphPane.YAxisList[3].IsVisible = true;
            }
            zedGraphControl1.Refresh();
        }

        private void chkCadence_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCadence.Checked == false)
            {
                zedGraphControl1.GraphPane.CurveList.Remove(teamDCurve);
                zedGraphControl1.GraphPane.YAxisList[4].IsVisible = false;
            }
            else
            {
                zedGraphControl1.GraphPane.CurveList.Add(teamDCurve);
                zedGraphControl1.GraphPane.YAxisList[4].IsVisible = true;
            }
            zedGraphControl1.Refresh();
        }

        private void axisChangeZedGraph(ZedGraphControl zg)
        {
            if (zg.InvokeRequired)
            {
                axisChangeZedGraphCallBack ad = new axisChangeZedGraphCallBack(axisChangeZedGraph);
                zg.Invoke(ad, new object[] { zg });
            }
            else
            {
                zedGraphControl1.AxisChange();
                zg.Invalidate();
                zg.Refresh();
            }
        }

        private void zedGraph()
        {

            GraphPane graphValue = zedGraphControl1.GraphPane;

            graphValue.CurveList.Clear();
            graphValue.GraphObjList.Clear();
            graphValue.YAxisList.Clear();

            // Set the Titles
            graphValue.Title.Text = "Data Analysis Software";
            graphValue.XAxis.Title.Text = "Time (HH:mm:ss)";

            graphValue.AddYAxis("Heart Rate");
            graphValue.AddYAxis("Speed");
            graphValue.AddYAxis("Altitude");
            graphValue.AddYAxis("Power");
            graphValue.AddYAxis("Cadence");

            // Heart Rate Y AXIS
            graphValue.YAxisList[0].Scale.Min = minHeartRate;
            graphValue.YAxisList[0].Scale.Max = maxHeartRate;

            // Speed Y AXIS
            graphValue.YAxisList[1].Scale.Min = 0;
            graphValue.YAxisList[1].Scale.Max = maxSpeed;

            // Altitude Y AXIS
            graphValue.YAxisList[2].Scale.Min = 0;
            graphValue.YAxisList[2].Scale.Max = maxAltitude;

            // Power Y AXIS
            graphValue.YAxisList[3].Scale.Min = 0;
            graphValue.YAxisList[3].Scale.Max = maxPower;

            // Cadence Y AXIS
            graphValue.YAxisList[4].Scale.Min = 0;
            graphValue.YAxisList[4].Scale.Max = 5000;


            graphValue.Title.FontSpec.FontColor = Color.Crimson;

            // Add gridlines to the plot, and make them gray
            double x, y1, y2, y3, y4, y5;

            // Move the legend location
            graphValue.Legend.Position = ZedGraph.LegendPos.Top;
            PointPairList teamAPairList = new PointPairList();
            PointPairList teamBPairList = new PointPairList();
            PointPairList teamCPairList = new PointPairList();
            PointPairList teamDPairList = new PointPairList();
            PointPairList teamEPairList = new PointPairList();

            graphValue.XAxis.Type = AxisType.Date;
            graphValue.XAxis.Scale.Format = "HH:mm:ss";


            graphValue.XAxis.Scale.Min = 0;
            graphValue.XAxis.Scale.Max = endTime.TotalSeconds - startTime.TotalSeconds;
            graphValue.XAxis.Scale.MinorUnit = DateUnit.Second;
            graphValue.XAxis.Scale.MajorUnit = DateUnit.Minute;


            double[] heartRate = graphHeartRate;
            double[] speed = graphSpeed;
            double[] cadence = graphCadence;
            double[] altitude = graphAltitude;
            double[] power = graphPower;
            for (int i = 0; i < heartRate.Length; i++)
            {
                teamAPairList.Add(i, heartRate[i]);
            }
            for (int i = 0; i < speed.Length; i++)
            {
                teamBPairList.Add(i, speed[i]);
            }
            for (int i = 0; i < cadence.Length; i++)
            {
                teamCPairList.Add(i, cadence[i]);
            }
            for (int i = 0; i < power.Length; i++)
            {

                teamDPairList.Add(i, power[i]);
            }
            for (int i = 0; i < altitude.Length; i++)
            {
                teamEPairList.Add(i, altitude[i]);
            }

            // Heart Rate
            teamACurve = graphValue.AddCurve("Heart Rate", teamAPairList, Color.Red, SymbolType.None);

            // Speed
            teamBCurve = graphValue.AddCurve("Speed ", teamBPairList, Color.Blue, SymbolType.None);

            // Power
            teamCCurve = graphValue.AddCurve("Power", teamCPairList, Color.Green, SymbolType.None);

            // Cadence
            teamDCurve = graphValue.AddCurve("Cadence", teamDPairList, Color.Yellow, SymbolType.None);

            // Altitude
            teamECurve = graphValue.AddCurve("Altitude ", teamEPairList, Color.Orange, SymbolType.None);

            axisChangeZedGraph(zedGraphControl1);

            SetSize();

            // end
        }

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(0, 0);
            zedGraphControl1.IsShowPointValues = true;
            // Leave a small margin around the outside of the control
            // zedGraphControl1.Size = new Size(this.ClientRectangle.Width - 0, this.ClientRectangle.Height - 0);

        }

        private void loadGraph()
        {
            zedGraph();
            SetSize();
        }
    }
}