using Microsoft.AspNetCore.Mvc;
using PoesiaFacil.Models;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PoesiaFacil.Controllers.Contracts
{
    public class CustomActionResult : IActionResult
    {
        private readonly object? _data;
        private readonly HttpStatusCode _statusCode;
        private ControllerResultViewModel _returnData = new ControllerResultViewModel();

        public CustomActionResult(HttpStatusCode statusCode, object? data = null)
        {
            _returnData.Data = data;
            _statusCode = statusCode;
        }
        public CustomActionResult(HttpStatusCode statusCode, List<string> list, bool isData = false)
        {
            if (isData == true)
                _returnData.Data = list;
            else
                _returnData.Errors = list;

            _statusCode = statusCode;
        }

        public CustomActionResult(HttpStatusCode statusCode, string content, bool isData = false)
        {
            if (isData == true)
                _returnData.Data = content;
            else
                _returnData.AddError(content);

            _statusCode = statusCode;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_returnData)
            {
                StatusCode = (int)_statusCode
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
