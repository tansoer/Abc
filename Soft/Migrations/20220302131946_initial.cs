using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abc.Soft.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Process");

            migrationBuilder.EnsureSchema(
                name: "Role");

            migrationBuilder.EnsureSchema(
                name: "Product");

            migrationBuilder.EnsureSchema(
                name: "Party");

            migrationBuilder.EnsureSchema(
                name: "Inventory");

            migrationBuilder.EnsureSchema(
                name: "Currency");

            migrationBuilder.EnsureSchema(
                name: "Classifier");

            migrationBuilder.EnsureSchema(
                name: "Quantity");

            migrationBuilder.EnsureSchema(
                name: "Order");

            migrationBuilder.EnsureSchema(
                name: "Rule");

            migrationBuilder.CreateTable(
                name: "ActionApprovers",
                schema: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproverSignatureId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionApprovers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Actions",
                schema: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitiatorSignatureId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionStatusClassifierId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleContextId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActionTypes",
                schema: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssignedResponsibilities",
                schema: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartyRoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibilityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartySignatureId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedResponsibilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeTypes",
                schema: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ElementTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeValues",
                schema: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProcessElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Equality = table.Column<int>(type: "int", nullable: false),
                    AttributeTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value_UnitOrCurrencyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value_ValueType = table.Column<int>(type: "int", nullable: true),
                    Value_ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Value_ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BatchCheckedByParties",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BatchId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartySignatureId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchCheckedByParties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Batches",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstSerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastSerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellBy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UseBy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BestBefore = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateProduced = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BodyMetrics",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SignatureId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetricType = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMetrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BodyMetricTypes",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMetricTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CapacityManagers",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacityManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardAssociations",
                schema: "Currency",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardAssociations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogedProducts",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CatalogEntryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogedProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogEntries",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CatalogId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classifiers",
                schema: "Classifier",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClassifierType = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommonTerms",
                schema: "Quantity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Power = table.Column<double>(type: "float", nullable: false),
                    TermId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonTerms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContainerItems",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContainerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowNumber = table.Column<byte>(type: "tinyint", nullable: false),
                    ColumnNumber = table.Column<byte>(type: "tinyint", nullable: false),
                    LevelNumber = table.Column<byte>(type: "tinyint", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OfficialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NativeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumericCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsIsoCountry = table.Column<bool>(type: "bit", nullable: false),
                    IsLoyaltyProgram = table.Column<bool>(type: "bit", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "Currency",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumericCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MajorUnitSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinorUnitSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatioOfMinorUnit = table.Column<double>(type: "float", nullable: false),
                    IsIsoCurrency = table.Column<bool>(type: "bit", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyUsages",
                schema: "Currency",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyNativeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyUsages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryTypes",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DiscountTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    OrderEventId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscountTypes",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderManagerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EligibilityRuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    CurrencyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeatureTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueSpecId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeatureSpecs",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value_UnitOrCurrencyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value_ValueType = table.Column<int>(type: "int", nullable: true),
                    Value_ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Value_ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureSpecs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeatureTypes",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    NumericCode = table.Column<int>(type: "int", nullable: false),
                    MustSatisfyAll = table.Column<bool>(type: "bit", nullable: false),
                    ValueType = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identifiers",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegisteredIdentifierTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationAuthorityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryEntries",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RestockPolicyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InventoryEntryType = table.Column<int>(type: "int", nullable: false),
                    CapacityManagerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InventoryId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Measures",
                schema: "Quantity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MeasureType = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderEvents",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderEventType = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductDeliveryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    AuthorizedPartySignatureId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldOrderLineId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderLineId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldPartySummaryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartySummaryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldTermsAndConditionsId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermsAndConditionsId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderLineItems",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderLineId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderLineType = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLineItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderEventId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAssessed = table.Column<bool>(type: "bit", nullable: false),
                    OrderLineType = table.Column<int>(type: "int", nullable: false),
                    NumberOfProducts = table.Column<int>(type: "int", nullable: false),
                    ExpectedDelivery = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    CurrencyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderLineId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalesTaxPolicyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderManagers",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderPayments",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    CurrencyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromPaymentMethodId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderEventId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToPaymentMethodId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderManagerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderType = table.Column<int>(type: "int", nullable: false),
                    DateSentReceived = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SalesChannelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountRuleContextId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseOrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderEventId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutcomeApprovers",
                schema: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OutcomeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproverSignatureId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutcomeApprovers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Outcomes",
                schema: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OutcomeTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPossibleOutcome = table.Column<bool>(type: "bit", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleContextId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outcomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutcomeTypes",
                schema: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutcomeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutstandingOrders",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PurchaseOrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InventoryEntryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutstandingOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackageComponents",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageComponents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackageContents",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    OrganizationTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyType = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartyAuthentications",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartyUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthenticationProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyAuthentications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartyCapabilities",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RuleContextId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyCapabilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartyContacts",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalDirectDialingPrefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Device = table.Column<int>(type: "int", nullable: false),
                    CityOrAreaCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionOrStateOrCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactType = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartyContactUsages",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyContactUsages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartyNames",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GivenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameType = table.Column<int>(type: "int", nullable: false),
                    PartyType = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartyNameUses",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameUseTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyNameId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyNameUses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartySignatures",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyAuthenticationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartySummaryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignedEntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignedEntityTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartySignatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartySummaries",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyRoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderLineId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartySummaryRoleClassifierId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleInOrder = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartySummaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                schema: "Currency",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DailyLimit = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    IssueOrBankIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingOrBankAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardOrCheckNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Payee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditLimit = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    CardAssociationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethodType = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonEthnicities",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EthnicityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonEthnicities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonNamePrefixes",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrefixTypeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonNamePrefixes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonNameSuffixes",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SuffixTypeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonNameSuffixes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PossibleFeatureValues",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Relation = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeatureTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueSpecId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PossibleFeatureValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreferenceOptions",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PreferenceTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenceOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Preferences",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferenceOptionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferenceTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyRoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceEndorsements",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PriceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartySignatureId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceEndorsements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    CurrencyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PossiblePriceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleOverrideId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kind = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Processes",
                schema: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProcessTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerPartyRoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitiatorPartyRoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityClassifierId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleContextId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessTypes",
                schema: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCatalogs",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCatalogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BaseCategoryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductDeliveries",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeliveryTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDeliveries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductInclusions",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InclusionKind = table.Column<int>(type: "int", nullable: false),
                    MinimumAmount = table.Column<double>(type: "float", nullable: false),
                    MaximumAmount = table.Column<double>(type: "float", nullable: false),
                    UnitId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductInclusionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInclusions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductRelationships",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationshipTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsumerEntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderEntityId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRelationships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductRelationshipTypes",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsumerTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRelationshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductKind = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    UnitId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduledFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveredFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveredTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryStatus = table.Column<int>(type: "int", nullable: false),
                    ReservationStatus = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSetContents",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSetContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSets",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PackageTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductKind = table.Column<int>(type: "int", nullable: false),
                    PricingStrategy = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    UnitId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodOfOperationFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PeriodOfOperationTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ColumnsCount = table.Column<byte>(type: "tinyint", nullable: false),
                    RowsCount = table.Column<byte>(type: "tinyint", nullable: false),
                    LevelsCount = table.Column<byte>(type: "tinyint", nullable: false),
                    AlternativeCodes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBaseType = table.Column<bool>(type: "bit", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RateRules",
                schema: "Currency",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RateTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                schema: "Currency",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrencyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RateTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RateTypes",
                schema: "Currency",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationAuthorities",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthorityTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationAuthorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipConstraints",
                schema: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConstraintTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationshipTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipConstraints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipConstraintTypes",
                schema: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConsumerRoleTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderRoleTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipConstraintTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relationships",
                schema: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationshipTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsumerEntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderEntityId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipTypes",
                schema: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsumerTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservationReceivers",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverPartySummaryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReservationRequestId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationReceivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservationRequests",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequesterPartySummaryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberRequested = table.Column<long>(type: "bigint", nullable: false),
                    ProductTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleContextId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleOverridesId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InventoryId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReservationRequestId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancellationPolicyRuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InventoryId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Responsibilities",
                schema: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsOptional = table.Column<bool>(type: "bit", nullable: false),
                    ResponsibilityTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyRoleTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsibilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResponsibilityTypes",
                schema: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequirementsRuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConditionsOfSatisfactionRuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsibilityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RestockPolicies",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RestockRuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestockRuleContextId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestockPolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReturnedItems",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderEventId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnedItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleConstraints",
                schema: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleConstraintTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyRoleTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleConstraints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleConstraintTypes",
                schema: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartyType = table.Column<int>(type: "int", nullable: false),
                    OrganizationTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleConstraintTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartyRoleTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleTypes",
                schema: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RuleContexts",
                schema: "Rule",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RuleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleContexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RuleElements",
                schema: "Rule",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    RuleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleContextId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOrCurrencyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleElementType = table.Column<int>(type: "int", nullable: false),
                    Operation = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleElements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RuleOverrides",
                schema: "Rule",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleContextId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VariableId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyAuthenticationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartySummaryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignedEntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignedEntityTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleOverrides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                schema: "Rule",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RuleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleKind = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RuleSets",
                schema: "Rule",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RuleUsages",
                schema: "Rule",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RuleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleUsages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesTaxPolicies",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderManagerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxationRate = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    TaxationTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesTaxPolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SignedEntityTypes",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignedEntityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemsOfUnits",
                schema: "Quantity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemsOfUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskParticipants",
                schema: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartyRoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskParticipants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                schema: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ThreadId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleContextId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromPartyAddressId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToPartyAddressId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEscalation = table.Column<bool>(type: "bit", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationshipTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsumerEntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderEntityId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskTypes",
                schema: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ThreadTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsumerTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelecomDeviceRegistrations",
                schema: "Party",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostalAddressId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelecomDeviceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelecomDeviceRegistrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TermsAndConditions",
                schema: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermsAndConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Threads",
                schema: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ThreadTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TerminatorSignatureId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleContextId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThreadTypes",
                schema: "Process",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProcessTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleSetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitFactors",
                schema: "Quantity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Factor = table.Column<double>(type: "float", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UnitId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemOfUnitsId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitFactors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitRules",
                schema: "Quantity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FromBaseUnitRuleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToBaseUnitRuleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UnitId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemOfUnitsId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                schema: "Quantity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MeasureId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitType = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Replaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedWhy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionApprovers",
                schema: "Process");

            migrationBuilder.DropTable(
                name: "Actions",
                schema: "Process");

            migrationBuilder.DropTable(
                name: "ActionTypes",
                schema: "Process");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssignedResponsibilities",
                schema: "Role");

            migrationBuilder.DropTable(
                name: "AttributeTypes",
                schema: "Process");

            migrationBuilder.DropTable(
                name: "AttributeValues",
                schema: "Process");

            migrationBuilder.DropTable(
                name: "BatchCheckedByParties",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Batches",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "BodyMetrics",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "BodyMetricTypes",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "CapacityManagers",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "CardAssociations",
                schema: "Currency");

            migrationBuilder.DropTable(
                name: "CatalogedProducts",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "CatalogEntries",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Classifiers",
                schema: "Classifier");

            migrationBuilder.DropTable(
                name: "CommonTerms",
                schema: "Quantity");

            migrationBuilder.DropTable(
                name: "ContainerItems",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "Currency");

            migrationBuilder.DropTable(
                name: "CurrencyUsages",
                schema: "Currency");

            migrationBuilder.DropTable(
                name: "DeliveryTypes",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "Discounts",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "DiscountTypes",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "Features",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "FeatureSpecs",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "FeatureTypes",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Identifiers",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "Inventories",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "InventoryEntries",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "Measures",
                schema: "Quantity");

            migrationBuilder.DropTable(
                name: "OrderEvents",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "OrderLineItems",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "OrderLines",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "OrderManagers",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "OrderPayments",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "OrderStatuses",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "OutcomeApprovers",
                schema: "Process");

            migrationBuilder.DropTable(
                name: "Outcomes",
                schema: "Process");

            migrationBuilder.DropTable(
                name: "OutcomeTypes",
                schema: "Process");

            migrationBuilder.DropTable(
                name: "OutstandingOrders",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "PackageComponents",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "PackageContents",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Parties",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "PartyAuthentications",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "PartyCapabilities",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "PartyContacts",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "PartyContactUsages",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "PartyNames",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "PartyNameUses",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "PartySignatures",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "PartySummaries",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "PaymentMethods",
                schema: "Currency");

            migrationBuilder.DropTable(
                name: "PersonEthnicities",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "PersonNamePrefixes",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "PersonNameSuffixes",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "PossibleFeatureValues",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "PreferenceOptions",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "Preferences",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "PriceEndorsements",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Prices",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Processes",
                schema: "Process");

            migrationBuilder.DropTable(
                name: "ProcessTypes",
                schema: "Process");

            migrationBuilder.DropTable(
                name: "ProductCatalogs",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "ProductCategories",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "ProductDeliveries",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "ProductInclusions",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "ProductRelationships",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "ProductRelationshipTypes",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "ProductSetContents",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "ProductSets",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "ProductTypes",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "RateRules",
                schema: "Currency");

            migrationBuilder.DropTable(
                name: "Rates",
                schema: "Currency");

            migrationBuilder.DropTable(
                name: "RateTypes",
                schema: "Currency");

            migrationBuilder.DropTable(
                name: "RegistrationAuthorities",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "RelationshipConstraints",
                schema: "Role");

            migrationBuilder.DropTable(
                name: "RelationshipConstraintTypes",
                schema: "Role");

            migrationBuilder.DropTable(
                name: "Relationships",
                schema: "Role");

            migrationBuilder.DropTable(
                name: "RelationshipTypes",
                schema: "Role");

            migrationBuilder.DropTable(
                name: "ReservationReceivers",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "ReservationRequests",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "Reservations",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "Responsibilities",
                schema: "Role");

            migrationBuilder.DropTable(
                name: "ResponsibilityTypes",
                schema: "Role");

            migrationBuilder.DropTable(
                name: "RestockPolicies",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "ReturnedItems",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "RoleConstraints",
                schema: "Role");

            migrationBuilder.DropTable(
                name: "RoleConstraintTypes",
                schema: "Role");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Role");

            migrationBuilder.DropTable(
                name: "RoleTypes",
                schema: "Role");

            migrationBuilder.DropTable(
                name: "RuleContexts",
                schema: "Rule");

            migrationBuilder.DropTable(
                name: "RuleElements",
                schema: "Rule");

            migrationBuilder.DropTable(
                name: "RuleOverrides",
                schema: "Rule");

            migrationBuilder.DropTable(
                name: "Rules",
                schema: "Rule");

            migrationBuilder.DropTable(
                name: "RuleSets",
                schema: "Rule");

            migrationBuilder.DropTable(
                name: "RuleUsages",
                schema: "Rule");

            migrationBuilder.DropTable(
                name: "SalesTaxPolicies",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "SignedEntityTypes",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "SystemsOfUnits",
                schema: "Quantity");

            migrationBuilder.DropTable(
                name: "TaskParticipants",
                schema: "Process");

            migrationBuilder.DropTable(
                name: "Tasks",
                schema: "Process");

            migrationBuilder.DropTable(
                name: "TaskTypes",
                schema: "Process");

            migrationBuilder.DropTable(
                name: "TelecomDeviceRegistrations",
                schema: "Party");

            migrationBuilder.DropTable(
                name: "TermsAndConditions",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "Threads",
                schema: "Process");

            migrationBuilder.DropTable(
                name: "ThreadTypes",
                schema: "Process");

            migrationBuilder.DropTable(
                name: "UnitFactors",
                schema: "Quantity");

            migrationBuilder.DropTable(
                name: "UnitRules",
                schema: "Quantity");

            migrationBuilder.DropTable(
                name: "Units",
                schema: "Quantity");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
