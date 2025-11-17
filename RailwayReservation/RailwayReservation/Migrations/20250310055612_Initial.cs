using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailwayReservation.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ClassId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ClassType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ClassName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Class__CB1927C09461449A", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "Query",
                columns: table => new
                {
                    QueryId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Keywords = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Query__5967F7DB55D6C226", x => x.QueryId);
                });

            migrationBuilder.CreateTable(
                name: "Quota",
                columns: table => new
                {
                    QuotaId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    QuotaType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    AgeLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Quota__AE96C9C2AD71DD0A", x => x.QuotaId);
                });

            migrationBuilder.CreateTable(
                name: "Train",
                columns: table => new
                {
                    TrainId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    TrainNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    TrainName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: false),
                    RunningDay = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Route = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Train__8ED2723A4AA63CB9", x => x.TrainId);
                });

            migrationBuilder.CreateTable(
                name: "TrainRoute",
                columns: table => new
                {
                    RouteID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Source = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Departure = table.Column<TimeOnly>(type: "time", nullable: true),
                    Destination = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Arrival = table.Column<TimeOnly>(type: "time", nullable: true),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Route__80979AAD9EFF58F3", x => x.RouteID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Password = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Role = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4C9849D219", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Coach",
                columns: table => new
                {
                    CoachId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ClassId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CoachNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Coach__F411D9411E387FDD", x => x.CoachId);
                    table.ForeignKey(
                        name: "FK__Coach__ClassId__3C69FB99",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId");
                });

            migrationBuilder.CreateTable(
                name: "Fare",
                columns: table => new
                {
                    FareId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ClassId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CancelCharge_48hrs = table.Column<double>(type: "float", nullable: false),
                    CancelCharge_12hrs = table.Column<double>(type: "float", nullable: false),
                    CancelCharge_4hrs = table.Column<double>(type: "float", nullable: false),
                    BaseFare = table.Column<double>(type: "float", nullable: false),
                    ReservationCharge = table.Column<double>(type: "float", nullable: false),
                    ChargePerKm = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Fare__1261FA163B2AF02B", x => x.FareId);
                    table.ForeignKey(
                        name: "FK__Fare__ClassId__47DBAE45",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId");
                });

            migrationBuilder.CreateTable(
                name: "QueryList",
                columns: table => new
                {
                    QueryListId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    QueryId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    QueryDescription = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QueryLis__ACD72B5FFD3A8083", x => x.QueryListId);
                    table.ForeignKey(
                        name: "FK__QueryList__Query__5812160E",
                        column: x => x.QueryId,
                        principalTable: "Query",
                        principalColumn: "QueryId");
                });

            migrationBuilder.CreateTable(
                name: "TrainClass",
                columns: table => new
                {
                    TrainId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ClassId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__TrainClas__Class__38996AB5",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId");
                    table.ForeignKey(
                        name: "FK__TrainClas__Train__398D8EEE",
                        column: x => x.TrainId,
                        principalTable: "Train",
                        principalColumn: "TrainId");
                });

            migrationBuilder.CreateTable(
                name: "TrainQuota",
                columns: table => new
                {
                    TrainId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    QuotaId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__TrainQout__Quota__31EC6D26",
                        column: x => x.QuotaId,
                        principalTable: "Quota",
                        principalColumn: "QuotaId");
                    table.ForeignKey(
                        name: "FK__TrainQout__Train__32E0915F",
                        column: x => x.TrainId,
                        principalTable: "Train",
                        principalColumn: "TrainId");
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    UserId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TrainId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    PNR = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Duration = table.Column<TimeOnly>(type: "time", nullable: false),
                    SourceStation = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DestinationStation = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    TicketStatus = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    TotalFare = table.Column<double>(type: "float", nullable: false),
                    JourneyStartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    JourneyEndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SeatNumber = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    ClassName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Coach = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ticket__712CC6074E53AE41", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK__Ticket__TrainId__440B1D61",
                        column: x => x.TrainId,
                        principalTable: "Train",
                        principalColumn: "TrainId");
                    table.ForeignKey(
                        name: "FK__Ticket__UserId__4316F928",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    SeatId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CoachId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    SeatNumber = table.Column<int>(type: "int", nullable: true),
                    AvailabilityStatus = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Seat__311713F310BEF864", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK__Seat__CoachId__403A8C7D",
                        column: x => x.CoachId,
                        principalTable: "Coach",
                        principalColumn: "CoachId");
                });

            migrationBuilder.CreateTable(
                name: "Support",
                columns: table => new
                {
                    SupportId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    UserId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    QueryListId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    QueryText = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Support__D82DBC8C65F15FA7", x => x.SupportId);
                    table.ForeignKey(
                        name: "FK__Support__QueryLi__5BE2A6F2",
                        column: x => x.QueryListId,
                        principalTable: "QueryList",
                        principalColumn: "QueryListId");
                    table.ForeignKey(
                        name: "FK__Support__UserId__5AEE82B9",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    UserId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    TicketId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PaymentMode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__9B556A3841ECCF0A", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK__Payment__TicketI__4BAC3F29",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "TicketId");
                    table.ForeignKey(
                        name: "FK__Payment__UserId__4AB81AF0",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ReservationDetails",
                columns: table => new
                {
                    ReservationId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    TicketId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    UserId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Gender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    PaymentId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reservat__B7EE5F245B61F91F", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK__Reservati__Payme__534D60F1",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId");
                    table.ForeignKey(
                        name: "FK__Reservati__Ticke__5070F446",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "TicketId");
                    table.ForeignKey(
                        name: "FK__Reservati__UserI__5165187F",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coach_ClassId",
                table: "Coach",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Fare_ClassId",
                table: "Fare",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_TicketId",
                table: "Payment",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserId",
                table: "Payment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QueryList_QueryId",
                table: "QueryList",
                column: "QueryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetails_PaymentId",
                table: "ReservationDetails",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetails_TicketId",
                table: "ReservationDetails",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetails_UserId",
                table: "ReservationDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_CoachId",
                table: "Seat",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Support_QueryListId",
                table: "Support",
                column: "QueryListId");

            migrationBuilder.CreateIndex(
                name: "IX_Support_UserId",
                table: "Support",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TrainId",
                table: "Ticket",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainClass_ClassId",
                table: "TrainClass",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainClass_TrainId",
                table: "TrainClass",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainQuota_QuotaId",
                table: "TrainQuota",
                column: "QuotaId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainQuota_TrainId",
                table: "TrainQuota",
                column: "TrainId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fare");

            migrationBuilder.DropTable(
                name: "ReservationDetails");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "Support");

            migrationBuilder.DropTable(
                name: "TrainClass");

            migrationBuilder.DropTable(
                name: "TrainQuota");

            migrationBuilder.DropTable(
                name: "TrainRoute");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Coach");

            migrationBuilder.DropTable(
                name: "QueryList");

            migrationBuilder.DropTable(
                name: "Quota");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Query");

            migrationBuilder.DropTable(
                name: "Train");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
