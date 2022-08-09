using System;
using System.Net;
using System.IO;
using UnityEngine;

[Serializable]
public class Joke
{
    public bool error;
    public string category;
    public string type;
    public string joke = "There are no jokes for you today. But hasn't your life laughed at you yet?";
    public struct flags
    {
        public bool nsfw;
        public bool religious;
        public bool political;
        public bool racist;
        public bool sexist;
        public bool explic;
    }
    public int id;
    public bool safe;
    public string lang;
}

public enum Category
{
    Programming,
    Miscellaneous,
    Dark,
    Pun,
    Spooky
}

public enum Blacklist
{
    Nsfw,
    Religious,
    Political,
    Racist,
    Sexist,
    Explicit
}

public static class JokeAPI
{
    private static string SetCategory()
    {
        string request = "";

        foreach (Category item in Enum.GetValues(typeof(Category)))
        {
            if (PlayerPrefs.GetInt(item.ToString(), 1) == 1)
            {
                request += item.ToString() + ",";
            }
        }

        return request.Length > 1 ? request.Remove(request.Length - 1) : "Programming,Miscellaneous,Dark,Pun,Spooky";
    }

    private static string SetBlacklist()
    {
        string request = "";

        foreach (Blacklist item in Enum.GetValues(typeof(Blacklist)))
        {
            if (PlayerPrefs.GetInt(item.ToString(), 1) == 1)
            {
                request += item.ToString() + ",";
            }
        }

        return request.Length > 1 ? "blacklistFlags=" + request.ToLower().Remove(request.Length - 1) + "&" : request;
    }

    public static Joke GenerateJoke()
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://v2.jokeapi.dev/joke/" + SetCategory() + "?" + SetBlacklist() + "type=single");

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());

            string json = reader.ReadToEnd();

            return JsonUtility.FromJson<Joke>(json);
        }
        catch (WebException)
        {
            return new Joke();
        }
    }
}
