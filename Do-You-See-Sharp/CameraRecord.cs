using System;

public sealed class CameraRecord
{
    private string _name;
    private string _location;
    private string _record;

    public string Name
    {
        get => _name;
    }

    public string Location
    {
        get => _location;
    }

    public string Record
    {
        get => _record;
    }

    public CameraRecord(string name, string location, string record)
    {
        _name = name;
        _location = location;
        _record = record;
    }
}
