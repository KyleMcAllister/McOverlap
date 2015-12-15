using Emgu.CV;
using Emgu.CV.Structure;
using System;


//gets passed an image which has two colors: key, notkey
//fills in the area enclosed by the polygon [polygon is in the key color]
public class PolyFiller
{
    Bgr key;
    Bgr notkey;

    int hit1;
    int hit2;

    int gradHit;

    bool maybeInbetween;
    bool inbetween;

    public PolyFiller(Bgr key, Bgr notkey)
	{
        this.key = key;
        this.notkey = notkey;
	}

    public Image<Bgr, byte> fillPoly(Image<Bgr, byte> img)
    {
        /*
        *   complete border:
        *   - first row; last col; last row; first col
        *   - 3 cases:
        *       -> if(inbetween): fill inbetween first and last hit
        *       -> else:
        *           -> if hit in 2nd half: fill until first hit
        *           -> if hit in first half: fill from first hit
        */

        //fill rows [2nd row to 2nd last row]: only fill between two hits if(inbetween); must be the case at this point

        completeBorder(img);

        fillEachRow(img);

        return img;
    }

    private void completeBorder(Image<Bgr, byte> img)
    {
        scanRow(img, 0);
        completeRow(img, 0);

        scanCol(img, img.Cols - 1);
        completeCol(img, img.Cols - 1);

        scanRow(img, img.Rows - 1);
        completeRow(img, img.Rows - 1);

        scanCol(img, 0);
        completeCol(img, 0);



    }

    private void scanRow(Image<Bgr, byte> img,int row)
    {
        hit1 = -1;
        hit2 = -1;
        maybeInbetween = false;
        inbetween = false;

        for(int j = 0; j < img.Cols; j++)
        {
            if(img[row, j].Equals(key))
            {
                if(hit1 == -1)
                {
                    hit1 = j;
                }
                else
                {
                    if (maybeInbetween)
                    {
                        inbetween = true;
                    }
                    hit2 = j;
                }
            }
            else
            {
                if(hit1 != -1)
                {
                    maybeInbetween = true;
                }
            }
        }
    }

    private void completeRow(Image<Bgr, byte> img, int row)
    {
        gradHit = -1;

        if (hit1 != -1)
        {
            if(hit2 != -1)
            {
                if (inbetween)
                {
                    for (int j = hit1; j <= hit2; j++)
                    {
                        img[row, j] = key;
                    }
                }
                else
                {
                    if (row < img.Rows - 2)
                    {
                        for (int j = 0; j < img.Cols; j++)
                        {
                            if (img[row + 1, j].Equals(key))
                            {
                                gradHit = j;
                                break;
                            }
                        }
                        if (gradHit != -1)
                        {
                            if (hit1 >= gradHit)
                            {
                                //fill from first hit:
                                for (int j = hit1; j < img.Cols; j++)
                                {
                                    img[row, j] = key;
                                }
                            }
                            else
                            {
                                //fill until first hit
                                for (int j = 0; j <= hit1; j++)
                                {
                                    img[row, j] = key;
                                }
                            }
                        }

                    }
                    else
                    {
                        for (int j = 0; j < img.Cols; j++)
                        {
                            if (img[row - 1, j].Equals(key))
                            {
                                gradHit = j;
                                break;
                            }
                        }
                        if (gradHit != -1)
                        {
                            if (hit1 >= gradHit)
                            {
                                //fill from first hit:
                                for (int j = hit1; j < img.Cols; j++)
                                {
                                    img[row, j] = key;
                                }
                            }
                            else
                            {
                                //fill until first hit
                                for (int j = 0; j <= hit1; j++)
                                {
                                    img[row, j] = key;
                                }
                            }
                        }

                    }

                }
            }
            else
            {
                if(row < img.Rows - 2)
                {
                    for(int j = 0; j < img.Cols; j++)
                    {
                        if (img[row + 1,j].Equals(key))
                        {
                            gradHit = j;
                            break;
                        }
                    }
                    if(gradHit != -1)
                    {
                        if (hit1 >= gradHit)
                        {
                            //fill from first hit:
                            for (int j = hit1; j < img.Cols; j++)
                            {
                                img[row, j] = key;
                            }
                        }
                        else
                        {
                            //fill until first hit
                            for (int j = 0; j <= hit1; j++)
                            {
                                img[row, j] = key;
                            }
                        }
                    }
                    
                }
                else
                {
                    for(int j = 0; j < img.Cols; j++)
                    {
                        if (img[row - 1, j].Equals(key))
                        {
                            gradHit = j;
                            break;
                        }
                    }
                    if (gradHit != -1)
                    {
                        if (hit1 >= gradHit)
                        {
                            //fill from first hit:
                            for (int j = hit1; j < img.Cols; j++)
                            {
                                img[row, j] = key;
                            }
                        }
                        else
                        {
                            //fill until first hit
                            for (int j = 0; j <= hit1; j++)
                            {
                                img[row, j] = key;
                            }
                        }
                    }
                    
                }
                
            }
        }
        
    }

