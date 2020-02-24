using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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

        [HttpPost]
        [Route("api/Productsapi/Addproduct")]
        // POST: api/Products
        public HttpResponseMessage Addproduct([FromBody]Products product) 
        {
            List<Products> products = new List<Products>();
            products.Add(product);
            Cls_Product cls_product = new Cls_Product();
            string result = cls_product.Add_Product(products);
            if (result == "S")
            {
                return Request.CreateResponse(HttpStatusCode.OK, "success");


            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "fail");
            }
        }


        [HttpPost]
        [Route("api/Productsapi/AddproductList")]
        // POST: api/Products
        public HttpResponseMessage AddproductList([FromBody]JObject JsonString)
        {
            if (JsonString != null)
            {
                string a = "";

                try
                {
                    a = HttpContext.Current.Server.UrlDecode(JsonString["JsonString"].ToString());
                }
                catch
                {
                    a = HttpContext.Current.Server.UrlDecode(JsonString.ToString());
                }
                List<Products> products = new List<Products>();
                products = JsonConvert.DeserializeObject<List<Products>>(a);
                Cls_Product cls_product = new Cls_Product();
                string result = cls_product.Add_Product(products);
                if (result == "S")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "success");


                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "fail");
                }
            }
            else
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, "fail");
            }
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
