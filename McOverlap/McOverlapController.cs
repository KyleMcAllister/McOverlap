using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McOverlap
{
    class McOverlapController
    {
        /// <summary>
        /// Load an image from file
        /// </summary>
        public void LoadImage()
        {

        }
        /// <summary>
        /// Load a directory
        /// </summary>
        public void LoadDirectory()
        {

        }
        /// <summary>
        /// Load a video
        /// </summary>
        public void LoadVideo()
        {

        }
        /// <summary>
        /// Compare to images and return the amount of overlap i.e. the percentage of baseImg contained in poImg
        /// </summary>
        /// <param name="baseImg"></param>
        /// <param name="poImg"></param>
        /// <returns></returns>
        public double compare(McOverlapCore.Image baseImg, McOverlapCore.Image poImg)
        {
            double ret = 0;

            return ret;
        }
        /// <summary>
        /// - compares a sequence of images
        /// - creates an output directory to extract the minimum amount of images that form a sequence with overlap lowerbound
        /// - creates a second directory in the output directory where all image pairs that could not be matched are placed [for debugging later if so desired]
        /// - generates a report:
        ///     -> total images processes
        ///     -> total images extracted
        ///     -> total time taken
        ///     -> output directory path
        ///     -> a log of each overlap sequence formed [start img, end img, lowest overlap obtained in sequence]
        ///     -> a log of each comparison made: base | po -> x% overlap
        /// </summary>
        public void compareImageDir(double lowerBound)
        {

        }
        /// <summary>
        /// - compares a sequence of frames from a video
        /// - creates an output directory to extract the minimum amount of frames that form a sequence with overlap lowerbound
        /// - creates a second directory in the output directory where all frame pairs that could not be matched are placed [for debugging later if so desired]
        /// - generates a report:
        ///     -> total frames processed
        ///     -> total frames extracted
        ///     -> total time taken
        ///     -> output directory path
        ///     -> a log of each overlap sequence formed [start frame, end frame, lowest overlap obtained in sequence]
        ///     -> a log of each comparison made: base | po -> x% overlap
        /// </summary>
        /// <param name="lowerBound"></param>
        public void compareImageVid(double lowerBound)
        {

        }
    }
}
