namespace FRCScout.Migrations
{
    using FRCScout.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ScoutContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ScoutContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            IList<Team> teams = new List<Team>
            {
                new Team { Number = 4, TeamName = "Team 4 ELEMENT", City = "Van Nuys", State = "CA", Country = "US" },
                new Team { Number = 207, TeamName = "METALCRAFTERS", City = "Hawthorne", State = "CA", Country = "US" },
                new Team { Number = 294, TeamName = "Beach Cities Robotics", City = "Redondo Beach", State = "CA", Country = "US" },
                new Team { Number = 330, TeamName = "The Beach Bots", City = "Hermosa Beach", State = "CA", Country = "US" },
                new Team { Number = 606, TeamName = "Cyber Eagles", City = "Los Angeles", State = "CA", Country = "US" },
                new Team { Number = 687, TeamName = "The Nerd Herd", City = "Carson", State = "CA", Country = "US" },
                new Team { Number = 691, TeamName = "Project 691", City = "Santa Clarita", State = "CA", Country = "US" },
                new Team { Number = 696, TeamName = "Circuit Breakers", City = "La Cresenta", State = "CA", Country = "US" },
                new Team { Number = 702, TeamName = "Bagel Bytes", City = "Culver City", State = "CA", Country = "US" },
                new Team { Number = 848, TeamName = "ROBOHUSKIES", City = "San Pedro", State = "CA", Country = "US" }
            };
            IList<Match> matches = new List<Match>
            {
                new Match
                {
                    Name = "Qualifying Match 1",
                    ScheduledTime = DateTime.Now.AddHours(-2),
                    Participants = new List<MatchParticipant>
                    {
                        new MatchParticipant { TeamNumber = teams[0].Number, Alliance = "blue" },
                        new MatchParticipant { TeamNumber = teams[1].Number, Alliance = "blue" },
                        new MatchParticipant { TeamNumber = teams[2].Number, Alliance = "blue" },
                        new MatchParticipant { TeamNumber = teams[3].Number, Alliance = "red" },
                        new MatchParticipant { TeamNumber = teams[4].Number, Alliance = "red" },
                        new MatchParticipant { TeamNumber = teams[5].Number, Alliance = "red" }
                    }
                }
            };

            foreach (Team team in teams)
            {
                context.Teams.AddOrUpdate<Team>(t => t.Number, team);
            }

            foreach (Match match in matches)
            {
                context.Matches.AddOrUpdate<Match>(m => m.MatchId, match);
            }
        }
    }
}
