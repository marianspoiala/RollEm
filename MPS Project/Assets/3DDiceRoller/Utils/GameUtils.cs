using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;

class GameUtils : GameUtilsIF
{

    private void saveUsersToFile(List<UserDTO> users, String fileName)
    {
        string json = JsonConvert.SerializeObject(users, Formatting.Indented);
        // serialize JSON to a string and then write string to a file
        File.WriteAllText(@fileName, JsonConvert.SerializeObject(users, Formatting.Indented));

        // serialize JSON directly to a file
        using (StreamWriter file = File.CreateText(@fileName))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, users);
        }
    }

    private List<UserDTO> loadUsersFromFile(String fileName)
    {
        List<UserDTO> users = new List<UserDTO>();

        // read file into a string and deserialize JSON to a type
        /*try {
            users = JsonConvert.DeserializeObject<List<UserDTO>>(File.ReadAllText(@fileName));
        }
        catch(FileNotFoundException ex)
        {
            //Console("FileNotFound : " + fileName);
        }*/

        // deserialize JSON directly from a file
        try {
            using (StreamReader file = File.OpenText(@fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                users = (List<UserDTO>)serializer.Deserialize(file, typeof(List<UserDTO>));
            }
        }
        catch(FileNotFoundException ex)
        {

        }

        return users;
    }

    public void startTimer(Double seconds)
    {
        Timer gameTimer = new Timer();
        gameTimer.Elapsed += new ElapsedEventHandler(TimerGameEvent);
        gameTimer.Interval = seconds;
        gameTimer.Enabled = true;
    }

    // Specify what you want to happen when the Elapsed event is raised.
    private static void TimerGameEvent(object source, ElapsedEventArgs e)
    {
        Console.WriteLine("Hello World!");
    }

    public void saveUsersDetails(List<UserDTO> users, String fileName)
    {
        saveUsersToFile(users, fileName);
    }

    public List<UserDTO> loadUsersDetails(String fileName)
    {
        return loadUsersFromFile(fileName);
    }
}
