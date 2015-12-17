using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using McOverlap;
using McOverlapCore;
using McOverlapCore.ImageProcessing;
using System;
using System.IO;
using System.Windows.Forms;

namespace McOverlap
{
    public partial class MainUI : Form
    {
        //General components:
        private OpenFileDialog opf = new OpenFileDialog();
        private FolderBrowserDialog fbd = new FolderBrowserDialog();


        private DirectoryInfo base_di = null;
        private DirectoryInfo po_di = null;
        private DirectoryInfo doAll_di = null;
        private DirectoryInfo extracted_di = null;
        private FileInfo videoFi = null;

        //OpenCV Data:
        private Image baseImg;
        private Image poImg;

        private Mat displayedBase = new Mat();
        private Mat displayedPo = new Mat();

        private DetectorType detectorType;
        private ExtractorType extractorType;
        private MatcherType matcherType;

        private OverlapEstimator overlapEstimator;


        public MainUI()
        {
            InitializeComponent();
            CenterToScreen();
            imageBox1.FunctionalMode = ImageBox.FunctionalModeOption.PanAndZoom;
            imageBox2.FunctionalMode = ImageBox.FunctionalModeOption.PanAndZoom;
            imageBox3.FunctionalMode = ImageBox.FunctionalModeOption.PanAndZoom;

            //set default detector, extractor and matcher types:
            detectorType = DetectorType.FAST;
            extractorType = ExtractorType.BRIEF;
            matcherType = MatcherType.L2;

            //display current settings:
            detectorTypeTB.Text = detectorType.ToString();
            extractorTypeTB.Text = extractorType.ToString();
            matcherTypeTB.Text = matcherType.ToString();

        }

        //Load Base Image Button:
        private void button1_Click(object sender, EventArgs e)
        {
            if (opf.ShowDialog() == DialogResult.OK){
                textBox13.Text = opf.FileName;
                baseImg = new Image(opf.FileName);
                if (drawImages())
                {
                    ImageBox ib = getBaseImageBox();
                    CvInvoke.Resize(baseImg.Mat, displayedBase, new System.Drawing.Size(ib.Width, ib.Height));
                    ib.Image = displayedBase;
                }
                
            }
            
        }
        //Load Potentially Overlapping Image Button:
        private void button2_Click(object sender, EventArgs e)
        {
            if (opf.ShowDialog() == DialogResult.OK)
            {
                textBox14.Text = opf.FileName;
                poImg = new Image(opf.FileName);
                if (drawImages())
                {
                    ImageBox ib = getPotentiallyOverlappingImageBox();
                    CvInvoke.Resize(poImg.Mat, displayedPo, new System.Drawing.Size(ib.Width, ib.Height));
                    ib.Image = displayedPo;
                }
                
            }
        }
        //Estimate Overlap Button:
        private void button3_Click(object sender, EventArgs e)
        {

            using (overlapEstimator = new OverlapEstimator(this, detectorType, extractorType, matcherType))
            {
                double dblOverlap = overlapEstimator.execute(baseImg, poImg);
                String s = "..Success.." + "\n" + "..estimated overlap is.." + dblOverlap;
                textBox1.AppendText(s);
            }
            
        }

        public ImageBox getBaseImageBox()
        {
            return imageBox1;
        }

        public ImageBox getPotentiallyOverlappingImageBox()
        {
            return imageBox3;
        }

        public ImageBox getEstimatedOverlapImageBox()
        {
            return imageBox2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox13.Text == "")
            {
                return; //don't need to do anything
            }

            String base_image_path = textBox13.Text;

            if(base_di == null)
            {
                int endOfDirIndex = base_image_path.LastIndexOf('\\');
                //strip off filename and use path to create directoryInfo object
                base_di = new DirectoryInfo(base_image_path.Substring(0, endOfDirIndex));
            }

            FileInfo[] files = base_di.GetFiles();
            bool getfile = false;
            foreach(FileInfo fi in files)
            {

                if (getfile)
                {
                    textBox13.Text = fi.FullName;
                    baseImg = new Image(fi.FullName);
                    if (drawImages())
                    {
                        ImageBox ib = getBaseImageBox();
                        CvInvoke.Resize(baseImg.Mat, displayedBase, new System.Drawing.Size(ib.Width, ib.Height));
                        ib.Image = displayedBase;
                    }
                    
                    return;
                }
                if (fi.FullName == base_image_path)
                {
                    getfile = true;
                }
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox14.Text == "")
            {
                return; //don't need to do anything
            }

            String po_image_path = textBox14.Text;

            if (po_di == null)
            {
                int endOfDirIndex = po_image_path.LastIndexOf('\\');
                //strip off filename and use path to create directoryInfo object
                po_di = new DirectoryInfo(po_image_path.Substring(0, endOfDirIndex));
            }

