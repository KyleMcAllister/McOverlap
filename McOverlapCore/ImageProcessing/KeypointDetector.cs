using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McOverlapCore.ImageProcessing
{
    public class KeypointDetector
    {
        private DetectorType type;
        private Feature2D detector;
        /// <summary>
        /// useful when initialising a collection of Keypoint Detectors
        /// </summary>
        public KeypointDetector()
        {
            type = DetectorType.NULL;
            detector = null;
        }
        
        /// <summary>
        /// creates a detector of a specified type
        /// </summary>
        /// <param name="type"></param>
        public KeypointDetector(DetectorType type)
        {
            this.type = type;
            detector = CreateDetector(type);
        }
        /// <summary>
        /// a more functional wrapping of detector method in EmguCV
        /// - returns that vector of keypoints, rather than silently populating one
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public bool DetectKeypoints(Image img)
        {
            if (!img.Mat.IsEmpty)
            {
                detector.DetectRaw(img.Mat, img.Keypoints);
                return true;
            }
            return false;
        }
        /// <summary>
        /// creates a keypoint detector based off the type that is passed
        /// </summary>
        /// <param name="type"></param>
        private Feature2D CreateDetector(DetectorType type)
        {
            Feature2D ret = null; //the detector to return
            switch (type)
            {
                case DetectorType.BRISK:
                    ret = new Brisk();
                    break;
                case DetectorType.FAST:
                    ret = new FastDetector();
                    break;
                case DetectorType.NULL:
                    ret = null;
                    break;
            }

            return ret;
        }
        /// <summary>
        /// get - return type of detector that has been initialized
        /// set - used to change the type of detector that is used
        /// </summary>
        public DetectorType Type
        {
            get { return type; }
            set
            {
                this.type = value;
                detector = CreateDetector(value);
            }
        }

        
    }
}
