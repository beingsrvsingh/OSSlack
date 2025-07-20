using System.Net.Http;
using System;
using Polly;
using Polly.Wrap;
using Polly.Retry;
using Polly.CircuitBreaker;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Resilience
{
    /// <summary>
    /// Provides Polly policies for resilience in HTTP requests.
    /// </summary>
    /// <remarks>
    /// This class encapsulates the creation of Polly policies that can be used to handle transient faults
    /// such as network issues or service unavailability when making HTTP requests.
    /// It includes retry policies with exponential backoff and circuit breaker policies to prevent overwhelming
    /// the service in case of repeated failures.
    /// </remarks>
    /// <example>
    /// <code>
    /// var policy = PollyPolicyProvider.CreatePolicy();
    /// var response = await policy.ExecuteAsync(() => httpClient.GetAsync("https://example.com/api"));
    /// </code>
    /// </example>
    /// <seealso cref="Polly"/>
    /// <seealso cref="HttpClient"/>
    /// <seealso cref="AsyncPolicyWrap{TResult}"/>
    /// <seealso cref="RetryPolicy"/>
    /// <seealso cref="CircuitBreakerPolicy"/>
    /// <seealso cref="HttpResponseMessage"/>
    /// <seealso cref="HttpRequestException"/>
    /// </summary> 
    public static class PollyPolicyProvider
    {
        public static AsyncPolicyWrap<HttpResponseMessage> CreatePolicy(
            int retryCount = 3,
            double initialBackoffSeconds = 2,
            int circuitBreakerFailures = 3,
            int circuitBreakerDurationSeconds = 30)
        {
            // Retry Policy with exponential backoff
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(retryCount,
                    attempt => TimeSpan.FromSeconds(Math.Pow(initialBackoffSeconds, attempt)));

            // Circuit Breaker Policy
            var circuitBreakerPolicy = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: circuitBreakerFailures,
                    durationOfBreak: TimeSpan.FromSeconds(circuitBreakerDurationSeconds));

            // Combine them
            return Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);
        }
    }
}
