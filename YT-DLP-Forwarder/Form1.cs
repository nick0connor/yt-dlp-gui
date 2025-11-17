using System.Drawing.Text;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using YT_DLP_Forwarder.Properties;

namespace YT_DLP_Forwarder
{
    public partial class Form1 : Form {

        private JsonHelper? jsonHelper;
        private string? highestQualityThumbnailURL;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            CheckForYTDLP();

            if (!string.IsNullOrWhiteSpace(Settings.Default.defaultPath)) {
                path_textbox.Text = Settings.Default.defaultPath;
            }

            ResetPicture();
        }

        private void CheckForYTDLP() {
            if (File.Exists(
                Path.Combine(Environment.CurrentDirectory, "yt-dlp.exe"))) return;

            if (MessageBox.Show(
                "YT-DLP not detected!\n\nPlease download yt-dlp.exe and put it in the same directory as this exe." +
                    "\nWould you like to open the download page now?",
                "ERROR", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk
                ) == DialogResult.Yes) {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = "https://github.com/yt-dlp/yt-dlp/releases";
                proc.Start();
            }

            this.Close();
        }

        private void ResetPicture() {
            if (thumbnail_pic.Image != null) thumbnail_pic.Image.Dispose();

            // Temporary Measure for if the Missing Thumbnail File is Missing
            // No, the irony is not lost on me
            try {
                thumbnail_pic.Image = Image.FromFile
                    (Path.Combine(Application.StartupPath, "missing_thumbnail.jpg"));
            } catch (Exception ex) { }

            thumbnail_pic.SizeMode = PictureBoxSizeMode.StretchImage;
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e) {

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) {

        }

        private void download_Click(object sender, EventArgs e) {
            //if (string.IsNullOrWhiteSpace(url_box.Text)
            //    || string.IsNullOrWhiteSpace(path_textbox.Text)
            //    || (!video_mp4_radio.Checked && !audio_mp3_button.Checked)) {
            //    MessageBox.Show("Fully input values!");
            //    return;
            //}

            //bool downloadMP4 = video_mp4_radio.Checked;
            //String url = url_box.Text;
            //String path = path_textbox.Text;

            ////MessageBox.Show("You entered " + url);
            //String command = "yt-dlp";

            //if (downloadMP4) {
            //    command += " -f \"bestvideo[ext=mp4]+bestaudio\"";
            //} else {
            //    command += " -x --audio-format mp3";
            //}

            //command += " -P \"" + path + "\"";
            //command += " " + url;

            //// TODO (replace with yt-dlp direct call)
            //System.Diagnostics.Process.Start("cmd.exe", "/c " + command); // opens CMD to run the command
            //result_box.Text = command; // displays the command (mostly for debug purposes)

            saveOptionsToFile();
        }

        private void label2_Click(object sender, EventArgs e) {

        }

