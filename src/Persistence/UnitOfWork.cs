﻿using Phony.Kernel;
using Phony.Kernel.Repositories;
using Phony.Persistence.Repositories;
using System.Threading.Tasks;

namespace Phony.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PhonyDbContext _context;

        public UnitOfWork(PhonyDbContext context)
        {
            _context = context;
            Bills = new BillRepo(_context);
            BillsItemsMoves = new BillItemMoveRepo(_context);
            BillsServicesMoves = new BillServiceMoveRepo(_context);
            Clients = new ClientRepo(_context);
            ClientsMoves = new ClientMoveRepo(_context);
            Companies = new CompanyRepo(_context);
            CompaniesMoves = new CompanyMoveRepo(_context);
            Items = new ItemRepo(_context);
            Notes = new NoteRepo(_context);
            SalesMen = new SalesManRepo(_context);
            SalesMenMoves = new SalesManMoveRepo(_context);
            Services = new ServiceRepo(_context);
            ServicesMoves = new ServiceMoveRepo(_context);
            Stores = new StoreRepo(_context);
            Suppliers = new SupplierRepo(_context);
            SuppliersMoves = new SupplierMoveRepo(_context);
            Treasuries = new TreasuryRepo(_context);
            TreasuriesMoves = new TreasuryMoveRepo(_context);
            Users = new UserRepo(_context);
        }

        public IBillRepo Bills { get; private set; }
        public IBillItemMoveRepo BillsItemsMoves { get; private set; }
        public IBillServiceMoveRepo BillsServicesMoves { get; private set; }
        public IClientRepo Clients { get; private set; }
        public IClientMoveRepo ClientsMoves { get; private set; }
        public ICompanyRepo Companies { get; private set; }
        public ICompanyMoveRepo CompaniesMoves { get; private set; }
        public IItemRepo Items { get; private set; }
        public INoteRepo Notes { get; private set; }
        public ISalesManRepo SalesMen { get; private set; }
        public ISalesManMoveRepo SalesMenMoves { get; private set; }
        public IServiceRepo Services { get; private set; }
        public IServiceMoveRepo ServicesMoves { get; private set; }
        public IStoreRepo Stores { get; private set; }
        public ISupplierRepo Suppliers { get; private set; }
        public ISupplierMoveRepo SuppliersMoves { get; private set; }
        public ITreasuryRepo Treasuries{ get; private set; }
        public ITreasuryMoveRepo TreasuriesMoves { get; private set; }
        public IUserRepo Users { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}