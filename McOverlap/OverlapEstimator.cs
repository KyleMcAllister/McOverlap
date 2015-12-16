﻿using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using Emgu.CV.XFeatures2D;
using System;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System.Drawing;

namespace McOverlap
{
    class OverlapEstimator : IDisposable
    {

        private MainUI form;

        private String detectorType;

        private Mat base_mat_gray;
        private Mat po_mat_gray;

        private VectorOfKeyPoint base_keypoints;
        private VectorOfKeyPoint po_keypoints;

        private Mat base_descriptors;
        private Mat po_descriptors;

        private VectorOfVectorOfDMatch kpMatches;

        private Mat maskMatches;

        private Mat result;

        private Mat homography;

        private Feature2D detector;
        private Feature2D extractor;
        private DescriptorMatcher matcher;
        
        public OverlapEstimator(MainUI form, Mat base_mat, Mat po_mat, String detectorType, String extractorType, String matcherType)
        {
            this.form = form;

            this.detectorType = detectorType;

            double resizeScale = form.resizeScale();

            try
            {
                base_mat_gray = new Mat();
                CvInvoke.Resize(base_mat, base_mat_gray, new Size(400, 300));
                po_mat_gray = new Mat();
                CvInvoke.Resize(po_mat, po_mat_gray, new Size(400, 300));
               
            }
            catch
            {

            }
            

            this.base_keypoints = new VectorOfKeyPoint();
            this.po_keypoints = new VectorOfKeyPoint();

            this.base_descriptors = new Mat();
            this.po_descriptors = new Mat();

            this.kpMatches = new VectorOfVectorOfDMatch();

            this.maskMatches = new Mat();

            this.result = new Mat();

            this.homography = null;

            switch (detectorType)
            {
                case "BRISK":
                    detector = new Brisk(form.detectorBriskThreshold(), form.detectorBriskOctaveLayers(), form.detectorBriskPatternScale());
                    break;

                case "FAST":
                    detector = new FastDetector(form.fastThreshold(), form.fastNonMaxSuppression(), form.fastDetectorType());
                    break;

                case "SURF":
                    detector = new SURF(form.detectorSurfHessianThresh(), form.detectorSurfNumOctaves(), form.detectorSurfNumOctaveLevels(), form.detectorSurfExtended(), form.detectorSurfUpright());
                    break;
            }

            switch (extractorType)
            {
                case "BRISK":
                    extractor = new Brisk(form.extractorBriskThreshold(), form.extractorBriskOctaveLayers(), form.extractorBriskPatternScale());
                    break;

                case "BRIEF":
                    extractor = new BriefDescriptorExtractor(form.briefDescriptorSize());
                    break;

                case "FREAK":
                    extractor = new Freak(form.isFreakOrientationNormalized(), form.isFreakScaleNormalized(), form.freakPatternScale(), form.freakNumOctaves());
                    break;

                case "SURF":
                    extractor = new SURF(form.extractorSurfHessianThreshold(), form.extractorSurfOctaves(), form.extractorSurfOctaveLayers(), form.extractorSurfExtended(), form.extractorSurfUpright());
                    break;
            }

            switch (matcherType)
            {
                case "BRUTE FORCE":
                    matcher = new BFMatcher(form.bruteforceMatcherType(), form.bruteForceCrosscheck());
                    break;
            }

        }

        public double execute()
        {
            
           extractFeatures(base_mat_gray, base_keypoints, base_descriptors); //extract features from base image
           extractFeatures(po_mat_gray, po_keypoints, po_descriptors); //extract features from potentially overlapping image        

            /*ImageBox ib = form.getBaseImageBox();
            ib.Image = drawFeatures(base_mat_gray.ToMat(Emgu.CV.CvEnum.AccessType.ReadWrite), base_keypoints);
            ib = form.getPotentiallyOverlappingImageBox();
            ib.Image = drawFeatures(po_mat_gray.ToMat(Emgu.CV.CvEnum.AccessType.ReadWrite), po_keypoints);*/


            int toConinue = matchFeatures(base_mat_gray, base_descriptors, po_mat_gray, po_descriptors, kpMatches);

            if (toConinue == -1)
            {
                System.Console.WriteLine("..problem..");
                return 0;
            }


            return drawEstimatedOverlap();
        }

