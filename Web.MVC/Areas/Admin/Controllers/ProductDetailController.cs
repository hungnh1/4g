using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnF;
using System.IO;
namespace baohiem.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductDetailController : Controller
    {
        private Sim4GEntities db = new Sim4GEntities();

        // GET: /Admin/ProductDetail/
        public ActionResult Index(int ProductGroupId)
        {
            var prodlist = db.Products.Where(p => p.ProductGroupID == ProductGroupId).OrderByDescending(p => p.ProductId).ToList();
            ViewBag.ProductGroupName = db.ProductGroups.Find(ProductGroupId).Name;
            ViewBag.ProductGroupID = ProductGroupId;
            return View(prodlist);
        }
        public ActionResult ShowProdct()
        {
            var prodlist = (from prt in db.Products
                            join pg in db.ProductGroups on prt.ProductGroupID
                           equals pg.ProductGroupId
                            where pg.IsDefault == true
                            select prt).OrderByDescending(o => o.ProductId).ToList();
            if(prodlist.Count()==0)
            {
                ViewBag.ProductGroupID = db.ProductGroups.Where(p => p.IsDefault == true).First().ProductGroupId;
            }
            else
            {
                ViewBag.ProductGroupID = prodlist.First().ProductGroupID;
            }
           
           
            return View(prodlist);
        }
        
        // GET: /Admin/ProductDetail/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Admin/ProductDetail/Create
        public ActionResult Create(int ProductGroupId)
        {
            ViewBag.ProductGroupId = ProductGroupId;
            return View();
        }

        // POST: /Admin/ProductDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            ProductGroup productGroup = db.ProductGroups.Find(product.ProductGroupID);
            product.ProductGroupName = productGroup.Name;
            db.Products.Add(product);
            db.SaveChanges();
            int indexfile = 0;
            foreach (string fl in Request.Files)
            {
                if (Request.Files[fl].ContentLength != 0)
                {
                    indexfile++;
                    if (Request.Files[fl].FileName.Contains(".jpg") || Request.Files[fl].FileName.Contains(".png")
                    || Request.Files[fl].FileName.Contains(".gif"))
                    {
                        string pathToSaveimage = Server.MapPath("/Storedata/Product/" + product.ProductId);//Phần vị trí lưu File .
                        CreateFolder(pathToSaveimage);
                        string filename = indexfile.ToString() + ".png";
                        Request.Files[fl].SaveAs(Path.Combine(pathToSaveimage, filename));
                        product.Image = "Storedata/Product/" + product.ProductId + "/" + filename;
                        db.SaveChanges();
                     
                    }
                }
            }
            //var listimg = "";
            //var ImageList = db.ProductImages.Where(p => p.ProductId == product.ProductId).ToList();
            //foreach (var il in ImageList)
            //{
            //    listimg += "{ \"Image\": \"" + il.Image + "\",\"ProductId\": " + il.ProductId + "},";
            //}
            //product.ImageList = "{ \"imalist\":[" + listimg + "] }";
            //db.SaveChanges();
            if (productGroup.IsDefault == true)
            {
                return RedirectToAction("ShowProdct", new { ProductGroupID = product.ProductGroupID });
            }
            return RedirectToAction("Index", new { ProductGroupID = product.ProductGroupID });
        }

        // GET: /Admin/ProductDetail/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductImage = db.ProductImages.Where(p => p.ProductId == product.ProductId).ToList();
            return View(product);
        }

        // POST: /Admin/ProductDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {

                foreach (string fl in Request.Files)
                {
                    if (Request.Files[fl].ContentLength != 0)
                    {
                        if (fl == "fileUpload")
                        {
                            if (Request.Files[fl].FileName.Contains(".jpg") || Request.Files[fl].FileName.Contains(".png")
             || Request.Files[fl].FileName.Contains(".gif"))
                            {
                                string pathToSaveimage = Server.MapPath("/Storedata/Product/" + product.ProductId);//Phần vị trí lưu File .
                                CreateFolder(pathToSaveimage);

                                string filename = "1.png";
                                Request.Files[fl].SaveAs(Path.Combine(pathToSaveimage, filename));
                                product.Image = "Storedata/Product/" + product.ProductId + "/" + filename;


                            }
                            continue;
                        }



                    }
                }


                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                ProductGroup productGroup = db.ProductGroups.Find(product.ProductGroupID);
                if (productGroup.IsDefault == true)
                {
                    return RedirectToAction("ShowProdct", new { ProductGroupID = product.ProductGroupID });
                }
                return RedirectToAction("Index", new { ProductGroupID = product.ProductGroupID });
            }
            return View(product);
        }

        public ActionResult DelImage(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImg = db.ProductImages.Find(id);
            if (productImg == null)
            {
                return HttpNotFound();
            }
            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory+ productImg.Image);
            db.ProductImages.Remove(productImg);
            db.SaveChanges();
            ///

            ///

            var listimg = "";
            var ImageList = db.ProductImages.Where(p => p.ProductId == productImg.ProductId).ToList();
            foreach (var il in ImageList)
            {
                listimg += "{ \"Image\": \"" + il.Image + "\",\"ProductId\": " + il.ProductId + "},";
            }
            var product = db.Products.Find(productImg.ProductId);
            product.ImageList = "{ \"imalist\":[" + listimg + "] }";

            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Edit", new { id = product.ProductId });

        }
        // GET: /Admin/ProductDetail/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            db.Products.Remove(product);
            db.SaveChanges();
            ProductGroup productGroup = db.ProductGroups.Find(product.ProductGroupID);
            if (productGroup.IsDefault == true)
            {
                return RedirectToAction("ShowProdct", new { ProductGroupID = product.ProductGroupID });
            }
            return RedirectToAction("Index", new { ProductGroupID = product.ProductGroupID });

        }

        // POST: /Admin/ProductDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
