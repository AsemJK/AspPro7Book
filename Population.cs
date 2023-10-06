namespace AspPro7Examples
{
    public class Population
    {
        private RequestDelegate? next;
        public Population() { }
        public Population(RequestDelegate requestDelegate)
        {
            next = requestDelegate;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            string[] parts = httpContext.Request.Path.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2 && parts[0] == "population")
            {
                string city = parts[1];
                int? pop = null;
                switch (city.ToLower())
                {
                    case "london":
                        pop = 8_136_000;
                        break;
                    case "paris":
                        pop = 2_141_000;
                        break;
                    case "monaco":
                        pop = 39_000;
                        break;
                }
                if (pop.HasValue)
                {
                    await httpContext.Response
                    .WriteAsync($"City: {city}, Population: {pop}");
                    return;
                }
            }
            if (next != null)
            {
                await next(httpContext);
            }
        }
    }
}