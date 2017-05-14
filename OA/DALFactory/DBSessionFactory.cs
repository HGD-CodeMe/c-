﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Remoting.Messaging;

namespace OA.DALFactory
{
    public class DBSessionFactory
    {

        public static IDAL.IDBSession CreateDbSession()
        {
            IDAL.IDBSession DbSession = (IDAL.IDBSession)CallContext.GetData("dbSession");
            if (DbSession == null)
            {
                DbSession = new DALFactory.DBSession();
                CallContext.SetData("dbSession", DbSession);
            }
            return DbSession;
        }
    }
}