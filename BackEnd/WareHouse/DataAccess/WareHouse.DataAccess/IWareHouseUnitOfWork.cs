using Core.Data.IRepository;
using Core.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.DataAccess.Entities;

namespace Warehouse.DataAccess
{
    /// <summary>
    /// Warehouse Unit Of Work interface.
    /// </summary>
    public interface IWareHouseUnitOfWork : IGenericUnitOfWork
    {
        /// <summary>
        /// City table repository.
        /// </summary>
        ITableGenericRepository<City> CityRepository { get; }

        /// <summary>
        /// Country table repository.
        /// </summary>
        ITableGenericRepository<Country> CountryRepository { get; }

        /// <summary>
        /// Customer table repository.
        /// </summary>
        ITableGenericRepository<Customer> CustomerRepository { get; }

        /// <summary>
        /// Customer employee table repository.
        /// </summary>
        ITableGenericRepository<CustomerEmployee> CustomerEmployeeRepository { get; }

        /// <summary>
        /// Customer store table repository.
        /// </summary>
        ITableGenericRepository<CustomerStore> CustomerStoreRepository { get; }

        /// <summary>
        /// Employee table repository.
        /// </summary>
        ITableGenericRepository<Employee> EmployeeRepository { get; }

        /// <summary>
        /// Fee table repository.
        /// </summary>
        ITableGenericRepository<Fee> FeeRepository { get; }

        /// <summary>
        /// File table repository.
        /// </summary>
        ITableGenericRepository<File> FileRepository { get; }

        /// <summary>
        /// Goods table repository.
        /// </summary>
        ITableGenericRepository<Goods> GoodsRepository { get; }

        /// <summary>
        /// Goods category table repository.
        /// </summary>
        ITableGenericRepository<GoodsCategory> GoodsCategoryRepository { get; }
        
        /// <summary>
        /// Goods issue table repository.
        /// </summary>
        ITableGenericRepository<GoodsIssue> GoodsIssueRepository { get; }
        
        /// <summary>
        /// Goods issue detail table repository.
        /// </summary>
        ITableGenericRepository<GoodsIssueDetail> GoodsIssueDetailRepository { get; }
        
        /// <summary>
        /// Goods issue location table repository.
        /// </summary>
        ITableGenericRepository<GoodsIssueLocation> GoodsIssueLocationRepository { get; }
        
        /// <summary>
        /// Goods receipt table repository.
        /// </summary>
        ITableGenericRepository<GoodsReceipt> GoodsReceiptRepository { get; }
        
        /// <summary>
        /// Goods receipt detail table repository.
        /// </summary>
        ITableGenericRepository<GoodsReceiptDetail> GoodsReceiptDetailRepository { get; }
        
        /// <summary>
        /// Goods receipt storage table repository.
        /// </summary>
        ITableGenericRepository<GoodsReceiptStorage> GoodsReceiptStorageRepository { get; }
        
        /// <summary>
        /// Goods unit table repository.
        /// </summary>
        ITableGenericRepository<GoodsUnit> GoodsUnitRepository { get; }

        /// <summary>
        /// Purchase order table repository.
        /// </summary>
        ITableGenericRepository<PurchaseOrder> PurchaseOrderRepository { get; }

        /// <summary>
        /// Purchase order delivery location table repository.
        /// </summary>
        ITableGenericRepository<PurchaseOrderDeliveryLocation> PurchaseOrderDeliveryLocationRepository { get; }

        /// <summary>
        /// Purchase order detail table repository.
        /// </summary>
        ITableGenericRepository<PurchaseOrderDetail> PurchaseOrderDetailRepository { get; }

        /// <summary>
        /// Purchase order fee table repository.
        /// </summary>
        ITableGenericRepository<PurchaseOrderFee> PurchaseOrderFeeRepository { get; }

        /// <summary>
        /// Purchase order status table repository.
        /// </summary>
        ITableGenericRepository<PurchaseOrderStatusHistory> PurchaseOrderStatusHistoryRepository { get; }

        /// <summary>
        /// Session log table repository.
        /// </summary>
        ITableGenericRepository<SessionLog> SessionLogRepository { get; }

        /// <summary>
        /// User table repository.
        /// </summary>
        ITableGenericRepository<User> UserRepository { get; }

        /// <summary>
        /// User type table repository.
        /// </summary>
        ITableGenericRepository<UserType> UserTypeRepository { get; }

        /// <summary>
        /// WareHouse table repository.
        /// </summary>
        ITableGenericRepository<WareHouse> WareHouseRepository { get; }
    }
}
