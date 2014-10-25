namespace FRCScout.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        MatchId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ScheduledTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ActualStartTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        UnaccountedBluePenalties = c.Int(nullable: false),
                        UnaccountedRedPenalties = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MatchId);
            
            CreateTable(
                "dbo.MatchParticipants",
                c => new
                    {
                        MatchParticipantId = c.Int(nullable: false, identity: true),
                        TeamNumber = c.Int(nullable: false),
                        Alliance = c.String(),
                        AutonomousPoints = c.Int(nullable: false),
                        TeleoperatedPoints = c.Int(nullable: false),
                        BonusPoints = c.Int(nullable: false),
                        PenaltyPoints = c.Int(nullable: false),
                        Match_MatchId = c.Int(),
                    })
                .PrimaryKey(t => t.MatchParticipantId)
                .ForeignKey("dbo.Matches", t => t.Match_MatchId)
                .ForeignKey("dbo.Teams", t => t.TeamNumber, cascadeDelete: true)
                .Index(t => t.TeamNumber)
                .Index(t => t.Match_MatchId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Number = c.Int(nullable: false),
                        SchoolName = c.String(),
                        TeamName = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Number);
            
            CreateTable(
                "dbo.Robots",
                c => new
                    {
                        RobotId = c.Int(nullable: false, identity: true),
                        TeamNumber = c.Int(nullable: false),
                        AutonomousControlType = c.String(),
                        ControllerType = c.String(),
                    })
                .PrimaryKey(t => t.RobotId)
                .ForeignKey("dbo.Teams", t => t.TeamNumber, cascadeDelete: true)
                .Index(t => t.TeamNumber);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PictureId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Robot_RobotId = c.Int(),
                    })
                .PrimaryKey(t => t.PictureId)
                .ForeignKey("dbo.Robots", t => t.Robot_RobotId)
                .Index(t => t.Robot_RobotId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MatchParticipants", "TeamNumber", "dbo.Teams");
            DropForeignKey("dbo.Robots", "TeamNumber", "dbo.Teams");
            DropForeignKey("dbo.Pictures", "Robot_RobotId", "dbo.Robots");
            DropForeignKey("dbo.MatchParticipants", "Match_MatchId", "dbo.Matches");
            DropIndex("dbo.Pictures", new[] { "Robot_RobotId" });
            DropIndex("dbo.Robots", new[] { "TeamNumber" });
            DropIndex("dbo.MatchParticipants", new[] { "Match_MatchId" });
            DropIndex("dbo.MatchParticipants", new[] { "TeamNumber" });
            DropTable("dbo.Pictures");
            DropTable("dbo.Robots");
            DropTable("dbo.Teams");
            DropTable("dbo.MatchParticipants");
            DropTable("dbo.Matches");
        }
    }
}
