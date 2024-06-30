using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ExceptionHandler
    {
        public static void Handle(Action action)
        {
            try
            {
                action();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    {
                        throw new Exception("Danh mục đã tồn tại. Vui lòng nhập tên danh mục khác.", ex);
                    }
                }
                // Log the error if needed
                LogError(ex);

                // Re-throw exception with detailed information
                throw new Exception("Có lỗi xảy ra khi thực hiện thao tác: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                // Log the error if needed
                LogError(ex);

                // Re-throw exception with detailed information
                throw new Exception("Có lỗi xảy ra khi thực hiện thao tác: " + ex.Message, ex);
            }
        }

        private static void LogError(Exception ex)
        {
            // Implement logging logic here
            Console.WriteLine("Lỗi: " + ex.Message);
            Console.WriteLine("Stack Trace: " + ex.StackTrace);
        }
    }
}
