using System;
namespace test.Controllers
{
	public class response
	{
        public static object BaseResponse<T>(T obj)
        {
            var response = new
            {
                data = obj,
                itemsCount = 1,
                success = true,
                statusCode = 200,
                message = "Success"
            };

            return response;
        }

        public static object BaseResponse<T>(List<T> obj)
        {
            var response = new
            {
                data = obj,
                itemsCount = obj.Count,
                success = true,
                statusCode = 200,
                message = "Success"
            };

            return response;
        }

        public static object BaseResponse(int code,string msg)
        {
            bool flag;

            if (code == 200 || code == 201)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }

            var response = new
            {
                data = (Object) null,
                itemsCount = 0,
                success = flag,
                statusCode = code,
                message = msg
            };

            return response;
        }
    }
}

