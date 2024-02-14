using Newtonsoft.Json;
using Synzeal_SAP_Stock_API.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Synzeal_SAP_Stock_API
{
    class Program
    {
        public static SynzealLiveEntities db = new SynzealLiveEntities();

        static void Main(string[] args)
        {
            WebClient wc = new WebClient();
            string inventoryjson = wc.DownloadString("http://synzeal.net:8282/KK/mGetBatchWiseItemStock");
            var inventoryData = JsonConvert.DeserializeObject<List<SZ_InventoryModel>>(inventoryjson);
            if(inventoryData != null && inventoryData.Count > 0)
            {
                inventoryData.ForEach(inv => {
                    var szInventory = db.SZ_Inventory.Where(x => x.BatchNo == inv.Batch).FirstOrDefault();
                    var product = db.Products.Where(x => x.Sku== inv.ItemNumber).FirstOrDefault();

                    if(product != null)
                    {
                        if (szInventory != null)
                        {
                            // Update Record
                            szInventory.Qty = Convert.ToDecimal(inv.BatchQty);
                            db.Entry(szInventory).State = EntityState.Modified;
                        }
                        //else {
                        //    // Insert Record
                        //    szInventory = new SZ_Inventory();
                        //    szInventory.Qty = Convert.ToDecimal(inv.BatchQty);
                        //    szInventory.BatchNo = inv.Batch;
                        //    szInventory.ProductId = product.Id;
                        //    szInventory.IsApproved = true;
                        //    szInventory.ApprovalDate = DateTime.Now;
                        //    szInventory.CreatedDate = DateTime.Now;
                        //}
                    }
                });
            }

            db.SaveChanges();
        }
    }
}
