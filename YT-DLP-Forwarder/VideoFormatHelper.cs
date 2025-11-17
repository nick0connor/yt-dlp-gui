using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YT_DLP_Forwarder;

public class VideoFormatHelper {

    string ID;
    private JsonElement formatJson;

    string? extension, resolution, vcodec, acodec, sizeString;
    double? sizeDouble;
    short? fps;
    VideoTypes videoType; 

    public VideoFormatHelper(JsonElement _formatJson) {
        formatJson = _formatJson;

        InitializeValues();
    }

    private void InitializeValues() {
        ID          = StringFromValue("format_id");
        extension   = StringFromValue("ext");
        resolution  = StringFromValue("resolution");
        fps         = NumberFromValue<short>("fps");
        vcodec      = StringFromValue("vcodec");
        acodec      = StringFromValue("acodec");
        sizeDouble  = NumberFromValue<double>("filesize");

        // Why tf isn't this working ???
        if (vcodec == "none" || vcodec == null) { // No Audio Codec > either Audio Only or Thumbnail
            videoType = (acodec == "none" || acodec == null) ? VideoTypes.Thumbnail : VideoTypes.Audio;
        }
        else {
            videoType = (acodec == "none" || acodec == null) ? VideoTypes.Video : VideoTypes.Both;
        }

        if (sizeDouble == null) {
            sizeString = "N/A";
            return;
        }

        sizeDouble /= 1000000; // Convert bytes to MB
        if (sizeDouble >= 1000) {
            sizeString = Math.Round((sizeDouble.Value/1000.0), 2) + " GB";
        } else {
            sizeString = Math.Round(sizeDouble.Value, 2) + " MB";
        }
        // must do .Value above due to Math.Round not taking nullable objects
        // even tho i check for null right before, the code don't know that
    }

    private string? StringFromValue(string value)
    {
        formatJson.TryGetProperty(value, out JsonElement output);
        return (output.ValueKind == JsonValueKind.Null) ? null : output.ToString();
    }

    private T? NumberFromValue<T>(string value) where T : struct
    {
        formatJson.TryGetProperty(value, out JsonElement output);
        if (output.ValueKind != JsonValueKind.Number) return null;

        // can't cast to ambiguous 'T' -- ie (T)output.GetDouble()
        // have to declare T is a value type (T : struct) and
        // use .ChangeType to output generic object 
        return (T)Convert.ChangeType(output.GetDouble(), typeof(T));
    }

    public string GetID() {
        return ID;
    }

    public VideoTypes GetType()
    {
        return videoType;
    }
    
    public override string ToString() {
        return ID + ", " + extension + ", " + resolution + ", " +
            fps + ", " + sizeString + ", " + vcodec + ", " + acodec + "\n";
    }
}