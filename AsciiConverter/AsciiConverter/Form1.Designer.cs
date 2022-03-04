
namespace AsciiConverter
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
            this.file_select_dialog = new System.Windows.Forms.OpenFileDialog();
            this.file_path_text_box = new System.Windows.Forms.TextBox();
            this.converted_output = new System.Windows.Forms.RichTextBox();
            this.start_convert_btn = new System.Windows.Forms.Button();
            this.select_file_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.image_display = new System.Windows.Forms.PictureBox();
            this.live_view_btn = new System.Windows.Forms.Button();
            this.record_button = new System.Windows.Forms.Button();
            this.clr_btn = new System.Windows.Forms.Button();
            this.folder_select_dialog = new System.Windows.Forms.FolderBrowserDialog();
            this.frame_cut_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.image_display)).BeginInit();
            this.SuspendLayout();
            // 
            // file_select_dialog
            // 
            this.file_select_dialog.FileName = "file_select";
            this.file_select_dialog.Filter = "Image File|*.jpg|Image File|*.jpeg|Image File|*.png|Video File|*.mp4";
            // 
            // file_path_text_box
            // 
            this.file_path_text_box.Enabled = false;
            this.file_path_text_box.Location = new System.Drawing.Point(11, 410);
            this.file_path_text_box.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.file_path_text_box.Name = "file_path_text_box";
            this.file_path_text_box.Size = new System.Drawing.Size(279, 22);
            this.file_path_text_box.TabIndex = 0;
            // 
            // converted_output
            // 
            this.converted_output.BackColor = System.Drawing.SystemColors.MenuText;
            this.converted_output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.converted_output.Font = new System.Drawing.Font("Microsoft Sans Serif", 3F);
            this.converted_output.ForeColor = System.Drawing.SystemColors.Window;
            this.converted_output.Location = new System.Drawing.Point(340, 62);
            this.converted_output.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.converted_output.Name = "converted_output";
            this.converted_output.ReadOnly = true;
            this.converted_output.Size = new System.Drawing.Size(550, 425);
            this.converted_output.TabIndex = 1;
            this.converted_output.Text = "";
            // 
            // start_convert_btn
            // 
            this.start_convert_btn.BackColor = System.Drawing.Color.Chartreuse;
            this.start_convert_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.start_convert_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.start_convert_btn.Location = new System.Drawing.Point(11, 438);
            this.start_convert_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.start_convert_btn.Name = "start_convert_btn";
            this.start_convert_btn.Size = new System.Drawing.Size(84, 27);
            this.start_convert_btn.TabIndex = 2;
            this.start_convert_btn.Text = "Start Scan";
            this.start_convert_btn.UseVisualStyleBackColor = false;
            this.start_convert_btn.Click += new System.EventHandler(this.start_convert_click);
            // 
            // select_file_btn
            // 
            this.select_file_btn.BackColor = System.Drawing.Color.Red;
            this.select_file_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.select_file_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.select_file_btn.Location = new System.Drawing.Point(101, 438);
            this.select_file_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.select_file_btn.Name = "select_file_btn";
            this.select_file_btn.Size = new System.Drawing.Size(85, 27);
            this.select_file_btn.TabIndex = 3;
            this.select_file_btn.Text = "Select File";
            this.select_file_btn.UseVisualStyleBackColor = false;
            this.select_file_btn.Click += new System.EventHandler(this.select_file_click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 390);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Video / Image File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "ASCII Converter";
            // 
            // image_display
            // 
            this.image_display.BackColor = System.Drawing.SystemColors.Desktop;
            this.image_display.Location = new System.Drawing.Point(916, 62);
            this.image_display.Margin = new System.Windows.Forms.Padding(4);
            this.image_display.Name = "image_display";
            this.image_display.Size = new System.Drawing.Size(550, 425);
            this.image_display.TabIndex = 6;
            this.image_display.TabStop = false;
            // 
            // live_view_btn
            // 
            this.live_view_btn.BackColor = System.Drawing.Color.Red;
            this.live_view_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.live_view_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.live_view_btn.ForeColor = System.Drawing.Color.Black;
            this.live_view_btn.Location = new System.Drawing.Point(203, 438);
            this.live_view_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.live_view_btn.Name = "live_view_btn";
            this.live_view_btn.Size = new System.Drawing.Size(85, 27);
            this.live_view_btn.TabIndex = 8;
            this.live_view_btn.Text = "Live View";
            this.live_view_btn.UseVisualStyleBackColor = false;
            this.live_view_btn.Click += new System.EventHandler(this.live_view_btn_Click);
            // 
            // record_button
            // 
            this.record_button.BackColor = System.Drawing.Color.Red;
            this.record_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.record_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.record_button.Location = new System.Drawing.Point(101, 478);
            this.record_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.record_button.Name = "record_button";
            this.record_button.Size = new System.Drawing.Size(85, 27);
            this.record_button.TabIndex = 9;
            this.record_button.Text = "Record";
            this.record_button.UseVisualStyleBackColor = false;
            this.record_button.Click += new System.EventHandler(this.record_btn_click);
            // 
            // clr_btn
            // 
            this.clr_btn.BackColor = System.Drawing.Color.Red;
            this.clr_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clr_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clr_btn.Location = new System.Drawing.Point(11, 478);
            this.clr_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clr_btn.Name = "clr_btn";
            this.clr_btn.Size = new System.Drawing.Size(84, 27);
            this.clr_btn.TabIndex = 10;
            this.clr_btn.Text = "Clear";
            this.clr_btn.UseVisualStyleBackColor = false;
            this.clr_btn.Click += new System.EventHandler(this.clr_btn_click);
            // 
            // frame_cut_btn
            // 
            this.frame_cut_btn.BackColor = System.Drawing.Color.Red;
            this.frame_cut_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.frame_cut_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frame_cut_btn.Location = new System.Drawing.Point(203, 478);
            this.frame_cut_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.frame_cut_btn.Name = "frame_cut_btn";
            this.frame_cut_btn.Size = new System.Drawing.Size(85, 27);
            this.frame_cut_btn.TabIndex = 11;
            this.frame_cut_btn.Text = "Cut";
            this.frame_cut_btn.UseVisualStyleBackColor = false;
            this.frame_cut_btn.Click += new System.EventHandler(this.frame_cut_btn_click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1477, 512);
            this.Controls.Add(this.frame_cut_btn);
            this.Controls.Add(this.clr_btn);
            this.Controls.Add(this.record_button);
            this.Controls.Add(this.live_view_btn);
            this.Controls.Add(this.image_display);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.select_file_btn);
            this.Controls.Add(this.start_convert_btn);
            this.Controls.Add(this.converted_output);
            this.Controls.Add(this.file_path_text_box);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "ASCII Media Converter";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.image_display)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog file_select_dialog;
        private System.Windows.Forms.TextBox file_path_text_box;
        private System.Windows.Forms.RichTextBox converted_output;
        private System.Windows.Forms.Button start_convert_btn;
        private System.Windows.Forms.Button select_file_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox image_display;
        private System.Windows.Forms.Button live_view_btn;
        private System.Windows.Forms.Button record_button;
        private System.Windows.Forms.Button clr_btn;
        private System.Windows.Forms.FolderBrowserDialog folder_select_dialog;
        private System.Windows.Forms.Button frame_cut_btn;
    }
}

