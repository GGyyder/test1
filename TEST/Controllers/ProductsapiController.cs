using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TEST.Models;

namespace TEST.Controllers
{
    public class ProductsapiController : ApiController
    {
        // GET: api/Products
        public IEnumerable<Products> Get()
        {
            Cls_Product cls_product = new Cls_Product();
            List<Products> products = new List<Products>();
            products = cls_product.GetAllProduct();

            return products;

        }

        // GET: api/Products/5
        public Products Get(int id)
        {

            Cls_Product cls_product = new Cls_Product();
            Products product= new Products();

            product = cls_product.GetProduct(id.ToString());
            return product;
        }

        // POST: api/Products
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
