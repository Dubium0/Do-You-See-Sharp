using System;

public sealed class People
{
    private string _name;
    private string _info;
    private string _initialClaim;
    private string _extraHint;
    private bool _isHintAcquired;
    
    public string Name
    {
        get => _name;
    }

    public string Info
    {
        get => _info; 
    }
    public string InitialClaim
    {
        get =>  _initialClaim; 
    }
    public string ExtraHint
    {
        get 
        {
            if (_isHintAcquired)
            {
                Console.WriteLine("Hint is already acquired");
            }
            _isHintAcquired = true;
           
            return _extraHint; 
        }

    }

    public bool IsHintAcquired
    {
        get => _isHintAcquired;
    }


    public People(string name, string info, string initialClaim, string extraHint)
    {
        _name = name;
        _info = info;
        _initialClaim = initialClaim;
        _extraHint = extraHint;
        _isHintAcquired = false;
    }
}
