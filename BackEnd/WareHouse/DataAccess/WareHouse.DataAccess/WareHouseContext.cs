using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.DataAccess.Entities;

namespace Warehouse.DataAccess
{

    /// <summary>
    /// Warehouse context.
    /// </summary>
    public class WareHouseContext : DbContext
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">DbContextOptions.</param>
        public WareHouseContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// City table.
        /// </summary>
        public virtual DbSet<City> City { get; set; }

        /// <summary>
        /// Country table.
        /// </summary>
        public virtual DbSet<Country> Country { get; set; }

        /// <summary>
        /// Customer table.
        /// </summary>
        public virtual DbSet<Customer> Customer { get; set; }

        /// <summary>
        /// Customer employee table.
        /// </summary>
        public virtual DbSet<CustomerEmployee> CustomerEmployee { get; set; }

        /// <summary>
        /// Customer store table.
        /// </summary>
        public virtual DbSet<CustomerStore> CustomerStore { get; set; }

        /// <summary>
        /// Employee table.
        /// </summary>
        public virtual DbSet<Employee> Employee { get; set; }

        /// <summary>
        /// Fee table.
        /// </summary>
        public virtual DbSet<Fee> Fee { get; set; }

        /// <summary>
        /// File table.
        /// </summary>
        public virtual DbSet<File> File { get; set; }

        /// <summary>
        /// Goods table.
        /// </summary>
        public virtual DbSet<Goods> Goods { get; set; }

        /// <summary>
        /// Goods category table.
        /// </summary>
        public virtual DbSet<GoodsCategory> GoodsCategory { get; set; }

        /// <summary>
        /// Goods issue table.
        /// </summary>
        public virtual DbSet<GoodsIssue> GoodsIssue { get; set; }

        /// <summary>
        /// Goods Issue detail table.
        /// </summary>
        public virtual DbSet<GoodsIssueDetail> GoodsIssueDetail { get; set; }

        /// <summary>
        /// Goods issue location table.
        /// </summary>
        public virtual DbSet<GoodsIssueLocation> GoodsIssueLocation { get; set; }

        /// <summary>
        /// Good receipt table.
        /// </summary>
        public virtual DbSet<GoodsReceipt> GoodsReceipt { get; set; }

        /// <summary>
        /// Goods receipt detail table.
        /// </summary>
        public virtual DbSet<GoodsReceiptDetail> GoodsReceiptDetail { get; set; }

        /// <summary>
        /// Goods receipt storage table.
        /// </summary>
        public virtual DbSet<GoodsReceiptStorage> GoodsReceiptStorage { get; set; }

        /// <summary>
        /// Goods unit table.
        /// </summary>
        public virtual DbSet<GoodsUnit> GoodsUnit { get; set; }

