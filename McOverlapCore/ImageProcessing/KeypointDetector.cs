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
        private EDetectorType type;
        private Feature2D detector;
        /// <summary>
        /// useful when initialising a collection of Keypoint Detectors
        /// </summary>
        public KeypointDetector()
        {
            type = EDetectorType.NULL;
            detector = null;
        }
        
        /// <summary>
        /// creates a detector of a specified type
        /// </summary>
        /// <param name="type"></param>
        public KeypointDetector(EDetectorType type)
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
        public VectorOfKeyPoint DetectKeypoints(Image img)
        {
            VectorOfKeyPoint ret = new VectorOfKeyPoint(); //the keypoints to return
            if (!img.Mat.IsEmpty)
            {
                detector.DetectRaw(img.Mat, ret);
            }
            return ret;
        }
        /// <summary>
        /// creates a keypoint detector based off the type that is passed
        /// </summary>
        /// <param name="type"></param>
        private Feature2D CreateDetector(EDetectorType type)
        {
            Feature2D ret = null; //the detector to return
            switch (type)
            {
                case EDetectorType.BRISK:
                    ret = new Brisk();
                    break;
                case EDetectorType.FAST:
                    ret = new FastDetector();
                    break;
                case EDetectorType.NULL:
                    ret = null;
                    break;
            }

            return ret;
        }
        /// <summary>
        /// get - return type of detector that has been initialized
        /// set - used to change the type of detector that is used
        /// </summary>
        public EDetectorType Type
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
