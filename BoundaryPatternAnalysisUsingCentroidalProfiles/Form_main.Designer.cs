namespace BoundaryPatternAnalysisUsingCentroidalProfiles
{
    partial class Form_main
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.pictureBox_org = new System.Windows.Forms.PictureBox();
            this.button_imgSelect = new System.Windows.Forms.Button();
            this.pictureBox_tran = new System.Windows.Forms.PictureBox();
            this.button_Convert = new System.Windows.Forms.Button();
            this.pictureBox_template = new System.Windows.Forms.PictureBox();
            this.button_templateSelect = new System.Windows.Forms.Button();
            this.pictureBox_templateContour = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_Match = new System.Windows.Forms.Button();
            this.chart_result = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_org)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tran)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_template)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_templateContour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_result)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_org
            // 
            this.pictureBox_org.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_org.Location = new System.Drawing.Point(12, 41);
            this.pictureBox_org.Name = "pictureBox_org";
            this.pictureBox_org.Size = new System.Drawing.Size(600, 400);
            this.pictureBox_org.TabIndex = 0;
            this.pictureBox_org.TabStop = false;
            // 
            // button_imgSelect
            // 
            this.button_imgSelect.Location = new System.Drawing.Point(12, 12);
            this.button_imgSelect.Name = "button_imgSelect";
            this.button_imgSelect.Size = new System.Drawing.Size(75, 23);
            this.button_imgSelect.TabIndex = 1;
            this.button_imgSelect.Text = "Select Image";
            this.button_imgSelect.UseVisualStyleBackColor = true;
            this.button_imgSelect.Click += new System.EventHandler(this.button_imgSelect_Click);
            // 
            // pictureBox_tran
            // 
            this.pictureBox_tran.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_tran.Location = new System.Drawing.Point(624, 41);
            this.pictureBox_tran.Name = "pictureBox_tran";
            this.pictureBox_tran.Size = new System.Drawing.Size(600, 400);
            this.pictureBox_tran.TabIndex = 2;
            this.pictureBox_tran.TabStop = false;
            // 
            // button_Convert
            // 
            this.button_Convert.Enabled = false;
            this.button_Convert.Location = new System.Drawing.Point(588, 481);
            this.button_Convert.Name = "button_Convert";
            this.button_Convert.Size = new System.Drawing.Size(56, 23);
            this.button_Convert.TabIndex = 3;
            this.button_Convert.Text = "Convert";
            this.button_Convert.UseVisualStyleBackColor = true;
            this.button_Convert.Click += new System.EventHandler(this.button_Convert_Click);
            // 
            // pictureBox_template
            // 
            this.pictureBox_template.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_template.Location = new System.Drawing.Point(72, 447);
            this.pictureBox_template.Name = "pictureBox_template";
            this.pictureBox_template.Size = new System.Drawing.Size(175, 135);
            this.pictureBox_template.TabIndex = 4;
            this.pictureBox_template.TabStop = false;
            // 
            // button_templateSelect
            // 
            this.button_templateSelect.Location = new System.Drawing.Point(93, 12);
            this.button_templateSelect.Name = "button_templateSelect";
            this.button_templateSelect.Size = new System.Drawing.Size(102, 23);
            this.button_templateSelect.TabIndex = 6;
            this.button_templateSelect.Text = "Select template";
            this.button_templateSelect.UseVisualStyleBackColor = true;
            this.button_templateSelect.Click += new System.EventHandler(this.button_templateSelect_Click);
            // 
            // pictureBox_templateContour
            // 
            this.pictureBox_templateContour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_templateContour.Location = new System.Drawing.Point(407, 447);
            this.pictureBox_templateContour.Name = "pictureBox_templateContour";
            this.pictureBox_templateContour.Size = new System.Drawing.Size(175, 135);
            this.pictureBox_templateContour.TabIndex = 7;
            this.pictureBox_templateContour.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 452);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Template:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(307, 452);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "Template Contours:";
            // 
            // button_Match
            // 
            this.button_Match.Enabled = false;
            this.button_Match.Location = new System.Drawing.Point(588, 452);
            this.button_Match.Name = "button_Match";
            this.button_Match.Size = new System.Drawing.Size(56, 23);
            this.button_Match.TabIndex = 10;
            this.button_Match.Text = "Match";
            this.button_Match.UseVisualStyleBackColor = true;
            this.button_Match.Click += new System.EventHandler(this.button_Match_Click);
            // 
            // chart_result
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_result.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_result.Legends.Add(legend1);
            this.chart_result.Location = new System.Drawing.Point(650, 447);
            this.chart_result.Name = "chart_result";
            this.chart_result.Size = new System.Drawing.Size(564, 135);
            this.chart_result.TabIndex = 11;
            this.chart_result.Text = "chart1";
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 594);
            this.Controls.Add(this.chart_result);
            this.Controls.Add(this.button_Match);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox_templateContour);
            this.Controls.Add(this.button_templateSelect);
            this.Controls.Add(this.pictureBox_template);
            this.Controls.Add(this.button_Convert);
            this.Controls.Add(this.pictureBox_tran);
            this.Controls.Add(this.button_imgSelect);
            this.Controls.Add(this.pictureBox_org);
            this.Name = "Form_main";
            this.Text = "main";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_org)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tran)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_template)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_templateContour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_result)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_org;
        private System.Windows.Forms.Button button_imgSelect;
        private System.Windows.Forms.PictureBox pictureBox_tran;
        private System.Windows.Forms.Button button_Convert;
        private System.Windows.Forms.PictureBox pictureBox_template;
        private System.Windows.Forms.Button button_templateSelect;
        private System.Windows.Forms.PictureBox pictureBox_templateContour;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_Match;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_result;
    }
}