            FileInfo[] files = po_di.GetFiles();
            bool getfile = false;
            foreach (FileInfo fi in files)
            {

                if (getfile)
                {
                    textBox14.Text = fi.FullName;
                    poImg = new Image(fi.FullName);
                    if (drawImages())
                    {
                        ImageBox ib = getPotentiallyOverlappingImageBox();
                        CvInvoke.Resize(poImg.Mat, displayedPo, new System.Drawing.Size(ib.Width, ib.Height));
                        ib.Image = displayedPo;
                    }
                    
                    return;
                }
                if (fi.FullName == po_image_path)
                {
                    getfile = true;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox13.Text == "")
            {
                return; //don't need to do anything
            }

            String base_image_path = textBox13.Text;

            if (base_di == null)
            {
                int endOfDirIndex = base_image_path.LastIndexOf('\\');
                //strip off filename and use path to create directoryInfo object
                base_di = new DirectoryInfo(base_image_path.Substring(0, endOfDirIndex));
            }

            FileInfo[] files = base_di.GetFiles();
            FileInfo prevfi = null;
            foreach (FileInfo fi in files)
            {
                if (fi.FullName == base_image_path)
                {
                    if(prevfi == null)
                    {
                        return; //nothing to do
                    }

                    textBox13.Text = prevfi.FullName;
                    baseImg = new Image(prevfi.FullName);
                    if (drawImages())
                    {
                        ImageBox ib = getBaseImageBox();
                        CvInvoke.Resize(baseImg.Mat, displayedBase, new System.Drawing.Size(ib.Width, ib.Height));
                        ib.Image = displayedBase;
                    }
                    
                    return;
                }

                prevfi = fi;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox14.Text == "")
            {
                return; //don't need to do anything
            }

            String po_image_path = textBox14.Text;

            if (po_di == null)
            {
                int endOfDirIndex = po_image_path.LastIndexOf('\\');
                //strip off filename and use path to create directoryInfo object
                po_di = new DirectoryInfo(po_image_path.Substring(0, endOfDirIndex));
            }

            FileInfo[] files = po_di.GetFiles();
            FileInfo prevfi = null;
            foreach (FileInfo fi in files)
            {
                if (fi.FullName == po_image_path)
                {
                    if (prevfi == null)
                    {
                        return; //nothing to do
                    }

                    textBox14.Text = prevfi.FullName;
                    poImg = new Image(prevfi.FullName);
                    if (drawImages())
                    {
                        ImageBox ib = getPotentiallyOverlappingImageBox();
                        CvInvoke.Resize(poImg.Mat, displayedPo, new System.Drawing.Size(ib.Width, ib.Height));
                        ib.Image = displayedPo;
                    }
                    
                    return;
                }

                prevfi = fi;
            }
        }

        public double resizeScale()
        {
            return 0.1;
        }

        public int numNearestNeighbours()
        {
            return 2;
        }

        public double uniqunessThreshold()
        {
            return 0.8;
        }

        public double scaleIncrement()
        {
            return 1.5;
        }

        public int numRotationBins()
        {
            return 20;
        }

        public double ransacReprojThreshold()
        {
            return 5;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                doAll_di = new DirectoryInfo(fbd.SelectedPath);
                textBox15.Text = doAll_di.FullName;
            }

           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            getConsoleTB().Text = "";
            double start = System.DateTime.Now.TimeOfDay.TotalMilliseconds;
            extracted_di = Directory.CreateDirectory(doAll_di.FullName + "_extracted_" + getOverlap() + "_" + detectorType + "_" + extractorType + "_" + matcherType);
            DirectoryInfo probFrames_di = Directory.CreateDirectory(extracted_di.FullName + "/" + "problem frames");

            Text = "Output Directory: " + extracted_di.Name;

            FileInfo baseFi = doAll_di.GetFiles()[0]; //get first file
            File.Copy(baseFi.FullName, extracted_di.FullName + "/" + baseFi.Name); //extract file to dir
            baseImg = new Image(baseFi.FullName);
            if (drawImages())
            {
                ImageBox ib = getBaseImageBox();
                CvInvoke.Resize(baseImg.Mat, displayedBase, new System.Drawing.Size(ib.Width, ib.Height));
                ib.Image = displayedBase;
            }

            textBox13.Text = baseFi.FullName;
            bool fileExists = false;
            FileInfo prevFi = baseFi;

            

