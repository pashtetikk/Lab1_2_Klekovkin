namespace klekovkinLab1
{
    partial class SimView
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
            this.generateBtm = new System.Windows.Forms.Button();
            this.fieldView = new System.Windows.Forms.PictureBox();
            this.manualCB = new System.Windows.Forms.CheckBox();
            this.modelUpdateTim = new System.Windows.Forms.Timer(this.components);
            this.viewUpdateTim = new System.Windows.Forms.Timer(this.components);
            this.lidarView = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cleanBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fieldView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lidarView)).BeginInit();
            this.SuspendLayout();
            // 
            // generateBtm
            // 
            this.generateBtm.Location = new System.Drawing.Point(12, 12);
            this.generateBtm.Name = "generateBtm";
            this.generateBtm.Size = new System.Drawing.Size(75, 23);
            this.generateBtm.TabIndex = 0;
            this.generateBtm.Text = "generate";
            this.generateBtm.UseVisualStyleBackColor = true;
            this.generateBtm.Click += new System.EventHandler(this.generateBtn_click);
            // 
            // fieldView
            // 
            this.fieldView.BackColor = System.Drawing.Color.White;
            this.fieldView.Location = new System.Drawing.Point(196, 12);
            this.fieldView.Name = "fieldView";
            this.fieldView.Size = new System.Drawing.Size(592, 426);
            this.fieldView.TabIndex = 1;
            this.fieldView.TabStop = false;
            this.fieldView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fieldView_MouseDown);
            this.fieldView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.fieldView_MouseMove);
            this.fieldView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fieldView_MouseUp);
            // 
            // manualCB
            // 
            this.manualCB.AutoSize = true;
            this.manualCB.Location = new System.Drawing.Point(12, 419);
            this.manualCB.Name = "manualCB";
            this.manualCB.Size = new System.Drawing.Size(103, 19);
            this.manualCB.TabIndex = 2;
            this.manualCB.Text = "manual conrol";
            this.manualCB.UseVisualStyleBackColor = true;
            this.manualCB.CheckedChanged += new System.EventHandler(this.manualCB_CheckedChanged);
            // 
            // modelUpdateTim
            // 
            this.modelUpdateTim.Enabled = true;
            this.modelUpdateTim.Interval = 10;
            this.modelUpdateTim.Tick += new System.EventHandler(this.modelUpddateTim_Tick);
            // 
            // viewUpdateTim
            // 
            this.viewUpdateTim.Enabled = true;
            this.viewUpdateTim.Interval = 20;
            this.viewUpdateTim.Tick += new System.EventHandler(this.viewUpdateTim_Tick);
            // 
            // lidarView
            // 
            this.lidarView.BackColor = System.Drawing.Color.White;
            this.lidarView.Location = new System.Drawing.Point(827, 139);
            this.lidarView.Name = "lidarView";
            this.lidarView.Size = new System.Drawing.Size(300, 300);
            this.lidarView.TabIndex = 3;
            this.lidarView.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(908, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 37);
            this.label1.TabIndex = 4;
            this.label1.Text = "Lidar View";
            // 
            // cleanBtn
            // 
            this.cleanBtn.Location = new System.Drawing.Point(13, 50);
            this.cleanBtn.Name = "cleanBtn";
            this.cleanBtn.Size = new System.Drawing.Size(75, 23);
            this.cleanBtn.TabIndex = 5;
            this.cleanBtn.Text = "Clean";
            this.cleanBtn.UseVisualStyleBackColor = true;
            this.cleanBtn.Click += new System.EventHandler(this.cleanBtn_Click);
            // 
            // SimView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 450);
            this.Controls.Add(this.cleanBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lidarView);
            this.Controls.Add(this.manualCB);
            this.Controls.Add(this.fieldView);
            this.Controls.Add(this.generateBtm);
            this.KeyPreview = true;
            this.Name = "SimView";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SimView_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SimView_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.fieldView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lidarView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button generateBtm;
        private PictureBox fieldView;
        private CheckBox manualCB;
        private System.Windows.Forms.Timer modelUpdateTim;
        private System.Windows.Forms.Timer viewUpdateTim;
        private PictureBox lidarView;
        private Label label1;
        private Button cleanBtn;
    }
}