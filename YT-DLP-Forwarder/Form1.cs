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

        private void ResetPicture() {
            if (thumbnail_pic.Image != null) thumbnail_pic.Image.Dispose();

            thumbnail_pic.Image = Image.FromFile
                (Path.Combine(Application.StartupPath, "missing_thumbnail.jpg"));
            thumbnail_pic.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Form1_Load(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(Settings.Default.defaultPath)) {
                path_textbox.Text = Settings.Default.defaultPath;
            }

            ResetPicture();
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) {

        }

        // Download Button
        private void button1_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(url_box.Text)
                || string.IsNullOrWhiteSpace(path_textbox.Text)
                || (!video_mp4_radio.Checked && !audio_mp3_button.Checked)) {
                MessageBox.Show("Fully input values!");
                return;
            }

            bool downloadMP4 = video_mp4_radio.Checked;
            String url = url_box.Text;
            String path = path_textbox.Text;

            //MessageBox.Show("You entered " + url);
            String command = "yt-dlp";

            if (downloadMP4) {
                command += " -f \"bestvideo[ext=mp4]+bestaudio\"";
            } else {
                command += " -x --audio-format mp3";
            }

            command += " -P \"" + path + "\"";
            command += " " + url;

            // TODO (replace with yt-dlp direct call)
            System.Diagnostics.Process.Start("cmd.exe", "/c " + command); // opens CMD to run the command
            result_box.Text = command; // displays the command (mostly for debug purposes)

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

        private void path_textbox_TextChanged(object sender, EventArgs e) {

        }

        private void saveOptionsToFile() {
            Settings.Default.defaultPath = path_textbox.Text;


            Settings.Default.Save();
        }

        // Paste Button 
        private void button1_Click_1(object sender, EventArgs e) {
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

        private void label3_Click(object sender, EventArgs e) {

        }

        private async void thumbnail_pic_Click(object sender, EventArgs e) {
            if (highestQualityThumbnailURL != null) {
                string? pathToSave = null;

                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog()) {
                    folderDialog.Description = "Select directory to save thumbnail to...";
                    folderDialog.UseDescriptionForTitle = true;
                    folderDialog.ShowNewFolderButton = true;

                    if (folderDialog.ShowDialog() == DialogResult.OK) {
                        pathToSave = folderDialog.SelectedPath;
                    }
                }

                if (pathToSave == null) return;

                using var client = new HttpClient();
                byte[] imageBytes = await client.GetByteArrayAsync(highestQualityThumbnailURL);
                await File.WriteAllBytesAsync
                    (Path.Combine(pathToSave, jsonHelper.ReturnResultsFor("title") + ".jpg"), imageBytes);
            }
        }

        private void url_box_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                VideoUrlEntered();
            }
        }
    }
}
