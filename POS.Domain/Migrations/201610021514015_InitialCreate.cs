namespace POS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BarCode = c.String(),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryId = c.Int(nullable: false),
                        UnitId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .Index(t => t.CategoryId)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.BarCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Barcode = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductProperties",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        PropertyId = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => new { t.ProductId, t.PropertyId })
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Properties", t => t.PropertyId)
                .Index(t => t.ProductId)
                .Index(t => t.PropertyId);
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseBackId = c.Int(),
                        PurchaseId = c.Int(),
                        SaleId = c.Int(),
                        SaleBackId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .ForeignKey("dbo.SaleBacks", t => t.SaleBackId)
                .ForeignKey("dbo.PurchaseBacks", t => t.PurchaseBackId)
                .Index(t => t.ProductId)
                .Index(t => t.PurchaseBackId)
                .Index(t => t.PurchaseId)
                .Index(t => t.SaleId)
                .Index(t => t.SaleBackId);
            
            CreateTable(
                "dbo.PurchaseBacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        PurchaseId = c.Int(nullable: false),
                        PointId = c.Int(nullable: false),
                        Number = c.String(),
                        Date = c.DateTime(nullable: false),
                        Paid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShiftId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Points", t => t.PointId)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .ForeignKey("dbo.Shifts", t => t.ShiftId)
                .Index(t => t.SupplierId)
                .Index(t => t.PurchaseId)
                .Index(t => t.PointId)
                .Index(t => t.ShiftId);
            
            CreateTable(
                "dbo.Points",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Damageds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PointId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Points", t => t.PointId)
                .Index(t => t.PointId);
            
            CreateTable(
                "dbo.TransferDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Damaged_Id = c.Int(),
                        Transfer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Damageds", t => t.Damaged_Id)
                .ForeignKey("dbo.Transfers", t => t.Transfer_Id)
                .Index(t => t.ProductId)
                .Index(t => t.Damaged_Id)
                .Index(t => t.Transfer_Id);
            
            CreateTable(
                "dbo.Transfers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromPointId = c.Int(nullable: false),
                        ToPointId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Points", t => t.ToPointId)
                .ForeignKey("dbo.Points", t => t.FromPointId)
                .Index(t => t.FromPointId)
                .Index(t => t.ToPointId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentMethod = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        PointId = c.Int(nullable: false),
                        Number = c.String(),
                        Date = c.DateTime(nullable: false),
                        Paid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShiftId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Points", t => t.PointId)
                .ForeignKey("dbo.Shifts", t => t.ShiftId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .Index(t => t.SupplierId)
                .Index(t => t.PointId)
                .Index(t => t.ShiftId);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsClosed = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                        MachineId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Machines", t => t.MachineId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.MachineId);
            
            CreateTable(
                "dbo.Machines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Installments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DueDate = c.DateTime(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        LoanType = c.Int(nullable: false),
                        PurchaseId = c.Int(),
                        SaleId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .Index(t => t.PurchaseId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.SaleBacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SaleId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        PointId = c.Int(nullable: false),
                        Number = c.String(),
                        Date = c.DateTime(nullable: false),
                        Paid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShiftId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .ForeignKey("dbo.Points", t => t.PointId)
                .ForeignKey("dbo.Shifts", t => t.ShiftId)
                .Index(t => t.SaleId)
                .Index(t => t.CustomerId)
                .Index(t => t.PointId)
                .Index(t => t.ShiftId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        PointId = c.Int(nullable: false),
                        PaymentMethod = c.Int(nullable: false),
                        Number = c.String(),
                        Date = c.DateTime(nullable: false),
                        Paid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShiftId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Points", t => t.PointId)
                .ForeignKey("dbo.Shifts", t => t.ShiftId)
                .Index(t => t.CustomerId)
                .Index(t => t.PointId)
                .Index(t => t.ShiftId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PropertyCategories",
                c => new
                    {
                        Property_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Property_Id, t.Category_Id })
                .ForeignKey("dbo.Properties", t => t.Property_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Property_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.UnitCategories",
                c => new
                    {
                        Unit_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Unit_Id, t.Category_Id })
                .ForeignKey("dbo.Units", t => t.Unit_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Unit_Id)
                .Index(t => t.Category_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Products", "UnitId", "dbo.Units");
            DropForeignKey("dbo.UnitCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.UnitCategories", "Unit_Id", "dbo.Units");
            DropForeignKey("dbo.PurchaseBacks", "ShiftId", "dbo.Shifts");
            DropForeignKey("dbo.TransactionDetails", "PurchaseBackId", "dbo.PurchaseBacks");
            DropForeignKey("dbo.SaleBacks", "ShiftId", "dbo.Shifts");
            DropForeignKey("dbo.TransactionDetails", "SaleBackId", "dbo.SaleBacks");
            DropForeignKey("dbo.SaleBacks", "PointId", "dbo.Points");
            DropForeignKey("dbo.Sales", "ShiftId", "dbo.Shifts");
            DropForeignKey("dbo.TransactionDetails", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.SaleBacks", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Sales", "PointId", "dbo.Points");
            DropForeignKey("dbo.Installments", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.SaleBacks", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Installments", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.PurchaseBacks", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Purchases", "ShiftId", "dbo.Shifts");
            DropForeignKey("dbo.Shifts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Shifts", "MachineId", "dbo.Machines");
            DropForeignKey("dbo.TransactionDetails", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.PurchaseBacks", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.Purchases", "PointId", "dbo.Points");
            DropForeignKey("dbo.PurchaseBacks", "PointId", "dbo.Points");
            DropForeignKey("dbo.Transfers", "FromPointId", "dbo.Points");
            DropForeignKey("dbo.Transfers", "ToPointId", "dbo.Points");
            DropForeignKey("dbo.TransferDetails", "Transfer_Id", "dbo.Transfers");
            DropForeignKey("dbo.TransferDetails", "Damaged_Id", "dbo.Damageds");
            DropForeignKey("dbo.TransferDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Damageds", "PointId", "dbo.Points");
            DropForeignKey("dbo.TransactionDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductProperties", "PropertyId", "dbo.Properties");
            DropForeignKey("dbo.PropertyCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.PropertyCategories", "Property_Id", "dbo.Properties");
            DropForeignKey("dbo.ProductProperties", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.BarCodes", "ProductId", "dbo.Products");
            DropIndex("dbo.UnitCategories", new[] { "Category_Id" });
            DropIndex("dbo.UnitCategories", new[] { "Unit_Id" });
            DropIndex("dbo.PropertyCategories", new[] { "Category_Id" });
            DropIndex("dbo.PropertyCategories", new[] { "Property_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Sales", new[] { "ShiftId" });
            DropIndex("dbo.Sales", new[] { "PointId" });
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.SaleBacks", new[] { "ShiftId" });
            DropIndex("dbo.SaleBacks", new[] { "PointId" });
            DropIndex("dbo.SaleBacks", new[] { "CustomerId" });
            DropIndex("dbo.SaleBacks", new[] { "SaleId" });
            DropIndex("dbo.Installments", new[] { "SaleId" });
            DropIndex("dbo.Installments", new[] { "PurchaseId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Shifts", new[] { "MachineId" });
            DropIndex("dbo.Shifts", new[] { "UserId" });
            DropIndex("dbo.Purchases", new[] { "ShiftId" });
            DropIndex("dbo.Purchases", new[] { "PointId" });
            DropIndex("dbo.Purchases", new[] { "SupplierId" });
            DropIndex("dbo.Transfers", new[] { "ToPointId" });
            DropIndex("dbo.Transfers", new[] { "FromPointId" });
            DropIndex("dbo.TransferDetails", new[] { "Transfer_Id" });
            DropIndex("dbo.TransferDetails", new[] { "Damaged_Id" });
            DropIndex("dbo.TransferDetails", new[] { "ProductId" });
            DropIndex("dbo.Damageds", new[] { "PointId" });
            DropIndex("dbo.PurchaseBacks", new[] { "ShiftId" });
            DropIndex("dbo.PurchaseBacks", new[] { "PointId" });
            DropIndex("dbo.PurchaseBacks", new[] { "PurchaseId" });
            DropIndex("dbo.PurchaseBacks", new[] { "SupplierId" });
            DropIndex("dbo.TransactionDetails", new[] { "SaleBackId" });
            DropIndex("dbo.TransactionDetails", new[] { "SaleId" });
            DropIndex("dbo.TransactionDetails", new[] { "PurchaseId" });
            DropIndex("dbo.TransactionDetails", new[] { "PurchaseBackId" });
            DropIndex("dbo.TransactionDetails", new[] { "ProductId" });
            DropIndex("dbo.ProductProperties", new[] { "PropertyId" });
            DropIndex("dbo.ProductProperties", new[] { "ProductId" });
            DropIndex("dbo.BarCodes", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "UnitId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.UnitCategories");
            DropTable("dbo.PropertyCategories");
            DropTable("dbo.Settings");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Employees");
            DropTable("dbo.Units");
            DropTable("dbo.Sales");
            DropTable("dbo.Customers");
            DropTable("dbo.SaleBacks");
            DropTable("dbo.Installments");
            DropTable("dbo.Suppliers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Machines");
            DropTable("dbo.Shifts");
            DropTable("dbo.Purchases");
            DropTable("dbo.Transfers");
            DropTable("dbo.TransferDetails");
            DropTable("dbo.Damageds");
            DropTable("dbo.Points");
            DropTable("dbo.PurchaseBacks");
            DropTable("dbo.TransactionDetails");
            DropTable("dbo.Properties");
            DropTable("dbo.ProductProperties");
            DropTable("dbo.BarCodes");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
