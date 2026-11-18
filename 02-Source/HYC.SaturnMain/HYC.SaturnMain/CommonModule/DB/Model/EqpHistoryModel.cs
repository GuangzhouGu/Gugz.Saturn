using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDB.Model
{
    public class EqpHistoryModel
    {
        public int EqpStatusType { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime? CancelTime { get; set; }
        public int StationNo { get; set; } = -1;
    }
}
