using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DataAccess;

namespace WebApp.DataAccess
{
    public partial class Repository : IRepository
    {
        public int Inventory_Create(InvModel inventory)
        {
            using (var _context = new AndromedaContext())
            {
                _context.Inventory.Add(inventory);
                _context.SaveChanges();
                return inventory.InventoryId;
            }
        }
        public InvModel Inventory_GetById(int id)
        {
            using (var _context = new AndromedaContext())
            {
                InvModel inventory = _context.Inventory.Where(i => i.InventoryId == id).FirstOrDefault();
                List<InvLineModel> Detail = InventoryLine_GetLinesByInventory(inventory.InventoryId);
                inventory.Detail = Detail;
                return inventory;
            }
        }
        public InvModel Inventory_GetOpen()
        {
            using (var _context = new AndromedaContext())
            {
                InvModel inventory = _context.Inventory.Where(i => i.InventoryStatus == InventoryStatusEnum.GENERATED).FirstOrDefault();
                List<InvLineModel> Detail = InventoryLine_GetLinesByInventory(inventory.InventoryId);
                inventory.Detail = Detail;
                return inventory;
            }
        }
        public List<InvModel> Inventory_GetAll()
        {
            using (var _context = new AndromedaContext())
            {
                return _context.Inventory.ToList();
            }
        }
        public bool Inventory_UpdateById(InvModel inventory)
        {
            using (var _context = new AndromedaContext())
            {
                InvModel origRecord = _context.Inventory.Where(i => i.InventoryId == inventory.InventoryId).FirstOrDefault();
                origRecord.InventoryDescription = inventory.InventoryDescription;
                origRecord.InventoryEndDate = inventory.InventoryEndDate;
                origRecord.InventoryStatus = inventory.InventoryStatus;
                _context.SaveChanges();
                return true;
            }
        }
        public InvLineModel InventoryLine_GetById(int inventoryId, int id)
        {
            using (var _context = new AndromedaContext())
            {
                return _context.InventoryLine.Where(i => i.InventoryId == inventoryId && i.InventoryLineId == id).FirstOrDefault();
            }
        }
        public int InventoryLine_Create(InvLineModel line)
        {
            using (var _context = new AndromedaContext())
            {
                _context.InventoryLine.Add(line);
                _context.SaveChanges();
                return line.InventoryLineId;
            }
        }
        public bool InventoryLine_UpdateById(InvLineModel line)
        {
            using (var _context = new AndromedaContext())
            {
                InvLineModel origLine = _context.InventoryLine.Where(l => l.InventoryId == line.InventoryId && l.InventoryLineId == line.InventoryLineId).FirstOrDefault();
                origLine.InventoryLinePU = line.InventoryLinePU;
                origLine.InventoryLineQty = line.InventoryLineQty;
                origLine.InventoryLineUser = line.InventoryLineUser;
                origLine.InventoryLineItemId = line.InventoryLineItemId;
                origLine.ItemData = Item_GetById(line.InventoryLineItemId);
                _context.SaveChanges();
                return true;
            }
        }
        public List<InvLineModel> InventoryLine_GetLinesByInventory(int id)
        {
            using (var _context = new AndromedaContext())
            {
                return _context.InventoryLine.Where(i => i.InventoryId == id).ToList();
            }
        }
    }
}