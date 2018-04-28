using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.ModelBuilder
{
    public class BookModelBuilder
    {
        [ModelBinderType(typeof(Book))]
        public class BookModelBinder : IModelBinder
        {
          
            HttpRequestBase request1;
            public BookModelBinder( HttpRequestBase request)
            {               
                this.request1 = request;
            }
          

            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                HttpRequestBase request = controllerContext.HttpContext.Request;
                request = request1;
                string title = "Title";
                string BookID = "BookID";
                string day = "Day";
                string month = "Month";
                string year = "Year";

                return new Book { BookID = BookID, Title = title + ":DI Test" + request.Form.Get("HttpRequestBaseDI"), Date = year + "-" + month + "-" + day };
            }
        }
    }
}