            foreach (FileInfo fi in doAll_di.GetFiles())
            {

                progressBar1.Value = (progressBar1.Value + 10) % 99;
                poImg = new Image(fi.FullName);
                textBox14.Text = fi.FullName;
                try
                {

                    if (drawImages())
                    {
                        ImageBox ib = getPotentiallyOverlappingImageBox();
                        CvInvoke.Resize(poImg.Mat, displayedPo, new System.Drawing.Size(ib.Width, ib.Height));
                        ib.Image = displayedPo;
                    }

                    overlapEstimator = new OverlapEstimator(this, detectorType, extractorType, matcherType);
                    double overlap = overlapEstimator.execute(baseImg, poImg);
                    textBox1.AppendText(baseFi.Name + " : " + fi.Name + " has " + overlap + "% overlap" + "\r\n");
                    if (overlap <= getOverlap())
                    {
                        if (overlap == 0)
                        {
                            //clearly something went wrong, abandon frame
                            //save base image in prob files
                            File.Copy(baseFi.FullName, probFrames_di.FullName + "/" + baseFi.Name);
                            //save po image in prob files
                            File.Copy(fi.FullName, probFrames_di.FullName + "/" + fi.Name);
                            baseFi = fi;
                            baseImg = new Image(fi.FullName);
                            if (drawImages())
                            {
                                ImageBox ib = getBaseImageBox();
                                CvInvoke.Resize(baseImg.Mat, displayedBase, new System.Drawing.Size(ib.Width, ib.Height));
                                ib.Image = displayedBase;
                            }

                            textBox13.Text = baseFi.FullName;
                            continue;
                        }
                        foreach (FileInfo fi2 in extracted_di.GetFiles())
                        {
                            if (fi.Name == fi2.Name)
                            {
                                fileExists = true;
                            }
                        }
                        if (!fileExists)
                        {
                            File.Copy(prevFi.FullName, extracted_di.FullName + "/" + fi.Name); //extract file to dir
                        }
                        baseFi = prevFi;
                        baseImg = new Image(prevFi.FullName);

                        if (drawImages())
                        {
                            ImageBox ib = getBaseImageBox();
                            CvInvoke.Resize(baseImg.Mat, displayedBase, new System.Drawing.Size(ib.Width, ib.Height));
                            ib.Image = displayedBase;
                        }

                        textBox13.Text = prevFi.FullName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    getConsoleTB().AppendText("image load error.." + "\n");
                    continue;
                }

                fileExists = false;

                Application.DoEvents();

                prevFi = fi;

            }


            textBox1.AppendText("..Success..");
            double end = System.DateTime.Now.TimeOfDay.TotalMilliseconds;
            textBox1.AppendText((end - start) / 1000 + "");

            StreamWriter sw = new StreamWriter(File.Create(extracted_di.FullName + "/" + "output.txt"));
            sw.Write(getConsoleTB().Text);
            sw.Close();

            progressBar1.Value = 100;

            MessageBox.Show("Done in " + ((end - start) / 1000) / 60 + " minutes");
        }

        public TextBox getConsoleTB()
        {
            return textBox1;
        }

        public bool drawImages()
        {
            return checkBox1.Checked;
        }

