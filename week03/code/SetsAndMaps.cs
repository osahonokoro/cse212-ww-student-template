using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // Create a set with all words for O(1) lookup
        var wordSet = new HashSet<string>(words);
        var result = new List<string>();

        foreach (string word in words)
        {
            // Reverse the two-letter word
            string reversed = word[1].ToString() + word[0].ToString();
            
            // Check if reversed exists AND avoid duplicates
            // Compare alphabetically: only add if word < reversed
            if (wordSet.Contains(reversed) && string.Compare(word, reversed) < 0)
            {
                result.Add($"{word} & {reversed}");
            }
        }

        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// 
    /// NOTE: In the actual census.txt file, the degree is in column 3 (index 3).
    /// The documentation says column 4 (0-indexed), but the file has it at index 3.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>Dictionary of degree names and counts</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            
            // ✅ FIXED: Degree is in column 3 (index 3)
            if (fields.Length > 3)
            {
                string degree = fields[3].Trim();
                
                if (!string.IsNullOrEmpty(degree))
                {
                    degrees.TryGetValue(degree, out int count);
                    degrees[degree] = count + 1;
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Remove spaces and convert to lowercase
        string clean1 = word1.Replace(" ", "").ToLower();
        string clean2 = word2.Replace(" ", "").ToLower();

        // If lengths differ, they can't be anagrams
        if (clean1.Length != clean2.Length)
            return false;

        // Count letter frequencies in the first word
        var letterCounts = new Dictionary<char, int>();

        foreach (char c in clean1)
        {
            letterCounts.TryGetValue(c, out int count);
            letterCounts[c] = count + 1;
        }

        // Check against the second word
        foreach (char c in clean2)
        {
            if (!letterCounts.TryGetValue(c, out int count) || count == 0)
                return false;
            letterCounts[c] = count - 1;
        }

        // All counts should be zero
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        var result = new List<string>();
        
        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                if (feature?.Properties != null)
                {
                    string place = feature.Properties.Place ?? "Unknown location";
                    double mag = feature.Properties.Mag;
                    result.Add($"{place} - Mag {mag:F2}");
                }
            }
        }

        return result.ToArray();
    }
}