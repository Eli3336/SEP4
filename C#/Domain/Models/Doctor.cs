﻿namespace Domain.Models;

public class Doctor : User
{
    public List<Request> Requests { get; set; }

    public Doctor(string name, string password, string phoneNumber)
        : base(name, password, phoneNumber, "doctor")
    {
        Requests = new List<Request>();
    }
    
    public Doctor(string name, string phoneNumber)
        : base(name, "0000", phoneNumber, "doctor" )
    {
        Requests = new List<Request>();
    }

    public Doctor():base("bob", "0000", "00000000", "doctor" )
    {
        
        Requests = new List<Request>();
    }
}