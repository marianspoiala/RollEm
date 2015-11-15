﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[JsonObject(MemberSerialization.OptIn)]
public class UserDTO
{
    [JsonProperty]
    private String name;

    [JsonProperty]
    private Int32 score;

    public UserDTO() { }

    [JsonConstructor]
    public UserDTO(String name, Int32 score)
    {
        this.score = score;
        this.name = name;
    }

    public void SetScore(Int32 score)
    {
        this.score = score;
    }

    public Int32 GetScore()
    {
        return this.score;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public string GetName()
    {
        return this.name;
    }

}