    private void scanCol(Image<Bgr, byte> img, int col)
    {
        hit1 = -1;
        hit2 = -1;
        maybeInbetween = false;
        inbetween = false;
        for(int i = 0; i < img.Rows; i++)
        {
            if(img[i, col].Equals(key))
            {
                if(hit1 == -1)
                {
                    hit1 = i;
                }
                else
                {
                    if (maybeInbetween)
                    {
                        inbetween = true;
                    }
                    hit2 = i;
                }
            }
            else
            {
                if(hit1 != -1)
                {
                    maybeInbetween = true;
                }
            }
        }
    }

    private void completeCol(Image<Bgr, byte> img, int col)
    {
        gradHit = -1;

        if (hit1 != -1)
        {
            if(hit2 != -1)
            {
                if (inbetween)
                {
                    for (int i = hit1; i < hit2; i++)
                    {
                        img[i, col] = key;
                    }
                }
                else
                {
                    if (col < img.Cols - 2)
                    {
                        for (int i = 0; i < img.Rows; i++)
                        {
                            if (img[i, col + 1].Equals(key))
                            {
                                gradHit = i;
                                break;
                            }

                        }
                        if (gradHit != -1)
                        {
                            if (hit1 >= gradHit)
                            {
                                //fill from first hit:
                                for (int i = hit1; i < img.Rows; i++)
                                {
                                    img[i, col] = key;
                                }
                            }
                            else
                            {
                                //fill until first hit:
                                for (int i = 0; i <= hit1; i++)
                                {
                                    img[i, col] = key;
                                }
                            }
                        }

                    }
                    else
                    {
                        for (int i = 0; i < img.Rows; i++)
                        {
                            if (img[i, col - 1].Equals(key))
                            {
                                gradHit = i;
                                break;
                            }
                        }
                        if (gradHit != -1)
                        {
                            if (hit1 >= gradHit)
                            {
                                //fill from first hit:
                                for (int i = hit1; i < img.Rows; i++)
                                {
                                    img[i, col] = key;
                                }
                            }
                            else
                            {
                                //fill until first hit:
                                for (int i = 0; i <= hit1; i++)
                                {
                                    img[i, col] = key;
                                }
                            }
                        }
                    }

                }
            }
            
            else
            {
                if(col < img.Cols - 2)
                {
                    for(int i = 0; i < img.Rows; i++)
                    {
                        if(img[i,col + 1].Equals(key))
                        {
                            gradHit = i;
                            break;
                        }

                    }
                    if(gradHit != -1)
                    {
                        if (hit1 >= gradHit)
                        {
                            //fill from first hit:
                            for (int i = hit1; i < img.Rows; i++)
                            {
                                img[i, col] = key;
                            }
                        }
                        else
                        {
                            //fill until first hit:
                            for (int i = 0; i <= hit1; i++)
                            {
                                img[i, col] = key;
                            }
                        }
                    }
                    
                }
                else
                {
                    for(int i = 0; i < img.Rows; i++)
                    {
                        if (img[i, col - 1].Equals(key))
                        {
                            gradHit = i;
                            break;
                        }
                    }
                    if(gradHit != -1)
                    {
                        if (hit1 >= gradHit)
                        {
                            //fill from first hit:
                            for (int i = hit1; i < img.Rows; i++)
                            {
                                img[i, col] = key;
                            }
                        }
                        else
                        {
                            //fill until first hit:
                            for (int i = 0; i <= hit1; i++)
                            {
                                img[i, col] = key;
                            }
                        }
                    }
                }
                
            }
        }
        
    }

    private void fillEachRow(Image<Bgr, byte> img)
    {
        for(int i = 1; i < img.Rows -1; i++)
        {
            scanRow(img, i);

            completeRow(img, i);

        }
    }


}
