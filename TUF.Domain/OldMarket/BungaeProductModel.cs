using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUF.Domain.OldMarket.Product;

// Root myDeserializedClass = JsonConvert.Deserializestring<Root>(myJsonResponse);
public class Bunpay
{
    public bool enabled { get; set; }
    public bool shipping { get; set; }
    public bool inPerson { get; set; }
}

public class Category
{
    public string id { get; set; }
    public string name { get; set; }
    public string imageUrl { get; set; }
}

public class Data
{
    public Product product { get; set; }
    //public List<string> banners { get; set; }
    public Bunpay bunpay { get; set; }
    //public List<string> badges { get; set; }
    //public List<string> benefitBadges { get; set; }
    public Shop shop { get; set; }
    public int shopProductCount { get; set; }
    //public List<ShopProduct> shopProducts { get; set; }
}

public class Geo
{
    public double lat { get; set; }
    public double lon { get; set; }
    public string address { get; set; }
    public string label { get; set; }
}

public class KeywordLink
{
    public string keyword { get; set; }
    public string appUrl { get; set; }
    public string imageUrl { get; set; }
    public bool emphasis { get; set; }
}

public class Metrics
{
    public int favoriteCount { get; set; }
    public int buntalkCount { get; set; }
    public int viewCount { get; set; }
    public int commentCount { get; set; }
}

public class Product
{
    public int pid { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int price { get; set; }
    public int qty { get; set; }
    public bool includeShippingCost { get; set; }
    public bool exchangeable { get; set; }
    public bool ad { get; set; }
    public string saleStatus { get; set; }
    public string status { get; set; }
    public List<string> keywords { get; set; }
    public string imageUrl { get; set; }
    public int imageCount { get; set; }
    public bool bunpayHope { get; set; }
    public Geo geo { get; set; }
    public Metrics metrics { get; set; }
    public Category category { get; set; }
    public string inspectionStatus { get; set; }
    public DateTime updatedAt { get; set; }
    public string updatedBefore { get; set; }
    public List<KeywordLink> keywordLinks { get; set; }
    public List<string> specLabels { get; set; }
}

public class Proshop
{
    public bool isProshop { get; set; }
    public bool isRestrictedCandidate { get; set; }
}

public class BungaeProductModel
{
    public Data data { get; set; }
}

public class Shop
{
    public int uid { get; set; }
    public string name { get; set; }
    public string imageUrl { get; set; }
    public int grade { get; set; }
    public int followerCount { get; set; }
    public bool isIdentified { get; set; }
    public Proshop proshop { get; set; }
    public DateTime joinDate { get; set; }
}

public class ShopProduct
{
    public int pid { get; set; }
    public string name { get; set; }
    public int price { get; set; }
    public bool bunpayHope { get; set; }
    public List<string> badges { get; set; }
    public string firstImageUrl { get; set; }
    public string inspectionStatus { get; set; }
    public bool care { get; set; }
}

