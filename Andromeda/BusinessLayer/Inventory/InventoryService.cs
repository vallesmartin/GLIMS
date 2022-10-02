using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.BusinessLayer
{
    public partial class Services : IServices
    {
        public ApiResponse Inventory_Create(InvModel inventory)
        {
            throw new NotImplementedException();
        }
        public InvModel Inventory_GetOpen()
        {
            throw new NotImplementedException();
        }
        public ApiResponse InventoryLine_Create(InvLineModel line)
        {
            throw new NotImplementedException();
        }
    }
}