using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using hringr.Models;

namespace hringr.Repository
{
	public class Repo
	{
        private static Repo _instance;

        public static Repo Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Repo();
                return _instance;
            }
        }

		static ApplicationDbContext m_db = new ApplicationDbContext();

	    public static ApplicationDbContext GetDbContext()
	    {
	        return m_db;	        
	    }
	}
}