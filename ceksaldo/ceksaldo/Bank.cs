using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceksaldo
{

    public class Bank
    {
		List<Bank> BankList = new List<Bank>();
		public string username;

		public double Balance;

		public Bank(string username, double Balance)
		{
			this.username = username; 
			this.Balance = Balance;
		}
    }
}

