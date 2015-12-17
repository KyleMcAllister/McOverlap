using Emgu.CV.Features2D;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McOverlapCore.ImageProcessing
{
    public class DescriptorMatcher
    {
        private MatcherType type;
        private Emgu.CV.Features2D.DescriptorMatcher matcher;
        /// <summary>
        /// usefule when iniatializing a collection of descriptor matchers
        /// </summary>
        public DescriptorMatcher()
        {
            type = MatcherType.NULL;
            matcher = null;
        }

        public DescriptorMatcher(MatcherType type)
        {
            this.type = type;
            matcher = CreateMatcher(type);
        }
        /// <summary>
        /// a more functional wrapping of the descriptor matcher methon in EmguCV
        /// </summary>
        /// <param name="k"></param>
        /// <param name="scene"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public VectorOfVectorOfDMatch MatchDescriptors(int k, Image scene, Image model)
        {
            VectorOfVectorOfDMatch ret = new VectorOfVectorOfDMatch(); //the matches to return
            if(!model.Descriptors.IsEmpty && !scene.Descriptors.IsEmpty)
            {
                matcher.Add(model.Descriptors);
                matcher.KnnMatch(scene.Descriptors, ret, k, null);
            }
            return ret;
        }
        /// <summary>
        /// creates a descriptor matcher based off the type that is passed
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Emgu.CV.Features2D.DescriptorMatcher CreateMatcher(MatcherType type)
        {
            Emgu.CV.Features2D.DescriptorMatcher ret = null;//the descriptor matcher to return

            switch (type)
            {
                case MatcherType.HAMMING:
                    ret = new BFMatcher(DistanceType.Hamming);
                    break;
                case MatcherType.HAMMING2:
                    ret = new BFMatcher(DistanceType.Hamming2);
                    break;
                case MatcherType.INF:
                    ret = new BFMatcher(DistanceType.Inf);
                    break;
                case MatcherType.L1:
                    ret = new BFMatcher(DistanceType.L1);
                    break;
                case MatcherType.L2:
                    ret = new BFMatcher(DistanceType.L2);
                    break;
                case MatcherType.L2SQR:
                    ret = new BFMatcher(DistanceType.L2Sqr);
                    break;
                case MatcherType.NULL:
                    ret = null;
                    break;
            }

            return ret;
        }
        /// <summary>
        /// get - returns the type of matcher that has been initialize
        /// set - used to change the type of matcher that is being used
        /// </summary>
        public MatcherType Type
        {
            get { return type; }
            set
            {
                this.type = value;
                matcher = CreateMatcher(value);
            }
        }
    }
}
