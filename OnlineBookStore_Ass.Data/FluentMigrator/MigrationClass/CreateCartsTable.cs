using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace OnlineBookStore_Ass.Data.FluentMigrator.MigrationClass
{
    [Migration(2823060103)]
    public class CreateCartsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Carts")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().ForeignKey("Users", "Id")
                .WithColumn("BookId").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Carts");
        }
    }

}