        /// <summary>
        /// Purchase order table.
        /// </summary>
        public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }

        /// <summary>
        /// Purchase order delivery location table
        /// </summary>
        public virtual DbSet<PurchaseOrderDeliveryLocation> PurchaseOrderDeliveryLocation { get; set; }

        /// <summary>
        /// Purchase order detail table
        /// </summary>
        public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }

        /// <summary>
        /// Purchase order fee table.
        /// </summary>
        public virtual DbSet<PurchaseOrderFee> PurchaseOrderFee { get; set; }

        /// <summary>
        /// Purchase Order Status History table.
        /// </summary>
        public virtual DbSet<PurchaseOrderStatusHistory> PurchaseOrderStatusHistory { get; set; }

        /// <summary>
        /// Session log table.
        /// </summary>
        public virtual DbSet<SessionLog> SessionLog { get; set; }

        /// <summary>
        /// User table.
        /// </summary>
        public virtual DbSet<User> User { get; set; }

        /// <summary>
        /// UserType table.
        /// </summary>
        public virtual DbSet<UserType> UserType { get; set; }

        /// <summary>
        /// WareHouse table.
        /// </summary>
        public virtual DbSet<WareHouse> WareHouse { get; set; }

        /// <summary>
        /// Model creating.
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            MappingCityTable(modelBuilder);
            MappingCountryTable(modelBuilder);
            MappingCustomerTable(modelBuilder);
            MappingCustomerEmployeeTable(modelBuilder);
            MappingCustomerStoreTable(modelBuilder);
            MappingEmployeeTable(modelBuilder);
            MappingFeeTable(modelBuilder);
            MappingFileTable(modelBuilder);
            MappingGoodsTable(modelBuilder);
            MappingGoodsCategoryTable(modelBuilder);
            MappingGoodsIssueTable(modelBuilder);
            MappingGoodsIssueDetailTable(modelBuilder);
            MappingGoodsIssueLocationTable(modelBuilder);
            MappingGoodsReceiptTable(modelBuilder);
            MappingGoodsReceiptDetailTable(modelBuilder);
            MappingGoodsReceiptStorageTable(modelBuilder);
            MappingGoodsUnitTable(modelBuilder);
            MappingPurchaseOrderTable(modelBuilder);
            MappingPurchaseOrderDeliveryLocationTable(modelBuilder);
            MappingPurchaseOrderDetailTable(modelBuilder);
            MappingPurchaseOrderFeeTable(modelBuilder);
            MappingPurchaseOrderStatusHistoryTable(modelBuilder);
            MappingSessionLogTable(modelBuilder);
            MappingUserTable(modelBuilder);
            MappingUserTypeTable(modelBuilder);
            MappingWareHouseTable(modelBuilder);
        }

        private void MappingCityTable(ModelBuilder builder)
        {
            builder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.HasIndex(e => e.Code)
                            .IsUnique()
                            .HasName("City_IDX_Code");

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.CountryId)
                            .HasColumnName("CountryId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Code)
                            .HasColumnName("Code")
                            .HasMaxLength(20)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.Name)
                            .HasColumnName("Name")
                            .HasMaxLength(200)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

        private void MappingCountryTable(ModelBuilder builder)
        {
            builder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.HasIndex(e => e.Code)
                            .IsUnique()
                            .HasName("Country_IDX_Code");

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Code)
                            .HasColumnName("Code")
                            .HasMaxLength(20)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.Name)
                            .HasColumnName("Name")
                            .HasMaxLength(200)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

        private void MappingCustomerTable(ModelBuilder builder)
        {
            builder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Name)
                            .HasColumnName("Name")
                            .HasMaxLength(300)
                            .IsRequired(true)
                            .IsUnicode(true);

                entity.Property(e => e.LogoFileId)
                            .HasColumnName("LogoFileId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(false);

                entity.Property(e => e.PrimaryPhone)
                            .HasColumnName("PrimaryPhone")
                            .HasMaxLength(20)
                            .IsUnicode(false);

                entity.Property(e => e.SecondaryPhone)
                            .HasColumnName("SecondaryPhone")
                            .HasMaxLength(20)
                            .IsUnicode(false);

                entity.Property(e => e.Fax)
                            .HasColumnName("Fax")
                            .HasMaxLength(20)
                            .IsUnicode(false);

                entity.Property(e => e.Website)
                            .HasColumnName("Website")
                            .HasMaxLength(50)
                            .IsUnicode(false);

                entity.Property(e => e.TaxCode)
                            .HasColumnName("TaxCode")
                            .HasMaxLength(50)
                            .IsUnicode(false);

                entity.Property(e => e.IsCompany)
                            .HasColumnName("IsCompany")
                            .HasMaxLength(20)
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.StartOn)
                            .HasColumnName("StartOn")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.Description)
                            .HasColumnName("Description")
                            .IsUnicode(true);

                entity.Property(e => e.Address)
                            .HasColumnName("Address")
                            .HasMaxLength(300)
                            .IsUnicode(true);

                entity.Property(e => e.CitiId)
                            .HasColumnName("CitiId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(false);

                entity.Property(e => e.CountryId)
                            .HasColumnName("CountryId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(false);

                entity.Property(e => e.Longtitue)
                            .HasColumnName("Longtitue");

                entity.Property(e => e.Latitude)
                            .HasColumnName("Latitude");

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

        private void MappingCustomerEmployeeTable(ModelBuilder builder)
        {
            builder.Entity<CustomerEmployee>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Code)
                            .HasColumnName("Code")
                            .HasMaxLength(20)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.Name)
                            .HasColumnName("Name")
                            .HasMaxLength(300)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.AvatarFileId)
                            .HasColumnName("AvatarFileId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(false);

                entity.Property(e => e.Phone)
                            .HasColumnName("Phone")
                            .HasMaxLength(20)
                            .IsUnicode(false);

                entity.Property(e => e.Email)
                            .HasColumnName("Email")
                            .HasMaxLength(50)
                            .IsUnicode(false);

                entity.Property(e => e.UserName)
                            .HasColumnName("UserName")
                            .HasMaxLength(50)
                            .IsUnicode(false);

                entity.Property(e => e.Password)
                            .HasColumnName("Password")
                            .HasMaxLength(250)
                            .IsRequired(false);

                entity.Property(e => e.StartOn)
                            .HasColumnName("StartOn")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.CustomerStoreId)
                            .HasColumnName("CustomerStoreId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(false);

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

        private void MappingCustomerStoreTable(ModelBuilder builder)
        {
            builder.Entity<CustomerStore>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Name)
                            .HasColumnName("Name")
                            .HasMaxLength(300)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.PrimaryPhone)
                            .HasColumnName("PrimaryPhone")
                            .HasMaxLength(20)
                            .IsUnicode(false);

                entity.Property(e => e.SecondaryPhone)
                            .HasColumnName("SecondaryPhone")
                            .HasMaxLength(20)
                            .IsUnicode(false);

                entity.Property(e => e.Fax)
                            .HasColumnName("Fax")
                            .HasMaxLength(20)
                            .IsUnicode(false);

                entity.Property(e => e.StoreManagerId)
                            .HasColumnName("StoreManagerId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(false);

                entity.Property(e => e.StartOn)
                            .HasColumnName("StartOn")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.Description)
                            .HasColumnName("Description")
                            .IsUnicode(true);

                entity.Property(e => e.Address)
                            .HasColumnName("Address")
                            .HasMaxLength(300)
                            .IsUnicode(true);

                entity.Property(e => e.CitiId)
                            .HasColumnName("CitiId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(false);

                entity.Property(e => e.CountryId)
                            .HasColumnName("CountryId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(false);

                entity.Property(e => e.Longtitue)
                            .HasColumnName("Longtitue");

                entity.Property(e => e.Latitude)
                            .HasColumnName("Latitude");

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

        private void MappingEmployeeTable(ModelBuilder builder)
        {
            builder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.HasIndex(e => e.Code)
                            .IsUnique()
                            .HasName("Employee_IDX_Code");

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Code)
                            .HasColumnName("Code")
                            .HasMaxLength(20)
                            .IsUnicode(false);

                entity.Property(e => e.Name)
                            .HasColumnName("Name")
                            .HasMaxLength(200)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.AvatarFileId)
                            .HasColumnName("AvatarFileId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(false);

                entity.Property(e => e.Mobile)
                            .HasColumnName("Mobile")
                            .HasMaxLength(20)
                            .IsUnicode(false);

                entity.Property(e => e.WorkPhone)
                            .HasColumnName("WorkPhone")
                            .HasMaxLength(20)
                            .IsUnicode(false);

                entity.Property(e => e.Fax)
                            .HasColumnName("Fax")
                            .HasMaxLength(20)
                            .IsUnicode(false);

                entity.Property(e => e.DateOfJoin)
                            .HasColumnName("DateOfJoin")
                            .HasColumnType("datetime")
                            .IsRequired(true);

                entity.Property(e => e.DateOfLeaving)
                            .HasColumnName("DateOfLeaving")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.Email)
                            .HasColumnName("Email")
                            .HasMaxLength(50)
                            .IsUnicode(false);

                entity.Property(e => e.DepartmentId)
                            .HasColumnName("DepartmentId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(false);

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

        private void MappingFeeTable(ModelBuilder builder)
        {
            builder.Entity<Fee>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Name)
                            .HasColumnName("Name")
                            .HasMaxLength(200)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.Value)
                            .HasColumnName("Fee")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

        private void MappingFileTable(ModelBuilder builder)
        {
            builder.Entity<File>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.FileName)
                            .HasColumnName("FileName")
                            .HasMaxLength(150)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.FileSystemName)
                            .HasColumnName("FileSystemName")
                            .HasMaxLength(100)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.FilePath)
                            .HasColumnName("FilePath")
                            .HasMaxLength(300)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.Size)
                            .HasColumnName("Size");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");
            });
        }

        private void MappingGoodsTable(ModelBuilder builder)
        {
            builder.Entity<Goods>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.HasIndex(e => e.Code)
                            .IsUnique()
                            .HasName("Goods_IDX_Code");

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Code)
                            .HasColumnName("Code")
                            .HasMaxLength(20)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.Name)
                            .HasColumnName("Name")
                            .HasMaxLength(200)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.Brand)
                            .HasColumnName("Brand")
                            .HasMaxLength(200)
                            .IsUnicode(true);

                entity.Property(e => e.Color)
                            .HasColumnName("Color")
                            .HasMaxLength(200)
                            .IsUnicode(true);

                entity.Property(e => e.Size)
                            .HasColumnName("Size")
                            .HasMaxLength(200)
                            .IsUnicode(true);

                entity.Property(e => e.FileId)
                            .HasColumnName("FileId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(false);

                entity.Property(e => e.Description)
                            .HasColumnName("Description")
                            .HasMaxLength(500)
                            .IsUnicode(true);

                entity.Property(e => e.UnitId)
                            .HasColumnName("UnitId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.GoodsCategoryId)
                            .HasColumnName("GoodsCategoryId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

        private void MappingGoodsCategoryTable(ModelBuilder builder)
        {
            builder.Entity<GoodsCategory>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Name)
                            .HasColumnName("Name")
                            .HasMaxLength(200)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.Description)
                            .HasColumnName("Description")
                            .HasMaxLength(500)
                            .IsRequired(true)
                            .IsUnicode(true);

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

        private void MappingGoodsIssueTable(ModelBuilder builder)
        {
            builder.Entity<GoodsIssue>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.HasIndex(e => e.IssueCode)
                            .IsUnique()
                            .HasName("GoodsIssue_UNI_IssueCode");

                entity.Property(e => e.IssueCode)
                            .HasColumnName("Id")
                            .HasColumnType("IssueCode")
                            .IsRequired(true);

                entity.Property(e => e.OrderNumber)
                            .HasColumnName("OrderNumber")
                            .HasMaxLength(20)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                            .HasColumnName("CustomerId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.EmployeId)
                            .HasColumnName("EmployeId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.WarehouseId)
                            .HasColumnName("WarehouseId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Date)
                            .HasColumnName("Date")
                            .IsRequired(true);

                entity.Property(e => e.Notes)
                            .HasColumnName("Notes")
                            .HasMaxLength(1000)
                            .IsUnicode(true);

                entity.Property(e => e.IsIssue)
                            .HasColumnName("IsIssue")
                            .IsUnicode(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsTransfer)
                            .HasColumnName("IsTransfer")
                            .IsUnicode(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

        private void MappingGoodsIssueDetailTable(ModelBuilder builder)
        {
            builder.Entity<GoodsIssueDetail>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.IssueId)
                            .HasColumnName("IssueId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.WarehouseId)
                            .HasColumnName("WarehouseId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.GoodsId)
                            .HasColumnName("GoodsId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Quantity)
                            .HasColumnName("Quantity")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.Cost)
                            .HasColumnName("Cost")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.Notes)
                            .HasColumnName("Notes")
                            .HasMaxLength(1000)
                            .IsUnicode(true);
            });
        }

        private void MappingGoodsIssueLocationTable(ModelBuilder builder)
        {
            builder.Entity<GoodsIssueLocation>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.IssueId)
                            .HasColumnName("IssueId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.CustomerId)
                            .HasColumnName("CustomerId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.CustomerStoreId)
                            .HasColumnName("CustomerStoreId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(false);

                entity.Property(e => e.WareHouseId)
                            .HasColumnName("WareHouseId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(false);

                entity.Property(e => e.Date)
                            .HasColumnName("Date")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TotalCost)
                            .HasColumnName("TotalCost")
                            .HasMaxLength(20)
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.Notes)
                            .HasColumnName("Notes")
                            .HasMaxLength(1000)
                            .IsUnicode(false);
            });
        }

        private void MappingGoodsReceiptTable(ModelBuilder builder)
        {
            builder.Entity<GoodsReceipt>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.HasIndex(e => e.ReceiptCode)
                            .IsUnique()
                            .HasName("GoodsReceipt_UNI_ReceiptCode");

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.ReceiptCode)
                            .HasColumnName("ReceiptCode")
                            .HasMaxLength(20)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.OrderNumber)
                            .HasColumnName("OrderNumber")
                            .HasMaxLength(20)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                            .HasColumnName("CustomerId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Date)
                            .HasColumnName("Date")
                            .IsRequired(true);

                entity.Property(e => e.Notes)
                            .HasColumnName("Notes")
                            .HasMaxLength(1000)
                            .IsUnicode(true);

                entity.Property(e => e.IsReceipt)
                            .HasColumnName("IsReceipt")
                            .IsUnicode(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsTransfer)
                            .HasColumnName("IsTransfer")
                            .IsUnicode(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsTurnBack)
                            .HasColumnName("IsTurnBack")
                            .IsUnicode(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

        private void MappingGoodsReceiptDetailTable(ModelBuilder builder)
        {
            builder.Entity<GoodsReceiptDetail>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.ReceiptId)
                            .HasColumnName("ReceiptId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.WareHouseId)
                            .HasColumnName("WareHouseId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.GoodsId)
                            .HasColumnName("GoodsId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Quantity)
                            .HasColumnName("Quantity")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.Cost)
                            .HasColumnName("Cost")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.Notes)
                            .HasColumnName("Notes")
                            .HasMaxLength(1000)
                            .IsUnicode(true);
            });
        }

        private void MappingGoodsReceiptStorageTable(ModelBuilder builder)
        {
            builder.Entity<GoodsReceiptStorage>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.ReceiptId)
                            .HasColumnName("ReceiptId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.EmployeeId)
                            .HasColumnName("EmployeeId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.WarehouseId)
                            .HasColumnName("WarehouseId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Date)
                            .HasColumnName("Date")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TotalCost)
                            .HasColumnName("TotalCost")
                            .HasMaxLength(20)
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.Notes)
                            .HasColumnName("Notes")
                            .HasMaxLength(1000)
                            .IsUnicode(false);
            });
        }

        private void MappingGoodsUnitTable(ModelBuilder builder)
        {
            builder.Entity<GoodsUnit>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.HasIndex(e => e.Code)
                            .IsUnique()
                            .HasName("GoodsUnit_UNI_Code");

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Code)
                            .HasColumnName("Code")
                            .HasMaxLength(20)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.Name)
                            .HasColumnName("Name")
                            .HasMaxLength(200)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

        private void MappingPurchaseOrderTable(ModelBuilder builder)
        {
            builder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.HasIndex(e => e.OrderNumber)
                            .IsUnique()
                            .HasName("PurchaseOrder_UNI_OrderNumber");

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.OrderNumber)
                            .HasColumnName("OrderNumber")
                            .HasMaxLength(20)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                            .HasColumnName("CustomerId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.EmployeId)
                            .HasColumnName("EmployeId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Date)
                            .HasColumnName("Date")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes)
                            .HasColumnName("Notes")
                            .HasMaxLength(1000)
                            .IsUnicode(true);

                entity.Property(e => e.Status)
                            .HasColumnName("Status")
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

        private void MappingPurchaseOrderDeliveryLocationTable(ModelBuilder builder)
        {
            builder.Entity<PurchaseOrderDeliveryLocation>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.PurchaseOrderId)
                            .HasColumnName("PurchaseOrderId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.CustomerStoreId)
                            .HasColumnName("CustomerStoreId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Address)
                            .HasColumnName("Address")
                            .HasMaxLength(250)
                            .IsRequired(true)
                            .IsUnicode(true);

                entity.Property(e => e.ContactName)
                            .HasColumnName("ContactName")
                            .HasMaxLength(100)
                            .IsUnicode(true);

                entity.Property(e => e.ContactPhone)
                            .HasColumnName("ContactPhone")
                            .HasMaxLength(20)
                            .IsUnicode(false);

                entity.Property(e => e.TotalCost)
                            .HasColumnName("TotalCost")
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.Notes)
                            .HasColumnName("Notes")
                            .HasMaxLength(1000)
                            .IsUnicode(true);

            });
        }

        private void MappingPurchaseOrderDetailTable(ModelBuilder builder)
        {
            builder.Entity<PurchaseOrderDetail>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.PurchaseOrderId)
                            .HasColumnName("PurchaseOrderId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.WareHouseId)
                            .HasColumnName("WareHouseId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.GoodsId)
                            .HasColumnName("GoodsId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Quantity)
                            .HasColumnName("Quantity")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.Notes)
                            .HasColumnName("Notes")
                            .HasMaxLength(1000)
                            .IsUnicode(true);
            });
        }

        private void MappingPurchaseOrderFeeTable(ModelBuilder builder)
        {
            builder.Entity<PurchaseOrderFee>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.PurchaseOrderId)
                            .HasColumnName("PurchaseOrderId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Name)
                            .HasColumnName("Name")
                            .HasMaxLength(250)
                            .IsRequired(true)
                            .IsUnicode(true);

                entity.Property(e => e.Fee)
                            .HasColumnName("Fee")
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.Notes)
                            .HasColumnName("Notes")
                            .HasMaxLength(1000)
                            .IsUnicode(true);
            });
        }

        private void MappingPurchaseOrderStatusHistoryTable(ModelBuilder builder)
        {
            builder.Entity<PurchaseOrderStatusHistory>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.PurchaseOrderId)
                            .HasColumnName("PurchaseOrderId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.UserId)
                            .HasColumnName("UserId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.IsEmployee)
                            .HasColumnName("IsEmployee")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                            .HasColumnName("Status")
                            .IsRequired(true);

                entity.Property(e => e.Date)
                            .HasColumnName("Date")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");
            });
        }

        private void MappingSessionLogTable(ModelBuilder builder)
        {
            builder.Entity<SessionLog>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Token)
                            .HasColumnName("Token")
                            .HasMaxLength(500)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.LoginTime)
                            .HasColumnName("LoginTime")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LogoutTime)
                            .HasColumnName("LogoutTime")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.ExpirationTime)
                            .HasColumnName("ExpirationTime")
                            .HasColumnType("datetime")
                            .IsRequired(true);

                entity.Property(e => e.IsOnline)
                            .HasColumnName("IsOnline")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.IPAddress)
                            .HasColumnName("IPAddress")
                            .HasMaxLength(50)
                            .IsUnicode(true);

                entity.Property(e => e.Platform)
                            .HasColumnName("Platform")
                            .HasMaxLength(100)
                            .IsUnicode(true)
                            .IsUnicode(true);

                entity.Property(e => e.Browser)
                            .HasColumnName("Browser")
                            .HasMaxLength(100)
                            .IsUnicode(true);

                entity.Property(e => e.OSName)
                            .HasColumnName("OSName")
                            .HasMaxLength(100)
                            .IsUnicode(true);

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(true);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion).IsRowVersion();
            });
        }

        private void MappingUserTable(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.HasIndex(e => e.UserName)
                            .IsUnique()
                            .HasName("USER_UNI_UserName");

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.EmployeeId)
                            .HasColumnName("EmployeeId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.UserTypeId)
                            .HasColumnName("UserTypeId")
                            .IsRequired(true);

                entity.Property(e => e.UserName)
                            .HasColumnName("UserName")
                            .HasMaxLength(40)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.Password)
                            .HasColumnName("Password")
                            .HasMaxLength(255)
                            .IsRequired(true)
                            .IsUnicode(false);

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastLogin)
                            .HasColumnName("LastLogin")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

        private void MappingUserTypeTable(ModelBuilder builder)
        {
            builder.Entity<UserType>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .IsRequired(true);

                entity.Property(e => e.Name)
                            .HasColumnName("Name")
                            .HasMaxLength(200)
                            .IsRequired(true)
                            .IsUnicode(true);

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

        private void MappingWareHouseTable(ModelBuilder builder)
        {
            builder.Entity<WareHouse>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(true);

                entity.Property(e => e.Id)
                            .HasColumnName("Id")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.Name)
                            .HasColumnName("Name")
                            .HasMaxLength(300)
                            .IsRequired(true)
                            .IsUnicode(true);

                entity.Property(e => e.Phone)
                            .HasColumnName("Phone")
                            .HasMaxLength(20)
                            .IsUnicode(false);

                entity.Property(e => e.Fax)
                            .HasColumnName("Fax")
                            .HasMaxLength(20)
                            .IsUnicode(false);

                entity.Property(e => e.Size)
                            .HasColumnName("Size")
                            .HasMaxLength(40)
                            .IsUnicode(true);

                entity.Property(e => e.Description)
                            .HasColumnName("Description")
                            .IsUnicode(true);

                entity.Property(e => e.Address)
                            .HasColumnName("Address")
                            .HasMaxLength(300)
                            .IsUnicode(true);

                entity.Property(e => e.CitiId)
                            .HasColumnName("CitiId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(false);

                entity.Property(e => e.CountryId)
                            .HasColumnName("CountryId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(false);

                entity.Property(e => e.Longtitue)
                            .HasColumnName("Longtitue");

                entity.Property(e => e.Latitude)
                            .HasColumnName("Latitude");

                entity.Property(e => e.StockKeeperId)
                            .HasColumnName("StockKeeperId")
                            .HasColumnType("uniqueidentifier")
                            .IsRequired(true);

                entity.Property(e => e.IsActive)
                            .HasColumnName("IsActive")
                            .IsRequired(true)
                            .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateBy)
                            .HasColumnName("CreateBy")
                            .HasMaxLength(50)
                            .IsRequired(true);

                entity.Property(e => e.CreateDate)
                            .HasColumnName("CreateDate")
                            .HasColumnType("datetime")
                            .IsRequired(true)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy)
                            .HasColumnName("UpdateBy")
                            .HasMaxLength(50)
                            .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                            .HasColumnName("UpdateDate")
                            .HasColumnType("datetime")
                            .IsRequired(false)
                            .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                            .HasColumnName("Deleted")
                            .IsRequired(true)
                            .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeleteBy)
                            .HasColumnName("DeleteBy")
                            .HasColumnType("varchar(50)")
                            .IsRequired(false);

                entity.Property(e => e.DeleteDate)
                            .HasColumnName("DeleteDate")
                            .HasColumnType("datetime")
                            .IsRequired(false);

                entity.Property(e => e.RowVersion)
                            .HasColumnName("RowVersion")
                            .IsRequired()
                            .IsRowVersion();
            });
        }

    }
}
