using Newtonsoft.Json;

namespace SportsDashboard.Models
{
    public class CompetitionMatches
    {
        [JsonProperty("competition", NullValueHandling = NullValueHandling.Ignore)]
        public Competition competition { get; set; }

        [JsonProperty("matches", NullValueHandling = NullValueHandling.Ignore)]
        public List<Match> matches { get; set; }
    }

    public class Match
    {        
        public int id { get; set; }
        public DateTime utcDate { get; set; }
        public string status { get; set; }
        public int matchday { get; set; }
        public DateTime lastUpdated { get; set; }
        public Hometeam homeTeam { get; set; }
        public Awayteam awayTeam { get; set; }
        public Score score { get; set; }
        public Odds odds { get; set; }

    }

    public class Competition
    {
        public int id { get; set; }
        public string name { get; set; }
        public string emblem { get; set; }
    }


    public class Hometeam
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Awayteam
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Score
    {
        public string winner { get; set; }
        public string duration { get; set; }
        public Fulltime fullTime { get; set; }
        public Halftime halfTime { get; set; }
    }

    public class Fulltime
    {
        public int home { get; set; }
        public int away { get; set; }
    }

    public class Halftime
    {
        public int home { get; set; }
        public int away { get; set; }
    }

    public class Odds
    {
        public string msg { get; set; }
    }

}
