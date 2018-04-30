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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyclingDataAnalysis
{
    public partial class MultipleFile : MaterialForm
    {
        List<string> files = new List<string>();
        public MultipleFile(List<string> files )
        {
            InitializeComponent();
            this.files = files;
        }
        private void MultipleFile_Load(object sender, EventArgs e)
        {
            
            int i = 1;
            foreach (string file in files)
            {
                if (i == 1)
                {
                    fileReader(i,file, dataGridViewfile1);
                    mainSummary(i,dataGridViewfile1);
                }
                else if (i == 2)
                {
                    fileReader(i,file, dataGridViewfile2);
                    mainSummary(i,dataGridViewfile2);
                }

                i++;
                
            }
           
            // If the two gridview have the same number of column and row :

            for (int k = 0; k < dataGridViewfile1.Rows.Count; k++)
            {
                var row1 = dataGridViewfile1.Rows[k].Cells.Count ;
                //int d1 = dataGridViewfile1.Rows[k].Cells[j].Value;
               // var row2 = src2.Rows[k].ItemArray;

                for (int j = 1; j <=6; j++)
                {
                   string  d1 = dataGridViewfile1.Rows[k].Cells[j].Value.ToString();
                    int g1 = Convert.ToInt32(d1);
                    string d2 = dataGridViewfile2.Rows[k].Cells[j].Value.ToString();
                    int g2 = Convert.ToInt32(d2);
                    if (g1 > g2)
                    {
                        dataGridViewfile1.Rows[k].Cells[j].Style.BackColor = Color.Green;
                        dataGridViewfile2.Rows[k].Cells[j].Style.BackColor = Color.Orange;
                    }else if (g1 < g2)
                    {
                        dataGridViewfile2.Rows[k].Cells[j].Style.BackColor = Color.Green;
                        dataGridViewfile1.Rows[k].Cells[j].Style.BackColor = Color.Orange;

                    }
                    else  if (g1 == g2)
                    {
                        dataGridViewfile1.Rows[k].Cells[j].Style.BackColor = Color.Yellow;
                        dataGridViewfile2.Rows[k].Cells[j].Style.BackColor = Color.Yellow;
                    }
                }
            }


        }
      

        string fileData;
        string startTimeValue, intervalValue;
        int count = 0;
        int timeArrCount = 0;
        public static List<TimeSpan> totalTime = new List<TimeSpan>();
        public static string smode { get; set; }


        private void dataGridViewfile1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
      
        
        
        private void mainSummary(int fileNumber, DataGridView dataGridViewfile1)
        {
            int chunkDivision = 0;
            

            if (chunkDivision == 0)
            {
                
                    int grdCount = dataGridViewfile1.Rows.Count;
                    double[] hr = new double[grdCount];
                    double[] sp = new double[grdCount];
                    double[] cd = new double[grdCount];
                    double[] al = new double[grdCount];
                    double[] po = new double[grdCount];


                    int i = 0;
                    foreach (DataGridViewRow row in dataGridViewfile1.Rows)
                    {
                        hr[i] = Convert.ToDouble(row.Cells[1].Value);
                        sp[i] = Convert.ToDouble(row.Cells[2].Value);
                        cd[i] = Convert.ToDouble(row.Cells[3].Value);
                        al[i] = Convert.ToDouble(row.Cells[4].Value);
                        po[i] = Convert.ToDouble(row.Cells[5].Value);


                        i++;
                    }
                    // chunkDataSummary();
                    calculate(fileNumber, chunkDivision, hr, sp, cd, al, po);
                    
                
            }
         }
        private void tabPage2_Click(object sender, EventArgs e)
        {
           

        }

      
        private void dataGridViewfile1_SelectionChanged(object sender, EventArgs e)
        {

           
        }

        private void dataGridViewfile1_MouseUp(object sender, MouseEventArgs e)
        {
            dataGridViewfile2.ClearSelection();
            dataGridViewfile1.SelectedRows.Cast<DataGridViewRow>().Select(x => x.Index)
                .ToList().ForEach(i =>
                {
                    if (i < this.dataGridViewfile2.RowCount)
                        this.dataGridViewfile2.Rows[i].Selected = true;
                });
            int fileNumberHere = 1;
            int chunkSection = 1;
            
            using (var form = new ChunkData())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    chunkSection = form.chunkGet;
                }
            }
            foreach (string file in files)
            {
                if (fileNumberHere == 1)
                {
                    int grdCount = dataGridViewfile1.SelectedRows.Count;

                    double[] hr = new double[grdCount];
                    double[] sp = new double[grdCount];
                    double[] cd = new double[grdCount];
                    double[] al = new double[grdCount];
                    double[] po = new double[grdCount];

                    int i = 0;
                    foreach (DataGridViewRow row in dataGridViewfile1.SelectedRows)
                    {
                        hr[i] = Convert.ToDouble(row.Cells[1].Value);
                        sp[i] = Convert.ToDouble(row.Cells[2].Value);
                        cd[i] = Convert.ToDouble(row.Cells[3].Value);
                        al[i] = Convert.ToDouble(row.Cells[4].Value);
                        po[i] = Convert.ToDouble(row.Cells[5].Value);


                        i++;
                    }
                    chunkDataSections(fileNumberHere, chunkSection, hr, sp, cd, al, po);
                }
                else if (fileNumberHere == 2)
                {
                    int grdCount = dataGridViewfile2.SelectedRows.Count;

                    double[] hr = new double[grdCount];
                    double[] sp = new double[grdCount];
                    double[] cd = new double[grdCount];
                    double[] al = new double[grdCount];
                    double[] po = new double[grdCount];

                    int i = 0;
                    foreach (DataGridViewRow row in dataGridViewfile2.SelectedRows)
                    {
                        hr[i] = Convert.ToDouble(row.Cells[1].Value);
                        sp[i] = Convert.ToDouble(row.Cells[2].Value);
                        cd[i] = Convert.ToDouble(row.Cells[3].Value);
                        al[i] = Convert.ToDouble(row.Cells[4].Value);
                        po[i] = Convert.ToDouble(row.Cells[5].Value);


                        i++;
                    }
                  
                    chunkDataSections(fileNumberHere, chunkSection, hr, sp, cd, al, po);

                }
                fileNumberHere++;
            }
            //+/- calculate
            plusMinus();

        }
        public void chunkDataSections(int fileNumberHere,int ChunkSection, double[] hr, double[] sp, double[] cd, double[] al, double[] po)
        {
            // int chunkValue = new ChunkData().sendChunkValue();

            int chunkStart = 0;
            int countVal = 0;
            int countHR = 0;
            int ChunkDivision = hr.Length / ChunkSection;
            while (chunkStart < ChunkSection)
            {
                double[] heartChunkValue = new double[ChunkDivision];
                double[] sp1 = new double[ChunkDivision];
                double[] cd1 = new double[ChunkDivision];
                double[] al1 = new double[ChunkDivision];
                double[] po1= new double[ChunkDivision];

                int i = 0;
                for (int k = countVal; k < ChunkDivision + countVal; k++)
                {
                    heartChunkValue[i] = hr[k];
                    sp1[i] = sp[k];
                    cd1[i] = cd[k];
                    al1[i] = al[k];
                    po1[i] = po[k];
                    countHR++;
                    i++;

                }
                countVal = countHR;
                chunkStart++;
                calculate(fileNumberHere, chunkStart, heartChunkValue, sp1, cd1, al1, po1);

            }

        }
        //.variable for +/-
        double[] compareavgspc1 = new double[2];
        double[] maxheartRatecomparechunk1 = new double[2];
        private void calculate(int fileno, int ChunkSection, double[] hr, double[] sp, double[] cd, double[] al, double[] po)
        {
            int count = hr.Length;
            double maxhr = hr.Max();
            double avgHR = hr.Sum() / count;
            double min = hr.Min();
            double maxsp = sp.Max();
            double avgsp = sp.Sum() / count;
            double maxal = al.Max();
            double avgal = al.Sum() / count;
            double maxpo = po.Max();
            double avgpo = po.Sum() / count;

          
            // int fileno = 1;
            switch (ChunkSection)
            {
                case 0:
                    {
                       
                        if (fileno == 1)
                        {
                            avgHeartRatef1.Text = avgHR.ToString();
                            maxHeartRate1.Text = maxhr.ToString();
                            minHRf1.Text = min.ToString();
                            f1sp.Text = avgsp.ToString();
                            maxspf1.Text = maxsp.ToString();
                            
                        }
                        else if (fileno == 2)
                        {
                            avgHeartRatef2.Text = avgHR.ToString();
                            maxhrf2.Text = maxhr.ToString();
                            minHRf2.Text = min.ToString();
                            f2sp.Text = avgsp.ToString();
                            maxspf2.Text = maxsp.ToString();
                          
                        }
                        break;
                    }
                case 1:
                    {
                      
                        if (fileno == 1)
                        {
                            cahr1.Text = Math.Round(avgHR,2).ToString();
                            cmhr1.Text = Math.Round(maxhr,2).ToString();
                            cmhrf1.Text = min.ToString();
                             f1sp.Text = avgsp.ToString();
                             maxspf1.Text = maxsp.ToString();
                            //for +/-
                            compareavgspc1[0] = avgHR;
                            maxheartRatecomparechunk1[0] = maxhr;
                        }
                        else if (fileno == 2)
                        {
                            cahr2.Text = Math.Round(avgHR,2).ToString();
                            cmhr2.Text = Math.Round(maxhr,2).ToString();
                            minhrcf2.Text = min.ToString();
                            f2sp.Text = avgsp.ToString();
                            maxspf2.Text = maxsp.ToString();
                            //for +/-
                            compareavgspc1[1] = avgHR;
                            maxheartRatecomparechunk1[1] = maxhr;
                        }
                        break;
                    }
                case 2:
                    {
                        if (fileno == 1)
                        {
                            averagehrate2.Text = avgHR.ToString();
                            minHR2.Text = min.ToString();
                            maxHR2.Text = maxhr.ToString();
                            averagesp2.Text = avgsp.ToString();
                            maxsp2.Text = maxsp.ToString();
                        }
                        else if (fileno == 2)
                        {
                            avghrc2f2.Text = Math.Round(avgHR,2).ToString();
                            maxhrc2f2.Text = maxhr.ToString();
                            minc2f2.Text = min.ToString();
                            f2sp.Text = asc2f2.ToString();
                            maxspf2.Text = maxsc2f2.ToString();

                        }

                        break;
                    }
                case 3:
                    {
                        if (fileno == 1)
                        {
                            averagehr3.Text = avgHR.ToString();
                            maxhr3.Text = maxhr.ToString();
                            minhr3.Text = min.ToString();
                            averagesp3.Text = avgsp.ToString();
                            maxsp3.Text = maxsp.ToString();
                        }
                        else if (fileno == 2)
                        {
                            avghrc3f2.Text = avgHR.ToString();
                            maxhrc3f2.Text = maxhr.ToString();
                            minhrc3f2.Text = min.ToString();
                            asc3f2.Text = avgsp.ToString();
                            maxsc3f2.Text = maxsp.ToString();

                        }

                        break;
                    }
                case 4:
                    {
                        if (fileno == 1)
                        {
                            averagehr4.Text = avgHR.ToString();
                            maxhr4.Text = maxhr.ToString();
                            minhr4.Text = min.ToString();
                            averagesp4.Text = avgsp.ToString();
                            maxsp4.Text = maxsp.ToString();
                        }
                        else if (fileno == 2)
                        {
                            avghrc4f2.Text = avgHR.ToString();
                            maxhrc4f2.Text = maxhr.ToString();
                            minhrc4f2.Text = min.ToString();
                            asc4f2.Text = avgsp.ToString();
                            maxsc4f2.Text = maxsp.ToString();


                        }

                        break;
                    }

            }

        }
        //for +/-
        private void plusMinus()
        {
            // this is for chunk1 average heartrate only ,do similar for othrs
           if( compareavgspc1[0]> compareavgspc1[1])
            {
                signaspf1.Text = "(+)";
                signaspf1.Visible = true;
                signaspf2.Text = "(-)";
                signaspf2.Visible = true;
            }else if(compareavgspc1[0] < compareavgspc1[1])
            {
                signaspf1.Text = "(-)";
                signaspf1.Visible = true;
                signaspf2.Text = "(+)";
                signaspf2.Visible = true;
            }
           //for max hr for chunk1 do similar for others
           if(maxheartRatecomparechunk1[0]> maxheartRatecomparechunk1[1])
            {

               mhf1.Text = "(+)";
               mhf1.Visible = true;
               mhf2.Text = "(-)";
               mhf2.Visible = true;
            }else if (maxheartRatecomparechunk1[0] < maxheartRatecomparechunk1[1])
            {
                mhf1.Text = "(-)";
                mhf1.Visible = true;
                mhf2.Text = "(+)";
                mhf2.Visible = true;
            }

        }

        

      //  string storeData;
      //  string dateStart = "1/23/2018";
        string dateFinal;
        double ftpGlobal;
        double ifGlobal;
        double tssGlobal;
        double avgPowerGlobal;
        double normalizationPowerGlobal;
        List<double> powerData = new List<double>();
        double averagePower;
        //int timeArrCount = 0;
        //public static List<TimeSpan> totalTime = new List<TimeSpan>();
        TimeSpan startTime, endTime;
        string lengthValue;
        double totalDistance;
        double averageSpeed;

        List<double> movAvgPow4 = new List<double>();
        List<double> movAvg = new List<double>();
        List<double> movAvgPow4Slt = new List<double>();
        List<double> movAvgSlt = new List<double>();

        double movAvgCount;
        public void fileReader( int fileValue ,string data, DataGridView dataGridViewfile1)
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
                        // lblStartTime.Text = item;
                        startTimeValue = item;
                    }
                }
                
                if (fileData.Contains("Length"))
                {
                    string length = fileData;
                    string[] arrayInterval = length.Split('=');
                    foreach (String itemLength in arrayInterval)
                    {
                        lengthValue = itemLength;

                    }
                }
            if (fileData.Contains("Interval"))
            {
                string interval = fileData;
                string[] arrayInterval = interval.Split('=');
                foreach (String itemLength in arrayInterval)
                {
                    intervalValue = itemLength;
                    // lblInterval.Text = itemLength;
                }
            }
                
                if (fileData.Contains("SMode"))
                {
                    string smodeValue = fileData;
                    string[] arraySmode = smodeValue.Split('=');
                    foreach (String itemSmode in arraySmode)
                    {

                        smode = itemSmode;
                    }
                }
                //}
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
           // double altitudeTotal = 0;
            double[] arraySpeed = new double[500000];
            double[] arrayHeartRate = new double[500000];
            double[] arrayPower = new double[500000];
            double[] arrayAltitude = new double[500000];
            double[] arrayCadence = new double[500000];
            string[] arrayLength = new string[500000];
            string[] arrayStartTime = new string[500000];
           // double intervalResult = 0;




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


                dataGridViewfile1.Rows.Add();
                // dataGridView1.Rows[i].Cells[0].Value = dateFinal+"   |   "+ hour + ":" + minute + ":" + sec;
                DateTime timer = DateTime.ParseExact(startTimeValue, "HH:mm:ss.FFF", CultureInfo.InvariantCulture);
                dataGridViewfile1.Rows[i].Cells[0].Value = dateFinal + " | " + timer.AddSeconds(intervalTwo).TimeOfDay;
                //totalTime[i] = timer.AddSeconds(intervalTwo).TimeOfDay; 
                totalTime.Add(timer.AddSeconds(intervalTwo).TimeOfDay);

                // int clm = 0;
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

                if (hrcc == '1')
                {
                    dataGridViewfile1.Rows[i].Cells[1].Value = hrData[i][0];
                }
                else if (hrcc == '0')
                {
                    dataGridViewfile1.Rows[i].Cells[1].Value = 0;
                }
                if (speed == '1')
                {
                    dataGridViewfile1.Rows[i].Cells[2].Value = hrData[i][1];
                }
                else if (speed == '0')
                {
                    dataGridViewfile1.Rows[i].Cells[2].Value = 0;
                }
                if (cadence == '1')
                {
                    dataGridViewfile1.Rows[i].Cells[3].Value = hrData[i][2];
                }
                else if (cadence == '0')
                {
                    dataGridViewfile1.Rows[i].Cells[3].Value = 0;
                }

                if (altitude == '1')
                {
                    dataGridViewfile1.Rows[i].Cells[4].Value = hrData[i][3];
                }
                else if (altitude == '0')
                {
                    dataGridViewfile1.Rows[i].Cells[4].Value = 0;
                }
                if (power == '1')
                {
                    dataGridViewfile1.Rows[i].Cells[5].Value = hrData[i][4];
                    powerData.Add(Convert.ToDouble(hrData[i][4]));
                }

                else if (power == '0')
                {
                    dataGridViewfile1.Rows[i].Cells[5].Value = 0;
                }
                if (powerLRBalance == '1')
                {
                    dataGridViewfile1.Rows[i].Cells[6].Value = hrData[i][5];
                    double val = Convert.ToDouble(hrData[i][5]); // calculation of PI and LRB
                    double pi = val / 256;
                    double lrb = val % 256;
                    double rb = 100 - lrb;
                    dataGridViewfile1.Rows[i].Cells[7].Value = Math.Round(pi, 0);
                    dataGridViewfile1.Rows[i].Cells[8].Value = "L" + lrb + "- R" + rb;
                }
                else if (powerLRBalance == '0')
                {
                    dataGridViewfile1.Rows[i].Cells[6].Value = 0;
                }
                if (speed == '1')
                {

                    // cadence 

                    arrayCadence[i] = int.Parse(hrData[i][2]);


                    // average speed 

                    speedTotal = speedTotal + int.Parse(hrData[i][1]);
                    averageSpeed = (speedTotal / count) * 0.1;
                    // averageSpeedMiles = averageSpeed / 1.6;



                    // maximum speed  

                    arraySpeed[i] = int.Parse(hrData[i][1]);
                }
                else
                {
                    averageSpeed = 0;
                    //averageSpeedMiles = 0;
                    arraySpeed[i] = 0;

                }

                if (hrcc == '1')
                {
                    // average heart rate 
                    heartRateTotal = heartRateTotal + int.Parse(hrData[i][0]);
                    // averageHeartRate = heartRateTotal / count;
                    // maximum heart rate
                    arrayHeartRate[i] = int.Parse(hrData[i][0]);
                }
                else
                {
                    // averageHeartRate = 0;
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
                double power1 = movAvgPow4Sum / movAvgCount;
                double normalizationPower = Math.Round(Math.Pow(power1, 1.0 / 4), 2);
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

               

                double threholdPowVal = Math.Round((105 * ftpGlobal) / 100, 2);
               
                int intervalCountUp = 0;
                int intervalCountDown = 0;
                List<double> chk = new List<double>();
               
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
             
            }

           

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
               // totalDistanceMiles = Math.Round(totalDistanceProcessMiles, 2); ;

            }
              
            
            if (fileValue == 1)
            {
                td1.Text = totalDistance.ToString();
                lblMovAvgf1.Text = Math.Round(avgPowerGlobal,2).ToString();
                lblNorPowf1.Text = Math.Round(normalizationPowerGlobal,2).ToString();
                lblFuncThresf1.Text = Math.Round(ftpGlobal,2).ToString();
                lblIntesiFacf1.Text = Math.Round(ifGlobal,2).ToString();
                lblTraiStresf1.Text = Math.Round(tssGlobal,2).ToString();
            }
            else if (fileValue == 2)
            {
                td1.Text = totalDistance.ToString();
                lblMovAvgf2.Text = Math.Round(avgPowerGlobal, 2).ToString();
                lblNorPowf2.Text = Math.Round(normalizationPowerGlobal, 2).ToString();
                lblFuncThresf2.Text = Math.Round(ftpGlobal, 2).ToString();
                lblIntesiFacf2.Text = Math.Round(ifGlobal, 2).ToString();
                lblTraiStresf2.Text = Math.Round(tssGlobal, 2).ToString();

            }
        }
        }
    }

