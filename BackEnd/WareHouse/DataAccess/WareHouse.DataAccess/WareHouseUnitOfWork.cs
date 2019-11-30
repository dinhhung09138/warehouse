using Core.Data.IRepository;
using Core.Data.SQL.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Warehouse.DataAccess.Entities;

namespace Warehouse.DataAccess
{
    /// <summary>
    /// Warehouse unit of work
    /// </summary>
    public class WareHouseUnitOfWork : IWareHouseUnitOfWork
    {
        /// <summary>
        /// Database context.
        /// </summary>
        private readonly WareHouseContext _context;

        /// <summary>
        /// Transaction object.
        /// </summary>
        private IDbContextTransaction _transaction;

        /// <summary>
        /// Initializes a new instance of the class.
        /// Constructor.
        /// </summary>
        /// <param name="context">Internal context.</param>
        public WareHouseUnitOfWork(WareHouseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// City table repository.
        /// </summary>
        private ITableGenericRepository<City> _cityRepository;

        /// <summary>
        /// City table repository.
        /// </summary>
        public ITableGenericRepository<City> CityRepository
        {
            get
            {
                return this._cityRepository = this._cityRepository ?? new TableGenericRepository<City>(this._context);
            }
        }

        /// <summary>
        /// Country table repository.
        /// </summary>
        private ITableGenericRepository<Country> _countryRepository;

        /// <summary>
        /// Country table repository.
        /// </summary>
        public ITableGenericRepository<Country> CountryRepository
        {
            get
            {
                return this._countryRepository = this._countryRepository ?? new TableGenericRepository<Country>(this._context);
            }
        }

        /// <summary>
        /// Customer table repository.
        /// </summary>
        private ITableGenericRepository<Customer> _customerRepository;

        /// <summary>
        /// Customer table repository.
        /// </summary>
        public ITableGenericRepository<Customer> CustomerRepository
        {
            get
            {
                return this._customerRepository = this._customerRepository ?? new TableGenericRepository<Customer>(this._context);
            }
        }

        /// <summary>
        /// Customer employee table repository.
        /// </summary>
        private ITableGenericRepository<CustomerEmployee> _customerEmployeeRepository;

        /// <summary>
        /// Customer employee table repository.
        /// </summary>
        public ITableGenericRepository<CustomerEmployee> CustomerEmployeeRepository
        {
            get
            {
                return this._customerEmployeeRepository = this._customerEmployeeRepository ?? new TableGenericRepository<CustomerEmployee>(this._context);
            }
        }

        /// <summary>
        /// Customer store table repository.
        /// </summary>
        private ITableGenericRepository<CustomerStore> _customerStoreRepository;

        /// <summary>
        /// Customer store table repository.
        /// </summary>
        public ITableGenericRepository<CustomerStore> CustomerStoreRepository
        {
            get
            {
                return this._customerStoreRepository = this._customerStoreRepository ?? new TableGenericRepository<CustomerStore>(this._context);
            }
        }

        /// <summary>
        /// Employee table repository.
        /// </summary>
        private ITableGenericRepository<Employee> _employeeRepository;

        /// <summary>
        /// Employee table repository.
        /// </summary>
        public ITableGenericRepository<Employee> EmployeeRepository
        {
            get
            {
                return this._employeeRepository = this._employeeRepository ?? new TableGenericRepository<Employee>(this._context);
            }
        }

        /// <summary>
        /// Fee table repository.
        /// </summary>
        private ITableGenericRepository<Fee> _feeRepository;

        /// <summary>
        /// Fee table repository.
        /// </summary>
        public ITableGenericRepository<Fee> FeeRepository
        {
            get
            {
                return this._feeRepository = this._feeRepository ?? new TableGenericRepository<Fee>(this._context);
            }
        }

        /// <summary>
        /// File table repository.
        /// </summary>
        private ITableGenericRepository<File> _fileRepository;

        /// <summary>
        /// File table repository.
        /// </summary>
        public ITableGenericRepository<File> FileRepository
        {
            get
            {
                return this._fileRepository = this._fileRepository ?? new TableGenericRepository<File>(this._context);
            }
        }

        /// <summary>
        /// Goods table repository.
        /// </summary>
        private ITableGenericRepository<Goods> _goodsRepository;

        /// <summary>
        /// Goods table repository.
        /// </summary>
        public ITableGenericRepository<Goods> GoodsRepository
        {
            get
            {
                return this._goodsRepository = this._goodsRepository ?? new TableGenericRepository<Goods>(this._context);
            }
        }

        /// <summary>
        /// Goods category table repository.
        /// </summary>
        private ITableGenericRepository<GoodsCategory> _goodsCategoryRepository;

        /// <summary>
        /// Goods category table repository.
        /// </summary>
        public ITableGenericRepository<GoodsCategory> GoodsCategoryRepository
        {
            get
            {
                return this._goodsCategoryRepository = this._goodsCategoryRepository ?? new TableGenericRepository<GoodsCategory>(this._context);
            }
        }

        /// <summary>
        /// Goods issue table repository.
        /// </summary>
        private ITableGenericRepository<GoodsIssue> _goodsIssueRepository;

        /// <summary>
        /// Goods issue table repository.
        /// </summary>
        public ITableGenericRepository<GoodsIssue> GoodsIssueRepository
        {
            get
            {
                return this._goodsIssueRepository = this._goodsIssueRepository ?? new TableGenericRepository<GoodsIssue>(this._context);
            }
        }

        /// <summary>
        /// Goods issue detail table repository.
        /// </summary>
        private ITableGenericRepository<GoodsIssueDetail> _goodsIssueDetailRepository;

        /// <summary>
        /// Goods issue detail table repository.
        /// </summary>
        public ITableGenericRepository<GoodsIssueDetail> GoodsIssueDetailRepository
        {
            get
            {
                return this._goodsIssueDetailRepository = this._goodsIssueDetailRepository ?? new TableGenericRepository<GoodsIssueDetail>(this._context);
            }
        }

        /// <summary>
        /// Goods issue location table repository.
        /// </summary>
        private ITableGenericRepository<GoodsIssueLocation> _goodsIssueLocationRepository;

        /// <summary>
        /// Goods issue location table repository.
        /// </summary>
        public ITableGenericRepository<GoodsIssueLocation> GoodsIssueLocationRepository
        {
            get
            {
                return this._goodsIssueLocationRepository = this._goodsIssueLocationRepository ?? new TableGenericRepository<GoodsIssueLocation>(this._context);
            }
        }

        /// <summary>
        /// Goods receipt table repository.
        /// </summary>
        private ITableGenericRepository<GoodsReceipt> _goodsReceiptRepository;

        /// <summary>
        /// Goods receipt table repository.
        /// </summary>
        public ITableGenericRepository<GoodsReceipt> GoodsReceiptRepository
        {
            get
            {
                return this._goodsReceiptRepository = this._goodsReceiptRepository ?? new TableGenericRepository<GoodsReceipt>(this._context);
            }
        }

        /// <summary>
        /// Goods receipt detail table repository.
        /// </summary>
        private ITableGenericRepository<GoodsReceiptDetail> _goodsReceiptDetailRepository;

        /// <summary>
        /// Goods receipt detail table repository.
        /// </summary>
        public ITableGenericRepository<GoodsReceiptDetail> GoodsReceiptDetailRepository
        {
            get
            {
                return this._goodsReceiptDetailRepository = this._goodsReceiptDetailRepository ?? new TableGenericRepository<GoodsReceiptDetail>(this._context);
            }
        }

        /// <summary>
        /// Goods receipt storage table repository.
        /// </summary>
        private ITableGenericRepository<GoodsReceiptStorage> _goodsReceiptStorageRepository;

        /// <summary>
        /// Goods receipt storage table repository.
        /// </summary>
        public ITableGenericRepository<GoodsReceiptStorage> GoodsReceiptStorageRepository
        {
            get
            {
                return this._goodsReceiptStorageRepository = this._goodsReceiptStorageRepository ?? new TableGenericRepository<GoodsReceiptStorage>(this._context);
            }
        }

        /// <summary>
        /// Goods unit table repository.
        /// </summary>
        private ITableGenericRepository<GoodsUnit> _goodsUnitRepository;

        /// <summary>
        /// Goods unit table repository.
        /// </summary>
        public ITableGenericRepository<GoodsUnit> GoodsUnitRepository
        {
            get
            {
                return this._goodsUnitRepository = this._goodsUnitRepository ?? new TableGenericRepository<GoodsUnit>(this._context);
            }
        }

        /// <summary>
        /// Purchase order table repository.
        /// </summary>
        private ITableGenericRepository<PurchaseOrder> _purchaseOrderRepository;

        /// <summary>
        /// Purchase order table repository.
        /// </summary>
        public ITableGenericRepository<PurchaseOrder> PurchaseOrderRepository
        {
            get
            {
                return this._purchaseOrderRepository = this._purchaseOrderRepository ?? new TableGenericRepository<PurchaseOrder>(this._context);
            }
        }

        /// <summary>
        /// Purchase order delivery location table repository.
        /// </summary>
        private ITableGenericRepository<PurchaseOrderDeliveryLocation> _purchaseOrderDeliveryLocationRepository;

        /// <summary>
        /// Purchase order delivery location table repository.
        /// </summary>
        public ITableGenericRepository<PurchaseOrderDeliveryLocation> PurchaseOrderDeliveryLocationRepository
        {
            get
            {
                return this._purchaseOrderDeliveryLocationRepository = this._purchaseOrderDeliveryLocationRepository ?? new TableGenericRepository<PurchaseOrderDeliveryLocation>(this._context);
            }
        }

        /// <summary>
        /// Purchase order detail table repository.
        /// </summary>
        private ITableGenericRepository<PurchaseOrderDetail> _purchaseOrderDetailRepository;

        /// <summary>
        /// Purchase order detail table repository.
        /// </summary>
        public ITableGenericRepository<PurchaseOrderDetail> PurchaseOrderDetailRepository
        {
            get
            {
                return this._purchaseOrderDetailRepository = this._purchaseOrderDetailRepository ?? new TableGenericRepository<PurchaseOrderDetail>(this._context);
            }
        }

        /// <summary>
        /// Purchase order fee table repository.
        /// </summary>
        private ITableGenericRepository<PurchaseOrderFee> _purchaseOrderFeeRepository;

        /// <summary>
        /// Purchase order fee table repository.
        /// </summary>
        public ITableGenericRepository<PurchaseOrderFee> PurchaseOrderFeeRepository
        {
            get
            {
                return this._purchaseOrderFeeRepository = this._purchaseOrderFeeRepository ?? new TableGenericRepository<PurchaseOrderFee>(this._context);
            }
        }

        /// <summary>
        /// Purchase order status table repository.
        /// </summary>
        private ITableGenericRepository<PurchaseOrderStatusHistory> _purchaseOrderStatusHistoryRepository;

        /// <summary>
        /// Purchase order status table repository.
        /// </summary>
        public ITableGenericRepository<PurchaseOrderStatusHistory> PurchaseOrderStatusHistoryRepository
        {
            get
            {
                return this._purchaseOrderStatusHistoryRepository = this._purchaseOrderStatusHistoryRepository ?? new TableGenericRepository<PurchaseOrderStatusHistory>(this._context);
            }
        }

        /// <summary>
        /// Session log table repository.
        /// </summary>
        private ITableGenericRepository<SessionLog> _sessionLogRepository;

        /// <summary>
        /// Session log table repository.
        /// </summary>
        public ITableGenericRepository<SessionLog> SessionLogRepository
        {
            get
            {
                return this._sessionLogRepository = this._sessionLogRepository ?? new TableGenericRepository<SessionLog>(this._context);
            }
        }

        /// <summary>
        /// User table repository.
        /// </summary>
        private ITableGenericRepository<User> _userRepository;

        /// <summary>
        /// User table repository.
        /// </summary>
        public ITableGenericRepository<User> UserRepository
        {
            get
            {
                return this._userRepository = this._userRepository ?? new TableGenericRepository<User>(this._context);
            }
        }

        /// <summary>
        /// User type table repository.
        /// </summary>
        private ITableGenericRepository<UserType> _userTypeRepository;

        /// <summary>
        /// User type table repository.
        /// </summary>
        public ITableGenericRepository<UserType> UserTypeRepository
        {
            get
            {
                return this._userTypeRepository = this._userTypeRepository ?? new TableGenericRepository<UserType>(this._context);
            }
        }

        /// <summary>
        /// WareHouse table repository.
        /// </summary>
        private ITableGenericRepository<WareHouse> _wareHouseRepository;

        /// <summary>
        /// WareHouse table repository.
        /// </summary>
        public ITableGenericRepository<WareHouse> WareHouseRepository
        {
            get
            {
                return this._wareHouseRepository = this._wareHouseRepository ?? new TableGenericRepository<WareHouse>(this._context);
            }
        }

        /// <summary>
        /// Start transaction.
        /// </summary>
        public void BeginTransaction()
        {
            this._transaction = _context.Database.BeginTransaction();
        }

        /// <summary>
        /// Start transaction.
        /// Using asynchonous
        /// </summary>
        public async Task BeginTransactionAsync()
        {
            this._transaction = await this._context.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// Commit transaction method.
        /// </summary>
        public void CommitTransaction()
        {
            this._transaction?.Commit();
        }

        /// <summary>
        /// Rollback transaction method.
        /// </summary>
        public void RollbackTransaction()
        {
            this._transaction.Rollback();
        }

        /// <summary>
        /// Submit all change on entity framework to database.
        /// </summary>
        /// <returns>-1: error, 0: no row effect, 1: success.</returns>
        public int SaveChanges()
        {
            return this._context.SaveChanges();
        }

        /// <summary>
        /// Submit all change on entity framework to database.
        /// Using asynchonous
        /// </summary>
        /// <returns>-1: error, 0: no row effect, 1: success.</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Disponse and close transaction.
        /// </summary>
        public void Dispose()
        {
            if (this._context != null)
            {
                if (this._transaction != null)
                {
                    this._transaction.Dispose();
                }

                _context.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
