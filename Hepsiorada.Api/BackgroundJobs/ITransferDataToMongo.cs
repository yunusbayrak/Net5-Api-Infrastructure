using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiorada.Api.BackgroundJobs
{
    public interface ITransferDataToMongo
    {
        void TransferTopListedProducts();
        void TransferUserTopListedProducts();
    }
}
