using Emgu.CV;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McOverlapCore
{
    public class Image
    {
        private Mat mat;
        private VectorOfKeyPoint keypoints;
        private Mat descriptors;
        /// <summary>
        /// useful when intialising a collection of images
        /// </summary>
        public Image()
        {
            mat = new Mat();
            keypoints = new VectorOfKeyPoint();
            descriptors = new Mat();
        }
        /// <summary>
        /// useful when creating an image from a frame ripped from a video
        /// </summary>
        /// <param name="mat"></param>
        public Image(Mat mat)
        {
            this.mat = mat;
            keypoints = new VectorOfKeyPoint();
            descriptors = new Mat();
        }

        public Image(String filePath)
        {
            LoadFromFile(filePath);
        }
        /// <summary>
        /// used to load image from file after it has been created
        /// </summary>
        /// <param name="filepath"></param>
        public void LoadFromFile(String filePath)
        {
            mat = CvInvoke.Imread(filePath, Emgu.CV.CvEnum.LoadImageType.AnyColor);
            keypoints = new VectorOfKeyPoint();
            descriptors = new Mat();
        }

        public Mat Mat
        {
            get { return mat; }
            set { mat = value; }
        }

        public VectorOfKeyPoint Keypoints
        {
            get { return keypoints; }
            set { keypoints = value; }
        }

        public Mat Descriptors
        {
            get { return descriptors; }
            set { descriptors = value; }
        }
    }
}
