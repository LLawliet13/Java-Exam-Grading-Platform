using ChamThiDotnet5.Models;
using System;

namespace ChamThiDotnet5.Services
{
    public class AccountServiceImpl : AccountService
    {
        public Account Login()
        {
            return new Account();
        }
        public string start()
        {
            return "Method 1 in account service";
        }
    }
}
