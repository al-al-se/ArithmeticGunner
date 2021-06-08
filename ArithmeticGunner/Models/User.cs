using System;
using System.ComponentModel.DataAnnotations;
public class User
{
    [Key]
    public string Login {get; set;} = "";

    public int Count {get; set;}

    public int Best {get; set;}

    public double Average {get; set;}
}