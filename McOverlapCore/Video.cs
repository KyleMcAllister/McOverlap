using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McOverlapCore
{
    public class Video
    {
        
        private Capture video;
        private Mat frame; //the currentFrame
        private int frameNumber; //number of currentFrame
        /// <summary>
        /// useful when initializing  a collection of Videos
        /// </summary>
        public Video()
        {
            frameNumber = 0;
            frame = null;
            video = null;

        }

        public Video(String filePath)
        {
            LoadFromFile(filePath);
        }
        /// <summary>
        /// used to load video from file after it has been created
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadFromFile(String filePath)
        {
            frameNumber = 0;
            video = new Capture(filePath);
            frame = null;
        }
        /// <summary>
        /// returns the next frame in the video or an emtpy mat if there is no next frame 
        /// </summary>
        /// <returns></returns>
        public Mat NextFrame()
        {
            Mat ret = new Mat();
            if (video.Grab())
            {
                video.Retrieve(ret);
                frameNumber++;
            }
            frame = ret;

            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        public Mat Frame
        {
            get { return frame; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int FrameNumber
        {
            get { return frameNumber; }
        }
    }
}
