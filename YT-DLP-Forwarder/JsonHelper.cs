using System;
using System.IO;
using System.Text.Json;

public class JsonHelper
{
	private string file;
	private Dictionary<string, JsonElement>? data;

	public JsonHelper(string _file)
	{
		file = _file;
		LinkJSON();
	}

	private void LinkJSON()
	{
        try
        {
            string json = File.ReadAllText(file);
            data = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);

            MessageBox.Show("read file!"); // Debug
        }
        catch (Exception ex)
        {
            MessageBox.Show("The file could not be read!\n" + ex.Message);
        }
    }

	public string? ReturnResultsFor(string label)
	{
		if (data == null) return null;

		data.TryGetValue(label, out JsonElement output);
		return output.ToString();
	}
}
