using ChamThiDotnet5.Models;
using ChamThiWeb5.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChamThiDotnet5.DAO
{
    public class AccountTypeDAO
    {
        AppDbContext DbContext = new AppDbContext();

        public int AddNewAccountType(AccountType AccountType)
        {
            int n = 0;
            try
            {
                DbContext.AccountTypes.Add(AccountType);
                n = DbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex.InnerException;
            }
            DbContext = new AppDbContext();
            return n;

        }

        public List<AccountType> ReadAllAccountType()
        {
            IQueryable<AccountType> AccountTypes = from a in DbContext.AccountTypes select a;
            foreach (AccountType AccountType in AccountTypes)
            {
                var e = DbContext.Entry(AccountType);
                e.Collection(a => a.Accounts).Load();
            }
            return AccountTypes.ToList();

        }

        public AccountType ReadAAccountType(int id)
        {
            AccountType AccountType = (from a in DbContext.AccountTypes where a.Id == id select a).FirstOrDefault();
            if (AccountType != null)
            {
                var e = DbContext.Entry(AccountType);
                e.Collection(a => a.Accounts).Load();
            }
            return AccountType;
        }

        // return false co nghia id khong ton tai
        public int UpdateAccountType(int id, AccountType NewAccountType)

        {
            int n = 0;
            AccountType AccountType = ReadAAccountType(id);
            if (AccountType == null) return n;
            AccountType = NewAccountType;

            
            try
            {
               n = DbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex.InnerException;
            }
            DbContext = new AppDbContext();
            return n;
                
            
        }
        public int DeleteAccountType(int id)
        {
            int n = 0;
            AccountType AccountType = ReadAAccountType(id);
            if (AccountType != null)
                DbContext.AccountTypes.Remove(AccountType);
            try
            {
                n = DbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex.InnerException;
            }
            DbContext = new AppDbContext();
            return n;

        }
    }
}
