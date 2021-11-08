using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Hepsiorada.Api.Helpers
{
    public class ApiResponseHelper
    {
        public static BaseApiResponse SuccessResponse()
        {
            var successResponse = new BaseApiResponse
            {
                Message = "Success",
                StatusCode = StatusCodes.Status200OK
            };

            return successResponse;
        }

        public static BaseApiResponse SuccessResponse<T>(T data)
        {
            var successResponse = new BaseApiResponse<T>
            {
                Message = "Success",
                StatusCode = StatusCodes.Status200OK,
                Data = data
            };

            return successResponse;
        }

        public static BaseApiResponse ErrorResponse(int errorCode)
        {
            var successResponse = new BaseApiResponse
            {
                Message = ((ApiErrorCode)errorCode).GetDescription(),
                StatusCode = errorCode
            };

            return successResponse;
        }

        public static BaseApiResponse ErrorResponse<T>(T data, int errorCode)
        {
            var successResponse = new BaseApiResponse<T>
            {
                Message = ((ApiErrorCode)errorCode).GetDescription(),
                StatusCode = errorCode,
                Data = data
            };

            return successResponse;
        }
    }
    public enum ApiErrorCode
    {
        [Description("Something Went Wrong")] ServerError = 500,
        [Description("Please Fill the Required Fields")] RequiredError = 400,
        [Description("Invalid or Empty Fields")] InvalidOrEmptyParamsError = 400,
        [Description("No Such Data")] NoSuchDataError = 404,
        [Description("Authorization Error")] AuthorizationError = 401,
        [Description("Expired Error")] ExpiredError = 498,
        [Description("Already Exist Error")] ExistError = 409
    }
    public static class EnumExtensions
    {
        /// <summary>
        /// Get the Description from the DescriptionAttribute.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            return enumValue.GetType()
                       .GetMember(enumValue.ToString())
                       .First()
                       .GetCustomAttribute<DescriptionAttribute>()?
                       .Description ?? string.Empty;
        }
    }
}
