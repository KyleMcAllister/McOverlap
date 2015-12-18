using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace McOverlapCore.ImageProcessing
{
    //computes the intersection between two shapes [red, blue] defined by 2 sets of points [redpts, bluepts]
    /// <summary>
    /// points are assumed to be ordered counterclockwise: 
    /// i.e. there exists a line between point[0] and point[1], ..., point[n] and point[0]
    /// </summary>
    public class IntersectionRegion
    {
        private Point[] redPts;
        private Point[] bluePts;

        private double redArea;

        private List<LineSegment2D> redLines = new List<LineSegment2D>();
        private List<LineSegment2D> blueLines = new List<LineSegment2D>();

        public IntersectionRegion(Point[] red, Point[] blue)
        {
            redPts = red;
            bluePts = blue;

            //populate lines arrays:
            for(int i = 0; i < redPts.Length; i++)
            {
                redLines.Add(new LineSegment2D(redPts[i % redPts.Length], redPts[(i + 1) % redPts.Length]));
            }

            
            for (int i = 0; i < bluePts.Length; i++)
            {
                blueLines.Add(new LineSegment2D(bluePts[i % bluePts.Length], bluePts[(i + 1) % bluePts.Length]));
            }

            redArea = ComputeAreaOfIntersectionRegion(redPts);
        }

        public double ComputeIntersectionRegionArea()
        {
            
            List<LineSegment2D> ret = new List<LineSegment2D>();

            String output = "\r\n" + "Blue points before clip:" + "\r\n";

            foreach(LineSegment2D line in blueLines)
            {
                output += line.P1;
                output += line.P2;
            }

           // MessageBox.Show(output);

            ClipBlueToRed();

            output = "\r\n" + "Blue points after clip:" + "\r\n";

            foreach (LineSegment2D line in blueLines)
            {
                output += line.P1;
                output += line.P2;
            }

           // MessageBox.Show(output);

            output = "\r\n" + "Red points before clip:" + "\r\n";

            foreach (LineSegment2D line in redLines)
            {
                output += line.P1;
                output += line.P2;
            }

           // MessageBox.Show(output);

            ClipRedToBlue();

            output = "\r\n" + "Red points after clip:" + "\r\n";

            foreach (LineSegment2D line in redLines)
            {
                output += line.P1;
                output += line.P2;
            }

           // MessageBox.Show(output);

            foreach (LineSegment2D line in blueLines)
            {
                ret.Add(line);
            }

            foreach(LineSegment2D line in redLines)
            {
                ret.Add(line);
            }

           
            //remove duplicates:
            int i = 0;
            int j = i + 1;
            while(i < ret.Count)
            {
                LineSegment2D p = ret[i];
                while (j < ret.Count)
                {
                    LineSegment2D p2 = ret[j];
                    if (p.Equals(p2))
                    {
                        ret.RemoveAt(j);
                    }

                    j++;
                }
                i++;
                j = i + 1;
            }

            output = "\r\n";
            foreach(LineSegment2D line in ret)
            {
                output += "(";
                output += line.P1;
                output += line.P2;
                output += ")";
                output += "\r\n";
            }
            //MessageBox.Show(output);

            List<LineSegment2D> orderedlines = new List<LineSegment2D>();
            orderedlines.Add(ret.First());
            ret.Remove(ret.First());
            int loopMax = ret.Count;
            for(i = 0; i < loopMax; i++)
            {
                LineSegment2D oLine = orderedlines.Last();
                foreach(LineSegment2D line in ret)
                {
                    if(Math.Abs(line.P1.X - oLine.P2.X) <= 4 && Math.Abs(line.P1.Y - oLine.P2.Y) <= 4)
                    {
                        orderedlines.Add(line);
                        ret.Remove(line);
                        break;
                    }
                }
            }

            output = "OrderedLines:";
            foreach(LineSegment2D line in orderedlines)
            {
                output += "\r\n";
                output += line.P1;
                output += line.P2;
            }

            //MessageBox.Show(output);

            List<Point> retPts = new List<Point>();

            foreach(LineSegment2D line in orderedlines)
            {
                retPts.Add(line.P1);
            }

            output = "Ret points: ";
            foreach(Point p in retPts)
            {
                output += "\r\n";
                output += p;
            }

            //MessageBox.Show(output);

            return Math.Abs(ComputeAreaOfIntersectionRegion(retPts.ToArray())/redArea)*100;
            
        }

        public void ClipRedToBlue()
        {
            //updating red points:
            foreach(LineSegment2D halfPlane in blueLines)
            {
                
                for (int i = 0; i < redLines.Count; i++)
                {
                    LineSegment2D line = redLines[i];
                    
                        switch (ClipToHalfPlaneAction(halfPlane, line))
                        {
                            case ImageProcessing.ClipToHalfPlaneAction.DISCARD_LINE:

                            redLines.Remove(line);
                            i--;
                                break;
                            case ImageProcessing.ClipToHalfPlaneAction.DO_NOTHING:
                                
                                //do nothing..
                                break;
                            case ImageProcessing.ClipToHalfPlaneAction.UPDATE_END:
                                
                                line.P2 = Intersection(halfPlane, line);
                            redLines[i] = line;
                                break;
                            case ImageProcessing.ClipToHalfPlaneAction.UPDATE_START:
                                
                                line.P1 = Intersection(halfPlane, line);
                            redLines[i] = line;
                                break;
                        }

                        
                    
                    
                }
            }

        }

        public void ClipBlueToRed()
        {
            //updating blue points
            foreach (LineSegment2D halfPlane in redLines)
            {
                for (int i = 0; i < blueLines.Count; i++)
                {
                    LineSegment2D line = blueLines[i];
                    //String result = "";
                    switch (ClipToHalfPlaneAction(halfPlane, line))
                        {
                            case ImageProcessing.ClipToHalfPlaneAction.DISCARD_LINE:
                                //result = "remove blue line";
                            blueLines.Remove(line);
                                i--;
                                break;
                            case ImageProcessing.ClipToHalfPlaneAction.DO_NOTHING:
                                //result = "do nothing";
                                //do nothing..
                                break;
                            case ImageProcessing.ClipToHalfPlaneAction.UPDATE_END:
                                //result = "update end of blue line";
                                line.P2 = Intersection(halfPlane, line);
                            blueLines[i] = line;
                                break;
                            case ImageProcessing.ClipToHalfPlaneAction.UPDATE_START:
                                //result = "update start of blue line";
                                line.P1 = Intersection(halfPlane, line);
                            blueLines[i] = line;
                                break;
                        }
                    //MessageBox.Show("HalfPlane: " + halfPlane.P1 + halfPlane.P2 + "\r\n" + "Line: " + line.P1 + line.P2 + "\r\n" + "Action taken: " + result);

                }
            }
        }

        public ClipToHalfPlaneAction ClipToHalfPlaneAction(LineSegment2D halfPlane, LineSegment2D line)
        {

            if ((halfPlane.Side(line.P1) == 1) && (halfPlane.Side(line.P2) == 1))
            {
                
                //both outside: discardLine
                return ImageProcessing.ClipToHalfPlaneAction.DISCARD_LINE;
                
            }   
            else if(halfPlane.Side(line.P1) == 1)
            {
                
                //start outside; end inside: replace start with intersection
                return ImageProcessing.ClipToHalfPlaneAction.UPDATE_START;
            }
            else if(halfPlane.Side(line.P2) == 1)
            {
                
                //end outside, start inside: replace end with intersection
                return ImageProcessing.ClipToHalfPlaneAction.UPDATE_END;
            }
            else
            {
                
                //both inside: keep both points
                return ImageProcessing.ClipToHalfPlaneAction.DO_NOTHING;
            }

        }

        public Point Intersection(LineSegment2D line1, LineSegment2D line2)
        {

            Point ret;
            double retX, retY;

            int numeratorExp1, numeratorExp2, denominator;

            numeratorExp1 = (line1.P1.X * line1.P2.Y) - (line1.P1.Y * line1.P2.X);
            numeratorExp2 = (line2.P1.X * line2.P2.Y) - (line2.P1.Y * line2.P2.X);

            denominator = ((line1.P1.X - line1.P2.X) * (line2.P1.Y - line2.P2.Y)) - ((line1.P1.Y - line1.P2.Y) * (line2.P1.X - line2.P2.X));

            retX = ((numeratorExp1 * (line2.P1.X - line2.P2.X)) - ((line1.P1.X - line1.P2.X) * numeratorExp2)) / (double)denominator;
            retY = ((numeratorExp1 * (line2.P1.Y - line2.P2.Y)) - ((line1.P1.Y - line1.P2.Y) * numeratorExp2)) / (double)denominator;

            //MessageBox.Show("Line1: " + line1.P1 + line1.P2 + "\r\n" + "Line2: " + line2.P1 + line2.P2 +"\r\n" + "Intersection X: " + retX + "\r\n" + "Intersection Y: " + retY);

            ret = new Point((int)retX, (int)retY);

            return ret;
        }

        public bool IsIntersecting(LineSegment2D line1, LineSegment2D line2)
        {
            int denominator = ((line1.P1.X - line1.P2.X) * (line2.P1.Y - line2.P2.Y)) - ((line1.P1.Y - line1.P2.Y) * (line2.P1.X - line2.P2.Y));
            return (denominator != 0);
        }

        //returns area of intersectionRegion
        public double ComputeAreaOfIntersectionRegion(Point[] intersectionRegionPts)
        {
            double area = 0;

            int numVertices = intersectionRegionPts.Length;

            int[] pairs = new int[numVertices];

            for (int i = 1; i <= numVertices; i++)
            {
                if (i == numVertices)
                {
                    pairs[i - 1] = (intersectionRegionPts[i - 1].X * intersectionRegionPts[0].Y) - (intersectionRegionPts[i - 1].Y * intersectionRegionPts[0].X);
                }
                else
                {
                    pairs[i - 1] = (intersectionRegionPts[i - 1].X * intersectionRegionPts[i].Y) - (intersectionRegionPts[i - 1].Y * intersectionRegionPts[i].X);
                }

            }

            //compute area using formula: [(x1y2 - y1x2) + (x2y3 - y2x3) + ... + (xny1 - ynx1)]/2 ; where n is the nubmer of vertices

            int sumPairs = 0;
            foreach (int i in pairs)
            {
                sumPairs += i;
            }

            area = sumPairs / 2.0;

            return area;
        }


    }
}
