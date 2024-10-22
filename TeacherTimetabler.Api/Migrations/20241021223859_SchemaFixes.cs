using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherTimetabler.Api.Migrations
{
  /// <inheritdoc />
  public partial class SchemaFixes : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(name: "FK_ItemInstances_Classes_ClassId", table: "ItemInstances");

      migrationBuilder.DropColumn(name: "DefaultTimetableType", table: "AspNetUsers");

      migrationBuilder.RenameColumn(name: "Id", table: "WeekInstances", newName: "EntityId");

      migrationBuilder.RenameColumn(name: "Id", table: "Timetables", newName: "EntityId");

      migrationBuilder.RenameColumn(name: "Id", table: "Timeslots", newName: "EntityId");

      migrationBuilder.RenameColumn(name: "Id", table: "RecurringItems", newName: "EntityId");

      migrationBuilder.RenameColumn(name: "Id", table: "ItemInstances", newName: "EntityId");

      migrationBuilder.RenameColumn(name: "Name", table: "Classes", newName: "ClassName");

      migrationBuilder.RenameColumn(name: "Id", table: "Classes", newName: "EntityId");

      migrationBuilder.AlterColumn<int>(
        name: "ClassId",
        table: "ItemInstances",
        type: "integer",
        nullable: false,
        defaultValue: 0,
        oldClrType: typeof(int),
        oldType: "integer",
        oldNullable: true
      );

      migrationBuilder.AddColumn<bool>(
        name: "TimetableIsBiweekly",
        table: "AspNetUsers",
        type: "boolean",
        nullable: false,
        defaultValue: false
      );

      migrationBuilder.AddForeignKey(
        name: "FK_ItemInstances_Classes_ClassId",
        table: "ItemInstances",
        column: "ClassId",
        principalTable: "Classes",
        principalColumn: "EntityId",
        onDelete: ReferentialAction.Cascade
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(name: "FK_ItemInstances_Classes_ClassId", table: "ItemInstances");

      migrationBuilder.DropColumn(name: "TimetableIsBiweekly", table: "AspNetUsers");

      migrationBuilder.RenameColumn(name: "EntityId", table: "WeekInstances", newName: "Id");

      migrationBuilder.RenameColumn(name: "EntityId", table: "Timetables", newName: "Id");

      migrationBuilder.RenameColumn(name: "EntityId", table: "Timeslots", newName: "Id");

      migrationBuilder.RenameColumn(name: "EntityId", table: "RecurringItems", newName: "Id");

      migrationBuilder.RenameColumn(name: "EntityId", table: "ItemInstances", newName: "Id");

      migrationBuilder.RenameColumn(name: "ClassName", table: "Classes", newName: "Name");

      migrationBuilder.RenameColumn(name: "EntityId", table: "Classes", newName: "Id");

      migrationBuilder.AlterColumn<int>(
        name: "ClassId",
        table: "ItemInstances",
        type: "integer",
        nullable: true,
        oldClrType: typeof(int),
        oldType: "integer"
      );

      migrationBuilder.AddColumn<string>(
        name: "DefaultTimetableType",
        table: "AspNetUsers",
        type: "text",
        nullable: true
      );

      migrationBuilder.AddForeignKey(
        name: "FK_ItemInstances_Classes_ClassId",
        table: "ItemInstances",
        column: "ClassId",
        principalTable: "Classes",
        principalColumn: "Id"
      );
    }
  }
}
