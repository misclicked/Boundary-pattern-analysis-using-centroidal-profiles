using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CVC;

namespace BoundaryPatternAnalysisUsingCentroidalProfiles
{
    public partial class Form_main : Form
    {
        private CVClass Cv = new CVClass();
        bool imageloaded = false;
        bool templateloaded = false;
        public Form_main()
        {
            InitializeComponent();
        }

        private void button_imgSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                using (Bitmap bmp = new Bitmap(open.FileName))
                {
                    pictureBox_org.Image = new Bitmap(bmp, ExpandToBound(bmp.Size, pictureBox_org.Size));
                }
                imageloaded = true;
                this.Text = open.FileName;
            }
        }

        private static Size ExpandToBound(Size image, Size boundingBox)
        {
            double widthScale = 0, heightScale = 0;
            if (image.Width != 0)
                widthScale = (double)boundingBox.Width / (double)image.Width;
            if (image.Height != 0)
                heightScale = (double)boundingBox.Height / (double)image.Height;

            double scale = Math.Min(widthScale, heightScale);

            Size result = new Size((int)(image.Width * scale),
                                (int)(image.Height * scale));
            return result;
        }

        private void button_Convert_Click(object sender, EventArgs e)
        {
            pictureBox_templateContour.Image = Cv.ToLargestContour((Bitmap)pictureBox_template.Image);
            chart_result.Series.Clear();
            chart_result.Series.Add(Cv.BoundaryAnalysis((Bitmap)pictureBox_templateContour.Image));
            if (imageloaded)
                button_Match.Enabled = true;
        }

        private void button_templateSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                using (Bitmap bmp = new Bitmap(open.FileName))
                {
                    pictureBox_template.Image = new Bitmap(bmp, ExpandToBound(bmp.Size, pictureBox_template.Size));
                }
                templateloaded = true;
                if (imageloaded == true)
                    button_Convert.Enabled = true;
                this.Text = open.FileName;
            }
        }

        private void button_Match_Click(object sender, EventArgs e)
        {
            pictureBox_tran.Image = Cv.ContourMatching((Bitmap)pictureBox_org.Image, (Bitmap)pictureBox_templateContour.Image);
        }
    }
}
