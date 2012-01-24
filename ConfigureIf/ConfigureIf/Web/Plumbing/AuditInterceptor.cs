using Castle.DynamicProxy;

namespace Web.Plumbing
{
    public class AuditInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.Name == "OnActionExecuting")
            {
                AuditRequest(invocation);
            }

            invocation.Proceed();
        }

        public void AuditRequest(IInvocation invocation)
        {
            // Audit request details
        }
    }
}