using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using McOverlap;
using McOverlapCore;
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
        private Mat base_mat_gray = null;
        private Mat po_mat_gray = null;

        private String detectorType;
        private String extractorType;
        private String matcherType;

        private OverlapEstimator overlapEstimator;


        public MainUI()
        {
            InitializeComponent();
            CenterToScreen();
            imageBox1.FunctionalMode = ImageBox.FunctionalModeOption.PanAndZoom;
            imageBox2.FunctionalMode = ImageBox.FunctionalModeOption.PanAndZoom;
            imageBox3.FunctionalMode = ImageBox.FunctionalModeOption.PanAndZoom;

            //set data for comboboxes used:
            comboBox1.Items.Add(DistanceType.Inf);
            comboBox1.Items.Add(DistanceType.L1);
            comboBox1.Items.Add(DistanceType.L2);
            comboBox1.Items.Add(DistanceType.L2Sqr);
            comboBox1.Items.Add(DistanceType.Hamming);
            comboBox1.Items.Add(DistanceType.Hamming2);
            comboBox1.SelectedItem = DistanceType.L2;

            comboBox2.Items.Add(16);
            comboBox2.Items.Add(32);
            comboBox2.Items.Add(64);
            comboBox2.SelectedItem = 32;

            comboBox3.Items.Add(FastDetector.DetectorType.Type5_8);
            comboBox3.Items.Add(FastDetector.DetectorType.Type7_12);
            comboBox3.Items.Add(FastDetector.DetectorType.Type9_16);
            comboBox3.SelectedItem = FastDetector.DetectorType.Type9_16;

            //set default detector, extractor and matcher types:
            detectorType = detectorTypeTextBox_brisk.Text;
            extractorType = extractorTypeTextBox_freak.Text;
            matcherType = matcherTypeTextBox_bruteforce.Text;

            //enable appropriate panels:
            detectorPanel_brisk.Enabled = true;
            detectorPanel_brisk.Visible = true;
            extractorPanel_freak.Enabled = true;
            extractorPanel_freak.Visible = true;

        }

        //Load Base Image Button:
        private void button1_Click(object sender, EventArgs e)
        {
            if(base_mat_gray != null)
            {
                base_mat_gray.Dispose();
            }
            if (opf.ShowDialog() == DialogResult.OK){
                textBox13.Text = opf.FileName;
                base_mat_gray = CvInvoke.Imread(opf.FileName, Emgu.CV.CvEnum.LoadImageType.Grayscale);
                if (drawImages())
                {
                    imageBox1.Image = base_mat_gray.ToImage<Gray, byte>().Resize(imageBox1.Width, imageBox1.Height, Emgu.CV.CvEnum.Inter.Linear);
                }
                
            }
            
        }
        //Load Potentially Overlapping Image Button:
        private void button2_Click(object sender, EventArgs e)
        {
            if(po_mat_gray != null)
            {
                po_mat_gray.Dispose();
            }
            if (opf.ShowDialog() == DialogResult.OK)
            {
                textBox14.Text = opf.FileName;
                po_mat_gray = CvInvoke.Imread(opf.FileName, Emgu.CV.CvEnum.LoadImageType.Grayscale);
                if (drawImages())
                {
                    imageBox3.Image = po_mat_gray.ToImage<Gray, byte>().Resize(imageBox3.Width, imageBox3.Height, Emgu.CV.CvEnum.Inter.Linear);
                }
                
            }
        }
        //Estimate Overlap Button:
        private void button3_Click(object sender, EventArgs e)
        {

            using (overlapEstimator = new OverlapEstimator(this, base_mat_gray, po_mat_gray, detectorType, extractorType, matcherType))
            {
                double dblOverlap = overlapEstimator.execute();
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
                    base_mat_gray = CvInvoke.Imread(fi.FullName, Emgu.CV.CvEnum.LoadImageType.Grayscale);
                    if (drawImages())
                    {
                        imageBox1.Image = base_mat_gray.ToImage<Gray, byte>().Resize(imageBox1.Width, imageBox1.Height, Emgu.CV.CvEnum.Inter.Linear);
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
                    po_mat_gray = CvInvoke.Imread(fi.FullName, Emgu.CV.CvEnum.LoadImageType.Grayscale);
                    if (drawImages())
                    {
                        imageBox3.Image = po_mat_gray.ToImage<Gray, byte>().Resize(imageBox3.Width, imageBox3.Height, Emgu.CV.CvEnum.Inter.Linear);
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
                    base_mat_gray = CvInvoke.Imread(prevfi.FullName, Emgu.CV.CvEnum.LoadImageType.Grayscale);
                    if (drawImages())
                    {
                        imageBox1.Image = base_mat_gray.ToImage<Gray, byte>().Resize(imageBox1.Width, imageBox1.Height, Emgu.CV.CvEnum.Inter.Linear);
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
                    po_mat_gray = CvInvoke.Imread(prevfi.FullName, Emgu.CV.CvEnum.LoadImageType.Grayscale);
                    if (drawImages())
                    {
                        imageBox3.Image = po_mat_gray.ToImage<Gray, byte>().Resize(imageBox3.Width, imageBox3.Height, Emgu.CV.CvEnum.Inter.Linear);
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

            getConsoleTB().Text = "";
            double start = System.DateTime.Now.TimeOfDay.TotalMilliseconds;
            extracted_di = Directory.CreateDirectory(doAll_di.FullName + "_extracted_" + getOverlap() + "_" + detectorType + "_" + extractorType + "_" + matcherType);

            FileInfo baseFi = doAll_di.GetFiles()[0]; //get first file
            File.Copy(baseFi.FullName, extracted_di.FullName + "/" + baseFi.Name); //extract file to dir
            base_mat_gray = CvInvoke.Imread(baseFi.FullName, Emgu.CV.CvEnum.LoadImageType.Grayscale); //make base file
            if (drawImages())
            {
                ImageBox ib = getBaseImageBox();
                ib.Image = base_mat_gray.ToImage<Gray, byte>().Resize(ib.Width, ib.Height, Emgu.CV.CvEnum.Inter.Linear);
            }
                
            textBox13.Text = baseFi.FullName;
            bool fileExists = false;
            FileInfo prevFi = baseFi;
            foreach (FileInfo fi in doAll_di.GetFiles())
            {

                
                po_mat_gray = CvInvoke.Imread(fi.FullName, Emgu.CV.CvEnum.LoadImageType.Grayscale);
                textBox14.Text = fi.FullName;
                try
                {
                    overlapEstimator = new OverlapEstimator(this, base_mat_gray, po_mat_gray, detectorType, extractorType, matcherType);
                    if (drawImages())
                    {
                        ImageBox ib = getPotentiallyOverlappingImageBox();
                        ib.Image = po_mat_gray.ToImage<Gray, byte>().Resize(ib.Width, ib.Height, Emgu.CV.CvEnum.Inter.Linear);
                    }


                    double overlap = overlapEstimator.execute();
                    textBox1.AppendText(baseFi.Name + " : " + fi.Name + " has " + overlap + "% overlap" + "\r\n");
                    if (overlap <= getOverlap())
                    {
                        if (overlap == 0)
                        {
                            //clearly something went wrong, abandon frame
                            baseFi = fi;
                            base_mat_gray = CvInvoke.Imread(fi.FullName, Emgu.CV.CvEnum.LoadImageType.Grayscale); //make base file
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
                        base_mat_gray = CvInvoke.Imread(prevFi.FullName, Emgu.CV.CvEnum.LoadImageType.Grayscale); //make base file
                        textBox13.Text = fi.FullName;
                    }
                }
                catch
                {
                    getConsoleTB().AppendText("image load error.." + "\n");
                    continue;
                }

                fileExists = false;

                this.Update();

                prevFi = fi;

            }
            

            textBox1.AppendText( "..Success..");
            double end = System.DateTime.Now.TimeOfDay.TotalMilliseconds;
            textBox1.AppendText((end - start)/1000 + "");

            StreamWriter sw = new StreamWriter(File.Create(extracted_di.FullName + "/" + "output.txt"));
            sw.Write(getConsoleTB().Text);
            sw.Close();

            MessageBox.Show("Done in " + ((end - start) / 1000) / 60 + " minutes");
        }

        public double detectorSurfHessianThresh()
        {
            return double.Parse(textBox10.Text);
        }

        public int detectorSurfNumOctaves()
        {
            return int.Parse(textBox11.Text);
        }

        public int detectorSurfNumOctaveLevels()
        {
            return int.Parse(textBox12.Text);
        }

        public bool detectorSurfExtended()
        {
            return checkBox2.Checked;
        }

        public bool detectorSurfUpright()
        {
            return checkBox3.Checked;
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

        private void bRIEFToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if(extractorPanel_brisk.Enabled || extractorPanel_brisk.Visible)
            {
                extractorPanel_brisk.Visible = false;
                extractorPanel_brisk.Enabled = false;
            }

            if(extractorPanel_freak.Enabled || extractorPanel_freak.Visible)
            {
                extractorPanel_freak.Visible = false;
                extractorPanel_freak.Enabled = false;
            }

            if(extractorPanel_surf.Enabled || extractorPanel_surf.Visible)
            {
                extractorPanel_surf.Visible = false;
                extractorPanel_surf.Enabled = false;
            }

            extractorPanel_brief.Enabled = true;
            extractorPanel_brief.Visible = true;

            extractorType = extractorTypeTextBox_brief.Text;
        }

        private void fREAKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (extractorPanel_brief.Enabled || extractorPanel_brief.Visible)
            {
                extractorPanel_brief.Visible = false;
                extractorPanel_brief.Enabled = false;
            }

            if (extractorPanel_brisk.Enabled || extractorPanel_brisk.Visible)
            {
                extractorPanel_brisk.Visible = false;
                extractorPanel_brisk.Enabled = false;
            }

            if (extractorPanel_surf.Enabled || extractorPanel_surf.Visible)
            {
                extractorPanel_surf.Visible = false;
                extractorPanel_surf.Enabled = false;
            }

            extractorPanel_freak.Enabled = true;
            extractorPanel_freak.Visible = true;

            extractorType = extractorTypeTextBox_freak.Text;

        }

        private void sURFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (extractorPanel_brief.Enabled || extractorPanel_brief.Visible)
            {
                extractorPanel_brief.Visible = false;
                extractorPanel_brief.Enabled = false;
            }

            if (extractorPanel_brisk.Enabled || extractorPanel_brisk.Visible)
            {
                extractorPanel_brisk.Visible = false;
                extractorPanel_brisk.Enabled = false;
            }

            if (extractorPanel_freak.Enabled || extractorPanel_freak.Visible)
            {
                extractorPanel_freak.Visible = false;
                extractorPanel_freak.Enabled = false;
            }

            extractorPanel_surf.Enabled = true;
            extractorPanel_surf.Visible = true;

            extractorType = extractorTypeTextBox_surf.Text;


        }

        public int briefDescriptorSize()
        {
            return (int)comboBox2.SelectedItem;
        }

        public bool isFreakOrientationNormalized()
        {
            return checkBox5.Checked;
        }

        public bool isFreakScaleNormalized()
        {
            return checkBox6.Checked;
        }

        public float freakPatternScale()
        {
            return float.Parse(textBox19.Text);
        }

        public int freakNumOctaves()
        {
            return int.Parse(textBox20.Text);
        }

        public int detectorBriskThreshold()
        {
            return int.Parse(textBox23.Text);
        }

        public int detectorBriskOctaveLayers()
        {
            return int.Parse(textBox24.Text);
        }

        public float detectorBriskPatternScale()
        {
            return float.Parse(textBox25.Text);
        }

        private void bRISKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (extractorPanel_brief.Enabled || extractorPanel_brief.Visible)
            {
                extractorPanel_brief.Visible = false;
                extractorPanel_brief.Enabled = false;
            }

            if (extractorPanel_freak.Enabled || extractorPanel_freak.Visible)
            {
                extractorPanel_freak.Visible = false;
                extractorPanel_freak.Enabled = false;
            }

            if (extractorPanel_surf.Enabled || extractorPanel_surf.Visible)
            {
                extractorPanel_surf.Visible = false;
                extractorPanel_surf.Enabled = false;
            }

            extractorPanel_brisk.Enabled = true;
            extractorPanel_brisk.Visible = true;

            extractorType = extractorTypeTextBox_brisk.Text;
        }

        

        public int fastThreshold()
        {
            return int.Parse(textBox26.Text);
        }

        public bool fastNonMaxSuppression()
        {
            return checkBox7.Checked;
        }

        public FastDetector.DetectorType fastDetectorType()
        {
            return (FastDetector.DetectorType)comboBox3.SelectedItem;
        }

        public int extractorBriskThreshold()
        {
            return int.Parse(textBox33.Text);
        }

        public int extractorBriskOctaveLayers()
        {
            return int.Parse(textBox32.Text);
        }

        public float extractorBriskPatternScale()
        {
            return float.Parse(textBox31.Text);
        }

        public double extractorSurfHessianThreshold()
        {
            return double.Parse(textBox28.Text);
        }

        public int extractorSurfOctaves()
        {
            return int.Parse(textBox29.Text);
        }

        public int extractorSurfOctaveLayers()
        {
            return int.Parse(textBox30.Text);
        }

        public bool extractorSurfExtended()
        {
            return checkBox9.Checked;
        }

        public bool extractorSurfUpright()
        {
            return checkBox8.Checked;
        }

        public DistanceType bruteforceMatcherType()
        {
            return (DistanceType)comboBox1.SelectedItem;
        }

        public bool bruteForceCrosscheck()
        {
            return checkBox4.Checked;
        }

        private void bRISKToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if(detectorPanel_surf.Enabled || detectorPanel_surf.Visible)
            {
                detectorPanel_surf.Visible = false;
                detectorPanel_surf.Enabled = false;
            }

            if (detectorPanel_fast.Enabled || detectorPanel_fast.Visible)
            {
                detectorPanel_fast.Visible = false;
                detectorPanel_fast.Enabled = false;
            }

            detectorPanel_brisk.Enabled = true;
            detectorPanel_brisk.Visible = true;

            detectorType = detectorTypeTextBox_brisk.Text;
        }

        private void fASTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (detectorPanel_surf.Enabled || detectorPanel_surf.Visible)
            {
                detectorPanel_surf.Visible = false;
                detectorPanel_surf.Enabled = false;
            }

            if (detectorPanel_brisk.Enabled || detectorPanel_brisk.Visible)
            {
                detectorPanel_brisk.Visible = false;
                detectorPanel_brisk.Enabled = false;
            }

            detectorPanel_fast.Enabled = true;
            detectorPanel_fast.Visible = true;

            detectorType = detectorTypeTextBox_fast.Text;
        }

        private void sURFToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (detectorPanel_fast.Enabled || detectorPanel_fast.Visible)
            {
                detectorPanel_fast.Visible = false;
                detectorPanel_fast.Enabled = false;
            }

            if (detectorPanel_brisk.Enabled || detectorPanel_brisk.Visible)
            {
                detectorPanel_brisk.Visible = false;
                detectorPanel_brisk.Enabled = false;
            }

            detectorPanel_surf.Enabled = true;
            detectorPanel_surf.Visible = true;

            detectorType = detectorTypeTextBox_surf.Text;
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

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            getConsoleTB().Text = "";
            double start = System.DateTime.Now.TimeOfDay.TotalMilliseconds;
            extracted_di = Directory.CreateDirectory(videoFi.FullName.Substring(0, videoFi.FullName.LastIndexOf(".")) + "_extracted_" + getOverlap() + "_" + detectorType + "_" + extractorType + "_" + matcherType);
            Video video = new Video(videoFi.FullName);
            Mat baseFrame = video.NextFrame();
            if (drawImages())
            {
                ImageBox ib = getBaseImageBox();
                ib.Image = baseFrame.ToImage<Gray, byte>().Resize(ib.Width, ib.Height, Emgu.CV.CvEnum.Inter.Linear);
                textBox13.Text = "Frame number: " + video.FrameNumber + "";

            }
            //write it to file:
            String preZeroes = "";
            for (int j = 10000; j > 0; j /= 10)
            {
                if (video.FrameNumber / j == 0)
                {
                    preZeroes += "0";
                }
            }
            String frameFileName = extracted_di.FullName + "/" + videoFi.Name + "_frame_" + preZeroes + video.FrameNumber + ".jpg";
            CvInvoke.Imwrite(frameFileName, baseFrame);
            Mat poFrame;
            Mat prevFrame = baseFrame;
            while (!video.NextFrame().IsEmpty)
            {
                poFrame = video.Frame;
                overlapEstimator = new OverlapEstimator(this, baseFrame.Split()[0], poFrame.Split()[0], detectorType, extractorType, matcherType);
                if (drawImages())
                {
                    ImageBox ib = getPotentiallyOverlappingImageBox();
                    ib.Image = poFrame.ToImage<Gray, byte>().Resize(ib.Width, ib.Height, Emgu.CV.CvEnum.Inter.Linear);
                    textBox14.Text = "Frame number: " + video.FrameNumber + "";
                }
                double overlap = overlapEstimator.execute();
                getConsoleTB().AppendText(textBox13.Text + ":" + textBox14.Text + " has " + overlap + "% overlap" + "\r\n");
                if (overlap <= getOverlap())
                    {
                        baseFrame = prevFrame;
                    if (drawImages())
                    {
                        ImageBox ib = getBaseImageBox();
                        ib.Image = baseFrame.ToImage<Gray, byte>().Resize(ib.Width, ib.Height, Emgu.CV.CvEnum.Inter.Linear);
                        textBox13.Text = "Frame Number: " + (video.FrameNumber - 1) + "";
                    }
                    //write it to file:
                    preZeroes = "";
                        for (int j = 10000; j > 0; j /= 10)
                        {
                            if (video.FrameNumber / j == 0)
                            {
                                preZeroes += "0";
                            }
                        }
                        frameFileName = extracted_di.FullName + "/" + videoFi.Name + "_frame_" + preZeroes + video.FrameNumber + ".jpg";
                        CvInvoke.Imwrite(frameFileName, baseFrame);
                    }

                this.Update();
                prevFrame = poFrame;

            }

            double end = System.DateTime.Now.TimeOfDay.TotalMilliseconds;

            StreamWriter sw = new StreamWriter(File.Create(extracted_di.FullName + "/" + "output.txt"));
            sw.Write(getConsoleTB().Text);
            sw.Close();

            MessageBox.Show("Done in " + ((end - start)/1000)/60 + " minutes");




        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
