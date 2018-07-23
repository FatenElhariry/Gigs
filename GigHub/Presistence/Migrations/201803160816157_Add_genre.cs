namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_genre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres(ID ,Name) values(1,'jazz')");
            Sql("INSERT INTO Genres(ID ,Name) values(2,'blues')");
            Sql("INSERT INTO Genres(ID ,Name) values(3,'Rock')");
            Sql("INSERT INTO Genres(ID ,Name) values(4,'Country')");


        }

        public override void Down()
        {
            Sql("Delete From Genres where ID in (1,2,3,4)");
        }
    }
}
