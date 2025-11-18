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
        private void InitializeComponent()
        {
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
            format_label = new Label();
            Video = new CheckedListBox();
            Audio = new CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)thumbnail_pic).BeginInit();
            SuspendLayout();
            // 
            // url_box
            // 
            url_box.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            url_box.Location = new Point(14, 36);
            url_box.Margin = new Padding(3, 4, 3, 4);
            url_box.Name = "url_box";
            url_box.Size = new Size(641, 27);
            url_box.TabIndex = 0;
            url_box.KeyDown += url_box_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 12);
            label1.Name = "label1";
            label1.Size = new Size(73, 20);
            label1.TabIndex = 1;
            label1.Text = "Enter URL";
            label1.TextAlign = ContentAlignment.TopCenter;
            label1.Click += label1_Click;
            // 
            // download_button
            // 
            download_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            download_button.Location = new Point(14, 656);
            download_button.Margin = new Padding(3, 4, 3, 4);
            download_button.Name = "download_button";
            download_button.Size = new Size(739, 60);
            download_button.TabIndex = 2;
            download_button.Text = "DOWNLOAD";
            download_button.UseVisualStyleBackColor = true;
            download_button.Click += download_Click;
            // 
            // path_textbox
            // 
            path_textbox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            path_textbox.Location = new Point(14, 617);
            path_textbox.Margin = new Padding(3, 4, 3, 4);
            path_textbox.Name = "path_textbox";
            path_textbox.ReadOnly = true;
            path_textbox.Size = new Size(641, 27);
            path_textbox.TabIndex = 5;
            path_textbox.TextChanged += path_textbox_TextChanged;
            // 
            // browse_button
            // 
            browse_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            browse_button.Location = new Point(662, 617);
            browse_button.Margin = new Padding(3, 4, 3, 4);
            browse_button.Name = "browse_button";
            browse_button.Size = new Size(91, 31);
            browse_button.TabIndex = 6;
            browse_button.Text = "Browse";
            browse_button.UseVisualStyleBackColor = true;
            browse_button.Click += browse_button_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(14, 593);
            label2.Name = "label2";
            label2.Size = new Size(37, 20);
            label2.TabIndex = 7;
            label2.Text = "Path";
            label2.Click += label2_Click;
            // 
            // result_box
            // 
            result_box.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            result_box.Location = new Point(14, 724);
            result_box.Margin = new Padding(3, 4, 3, 4);
            result_box.Name = "result_box";
            result_box.ReadOnly = true;
            result_box.Size = new Size(641, 27);
            result_box.TabIndex = 8;
            // 
            // paste_button
            // 
            paste_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            paste_button.Location = new Point(662, 36);
            paste_button.Margin = new Padding(3, 4, 3, 4);
            paste_button.Name = "paste_button";
            paste_button.Size = new Size(91, 31);
            paste_button.TabIndex = 9;
            paste_button.Text = "Paste";
            paste_button.UseVisualStyleBackColor = true;
            paste_button.Click += paste_Click;
            // 
            // copy_button
            // 
            copy_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            copy_button.Location = new Point(662, 724);
            copy_button.Margin = new Padding(3, 4, 3, 4);
            copy_button.Name = "copy_button";
            copy_button.Size = new Size(91, 31);
            copy_button.TabIndex = 10;
            copy_button.Text = "Copy";
            copy_button.UseVisualStyleBackColor = true;
            copy_button.Click += copy_button_Click;
            // 
            // thumbnail_pic
            // 
            thumbnail_pic.Location = new Point(14, 75);
            thumbnail_pic.Margin = new Padding(3, 4, 3, 4);
            thumbnail_pic.Name = "thumbnail_pic";
            thumbnail_pic.Size = new Size(219, 144);
            thumbnail_pic.TabIndex = 11;
            thumbnail_pic.TabStop = false;
            thumbnail_pic.Click += thumbnail_pic_Click;
            // 
            // vid_name_label
            // 
            vid_name_label.AutoSize = true;
            vid_name_label.Location = new Point(240, 75);
            vid_name_label.Name = "vid_name_label";
            vid_name_label.Size = new Size(0, 20);
            vid_name_label.TabIndex = 12;
            vid_name_label.Click += label3_Click;
            // 
            // format_label
            // 
            format_label.AutoSize = true;
            format_label.Location = new Point(14, 229);
            format_label.Name = "format_label";
            format_label.Size = new Size(62, 20);
            format_label.TabIndex = 15;
            format_label.Text = "Formats";
            format_label.Click += label3_Click_1;
            // 
            // Video
            // 
            Video.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Video.CheckOnClick = true;
            Video.FormattingEnabled = true;
            Video.Location = new Point(14, 252);
            Video.Name = "Video";
            Video.Size = new Size(393, 334);
            Video.TabIndex = 16;
            Video.SelectedIndexChanged += Video_SelectedIndexChanged;
            // 
            // Audio
            // 
            Audio.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Audio.CheckOnClick = true;
            Audio.FormattingEnabled = true;
            Audio.Location = new Point(413, 252);
            Audio.Name = "Audio";
            Audio.Size = new Size(340, 334);
            Audio.TabIndex = 17;
            Audio.SelectedIndexChanged += Audio_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(767, 771);
            Controls.Add(Audio);
            Controls.Add(Video);
            Controls.Add(format_label);
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
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "YT-DLP GUI";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)thumbnail_pic).EndInit();
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
        private Label format_label;
        private CheckedListBox Video;
        private CheckedListBox Audio;
    }
}
