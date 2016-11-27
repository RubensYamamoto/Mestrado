namespace VolatilityViewer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series21 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series22 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series23 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series24 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.comboBoxMaturities = new System.Windows.Forms.ComboBox();
            this.labelMaturity = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea6.AxisX.Interval = 2.5D;
            chartArea6.AxisX.IsStartedFromZero = false;
            chartArea6.AxisX2.Interval = 2.5D;
            chartArea6.AxisX2.IsStartedFromZero = false;
            chartArea6.AxisY.Interval = 2.5D;
            chartArea6.AxisY.IsStartedFromZero = false;
            chartArea6.AxisY2.Interval = 2.5D;
            chartArea6.AxisY2.IsStartedFromZero = false;
            chartArea6.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart1.Legends.Add(legend6);
            this.chart1.Location = new System.Drawing.Point(15, 43);
            this.chart1.Name = "chart1";
            series21.ChartArea = "ChartArea1";
            series21.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series21.Legend = "Legend1";
            series21.MarkerImage = "C:\\Users\\Rubens\\Documents\\Visual Studio 2010\\Projects\\VolatilityViewer\\Volatility" +
                "Viewer\\bmp\\upBlueTriangle.bmp";
            series21.MarkerImageTransparentColor = System.Drawing.Color.White;
            series21.Name = "call_bid";
            series22.ChartArea = "ChartArea1";
            series22.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series22.Legend = "Legend1";
            series22.MarkerImage = "C:\\Users\\Rubens\\Documents\\Visual Studio 2010\\Projects\\VolatilityViewer\\Volatility" +
                "Viewer\\bmp\\downBlueTriangle.bmp";
            series22.MarkerImageTransparentColor = System.Drawing.Color.White;
            series22.Name = "call_ask";
            series23.ChartArea = "ChartArea1";
            series23.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series23.Legend = "Legend1";
            series23.MarkerImage = "C:\\Users\\Rubens\\Documents\\Visual Studio 2010\\Projects\\VolatilityViewer\\Volatility" +
                "Viewer\\bmp\\upRedTriangle.bmp";
            series23.MarkerImageTransparentColor = System.Drawing.Color.White;
            series23.Name = "put_bid";
            series24.ChartArea = "ChartArea1";
            series24.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series24.Legend = "Legend1";
            series24.MarkerImage = "C:\\Users\\Rubens\\Documents\\Visual Studio 2010\\Projects\\VolatilityViewer\\Volatility" +
                "Viewer\\bmp\\downRedTriangle.bmp";
            series24.MarkerImageTransparentColor = System.Drawing.Color.White;
            series24.Name = "put_ask";
            this.chart1.Series.Add(series21);
            this.chart1.Series.Add(series22);
            this.chart1.Series.Add(series23);
            this.chart1.Series.Add(series24);
            this.chart1.Size = new System.Drawing.Size(887, 527);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // buttonBuild
            // 
            this.buttonBuild.Location = new System.Drawing.Point(15, 11);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(91, 26);
            this.buttonBuild.TabIndex = 3;
            this.buttonBuild.Text = "Load Data";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.buttonLoadData_Click);
            // 
            // comboBoxMaturities
            // 
            this.comboBoxMaturities.FormatString = "dd-MMM-yyyy";
            this.comboBoxMaturities.FormattingEnabled = true;
            this.comboBoxMaturities.Location = new System.Drawing.Point(781, 11);
            this.comboBoxMaturities.Name = "comboBoxMaturities";
            this.comboBoxMaturities.Size = new System.Drawing.Size(121, 24);
            this.comboBoxMaturities.TabIndex = 4;
            this.comboBoxMaturities.SelectedIndexChanged += new System.EventHandler(this.comboBoxMaturities_SelectedIndexChanged);
            // 
            // labelMaturity
            // 
            this.labelMaturity.AutoSize = true;
            this.labelMaturity.Location = new System.Drawing.Point(713, 14);
            this.labelMaturity.Name = "labelMaturity";
            this.labelMaturity.Size = new System.Drawing.Size(62, 17);
            this.labelMaturity.TabIndex = 5;
            this.labelMaturity.Text = "Maturity:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 584);
            this.Controls.Add(this.labelMaturity);
            this.Controls.Add(this.comboBoxMaturities);
            this.Controls.Add(this.buttonBuild);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Volatility Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.ComboBox comboBoxMaturities;
        private System.Windows.Forms.Label labelMaturity;
    }
}

