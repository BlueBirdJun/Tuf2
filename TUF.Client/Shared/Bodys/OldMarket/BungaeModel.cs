using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUF.Client.Shared.Bodys.OldMarket
{
    public class BungaeModel
    {
        public string Title { get; set; }
        public string Price { get; set; }
        public string Pid { get; set; }
        public string product_image { get; set; }
        public string update_time { get; set; }

        public List<string> Images { get; set; }
        /// <summary>
        /// 배송비
        /// </summary>
        public bool ShipInclude { get; set; }
        public string RegTime { get; set; }
        public string ProductDesc { get; set; }
    }
}
