using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace CVC
{
    class CVClass
    {
        #region GlobalValley
        private int s(int u)
        {
            return u > 0 ? u : 0;
        }
        public Image GlobalValley(Bitmap bmp, bool useF, bool usemedianBlur, out int threshold)
        {
            Image ret;
            int[] hist = new int[256];
            int[] L = new int[256];
            int[] R = new int[256];
            using (Mat mat = BitmapConverter.ToMat(bmp))
            {
                if (usemedianBlur)
                {
                    using (Mat newMat = mat.MedianBlur(3))
                    {
                        for (int i = 0; i < newMat.Height; i++)
                        {
                            for (int j = 0; j < newMat.Width; j++)
                            {
                                hist[newMat.Get<Vec3b>(i, j).Item0]++;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < mat.Height; i++)
                    {
                        for (int j = 0; j < mat.Width; j++)
                        {
                            hist[mat.Get<Vec3b>(i, j).Item0]++;
                        }
                    }
                }

                for (int i = 1; i <= 255; i++)
                {
                    if (hist[i] > L[i - 1])
                    {
                        L[i] = hist[i];
                    }
                    else
                    {
                        L[i] = L[i - 1];
                    }
                }
                for (int i = 254; i >= 0; i--)
                {
                    if (hist[i] > R[i + 1])
                    {
                        R[i] = hist[i];
                    }
                    else
                    {
                        R[i] = R[i + 1];
                    }
                }
                int max = 0;
                int Fi;
                threshold = 0;
                for (int i = 1; i < 255; i++)
                {
                    if (useF)
                        Fi = s(L[i - 1] - hist[i]) + s(R[i + 1] - hist[i]);
                    else
                        Fi = s(L[i - 1] - hist[i]) * s(R[i + 1] - hist[i]);
                    if (Fi > max)
                    {
                        max = Fi;
                        threshold = i;
                    }
                }

                using (Mat newImage = new Mat())
                {
                    Cv2.Threshold(mat, newImage, threshold, 255, ThresholdTypes.Binary);
                    ret = newImage.ToBitmap();
                }
            }
            return ret;
        }
        #endregion
        #region Utli
        public Image ToGray(Bitmap bmp)
        {
            Image ret;
            using (Mat mat = BitmapConverter.ToMat(bmp))
            {
                using (Mat newImage = new Mat())
                {
                    Cv2.CvtColor(mat, newImage, ColorConversionCodes.BGR2GRAY);
                    ret = BitmapConverter.ToBitmap(newImage);
                }
            }
            return ret;
        }
        public Image fanbai(Bitmap bmp)
        {
            using (Mat mat = BitmapConverter.ToMat(bmp))
            {
                for (int i = 0; i < mat.Height; i++)
                {
                    for (int j = 0; j < mat.Width; j++)
                    {
                        Vec3b v = mat.Get<Vec3b>(i, j);
                        byte b = BitConverter.GetBytes(255)[0];
                        v.Item0 = BitConverter.GetBytes(b - v.Item0)[0];
                        v.Item1 = BitConverter.GetBytes(b - v.Item1)[0];
                        v.Item2 = BitConverter.GetBytes(b - v.Item2)[0];
                        mat.Set<Vec3b>(i, j, v);
                    }
                }
                return mat.ToBitmap();
            }
        }
        Window histoWindow;
        public void ShowHistogram(Bitmap img, int threshold, bool useM)
        {
            if (histoWindow == null)
            {
                histoWindow = new Window("Histogram", GetHistogram(img, threshold, useM));
            }
            else
            {
                Cv2.ImShow("Histogram", GetHistogram(img, threshold, useM));
            }
        }
        public Mat GetHistogram(Bitmap img, int threshold, bool useM)
        {
            int[] hist = new int[256];
            int width = 800, height = 450;
            using (Mat mat = useM ? BitmapConverter.ToMat(img).MedianBlur(3) : BitmapConverter.ToMat(img))
            {
                for (int i = 0; i < mat.Height; i++)
                {
                    for (int j = 0; j < mat.Width; j++)
                    {
                        hist[mat.Get<Vec3b>(i, j).Item0]++;
                    }
                }
            }
            int MaxVal = hist[0];
            for (int i = 1; i < 256; i++)
            {
                MaxVal = Math.Max(MaxVal, hist[i]);
            }

            Mat render = new Mat(new OpenCvSharp.Size(width, height), MatType.CV_8UC3, Scalar.All(255));
            Scalar color = Scalar.All(100);
            int binW = width / 256;
            for (int j = 0; j < 256; ++j)
            {
                if (j == threshold)
                {
                    render.Rectangle(
                        new OpenCvSharp.Point(j * binW, render.Rows - (hist[j] / (double)MaxVal) * height),
                        new OpenCvSharp.Point((j + 1) * binW, render.Rows),
                        Scalar.Red,
                        -1);
                }
                else
                {
                    render.Rectangle(
                        new OpenCvSharp.Point(j * binW, render.Rows - (hist[j] / (double)MaxVal) * height),
                        new OpenCvSharp.Point((j + 1) * binW, render.Rows),
                        color,
                        -1);
                }
            }
            Cv2.ArrowedLine(render, new OpenCvSharp.Point((threshold + 0.5) * binW, 0),
                new OpenCvSharp.Point((threshold + 0.5) * binW, height * 0.3), Scalar.Red, binW / 2);
            return render;
        }
        #endregion
        #region edgeDetection
        const int SIZE = (int)(2 * Math.PI * 100) + 1;
        public Image ToContours(Bitmap bmp)
        {
            using(Mat mat = BitmapConverter.ToMat(bmp))
            {
                var grayed = mat.CvtColor(ColorConversionCodes.BGR2GRAY);
                //var blurred = grayed.GaussianBlur(new OpenCvSharp.Size(11, 11), 0);
                var blurred = grayed.BilateralFilter(5, 30, 30);
                Mat tmp = new Mat();
                double thresh = Cv2.Threshold(blurred, tmp, 0, 255, ThresholdTypes.Otsu);
                double thresh_high = thresh;
                double thresh_low = thresh * 0.5;
                var edged = blurred.Canny(thresh_low, thresh_high);
                Mat output, outputArr;
                Mat[] contours;
                output = new Mat();
                outputArr = new Mat();
                Cv2.FindContours(edged, out contours, outputArr, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
                Mat ret = new Mat(bmp.Size.Height, bmp.Size.Width, MatType.CV_8UC1, new Scalar(0));
                Cv2.DrawContours(ret, contours, -1, Scalar.DarkBlue, 1);
                return ret.ToBitmap();
            }
        }
        public Image ToLargestContour(Bitmap bmp)
        {
            using (Mat mat = BitmapConverter.ToMat(bmp))
            {
                var grayed = mat.CvtColor(ColorConversionCodes.BGR2GRAY);
                //var blurred = grayed.GaussianBlur(new OpenCvSharp.Size(11, 11), 0);
                var blurred = grayed.BilateralFilter(5, 30, 30);
                Mat tmp = new Mat();
                double thresh = Cv2.Threshold(blurred, tmp, 0, 255, ThresholdTypes.Otsu);
                double thresh_high = thresh;
                double thresh_low = thresh * 0.5;
                var edged = blurred.Canny(thresh_low, thresh_high);
                Mat output, outputArr;
                Mat[] contours;
                output = new Mat();
                outputArr = new Mat();
                Cv2.FindContours(edged, out contours, outputArr, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
                int maxsz = -1;
                Mat[] target = new Mat[1];
                foreach (Mat m in contours)
                {
                    Rect r = Cv2.BoundingRect(m);
                    if (r.Height * r.Width > maxsz)
                    {
                        maxsz = r.Height * r.Width;
                        target[0] = m;
                    }
                }
                Mat ret = new Mat(bmp.Size.Height, bmp.Size.Width, MatType.CV_8UC1, new Scalar(0));
                Cv2.DrawContours(ret, target, 0, Scalar.DarkBlue, 1);
                return ret.ToBitmap();
            }
        }
        public Image ContourMatching(Bitmap target, Bitmap template)
        {
            Bitmap targetCon = (Bitmap)ToContours(target);
            Mat[] tarCons, temCons;
            using (Mat tar = BitmapConverter.ToMat(targetCon))
            {
                using(Mat tem = BitmapConverter.ToMat(template))
                {
                    Cv2.FindContours(tar, out tarCons, new Mat(), RetrievalModes.External, ContourApproximationModes.ApproxSimple);
                    Cv2.FindContours(tem, out temCons, new Mat(), RetrievalModes.External, ContourApproximationModes.ApproxSimple);
                    double min = double.MaxValue;
                    double mmin = double.MaxValue;
                    int idx = 0;
                    double[] temBA = BoundaryArrNormal(template);
                    double[] tarBA;
                    int rotation = 0;
                    int iidx = 0;
                    for(int i=0;i<tarCons.Count();i++)
                    {
                        double val = Cv2.MatchShapes(tarCons[i], temCons[0], ShapeMatchModes.I1);
                        if (mmin > val)
                        {
                            mmin = val;
                            iidx = i;
                        }
                        Mat tmp = new Mat(target.Size.Height, target.Size.Width, MatType.CV_8UC1, new Scalar(0));
                        tmp.DrawContours(tmp, tarCons, i, Scalar.White, 1);
                        Mat tmpCropped = new Mat(tmp, Cv2.BoundingRect(tarCons[i]));
                        tarBA = BoundaryArrNormal(tmpCropped.ToBitmap());
                        
                        double Dmin = double.MaxValue;
                        int Drt = 0;
                        for (int j = 0; j < SIZE; j++)
                        {
                            double Dj = 0.0;
                            for (int k = 0; k < SIZE; k++)
                            {
                                Dj += Math.Pow(temBA[k] - tarBA[(k + j) % SIZE], 2);
                                if (Dj > Dmin) break;
                            }
                            if (Dmin >= Dj)
                            {
                                Dmin = Dj;
                                Drt = j;
                            }
                        }
                        if (Dmin < min)
                        {
                            min = Dmin;
                            idx = i;
                            rotation = Drt;
                        }
                    }
                    Mat ret = new Mat(target.Size.Height, target.Size.Width, MatType.CV_8UC1, new Scalar(0));
                    Cv2.DrawContours(ret, tarCons, idx, Scalar.DarkBlue, 1);
                    return ret.ToBitmap();
                }
            }
        }
        #endregion
        #region BoundaryAnalysis
        public double[] BoundaryArrNormal(Bitmap bmp)
        {
            List<int>[] array = new List<int>[SIZE];
            for(int i = 0; i < SIZE; i++)
            {
                array[i] = new List<int>();
            }
            using (Mat mat = BitmapConverter.ToMat(bmp))
            {
                double centerX = mat.Width / 2;
                double centerY = mat.Height / 2;
                double mx = 0;
                for (int i = 0; i < mat.Height; i++)
                {
                    for (int j = 0; j < mat.Width; j++)
                    {
                        Vec3b v = mat.Get<Vec3b>(i, j);
                        if (v.Item0 != 0 || v.Item1 != 0 || v.Item2 != 0)
                        {
                            int idx = (int)((Math.Atan2(j - centerY, i - centerX) + Math.PI) * 100);
                            int value = (int)Math.Sqrt(Math.Pow(j - centerY, 2) + Math.Pow(i - centerX, 2));
                            array[idx].Add(value);
                            mx = Math.Max(mx, (double)(value));
                            
                        }
                    }
                }
                for (int i = 0; i < SIZE; i++)
                {
                    array[i].Sort();
                }
                double[] result = new double[SIZE];
                for (int i = 0; i < SIZE; i++)
                {
                    if(array[i].Count>0)
                        result[i] = array[i].Max()/mx;
                }
                return result;
            }
        }
        public Series BoundaryAnalysis (Bitmap bmp)
        {
            int[] array = new int[SIZE];
            Series series = new Series("result");
            series.Color = Color.Blue;
            series.ChartType = SeriesChartType.FastPoint;
            using (Mat mat = BitmapConverter.ToMat(bmp))
            {
                double centerX = mat.Width / 2;
                double centerY = mat.Height / 2;
                for (int i = 0; i < mat.Height; i++)
                {
                    for (int j = 0; j < mat.Width; j++)
                    {
                        Vec3b v = mat.Get<Vec3b>(i, j);
                        Console.WriteLine(v.Item0+" "+v.Item1+" "+v.Item2);
                        if (v.Item0!=0||v.Item1!=0||v.Item2!=0)
                        {
                            int idx = (int)((Math.Atan2(j - centerY, i - centerX) + Math.PI) * 100);
                            series.Points.AddXY(idx, (int)Math.Sqrt(Math.Pow(j - centerY, 2) + Math.Pow(i - centerX, 2)));
                            array[idx] = (int)Math.Sqrt(Math.Pow(j - centerY, 2) + Math.Pow(i - centerX, 2));
                        }
                    }
                }
                return series;
            }
        }
        #endregion
    }
}
