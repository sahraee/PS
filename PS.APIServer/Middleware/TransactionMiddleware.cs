﻿
using PS.Persistence.Contexts;

namespace PS.PBService.MiddleWares
{
    public class TransactionMiddleware
    {
        private PSDBContext _ctx;
        private readonly RequestDelegate _next;

        public TransactionMiddleware(RequestDelegate next)
        {

            _next = next;
        }
        /// <summary>
        /// This method create transaction for methods
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext, PSDBContext context)
        {
            string httpVerb = httpContext.Request.Method.ToUpper();

            if (httpVerb == "POST" || httpVerb == "PUT" || httpVerb == "DELETE")
            {
                var strategy = context.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync<object, object>(null!, operation: async (dbctx, state, cancel) =>
                {
                    // start the transaction
                    await using var transaction = await context.Database.BeginTransactionAsync();

                    // invoke next middleware 
                    await _next(httpContext);

                    if (httpContext.Response.StatusCode == 200)

                        await transaction.CommitAsync();
                    else
                        await transaction.RollbackAsync();
                    return null!;
                }, null);
            }
            else
            {
                await _next(httpContext);
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TransactionMiddlewareExtensions
    {
        public static IApplicationBuilder UseTransactionMiddleware(this IApplicationBuilder builder)        {

            return builder.UseMiddleware<TransactionMiddleware>();
        }
    }
}
