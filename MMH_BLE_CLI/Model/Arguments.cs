using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMH_BLE_CLI.Model {
    class Arguments {
        public bool is_vaild = true;
        public string address = "";
        public string name = "";
        public int timeout = Constants.DEFAULT_TIMEOUT;
        public bool json = false;
    }
}
