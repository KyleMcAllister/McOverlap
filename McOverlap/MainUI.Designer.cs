﻿namespace McOverlap
{
    partial class MainUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

            /*if(disposing && overlapEstimator != null)
            {
                overlapEstimator.Dispose();
            }*/
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ConsoleOutputTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.loadBaseImageBtn = new System.Windows.Forms.Button();
            this.loadPOImageBtn = new System.Windows.Forms.Button();
            this.estimateOverlapBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.baseImagePathTB = new System.Windows.Forms.TextBox();
            this.poImagePathTB = new System.Windows.Forms.TextBox();
            this.loadPrevBaseImageBtn = new System.Windows.Forms.Button();
            this.loadNextBaseImageBtn = new System.Windows.Forms.Button();
            this.loadPrevPOImageBtn = new System.Windows.Forms.Button();
            this.loadNextPOImageBtn = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.baseImageIB = new Emgu.CV.UI.ImageBox();
            this.estimatedOverlapIB = new Emgu.CV.UI.ImageBox();
            this.imageBox3 = new Emgu.CV.UI.ImageBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.detectorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bRISKToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fASTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bRIEFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bRISKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fREAKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hammingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hamming2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.l1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.l2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.l2SqrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label40 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.button11 = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.detectorTypeTB = new System.Windows.Forms.TextBox();
            this.extractorTypeTB = new System.Windows.Forms.TextBox();
            this.matcherTypeTB = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.baseImageIB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.estimatedOverlapIB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // ConsoleOutputTB
            // 
            this.ConsoleOutputTB.Location = new System.Drawing.Point(336, 358);
            this.ConsoleOutputTB.Multiline = true;
            this.ConsoleOutputTB.Name = "ConsoleOutputTB";
            this.ConsoleOutputTB.ReadOnly = true;
            this.ConsoleOutputTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConsoleOutputTB.Size = new System.Drawing.Size(282, 41);
            this.ConsoleOutputTB.TabIndex = 3;
            this.ConsoleOutputTB.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(91, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "BaseImage";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(670, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "PotentiallyOverlappingImage";
            // 
            // loadBaseImageBtn
            // 
            this.loadBaseImageBtn.Location = new System.Drawing.Point(236, 358);
            this.loadBaseImageBtn.Name = "loadBaseImageBtn";
            this.loadBaseImageBtn.Size = new System.Drawing.Size(81, 48);
            this.loadBaseImageBtn.TabIndex = 7;
            this.loadBaseImageBtn.Text = "Load B_Image";
            this.loadBaseImageBtn.UseVisualStyleBackColor = true;
            this.loadBaseImageBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // loadPOImageBtn
            // 
            this.loadPOImageBtn.Location = new System.Drawing.Point(858, 358);
            this.loadPOImageBtn.Name = "loadPOImageBtn";
            this.loadPOImageBtn.Size = new System.Drawing.Size(75, 45);
            this.loadPOImageBtn.TabIndex = 8;
            this.loadPOImageBtn.Text = "Load PO_Image";
            this.loadPOImageBtn.UseVisualStyleBackColor = true;
            this.loadPOImageBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // estimateOverlapBtn
            // 
            this.estimateOverlapBtn.Location = new System.Drawing.Point(347, 405);
            this.estimateOverlapBtn.Name = "estimateOverlapBtn";
            this.estimateOverlapBtn.Size = new System.Drawing.Size(262, 48);
            this.estimateOverlapBtn.TabIndex = 9;
            this.estimateOverlapBtn.Text = "Estimate_Overlap";
            this.estimateOverlapBtn.UseVisualStyleBackColor = true;
            this.estimateOverlapBtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(397, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "General:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Resize Scale: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(126, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "#Nearest Neighbhours:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(285, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Uniquness Threshold:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(436, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(142, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Vote for Size and Orientation";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(584, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Scale Increment:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(584, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "#Rotation Bins:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(703, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(160, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Ransac Reprojection Threshold:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(83, 34);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(37, 20);
            this.textBox2.TabIndex = 18;
            this.textBox2.Text = "0.1";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(249, 31);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(30, 20);
            this.textBox3.TabIndex = 19;
            this.textBox3.Text = "2";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(401, 31);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(29, 20);
            this.textBox4.TabIndex = 20;
            this.textBox4.Text = "0.8";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(668, 31);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(29, 20);
            this.textBox5.TabIndex = 21;
            this.textBox5.Text = "1.5";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(670, 56);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(29, 20);
            this.textBox6.TabIndex = 22;
            this.textBox6.Text = "20";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(869, 31);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(29, 20);
            this.textBox7.TabIndex = 23;
            this.textBox7.Text = "5";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.textBox6);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.textBox7);
            this.panel1.Location = new System.Drawing.Point(12, 463);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(924, 85);
            this.panel1.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(386, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 20);
            this.label1.TabIndex = 43;
            this.label1.Text = "Estimated Overlap:";
            // 
            // baseImagePathTB
            // 
            this.baseImagePathTB.Location = new System.Drawing.Point(12, 358);
            this.baseImagePathTB.Multiline = true;
            this.baseImagePathTB.Name = "baseImagePathTB";
            this.baseImagePathTB.Size = new System.Drawing.Size(220, 45);
            this.baseImagePathTB.TabIndex = 44;
            this.baseImagePathTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // poImagePathTB
            // 
            this.poImagePathTB.Location = new System.Drawing.Point(624, 358);
            this.poImagePathTB.Multiline = true;
            this.poImagePathTB.Name = "poImagePathTB";
            this.poImagePathTB.Size = new System.Drawing.Size(228, 45);
            this.poImagePathTB.TabIndex = 45;
            this.poImagePathTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // loadPrevBaseImageBtn
            // 
            this.loadPrevBaseImageBtn.Location = new System.Drawing.Point(12, 405);
            this.loadPrevBaseImageBtn.Name = "loadPrevBaseImageBtn";
            this.loadPrevBaseImageBtn.Size = new System.Drawing.Size(111, 52);
            this.loadPrevBaseImageBtn.TabIndex = 46;
            this.loadPrevBaseImageBtn.Text = "Load Previous Image";
            this.loadPrevBaseImageBtn.UseVisualStyleBackColor = true;
            this.loadPrevBaseImageBtn.Click += new System.EventHandler(this.button4_Click);
            // 
            // loadNextBaseImageBtn
            // 
            this.loadNextBaseImageBtn.Location = new System.Drawing.Point(129, 405);
            this.loadNextBaseImageBtn.Name = "loadNextBaseImageBtn";
            this.loadNextBaseImageBtn.Size = new System.Drawing.Size(101, 52);
            this.loadNextBaseImageBtn.TabIndex = 47;
            this.loadNextBaseImageBtn.Text = "Load Next Image";
            this.loadNextBaseImageBtn.UseVisualStyleBackColor = true;
            this.loadNextBaseImageBtn.Click += new System.EventHandler(this.button5_Click);
            // 
            // loadPrevPOImageBtn
            // 
            this.loadPrevPOImageBtn.Location = new System.Drawing.Point(624, 403);
            this.loadPrevPOImageBtn.Name = "loadPrevPOImageBtn";
            this.loadPrevPOImageBtn.Size = new System.Drawing.Size(111, 52);
            this.loadPrevPOImageBtn.TabIndex = 48;
            this.loadPrevPOImageBtn.Text = "Load Previous Image";
            this.loadPrevPOImageBtn.UseVisualStyleBackColor = true;
            this.loadPrevPOImageBtn.Click += new System.EventHandler(this.button6_Click);
            // 
            // loadNextPOImageBtn
            // 
            this.loadNextPOImageBtn.Location = new System.Drawing.Point(741, 403);
            this.loadNextPOImageBtn.Name = "loadNextPOImageBtn";
            this.loadNextPOImageBtn.Size = new System.Drawing.Size(111, 52);
            this.loadNextPOImageBtn.TabIndex = 49;
            this.loadNextPOImageBtn.Text = "Load Next Image";
            this.loadNextPOImageBtn.UseVisualStyleBackColor = true;
            this.loadNextPOImageBtn.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(432, 45);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(97, 60);
            this.button8.TabIndex = 52;
            this.button8.Text = "Compare and Extract all images in Directory";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(93, 49);
            this.textBox15.Multiline = true;
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(333, 60);
            this.textBox15.TabIndex = 53;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 49);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(77, 13);
            this.label18.TabIndex = 54;
            this.label18.Text = "Directory Path:";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(13, 65);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 40);
            this.button9.TabIndex = 55;
            this.button9.Text = "Load Directory";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // baseImageIB
            // 
            this.baseImageIB.BackColor = System.Drawing.Color.Black;
            this.baseImageIB.Location = new System.Drawing.Point(12, 135);
            this.baseImageIB.Name = "baseImageIB";
            this.baseImageIB.Size = new System.Drawing.Size(305, 217);
            this.baseImageIB.TabIndex = 2;
            this.baseImageIB.TabStop = false;
            // 
            // estimatedOverlapIB
            // 
            this.estimatedOverlapIB.BackColor = System.Drawing.Color.Black;
            this.estimatedOverlapIB.Location = new System.Drawing.Point(323, 136);
            this.estimatedOverlapIB.Name = "estimatedOverlapIB";
            this.estimatedOverlapIB.Size = new System.Drawing.Size(295, 216);
            this.estimatedOverlapIB.TabIndex = 2;
            this.estimatedOverlapIB.TabStop = false;
            // 
            // imageBox3
            // 
            this.imageBox3.BackColor = System.Drawing.Color.Black;
            this.imageBox3.Location = new System.Drawing.Point(624, 135);
            this.imageBox3.Name = "imageBox3";
            this.imageBox3.Size = new System.Drawing.Size(312, 217);
            this.imageBox3.TabIndex = 2;
            this.imageBox3.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(336, 557);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(98, 17);
            this.checkBox1.TabIndex = 56;
            this.checkBox1.Text = "Render Images";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(468, 554);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(130, 20);
            this.label19.TabIndex = 57;
            this.label19.Text = "Desired Overlap: ";
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(604, 554);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(31, 20);
            this.textBox16.TabIndex = 58;
            this.textBox16.Text = "80";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detectorToolStripMenuItem1,
            this.detectorToolStripMenuItem,
            this.matcherToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(948, 24);
            this.menuStrip1.TabIndex = 59;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // detectorToolStripMenuItem1
            // 
            this.detectorToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bRISKToolStripMenuItem1,
            this.fASTToolStripMenuItem});
            this.detectorToolStripMenuItem1.Name = "detectorToolStripMenuItem1";
            this.detectorToolStripMenuItem1.Size = new System.Drawing.Size(64, 20);
            this.detectorToolStripMenuItem1.Text = "Detector";
            // 
            // bRISKToolStripMenuItem1
            // 
            this.bRISKToolStripMenuItem1.Name = "bRISKToolStripMenuItem1";
            this.bRISKToolStripMenuItem1.Size = new System.Drawing.Size(104, 22);
            this.bRISKToolStripMenuItem1.Text = "BRISK";
            this.bRISKToolStripMenuItem1.Click += new System.EventHandler(this.bRISKToolStripMenuItem1_Click);
            // 
            // fASTToolStripMenuItem
            // 
            this.fASTToolStripMenuItem.Name = "fASTToolStripMenuItem";
            this.fASTToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.fASTToolStripMenuItem.Text = "FAST";
            this.fASTToolStripMenuItem.Click += new System.EventHandler(this.fASTToolStripMenuItem_Click);
            // 
            // detectorToolStripMenuItem
            // 
            this.detectorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bRIEFToolStripMenuItem,
            this.bRISKToolStripMenuItem,
            this.fREAKToolStripMenuItem});
            this.detectorToolStripMenuItem.Name = "detectorToolStripMenuItem";
            this.detectorToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.detectorToolStripMenuItem.Text = "Extractor";
            // 
            // bRIEFToolStripMenuItem
            // 
            this.bRIEFToolStripMenuItem.Name = "bRIEFToolStripMenuItem";
            this.bRIEFToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.bRIEFToolStripMenuItem.Text = "BRIEF";
            this.bRIEFToolStripMenuItem.Click += new System.EventHandler(this.bRIEFToolStripMenuItem_Click);
            // 
            // bRISKToolStripMenuItem
            // 
            this.bRISKToolStripMenuItem.Name = "bRISKToolStripMenuItem";
            this.bRISKToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.bRISKToolStripMenuItem.Text = "BRISK";
            this.bRISKToolStripMenuItem.Click += new System.EventHandler(this.bRISKToolStripMenuItem_Click);
            // 
            // fREAKToolStripMenuItem
            // 
            this.fREAKToolStripMenuItem.Name = "fREAKToolStripMenuItem";
            this.fREAKToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.fREAKToolStripMenuItem.Text = "FREAK";
            this.fREAKToolStripMenuItem.Click += new System.EventHandler(this.fREAKToolStripMenuItem_Click);
            // 
            // matcherToolStripMenuItem
            // 
            this.matcherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hammingToolStripMenuItem,
            this.hamming2ToolStripMenuItem,
            this.infToolStripMenuItem,
            this.l1ToolStripMenuItem,
            this.l2ToolStripMenuItem,
            this.l2SqrToolStripMenuItem});
            this.matcherToolStripMenuItem.Name = "matcherToolStripMenuItem";
            this.matcherToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.matcherToolStripMenuItem.Text = "Matcher";
            // 
            // hammingToolStripMenuItem
            // 
            this.hammingToolStripMenuItem.Name = "hammingToolStripMenuItem";
            this.hammingToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.hammingToolStripMenuItem.Text = "Hamming";
            this.hammingToolStripMenuItem.Click += new System.EventHandler(this.hammingToolStripMenuItem_Click);
            // 
            // hamming2ToolStripMenuItem
            // 
            this.hamming2ToolStripMenuItem.Name = "hamming2ToolStripMenuItem";
            this.hamming2ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.hamming2ToolStripMenuItem.Text = "Hamming2";
            this.hamming2ToolStripMenuItem.Click += new System.EventHandler(this.hamming2ToolStripMenuItem_Click);
            // 
            // infToolStripMenuItem
            // 
            this.infToolStripMenuItem.Name = "infToolStripMenuItem";
            this.infToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.infToolStripMenuItem.Text = "Inf";
            this.infToolStripMenuItem.Click += new System.EventHandler(this.infToolStripMenuItem_Click);
            // 
            // l1ToolStripMenuItem
            // 
            this.l1ToolStripMenuItem.Name = "l1ToolStripMenuItem";
            this.l1ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.l1ToolStripMenuItem.Text = "L1";
            this.l1ToolStripMenuItem.Click += new System.EventHandler(this.l1ToolStripMenuItem_Click);
            // 
            // l2ToolStripMenuItem
            // 
            this.l2ToolStripMenuItem.Name = "l2ToolStripMenuItem";
            this.l2ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.l2ToolStripMenuItem.Text = "L2";
            this.l2ToolStripMenuItem.Click += new System.EventHandler(this.l2ToolStripMenuItem_Click);
            // 
            // l2SqrToolStripMenuItem
            // 
            this.l2SqrToolStripMenuItem.Name = "l2SqrToolStripMenuItem";
            this.l2SqrToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.l2SqrToolStripMenuItem.Text = "L2Sqr";
            this.l2SqrToolStripMenuItem.Click += new System.EventHandler(this.l2SqrToolStripMenuItem_Click);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(560, 45);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(75, 13);
            this.label40.TabIndex = 60;
            this.label40.Text = "VideoFilePath:";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(563, 61);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 44);
            this.button10.TabIndex = 61;
            this.button10.Text = "Load Video";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(644, 45);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(168, 60);
            this.textBox8.TabIndex = 62;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(818, 43);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(115, 62);
            this.button11.TabIndex = 63;
            this.button11.Text = "Extract Frames from Video";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // detectorTypeTB
            // 
            this.detectorTypeTB.BackColor = System.Drawing.Color.Black;
            this.detectorTypeTB.ForeColor = System.Drawing.Color.White;
            this.detectorTypeTB.Location = new System.Drawing.Point(12, 554);
            this.detectorTypeTB.Multiline = true;
            this.detectorTypeTB.Name = "detectorTypeTB";
            this.detectorTypeTB.Size = new System.Drawing.Size(100, 31);
            this.detectorTypeTB.TabIndex = 64;
            this.detectorTypeTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // extractorTypeTB
            // 
            this.extractorTypeTB.BackColor = System.Drawing.Color.Black;
            this.extractorTypeTB.ForeColor = System.Drawing.Color.White;
            this.extractorTypeTB.Location = new System.Drawing.Point(118, 554);
            this.extractorTypeTB.Multiline = true;
            this.extractorTypeTB.Name = "extractorTypeTB";
            this.extractorTypeTB.Size = new System.Drawing.Size(100, 31);
            this.extractorTypeTB.TabIndex = 65;
            this.extractorTypeTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // matcherTypeTB
            // 
            this.matcherTypeTB.BackColor = System.Drawing.Color.Black;
            this.matcherTypeTB.ForeColor = System.Drawing.Color.White;
            this.matcherTypeTB.Location = new System.Drawing.Point(224, 554);
            this.matcherTypeTB.Multiline = true;
            this.matcherTypeTB.Name = "matcherTypeTB";
            this.matcherTypeTB.Size = new System.Drawing.Size(100, 31);
            this.matcherTypeTB.TabIndex = 66;
            this.matcherTypeTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(641, 554);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(292, 31);
            this.progressBar1.TabIndex = 67;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 592);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.matcherTypeTB);
            this.Controls.Add(this.extractorTypeTB);
            this.Controls.Add(this.detectorTypeTB);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.textBox16);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.imageBox3);
            this.Controls.Add(this.estimatedOverlapIB);
            this.Controls.Add(this.baseImageIB);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.textBox15);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.loadNextPOImageBtn);
            this.Controls.Add(this.loadPrevPOImageBtn);
            this.Controls.Add(this.loadNextBaseImageBtn);
            this.Controls.Add(this.loadPrevBaseImageBtn);
            this.Controls.Add(this.poImagePathTB);
            this.Controls.Add(this.baseImagePathTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.estimateOverlapBtn);
            this.Controls.Add(this.loadPOImageBtn);
            this.Controls.Add(this.loadBaseImageBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ConsoleOutputTB);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainUI";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainUI_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.baseImageIB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.estimatedOverlapIB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ConsoleOutputTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button loadBaseImageBtn;
        private System.Windows.Forms.Button loadPOImageBtn;
        private System.Windows.Forms.Button estimateOverlapBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox baseImagePathTB;
        private System.Windows.Forms.TextBox poImagePathTB;
        private System.Windows.Forms.Button loadPrevBaseImageBtn;
        private System.Windows.Forms.Button loadNextBaseImageBtn;
        private System.Windows.Forms.Button loadPrevPOImageBtn;
        private System.Windows.Forms.Button loadNextPOImageBtn;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button9;
        private Emgu.CV.UI.ImageBox baseImageIB;
        private Emgu.CV.UI.ImageBox estimatedOverlapIB;
        private Emgu.CV.UI.ImageBox imageBox3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem detectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bRIEFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fREAKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detectorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bRISKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bRISKToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fASTToolStripMenuItem;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ToolStripMenuItem matcherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hammingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hamming2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem l1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem l2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem l2SqrToolStripMenuItem;
        private System.Windows.Forms.TextBox detectorTypeTB;
        private System.Windows.Forms.TextBox extractorTypeTB;
        private System.Windows.Forms.TextBox matcherTypeTB;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

