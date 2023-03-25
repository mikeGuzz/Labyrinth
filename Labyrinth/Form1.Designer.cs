namespace Labyrinth
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.status_label = new System.Windows.Forms.Label();
            this.diamondIcn_pictureBox = new System.Windows.Forms.PictureBox();
            this.diamonds_label = new System.Windows.Forms.Label();
            this.mode_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bestTime_label = new System.Windows.Forms.Label();
            this.bestTimeIcn_pictureBox = new System.Windows.Forms.PictureBox();
            this.winsCount_label = new System.Windows.Forms.Label();
            this.winsCountIcn_pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diamondIcn_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bestTimeIcn_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.winsCountIcn_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(533, 683);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 42);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(624, 624);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // status_label
            // 
            this.status_label.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.status_label.Location = new System.Drawing.Point(144, 9);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(464, 30);
            this.status_label.TabIndex = 2;
            this.status_label.Text = "Are you ready?";
            this.status_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // diamondIcn_pictureBox
            // 
            this.diamondIcn_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.diamondIcn_pictureBox.Location = new System.Drawing.Point(12, 9);
            this.diamondIcn_pictureBox.Name = "diamondIcn_pictureBox";
            this.diamondIcn_pictureBox.Size = new System.Drawing.Size(27, 27);
            this.diamondIcn_pictureBox.TabIndex = 3;
            this.diamondIcn_pictureBox.TabStop = false;
            this.diamondIcn_pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.diamondIcn_pictureBox_Paint);
            // 
            // diamonds_label
            // 
            this.diamonds_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.diamonds_label.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.diamonds_label.ForeColor = System.Drawing.Color.Teal;
            this.diamonds_label.Location = new System.Drawing.Point(45, 9);
            this.diamonds_label.Name = "diamonds_label";
            this.diamonds_label.Size = new System.Drawing.Size(93, 27);
            this.diamonds_label.TabIndex = 4;
            this.diamonds_label.Text = "Diamonds: 00";
            this.diamonds_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.diamonds_label.Click += new System.EventHandler(this.diamonds_label_Click);
            // 
            // mode_comboBox
            // 
            this.mode_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mode_comboBox.FormattingEnabled = true;
            this.mode_comboBox.Location = new System.Drawing.Point(405, 684);
            this.mode_comboBox.Name = "mode_comboBox";
            this.mode_comboBox.Size = new System.Drawing.Size(121, 23);
            this.mode_comboBox.TabIndex = 5;
            this.mode_comboBox.SelectedIndexChanged += new System.EventHandler(this.mode_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(355, 687);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Mode: ";
            // 
            // bestTime_label
            // 
            this.bestTime_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bestTime_label.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bestTime_label.ForeColor = System.Drawing.SystemColors.Highlight;
            this.bestTime_label.Location = new System.Drawing.Point(45, 679);
            this.bestTime_label.Name = "bestTime_label";
            this.bestTime_label.Size = new System.Drawing.Size(97, 27);
            this.bestTime_label.TabIndex = 8;
            this.bestTime_label.Text = "Best time: 00";
            this.bestTime_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bestTimeIcn_pictureBox
            // 
            this.bestTimeIcn_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.bestTimeIcn_pictureBox.Location = new System.Drawing.Point(12, 679);
            this.bestTimeIcn_pictureBox.Name = "bestTimeIcn_pictureBox";
            this.bestTimeIcn_pictureBox.Size = new System.Drawing.Size(27, 27);
            this.bestTimeIcn_pictureBox.TabIndex = 7;
            this.bestTimeIcn_pictureBox.TabStop = false;
            this.bestTimeIcn_pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.bestTimeIcn_pictureBox_Paint);
            // 
            // winsCount_label
            // 
            this.winsCount_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.winsCount_label.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.winsCount_label.ForeColor = System.Drawing.SystemColors.Highlight;
            this.winsCount_label.Location = new System.Drawing.Point(181, 679);
            this.winsCount_label.Name = "winsCount_label";
            this.winsCount_label.Size = new System.Drawing.Size(134, 27);
            this.winsCount_label.TabIndex = 10;
            this.winsCount_label.Text = "Number of wins: 00";
            this.winsCount_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // winsCountIcn_pictureBox
            // 
            this.winsCountIcn_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.winsCountIcn_pictureBox.Location = new System.Drawing.Point(148, 679);
            this.winsCountIcn_pictureBox.Name = "winsCountIcn_pictureBox";
            this.winsCountIcn_pictureBox.Size = new System.Drawing.Size(27, 27);
            this.winsCountIcn_pictureBox.TabIndex = 9;
            this.winsCountIcn_pictureBox.TabStop = false;
            this.winsCountIcn_pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.winsCountIcn_pictureBox_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 718);
            this.Controls.Add(this.winsCount_label);
            this.Controls.Add(this.winsCountIcn_pictureBox);
            this.Controls.Add(this.bestTime_label);
            this.Controls.Add(this.bestTimeIcn_pictureBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mode_comboBox);
            this.Controls.Add(this.diamonds_label);
            this.Controls.Add(this.diamondIcn_pictureBox);
            this.Controls.Add(this.status_label);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "mainForm";
            this.Text = "Labyrinth";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diamondIcn_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bestTimeIcn_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.winsCountIcn_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button button1;
        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private Label status_label;
        private PictureBox diamondIcn_pictureBox;
        private Label diamonds_label;
        private ComboBox mode_comboBox;
        private Label label1;
        private Label bestTime_label;
        private PictureBox bestTimeIcn_pictureBox;
        private Label winsCount_label;
        private PictureBox winsCountIcn_pictureBox;
    }
}