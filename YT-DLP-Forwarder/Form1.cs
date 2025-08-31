using System.Text.Json;
using System.Threading.Tasks;
using YT_DLP_Forwarder.Properties;

namespace YT_DLP_Forwarder
{
    public partial class Form1 : Form
    {

        private JsonHelper? jsonHelper;
        public Form1()
        {
            InitializeComponent();
        }

        private void ResetPicture()
        {
            if (thumbnail_pic.Image != null) thumbnail_pic.Image.Dispose();

            thumbnail_pic.Image = Image.FromFile
                (Path.Combine(Application.StartupPath, "missing_thumbnail.jpg"));
            thumbnail_pic.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Settings.Default.defaultPath))
            {
                path_textbox.Text = Settings.Default.defaultPath;
            }

            ResetPicture();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Download Button
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(url_box.Text)
                || string.IsNullOrWhiteSpace(path_textbox.Text)
                || (!video_mp4_radio.Checked && !audio_mp3_button.Checked))
            {
                MessageBox.Show("Fully input values!");
                return;
            }

            bool downloadMP4 = video_mp4_radio.Checked;
            String url = url_box.Text;
            String path = path_textbox.Text;

            //MessageBox.Show("You entered " + url);
            String command = "yt-dlp";

            if (downloadMP4)
            {
                command += " -f \"bestvideo[ext=mp4]+bestaudio\"";
            }
            else
            {
                command += " -x --audio-format mp3";
            }

            command += " -P \"" + path + "\"";
            command += " " + url;

            // TODO (replace with yt-dlp direct call)
            System.Diagnostics.Process.Start("cmd.exe", "/c " + command); // opens CMD to run the command
            result_box.Text = command; // displays the command (mostly for debug purposes)

            saveOptionsToFile();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void browse_button_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select directory to save file to...";
                folderDialog.UseDescriptionForTitle = true;
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    path_textbox.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void path_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void saveOptionsToFile()
        {
            Settings.Default.defaultPath = path_textbox.Text;


            Settings.Default.Save();
        }

        // Paste Button 
        private async void button1_Click_1(object sender, EventArgs e)
        {
            ResetPicture();

            // If the clipboard contains non-text data
            if (!Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                MessageBox.Show("No Text Copied!");
                return;
            }

            url_box.Text = "";
            url_box.Paste();

            await DownloadJSON(); 
            ApplyVideoInfo();
        }
        
        private async Task DownloadJSON()
        {
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

        private async void ApplyVideoInfo()
        {
            string? thumbnailURL = GetBestThumbnailURL();

            using var client = new HttpClient();
            byte[] imageBytes = await client.GetByteArrayAsync(thumbnailURL);
            await File.WriteAllBytesAsync
                (Path.Combine(Path.GetTempPath(), "thumbnail.jpg"), imageBytes);

            thumbnail_pic.Image = Image.FromFile
                (Path.Combine(Path.GetTempPath(), "thumbnail.jpg"));
            thumbnail_pic.SizeMode = PictureBoxSizeMode.StretchImage;

            string? vidName = jsonHelper.ReturnResultsFor("title");
            vid_name_label.Text = vidName;
        }

        private string? GetBestThumbnailURL()
        {
            /* YT-DLP contains both a singular "thumbnail" field along with a "thumbnails" list
             * "thumbnails" contains the higher quality versions but might not always exist
             * use "thumbnail" as a backup for such instances */
            
            Stack<JsonElement>? thumbnailStack = new Stack<JsonElement>(jsonHelper.ReturnListFor("thumbnails"));

            JsonElement poppedThumbnail;
            do {
                poppedThumbnail = thumbnailStack.Pop();
            } while (!poppedThumbnail.ToString().Contains(".jpg"));

            if (thumbnailStack.Count > 0)
                return poppedThumbnail.GetProperty("url").GetString();

            return jsonHelper.ReturnResultsFor("thumbnail");
        }

        private void copy_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(result_box.Text))
            {
                return;
            }

            //System.Windows.Forms.Clipboard.SetText(result_box.Text);
            Clipboard.SetText(result_box.Text);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
