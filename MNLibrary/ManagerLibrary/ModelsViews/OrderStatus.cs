using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLibrary.ModelsViews
{
    internal enum OrderStatus
    {
    
            Submitted = 0,   // Đã gửi
            Confirmed = 1,   // Đã xác nhận
            Borrowed = 2,    // Đang mượn
            Returned = 3,    // Đã trả
            Reject = -1,     // Từ chối
            Overdue = -2,    // Quá hạn
           
    }
}
