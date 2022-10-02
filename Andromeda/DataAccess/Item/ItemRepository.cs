using System.Collections.Generic;
using System.Linq;
using WebApp.Models;

namespace WebApp.DataAccess
{
    public partial class Repository : IRepository
    {
        #region Item
        public List<ItemModel> Item_GetAll()
        {
            using (var _context = new AndromedaContext())
            {
                var items = _context.Items.ToList();
                foreach (var item in items)
                {
                    item.EntityData = Entity_GetById(item.EntityId);
                }
                return items;
            }
            
        }
        public ItemModel Item_GetById(int id)
        {
            using (var _context = new AndromedaContext())
            {
                var item = _context.Items.Where(i => i.ItemId == id).First();
                item.EntityData = Entity_GetById(item.EntityId);
                return item;
            }
        }
        public bool Item_Update(ItemModel item)
        {
            using (var _context = new AndromedaContext())
            {
                var dbEnt = _context.Items.Where(e => e.ItemId == item.ItemId).First();

                dbEnt.ItemBarcode = item.ItemBarcode.Length > 0 ? item.ItemBarcode : dbEnt.ItemBarcode;
                dbEnt.ItemCost = item.ItemCost > 0 ? item.ItemCost : dbEnt.ItemCost;
                dbEnt.ItemDescription = item.ItemDescription.Length > 0 ? item.ItemDescription : dbEnt.ItemDescription;
                dbEnt.ItemDisabled = item.ItemDisabled != false ? item.ItemDisabled : dbEnt.ItemDisabled;
                dbEnt.ItemExternalCode = item.ItemExternalCode.Length > 0 ? item.ItemExternalCode : dbEnt.ItemExternalCode;
                dbEnt.ItemInternalCode = item.ItemInternalCode.Length > 0 ? item.ItemInternalCode : dbEnt.ItemInternalCode;
                dbEnt.ItemInternalDescription = item.ItemInternalDescription != null ? item.ItemInternalDescription.Length > 0 ? item.ItemInternalDescription : dbEnt.ItemInternalDescription : dbEnt.ItemInternalDescription;
                dbEnt.ItemOrder = item.ItemOrder > 0 ? item.ItemOrder : dbEnt.ItemOrder;
                dbEnt.ItemPackUnit = item.ItemPackUnit > 0 ? item.ItemPackUnit : dbEnt.ItemPackUnit;
                dbEnt.ItemPrice = item.ItemPrice > 0 ? item.ItemPrice : dbEnt.ItemPrice;
                dbEnt.ItemSugg = item.ItemSugg > 0 ? item.ItemSugg : dbEnt.ItemSugg;
                dbEnt.ItemSuggHigh = item.ItemSuggHigh > 0 ? item.ItemSuggHigh : dbEnt.ItemSuggHigh;
                dbEnt.ItemSuggLow = item.ItemSuggLow > 0 ? item.ItemSuggLow : dbEnt.ItemSuggLow;
                dbEnt.ItemSuggStep = item.ItemSuggStep > 0 ? item.ItemSuggStep : dbEnt.ItemSuggStep;

                _context.SaveChanges();
                Changes_Set(AndromedaContextObject.OBJ_ITEMS);
                return true;
            }
        }
        #endregion
    }
}