        public int extractFeatures(Mat mat, VectorOfKeyPoint vkp, Mat md)
        {

            detector.DetectRaw(mat, vkp, null);
            extractor.Compute(mat, vkp, md);

            return 0;
        }

        public int matchFeatures(Mat mat1, Mat md1, Mat mat2, Mat md2, VectorOfVectorOfDMatch matches)
        {

            matcher.Add(md2);

            int nearestNeighbours = form.numNearestNeighbours();
            

            try
            {
                matcher.KnnMatch(md1, matches, nearestNeighbours, null);
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                maskMatches = new Mat(matches.Size, 1, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                maskMatches.SetTo(new MCvScalar(255));

                double uniqunessThreshold = form.uniqunessThreshold();

                
                Features2DToolbox.VoteForUniqueness(matches, uniqunessThreshold, maskMatches);

                
                int nonZeroCount = CvInvoke.CountNonZero(maskMatches);
                if(nonZeroCount >= 4)
                {
                    double scaleIncrement = form.scaleIncrement();
                    int rotationBins = form.numRotationBins();

                    nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(po_keypoints, base_keypoints, matches, maskMatches, scaleIncrement, rotationBins);

                    if(nonZeroCount >= 4)
                    {
                        double ransacReprojectionThreshold = form.ransacReprojThreshold();

                        homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(po_keypoints, base_keypoints, matches, maskMatches, ransacReprojectionThreshold);
                    }
                    
                }
                
                

                return 0;
            }
            catch
            {
                return -1;
            }

            
            
        }

        public Mat drawFeatures(Mat mat, VectorOfKeyPoint vkp)
        {
            Features2DToolbox.DrawKeypoints(mat,vkp,mat, new Bgr(System.Drawing.Color.Green),Features2DToolbox.KeypointDrawType.Default);

            return mat;
        }

        public double drawEstimatedOverlap()
        {

            double overlap = 0;
            //Features2DToolbox.DrawMatches(po_mat_gray, po_keypoints, base_mat_gray, base_keypoints, kpMatches, result, new MCvScalar(255, 255, 255), new MCvScalar(255, 255, 255), maskMatches);



            if (homography != null)
            {
                //draw a rectangle along the projected model
                Rectangle rect = new Rectangle(Point.Empty, po_mat_gray.Size);
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

                    CvInvoke.Polylines(base_mat_gray, vp, true, new MCvScalar(255, 255, 0, 255), 0);

                }

                ///////////////////////////////////////////////////////
                Rectangle rect_border = new Rectangle(Point.Empty, base_mat_gray.Size);
                PointF[] borderPts = new PointF[]
                {
                  new PointF(rect.Left, rect.Bottom),
                  new PointF(rect.Right, rect.Bottom),
                  new PointF(rect.Right, rect.Top),
                  new PointF(rect.Left, rect.Top)
                };

                Point[] borderPoints = Array.ConvertAll<PointF, Point>(borderPts, Point.Round);

                IntersectionRegion2Point0 ir = new IntersectionRegion2Point0(borderPoints, points);
                overlap = ir.ComputeIntersectionRegionArea();
                overlap = Math.Round(overlap);

                if (form.drawImages())
                {
                    ImageBox ib = form.getEstimatedOverlapImageBox();
                    ib.Image = base_mat_gray.ToImage<Gray, byte>().Resize(ib.Width, ib.Height, Emgu.CV.CvEnum.Inter.Linear);
                }

                return overlap;

            }
            else
            {
                return 0;
            }
        }

        public VectorOfKeyPoint getBase_keypoints()
        {
            return base_keypoints;
        }

        public VectorOfKeyPoint getPO_keypoints()
        {
            return po_keypoints;
        }

        public void Dispose()
        {
            base_mat_gray.Dispose();
            base_keypoints.Dispose();
            base_descriptors.Dispose();
            po_mat_gray.Dispose();
            po_keypoints.Dispose();
            po_descriptors.Dispose();
            kpMatches.Dispose();
            maskMatches.Dispose();
            if(homography != null)
            {
                homography.Dispose();
            }
            detector.Dispose();
            extractor.Dispose();
            matcher.Dispose();
            result.Dispose();
        }        
    }

        
   }

