using ChamThiDotnet5.Models;
using ChamThiWeb5.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChamThiDotnet5.DAO
{
    public class AccountDAO
    {
        AppDbContext DbContext = new AppDbContext();

        public void AddNewAccount(Account Account)
        {
            DbContext.Accounts.Add(Account);
            DbContext.SaveChanges();
        }

        public DbSet<Account> ReadAllAccount()
        {
            return DbContext.Accounts;

        }

        public Account ReadAAccount(int id)
        {
            var Account = from a in DbContext.Accounts where a.Id == id select a;
            return (Account)Account;
        }

        // return false co nghia id khong ton tai
        public bool UpdateAccount(int id, Account NewAccount)

        {
            Account Account = ReadAAccount(id);
            if (Account == null) return false;
            Account = NewAccount;
            DbContext.SaveChanges();
            return true;
        }
        public void DeleteAccount(int id)
        {
            Account Account = ReadAAccount(id);
            if (Account != null)
                DbContext.Accounts.Remove(Account);
            DbContext.SaveChanges();
        }
    }
}
