namespace Lab09taks2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

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
            comboBoxCities = new System.Windows.Forms.ComboBox();
            buttonGetWeather = new System.Windows.Forms.Button();
            listViewResults = new System.Windows.Forms.ListView();
            columnHeaderCity = new System.Windows.Forms.ColumnHeader();
            columnHeaderCountry = new System.Windows.Forms.ColumnHeader();
            columnHeaderTemp = new System.Windows.Forms.ColumnHeader();
            columnHeaderDesc = new System.Windows.Forms.ColumnHeader();
            progressBar = new System.Windows.Forms.ProgressBar();
            statusLabel = new System.Windows.Forms.Label();
            labelCity = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // comboBoxCities
            // 
            comboBoxCities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxCities.FormattingEnabled = true;
            comboBoxCities.Location = new System.Drawing.Point(19, 53);
            comboBoxCities.Name = "comboBoxCities";
            comboBoxCities.Size = new System.Drawing.Size(769, 40);
            comboBoxCities.TabIndex = 0;
            // 
            // buttonGetWeather
            // 
            buttonGetWeather.Location = new System.Drawing.Point(816, 519);
            buttonGetWeather.Name = "buttonGetWeather";
            buttonGetWeather.Size = new System.Drawing.Size(224, 45);
            buttonGetWeather.TabIndex = 1;
            buttonGetWeather.Text = "Получить погоду";
            buttonGetWeather.UseVisualStyleBackColor = true;
            buttonGetWeather.Click += buttonGetWeather_Click;
            // 
            // listViewResults
            // 
            listViewResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeaderCity, columnHeaderCountry, columnHeaderTemp, columnHeaderDesc });
            listViewResults.FullRowSelect = true;
            listViewResults.GridLines = true;
            listViewResults.Location = new System.Drawing.Point(19, 122);
            listViewResults.Name = "listViewResults";
            listViewResults.Size = new System.Drawing.Size(1021, 358);
            listViewResults.TabIndex = 2;
            listViewResults.UseCompatibleStateImageBehavior = false;
            listViewResults.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderCity
            // 
            columnHeaderCity.Name = "columnHeaderCity";
            columnHeaderCity.Text = "Город";
            columnHeaderCity.Width = 150;
            // 
            // columnHeaderCountry
            // 
            columnHeaderCountry.Name = "columnHeaderCountry";
            columnHeaderCountry.Text = "Страна";
            columnHeaderCountry.Width = 169;
            // 
            // columnHeaderTemp
            // 
            columnHeaderTemp.Name = "columnHeaderTemp";
            columnHeaderTemp.Text = "Температура";
            columnHeaderTemp.Width = 227;
            // 
            // columnHeaderDesc
            // 
            columnHeaderDesc.Name = "columnHeaderDesc";
            columnHeaderDesc.Text = "Описание";
            columnHeaderDesc.Width = 419;
            // 
            // progressBar
            // 
            progressBar.Location = new System.Drawing.Point(19, 519);
            progressBar.Name = "progressBar";
            progressBar.Size = new System.Drawing.Size(770, 45);
            progressBar.TabIndex = 3;
            progressBar.Visible = false;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new System.Drawing.Point(218, 448);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(0, 32);
            statusLabel.TabIndex = 4;
            // 
            // labelCity
            // 
            labelCity.AutoSize = true;
            labelCity.Location = new System.Drawing.Point(19, 9);
            labelCity.Name = "labelCity";
            labelCity.Size = new System.Drawing.Size(199, 32);
            labelCity.TabIndex = 5;
            labelCity.Text = "Выберите город:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1052, 594);
            Controls.Add(labelCity);
            Controls.Add(statusLabel);
            Controls.Add(progressBar);
            Controls.Add(listViewResults);
            Controls.Add(buttonGetWeather);
            Controls.Add(comboBoxCities);
            Margin = new System.Windows.Forms.Padding(5);
            Text = "Погода в городах мира";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCities;
        private System.Windows.Forms.Button buttonGetWeather;
        private System.Windows.Forms.ListView listViewResults;
        private System.Windows.Forms.ProgressBar progressBar;
        private Label statusLabel;
        private System.Windows.Forms.Label labelCity;
        private ColumnHeader columnHeaderCity;
        private ColumnHeader columnHeaderCountry;
        private ColumnHeader columnHeaderTemp;
        private ColumnHeader columnHeaderDesc;
    }
}