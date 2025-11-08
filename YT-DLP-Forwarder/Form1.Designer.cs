namespace YT_DLP_Forwarder
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
        private void InitializeComponent() {
            url_box = new TextBox();
            label1 = new Label();
            download_button = new Button();
            path_textbox = new TextBox();
            browse_button = new Button();
            label2 = new Label();
            result_box = new TextBox();
            paste_button = new Button();
            copy_button = new Button();
            thumbnail_pic = new PictureBox();
            vid_name_label = new Label();
            vScrollBar1 = new VScrollBar();
            format_label = new Label();
            panel1 = new Panel();
            debug_format_label = new Label();
            ((System.ComponentModel.ISupportInitialize)thumbnail_pic).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // url_box
            // 
            url_box.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            url_box.Location = new Point(12, 27);
            url_box.Name = "url_box";
            url_box.Size = new Size(561, 23);
            url_box.TabIndex = 0;
            url_box.KeyDown += url_box_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 1;
            label1.Text = "Enter URL";
            label1.TextAlign = ContentAlignment.TopCenter;
            label1.Click += label1_Click;
            // 
            // download_button
            // 
            download_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            download_button.Location = new Point(12, 492);
            download_button.Name = "download_button";
            download_button.Size = new Size(647, 45);
            download_button.TabIndex = 2;
            download_button.Text = "DOWNLOAD";
            download_button.UseVisualStyleBackColor = true;
            download_button.Click += download_Click;
            // 
            // path_textbox
            // 
            path_textbox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            path_textbox.Location = new Point(12, 463);
            path_textbox.Name = "path_textbox";
            path_textbox.ReadOnly = true;
            path_textbox.Size = new Size(561, 23);
            path_textbox.TabIndex = 5;
            path_textbox.TextChanged += path_textbox_TextChanged;
            // 
            // browse_button
            // 
            browse_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            browse_button.Location = new Point(579, 463);
            browse_button.Name = "browse_button";
            browse_button.Size = new Size(80, 23);
            browse_button.TabIndex = 6;
            browse_button.Text = "Browse";
            browse_button.UseVisualStyleBackColor = true;
            browse_button.Click += browse_button_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(12, 445);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 7;
            label2.Text = "Path";
            label2.Click += label2_Click;
            // 
            // result_box
            // 
            result_box.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            result_box.Location = new Point(12, 543);
            result_box.Name = "result_box";
            result_box.ReadOnly = true;
            result_box.Size = new Size(561, 23);
            result_box.TabIndex = 8;
            // 
            // paste_button
            // 
            paste_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            paste_button.Location = new Point(579, 27);
            paste_button.Name = "paste_button";
            paste_button.Size = new Size(80, 23);
            paste_button.TabIndex = 9;
            paste_button.Text = "Paste";
            paste_button.UseVisualStyleBackColor = true;
            paste_button.Click += paste_Click;
            // 
            // copy_button
            // 
            copy_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            copy_button.Location = new Point(579, 543);
            copy_button.Name = "copy_button";
            copy_button.Size = new Size(80, 23);
            copy_button.TabIndex = 10;
            copy_button.Text = "Copy";
            copy_button.UseVisualStyleBackColor = true;
            copy_button.Click += copy_button_Click;
            // 
            // thumbnail_pic
            // 
            thumbnail_pic.Location = new Point(12, 56);
            thumbnail_pic.Name = "thumbnail_pic";
            thumbnail_pic.Size = new Size(192, 108);
            thumbnail_pic.TabIndex = 11;
            thumbnail_pic.TabStop = false;
            thumbnail_pic.Click += thumbnail_pic_Click;
            // 
            // vid_name_label
            // 
            vid_name_label.AutoSize = true;
            vid_name_label.Location = new Point(210, 56);
            vid_name_label.Name = "vid_name_label";
            vid_name_label.Size = new Size(0, 15);
            vid_name_label.TabIndex = 12;
            vid_name_label.Click += label3_Click;
            // 
            // vScrollBar1
            // 
            vScrollBar1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            vScrollBar1.Location = new Point(642, 192);
            vScrollBar1.Name = "vScrollBar1";
            vScrollBar1.Size = new Size(17, 240);
            vScrollBar1.TabIndex = 14;
            vScrollBar1.Scroll += vScrollBar1_Scroll;
            // 
            // format_label
            // 
            format_label.AutoSize = true;
            format_label.Location = new Point(12, 174);
            format_label.Name = "format_label";
            format_label.Size = new Size(50, 15);
            format_label.TabIndex = 15;
            format_label.Text = "Formats";
            format_label.Click += label3_Click_1;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(debug_format_label);
            panel1.Location = new Point(12, 192);
            panel1.Name = "panel1";
            panel1.Size = new Size(627, 240);
            panel1.TabIndex = 13;
            // 
            // debug_format_label
            // 
            debug_format_label.AutoSize = true;
            debug_format_label.Location = new Point(12, 13);
            debug_format_label.Name = "debug_format_label";
            debug_format_label.Size = new Size(38, 15);
            debug_format_label.TabIndex = 0;
            debug_format_label.Text = "label3";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(671, 578);
            Controls.Add(format_label);
            Controls.Add(vScrollBar1);
            Controls.Add(panel1);
            Controls.Add(vid_name_label);
            Controls.Add(thumbnail_pic);
            Controls.Add(copy_button);
            Controls.Add(paste_button);
            Controls.Add(result_box);
            Controls.Add(label2);
            Controls.Add(browse_button);
            Controls.Add(path_textbox);
            Controls.Add(download_button);
            Controls.Add(label1);
            Controls.Add(url_box);
            Name = "Form1";
            Text = "YT-DLP GUI";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)thumbnail_pic).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox url_box;
        private Label label1;
        private Button download_button;
        private TextBox path_textbox;
        private Button browse_button;
        private Label label2;
        private TextBox result_box;
        private Button paste_button;
        private Button copy_button;
        private PictureBox thumbnail_pic;
        private Label vid_name_label;
        private VScrollBar vScrollBar1;
        private Label format_label;
        private Panel panel1;
        private Label debug_format_label;
    }
}
