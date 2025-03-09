using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainningPlans",
                columns: table => new
                {
                    TrainningPlanId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BodyPart = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainningPlans", x => x.TrainningPlanId);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PersonalRecord = table.Column<double>(type: "REAL", nullable: false),
                    TrainningPlanId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.ExerciseId);
                    table.ForeignKey(
                        name: "FK_Exercises_TrainningPlans_TrainningPlanId",
                        column: x => x.TrainningPlanId,
                        principalTable: "TrainningPlans",
                        principalColumn: "TrainningPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainnings",
                columns: table => new
                {
                    TrainningId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TrainningPlanId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainnings", x => x.TrainningId);
                    table.ForeignKey(
                        name: "FK_Trainnings_TrainningPlans_TrainningPlanId",
                        column: x => x.TrainningPlanId,
                        principalTable: "TrainningPlans",
                        principalColumn: "TrainningPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoneExercises",
                columns: table => new
                {
                    DoneExerciseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExerciseId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainningId = table.Column<int>(type: "INTEGER", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    Repetitions = table.Column<int>(type: "INTEGER", nullable: false),
                    Sets = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoneExercises", x => x.DoneExerciseId);
                    table.ForeignKey(
                        name: "FK_DoneExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoneExercises_Trainnings_TrainningId",
                        column: x => x.TrainningId,
                        principalTable: "Trainnings",
                        principalColumn: "TrainningId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoneExercises_ExerciseId",
                table: "DoneExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_DoneExercises_TrainningId",
                table: "DoneExercises",
                column: "TrainningId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_TrainningPlanId",
                table: "Exercises",
                column: "TrainningPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainnings_TrainningPlanId",
                table: "Trainnings",
                column: "TrainningPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoneExercises");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Trainnings");

            migrationBuilder.DropTable(
                name: "TrainningPlans");
        }
    }
}
