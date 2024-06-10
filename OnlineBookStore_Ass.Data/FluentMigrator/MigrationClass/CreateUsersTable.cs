using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace OnlineBookStore_Ass.Data.FluentMigrator.MigrationClass
{
    [Migration(2123060102)]
    public class CreateUsersTable : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Username").AsString().NotNullable()
                .WithColumn("LoginStatus").AsGuid().Nullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("FullName").AsString().NotNullable()
                .WithColumn("UserType").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }

}
