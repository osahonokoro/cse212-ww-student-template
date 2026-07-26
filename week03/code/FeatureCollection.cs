/// <summary>
/// Classes for deserializing earthquake JSON data from USGS
/// </summary>
public class FeatureCollection
{
    public Feature[] Features { get; set; }
}

public class Feature
{
    public FeatureProperties Properties { get; set; }
}

public class FeatureProperties
{
    public string Place { get; set; }
    public double Mag { get; set; }
}