        private void browse_button_Click(object sender, EventArgs e) {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog()) {
                folderDialog.Description = "Select directory to save file to...";
                folderDialog.UseDescriptionForTitle = true;
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK) {
                    path_textbox.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void saveOptionsToFile() {
            Settings.Default.defaultPath = path_textbox.Text;
            Settings.Default.Save();
        }


        //********************* VIDEO ENTRY *********************//
        private void paste_Click(object sender, EventArgs e) {
            ResetPicture();

            // If the clipboard contains non-text data
            if (!Clipboard.GetDataObject().GetDataPresent(DataFormats.Text)) {
                MessageBox.Show("No Text Copied!");
                return;
            }

            url_box.Text = "";
            url_box.Paste();

            VideoUrlEntered();
        }
        private void url_box_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                VideoUrlEntered();
            }
        }
        private async void VideoUrlEntered() {
            await DownloadJSON();
            ApplyVideoInfo();
        }

        private async Task DownloadJSON() {
            var process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "yt-dlp.exe";
            process.StartInfo.Arguments =
                "-o " + Path.Combine(Path.GetTempPath(), "cv") + " --write-info-json --skip-download " + url_box.Text;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            await process.WaitForExitAsync();
            jsonHelper = new JsonHelper
                (Path.Combine(Path.GetTempPath(), "cv.info.json"));
        }

        private void ApplyVideoInfo() {
            string? vidName = jsonHelper.ReturnResultsFor("title");
            if (vidName != null) vid_name_label.Text = vidName;

            ResetPicture();
            LoadBestAvailableThumbnail();
            thumbnail_pic.SizeMode = PictureBoxSizeMode.StretchImage;


            List<JsonElement>? formatsList = jsonHelper.ReturnListFor("formats");
            if (formatsList == null) {
                MessageBox.Show("Unable to retrieve available formats!");
                return;
            }
            ListAvailableFormats(formatsList);


            // KILL THE EVIL JSON FILE AFTER IT STOPS BEING USEFUL!!!
            //File.Delete(Path.Combine(Path.GetTempPath(), "cv.info.json"));
        }

        private void ListAvailableFormats(List<JsonElement> formats) {

            //string debugLabelString = "ID, EXT, RES, FPS, SIZE, VCODEC, ACODEC\n";
            //List<VideoFormatHelper> formatList = new List<VideoFormatHelper>(); // Put this outside the function so u can use ID later

            //foreach (JsonElement format in formats) {
            //    //if (GetValueString(format, "format_note") == "storyboard") continue;

            //    formatList.Add(new VideoFormatHelper(format));

            //    debugLabelString += formatList.Last().ToString();
            //}

            //debug_format_label.Text = debugLabelString;

            List<VideoFormatHelper> videoList = new List<VideoFormatHelper>(), audioList = new List<VideoFormatHelper>(), thumbnailList = new List<VideoFormatHelper>();
            string columnLabels = "ID, EXT, RES, FPS, SIZE, VCODEC, ACODEC\n";

            foreach (JsonElement format in formats) {
                VideoFormatHelper curFormat = new VideoFormatHelper(format);

                switch (curFormat.GetType()){
                    case VideoTypes.Both:
                    case VideoTypes.Video:
                        videoList.Add(curFormat);
                        break;
                    case VideoTypes.Audio:
                        audioList.Add(curFormat);
                        break;
                    case VideoTypes.Thumbnail:
                        thumbnailList.Add(curFormat);
                        break;
                }
            }

            string s = "";
            foreach (VideoFormatHelper f in videoList)
            {
                s += f.ToString();
            }
            MessageBox.Show("VIDEOS\n\n" + columnLabels + "\n" + s);

            s = "";
            foreach (VideoFormatHelper f in audioList)
            {
                s += f.ToString();
            }
            MessageBox.Show("AUDIO ONLYs\n\n" + columnLabels + "\n" + s);

            s = "";
            foreach (VideoFormatHelper f in thumbnailList)
            {
                s += f.ToString();
            }
            MessageBox.Show("THUMBNAILS\n\n" + columnLabels + "\n" + s);
        }

        // hardcoding right now while testing
        private static readonly List<string> THUMBNAIL_TITLES =
            new() { "hq720.jpg", "maxresdefault.jpg" };
        private void LoadBestAvailableThumbnail() {
            List<JsonElement> thumbnailJsons =
                ReturnMatches<JsonElement>(jsonHelper.ReturnListFor("thumbnails"), THUMBNAIL_TITLES);

            bool success = false;
            // reverse order here due to higher quality thumbnails being at the bottom of the list
            for (int i = thumbnailJsons.Count - 1; i >= 0 && !success; i--) {
                try {
                    highestQualityThumbnailURL = thumbnailJsons[i].GetProperty("url").GetString();
                    thumbnail_pic.Load(highestQualityThumbnailURL);
                    success = true;
                } catch (Exception ex) { Console.WriteLine(ex); }
            }

            if (!success) {
                highestQualityThumbnailURL = jsonHelper.ReturnResultsFor("thumbnail");
                thumbnail_pic.Load(highestQualityThumbnailURL);
            }
        }

        private List<T> ReturnMatches<T>(List<T> inputList, List<string> filterList) {
            List<T> outputList = new List<T>();

            foreach (T listItem in inputList) {
                if (listItem == null) continue;
                foreach (string filter in filterList) {
                    if (listItem.ToString().Contains(filter)) {
                        outputList.Add(listItem); break;
                    }
                }
            }
            return outputList;
        }

        private void copy_button_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(result_box.Text)) {
                return;
            }

            //System.Windows.Forms.Clipboard.SetText(result_box.Text);
            Clipboard.SetText(result_box.Text);
        }

        private async void thumbnail_pic_Click(object sender, EventArgs e) {
            if (highestQualityThumbnailURL != null) {

                // remove all the illegal characters (< > : " / \ | ? *)
                string fileName = jsonHelper.ReturnResultsFor("title") + ".jpg";
                foreach (char c in Path.GetInvalidFileNameChars()) {
                    fileName = fileName.Replace(c.ToString(), "");
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPEG Image|*.jpg";
                saveFileDialog.Title = "Save an Image File";
                saveFileDialog.FileName = fileName;

                if (saveFileDialog.ShowDialog(this) == DialogResult.OK) {

                    using var client = new HttpClient();
                    byte[] imageBytes = await client.GetByteArrayAsync(highestQualityThumbnailURL);
                    await File.WriteAllBytesAsync
                        (saveFileDialog.FileName, imageBytes);
                }
            }
        }

        







        //********************* UNUSED FUNCTIONS *********************//
        private void label1_Click(object sender, EventArgs e) {

        }

        private void path_textbox_TextChanged(object sender, EventArgs e) {

        }

        private void label3_Click(object sender, EventArgs e) {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e) {

        }

        private void label3_Click_1(object sender, EventArgs e) {

        }
    }
}
