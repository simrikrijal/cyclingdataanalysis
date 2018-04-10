using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyclingDataAnalysis
{
    public partial class ChunkDataSummary : MaterialForm
    {
        //string avgHR1, avgHR2, avgHR3, avgHR4;
        //string mxHR1, mxHR2, mxHR3, mxHR4;
        //string mnHR1, mnHR3, mnHR4;
        double[] hr , sp,  cd,  al,  po;
        int count ;
        int ChunkDivision ;
        int chunkNumber=0;
        public ChunkDataSummary(int chunkNumber,double[] hr, double[] sp, double[] cd, double[] al, double[] po)
        {
            InitializeComponent();
            this.hr = hr;
            this.sp = sp;
            this.cd = cd;
            this.al = al;
            this.po = po;
            count = hr.Length;
            this.chunkNumber = chunkNumber;
            ChunkDivision = count/ chunkNumber;

        }
        private void ChunkDataSummary_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages[2].Visible = false;
            tabControl1.TabPages[3].Visible = false;
            chunkSectionsSummary();
            //chunkSpeedSummary();
            // chunk


        }
        public void chunkSectionsSummary()
        {
           // int chunkValue = new ChunkData().sendChunkValue();
          
            int chunkStart = 0;
            int countVal = 0;
            int countHR = 0;

            while (chunkStart < chunkNumber)
            {
                double[] heartChunkValue = new double[ChunkDivision];
                double[] sp1 = new double[ChunkDivision];
                double[] cd1 = new double[ChunkDivision];
                double[] al1 = new double[ChunkDivision];
                double[] po1 = new double[ChunkDivision];
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
              //  if(heartChunkValue.Length<ChunkDivison)
                calculateData(chunkStart, heartChunkValue, sp1, cd1, al1, po1);

            }

        }

        private void calculateData(int chunkNo, double[] hr, double[] sp, double[] cd, double[] al, double[] po)
        {
            //put logic here for km/miles
            double maxhr = hr.Max();
            double avgHR = hr.Sum() / ChunkDivision;
            double min = hr.Min();

            double maxsp = sp.Max();
            double avgsp = sp.Sum() / ChunkDivision;

            double avgal = al.Sum() / ChunkDivision;
            double maxal = al.Max();

            double avpo = po.Sum() / ChunkDivision;
            double maxpo = po.Max();

            switch (chunkNo)
            {
                case 1:
                    {
                        avgHeartRate1.Text = avgHR.ToString();
                        maxHeartRate1.Text = maxhr.ToString();
                        minHR1.Text = min.ToString();
                        averagesp1.Text = avgsp.ToString();
                        maxsp1.Text = maxsp.ToString();
                        aa1.Text = avgal.ToString();
                        maxa1.Text = maxal.ToString();
                        ap1.Text = avpo.ToString();
                        maxp1.Text = maxpo.ToString();
                       // td1.Text=
                        break;
                    }
                case 2:
                    {
                        averagehrate2.Text= avgHR.ToString(); 
                        maxHR2.Text= maxhr.ToString();
                        minHR2.Text = min.ToString();


                        averagesp2.Text = avgsp.ToString();
                        maxsp2.Text = maxsp.ToString();
                        aa2.Text = avgal.ToString();
                        maxa2.Text = maxal.ToString();
                        ap2.Text = avpo.ToString();
                        maxp2.Text = maxpo.ToString();
                        break;
                    }
                case 3:
                    {
                        tabControl1.TabPages[2].Visible =true;
                        averagehr3.Text= avgHR.ToString();
                        maxhr3.Text = maxhr.ToString();
                        minhr3.Text = min.ToString();

                        averagesp3.Text = avgsp.ToString();
                        maxsp3.Text = maxsp.ToString();
                        aa3.Text = avgal.ToString();
                        maxa3.Text = maxal.ToString();
                        ap3.Text = avpo.ToString();
                        maxp3.Text = maxpo.ToString();
                        break;
                    }
                case 4:
                    {
                        tabControl1.TabPages[3].Visible = true;
                        averagehr4.Text = avgHR.ToString();
                        maxhr4.Text = maxhr.ToString();
                        minhr4.Text = min.ToString();

                        averagesp4.Text = avgsp.ToString();
                        maxsp4.Text = maxsp.ToString();
                        aa4.Text = avgal.ToString();
                        maxa4.Text = maxal.ToString();
                        ap4.Text = avpo.ToString();
                        maxp4.Text = maxpo.ToString();
                        break;

                    }
            }
      
        }
        //private void chunkSpeedSummary()
        //{
        //    int chunkStart = 0;
        //    int countVal = 0;
        //    int countsp = 0;

        //    while (chunkStart<chunkNumber)
        //    {
        //        double[] SpeedChunkValue = new double[100];
        //        int i = 0;
        //        for (int k = countVal; k < ChunkDivision + countVal; k++)
        //        {
        //            SpeedChunkValue[i] = sp[k];
        //            countsp++;
        //            i++;

        //        }
        //        countVal = countsp;
        //        chunkStart++;
        //        calculateSpeed(chunkStart, SpeedChunkValue);

        //    }

        //}

        //private void calculateSpeed(int chunkNo, double[] sp)
        //{
        //    double maxsp = sp.Max();
        //    double avgsp = sp.Sum() / ChunkDivision;
        //    //double min = sp.Min();
        //    switch (chunkNo)
        //    {
        //        case 1:
        //            {
        //                averagesp1.Text = avgsp.ToString();
        //                maxsp1.Text = maxsp.ToString();
        //                break;
        //            }
        //        case 2:
        //            {
        //                averagesp2.Text = avgsp.ToString();
        //                maxsp2.Text = maxsp.ToString();
        //                break;
        //            }
        //        case 3:
        //            {
        //                averagesp3.Text = avgsp.ToString();
        //                maxsp3.Text = maxsp.ToString();
        //                break;
        //            }
        //        case 4:
        //            {
        //                averagesp4.Text = avgsp.ToString();
        //                maxsp4.Text = maxsp.ToString();
        //                break;

        //            }
        //    }
        //}
       

        private void chunk1_Click(object sender, EventArgs e)
        {

        }

 
    }
}
