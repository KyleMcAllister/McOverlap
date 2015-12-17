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
        private int frameNumber; //number of currentFrame
        /// <summary>
        /// useful when initializing  a collection of Videos
        /// </summary>
        public Video()
        {
            frameNumber = 0;
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
        }
        /// <summary>
        /// returns the next frame in the video or an emtpy mat if there is no next frame 
        /// </summary>
        /// <returns></returns>
        public bool NextFrame(Mat mat)
        {
            
            if (video.Grab())
            {
                video.Retrieve(mat);
                frameNumber++;
                return true;
            }
            else
            {
                return false;
            }
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
