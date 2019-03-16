namespace BryceGrahamProject7a
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btn_NewGame = new System.Windows.Forms.Button();
            this.btn_Settings = new System.Windows.Forms.Button();
            this.pctr_MainLogo = new System.Windows.Forms.PictureBox();
            this.btn_Credits = new System.Windows.Forms.Button();
            this.controlsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pctr_MainLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_NewGame
            // 
            this.btn_NewGame.Location = new System.Drawing.Point(293, 378);
            this.btn_NewGame.Name = "btn_NewGame";
            this.btn_NewGame.Size = new System.Drawing.Size(75, 23);
            this.btn_NewGame.TabIndex = 0;
            this.btn_NewGame.Text = "New Game";
            this.btn_NewGame.UseVisualStyleBackColor = true;
            this.btn_NewGame.Click += new System.EventHandler(this.btn_NewGame_Click);
            // 
            // btn_Settings
            // 
            this.btn_Settings.Location = new System.Drawing.Point(212, 378);
            this.btn_Settings.Name = "btn_Settings";
            this.btn_Settings.Size = new System.Drawing.Size(75, 23);
            this.btn_Settings.TabIndex = 1;
            this.btn_Settings.Text = "Settings";
            this.btn_Settings.UseVisualStyleBackColor = true;
            this.btn_Settings.Click += new System.EventHandler(this.btn_Settings_Click);
            // 
            // pctr_MainLogo
            // 
            this.pctr_MainLogo.Image = ((System.Drawing.Image)(resources.GetObject("pctr_MainLogo.Image")));
            this.pctr_MainLogo.Location = new System.Drawing.Point(12, 155);
            this.pctr_MainLogo.MinimumSize = new System.Drawing.Size(600, 100);
            this.pctr_MainLogo.Name = "pctr_MainLogo";
            this.pctr_MainLogo.Size = new System.Drawing.Size(600, 100);
            this.pctr_MainLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pctr_MainLogo.TabIndex = 2;
            this.pctr_MainLogo.TabStop = false;
            // 
            // btn_Credits
            // 
            this.btn_Credits.Location = new System.Drawing.Point(293, 407);
            this.btn_Credits.Name = "btn_Credits";
            this.btn_Credits.Size = new System.Drawing.Size(75, 23);
            this.btn_Credits.TabIndex = 3;
            this.btn_Credits.Text = "Credits";
            this.btn_Credits.UseVisualStyleBackColor = true;
            this.btn_Credits.Click += new System.EventHandler(this.btn_Credits_Click);
            // 
            // controlsButton
            // 
            this.controlsButton.Location = new System.Drawing.Point(212, 407);
            this.controlsButton.Name = "controlsButton";
            this.controlsButton.Size = new System.Drawing.Size(75, 23);
            this.controlsButton.TabIndex = 4;
            this.controlsButton.Text = "Controls";
            this.controlsButton.UseVisualStyleBackColor = true;
            this.controlsButton.Click += new System.EventHandler(this.controlsButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.controlsButton);
            this.Controls.Add(this.btn_Credits);
            this.Controls.Add(this.btn_Settings);
            this.Controls.Add(this.btn_NewGame);
            this.Controls.Add(this.pctr_MainLogo);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MainForm";
            this.Text = "Astaroids";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pctr_MainLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_NewGame;
        private System.Windows.Forms.Button btn_Settings;
        private System.Windows.Forms.PictureBox pctr_MainLogo;
        private System.Windows.Forms.Button btn_Credits;
        private System.Windows.Forms.Button controlsButton;
    }
}

