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

        private String lastFileDirectory = "";

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
            baseImageIB.FunctionalMode = ImageBox.FunctionalModeOption.PanAndZoom;
            estimatedOverlapIB.FunctionalMode = ImageBox.FunctionalModeOption.PanAndZoom;
            imageBox3.FunctionalMode = ImageBox.FunctionalModeOption.PanAndZoom;

            //set default detector, extractor and matcher types:
            detectorType = DetectorType.FAST;
            extractorType = ExtractorType.BRIEF;
            matcherType = MatcherType.L2;

            //display current settings:
            detectorTypeTB.Text = detectorType.ToString();
            extractorTypeTB.Text = extractorType.ToString();
            matcherTypeTB.Text = matcherType.ToString();

            Text = "McOverlap (|___|)";
        }

        //Load Base Image Button:
        private void button1_Click(object sender, EventArgs e)
        {
            if(lastFileDirectory != "")
            {
                opf.InitialDirectory = lastFileDirectory;
            }

            if (opf.ShowDialog() == DialogResult.OK){
                FileInfo fi = new FileInfo(opf.FileName);
                lastFileDirectory = fi.DirectoryName;
                baseImagePathTB.Text = opf.FileName;
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
            if (lastFileDirectory != "")
            {
                opf.InitialDirectory = lastFileDirectory;
            }

            if (opf.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(opf.FileName);
                lastFileDirectory = fi.DirectoryName;
                poImagePathTB.Text = opf.FileName;
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
                ConsoleOutputTB.AppendText(s);
            }
            
        }

        public ImageBox getBaseImageBox()
        {
            return baseImageIB;
        }

        public ImageBox getPotentiallyOverlappingImageBox()
        {
            return imageBox3;
        }

        public ImageBox getEstimatedOverlapImageBox()
        {
            return estimatedOverlapIB;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(baseImagePathTB.Text == "")
            {
                return; //don't need to do anything
            }

            String base_image_path = baseImagePathTB.Text;

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
                    baseImagePathTB.Text = fi.FullName;
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
            if (poImagePathTB.Text == "")
            {
                return; //don't need to do anything
            }

            String po_image_path = poImagePathTB.Text;

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
                    poImagePathTB.Text = fi.FullName;
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
            if (baseImagePathTB.Text == "")
            {
                return; //don't need to do anything
            }

            String base_image_path = baseImagePathTB.Text;

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

                    baseImagePathTB.Text = prevfi.FullName;
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
            if (poImagePathTB.Text == "")
            {
                return; //don't need to do anything
            }

            String po_image_path = poImagePathTB.Text;

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

                    poImagePathTB.Text = prevfi.FullName;
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

            int totalImagesProcessed = 0;
            int totalImagesExtracted = 0;
            int totalFailedMatches = 0;

            String outputDirectory = doAll_di.FullName + "_extracted_" + getOverlap() + "_" + detectorType + "_" + extractorType + "_" + matcherType;
            //check if directory already exists:
            //if it does: create a new one appending some counter to the name

            int i = 1;

            String outputDirectorySuffix = "";

            foreach(DirectoryInfo di in doAll_di.Parent.GetDirectories())
            {
                MessageBox.Show(di.FullName + "\r\n" + (outputDirectory + outputDirectorySuffix));
                if (di.FullName == (outputDirectory + outputDirectorySuffix))
                {
                    outputDirectorySuffix = "(" + i + ")";
                    i++;
                }
            }

            outputDirectory += outputDirectorySuffix;

            int currentSequenceNumber = 0;

            progressBar1.Value = 0;
            getConsoleTB().Text = "";
            double start = System.DateTime.Now.TimeOfDay.TotalMilliseconds;

            MessageBox.Show(outputDirectory);

            extracted_di = Directory.CreateDirectory(outputDirectory);
            DirectoryInfo probFrames_di = Directory.CreateDirectory(extracted_di.FullName + "/" + "problem frames");

            FileInfo baseFi = doAll_di.GetFiles()[0]; //get first file
            File.Copy(baseFi.FullName, extracted_di.FullName + "/" + baseFi.Name); //extract file to dir
            totalImagesExtracted++;
            baseImg = new Image(baseFi.FullName);
            if (drawImages())
            {
                ImageBox ib = getBaseImageBox();
                CvInvoke.Resize(baseImg.Mat, displayedBase, new System.Drawing.Size(ib.Width, ib.Height));
                ib.Image = displayedBase;
            }

            baseImagePathTB.Text = baseFi.FullName;
            bool fileExists = false;
            FileInfo prevFi = baseFi;

            foreach (FileInfo fi in doAll_di.GetFiles())
            {

                progressBar1.Value = (progressBar1.Value + 10) % 100;
                poImg = new Image(fi.FullName);
                poImagePathTB.Text = fi.FullName;
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
                    ConsoleOutputTB.AppendText(baseFi.Name + " : " + fi.Name + " has " + overlap + "% overlap" + "\r\n");
                    if (overlap <= getOverlap())
                    {
                        if (overlap == 0)
                        {
                            //clearly something went wrong, abandon frame
                            //save base image in prob files

                            totalFailedMatches++;

                            bool baseFileExists = false;
                            bool poFileExists = false;

                            foreach(FileInfo fi3 in probFrames_di.GetFiles())
                            {
                                if(fi3.Name == baseFi.Name)
                                {
                                    baseFileExists = true;
                                }
                                if(fi3.Name == fi.Name)
                                {
                                    poFileExists = true;
                                }
                                if(baseFileExists && poFileExists)
                                {
                                    break;
                                }
                            }
                            if (!baseFileExists)
                            {
                                File.Copy(baseFi.FullName, probFrames_di.FullName + "/" + baseFi.Name);
                            }
                            if (!poFileExists)
                            {
                                File.Copy(fi.FullName, probFrames_di.FullName + "/" + fi.Name);
                            }
                            
                            baseFi = fi;
                            baseImg = new Image(fi.FullName);
                            if (drawImages())
                            {
                                ImageBox ib = getBaseImageBox();
                                CvInvoke.Resize(baseImg.Mat, displayedBase, new System.Drawing.Size(ib.Width, ib.Height));
                                ib.Image = displayedBase;
                            }

                            baseImagePathTB.Text = baseFi.FullName;
                            totalImagesProcessed++;
                            continue;
                        }
                        foreach (FileInfo fi2 in extracted_di.GetFiles())
                        {
                            if (prevFi.Name == fi2.Name)
                            {
                                fileExists = true;
                            }
                        }
                        if (!fileExists)
                        {
                            File.Copy(prevFi.FullName, extracted_di.FullName + "/" + prevFi.Name); //extract file to dir
                            totalImagesExtracted++;
                        }
                        baseFi = prevFi;
                        baseImg = new Image(prevFi.FullName);
                        overlapEstimator = new OverlapEstimator(this, detectorType, extractorType, matcherType);
                        overlap = overlapEstimator.execute(baseImg, poImg);
                        ConsoleOutputTB.AppendText(baseFi.Name + " : " + fi.Name + " has " + overlap + "% overlap" + "\r\n");
                        if ( overlap <= getOverlap())
                        {
                            //save current potentially overlapping image as well:
                            bool poFileExists = false;
                            foreach(FileInfo fileInfo2 in extracted_di.GetFiles())
                            {
                                if(fileInfo2.Name == fi.Name)
                                {
                                    poFileExists = true;
                                    break;
                                }
                            }
                            if (!poFileExists)
                            {
                                File.Copy(fi.FullName, extracted_di.FullName + "/" + fi.Name); //extract file to dir
                                totalImagesExtracted++;
                            }
                        }

                        if (drawImages())
                        {
                            ImageBox ib = getBaseImageBox();
                            CvInvoke.Resize(baseImg.Mat, displayedBase, new System.Drawing.Size(ib.Width, ib.Height));
                            ib.Image = displayedBase;
                        }

                        

                        baseImagePathTB.Text = prevFi.FullName;
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

                totalImagesProcessed++;

            }


            ConsoleOutputTB.AppendText("..Success..");
            double end = System.DateTime.Now.TimeOfDay.TotalMilliseconds;
            ConsoleOutputTB.AppendText((end - start) / 1000 + "");

            double time = Math.Round(((end - start) / 1000)/60);

            StreamWriter sw = new StreamWriter(File.Create(extracted_di.FullName + "/" + "output.txt"));
            sw.WriteLine("Images Processes: " + totalImagesProcessed);
            sw.WriteLine("Images Extracted: " + totalImagesExtracted);
            sw.WriteLine("Failed Matches: " + totalFailedMatches);
            sw.Write(getConsoleTB().Text);
            sw.Close();

            progressBar1.Value = 100;

            MessageBox.Show("Done in " + time + " minutes" + "\r\n" + "Output Directory: " + "\r\n" + extracted_di.FullName, "Report");
        }

        public TextBox getConsoleTB()
        {
            return ConsoleOutputTB;
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
            if (lastFileDirectory != "")
            {
                opf.InitialDirectory = lastFileDirectory;
            }
            if (opf.ShowDialog() == DialogResult.OK)
            {
                videoFi = new FileInfo(opf.FileName);

                
                
                

                lastFileDirectory = videoFi.DirectoryName;
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

            int totalFramesProcessed = 1;
            int totalFramesExtracted = 0;
            int totalFailedMatches = 0;

            //find frame rate: gopro asserts 24 frames per second..
            //can have user input frames per second and length in seconds
            //then give an estimate on completion time

            //double fps = 24;
            //double lengthInSeconds = 194;
            //double percentageCompleted = (totalFramesProcessed / (fps * lengthInSeconds))*100;

            //what about time remaining??:

            String outputDirectory = videoFi.DirectoryName + "\\" + videoFi.Name.Split('.')[0] + "_extracted_frames_" + detectorType + "_" + extractorType + "_" + matcherType + "_" + getOverlap();
            //check if directory already exists:
            //if it does: create a new one appending some counter to the name

            int i2 = 1;

            String outputDirectorySuffix = "";

            DirectoryInfo videoDirIn = videoFi.Directory;

            foreach (DirectoryInfo di2 in videoDirIn.GetDirectories())
            {
                if (di2.FullName == (outputDirectory + outputDirectorySuffix))
                {
                    outputDirectorySuffix = "(" + i2 + ")";
                    i2++;
                }
            }

            outputDirectory += outputDirectorySuffix;

            //check if directory already exists:
            //if it does: create a new one appending some counter to the name

            int currentSequenceNumber = 0;

            progressBar1.Value = 0;

            getConsoleTB().Text = "";

            MessageBox.Show(outputDirectory);

            DirectoryInfo di = Directory.CreateDirectory(outputDirectory);
            DirectoryInfo probFrames_di = Directory.CreateDirectory(di.FullName + "/" + "problem frames");

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
            
            baseImagePathTB.Text = "Frame number: " + i + "";

            

            baseframe.Mat.CopyTo(prevFrame.Mat);
            //String name;
            double start = DateTime.Now.TimeOfDay.TotalMinutes;
            while (capture.Grab())
            {
                //percentageCompleted = ((totalFramesProcessed / (fps * lengthInSeconds))) * 100;
                //percentageCompleted = Math.Round(percentageCompleted);
                //progressBar1.Value = (int)percentageCompleted;
                progressBar1.Value = (progressBar1.Value + 10) % 100;

                i++;
                capture.Retrieve(poframe.Mat);
                poImagePathTB.Text = "Frame number: " + i + "";

                if (drawImages())
                {
                    ib = getPotentiallyOverlappingImageBox();
                    CvInvoke.Resize(poframe.Mat, displayedPO, new System.Drawing.Size(ib.Width, ib.Height));
                    ib.Image = displayedPO;
                }


               
                
                    try {
                    overlapEstimator = new OverlapEstimator(this, detectorType, extractorType, matcherType);
                    double overlap = overlapEstimator.execute(baseframe, poframe);
                        getConsoleTB().AppendText(baseImagePathTB.Text + " | " + poImagePathTB.Text + " has " + overlap + "% overlap" + "\r\n");
                        if (overlap <= getOverlap())
                        {

                            if (overlap == 0)
                            {
                                //clearly something went wrong, abandon frame
                                totalFailedMatches++;
                                bool baseFrameExists = false;
                                bool poFrameExists = false;

                                foreach(FileInfo frameFi in probFrames_di.GetFiles())
                                {
                                    if(frameFi.Name == baseImagePathTB.Text.Split(':')[1] + ".jpg")
                                    {
                                        baseFrameExists = true;
                                    }
                                    if(frameFi.Name == poImagePathTB.Text.Split(':')[1] + ".jpg")
                                    {
                                        poFrameExists = true;
                                    }
                                    if(baseFrameExists && poFrameExists)
                                    {
                                        break;
                                    }
                                }
                                if (!baseFrameExists)
                                {
                                    CvInvoke.Imwrite(probFrames_di.FullName + "/" + baseImagePathTB.Text.Split(':')[1] + ".jpg", baseframe.Mat);
                                }
                                if (!poFrameExists)
                                {
                                    CvInvoke.Imwrite(probFrames_di.FullName + "/" + poImagePathTB.Text.Split(':')[1] + ".jpg", poframe.Mat);
                                }
                            
                                    poframe.Mat.CopyTo(baseframe.Mat);

                                if (drawImages())
                                {
                                    ib = getBaseImageBox();
                                    CvInvoke.Resize(baseframe.Mat, displayedBase, new System.Drawing.Size(ib.Width, ib.Height));
                                    ib.Image = displayedBase;
                                }
                                baseImagePathTB.Text = "Frame number: " + i + "";
                                totalFramesProcessed++;
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

                            bool frameExists = false;
                            foreach(FileInfo fi4 in di.GetFiles())
                            {
                                if(fi4.FullName == frameFileName)
                                {
                                    frameExists = true;
                                    break;
                                }
                            }

                            if (!frameExists)
                            {
                                CvInvoke.Imwrite(frameFileName, baseframe.Mat);
                            totalFramesExtracted++;
                            }
                        baseImagePathTB.Text = "Frame number: " + i + "";
                        overlapEstimator = new OverlapEstimator(this, detectorType, extractorType, matcherType);
                        overlap = overlapEstimator.execute(baseframe, poframe);
                        ConsoleOutputTB.AppendText(baseImagePathTB.Text + " | " + poImagePathTB.Text + " has " + overlap + "% overlap" + "\r\n");
                        if (overlap <= getOverlap())
                        {
                            //save current potentially overlapping image as well:
                            bool poFileExists = false;

                            frameFileName = di.FullName + "/" + videoFi.Name.Split('.')[0] + "_frame_" + preZeroes + i + ".jpg";

                            foreach (FileInfo fileInfo2 in di.GetFiles())
                            {
                                if (fileInfo2.FullName == frameFileName)
                                {
                                    poFileExists = true;
                                    break;
                                }
                            }
                            if (!poFileExists)
                            {
                                CvInvoke.Imwrite(frameFileName, poframe.Mat);
                                totalFramesExtracted++;
                            }
                        }

                        if (drawImages())
                            {
                                ib = getBaseImageBox();
                                CvInvoke.Resize(baseframe.Mat, displayedBase, new System.Drawing.Size(ib.Width, ib.Height));
                                ib.Image = displayedBase;
                            }

                            baseImagePathTB.Text = "Frame number: " + (i - 1) + "";
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("overlap estimator problem");
                        MessageBox.Show(ex.ToString());
                    }
                    
                

                Application.DoEvents();
                poframe.Mat.CopyTo(prevFrame.Mat);
                totalFramesProcessed++;
            }
            double end = DateTime.Now.TimeOfDay.TotalMinutes;

            double time = Math.Round(end - start);

            String finalOutput = "Completed ripping " + i + " frames in: " + time + " minutes";

            getConsoleTB().AppendText(finalOutput + "\r\n");

            StreamWriter sw = new StreamWriter(File.Create(di.FullName + "/" + "output.txt"));
            sw.WriteLine("Frames Processes: " + totalFramesProcessed);
            sw.WriteLine("Frames Extracted: " + totalFramesExtracted);
            sw.WriteLine("Failed Matches: " + totalFailedMatches);
            sw.Write(getConsoleTB().Text);
            sw.Close();

            progressBar1.Value = 100;

            MessageBox.Show(finalOutput + "\r\n" + "Output Directory: " + "\r\n" + di.FullName, "Report");

            
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
