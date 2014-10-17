namespace FRCScout.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ScoutContext : DbContext
    {
        // Your context has been configured to use a 'ScoutContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'FRCScout.Models.ScoutContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ScoutContext' 
        // connection string in the application configuration file.
        public ScoutContext()
            : base("name=ScoutContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual IDbSet<Team> Teams { get; set; }

        public virtual IDbSet<Match> Matches { get; set; }

        public virtual IDbSet<MatchParticipant> MatchParticipants { get; set; }

        public virtual IDbSet<Robot> Robots { get; set; }

        public virtual IDbSet<Picture> Pictures { get; set; }
    }
}