using ChamThiDotnet5.Models;
using ChamThiWeb5.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChamThiDotnet5.DAO
{
    public class StudentDAO
    {
        AppDbContext DbContext = new AppDbContext();
        public void AddNewAccountType(AccountType Type)
        {
            DbContext.AccountTypes.Add(Type);
            DbContext.SaveChanges();
        }
        public DbSet<AccountType> ReadAllAccountType(int id)
        {
            return DbContext.AccountTypes;
            
        }
        public AccountType ReadAAccountType(int id)
        {
            var accountType = from a in DbContext.AccountTypes where a.Id == id select a;
            return (AccountType)accountType;
        }
        public void DeleteAccountType(int id)
        {
            AccountType accountType = ReadAAccountType(id);
            if (accountType != null)
            DbContext.AccountTypes.Remove(accountType);
            DbContext.SaveChanges();
        }
    }
}
