using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace OnlineBookStore_Ass.Data.FluentMigrator.MigrationClass
{

    [Migration(2923060104)]
    public class CreatePurchasesTable : Migration
    {
        public override void Up()
        {
            Create.Table("Purchases")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().ForeignKey("Users", "Id")
                .WithColumn("BooksId").AsString().NotNullable()
                .WithColumn("PaymentOption").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Purchases");
        }
    }

}
