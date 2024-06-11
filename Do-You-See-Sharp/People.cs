﻿using System;

public class People
{
    private string _name;
    private string _info;
    private string _initialClaim;
    private string _extraHint;
    private bool _isHintAcquired;

    public People(string name, string info, string initialClaim, string extraHint)
    {
        _name = name;
        _info = info;
        _initialClaim = initialClaim;
        _extraHint = extraHint;
        _isHintAcquired = false;
    }
    public void SetName(string value)
    {
        _name = value;
    }
    public string GetName()
    {
        return _name;
    }
    public string GetInfo()
    {
        return _info;
    }

    public void SetInfo(string value)
    {
        _info = value;
    }

    public string GetInitialClaim()
    {
        return _initialClaim;
    }

    public void SetInitialClaim(string value)
    {
        _initialClaim = value;
    }

    public string GetExtraHint()
    {
        _isHintAcquired = true;
        return _extraHint;
    }

    public void SetExtraHint(string value)
    {
        _extraHint = value;
    }

    public bool IsHintAcquired()
    {
        return _isHintAcquired;
    }
}