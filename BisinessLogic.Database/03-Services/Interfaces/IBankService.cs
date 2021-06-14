﻿using System.Collections.Generic;

namespace BisinessLogic.Database
{
    interface IBankService
    {
        IEnumerable<BankDTO> GetData();
        void Add(BankDTO item);
        void Update(BankDTO item);
        void Delete(BankDTO item);
        BankDTO GetWithInclude(BankDTO item);
    }
}