        public double getOverlap()
        {
            return double.Parse(textBox16.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (opf.ShowDialog() == DialogResult.OK)
            {
                videoFi = new FileInfo(opf.FileName);
                if(videoFi.Extension.ToLower() == ".mp4")
                {
                    textBox8.Text = videoFi.FullName;
                }
                else
                {
                    MessageBox.Show("mp4 video file not selected");
                }
                
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {

            progressBar1.Value = 0;

            getConsoleTB().Text = "";
            DirectoryInfo di = Directory.CreateDirectory(videoFi.DirectoryName + "/" + videoFi.Name.Split('.')[0] + "_extracted_frames_" + detectorType + "_" + extractorType + "_" + matcherType + "_" + getOverlap());
            DirectoryInfo probFrames_di = Directory.CreateDirectory(di.FullName + "/" + "problem frames");
            Text = "Ouput Directory: " + di.Name;

            Capture capture = new Capture(videoFi.FullName);

            ImageBox ib;

            Image baseframe = new Image();
            Image poframe = new Image();
            Image prevFrame = new Image();

            Mat displayedBase = new Mat();
            Mat displayedPO = new Mat();

            capture.Grab();
            capture.Retrieve(baseframe.Mat);

            int i = 1;
            String preZeroes = "";

            for(int j = 10000; j > 0; j/= 10)
            {
                if(i/j == 0)
                {
                    preZeroes += "0";
                }
            }

            String frameFileName = di.FullName + "/" + videoFi.Name.Split('.')[0] + "_frame_" + preZeroes + i + ".jpg";

            CvInvoke.Imwrite(frameFileName, baseframe.Mat);

            if (drawImages())
            {
                ib = getBaseImageBox();
                CvInvoke.Resize(baseframe.Mat, displayedBase, new System.Drawing.Size(ib.Width, ib.Height));
                ib.Image = displayedBase;
            }
            
            textBox13.Text = "Frame number: " + i + "";

            

            baseframe.Mat.CopyTo(prevFrame.Mat);
            //String name;
            double start = DateTime.Now.TimeOfDay.TotalMinutes;
            while (capture.Grab())
            {
                progressBar1.Value = (progressBar1.Value + 10) % 99;
                i++;
                capture.Retrieve(poframe.Mat);
                textBox14.Text = "Frame number: " + i + "";

                if (drawImages())
                {
                    ib = getPotentiallyOverlappingImageBox();
                    CvInvoke.Resize(poframe.Mat, displayedPO, new System.Drawing.Size(ib.Width, ib.Height));
                    ib.Image = displayedPO;
                }


               
                
                    try {
                    overlapEstimator = new OverlapEstimator(this, detectorType, extractorType, matcherType);
                    double overlap = overlapEstimator.execute(baseframe, poframe);
                        getConsoleTB().AppendText(textBox13.Text + " | " + textBox14.Text + " has " + overlap + "% overlap" + "\r\n");
                        if (overlap <= getOverlap())
                        {

                            if (overlap == 0)
                            {
                            //clearly something went wrong, abandon frame
                            CvInvoke.Imwrite(probFrames_di.FullName + "/" + textBox13.Text.Split(':')[1] + ".jpg", baseframe.Mat);
                            CvInvoke.Imwrite(probFrames_di.FullName + "/" + textBox14.Text.Split(':')[1] + ".jpg", poframe.Mat);
                                poframe.Mat.CopyTo(baseframe.Mat);

                            if (drawImages())
                            {
                                ib = getBaseImageBox();
                                CvInvoke.Resize(baseframe.Mat, displayedBase, new System.Drawing.Size(ib.Width, ib.Height));
                                ib.Image = displayedBase;
                            }
                            textBox13.Text = "Frame number: " + i + "";
                            continue;
                            }

                            prevFrame.Mat.CopyTo(baseframe.Mat);

                            preZeroes = "";

                            for (int j = 10000; j > 0; j /= 10)
                            {
                                if ((i - 1) / j == 0)
                                {
                                    preZeroes += "0";
                                }
                            }

                            frameFileName = di.FullName + "/" + videoFi.Name.Split('.')[0] + "_frame_" + preZeroes + (i - 1) + ".jpg";

                            CvInvoke.Imwrite(frameFileName, baseframe.Mat);

                            if (drawImages())
                            {
                                ib = getBaseImageBox();
                                CvInvoke.Resize(baseframe.Mat, displayedBase, new System.Drawing.Size(ib.Width, ib.Height));
                                ib.Image = displayedBase;
                            }

                            textBox13.Text = "Frame number: " + (i - 1) + "";
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("overlap estimator problem");
                        MessageBox.Show(ex.ToString());
                    }
                    
                

                Application.DoEvents();
                poframe.Mat.CopyTo(prevFrame.Mat);

            }
            double end = DateTime.Now.TimeOfDay.TotalMinutes;

            String finalOutput = "Completed ripping " + i + " frames in: " + (end - start) + " minutes";

            getConsoleTB().AppendText(finalOutput + "\r\n");

            StreamWriter sw = new StreamWriter(File.Create(di.FullName + "/" + "output.txt"));
            sw.Write(getConsoleTB().Text);
            sw.Close();

            progressBar1.Value = 100;

            MessageBox.Show(finalOutput);

            
        }

        private void bRISKToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            detectorType = DetectorType.BRISK;
            detectorTypeTB.Text = detectorType.ToString();
        }

        private void fASTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            detectorType = DetectorType.FAST;
            detectorTypeTB.Text = detectorType.ToString();
        }

        private void bRIEFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractorType = ExtractorType.BRIEF;
            extractorTypeTB.Text = extractorType.ToString();
        }

        private void bRISKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractorType = ExtractorType.BRISK;
            extractorTypeTB.Text = extractorType.ToString();
        }

        private void fREAKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractorType = ExtractorType.FREAK;
            extractorTypeTB.Text = extractorType.ToString();
        }

        private void hammingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            matcherType = MatcherType.HAMMING;
            matcherTypeTB.Text = matcherType.ToString();
        }

        private void hamming2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            matcherType = MatcherType.HAMMING2;
            matcherTypeTB.Text = matcherType.ToString();
        }

        private void infToolStripMenuItem_Click(object sender, EventArgs e)
        {
            matcherType = MatcherType.INF;
            matcherTypeTB.Text = matcherType.ToString();
        }

        private void l1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            matcherType = MatcherType.L1;
            matcherTypeTB.Text = matcherType.ToString();
        }

        private void l2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            matcherType = MatcherType.L2;
            matcherTypeTB.Text = matcherType.ToString();
        }

        private void l2SqrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            matcherType = MatcherType.L2SQR;
            matcherTypeTB.Text = matcherType.ToString();
        }

        private void MainUI_Load(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }

}
