using Microsoft.EntityFrameworkCore.Migrations;

namespace Watcher_GUI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Symbol",
                columns: table => new
                {
                    SymbolName = table.Column<string>(nullable: false),
                    IoTHubName = table.Column<string>(nullable: true),
                    TimeChartDeviceName = table.Column<string>(nullable: true),
                    LongRenkoChartDeviceName = table.Column<string>(nullable: true),
                    ShortRenkoChartDeviceName = table.Column<string>(nullable: true),
                    TimeChartConnectionString = table.Column<string>(nullable: true),
                    LongRenkoChartConnectionString = table.Column<string>(nullable: true),
                    ShortRenkoChartConnectionString = table.Column<string>(nullable: true),
                    LongTermMasterDirection = table.Column<string>(nullable: true),
                    ShortTermMasterDirection = table.Column<string>(nullable: true),
                    LongTermTrend = table.Column<string>(nullable: true),
                    ShortTermTrend = table.Column<string>(nullable: true),
                    LongTermATR = table.Column<double>(nullable: false),
                    ShortTermATR = table.Column<double>(nullable: false),
                    LongTermMasterTimeFrame = table.Column<string>(nullable: true),
                    ShortTermMasterTimeFrame = table.Column<string>(nullable: true),
                    LongTermTrendTimeFrame = table.Column<string>(nullable: true),
                    ShortTermTrendTimeFrame = table.Column<string>(nullable: true),
                    LongTermRenkoBarType = table.Column<string>(nullable: true),
                    LongTermRenkoBarSize = table.Column<int>(nullable: false),
                    ShortTermRenkoBarType = table.Column<string>(nullable: true),
                    ShortTermRenkoBarSize = table.Column<int>(nullable: false),
                    LongTermRenkoLastUpdate = table.Column<string>(nullable: true),
                    ShortTermRenkoLastUpdate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symbol", x => x.SymbolName);
                });

            migrationBuilder.CreateTable(
                name: "Watcher",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IoTHubName = table.Column<string>(nullable: true),
                    EventHubEndpoint = table.Column<string>(nullable: true),
                    EventHubPath = table.Column<string>(nullable: true),
                    EventHubKeyName = table.Column<string>(nullable: true),
                    EventHubPrimayKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watcher", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Symbol");

            migrationBuilder.DropTable(
                name: "Watcher");
        }
    }
}
