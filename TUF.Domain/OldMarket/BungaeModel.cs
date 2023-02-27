using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUF.Domain.OldMarket;

 

public class AssociateKeyword
{
    public string name { get; set; }
    public int rank { get; set; }
}

public class Category
{
    public string id { get; set; }
    public int count { get; set; }
    public string title { get; set; }
    public int order { get; set; }
    public string icon_url { get; set; }
    public List<Category> categories { get; set; }
    public int need_size_tag { get; set; }
    public bool require_size { get; set; }
    public bool require_brand { get; set; }
    public bool disable_price { get; set; }
    public bool disable_quantity { get; set; }
    public bool disable_bunpay { get; set; }
    public bool disable_inspection { get; set; }
}

public class Filters
{
    public string q { get; set; }
    public string page { get; set; }
}

public class Flag
{
    public string type { get; set; }
    public string label { get; set; }
    public string event_type { get; set; }
}

public class List
{
    public string pid { get; set; }
    public string name { get; set; }
    public string price { get; set; }
    public string product_image { get; set; }
    public string status { get; set; }
    public bool ad { get; set; }
    public string inspection { get; set; }
    public bool bun_pay_filter_enabled { get; set; }
    public bool care { get; set; }
    public string location { get; set; }
    public List<string> badges { get; set; }
    public string name_prefix { get; set; }
    public bool bizseller { get; set; }
    public bool checkout { get; set; }
    public bool contact_hope { get; set; }
    public bool free_shipping { get; set; }
    public bool is_adult { get; set; }
    public string is_super_up_shop { get; set; }
    public string max_cpc { get; set; }
    public string num_comment { get; set; }
    public string num_faved { get; set; }
    public bool only_neighborhood { get; set; }
    public string outlink_url { get; set; }
    public string pu_id { get; set; }
    public string style { get; set; }
    public string super_up { get; set; }
    public string tag { get; set; }
    public string uid { get; set; }
    public int update_time { get; set; }
    public int used { get; set; }
    public bool proshop { get; set; }
    public string category_id { get; set; }
    public string ref_campaign { get; set; }
    public string ref_code { get; set; }
    public string ref_medium { get; set; }
    public string ref_content { get; set; }
    public string ref_source { get; set; }
    public string ref_test { get; set; }
    public string imp_id { get; set; }
    public string ad_ref { get; set; }
    public bool faved { get; set; }
}

public class RecommendedCategory
{
    public string id { get; set; }
    public int count { get; set; }
    public string title { get; set; }
    public int order { get; set; }
    public string icon_url { get; set; }
    public int need_size_tag { get; set; }
    public bool require_size { get; set; }
    public bool require_brand { get; set; }
    public bool disable_price { get; set; }
    public bool disable_quantity { get; set; }
    public bool disable_bunpay { get; set; }
    public bool disable_inspection { get; set; }
}

public class BungaeModel
{
    public string result { get; set; }
    public bool no_result { get; set; }
    public string no_result_type { get; set; }
    public string no_result_message { get; set; }
    public bool ad_more_info { get; set; }
    public List<string> ad_products { get; set; }
    public List<List> list { get; set; }
    public List<string> specs { get; set; }
    public Filters filters { get; set; }
    public List<Category> categories { get; set; }
    public List<RecommendedCategory> recommended_categories { get; set; }
    public List<AssociateKeyword> associate_keywords { get; set; }
    public int n { get; set; }
    public int num_found { get; set; }
    public string redirect_uri { get; set; }
    public bool shopingmoa_enable { get; set; }
    public string view_type { get; set; }
    public List<Flag> flags { get; set; }
    public string filter_bun_pay { get; set; }
    public string filter_new_arrival { get; set; }
}