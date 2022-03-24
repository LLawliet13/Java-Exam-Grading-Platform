using ChamThiDotnet5.Models;
using ChamThiWeb5.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChamThiDotnet5.DAO
{
    public class AccountDAO
    {
        AppDbContext DbContext = new AppDbContext();

        public int AddNewAccount(Account Account)
        {
            int n = 0;
            try
            {
                DbContext.Accounts.Add(Account);
                n = DbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex.InnerException;
            }
            DbContext = new AppDbContext();
            return n;

        }

        public List<Account> ReadAllAccount()
        {
            IQueryable<Account> Accounts = from a in DbContext.Accounts select a;

            //reference navigation

            foreach (Account Account in Accounts)
            {
                var e = DbContext.Entry(Account);
                e.Reference(a => a.AccountType).Load();
                e.Reference(a => a.Student).Load();
                e.Reference(a => a.Teacher).Load();

            }
            return Accounts.ToList();

        }

        public Account ReadAAccount(int id)
        {
            Account Account = (from a in DbContext.Accounts where a.Id == id select a).FirstOrDefault();
            if (Account != null)
            {
                var e = DbContext.Entry(Account);
                e.Reference(a => a.AccountType).Load();
                e.Reference(a => a.Student).Load();
                e.Reference(a => a.Teacher).Load();
            }
            return Account;
        }

        // return false co nghia id khong ton tai
        public int UpdateAccount(int id, Account NewAccount)

        {
            int n = 0;
            Account Account = ReadAAccount(id);
            if (Account == null) return n;
            Account.AccountTypeId = NewAccount.AccountTypeId;
            Account.Password = NewAccount.Password;
            Account.Username = NewAccount.Username;
            Account.Email = NewAccount.Email;

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
        public int DeleteAccount(int id)
        {
            int n = 0;
            Account Account = ReadAAccount(id);
            if (Account != null)
                DbContext.Accounts.Remove(Account);
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
