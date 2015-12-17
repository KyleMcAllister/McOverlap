using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McOverlapCore.ImageProcessing
{
    public class DescriptorExtractor
    {
        private ExtractorType type;
        private Feature2D extractor;
        /// <summary>
        /// useful when initialising a collection of descriptor extractors
        /// </summary>
        public DescriptorExtractor()
        {
            type = ExtractorType.NULL;
            extractor = null;
        }

        public DescriptorExtractor(ExtractorType type)
        {
            this.type = type;
            extractor = CreateExtractor(type);
            
        }
        /// <summary>
        /// a more functional wrapping of descriptor extractor method in EmguCV
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="keypoints"></param>
        /// <returns></returns>
        public bool ExtractDescriptors(Image img)
        {
            if(!img.Mat.IsEmpty && img.Keypoints.Size != 0)
            {
                extractor.Compute(img.Mat, img.Keypoints, img.Descriptors);
                return true;
            }
            return false;
        }
        /// <summary>
        /// creates a descriptor extractor based off the type that is passed
        /// </summary>
        /// <param name="type"></param>
        private Feature2D CreateExtractor(ExtractorType type)
        {
            Feature2D ret = null; //the extractor to return
            switch (type)
            {
                case ExtractorType.BRIEF:
                    ret = new BriefDescriptorExtractor();
                    break;
                case ExtractorType.BRISK:
                    ret = new Brisk();
                    break;
                case ExtractorType.FREAK:
                    ret = new Freak();
                    break;
                case ExtractorType.NULL:
                    ret = null;
                    break;
            }

            return ret;
        }
        /// <summary>
        /// get - returns the type of extractor that has been initialized
        /// set - used to change the type of extractor that is used
        /// </summary>
        public ExtractorType Type
        {
            get { return type; }
            set
            {
                this.type = value;
                extractor = CreateExtractor(value);
            }
        }
    }
}
