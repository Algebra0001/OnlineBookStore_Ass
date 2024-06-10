using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;


namespace OnlineBookStore_Ass.Data.FluentMigrator.MigrationClass
{

    [Migration(2523060101)]
    public class CreateBooksTable : Migration
    {
        public override void Up()
        {
            Create.Table("Books")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("Author").AsString().NotNullable()
                .WithColumn("ISBN").AsString().NotNullable()
                .WithColumn("PublicationYear").AsString().NotNullable()
                .WithColumn("Price").AsDouble().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Books");
        }
    }

}
