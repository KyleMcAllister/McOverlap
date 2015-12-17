using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using System;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System.Drawing;
using System.Windows.Forms;
using McOverlapCore.ImageProcessing;

namespace McOverlap
{
    class OverlapEstimator : IDisposable
    {

        private MainUI form;

        private Mat displayedOverlap;

        private VectorOfVectorOfDMatch kpMatches;

        private Mat maskMatches;

        private Mat result;

        private Mat homography;

        private KeypointDetector detector;
        private DescriptorExtractor extractor;
        private McOverlapCore.ImageProcessing.DescriptorMatcher matcher;
        
        public OverlapEstimator(MainUI form, DetectorType detectorType, ExtractorType extractorType, MatcherType matcherType)
        {
            this.form = form;

            double resizeScale = form.resizeScale();

            displayedOverlap = new Mat();

            this.kpMatches = new VectorOfVectorOfDMatch();

            this.maskMatches = new Mat();

            this.result = new Mat();

            this.homography = null;

            detector = new KeypointDetector(detectorType);
            extractor = new DescriptorExtractor(extractorType);
            matcher = new McOverlapCore.ImageProcessing.DescriptorMatcher(matcherType);

        }

        public double execute(McOverlapCore.Image baseImg, McOverlapCore.Image poImg)
        {

            Mat reducedBase = new Mat();
            Mat reducedPO = new Mat();

            try
            {
                CvInvoke.Resize(baseImg.Mat, reducedBase, new Size(400, 300));
                CvInvoke.Resize(poImg.Mat, reducedPO, new Size(400, 300));
            }
            catch(Exception ex)
            {
                MessageBox.Show("failed to reduce images in overlap estiamtor");
                MessageBox.Show(ex.ToString());
            }
            

            McOverlapCore.Image reducedBaseImg = new McOverlapCore.Image(reducedBase.Split()[0]);
            McOverlapCore.Image reducedPOImg = new McOverlapCore.Image(reducedPO.Split()[0]);


            detector.DetectKeypoints(reducedBaseImg);
            detector.DetectKeypoints(reducedPOImg);
            extractor.ExtractDescriptors(reducedBaseImg);
            extractor.ExtractDescriptors(reducedPOImg);

            kpMatches = matcher.MatchDescriptors(2, reducedBaseImg, reducedPOImg);

            int i = 0;

            if(kpMatches.Size == 0)
            {
                System.Console.WriteLine("..No Matches");
                return 0;
            }
            else
            {

                i = createHomography(reducedBaseImg, reducedPOImg, kpMatches);
                
            }

            if (i == -1)
            {
                return 0; ;
            }

            return drawEstimatedOverlap(reducedBaseImg, reducedPOImg);
        }

        public int createHomography(McOverlapCore.Image baseImg, McOverlapCore.Image poImg,VectorOfVectorOfDMatch matches)
        {
            try
            { 
                maskMatches = new Mat(matches.Size, 1, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                maskMatches.SetTo(new MCvScalar(255));

                double uniqunessThreshold = form.uniqunessThreshold();

                try
                {
                    Features2DToolbox.VoteForUniqueness(matches, uniqunessThreshold, maskMatches);

                }
                catch(IndexOutOfRangeException ex)
                {
                    Console.WriteLine("..problem..");
                    return -1;
                }


                int nonZeroCount = CvInvoke.CountNonZero(maskMatches);
                if(nonZeroCount >= 4)
                {
                    double scaleIncrement = form.scaleIncrement();
                    int rotationBins = form.numRotationBins();

                    nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(poImg.Keypoints, baseImg.Keypoints, matches, maskMatches, scaleIncrement, rotationBins);

                    if(nonZeroCount >= 4)
                    {
                        double ransacReprojectionThreshold = form.ransacReprojThreshold();

                        homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(poImg.Keypoints, baseImg.Keypoints, matches, maskMatches, ransacReprojectionThreshold);
                    }
                    
                }
                
                

                return 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return -1;
            }

            
            
        }

        public double drawEstimatedOverlap(McOverlapCore.Image baseImg, McOverlapCore.Image poImg)
        {

            double overlap = 0;

            if (homography != null)
            {
                //draw a rectangle along the projected model
                Rectangle rect = new Rectangle(Point.Empty, poImg.Mat.Size);
                PointF[] pts = new PointF[]
                {
                  new PointF(rect.Left, rect.Bottom),
                  new PointF(rect.Right, rect.Bottom),
                  new PointF(rect.Right, rect.Top),
                  new PointF(rect.Left, rect.Top)
                };
                pts = CvInvoke.PerspectiveTransform(pts, homography);



                Point[] points = Array.ConvertAll<PointF, Point>(pts, Point.Round);

                using (VectorOfPoint vp = new VectorOfPoint(points))
                {

                    baseImg.Mat.CopyTo(displayedOverlap);
                    CvInvoke.Polylines(displayedOverlap, vp, true, new MCvScalar(255, 255, 0, 255), 0);

                }

                ///////////////////////////////////////////////////////
                Rectangle rect_border = new Rectangle(Point.Empty, baseImg.Mat.Size);
                PointF[] borderPts = new PointF[]
                {
                  new PointF(rect.Left, rect.Bottom),
                  new PointF(rect.Right, rect.Bottom),
                  new PointF(rect.Right, rect.Top),
                  new PointF(rect.Left, rect.Top)
                };

                Point[] borderPoints = Array.ConvertAll<PointF, Point>(borderPts, Point.Round);

                IntersectionRegion ir = new IntersectionRegion(borderPoints, points);
                overlap = ir.ComputeIntersectionRegionArea();
                overlap = Math.Round(overlap);

                if (form.drawImages())
                {
                    ImageBox ib = form.getEstimatedOverlapImageBox();
                    CvInvoke.Resize(displayedOverlap, displayedOverlap, new Size(ib.Width, ib.Height));
                    ib.Image = displayedOverlap;
                }


                return overlap;

            }
            else
            {
                return 0;
            }
        }

        public void Dispose()
        {
            kpMatches.Dispose();
            maskMatches.Dispose();
            if(homography != null)
            {
                homography.Dispose();
            }
            result.Dispose();
        }        
    }

        
   }

