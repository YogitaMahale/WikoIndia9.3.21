using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Autofocus.Utility
{
    public static class SD
    {
        //public static HttpClient webApiClient = new HttpClient();
        //static SD()
        //{
        //    webApiClient.BaseAddress = new Uri("http://wikoindiawebapi.onlineerp.org/api/v1/");
        //    webApiClient.DefaultRequestHeaders.Clear();
        //    webApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //}
        public const string Role_Admin = "Admin";
        public const string Role_Buyer = "Buyer";
        public const string Role_Seller = "Seller";
        public const string Role_Employee = "Employee";

        //public const string Role_Vendor = "Vendor";
        //public const string Role_Account = "Account";

        // public static string APIBaseUrl = "https://localhost:44375/";




       public static string APIBaseUrl = "https://localhost:44368/";
     // public static string APIBaseUrl = "http://wikoindia.onlineerp.org/";
        //public static string APIBaseUrls = "https://wikoindia.onlineerp.org/";
        public static string MainCategoryAPIPath = APIBaseUrl + "api/v1/MainCategory";
        public static string SubCategoryAPIPath = APIBaseUrl + "api/v1/SubCategory";

        public static string ImageFolderPath = "http://wikoindia.onlineerp.org";
        //  public static string APIBaseUrl = "http://wikoindiawebapi.onlineerp.org/";
        //public static string MainCategoryAPIPath = APIBaseUrl+ "api/v1/MainCategory/";
        //public static string SubCategoryAPIPath = APIBaseUrl+ "api/v1/SubCategory/";

        //

    }
}


 