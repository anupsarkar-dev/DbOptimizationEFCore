using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DBOptimizedDotNet.Migrations
{
    public partial class AddLeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leaders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameWithCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverPhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutoGenCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivationStatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreelancingAdjustHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejectedReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlacementTeamOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnershipChangeHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralChangeHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlacementChangeHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MacAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrowserInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisableReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnableReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUserChosen = table.Column<bool>(type: "bit", nullable: false),
                    IsCalculated = table.Column<bool>(type: "bit", nullable: false),
                    IsMaskEmail = table.Column<bool>(type: "bit", nullable: false),
                    IsOrgAcc = table.Column<bool>(type: "bit", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: true),
                    BloodGroupId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    PoliceStationId = table.Column<int>(type: "int", nullable: true),
                    UnionId = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: true),
                    ActivationStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedLeaderId = table.Column<long>(type: "bigint", nullable: true),
                    ReferralId = table.Column<long>(type: "bigint", nullable: true),
                    PlacementId = table.Column<long>(type: "bigint", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    FirstGenId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    RankId = table.Column<long>(type: "bigint", nullable: true),
                    Unit = table.Column<double>(type: "float", nullable: false),
                    ECommerceUnit = table.Column<double>(type: "float", nullable: false),
                    FreelancingAmount = table.Column<double>(type: "float", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    TotalAssociate = table.Column<int>(type: "int", nullable: false),
                    TeamATotalAssociate = table.Column<int>(type: "int", nullable: false),
                    TeamBTotalAssociate = table.Column<int>(type: "int", nullable: false),
                    TeamCTotalAssociate = table.Column<int>(type: "int", nullable: false),
                    TeamDTotalAssociate = table.Column<int>(type: "int", nullable: false),
                    TeamETotalAssociate = table.Column<int>(type: "int", nullable: false),
                    TeamFTotalAssociate = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    DisabledById = table.Column<long>(type: "bigint", nullable: true),
                    EnabledById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisabledOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisabledFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisabledTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnabledOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leaders");
        }
    }
}
